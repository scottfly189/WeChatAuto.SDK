using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using WeAutoCommon.Utils;
using System;
using WeAutoCommon.Models;
using OneOf;
using WeChatAuto.Utils;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using FlaUI.Core.Tools;
using WeAutoCommon.Exceptions;
using FlaUI.UIA3;
using WeAutoCommon.Simulator;
using System.Threading.Tasks;
using FlaUI.Core.Capturing;
using WeAutoCommon.Enums;
using WeChatAuto.Extentions;
using WeChatAuto.Models;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.IO;
using WeChatAuto.Options;
using RapidOCRLib;
using System.Threading.Channels;


namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端
    /// 适用于单个微信客户端的自动化操作
    /// </summary>
    public partial class WeChatClient : IDisposable
    {
        private readonly AutoLogger<WeChatClient> _logger;
        private IServiceProvider serviceProvider;
        private volatile bool _disposed = false;
        private Navigation _Navigation;
        private UIThreadInvoker _MainThreadInvoker;
        private ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        private SemaphoreSlim monitorEvent;

        #region 比较稳定的字段
        public readonly Window MainWindow;
        public readonly int ClientProcessId;
        public readonly WeChatClientFactory Factory;
        public readonly string NickName;
        public readonly string WxId;
        public readonly string AvatorPath;
        public readonly int WechatIndex;
        public UIThreadInvoker MainThreadInvoker => _MainThreadInvoker;
        public IServiceProvider Provider => serviceProvider;
        #endregion

        #region 系统消息监听Channel
        private readonly Channel<(SystemMonitorOption option, List<string> messaages)> _SystemMonitorChannel = Channel.CreateBounded<(SystemMonitorOption option, List<string> messaages)>(new BoundedChannelOptions(100)
        {
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.Wait,
        });
        public Channel<(SystemMonitorOption option, List<string> messaages)> SystemMonitorChannel => _SystemMonitorChannel;
        private int _SystemMonitorChannelStarted = 0;
        private readonly CancellationTokenSource _ActionTokenSource = new CancellationTokenSource();
        private Task _SystemMonitorTask;
        #endregion

        /// <summary>
        /// 构造器
        /// 不应该自行调用,应该通过<see cref="WeChatClientFactory.GetWeChatClient"/>方法来获取
        /// </summary>
        /// <param name="clientProcessId"></param>
        /// <param name="provider"></param>
        /// <param name="factory"></param>
        /// <param name="window"></param>
        /// <param name="uIThreadInvoker"></param>
        /// <param name="ownerInfo">个人信息</param>
        /// <param name="index">微信在任务栏的索引</param>
        /// <param name="monitorEvent">任务统一器</param>
        public WeChatClient(int clientProcessId, IServiceProvider provider, WeChatClientFactory factory,
         Window window, UIThreadInvoker uIThreadInvoker, OwerInfo ownerInfo, int index, SemaphoreSlim monitorEvent)
        {
            this.monitorEvent = monitorEvent;
            this._MainThreadInvoker = uIThreadInvoker;
            this.MainWindow = window;
            this.serviceProvider = provider;
            this.Factory = factory;
            this.ClientProcessId = clientProcessId;
            this.NickName = ownerInfo.NickName;
            this.WxId = ownerInfo.WxId;
            this.AvatorPath = ownerInfo.AvatorPath;
            this.WechatIndex = index;
            _logger = provider.GetRequiredService<AutoLogger<WeChatClient>>();
            _Initialize();
        }

        private void _Initialize()
        {
            this.Navigation.SwitchNavigationCore(null, NavigationType.微信);
            this.ToolBar = new ToolBar(this.MainWindow, this.MainThreadInvoker, serviceProvider);
            this.Conversations = new ConversationList(this, this._MainThreadInvoker, serviceProvider);
            this.ChatContent = new ChatContent(this, this._MainThreadInvoker, serviceProvider);
            this.AddressBookList = new AddressBookList(this, this._MainThreadInvoker, serviceProvider);
            this.MessageMonitor = new MessageMonitor(this, serviceProvider, _MainThreadInvoker, monitorEvent);
            this.NewFriendMonitor = new NewFriendMonitor(this, serviceProvider, _MainThreadInvoker, monitorEvent);
            this.NotifyIcon = new ShellNotifyIcon(this, serviceProvider, WechatIndex);
            this.OwnerGroup = new OwnerGroup(this, _MainThreadInvoker, serviceProvider);
            this.OuterGroup = new OuterGroup(this, _MainThreadInvoker, serviceProvider);
            this.Moments = new Moments(this, _MainThreadInvoker, serviceProvider);
            this.Search = new Search(this, _MainThreadInvoker, serviceProvider);
            this.CacheManager = new CacheManager(this);
            _RunCheckAddressBook();
        }

        internal async Task _InitializeSystemMonitorConsumption()
        {
            if (Interlocked.CompareExchange(ref _SystemMonitorChannelStarted, 1, 0) == 1)
                return;
            TaskCompletionSource tcs = new TaskCompletionSource();
            _SystemMonitorTask = Task.Run(async () =>
            {
                try
                {
                    tcs.SetResult();
                    var token = _ActionTokenSource.Token;
                    await foreach (var message in _SystemMonitorChannel.Reader.ReadAllAsync(token))
                    {
                        try
                        {
                            await monitorEvent.WaitAsync(token);
                            try
                            {
                                if (message.option.CallBack != null)
                                {
                                    await SystemMonitorConsumptionActionCore(message);
                                }
                            }
                            finally
                            {
                                monitorEvent.Release();
                            }
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"系统消息消费者发生错误，错误原因:{ex.ToString()}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    tcs?.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    _logger.Error($"系统消息消费者发生错误，错误原因:{ex.ToString()}");
                    tcs?.TrySetException(ex);
                }
            });
            await tcs.Task;
        }

        private async Task SystemMonitorConsumptionActionCore((SystemMonitorOption option, List<string> messaages) message)
        {
            SystemMessageContext context = new SystemMessageContext(message.messaages, this, this.serviceProvider, message.option.Who);
            await message.option.CallBack.Invoke(context);
        }

        private void _RunCheckAddressBook()
        {
            if (WeAutomation.Config.InitAdressBook)
            {
                var path = Path.Combine(AppContext.BaseDirectory, this.WxId + "_cache.dat");
                if (File.Exists(path))
                    return;
                this.GetAllFriends(false).GetAwaiter().GetResult();
            }
        }

        #region POM对象
        /// <summary>
        /// 导航栏, 参考: <see cref="Navigation"/>
        /// </summary>
        public Navigation Navigation => GetNavigation();
        /// <summary>
        /// 微信ToolBar,参考:<see cref="ToolBar"/>
        /// </summary>
        public ToolBar ToolBar;
        /// <summary>
        /// 会话管理对象,参考:<see cref="ConversationList"/>
        /// </summary>
        public ConversationList Conversations;
        /// <summary>
        /// ChatContent对象,参考:<see cref="ChatContent"/>
        /// </summary>
        public ChatContent ChatContent;
        /// <summary>
        /// 通讯录对象，参考:<see cref="AddressBookList"/>
        /// </summary>
        public AddressBookList AddressBookList;
        /// <summary>
        /// 消息监听器对象，参考:<see cref="MessageMonitor"/>
        /// </summary>
        public MessageMonitor MessageMonitor;
        /// <summary>
        /// 任务栏操作对象，参考<see cref="ShellNotifyIcon"/>
        /// </summary>
        public ShellNotifyIcon NotifyIcon;
        /// <summary>
        /// 通过新好友添加好友监听器
        /// </summary>
        public NewFriendMonitor NewFriendMonitor;

        public Search Search;

        /// <summary>
        /// 得到OCR引擎
        /// </summary>
        public OCRService OcrEngee => serviceProvider.GetRequiredService<OCRService>();
        /// <summary>
        /// 自有群
        /// </summary>
        public OwnerGroup OwnerGroup;

        /// <summary>
        /// 外部群
        /// </summary>
        public OuterGroup OuterGroup;

        /// <summary>
        /// 朋友圈
        /// </summary>
        public Moments Moments;

        /// <summary>
        /// cache管理器
        /// </summary>
        public CacheManager CacheManager;


        #endregion
        private Navigation GetNavigation()
        {
            _Navigation = new Navigation(this, this.MainThreadInvoker, this.serviceProvider);
            return _Navigation;
        }


        #region 个人信息
        /// <summary>
        /// 获取本微信的个人信息，包括头像文件位置，wxid,昵称
        /// </summary>
        /// <returns>返回<see cref="OwerInfo"/></returns>
        public OwerInfo GetOwerInfo()
        {
            return new OwerInfo
            {
                AvatorPath = this.AvatorPath,
                WxId = this.WxId,
                NickName = this.NickName,
            };
        }
        /// <summary>
        /// 保存头像至其他路径
        /// </summary>
        /// <param name="path">待保存的头像路径</param>
        /// <returns></returns>
        public async Task SaveOwnerAvator(string path) => await this.Navigation.SaveOwnerAvator(path);
        #endregion

        #region 窗口管理
        /// <summary>
        /// 最大化微信窗口
        /// </summary>
        public async Task Max() => await ToolBar.Max();
        /// <summary>
        /// 还原微信窗口
        /// </summary>
        public async Task Restore() => await ToolBar.Restore();
        /// <summary>
        /// 置顶微信窗口
        /// </summary>
        /// <returns></returns>
        public async Task Pinned() => await ToolBar.Top(true);
        /// <summary>
        /// 取消置顶微信窗口
        /// </summary>
        /// <returns></returns>
        public async Task UnPinned() => await ToolBar.Top(false);
        /// <summary>
        /// 使主窗口获取焦点
        /// </summary>
        /// <returns></returns>
        public async Task Focus()
        {
            this.MainWindow.Focus();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 关闭查询窗口,如果查询窗口打开则关闭，如果查询窗口没有打开，则不作动作
        /// </summary>
        /// <param name="who">关闭谁的查询窗口</param>
        /// <returns></returns>
        public async Task CloseSearchWindow(string who) => await this.ChatContent.CloseSearchWindow(who);

        /// <summary>
        /// 移动窗口至主窗口的中间
        /// </summary>
        /// <param name="window"></param>
        public void MoveWinToMainCenter(Window window)
        {
            if (window == null)
                return;
            window.Focus();
            RandomWait.Wait(100, 600);
            window.Move(
                this.MainWindow.BoundingRectangle.X + (int)((this.MainWindow.BoundingRectangle.Width - window.BoundingRectangle.Width) / 2),
                this.MainWindow.BoundingRectangle.Y + (int)((this.MainWindow.BoundingRectangle.Height - window.BoundingRectangle.Height) / 2)
            );
            RandomWait.Wait(300, 900);
        }

        /// <summary>
        /// 打开who指定的子窗口
        /// </summary>
        /// <param name="who"></param>
        /// <returns></returns>
        public async Task<Window> OpenSubWin(string who) => await this.Conversations.OpenSubWin(who);
        /// <summary>
        /// 如果子窗口存在，关闭子窗口，如果不存在，则不做动作。
        /// </summary>
        /// <param name="who">好友、群的昵称</param>
        /// <returns></returns>
        public async Task CloseSubWin(string who) => await this.Conversations.CloseSubWin(who);
        /// <summary>
        /// 得到本微信窗口句柄
        /// </summary>
        /// <returns></returns>
        public nint GetHandler() => this.MainWindow.Properties.NativeWindowHandle.Value;
        /// <summary>
        /// 得到本微信窗口的进程id
        /// </summary>
        /// <returns></returns>
        public nint GetProcessId() => this.MainWindow.Properties.ProcessId.Value;

        #endregion

        #region Navigator管理
        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型,请参见枚举类型<seealso cref="NavigationType"/></param>
        public async Task SwitchNavigation(NavigationType navigationType) => await this.Navigation.SwitchNavigation(navigationType);

        /// <summary>
        /// 关闭通过导航栏打开的窗口.
        /// 仅支持聊天文件、朋友圈、视频号、看一看、搜一搜、小程序面板等窗口
        /// </summary>
        /// <param name="navigationType">导航栏类型,请参见枚举类型<seealso cref="NavigationType"/></param>
        public async Task CloseNavWin(NavigationType navigationType) => await this.Navigation.CloseNavWin(navigationType);
        /// <summary>
        /// 点击任务栏微信图标
        /// </summary>
        /// <param name="index">图标索引，从1开始,索引范围不能越界</param>
        /// <returns></returns>
        public async Task ClickNotifyIcon(int index) => (await this.NotifyIcon.GetButtons())[index - 1].Click();
        /// <summary>
        /// 点击指定微信名称的任务栏图标
        /// </summary>
        /// <param name="WechatName">微信名称</param>
        /// <returns></returns>
        public async Task ClickNotifyIcon(string WechatName)
        {
            var client = this.Factory.GetWeChatClient(WechatName);
            var button = await client.NotifyIcon.GetButton();
            button.Click();
        }
        #endregion

        #region 会话管理
        /// <summary>
        /// 获取会话列表所有会话的标题
        /// 考虑到效率，只返回名称列表
        /// </summary>
        /// <returns>所有会话列表的标题</returns>
        public async Task<List<string>> GetAllConversations() => await this.Conversations.GetAllConversations();

        /// <summary>
        /// 获取会话列表可见会话标题
        /// </summary>
        /// <returns>可见的会话列表的标题列表</returns>
        public async Task<List<string>> GetVisibleConversationTitles() => await this.Conversations.GetVisibleConversationTitles();

        /// <summary>
        /// 获取可见会话列表
        /// 会话信息包含：会话名称、会话未读消息数、会话头像等具体信息，请参考<see cref="SimpleConversation"/>
        /// </summary>
        /// <returns>返回<see cref="SimpleConversation"/>列表</returns>
        public async Task<List<SimpleConversation>> GetVisibleConversations() => await this.Conversations.GetVisibleConversations();

        /// <summary>
        /// 搜索好友/群聊
        /// </summary>
        /// <param name="who">待搜索的好友/群聊昵称,who - 微信会话列表肉眼可见的名称,如果群有备注，则这个who即为备注名</param>
        /// <returns>如果找到，返回true,如果没有找到，则返回false.</returns>
        public async Task<bool> SearchFriend(string who) => await this.Conversations.Search(who);

        /// <summary>
        /// 定位会话
        /// 定位会话的用途：可以将会话列表滚动到指定会话的位置，使指定会话可见
        /// </summary>
        /// <param name="title">会话标题</param>
        /// <returns>如果找到会话，则返回true，否则返回false</returns>
        public async Task<bool> LocateConversation(string title) => await this.Conversations.LocateConversation(title);

        /// <summary>
        /// 会话列表向上滚动，并且执行需要的业务逻辑
        /// </summary>
        /// <param name="callBack">
        /// <para>滚动过程中的回调，建议：如果处理业务结束，返回false,意味着不向上滚动，如果业务没有处理到，则返回true,继续滚动</para>
        /// <para>参数中的Rectangle为会话列表容器的<see cref="Rectangle"/>,如果超出容器Rectangle范围，应该返回true继续滚动，直到可以点击为止</para>
        /// </param>
        /// <returns></returns>
        public async Task Up(Func<AutomationElement[], Rectangle, bool> callBack) => await this.Conversations.Up(callBack);

        /// <summary>
        /// 会话列表滚动向下滚动，并且执行需要的业务逻辑
        /// <param name="callBack">
        /// <para>滚动过程中的回调，建议：如果处理业务结束，返回false,意味着不向上滚动，如果业务没有处理到，则返回true,继续滚动</para>
        /// <para>参数中的Rectangle为会话列表容器的<see cref="Rectangle"/>,如果超出容器Rectangle范围，应该返回true继续滚动，直到可以点击为止</para>
        /// </param>
        /// </summary>
        /// <returns></returns>
        public async Task Down(Func<AutomationElement[], Rectangle, bool> callBack) => await this.Conversations.Down(callBack);

        /// <summary>
        /// 设置会话消息免打扰
        /// </summary>
        /// <param name="setting">如果为:true,则设置会话消息免打扰，如果为:false,则：允许消息通知</param>
        /// <param name="who">要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口设置免打扰</param>
        /// <returns>执行消息免打扰结果</returns>
        public async Task<bool> SetDoNotDisturb(string who, bool setting = true) => await this.Conversations.SetDoNotDisturb(who, setting);
        /// <summary>
        /// 设置会话置顶
        /// </summary>
        /// <param name="setting">true:聊天置顶;false:取消聊天置顶</param>
        /// <param name="who">要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口设置置顶</param>
        /// <returns>执行会话置顶结果</returns>
        public async Task<bool> SetTopMost(string who, bool setting = true) => await this.Conversations.SetTopMost(who, setting);

        #endregion

        #region 消息管理
        /// <summary>
        /// 获取当前窗口的标题对象
        /// </summary>
        /// <returns>标题对象，请参考:<see cref="HeaderInfo"/></returns>
        public async Task<HeaderInfo> GetTitle() => await this.ChatContent.ChatHeader.GetTitle();

        /// <summary>
        /// 当前窗口的Sender输入区域点击，以获得焦点，也可以取消系统的消息提醒或者关闭右侧Pane等作用
        /// </summary>
        /// <returns></returns>
        public async Task FocuseSenderInput() => await this.ChatContent.FocuseSenderInput();

        /// <summary>
        /// 获取当前标窗的标题
        /// </summary>
        /// <returns>当前窗口的标题名称</returns>
        public async Task<string> GetOnlyTitle() => await this.ChatContent.ChatHeader.GetOnlyTitle();
        /// <summary>
        /// 发送文本消息,可以是群聊名称或者好友名称，名称可以为空，如果为空，则给当前聊天窗口发送消息
        /// </summary>
        /// <param name="who">好名/群聊的名称,也就是肉眼所见的标题</param>
        /// <param name="message">消息内容，文本消息内容</param>
        /// <param name="atUser">被@的好友,可以多个</param>
        /// <param name="refer">引用的对话内容,请参考<see cref="ChatRefer"/></param>
        public async Task SendMessage(string who, string message, OneOf<string, string[], List<string>> atUser = default, ChatRefer refer = null)
            => await ChatContent.SendMessage(who, message, atUser, refer);
        /// <summary>
        /// <para>发送消息,给当前窗口发送消息</para>
        /// <para>注意：仅给当前窗口发送消息，要注意当前聊天窗口可用</para>
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友</param>
        /// <param name="refer">引用的对话内容,请参考<see cref="ChatRefer"/></param>
        public async Task SendMessage(string message, OneOf<string, string[], List<string>> atUser = default, ChatRefer refer = null)
            => await ChatContent.SendMessage(message, atUser, refer);

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="who">好友/群聊，可以为空,如果为空，则发送到当前聊天窗口</param>
        /// <param name="files">文件路径列表</param>
        public async Task SendFile(string who, string[] files) => await ChatContent.SendFile(who, files);

        /// <summary>
        /// 发送表情    
        /// </summary>  
        /// <param name="who">被发送消息的好友名称/群聊名称</param>
        /// <param name="emoji">表情名称或者描述或者索引,具体请参见<see cref="EmojiListHelper"/></param>
        /// <param name="atUserList">被@的好友列表</param>
        public async Task SendEmoji(string who, OneOf<int, string> emoji, List<string> atUserList = null) => await ChatContent.SendEmoji(who, emoji, atUserList);

        /// <summary>
        /// 发起单人语音聊天
        /// </summary>
        /// <param name="who">好友昵称,可以为空，如果为空，则发送到当前聊天窗口</param>
        public async Task SendVoiceChat(string who) => await ChatContent.SendVoiceChat(who);

        /// <summary>
        /// 发起单人视频聊天
        /// </summary>
        /// <param name="who">好友昵称,可以为空，如果为空，则发送到当前聊天窗口</param>
        public async Task SendVedioChat(string who) => await ChatContent.SendVedioChat(who);
        /// <summary>
        /// 发起多人语音聊天，适用于群聊发起语音聊天
        /// </summary>
        /// <param name="who">群聊名称,可以为空，如果为空，则发送到当前聊天窗口</param>
        /// <param name="partner">参与者，好友昵称列表,必须是群聊成员</param>
        public async Task SendVoiceChats(string who, string[] partner) => await ChatContent.SendVoiceChats(who, partner);
        /// <summary>
        /// 发送语音消息,此功能依赖虚拟声卡：Cable input/Cable output
        /// 请在声音-->设置-->将输入设备改成: Cable output
        /// 如果没有安装虚拟声卡，请在:https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip下载
        /// </summary>
        /// <param name="who">好友昵称或群聊名称</param>
        /// <param name="filePath">语音文件路径</param>
        public async Task SendVoiceMessage(string who, string filePath) => await ChatContent.SendVoiceMessage(who, filePath);
        /// <summary>
        /// 给本聊天窗口发送语音消息，请确保本聊天窗口可用.
        /// 请在声音-->设置-->将输入设备改成: Cable output
        /// 如果没有安装虚拟声卡，请在:https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip下载
        /// </summary>
        /// <param name="filePath">语音文件路径</param>
        /// <returns></returns>
        public async Task SendVoiceMessage(string filePath) => await ChatContent.SendVoiceMessage(filePath);
        /// <summary>
        /// 文字转语音发送。
        ///
        /// 工作原理：通过音频大模型将文字转换成语音，然后通过微信发送给指定的好友或群聊。
        ///
        /// 注：系统默认支持阿里千问 Qwen3-TTS 系列模型。
        ///
        /// 为什么选择阿里千问 Qwen3-TTS 系列模型？
        /// 1. 阿里千问 Qwen3-TTS 系列在国际语音合成领域也处于第一梯队；
        /// 2. 完美支持声音克隆、声音设计，并可以通过指令方便地控制语速、情感和语言风格；
        ///    生成的语音聊天自然，可以实现停顿、笑声等效果，为未来的 AI 电话/语音聊天做准备。
        /// </summary>
        /// <param name="apiKey">
        /// 千问 API Key。
        /// 申请地址：
        /// <see href="https://bailian.console.aliyun.com/?spm=a2c4g.11186623.0.0.3f801457p6h0qM&amp;tab=model#/api-key">
        /// 阿里云百炼控制台
        /// </see>
        /// </param>
        /// <param name="who">
        /// 好友或者群聊，可以为空。
        /// 如果为空，则使用当前焦点聊天窗口。
        /// </param>
        /// <param name="message">
        /// 要转换成语音的文本消息。
        /// </param>
        /// <param name="options">
        /// 声音选项，用于指定模型、音色等参数。
        /// </param>
        /// <param name="optimizeWithLlm">
        /// 待发送的消息是否需要通过 LLM 进行优化。
        /// 由于文字内容有时候与实际的口语场景存在较大差异，
        /// 可以通过 LLM 对文字进行优化，使其更加适合口语化表达。
        /// </param>
        /// <param name="customProcess">
        /// 自定义文字转语音方法。
        /// 如果系统提供的大模型不能满足使用需求，可以通过此参数自定义文字转语音处理方法。
        /// 要求返回本地目录中的语音文件路径。
        /// </param>
        /// <returns>
        /// 无返回值。
        /// </returns>

        public async Task SendVoiceMessageWithTTS(string who, string apiKey, string message, VoiceOptions options, bool optimizeWithLlm = false, Func<string, string> customProcess = null) => await this.ChatContent.SendVoiceMessageWithTTS(who, apiKey, message, options, optimizeWithLlm, customProcess);
        /// <summary>
        /// 根据日期获取当前聊天窗口的聊天历史
        /// </summary>
        /// <param name="date">查询日期,如果为空，则为当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(DateTime date = default) => await ChatContent.GetChatHistory(date);

        /// <summary>
        /// 根据日期获取聊天历史
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称,可以为空，如果为空，则获取当前聊天窗口的历史记录</param>
        /// <param name="date">查询日期,如果不传，则是当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime date = default) => await ChatContent.GetChatHistory(who, date);

        /// <summary>
        /// 获取一段时间的(开始时间与结束时间)聊天历史记录
        /// 适用于历史消息比较多，分段获取的场景
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称</param>
        /// <param name="startDate">开始日期,支持时、分、秒</param>
        /// <param name="endDate">结束日期，支持时、分、秒</param>
        /// <returns></returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime startDate, DateTime endDate) => await ChatContent.GetChatHistory(who, startDate, endDate);

        /// <summary>
        /// 拍一拍
        /// 注意：只能拍一拍当前聊天窗口的好友,一般结合消息监听或者<seealso cref="SearchFriend"/>使用.
        /// 只有两个地方可以拍一拍：一个是群聊中，一个是好友聊天窗口（非企业微信,企业微信聊天不能拍一拍).
        /// </summary>
        /// <param name="who">要拍一拍的好友昵称</param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        /// <returns>是否成功拍一拍</returns>
        public async Task<bool> TapWho(string who, int prevScrollNumber = 30) => await this.ChatContent.MessageBubbleList.TapWho(who, prevScrollNumber);

        /// <summary>
        /// 引用消息
        /// 注意：引用消息只能是当前窗口的好友消息，一般结合消息监听或者<seealso cref="SearchFriend"/>使用.
        /// </summary>
        /// <param name="chatSimpleMessage">要引用的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        public async Task<bool> ReferencedMessage(ChatSimpleMessage chatSimpleMessage, int prevScrollNumber = 30) => await this.ChatContent.MessageBubbleList.ReferencedMessage(chatSimpleMessage, prevScrollNumber);

        /// <summary>
        /// 引用消息
        /// 注意：引用消息只能是当前窗口的好友消息，一般结合消息监听或者<seealso cref="SearchFriend"/>使用.        
        /// </summary>
        /// <param name="who">要引用的好友昵称</param>
        /// <param name="message">要引用的消息内容</param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        public async Task<bool> ReferencedMessage(string who, string message, int prevScrollNumber = 30) => await this.ChatContent.MessageBubbleList.ReferencedMessage(who, message, prevScrollNumber);

        /// <summary>
        /// 引用最后一条消息
        /// 注意：引用消息只能是当前窗口的好友消息，一般结合消息监听或者<seealso cref="SearchFriend"/>使用.    
        /// 注意，只能引用有的消息，不会翻页，如果消息不在当前页，则不会引用
        /// </summary>
        public async Task<bool> ReferencedLastMessage() => await this.ChatContent.MessageBubbleList.ReferencedLastMessage();


        #endregion

        #region 好友/群聊管理
        /// <summary>
        /// 打开新增朋友窗口
        /// </summary>
        /// <returns></returns>
        public async Task<Window> OpenAddFriensWin() => await this.Search.OpenAddFriensWin();
        /// <summary>
        /// 关闭新增朋友窗口
        /// </summary>
        /// <returns></returns>
        public async Task CloseAddFriendWin() => await this.Search.CloseAddFriendWin();
        /// <summary>
        /// 通过手机号码、微信号查找并添加好友
        /// </summary>
        /// <param name="friends">手机号码或者微信号列表</param>
        /// <param name="options">增加朋友选项，具体请参考<see cref="AddFriendsOptions"/></param>
        /// <param name="token">取消令版</param>
        /// <returns>添加好友结果列表，详情请参见<see cref="FriendAddResult"/></returns>
        public async Task<IDictionary<string, FriendAddResult>> AddFriends(OneOf<string, string[]> friends, AddFriendsOptions options = null, CancellationToken token = default)
            => await this.Search.AddFriends(friends, options, token);

        #region 缓存中好友信息管理
        /// <summary>
        /// 显示缓存中存储的好友信息.
        /// </summary>
        /// <returns>好友信息列表，请参考<see cref="FriendInfo"/></returns>
        public List<FriendInfo> GetFriendListFromCache() => CacheManager.GetFriendListFromCache();

        /// <summary>
        /// 显示缓存中存储的好友信息,异步方法
        /// </summary>
        /// <returns>好友信息列表，请参考<see cref="FriendInfo"/></returns>
        public async Task<List<FriendInfo>> GetFriendListFromCacheAsync() => await CacheManager.GetFriendListFromCacheAsync();

        /// <summary>
        /// 从缓存中得到一个好友的信息
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public FriendInfo GetFriendFromCache(string who) => CacheManager.GetFriendFromCache(who);

        /// <summary>
        /// 从缓存中得到一个好友信息,适用于昵称重复的场合
        /// 建议：建议使昵称唯一
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <returns></returns>
        public List<FriendInfo> GetFriendsFromCache(string who) => CacheManager.GetFriendsFromCache(who);

        /// <summary>
        /// 从缓存中得到一个好友的信息,因为名字可能重复，而wxid永远不重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public FriendInfo GetFriendWithWxIDFromCache(string wxid) => CacheManager.GetFriendWithWxIDFromCache(wxid);

        /// <summary>
        /// 从缓存中得到一个好友的信息,异步方法
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public async Task<FriendInfo> GetFriendFromCacheAsync(string who) => await CacheManager.GetFriendFromCacheAsync(who);

        /// <summary>
        /// 从缓存中得到一个好友的信息,通过wxid来获取，因为名字可能重复,而微信id号永不重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        /// <returns>好友对象，请参考:<see cref="FriendInfo"/></returns>
        public async Task<FriendInfo> GetFriendWithWxIDFromCacheAsync(string wxid) => await CacheManager.GetFriendWithWxIDFromCacheAsync(wxid);

        /// <summary>
        /// 从缓存中移除一个好友
        /// </summary>
        /// <param name="who"></param>
        public void RemoveFriendFromCache(string who) => CacheManager.RemoveFriendFromCache(who);
        /// <summary>
        /// 从缓存中移除一个好友,异步方法
        /// </summary>
        /// <param name="who"></param>
        public async Task RemoveFriendFromCacheAsync(string who) => await CacheManager.RemoveFriendFromCacheAsync(who);

        /// <summary>
        /// 从缓存中移除一个好友,通过微信id，因为通过微信名可能会重复
        /// </summary>
        /// <param name="wxid">微信号</param>
        public void RemoveFriendWithWxIDFromCache(string wxid) => CacheManager.RemoveFriendWithWxIDFromCache(wxid);

        /// <summary>
        /// 从缓存中移除一个好友,通过wxid,异步方法
        /// </summary>
        /// <param name="wxid">微信号</param>
        public async Task RemoveFriendWithWxIDFromCacheAsync(string wxid) => await CacheManager.RemoveFriendWithWxIDFromCacheAsync(wxid);

        /// <summary>
        /// 手动增加或者修改一个好友对象
        /// </summary>
        /// <param name="friend">好友对象，请参考<see cref="FriendInfo"/></param>
        public void AddOrUpdateFriendFromCache(FriendInfo friend) => CacheManager.AddOrUpdateFriendFromCache(friend);

        /// <summary>
        /// 手动增加或者修改一个好友对象，使用异步方法
        /// </summary>
        /// <param name="friend">好友对象，请参考<see cref="FriendInfo"/></param>
        public async Task AddOrUpdateFriendFromCacheAsync(FriendInfo friend) => await CacheManager.AddOrUpdateFriendFromCacheAsync(friend);

        #endregion
        /// <summary>
        /// 是否是自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>是否是自有群</returns>
        public async Task<bool> IsOwnerChatGroup(string groupName) => await OwnerGroup.IsOwnerChatGroup(groupName);

        /// <summary>
        /// 获取群主
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>群主昵称</returns>
        public async Task<string> GetGroupOwner(string groupName) => await OwnerGroup.GetGroupOwner(groupName);

        /// <summary>
        /// 添加群聊成员，适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则在焦点聊天群聊中添加群聊成员</param>
        /// <param name="memberName">成员名称</param>
        public async Task AddOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName) => await OwnerGroup.AddOwnerChatGroupMember(groupName, memberName);

        /// <summary>
        /// 创建群聊,如果存在，则打开群聊，否则创建一个新群聊
        /// </summary>
        /// <param name="groupName">群聊名称,不能与之前的群聊名称重复</param>
        /// <param name="firstWho">首个成员名称，必须是好友，不能是群聊名称，用来创建群聊定位,可以为空，如果为空，则以当前聊天的好友为基准创建群聊</param>
        /// <param name="memberName">成员名称,成员数量要大于0</param>
        /// <returns>是否创建成功,如果创建失败，则显示原因,具体请参考<see cref="Result"/></returns>
        public async Task<Result> CreateOwnerChatGroup(string groupName, string firstWho, string[] memberName) => await this.OwnerGroup.CreateOwnerChatGroup(groupName, firstWho, memberName);

        /// <summary>
        /// 修改群名，适用于自有群群名修改
        /// </summary>
        /// <param name="oldGroupName">旧群名称</param>
        /// <param name="newGroupName">新群名称</param>
        /// <returns>是否修改成功</returns>
        public async Task<Result> ChangeOwnerChatGroupName(string oldGroupName, string newGroupName) => await this.OwnerGroup.ChangeOwnerChatGroupName(oldGroupName, newGroupName);

        /// <summary>
        /// 修改自己在群中的昵称
        /// </summary>
        /// <param name="groupName">群名,可以为空，如果为空，则修改焦点群聊的自己在群中的昵称</param>
        /// <param name="nickName">昵称，如果为空，则删除自己在本群中的昵称</param>
        /// <returns>是否修改成功<see cref="Result"/></returns>
        public async Task<Result> ChangeChatGroupNickName(string groupName, string nickName) => await this.OwnerGroup.ChangeChatGroupNickName(groupName, nickName);


        /// <summary>
        /// 改变群备注,群备注仅自己可见.
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则改变焦点聊天群的备注</param>
        /// <param name="newMemo">新备注，可以为空，如果为空，则删除本群备注</param>
        /// <returns>是否修改成功<see cref="Result"/></returns>
        public async Task<Result> ChangeChatGroupMemo(string groupName, string newMemo) => await this.OwnerGroup.ChangeChatGroupMemo(groupName, newMemo);

        /// <summary>
        /// 更新群聊公告,仅适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称，可以为空字符串，如果为空，则更新焦点聊天群聊窗口的公告</param>
        /// <param name="groupNotice">群聊公告</param>
        /// <returns>是否修改成功<see cref="ChatResponse"/></returns>
        public async Task<Result> UpdateGroupNotice(string groupName, string groupNotice) => await this.OwnerGroup.UpdateGroupNotice(groupName, groupNotice);


        /// <summary>
        /// 获取群聊成员列表
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则获取的是焦点聊天群聊的成员列表</param>
        /// <returns>群聊成员列表</returns>
        public async Task<List<string>> GetChatGroupMemberList(string groupName) => await this.OwnerGroup.GetChatGroupMemberList(groupName);

        /// <summary>
        /// 移除群聊成员,适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则从焦点聊天群聊中移除好友</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>操作结果<see cref="Result"/></returns>
        public async Task<Result> RemoveOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName) => await this.OwnerGroup.RemoveOwnerChatGroupMember(groupName, memberName);

        /// <summary>
        /// 退出群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="clearHistory">是否清除历史消息</param>
        public async Task QuitChatGroup(string groupName, bool clearHistory = true) => await this.OwnerGroup.QuitChatGroup(groupName, clearHistory);

        /// <summary>
        /// 邀请群聊成员,适用于外部群
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则在本焦点群聊窗口邀请好友</param>
        /// <param name="members">被邀请的成员名称列表,要求在自己的通讯录中</param>
        /// <param name="inviteReasonIfNeed">邀请原因，只在群管理员开启了 进群需要群主或者管理员确认 功能时有效，可以为空</param>
        /// <returns>操作结果<see cref="Result"/></returns>
        public async Task<Result> InviteChatGroupMember(string groupName, List<string> members, string inviteReasonIfNeed = "") => await this.OuterGroup.InviteChatGroupMember(groupName, members, inviteReasonIfNeed);

        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于从外部群中添加好友为自己的好友
        /// 此操作为微信严风控操作，因为微信对于一天加好友应该有数量限定，建议分批次加，一次不要超过20-30个，时间延长为4小时或者一天后
        /// </summary>
        /// <param name="groupName">群聊名称,可以为空，如果为空，则在本焦点群聊窗口邀请好友</param>
        /// <param name="memberName">成员名称列表,考虑风控,建议先运行<see cref="Group.GetChatGroupMemberList(string)"/>获取群聊的成员列表，然后分批增加</param>
        /// <param name="options">好友选项，可以增加好友时设置备注后缀、打招呼内容及标签等，方便分类管理</param>
        /// <returns>返回每个好友增加情况的字典</returns>
        public async Task<IDictionary<string, FriendAddResult>> AddChatGroupMemberToFriends(string groupName, List<string> memberName, AddFriendsOptions options = null) => await this.OuterGroup.AddChatGroupMemberToFriends(groupName, memberName, options);

        #endregion

        #region 通讯录管理
        /// <summary>
        ///<para> 获取所有好友的信息列表,具体请考<see cref="FriendInfo"/>类说明.</para>
        ///<para> 注意：只会获取通讯录中的联系人、企业微信联系人和群聊的记录,公众号，服务号，我的企业等特殊账号不会获取.</para>
        ///<para> 1.如果是企业微信，会剔除@xxxx后缀，以保持一致性.</para>
        ///<para> 2.如果好友/群聊/企业微信联系人等有备注，则备注会覆盖昵称显示.</para>
        ///<para> 3.注意：如果微信联系人有重名，此方法会仅获取/保存一个联系人，所以运行此方法前:建议好友/群聊/企业微信联系人有重名时，通过手工的方式添加备注，以保持区分.</para>
        ///<para> 4.普通联系人可以获取wxid,其他的如：群聊/企业微信联系人无法获取wxid.</para>
        ///<para> 5. 此方法运行结果会保存在cache中,默认为true,从cache中获取数据，如果设置为false,则重新刷新一遍通讯录,cache也会同步更新，建议实际开发过程中运行一遍从通讯录获取好友信息的操作,并且做好添加好友时的同步工作（在一些监听的场景，如果读取到此好友没有wxid,也会自动获取,并同步更新cache）</para>
        /// <para>6. 不必太过于担心cache过期的问题，因为实际需要识别wxid的业务场景中，如果碰到新好友在cache中没有数据，会自动获取好友的信息并更新cahce，所以也不必太担心cache的过时问题</para>
        /// </summary>
        /// <param name="fromCache">是否从cahce中获取数据</param>
        /// <returns>好友列表</returns>
        public async Task<List<FriendInfo>> GetAllFriends(bool fromCache = true) => await AddressBookList.GetAllFriends(fromCache);
        /// <summary>
        /// 获取所有好友名称列表.（通过通讯录）
        /// 如果好友有昵称与备注，优先选择备注名
        /// 注意：如果是企业微信，会剔除@xxxx后缀，以保持一致性.
        /// </summary>
        /// <returns>好友名称列表</returns>
        public async Task<List<string>> GetAllFriendNames() => await AddressBookList.GetAllFriendNames();

        /// <summary>
        /// 通过加好友添加申请
        /// </summary>
        /// <param name="options">配置对象，具体参见<see cref="FriendRequestAutoAcceptOptions"/></param>
        /// <param name="token">取消今牌</param>
        /// <returns>返回加成功的好友昵称列表</returns>
        public async Task<List<NewFriendBackItem>> PassedAllNewFriend(FriendRequestAutoAcceptOptions options, CancellationToken token = default)
          => await this.AddressBookList.PassedAllNewFriend(options, token);
        /// <summary>
        /// 移除好友
        /// 注意： 如果删除好友，从通讯录删除好友后，同步的，如果此好友处在监听中，应该将监听中的好友删除
        /// </summary>
        /// <param name="nickName">好友昵称,可以为空，如果为空，则将焦点窗口的好友删除</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RemoveFriend(string nickName) => await this.AddressBookList.RemoveFriend(nickName);

        #endregion


        #region 释放资源
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~WeChatClient()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;
            if (disposing)
            {
                SystemMonitorChannel.Writer.Complete();
                _ActionTokenSource?.Cancel();
                if (_SystemMonitorTask != null && !_SystemMonitorTask.IsCompleted)
                {
                    try
                    {
                        _SystemMonitorTask.Wait(TimeSpan.FromSeconds(3));
                    }
                    catch (AggregateException) { }
                    catch (Exception) { }
                }

                MessageMonitor?.Dispose();
                NewFriendMonitor?.Dispose();
            }
        }
        #endregion
    }
}
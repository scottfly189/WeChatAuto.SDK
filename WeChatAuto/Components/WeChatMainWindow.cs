using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WeAutoCommon.Utils;
using WeAutoCommon.Enums;
using WeAutoCommon.Interface;
using FlaUI.Core.Input;
using System.Threading.Tasks;
using OneOf;
using WeAutoCommon.Models;
using System;
using WeChatAuto.Utils;
using System.Threading;
using WeAutoCommon.Configs;
using FlaUI.Core.Tools;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeChatAuto.Services;
using WeChatAuto.Extentions;
using WeChatAuto.Models;
using OneOf.Types;



namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端窗口,封装的微信窗口，包含工具栏、导航栏、搜索、会话列表、通讯录、聊天窗口等
    /// </summary>
    public class WeChatMainWindow : IWeChatWindow, IDisposable
    {
        private readonly AutoLogger<WeChatMainWindow> _logger;
        private WeChatClientFactory _WeChatClientFactory;
        private Window _MainWindow;
        private ToolBar _ToolBar;  // 工具栏
        private SubWinList _SubWinList;  // 弹出窗口列表
        private Navigation _Navigation;  // 导航栏
        private Search _Search;  // 搜索
        private ConversationList _Conversations;  // 会话列表
        private AddressBookList _AddressBook;  // 通讯录
        private ChatContent _WxMainChatContent;  // 主聊天窗口,其实每个SubWin都有一个ChatContent
        private IServiceProvider _serviceProvider;
        public ToolBar ToolBar => _ToolBar;  // 工具栏
        public Navigation Navigation => _Navigation;  // 导航栏
        public ConversationList Conversations => _Conversations;  // 会话列表
        public AddressBookList AddressBook => _AddressBook;  // 通讯录
        public Search Search => _Search;  // 搜索
        public ChatContent MainChatContent => _WxMainChatContent;  // 主聊天窗口
        public SubWinList SubWinList => _SubWinList;  // 子窗口列表
        public int ProcessId { get; private set; }
        private string _nickName;
        public string NickName => _nickName;
        public Window Window => _MainWindow;
        public WeChatClient Client { get; set; }
        private volatile bool _disposed = false;
        private Moments _moments;
        public Window SelfWindow { get => _MainWindow; set => _MainWindow = value; }  //实现IWeChatWindow接口
        public WeChatClientFactory weChatClientFactory => _WeChatClientFactory;
        public Moments Moments { get => _moments; set => _moments = value; }
        private UIThreadInvoker _uiMainThreadInvoker;   //每个微信窗口一个单独的UI线程
        public UIThreadInvoker UiMainThreadInvoker => _uiMainThreadInvoker;
        private Thread _newUserListenerThread;
        private CancellationTokenSource _newUserListenerCancellationTokenSource = new CancellationTokenSource();
        private List<(Action<List<string>> callBack, FriendListenerOptions options)> _newUserActionList = new List<(Action<List<string>> callBack, FriendListenerOptions options)>();
        private TaskCompletionSource<bool> _newUserListenerStarted = new TaskCompletionSource<bool>();

        /// <summary>
        /// 微信客户端窗口构造函数
        /// </summary>
        /// <param name="weChatClientFactory">微信客户端工厂<see cref="WeChatClientFactory"/></param>
        /// <param name="serviceProvider">服务提供者<see cref="IServiceProvider"/></param>
        /// <param name="topWindowProcessId">窗口进程ID</param>
        public WeChatMainWindow(WeChatClientFactory weChatClientFactory, IServiceProvider serviceProvider, int topWindowProcessId)
        {
            _uiMainThreadInvoker = new UIThreadInvoker($"WeChatMainWindow_processId_{topWindowProcessId}_Main_Invoker");
            _serviceProvider = serviceProvider;
            _WeChatClientFactory = weChatClientFactory;
            ProcessId = topWindowProcessId;
            _MainWindow = _GetClientWindow(topWindowProcessId);
            _logger = _serviceProvider.GetRequiredService<AutoLogger<WeChatMainWindow>>();
            _InitStaticWxWindowComponents();  //初始化微信窗口的各种组件
            _InitNewUserListener();  //初始化新用户监听
            _newUserListenerStarted.Task.GetAwaiter().GetResult();  //等待新用户监听线程启动
        }
        /// <summary>
        /// 获取微信客户端窗口
        /// </summary>
        /// <param name="topWindowProcessId">窗口进程ID</param>
        /// <returns>微信客户端窗口</returns>
        private Window _GetClientWindow(int topWindowProcessId)
        {
            return _uiMainThreadInvoker.Run(automation =>
            {
                var window = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByClassName("WeChatMainWndForPC")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(topWindowProcessId)))).AsWindow(),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
                return window.Result;
            }).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 初始化新好友监听
        /// </summary>
        private void _InitNewUserListener()
        {
            _newUserListenerThread = new Thread(async () =>
            {
                try
                {
                    _newUserListenerStarted.SetResult(true);
                    while (!_newUserListenerCancellationTokenSource.Token.IsCancellationRequested)
                    {
                        if (_disposed)
                            break;
                        if (_newUserListenerCancellationTokenSource.IsCancellationRequested)
                            break;
                        if (_newUserActionList.Count > 0)
                        {
                            //如果用户设置了新好友监听，则获取新好友申请列表
                            await _FetchNewUserNoticeAction(_newUserListenerCancellationTokenSource.Token);
                        }
                        await Task.Delay(WeAutomation.Config.NewUserListenerInterval * 1000);
                    }
                }
                catch (OperationCanceledException)
                {
                    Trace.WriteLine($"新好友监听线程[{_newUserListenerThread.Name}]已停止，正常取消,不做处理");
                    _logger.Info($"新好友监听线程[{_newUserListenerThread.Name}]已停止，正常取消,不做处理");
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"新好友监听线程[{_newUserListenerThread.Name}]异常，异常信息：" + e.Message);
                    _logger.Error($"新好友监听线程[{_newUserListenerThread.Name}]异常，异常信息：" + e.Message);
                    throw;
                }
            });
            _newUserListenerThread.Priority = ThreadPriority.Lowest;
            _newUserListenerThread.Name = "NewUserListenerThread";
            _newUserListenerThread.IsBackground = true;
            _newUserListenerThread.Start();
        }
        /// <summary>
        /// 获取新好友申请
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task _FetchNewUserNoticeAction(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var resultFlag = _uiMainThreadInvoker.Run(automation =>
            {
                try
                {
                    var xPath = "//ToolBar[@Name='导航']/Button[@Name='通讯录']";
                    if (!AutomationValid.IsValid(_MainWindow))
                    {
                        var winResult = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByName("微信").And(cf.ByClassName("WeChatMainWndForPC")).And(cf.ByProcessId(_MainWindow.Properties.ProcessId))).AsWindow(),
                            timeout: TimeSpan.FromSeconds(5),
                            interval: TimeSpan.FromMilliseconds(200));
                        if (winResult.Success)
                        {
                            _MainWindow = winResult.Result;
                        }
                    }
                    var button = _MainWindow.FindFirstByXPath(xPath)?.AsButton();
                    if (button != null)
                    {
                        var result = button.Patterns.Value.IsSupported;
                        if (result)
                        {
                            var pattern = button.Patterns.Value.Pattern;
                            if (pattern != null)
                            {
                                var value = pattern.Value;
                                if (!string.IsNullOrEmpty(value.Value) && int.Parse(value.Value) > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("新好友监听线程异常，异常信息：" + ex.Message);
                    _logger.Error("新好友监听线程异常，异常信息：" + ex.Message);
                    return false;
                }
            }).GetAwaiter().GetResult();
            if (resultFlag)
            {
                _newUserActionList.ForEach(async item =>
                {
                    ChatActionMessage msg = new ChatActionMessage()
                    {
                        Type = ActionType.添加好友,
                        ToUser = "",
                        Message = "",
                        Payload = item.options,
                        IsOpenSubWin = false,
                    };
                    var result = await this.AddFriendDispatch(msg);  //得到这次所有新增好友的昵称列表，实际已经通过
                    if (result != null && result is List<string> list)
                    {
                        item.callBack(list);
                    }
                });
            }
            await Task.CompletedTask;
        }
        /// <summary>
        /// 清除所有事件及其他
        /// </summary>
        public void ClearAllEvent()
        {
            if (_disposed)
            {
                return;
            }
            _uiMainThreadInvoker.Run(automation => automation.UnregisterAllEvents()).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 初始化微信窗口的各种组件
        /// </summary>
        private void _InitStaticWxWindowComponents()
        {
            _nickName = _uiMainThreadInvoker.Run(automation => _MainWindow.FindFirstByXPath($"/Pane/Pane/ToolBar[@Name='{WeChatConstant.WECHAT_NAVIGATION_NAVIGATION}'][@IsEnabled='true']").FindFirstChild().Name).GetAwaiter().GetResult();
            _ToolBar = new ToolBar(_MainWindow, _uiMainThreadInvoker, _serviceProvider);  // 工具栏
            _Navigation = new Navigation(_MainWindow, this, _uiMainThreadInvoker, _serviceProvider);  // 导航栏
            _Search = new Search(this, _uiMainThreadInvoker, _MainWindow, _serviceProvider);  // 搜索
            _Conversations = new ConversationList(_MainWindow, this, _uiMainThreadInvoker, _serviceProvider);  // 会话列表
            _AddressBook = new AddressBookList(_MainWindow, this, _uiMainThreadInvoker, _serviceProvider);  // 通讯录
            _moments = new Moments(_MainWindow, this, _uiMainThreadInvoker, _serviceProvider);
            _SubWinList = new SubWinList(_MainWindow, this, _uiMainThreadInvoker, _serviceProvider);
            //这里是主聊天窗口的ChatContent,子窗口也有ChatContent,但是是不同的对象，要注意传入的参数！
            _WxMainChatContent = new ChatContent(_MainWindow, ChatContentType.Inline, "/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane", this, _uiMainThreadInvoker, this, _serviceProvider);
        }

        public async Task SendMessageDispatch(ChatActionMessage msg)
        {
            await this.SendMessageCore(msg.ToUser, msg.Message, msg.IsOpenSubWin, msg.AtUsers);
        }

        public async Task SendEmojiDispatch(ChatActionMessage msg, OneOf<string, string[]> atUser = default)
        {
            await this.SendEmojiCore(msg, atUser);
        }
        public async Task SendFileDispatch(ChatActionMessage msg)
        {
            await this.SendFileCore(msg);
        }
        public async Task<List<string>> AddFriendDispatch(ChatActionMessage msg)
        {
            return await this.AddFriendCore(msg);
        }
        public async Task<bool> OpenSubWinDispatch(ChatActionMessage msg)
        {
            return await this.OpenSubWinCore(msg);
        }

        #region 窗口操作
        /// <summary>
        /// 置顶窗口
        /// </summary>
        /// <param name="isTop"></param>
        public void WindowTop(bool isTop = true)
        {
            ToolBar.Top(isTop);
        }
        /// <summary>
        /// 最小化窗口
        /// </summary>
        public void WindowMin()
        {
            ToolBar.Min();
        }


        /// <summary>
        /// 最大化窗口
        /// </summary>
        public void WindowMax()
        {
            ToolBar.Max();
        }

        /// <summary>
        /// 窗口还原
        /// </summary>
        public void WindowRestore()
        {
            ToolBar.Restore();
        }
        #endregion
        #region 导航栏操作
        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void NavigationSwitch(NavigationType navigationType)
        {
            Navigation.SwitchNavigation(navigationType);
        }
        #endregion
        #region 发送消息操作
        /// <summary>
        /// 单个发送消息，发送消息给单个好友
        /// 注意：此方法不会打开子窗口
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        public async Task SendWho(string who, string message, OneOf<string, string[]> atUser = default)
        {
            var atUserList = new List<string>();
            if (atUser.Value != default)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        if (!string.IsNullOrWhiteSpace(user))
                        {
                            atUserList.Add(user);
                        }
                    },
                    (string[] atUsers) =>
                    {
                        atUserList.AddRange(atUsers);
                    }
                );
            }
            await this.SendMessageDispatch(new ChatActionMessage()
            {
                Type = ActionType.发送消息,
                ToUser = who,
                Message = message,
                IsOpenSubWin = false,
                AtUsers = atUserList,
            });
        }
        /// <summary>
        /// 聚焦到指定好友的聊天窗口
        /// </summary>
        /// <param name="who"></param>
        /// <returns></returns>
        public void FocusWho(string who)
        {
            try
            {
                if (_SubWindowIsOpen(who, "", subWin => subWin.SelfWindow.Focus()))
                {
                    return;
                }

                if (_IsInConversationFocusCore(who))
                {
                    return;
                }

                if (_IsSearchFocusCore(who))
                {
                    return;
                }

                throw new Exception($"错误：好友[{who}]不存在,请检查您的输入是否正确");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("聚焦到指定好友的聊天窗口失败：" + ex.Message);
                _logger.Error("聚焦到指定好友的聊天窗口失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 在指定好友的聊天窗口中粘贴图片，即执行Ctrl+V操作
        /// </summary>
        /// <param name="who"></param>
        /// <returns></returns>
        public async Task PasteContentToWho(string who)
        {
            try
            {
                if (_SubWindowIsOpen(who, "", subWin => subWin.ChatContent.ChatBody.Sender.PasteImageFiles()))
                {
                    return;
                }

                if (await _IsInConversationPasteImageFilesCore(who, () => this.PasteContentToWho(who)))
                {
                    return;
                }

                if (await _IsSearchPasteImageFilesCore(who, () => this.PasteContentToWho(who)))
                {
                    return;
                }

                throw new Exception($"错误：好友[{who}]不存在,请检查您的输入是否正确");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("粘贴图片等失败：" + ex.Message);
                _logger.Error("粘贴图片等操作失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 批量发送消息
        /// 注意：此方法不会打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友昵称列表</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        public async Task SendWhos(string[] whos, string message, OneOf<string, string[]> atUser = default)
        {
            foreach (var who in whos)
            {
                await SendWho(who, message, atUser);
                RandomWait.Wait(300, 1000);
            }
        }
        /// <summary>
        /// 单个发送消息，发送消息给单个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        public async Task SendWhoAndOpenChat(string who, string message, OneOf<string, string[]> atUser = default)
        {
            var atUserList = new List<string>();
            if (atUser.Value != default)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        if (!string.IsNullOrWhiteSpace(user))
                        {
                            atUserList.Add(user);
                        }
                    },
                    (string[] atUsers) =>
                    {
                        atUserList.AddRange(atUsers);
                    }
                );
            }
            await this.SendMessageDispatch(new ChatActionMessage()
            {
                Type = ActionType.发送消息,
                ToUser = who,
                Message = message,
                IsOpenSubWin = true,
                AtUsers = atUserList,
            });
        }

        /// <summary>
        /// 批量发送消息，发送消息给多个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友昵称列表</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        public async Task SendWhosAndOpenChat(string[] whos, string message, OneOf<string, string[]> atUser = default)
        {
            foreach (var who in whos)
            {
                await SendWhoAndOpenChat(who, message, atUser);
                RandomWait.Wait(300, 1000);
            }
        }
        /// <summary>
        /// 发起语音聊天,适用于单个好友
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
        public void SendVoiceChat(string who, bool isOpenChat = false)
        {
            //发送给指定好友
            //步骤：
            //1.首先查询此用户是否在弹出窗口列表中
            //2.如果存在，则用弹出窗口发出消息
            if (_SubWindowIsOpen(who, "", subWin => subWin.ChatContent.ChatBody.Sender.SendVoiceChat()))
            {
                return;
            }
            if (_IsInConversationAndActionCoreExt(who, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVoiceChat(), SendVoiceChat))
            {
                return;
            }
            if (_IsSearchAndActionExt(who, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVoiceChat(), SendVoiceChat))
            {
                return;
            }
            _logger.Error($"无法找到{who}的聊天窗口，无法发起语音聊天");
        }

        /// <summary>
        /// 发起语音聊天,适用于群聊中发起语音聊天
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="whos">好友昵称列表</param>
        /// <param name="isOpenChat">是否打开子聊天窗口,默认是True:打开,False:不打开</param>
        public void SendVoiceChats(string groupName, string[] whos, bool isOpenChat = true)
        {
            if (_SubWindowIsOpen(groupName, "", subWin => subWin.ChatContent.ChatBody.Sender.SendVoiceChats(whos)))
            {
                return;
            }
            if (_IsInConversationAndActionCore(groupName, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVoiceChats(whos)))
            {
                return;
            }
            if (_IsSearchAndAction(groupName, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVoiceChats(whos)))
            {
                return;
            }
            _logger.Error($"无法找到{groupName}的聊天窗口，无法发起语音聊天");
        }

        /// <summary>
        /// 发起视频聊天,适用于单个好友,群聊没有视频聊天功能
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
        public void SendVideoChat(string who, bool isOpenChat = false)
        {
            if (_SubWindowIsOpen(who, "", subWin => subWin.ChatContent.ChatBody.Sender.SendVideoChat()))
            {
                return;
            }
            if (_IsInConversationAndActionCoreExt(who, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVideoChat(), SendVideoChat))
            {
                return;
            }
            if (_IsSearchAndActionExt(who, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendVideoChat(), SendVideoChat))
            {
                return;
            }
            _logger.Error($"无法找到{who}的聊天窗口，无法发起视频聊天");
        }
        /// <summary>
        /// 发起直播,适用于群聊中发起直播，单个好友没有直播功能
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
        public void SendLiveStreaming(string groupName, bool isOpenChat = false)
        {
            if (_SubWindowIsOpen(groupName, "", subWin => subWin.ChatContent.ChatBody.Sender.SendLiveStreaming()))
            {
                return;
            }
            if (_IsInConversationAndActionCoreExt(groupName, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendLiveStreaming(), SendLiveStreaming))
            {
                return;
            }
            if (_IsSearchAndActionExt(groupName, isOpenChat, () => this.MainChatContent.ChatBody.Sender.SendLiveStreaming(), SendLiveStreaming))
            {
                return;
            }
            _logger.Error("无法找到当前聊天窗口，无法发起直播,请检查是否在聊天窗口中");
        }

        /// <summary>
        /// 获取所有气泡标题列表
        /// <see cref="ChatSimpleMessage"/>
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="pageCount">获取的气泡数量，默认是10页,可以指定获取的页数，如果指定为-1，则获取所有气泡</param>
        /// <returns>所有气泡标题列表</returns>
        public List<ChatSimpleMessage> GetAllChatHistory(string who, int pageCount = 10)
        {
            var (success, subWin) = _SubWindowIsOpenExt(who);
            if (success)
            {
                return subWin.ChatContent.ChatBody.GetAllChatHistory(pageCount);
            }
            if (_IsInConversationNotOpenAndActionCoreExt(who))
            {
                return this.MainChatContent.ChatBody.GetAllChatHistory(pageCount);
            }
            if (_IsSearchAndNotOpenAndActionExt(who))
            {
                return this.MainChatContent.ChatBody.GetAllChatHistory(pageCount);
            }
            _logger.Error($"无法找到{who}的聊天窗口，无法获取所有气泡标题列表");
            return null;
        }
        /// <summary>
        /// 给当前聊天窗口发送消息的核心方法
        /// 可能存在不能发送消息的窗口情况，因为当前可能是非聊天窗口
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUserList">被@的好友列表</param>
        private void __SendCurrentMessageCore(string message, List<string> atUserList = null)
        {
            this.MainChatContent.ChatBody.Sender.SendMessage(message, atUserList);
        }
        /// <summary>
        /// 获取当前聊天窗口的标题
        /// </summary>
        /// <returns>当前聊天窗口的标题</returns>
        public string GetCurrentChatTitle()
        {
            return this.MainChatContent.ChatHeader.Title;
        }
        private bool _SubWindowIsOpen(string who, string message, Action<SubWin> action)
        {
            var subWin = this.SubWinList.GetSubWin(who);
            if (subWin != null)
            {
                action(subWin);
                return true;
            }
            return false;
        }
        private (bool success, SubWin subWin) _SubWindowIsOpenExt(string who)
        {
            var subWin = this.SubWinList.GetSubWin(who);
            if (subWin != null)
            {
                return (true, subWin);
            }
            return (false, subWin);
        }
        //此用户是否在会话列表中，如果存在，则打开或者点击此会话，并且发送消息
        private async Task<bool> _IsInConversation(string who, string message, bool isOpenChat, List<string> atUserList = null)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    await SendMessageCore(who, message, isOpenChat, atUserList); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.__SendCurrentMessageCore(message, atUserList);
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> _IsInConversationPasteImageFilesCore(string who, Func<Task> action)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.DoubleClickConversation(who);
                Wait.UntilInputIsProcessed();
                await action();
                return true;
            }
            return false;
        }
        private bool _IsInConversationFocusCore(string who)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.ClickConversation(who);
                Wait.UntilInputIsProcessed();
                return true;
            }
            return false;
        }
        private bool _IsInConversationAndActionCore(string who, bool isOpenChat, Action action)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    SendVoiceChat(who, isOpenChat);
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    action();
                    return true;
                }
            }
            return false;
        }

        private bool _IsInConversationAndActionCoreExt(string who, bool isOpenChat, Action action, Action<string, bool> subWinAction)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    subWinAction(who, isOpenChat);
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    action();
                    return true;
                }
            }
            return false;
        }
        private bool _IsInConversationNotOpenAndActionCoreExt(string who)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.ClickConversation(who);
                Wait.UntilInputIsProcessed();
                return true;
            }
            return false;
        }
        //此用户是否在会话列表中，如果存在，则打开或者点击此会话，并且发送消息
        private async Task<bool> _IsInConversation(string who)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.DoubleClickConversation(who);
                Wait.UntilInputIsProcessed();
                return true;

            }
            await Task.CompletedTask;
            return false;
        }
        //此用户是否在会话列表中，如果存在，则打开或者点击此会话，并且发送文件
        private async Task<bool> _IsInConversationFile(string who, string[] files, bool isOpenChat)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    await _SendFileCore(files, who, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.MainChatContent.ChatBody.Sender.SendFile(files);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 转发消息
        /// </summary>
        /// <param name="fromWho">转发消息的来源,可以是好友昵称，也可以是群聊名称</param>
        /// <param name="toWho">转发消息的接收者,可以是好友昵称，也可以是群聊名称</param>
        /// <param name="rowCount">转发消息的行数</param>
        /// <returns>是否转发成功</returns>
        public async Task<bool> ForwardMessage(string fromWho, string toWho, int rowCount = 5)
        {
            if (await this.SubWinList.CheckSubWinExistAndOpen(fromWho))
            {
                var subWin = this.SubWinList.GetSubWin(fromWho);
                if (subWin != null)
                {
                    subWin.ChatContent.ChatBody.MessageBubbleList.ForwardMultipleMessage(toWho, true, rowCount);
                    return await Task.FromResult(true);
                }
            }

            return await Task.FromResult(false);
        }
        /// <summary>
        /// 查找并打开好友或者群聊昵称,如果找到，则打开好友或者群聊窗口
        /// </summary>
        /// <param name="who">好友或者群聊昵称</param>
        /// <returns>是否找到并打开会话</returns>
        public WeAutoCommon.Models.Result FindAndOpenFriendOrGroup(string who)
        {
            var result = this.SubWinList.CheckSubWinExistAndOpen(who).GetAwaiter().GetResult();
            return result ? Result.Ok() : Result.Fail("无法找到好友或者群聊昵称");
        }

        //此用户是否在搜索结果中
        private async Task<bool> _IsSearch(string who, string message, bool isOpenChat, List<string> atUserList = null)
        {
            this.Search.SearchChat(who);
            await Task.Delay(1000);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    await SendMessageCore(who, message, isOpenChat, atUserList); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.__SendCurrentMessageCore(message, atUserList);
                    return true;
                }
            }
            this.Search.ClearText();
            return false;
        }
        private async Task<bool> _IsSearchPasteImageFilesCore(string who, Func<Task> action)
        {
            this.Search.SearchChat(who);
            await Task.Delay(1000);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {

                this.Conversations.DoubleClickConversation(who);
                Wait.UntilInputIsProcessed();
                await action();
                return true;

            }
            this.Search.ClearText();
            return false;
        }
        private bool _IsSearchFocusCore(string who)
        {
            this.Search.SearchChat(who);
            RandomWait.Wait(300, 1500);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.ClickConversation(who);
                Wait.UntilInputIsProcessed();
                return true;

            }
            this.Search.ClearText();
            return false;
        }
        //此用户是否在搜索结果中
        private bool _IsSearchAndAction(string who, bool isOpenChat, Action action)
        {
            this.Search.SearchChat(who);
            RandomWait.Wait(300, 1500);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    SendVoiceChat(who, isOpenChat);
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    action();
                    return true;
                }
            }
            this.Search.ClearText();
            return false;
        }
        private bool _IsSearchAndActionExt(string who, bool isOpenChat, Action action, Action<string, bool> subWinAction)
        {
            this.Search.SearchChat(who);
            RandomWait.Wait(300, 1500);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    subWinAction(who, isOpenChat);
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    action();
                    return true;
                }
            }
            this.Search.ClearText();
            return false;
        }

        private bool _IsSearchAndNotOpenAndActionExt(string who)
        {
            this.Search.SearchChat(who);
            RandomWait.Wait(300, 1500);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.ClickConversation(who);
                Wait.UntilInputIsProcessed();
                return true;
            }
            this.Search.ClearText();
            return false;
        }

        //此用户是否在搜索结果中
        private async Task<bool> _IsSearch(string who)
        {
            this.Search.SearchChat(who);
            await Task.Delay(1000);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                this.Conversations.DoubleClickConversation(who);
                Wait.UntilInputIsProcessed();
                this.Search.ClearText();
                return true;

            }
            return false;
        }
        //此用户是否在搜索结果中，如果存在，则打开或者点击此会话，并且发送文件
        private async Task<bool> _IsSearchFile(string who, string[] files, bool isOpenChat)
        {
            this.Search.SearchChat(who);
            await Task.Delay(1000);
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    await _SendFileCore(files, who, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.MainChatContent.ChatBody.Sender.SendFile(files);
                    return true;
                }
            }
            this.Search.ClearText();
            return false;
        }
        #endregion
        #region 发送文件操作
        /// <summary>
        /// 给指定好友发送文件
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="files">文件路径,可以是单个文件路径，也可以是多个文件路径</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public async Task SendFile(string who, OneOf<string, string[]> files, bool isOpenChat = false)
        {
            ChatActionMessage msg = new ChatActionMessage();
            msg.Type = ActionType.发送文件;
            msg.ToUser = who;
            msg.Payload = files;
            msg.IsOpenSubWin = isOpenChat;
            await this.SendFileDispatch(msg);
        }
        /// <summary>
        /// 给多个好友发送文件
        /// </summary>
        /// <param name="whos">好友昵称列表</param>
        /// <param name="files">文件路径,可以是单个文件路径，也可以是多个文件路径</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public async Task SendFiles(string[] whos, OneOf<string, string[]> files, bool isOpenChat = false)
        {
            foreach (var who in whos)
            {
                await SendFile(who, files, isOpenChat);
                RandomWait.Wait(300, 1000);
            }
        }


        #endregion
        #region 发送表情操作
        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="emoji">表情名称</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public async Task SendEmoji(string who, OneOf<int, string> emoji, OneOf<string, string[]> atUser = default, bool isOpenChat = false)
        {
            ChatActionMessage msg = new ChatActionMessage();
            msg.Type = ActionType.自定义表情;
            msg.ToUser = who;
            msg.Payload = emoji;
            msg.IsOpenSubWin = isOpenChat;
            await this.SendEmojiDispatch(msg, atUser);
        }

        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="whos">好友昵称列表</param>
        /// <param name="emoji">表情名称</param>
        /// <param name="atUser">被@的好友,最主要用于群聊中@人,可以是一个好友，也可以是多个好友，如果是自有群，可以@所有人，也可以@单个好友，外部群不能@所有人</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public async Task SendEmojis(string[] whos, OneOf<int, string> emoji, OneOf<string, string[]> atUser = default, bool isOpenChat = false)
        {
            foreach (var who in whos)
            {
                await SendEmoji(who, emoji, atUser, isOpenChat);
                RandomWait.Wait(300, 1000);
            }
        }
        #endregion
        #region 实际发送消息、文件、表情操作
        /// <summary>
        /// 发送消息核心方法
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="message">消息内容</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        /// <param name="atUserList">被@的好友列表</param>
        private async Task SendMessageCore(string who, string message, bool isOpenChat = false, List<string> atUserList = null)
        {
            try
            {
                if (string.IsNullOrEmpty(who))
                {
                    //发送给当前聊天窗口
                    this.__SendCurrentMessageCore(message, atUserList);
                    return;
                }
                else
                {
                    //发送给指定好友
                    //步骤：
                    //1.首先查询此用户是否在弹出窗口列表中
                    //2.如果存在，则用弹出窗口发出消息
                    if (_SubWindowIsOpen(who, message, subWin => subWin.ChatContent.ChatBody.Sender.SendMessage(message, atUserList)))
                    {
                        return;
                    }
                    //3.如果不存在，则查询当前聊天窗口是否是此用户(即who)
                    //4.如果是，则发送消息
                    // if (_IsCurrentChat(who, message, isOpenChat))
                    // {
                    //     return;
                    // }
                    //5.如果不是，则查询此用户是否在会话列表中
                    //6.如果存在，则打开或者点击此会话，并且发送消息
                    if (await _IsInConversation(who, message, isOpenChat, atUserList))
                    {
                        return;
                    }
                    //7.如果不存在，则进行查询,如果查询到有此用户，则打开或者点击此会话，并且发送消息
                    //8.如果查询不到，则提示用户不存在.
                    if (await _IsSearch(who, message, isOpenChat, atUserList))
                    {
                        return;
                    }

                    throw new Exception($"错误：好友[{who}]不存在,请检查您的输入是否正确");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("发送消息失败：" + ex.Message);
                _logger.Error("发送消息失败：" + ex.Message);
            }
        }
        //发送文件核心方法
        private async Task SendFileCore(ChatActionMessage msg)
        {
            try
            {
                OneOf<string, string[]> file = (OneOf<string, string[]>)msg.Payload;
                string[] files = null;
                if (file.IsT0)
                {
                    files = new string[] { file.AsT0 };
                }
                else
                {
                    files = file.AsT1;
                }
                await this._SendFileCore(files, msg.ToUser, msg.IsOpenSubWin);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("发送文件失败：" + ex.Message);
                _logger.Error("发送文件失败：" + ex.Message);
            }
        }
        //发送文件核心方法
        private async Task _SendFileCore(string[] files, string who, bool isOpenChat)
        {
            if (_SubWindowIsOpen(who, "", subWin => subWin.ChatContent.ChatBody.Sender.SendFile(files)))
            {
                return;
            }
            if (await _IsInConversationFile(who, files, isOpenChat))
            {
                return;
            }
            if (await _IsSearchFile(who, files, isOpenChat))
            {
                return;
            }
        }
        // 发送表情核心方法
        private async Task SendEmojiCore(ChatActionMessage msg, OneOf<string, string[]> atUser = default)
        {
            try
            {
                OneOf<int, string> emoji = (OneOf<int, string>)msg.Payload;
                msg.Type = ActionType.发送消息;
                var message = "";
                emoji.Switch(
                    (int emojiId) =>
                    {
                        message = EmojiListHelper.Items.FirstOrDefault(item => item.Index == emojiId)?.Value ?? EmojiListHelper.Items[0].Value;
                    },
                    (string emojiName) =>
                    {
                        message = emojiName;
                        if (!(message.StartsWith("[") && message.EndsWith("]")))
                        {
                            message = EmojiListHelper.Items.FirstOrDefault(item => item.Description == emojiName)?.Value ?? message;
                        }
                    }
                );
                var atUserList = atUser.Value == default
                    ? new List<string>()
                    : atUser.Value is string s ? new List<string> { s } : ((string[])atUser.Value).ToList();
                msg.Message = message;
                await SendMessageCore(msg.ToUser, message, msg.IsOpenSubWin, atUserList: atUserList);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("发送表情失败：" + ex.Message);
                _logger.Error("发送表情失败：" + ex.Message);
            }
        }
        private async Task<bool> OpenSubWinCore(ChatActionMessage msg)
        {
            bool result = false;
            try
            {
                _Navigation.SwitchNavigation(NavigationType.聊天);
                string who = msg.ToUser;
                if (await _IsInConversation(who))
                {
                    result = true;
                    return result;
                }
                if (await _IsSearch(who))
                {
                    result = true;
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("添加好友失败：" + ex.Message);
                throw;
            }
        }
        private async Task<List<string>> AddFriendCore(ChatActionMessage msg)
        {
            List<string> list = null;
            FriendListenerOptions options = (FriendListenerOptions)msg.Payload;
            try
            {
                if (options != null)
                {
                    //自动通过新好友后返回
                    list = _AddressBook.PassedAllNewFriend(options.KeyWord, options.Suffix, options.Label);
                }
                else
                {
                    //仅获取所有新加好友列表，不自动通过，需要用户手动通过
                    list = _AddressBook.GetAllFriends();
                }
                await Task.CompletedTask;
                return list;
            }
            catch (Exception ex)
            {
                _logger.Error("添加好友失败：" + ex.Message);
                Trace.WriteLine("添加好友失败：" + ex.Message);
                throw;
            }
        }
        #endregion

        #region 监听消息
        /// <summary>
        /// 添加消息监听，用户需要提供一个回调函数，当有消息时，会调用回调函数
        /// </summary>
        /// <param name="nickName">好友昵称</param>
        /// <param name="callBack">回调函数,由使用者提供,参数：消息上下文<see cref="MessageContext"/></param>
        /// <param name="firstMessageAction">适用于当开始消息监听时,发送一些信息（如：发送文字、表情、文件等）给好友的场景,参数：发送者<see cref="Sender"/></param>
        /// <param name="isMonitorSubWin">是否监听子窗口,如果为true，则监听子窗口，如果为false，则不监听子窗口,默认监听子窗口</param>
        public async Task AddMessageListener(string nickName, Action<MessageContext> callBack,Action<Sender> firstMessageAction = null,bool isMonitorSubWin = true)
        {
            await _SubWinList.CheckSubWinExistAndOpen(nickName);
            await Task.Delay(500);
            if (isMonitorSubWin)
            {
                _SubWinList.RegisterMonitorSubWin(nickName);
            }
            await _SubWinList.AddMessageListener(callBack, nickName, firstMessageAction);
        }
        /// <summary>
        /// 添加消息监听，用户需要提供一个回调函数，当有消息时，会调用回调函数
        /// 如果指定了回复者，可以根据设定的规则（如LLM大模型）转发消息给回复者，回复者进行回复后，转发回当前窗口(who)。
        /// </summary>
        /// <param name="nickName">好友昵称</param>
        /// <param name="replyer">回复者名称（微信昵称）</param>
        /// <param name="callBack">回调函数,由使用者提供,参数：消息上下文<see cref="MessageContext"/></param>
        public async Task AddTransferMessageListener(string nickName, Action<MessageContext> callBack, string replyer = null)
        {
            //will do
            await Task.FromResult(true);
        }
        /// <summary>
        /// 添加新好友监听，用户需要提供一个回调函数，当有新好友时，会调用回调函数
        /// </summary>
        /// <param name="callBack">回调函数</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="suffix">后缀</param>
        /// <param name="label">标签</param>
        public void AddNewFriendAutoPassedListener(Action<List<string>> callBack, string keyWord = null, string suffix = null, string label = null)
        {
            _AddNewFriendListener(callBack, new FriendListenerOptions() { KeyWord = keyWord, Suffix = suffix, Label = label });
        }

        /// <summary>
        /// 添加新好友监听，用户需要提供一个回调函数，当有新好友时，会自动通过此好友，并且将此好友打开到子窗口，当有新消息时，会调用回调函数
        /// </summary>
        /// <param name="callBack">回调函数,参数：消息上下文<see cref="MessageContext"/></param>
        /// <param name="senderAction">适用于新好友通过后,发送一些信息（如：发送文字、表情、文件等）给好友的场景,参数：发送者<see cref="Sender"/></param>
        /// <param name="keyWord">关键字</param>
        /// <param name="suffix">后缀</param>
        /// <param name="label">标签</param>
        /// <param name="isMonitorSubWin">是否监听子窗口,如果为true，则监听子窗口，如果为false，则不监听子窗口,默认监听子窗口</param>
        public void AddFriendRequestAutoAcceptAndOpenChatListener(Action<MessageContext> callBack, Action<Sender> senderAction = null, string keyWord = null, string suffix = null, string label = null,bool isMonitorSubWin = true)
        {
            _AddNewFriendListener(nickNameList =>
            {
                nickNameList.ForEach(async nickName =>
                {
                    await this.AddMessageListener(nickName, callBack, senderAction, isMonitorSubWin);
                });
            }, new FriendListenerOptions() { KeyWord = keyWord, Suffix = suffix, Label = label });
        }

        /// <summary>
        /// 添加新好友监听，用户需要提供一个回调函数，当有新好友时，会调用回调函数
        /// </summary>
        /// <param name="callBack">回调函数</param>
        /// <param name="options">监听选项</param>
        private void _AddNewFriendListener(Action<List<string>> callBack, FriendListenerOptions options)
        {
            _newUserActionList.Add((callBack, options));
        }
        /// <summary>
        /// 移除监听消息
        /// </summary>
        /// <param name="nickName">好友昵称</param>
        public void StopMessageListener(string nickName)
        {
            _SubWinList.StopMessageListener(nickName);
        }
        /// <summary>
        /// 移除新好友监听
        /// </summary>
        public void StopNewUserListener()
        {
            _newUserActionList.Clear();
        }
        #endregion

        #region 群聊操作
        #region 群基础操作，适用于自有群与外部群
        /// <summary>
        /// 更新群聊选项
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="action">更新群聊选项的Action<see cref="ChatGroupOptions"/></param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        private async Task<ChatResponse> UpdateChatGroupOptions(string groupName, Action<ChatGroupOptions> action)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.UpdateChatGroupOptions(action);
        }
        /// <summary>
        /// 获取群聊成员列表
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>群聊成员列表</returns>
        public async Task<List<string>> GetChatGroupMemberList(string groupName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.GetChatGroupMemberList();
        }
        /// <summary>
        /// 是否是群聊成员
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>是否是群聊成员</returns>
        public async Task<bool> IsChatGroupMember(string groupName, string memberName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.IsChatGroupMember(memberName);
        }
        /// <summary>
        /// 是否是自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>是否是自有群</returns>
        public async Task<bool> IsOwnerChatGroup(string groupName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.IsOwnerChatGroup();
        }
        /// <summary>
        /// 获取群主
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>群主昵称</returns>
        public async Task<string> GetGroupOwner(string groupName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.GetGroupOwner();
        }
        /// <summary>
        /// 清空群聊历史聊天记录
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        public async Task ClearChatGroupHistory(string groupName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            subWin.ClearChatGroupHistory();
        }
        /// <summary>
        /// 退出群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        public async Task QuitChatGroup(string groupName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            subWin.QuitChatGroup();
        }

        /// <summary>
        /// 设置消息免打扰
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="isMessageWithoutInterruption">是否消息免打扰,默认是True:消息免打扰,False:取消消息免打扰</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse SetMessageWithoutInterruption(string groupName, bool isMessageWithoutInterruption = true)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                //1.首先检查子窗口有没有好友，如果有，则关闭窗口
                var (success, window) = __CheckSubWinIsOpen(groupName, false);
                if (success)
                {
                    window.Close();
                }
                ListBoxItem listItem = null;
                NavigationSwitch(NavigationType.聊天);
                //2.检查会话列表有没有好友，如果有，则点击打开为当前聊天
                var (success2, item) = __CheckConversationExist(groupName, false);
                if (success2)
                {
                    listItem = item;
                }
                else
                {
                    //3.如果会话列表没有好友，则打开搜索框，输入好友昵称搜索.
                    this.__SearchChat(groupName);
                    var (success3, item3) = __CheckConversationExist(groupName, false);
                    if (success3)
                    {
                        listItem = item3;
                    }
                    else
                    {
                        throw new Exception($"{groupName} 好友不存在");
                    }
                }
                //4.执行设置消息免打扰
                this._SetMessageWithoutInterruptionCore(groupName, listItem, isMessageWithoutInterruption);
                _logger.Info($"设置{groupName}消息免打扰成功");
                result.Success = true;
                result.Message = $"设置{groupName}消息免打扰成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                result.Message = ex.Message;
                return result;
            }
        }


        /// <summary>
        /// 设置保存到通讯录
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="isSaveToAddress">是否保存到通讯录,默认是True:保存,False:取消保存</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse SetSaveToAddress(string groupName, bool isSaveToAddress = true)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                //1.首先检查子窗口有没有群聊，如果有，则关闭窗口
                var (success, window) = __CheckSubWinIsOpen(groupName, false);
                if (success)
                {
                    window.Close();
                }
                ListBoxItem listItem = null;
                NavigationSwitch(NavigationType.聊天);
                //2.检查会话列表有没有群聊，如果有，则点击打开为当前聊天
                var (success2, item) = __CheckConversationExist(groupName, false);
                if (success2)
                {
                    listItem = item;
                }
                else
                {
                    //3.如果会话列表没有群聊，则打开搜索框，输入群聊名称搜索.
                    this.__SearchChat(groupName);
                    var (success3, item3) = __CheckConversationExist(groupName, false);
                    if (success3)
                    {
                        listItem = item3;
                    }
                    else
                    {
                        throw new Exception($"{groupName} 群聊不存在");
                    }
                }
                //4.执行设置保存到通讯录
                this._SaveToAddressCore(groupName, listItem, isSaveToAddress);
                _logger.Info($"设置{groupName}保存到通讯录成功");
                result.Success = true;
                result.Message = $"设置{groupName}保存到通讯录成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                result.Message = ex.Message;
                return result;
            }
        }
        /// <summary>
        /// 设置聊天置顶
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="isTop">是否置顶,默认是True:置顶,False:取消置顶</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse SetChatTop(string groupName, bool isTop = true)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                //1.首先检查子窗口有没有好友，如果有，则关闭窗口
                var (success, window) = __CheckSubWinIsOpen(groupName, false);
                if (success)
                {
                    window.Close();
                }
                ListBoxItem listItem = null;
                NavigationSwitch(NavigationType.聊天);
                //2.检查会话列表有没有好友，如果有，则点击打开为当前聊天
                var (success2, item) = __CheckConversationExist(groupName, false);
                if (success2)
                {
                    listItem = item;
                }
                else
                {
                    //3.如果会话列表没有好友，则打开搜索框，输入好友昵称搜索.
                    this.__SearchChat(groupName);
                    var (success3, item3) = __CheckConversationExist(groupName, false);
                    if (success3)
                    {
                        listItem = item3;
                    }
                    else
                    {
                        throw new Exception($"{groupName} 好友不存在");
                    }
                }
                //4.执行设置聊天置顶
                this._SetFriendChatTop(groupName, listItem, isTop);
                _logger.Info($"设置{groupName}聊天置顶成功");
                result.Success = true;
                result.Message = $"设置{groupName}聊天置顶成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                result.Message = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// 改变自有群群备注
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="newMemo">新备注</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse ChangeOwnerChatGroupMemo(string groupName, string newMemo)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                //1.首先检查子窗口有没有旧群名，如果有，则关闭窗口
                var (success, window) = __CheckSubWinIsOpen(groupName, false);
                if (success)
                {
                    _uiMainThreadInvoker.Run(automation => window.Close()).GetAwaiter().GetResult();
                }
                ListBoxItem listItem = null;
                NavigationSwitch(NavigationType.聊天);
                //2.检查会话列表有没有旧群名，如果有，则点击打开为当前聊天
                var (success2, item) = __CheckConversationExist(groupName, false);
                if (success2)
                {
                    listItem = item;
                }
                else
                {
                    //3.如果会话列表没有旧群名，则打开搜索框，输入旧群名搜索.
                    this.__SearchChat(groupName);
                    var (success3, item3) = __CheckConversationExist(groupName, false);
                    if (success3)
                    {
                        listItem = item3;
                    }
                    else
                    {
                        throw new Exception($"{groupName} 群聊不存在");
                    }
                }
                //4.执行改变群备注
                this._UpdateOnwerGroupMemo(groupName, newMemo, listItem);

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                result.Message = ex.Message;
                return result;
            }
        }
        /// <summary>
        /// 改变自有群群名
        /// </summary>
        /// <param name="oldGroupName">旧群名称</param>
        /// <param name="newGroupName">新群名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse ChangeOwnerChatGroupName(string oldGroupName, string newGroupName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                //1.首先检查子窗口有没有旧群名，如果有，则关闭窗口
                var (success, window) = __CheckSubWinIsOpen(oldGroupName, false);
                if (success)
                {
                    _uiMainThreadInvoker.Run(_ => window.Close());
                }
                ListBoxItem listItem = null;
                NavigationSwitch(NavigationType.聊天);
                //2.检查会话列表有没有旧群名，如果有，则点击打开为当前聊天
                var (success2, item) = __CheckConversationExist(oldGroupName, false);
                if (success2)
                {
                    listItem = item;
                }
                else
                {
                    //3.如果会话列表没有旧群名，则打开搜索框，输入旧群名搜索.
                    this.__SearchChat(oldGroupName);
                    var (success3, item3) = __CheckConversationExist(oldGroupName, false);
                    if (success3)
                    {
                        listItem = item3;
                    }
                    else
                    {
                        throw new Exception($"{oldGroupName} 群聊不存在");
                    }
                }
                //4.执行旧群名重命名新群名
                this._UpdateOnwerGroupName(oldGroupName, newGroupName, listItem);

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        private void _UpdateOnwerGroupName(string oldGroupName, string newGroupName, ListBoxItem firstItem)
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                firstItem.DrawHighlightExt();
                var xPath = "//Button";
                var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                if (itemButton != null)
                {
                    itemButton.DrawHighlightExt();
                    itemButton.Focus();
                    itemButton.WaitUntilClickable();
                    itemButton.RightClick();
                    this._OpenUpdateGroupNameWindow(newGroupName);
                }
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }
        private void _UpdateOnwerGroupMemo(string groupName, string newMemo, ListBoxItem firstItem)
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                firstItem.DrawHighlightExt();
                var xPath = "//Button";
                var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                if (itemButton != null)
                {
                    itemButton.DrawHighlightExt();
                    itemButton.Focus();
                    itemButton.WaitUntilClickable();
                    itemButton.RightClick();
                    this._OpenUpdateGroupWin(groupName, newMemo);
                }
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }
        private void _SetMessageWithoutInterruptionCore(string friendName, ListBoxItem firstItem, bool isMessageWithoutInterruption = true)
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                firstItem.DrawHighlightExt();
                var xPath = "//Button";
                var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                if (itemButton != null)
                {
                    itemButton.DrawHighlightExt();
                    itemButton.Focus();
                    itemButton.WaitUntilClickable();
                    itemButton.RightClick();
                    //设置消息免打扰
                    var winResult = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByClassName("CMenuWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                    if (winResult.Success)
                    {
                        var menu = winResult.Result.AsMenu();
                        menu.DrawHighlightExt();
                        var menuName = isMessageWithoutInterruption ? "消息免打扰" : "开启新消息提醒";
                        var item = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName(menuName)))?.AsMenuItem();
                        if (item != null)
                        {
                            item.DrawHighlightExt();
                            item.Focus();
                            item.WaitUntilClickable();
                            item.Click();
                            if (isMessageWithoutInterruption)
                            {
                                _logger.Info($"设置{friendName}消息免打扰成功");
                            }
                            else
                            {
                                _logger.Info($"设置{friendName}开启新消息提醒成功");
                            }
                        }
                        else
                        {
                            Trace.WriteLine($"没有找到{menuName}菜单项");
                            _logger.Error($"没有找到{menuName}菜单项");
                        }
                    }
                }
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }
        private void _SaveToAddressCore(string friendName, ListBoxItem firstItem, bool isSaveToAddress = true)
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                firstItem.DrawHighlightExt();
                var xPath = "//Button";
                var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                if (itemButton != null)
                {
                    itemButton.DrawHighlightExt();
                    itemButton.Focus();
                    itemButton.WaitUntilClickable();
                    itemButton.RightClick();
                    //设置保存到通讯录
                    var winResult = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByClassName("CMenuWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                    if (winResult.Success)
                    {
                        var menu = winResult.Result.AsMenu();
                        menu.DrawHighlightExt();
                        var menuName = isSaveToAddress ? "保存到通讯录" : "从通讯录中删除";
                        var item = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName(menuName)))?.AsMenuItem();
                        if (item != null)
                        {
                            item.DrawHighlightExt();
                            item.Focus();
                            item.WaitUntilClickable();
                            item.Click();
                            if (isSaveToAddress)
                            {
                                _logger.Info($"设置{friendName}保存到通讯录成功");
                            }
                            else
                            {
                                _logger.Info($"设置{friendName}从通讯录中删除成功");
                            }
                        }
                        else
                        {
                            Trace.WriteLine($"没有找到{menuName}菜单项");
                            _logger.Error($"没有找到{menuName}菜单项,可能已是[{menuName}]状态");
                        }
                    }
                }
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }
        private void _SetFriendChatTop(string friendName, ListBoxItem firstItem, bool isTop = true)
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                firstItem.DrawHighlightExt();
                var xPath = "//Button";
                var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                if (itemButton != null)
                {
                    itemButton.DrawHighlightExt();
                    itemButton.Focus();
                    itemButton.WaitUntilClickable();
                    itemButton.RightClick();
                    //设置聊天置顶
                    var winResult = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByClassName("CMenuWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                    if (winResult.Success)
                    {
                        var menu = winResult.Result.AsMenu();
                        menu.DrawHighlightExt();
                        var menuName = isTop ? "置顶" : "取消置顶";
                        var item = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName(menuName)))?.AsMenuItem();
                        if (item != null)
                        {
                            item.DrawHighlightExt();
                            item.Focus();
                            item.WaitUntilClickable();
                            item.Click();
                        }
                        else
                        {
                            Trace.WriteLine($"没有找到{menuName}菜单项");
                            _logger.Error($"没有找到{menuName}菜单项,可能已是{menuName}状态");
                            throw new Exception($"没有找到{menuName}菜单项,可能已是{menuName}状态");
                        }
                    }
                }
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }
        private void __SearchChat(string chatName)
        {
            this.Search.SearchChat(chatName);
        }
        /// <summary>
        /// 检查会话是否存在
        /// </summary>
        /// <param name="friendName">会话名称</param>
        /// <param name="inActionThread">是否在Action线程中执行，默认在Action线程中执行</param>
        /// <returns>是否存在,True:是,False:否</returns>
        private (bool success, ListBoxItem item) __CheckConversationExist(string friendName, bool inActionThread = true)
        {
            Func<(bool success, ListBoxItem item)> func = () =>
            {
                string xPath = $"/Pane/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_SESSION_BOX_CONVERSATION}'][@IsOffscreen='false']";
                var root = Retry.WhileNull(() => _MainWindow.FindFirstByXPath(xPath)?.AsListBox(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (root != null)
                {
                    root.Focus();
                }
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))?.ToList();
                var item = items?.FirstOrDefault(u => u.Name.Equals(friendName) || u.Name.Equals(friendName + WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP));
                return (item != null ? true : false, item?.AsListBoxItem());
            };
            if (inActionThread)
            {
                return func();
            }
            return _uiMainThreadInvoker.Run(automation => func()).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 判断子窗口是否打开
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <param name="inActionThread">是否在Action线程中执行，默认在Action线程中执行</param>
        /// <returns>如果子窗口存在，则返回true，否则返回false</returns>
        private (bool success, Window window) __CheckSubWinIsOpen(string name, bool inActionThread = true)
        {
            Func<(bool success, Window window)> func = () =>
            {
                var desktop = _MainWindow.Automation.GetDesktop();
                var result = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(this.ProcessId))
                        .And(cf.ByName(name)))),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
                return (result.Success && result.Result != null ? true : false, result.Result?.AsWindow());
            };
            if (inActionThread)
            {
                return func();
            }
            return _uiMainThreadInvoker.Run(automation => func()).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 创建群聊
        /// 如果存在，则打开它，否则创建一个新群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse CreateOrUpdateOwnerChatGroup(string groupName, OneOf<string, string[]> memberName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                var list = new List<string>();
                if (memberName.IsT0)
                {
                    list.Add(memberName.AsT0);
                }
                else
                {
                    list.AddRange(memberName.AsT1);
                }
                var flag = this.CheckFriendExist(groupName, true);
                if (flag)
                {
                    //打开群
                    NavigationSwitch(NavigationType.聊天);
                    result = this.AddOwnerChatGroupMember(groupName, memberName).Result;
                }
                else
                {
                    //新建
                    NavigationSwitch(NavigationType.聊天);
                    result = _CreateChatGroupCore(groupName, result, list);
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        /// <summary>
        /// 检查会话是否存在
        /// </summary>
        /// <param name="conversationName">会话名称</param>
        /// <param name="isDoubleClick">是否双击,True:是,False:否</param>
        /// <returns>是否存在,True:是,False:否</returns>
        private bool _CheckConversationExist(string conversationName, bool isDoubleClick = false)
        {
            var xPath = "//List[@Name='会话']";
            var root = _MainWindow.FindFirstByXPath(xPath)?.AsListBox();
            root.DrawHighlightExt();
            if (root != null)
            {
                var list = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))?.ToList();
                if (list != null && list.Count > 0)
                {
                    var item = list.FirstOrDefault(cItem => cItem.Name.Equals(conversationName) || cItem.Name.Equals(conversationName + WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP));
                    if (item != null)
                    {
                        if (isDoubleClick)
                        {
                            var button = item.FindFirstByXPath("//Button")?.AsButton();
                            if (button != null)
                            {
                                //确保按钮可见
                                while (button.Properties.IsOffscreen.Value)
                                {
                                    var scrollPattern = root.Patterns.Scroll.Pattern;
                                    if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                                    {
                                        double currentPercent = scrollPattern.VerticalScrollPercent;
                                        double newPercent = Math.Min(currentPercent + scrollPattern.VerticalViewSize, 1);
                                        scrollPattern.SetScrollPercent(0, newPercent);
                                        Thread.Sleep(600);
                                        var buttonResult = Retry.WhileNull(() => item.FindFirstByXPath(xPath)?.AsButton());
                                        button = buttonResult.Result;
                                    }
                                    else
                                    {
                                        _logger.Trace($"会话列表不可滚动，无法定位会话按钮元素");
                                        break;
                                    }
                                }

                                button = _IsButtonVisible(root, item);

                                button.Focus();
                                button.WaitUntilClickable();
                                button.DoubleClick();
                                Thread.Sleep(300);
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private Button _IsButtonVisible(ListBox root, AutomationElement item)
        {
            Button button = item.FindFirstByXPath("//Button")?.AsButton();
            var centerPoint = button.BoundingRectangle.Center();
            var bottomPoint = root.BoundingRectangle.Bottom;
            if (centerPoint.Y + 5 >= bottomPoint)
            {
                var scrollPattern = root.Patterns.Scroll.Pattern;
                double currentPercent = scrollPattern.VerticalScrollPercent;
                double newPercent = Math.Min(currentPercent + scrollPattern.VerticalViewSize, 1);
                scrollPattern.SetScrollPercent(0, newPercent);
                Thread.Sleep(600);
            }
            button = item.FindFirstByXPath("//Button")?.AsButton();
            var topPoint = root.BoundingRectangle.Top;
            if (centerPoint.Y - 5 <= topPoint)
            {
                var scrollPattern = root.Patterns.Scroll.Pattern;
                double currentPercent = scrollPattern.VerticalScrollPercent;
                double newPercent = Math.Max(currentPercent - scrollPattern.VerticalViewSize, 0);
                scrollPattern.SetScrollPercent(0, newPercent);
                Thread.Sleep(600);
            }

            return button;
        }

        /// <summary>
        /// 检查子窗口是否存在
        /// </summary>
        /// <param name="subWinName">子窗口名称</param>
        /// <returns>是否存在,True:是,False:否</returns>
        private bool _CheckSubWinExist(string subWinName)
        {
            var result = Retry.WhileNull(() => _MainWindow.Automation.GetDesktop().FindFirstChild(cf => cf.ByClassName("ChatWnd")
                         .And(cf.ByControlType(ControlType.Window)
                         .And(cf.ByProcessId(this.ProcessId))
                         .And(cf.ByName(subWinName)))),
                         timeout: TimeSpan.FromSeconds(5),
                         interval: TimeSpan.FromMilliseconds(200));
            return result.Success && result.Result != null;
        }
        /// <summary>
        /// 检查好友是否存在,好友可以为群聊与普通好友
        /// </summary>
        /// <param name="friendName">好友昵称</param>
        /// <param name="doubleClick">是否双击,True:是,False:否</param>
        /// <returns>是否存在,True:是,False:否</returns>
        public bool CheckFriendExist(string friendName, bool doubleClick = false)
        {
            Navigation.SwitchNavigation(NavigationType.聊天);
            var result = _uiMainThreadInvoker.Run(automation =>
            {
                //检查子窗口是否存在
                if (_CheckSubWinExist(friendName))
                {
                    return true;
                }
                //检查会话列表，如果会话列表中存在，则直接返回true
                if (_CheckConversationExist(friendName, doubleClick))
                {
                    return true;
                }
                var xPath = "//Edit[@Name='搜索']";
                var edit = _MainWindow.FindFirstByXPath(xPath)?.AsTextBox();
                if (edit != null)
                {
                    edit.Focus();
                    edit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Thread.Sleep(100);
                    Keyboard.Type(friendName);
                    // edit.Click();
                    Thread.Sleep(300);
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Thread.Sleep(1000);

                    return _CheckConversationExist(friendName, doubleClick);
                }
                return false;
            }).GetAwaiter().GetResult();

            return result;
        }

        private ChatResponse _CreateChatGroupCore(string groupName, ChatResponse result, List<string> list)
        {
            var tempName = _uiMainThreadInvoker.Run((automation) =>
            {
                var xPath = "//Button[@Name='发起群聊']";
                var button = Retry.WhileNull(() => _MainWindow.FindFirstByXPath(xPath)?.AsButton(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (button != null)
                {
                    button.DrawHighlightExt();
                    button.Click();
                    Thread.Sleep(600);
                    var AddMemberWnd = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("AddMemberWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                    if (AddMemberWnd != null)
                    {
                        var searchTextBox = AddMemberWnd.FindFirstByXPath("//Edit[@Name='搜索']")?.AsTextBox();
                        if (searchTextBox != null)
                        {
                            searchTextBox.DrawHighlightExt();
                            foreach (var member in list)
                            {
                                searchTextBox.Focus();
                                searchTextBox.Click();
                                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                                Thread.Sleep(200);
                                Keyboard.Type(member);
                                Wait.UntilInputIsProcessed();
                                Thread.Sleep(1000);
                                //选择的列表中打上勾
                                var listBox = AddMemberWnd.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                                if (listBox != null)
                                {
                                    var subList = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                                    subList = subList.Where(item => !string.IsNullOrWhiteSpace(item.Name) && item.Name == member).ToList();
                                    foreach (var subItem in subList)
                                    {
                                        var listItem = subItem.AsListBoxItem();
                                        xPath = "//Button";
                                        var itemButton = listItem.FindFirstByXPath(xPath)?.AsButton();
                                        if (itemButton != null)
                                        {
                                            itemButton.Focus();
                                            itemButton.WaitUntilClickable();
                                            itemButton.Click();
                                        }
                                        Thread.Sleep(300);
                                    }
                                }
                            }
                            var finishButton = AddMemberWnd.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                            if (finishButton != null)
                            {
                                finishButton.DrawHighlightExt();
                                finishButton.Focus();
                                finishButton.WaitUntilClickable();
                                finishButton.Click();
                                RandomWait.Wait(1000,3000);
                                //修改名字
                                var checkAddWinRresult = Retry.WhileNotNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("AddMemberWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                                if (checkAddWinRresult.Success)
                                {
                                    xPath = "//List[@Name='会话']";
                                    var cListItemBox = _MainWindow.FindFirstByXPath(xPath)?.AsListBox();
                                    Thread.Sleep(300);
                                    var cListItems = cListItemBox?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))?.ToList();
                                    cListItems = cListItems?.Where(item => !item.Name.EndsWith("已置顶"))?.ToList();
                                    var firstItemName = list[0].ToString();
                                    var firstItem = cListItems?.FirstOrDefault(item => item.Name.StartsWith(firstItemName) || item.Name.Contains("、"+firstItemName));
                                    if (firstItem != null)
                                    {
                                        firstItem.DrawHighlightExt();
                                        xPath = "//Button";
                                        var itemButton = firstItem.FindFirstByXPath(xPath)?.AsButton();
                                        if (itemButton != null)
                                        {
                                            itemButton.DrawHighlightExt();
                                            itemButton.Focus();
                                            itemButton.WaitUntilClickable();
                                            itemButton.RightClick();
                                            this._OpenUpdateGroupNameWindow(groupName);
                                        }
                                        Thread.Sleep(300);
                                        return firstItem.Name;
                                    }
                                }
                            }
                        }
                    }
                }
                return "";
            }).GetAwaiter().GetResult();
            Trace.WriteLine("临时群名称：" + tempName);
            result.Success = true;
            result.Message = "创建群聊成功";
            _logger.Info("创建临时群名称：" + tempName + "成功，下一步修改成正确的群名");
            return result;
        }

        private void _OpenUpdateGroupNameWindow(string groupName)
        {
            var winResult = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByClassName("CMenuWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
            if (winResult.Success)
            {
                var menu = winResult.Result.AsMenu();
                menu.DrawHighlightExt();
                var item = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("修改群聊名称")))?.AsMenuItem();
                if (item != null)
                {
                    item.DrawHighlightExt();
                    item.Focus();
                    item.WaitUntilClickable();
                    item.Click();
                    this._UpdateGroupName(groupName);
                }
                else
                {
                    Trace.WriteLine("没有找到修改群聊名称菜单项");
                }
            }
        }

        private void _OpenUpdateGroupWin(string groupName, string newMemo)
        {
            var winResult = Retry.WhileNull(() => _MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByClassName("CMenuWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
            if (winResult.Success)
            {
                var menu = winResult.Result.AsMenu();
                menu.DrawHighlightExt();
                var item = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("设置备注")))?.AsMenuItem();
                if (item != null)
                {
                    item.DrawHighlightExt();
                    item.Focus();
                    item.WaitUntilClickable();
                    item.Click();
                    this._UpdateGroupMemoCore(newMemo);
                }
                else
                {
                    Trace.WriteLine("没有找到设置备注菜单项");
                }
            }
        }
        private void _UpdateGroupMemoCore(string newMemo)
        {
            var modifyDialog = Retry.WhileNull(() => _MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("RoomInfoModifyDialog"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
            if (modifyDialog.Success)
            {
                var dialog = modifyDialog.Result.AsWindow();
                dialog.Focus();
                var xPath = "//Edit";
                var edit = dialog.FindFirstByXPath(xPath)?.AsTextBox();
                edit.Focus();
                edit.DrawHighlightExt();
                edit.WaitUntilClickable();
                edit.Click();
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                Keyboard.Type(newMemo);
                Wait.UntilInputIsProcessed();
                xPath = "//Button[@Name='确定']";
                var confirmButton = dialog.FindFirstByXPath(xPath)?.AsButton();
                confirmButton.DrawHighlightExt();
                confirmButton.Focus();
                confirmButton.WaitUntilClickable();
                confirmButton.Click();
                Thread.Sleep(1000);
                Wait.UntilInputIsProcessed();

                var cResult = Retry.WhileNull(() => _MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("RoomInfoModifyDialog"))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (cResult.Success)
                {
                    xPath = "//Button[@Name='取消']";
                    var cancelButton = dialog.FindFirstByXPath(xPath)?.AsButton();
                    cancelButton?.DrawHighlightExt();
                    cancelButton?.Focus();
                    cancelButton?.WaitUntilClickable();
                    cancelButton?.Click();
                }
            }
        }

        private void _UpdateGroupName(string groupName)
        {
            var modifyDialog = Retry.WhileNull(() => _MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("RoomInfoModifyDialog"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
            if (modifyDialog.Success)
            {
                var dialog = modifyDialog.Result.AsWindow();
                dialog.DrawHighlightExt();
                dialog.Focus();
                var xPath = "//Edit";
                var edit = dialog.FindFirstByXPath(xPath)?.AsTextBox();
                edit.Focus();
                edit.DrawHighlightExt();
                edit.WaitUntilClickable();
                edit.Click();
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                Keyboard.Type(groupName);
                Wait.UntilInputIsProcessed();
                xPath = "//Button[@Name='确定']";
                var confirmButton = dialog.FindFirstByXPath(xPath)?.AsButton();
                confirmButton.DrawHighlightExt();
                confirmButton.Focus();
                confirmButton.WaitUntilClickable();
                confirmButton.Click();
                Thread.Sleep(1000);
                Wait.UntilInputIsProcessed();

                var cResult = Retry.WhileNull(() => _MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("RoomInfoModifyDialog"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                if (cResult.Success)
                {
                    xPath = "//Button[@Name='取消']";
                    var cancelButton = dialog.FindFirstByXPath(xPath)?.AsButton();
                    cancelButton?.DrawHighlightExt();
                    cancelButton?.Focus();
                    cancelButton?.WaitUntilClickable();
                    cancelButton?.Click();
                }
            }
        }

        /// <summary>
        /// 添加群聊成员，适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> AddOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                await _SubWinList.CheckSubWinExistAndOpen(groupName);
                await Task.Delay(500);
                var subWin = _SubWinList.GetSubWin(groupName);
                result = subWin.AddOwnerChatGroupMember(memberName);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }

        }
        /// <summary>
        /// 删除群聊，适用于自有群,与退出群聊不同，退出群聊是退出群聊，删除群聊会删除自有群的所有好友，然后退出群聊
        /// willdo: 这里有一个问题，如果删除群的好友很多，则需要滚屏才能全部选中。
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> DeleteOwnerChatGroup(string groupName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                await _SubWinList.CheckSubWinExistAndOpen(groupName);
                await Task.Delay(500);
                var subWin = _SubWinList.GetSubWin(groupName);
                result = subWin.DeleteOwnerChatGroup();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }

        /// <summary>
        /// 移除群聊成员,适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> RemoveOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.RemoveOwnerChatGroupMember(memberName);
        }
        /// <summary>
        /// 邀请群聊成员,适用于外部群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> InviteChatGroupMember(string groupName, OneOf<string, string[]> memberName, string helloText = "")
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.InviteChatGroupMember(memberName, helloText);
        }

        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于从外部群中添加好友为自己的好友
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> AddChatGroupMemberToFriends(string groupName, OneOf<string, string[]> memberName, int intervalSecond = 3, string helloText = "")
        {
            return await this.AddChatGroupMemberToFriends(groupName, memberName, intervalSecond, helloText, "");
        }
        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于从外部群中添加好友为自己的好友
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <param name="label">好友标签,方便归类管理</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> AddChatGroupMemberToFriends(string groupName, OneOf<string, string[]> memberName, int intervalSecond = 5, string helloText = "", string label = "")
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.AddChatGroupMemberToFriends(memberName, intervalSecond, helloText, label);
        }

        /// <summary>
        /// 添加群聊里面的所有好友为自己的好友,适用于从外部群中添加所有好友为自己的好友
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="exceptList">排除列表</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> AddAllChatGroupMemberToFriends(string groupName, List<string> exceptList = null, int intervalSecond = 3, string helloText = "")
        {
            return await this.AddAllChatGroupMemberToFriends(groupName, exceptList, intervalSecond, helloText, "");
        }

        /// <summary>
        /// 添加群聊里面的所有好友为自己的好友,适用于从外部群中添加所有好友为自己的好友
        /// 风控提醒：
        /// 1、此方法容易触发微信风控机制，建议使用分页添加，并使用键鼠模拟器的方式增加好友。
        /// 1、微信对于加好友每天有数量的限制，实际测试一天只能加20多个，超出数量会返回[操作过于频繁，请稍后再试。]消息.
        /// 2、实际测试:使用键鼠模拟器的方式增加好友，只会受上述的增加好友数量限制，不会被风控退出。
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="exceptList">排除列表</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <param name="label">好友标签,方便归类管理</param>
        /// <param name="pageNo">起始页码,从1开始,如果从0开始，表示不使用分页，全部添加好友，但容易触发微信风控机制，建议使用分页添加</param>
        /// <param name="pageSize">页数量</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> AddAllChatGroupMemberToFriends(string groupName, List<string> exceptList = null, int intervalSecond = 3,
            string helloText = "", string label = "", int pageNo = 1, int pageSize = 15)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.AddAllChatGroupMemberToFriends(exceptList, intervalSecond, helloText, label, pageNo, pageSize);
        }
        /// <summary>
        /// 添加群聊里面的所有好友为自己的好友,适用于从外部群中添加所有好友为自己的好友
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="options">添加群聊成员为好友的选项<see cref="AddGroupMemberOptions"/></param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> AddAllChatGroupMemberToFriends(string groupName, Action<AddGroupMemberOptions> options)
        {
            var opitons = new AddGroupMemberOptions();
            options?.Invoke(opitons);
            return await AddAllChatGroupMemberToFriends(groupName, opitons.ExceptList, opitons.IntervalSecond, opitons.HelloText, opitons.Label, opitons.PageNo, opitons.PageSize);
        }


        /// <summary>
        /// 更新群聊公告
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="groupNotice">群聊公告</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> UpdateGroupNotice(string groupName, string groupNotice)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);

            return subWin.UpdateGroupNotice(groupNotice);
        }

        #endregion
        #endregion

        #region 释放资源
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~WeChatMainWindow()
        {
            Dispose(false);
        }
        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;
            if (disposing)
            {
                //释放托管资源
                this.ClearAllEvent();
                _newUserListenerCancellationTokenSource?.Cancel();

                if (_newUserListenerThread?.IsAlive ?? false)
                {
                    if (!_newUserListenerThread.Join(5000))
                    {
                        _newUserListenerThread.Interrupt();
                    }
                }

                _WxMainChatContent.Dispose();
                _moments.Dispose();
                _SubWinList.Dispose();

                _newUserListenerCancellationTokenSource?.Dispose();
                _newUserListenerStarted?.TrySetCanceled();
                _uiMainThreadInvoker?.Dispose();

                _newUserListenerCancellationTokenSource = null;
                _uiMainThreadInvoker = null;
            }
        }
        #endregion
    }
}
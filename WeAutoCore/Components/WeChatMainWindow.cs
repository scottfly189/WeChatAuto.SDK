using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using FlaUI.Core.Input;
using System.Threading.Tasks;
using OneOf;
using WxAutoCommon.Models;
using System;
using WeAutoCommon.Classes;
using WxAutoCore.Utils;
using System.Threading;
using WxAutoCommon.Configs;
using WxAutoCommon.Classes;
using System.Windows.Documents;
using FlaUI.Core.Tools;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WxAutoCore.Services;



namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端窗口,封装的微信窗口，包含工具栏、导航栏、搜索、会话列表、通讯录、聊天窗口等
    /// </summary>
    public class WeChatMainWindow : IWeChatWindow, IDisposable
    {
        private readonly ActionQueueChannel<ChatActionMessage> _actionQueueChannel = new ActionQueueChannel<ChatActionMessage>();
        private readonly AutoLogger<WeChatMainWindow> _logger;
        private WeChatFramwork _WeChatFramwork;
        private Window _Window;
        private ToolBar _ToolBar;  // 工具栏
        private SubWinList _SubWinList;  // 弹出窗口列表
        private Navigation _Navigation;  // 导航栏
        private Search _Search;  // 搜索
        private ConversationList _Conversations;  // 会话列表
        private AddressBookList _AddressBook;  // 通讯录
        private ChatContent _WxChatContent;  // 聊天窗口
        private IServiceProvider _serviceProvider;
        public ToolBar ToolBar => _ToolBar;  // 工具栏
        public Navigation Navigation => _Navigation;  // 导航栏
        public ConversationList Conversations => _Conversations;  // 会话列表
        public AddressBookList AddressBook => _AddressBook;  // 通讯录
        public Search Search => _Search;  // 搜索
        public ChatContent ChatContent => _WxChatContent;  // 聊天窗口
        public SubWinList SubWinList => _SubWinList;  // 子窗口列表
        public int ProcessId { get; private set; }
        public string NickName => _uiThreadInvoker.Run(automation => _Window.FindFirstByXPath($"/Pane/Pane/ToolBar[@Name='{WeChatConstant.WECHAT_NAVIGATION_NAVIGATION}'][@IsEnabled='true']").FindFirstChild().Name).Result;
        public Window Window => _Window;
        public WeChatClient Client { get; set; }
        private UIThreadInvoker _uiThreadInvoker;   //每个微信窗口一个单独的UI线程
        public UIThreadInvoker UiThreadInvoker => _uiThreadInvoker;
        private volatile bool _disposed = false;
        private Thread _newUserListenerThread;
        private CancellationTokenSource _newUserListenerCancellationTokenSource = new CancellationTokenSource();
        private List<(Action<List<string>> callBack, FriendListenerOptions options)> _newUserActionList = new List<(Action<List<string>> callBack, FriendListenerOptions options)>();
        private TaskCompletionSource<bool> _newUserListenerStarted = new TaskCompletionSource<bool>();
        public Window SelfWindow { get => _Window; set => _Window = value; }
        public WeChatFramwork WeChatFramwork => _WeChatFramwork;


        public ActionQueueChannel<ChatActionMessage> ActionQueueChannel => _actionQueueChannel;

        /// <summary>
        /// 微信客户端窗口构造函数
        /// </summary>
        /// <param name="window">微信窗口<see cref="Window"/></param>
        /// <param name="notifyIcon">微信通知图标<see cref="WeChatNotifyIcon"/></param>
        public WeChatMainWindow(Window window, WeChatNotifyIcon notifyIcon, WeChatFramwork weChatFramwork, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiThreadInvoker = new UIThreadInvoker();
            _WeChatFramwork = weChatFramwork;
            _Window = window;
            ProcessId = window.Properties.ProcessId;
            _logger = _serviceProvider.GetRequiredService<AutoLogger<WeChatMainWindow>>();
            _InitStaticWxWindowComponents(notifyIcon);
            _InitSubscription();
            _InitNewUserListener();
            _newUserListenerStarted.Task.Wait();
        }
        /// <summary>
        /// 初始化新用户监听
        /// </summary>
        private void _InitNewUserListener()
        {
            _newUserListenerThread = new Thread(async () =>
            {
                try
                {
                    IMEHelper.DisableImeForCurrentThread();
                    _newUserListenerStarted.SetResult(true);
                    while (!_newUserListenerCancellationTokenSource.IsCancellationRequested)
                    {
                        if (_newUserActionList.Count > 0)
                        {
                            await _FetchNewUserNoticeAction(_newUserListenerCancellationTokenSource.Token);
                        }
                        Thread.Sleep(WeAutomation.Config.NewUserListenerInterval * 1000);
                    }
                }
                catch (OperationCanceledException)
                {
                    Trace.WriteLine("新用户监听线程已停止，正常取消,不做处理");
                    _logger.Info("新用户监听线程已停止，正常取消,不做处理");
                }
                catch (Exception e)
                {
                    Trace.WriteLine("新用户监听线程异常，异常信息：" + e.Message);
                    _logger.Error("新用户监听线程异常，异常信息：" + e.Message);
                    throw;
                }
            });
            _newUserListenerThread.Priority = ThreadPriority.Lowest;
            _newUserListenerThread.IsBackground = true;
            _newUserListenerThread.Start();
        }
        private async Task _FetchNewUserNoticeAction(CancellationToken cancellationToken)
        {
            var resultFlag = _uiThreadInvoker.Run(automation =>
            {
                try
                {
                    IMEHelper.DisableImeForCurrentThread();
                    var xPath = "//ToolBar[@Name='导航']/Button[@Name='通讯录']";
                    if (!AutomationValid.IsValid(_Window))
                    {
                        var winResult = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByName("微信").And(cf.ByClassName("WeChatMainWndForPC")).And(cf.ByProcessId(_Window.Properties.ProcessId))).AsWindow(),
                            timeout: TimeSpan.FromSeconds(5),
                            interval: TimeSpan.FromMilliseconds(200));
                        if (winResult.Success)
                        {
                            _Window = winResult.Result;
                        }
                    }
                    var button = _Window.FindFirstByXPath(xPath)?.AsButton();
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
                    Trace.WriteLine("新用户监听线程异常，异常信息：" + ex.Message);
                    _logger.Error("新用户监听线程异常，异常信息：" + ex.Message);
                    return false;
                }
            }).Result;
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
                    var result = await _actionQueueChannel.PutAndWaitAsync(msg, cancellationToken);
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
            _uiThreadInvoker.Run(automation => automation.UnregisterAllEvents()).Wait();
        }
        /// <summary>
        /// 初始化微信窗口的各种组件,这些组件在微信窗口中是静态的，不会随着微信窗口的变化而变化
        /// </summary>
        private void _InitStaticWxWindowComponents(WeChatNotifyIcon notifyIcon)
        {
            _ToolBar = new ToolBar(_Window, notifyIcon, _uiThreadInvoker, _serviceProvider);  // 工具栏
            _Navigation = new Navigation(_Window, this, _uiThreadInvoker, _serviceProvider);  // 导航栏
            _Search = new Search(this, _uiThreadInvoker, _Window, _serviceProvider);  // 搜索
            _Conversations = new ConversationList(_Window, this, _uiThreadInvoker, _serviceProvider);  // 会话列表
            _AddressBook = new AddressBookList(_Window, this, _uiThreadInvoker, _serviceProvider);  // 通讯录
            _SubWinList = new SubWinList(_Window, this, _uiThreadInvoker, _serviceProvider);
            _WxChatContent = new ChatContent(_Window, ChatContentType.Inline, "/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane", this, _uiThreadInvoker, this, _serviceProvider);
        }
        /// <summary>
        /// 初始化订阅
        /// </summary>
        private void _InitSubscription()
        {
            Task.Run(async () =>
            {
                while (await _actionQueueChannel.WaitToReadAsync())
                {
                    var msg = await _actionQueueChannel.ReadAsync();
                    await _DispatchMessage(msg);
                }
            });
        }
        /// <summary>
        /// 消息分发
        /// <see cref="ChatActionMessage"/>
        /// </summary>
        /// <param name="msg">消息<see cref="ChatActionMessage"/></param>
        private async Task _DispatchMessage(ChatActionMessage msg)
        {
            switch (msg.Type)
            {
                case ActionType.发送消息:
                    await this.SendMessageCore(msg.ToUser, msg.Message, msg.IsOpenSubWin);
                    break;
                case ActionType.自定义表情:
                    await this.SendEmojiCore(msg);
                    break;
                case ActionType.发送文件:
                    await this.SendFileCore(msg);
                    break;
                case ActionType.添加好友:
                    await this.AddFriendCore(msg);
                    break;
                case ActionType.打开子窗口:
                    await this.OpenSubWinCore(msg);
                    break;
                default:
                    break;
            }
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
        /// 最小化后的还原操作
        /// </summary>
        public void WinMinRestore()
        {
            ToolBar.MinRestore();
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
        /// 单个查询，查询单个好友
        /// 注意：此方法不会打开子聊天窗口
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public async Task SendWho(string who, string message, OneOf<string, string[]> atUser = default)
        {
            if (atUser.Value != default)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        message = $"@{user} {message}";
                    },
                    (string[] atUsers) =>
                    {
                        var atUserList = atUsers.ToList();
                        var atUserString = "";
                        atUserList.ForEach(user =>
                        {
                            atUserString += $"@{user} ";
                        });
                        message = $"{atUserString} {message}";
                    }
                );
            }
            _actionQueueChannel.Put(new ChatActionMessage()
            {
                Type = ActionType.发送消息,
                ToUser = who,
                Message = message,
                IsOpenSubWin = false
            });
            await Task.CompletedTask;
        }
        /// <summary>
        /// 批量查询，查询多个好友
        /// 注意：此方法不会打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public void SendWhos(string[] whos, string message, OneOf<string, string[]> atUser = default)
        {
            whos.ToList().ForEach(async who =>
            {
                await SendWho(who, message, atUser);
            });
        }
        /// <summary>
        /// 单个查询，查询单个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public async Task SendWhoAndOpenChat(string who, string message, OneOf<string, string[]> atUser = default)
        {
            if (atUser.Value != null)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        message = $"@{user} {message}";
                    },
                    (string[] atUsers) =>
                    {
                        var atUserList = atUsers.ToList();
                        var atUserString = "";
                        atUserList.ForEach(user =>
                        {
                            atUserString += $"@{user} ";
                        });
                        message = $"{atUserString}{message}";
                    }
                );
            }
            _actionQueueChannel.Put(new ChatActionMessage()
            {
                Type = ActionType.发送消息,
                ToUser = who,
                Message = message,
                IsOpenSubWin = true
            });
            await Task.CompletedTask;
        }

        /// <summary>
        /// 批量查询，查询多个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public void SendWhosAndOpenChat(string[] whos, string message, OneOf<string, string[]> atUser = default)
        {
            whos.ToList().ForEach(async who => await SendWhoAndOpenChat(who, message, atUser));
        }
        /// <summary>
        /// 发送给当前聊天窗口
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public void SendCurrentMessage(string message, string atUser = null)
        {
            _actionQueueChannel.Put(new ChatActionMessage()
            {
                Type = ActionType.发送消息,
                ToUser = null,
                Message = message,
                IsOpenSubWin = false
            });
        }

        /// <summary>
        /// 给当前聊天窗口发送消息
        /// 可能存在不能发送消息的窗口情况.
        /// </summary>
        /// <param name="message">消息内容</param>
        private void __SendCurrentMessage(string message, string atUser = null)
        {
            if (atUser != null)
            {
                message = $"@{atUser} {message}";
            }
            this.ChatContent.ChatBody.Sender.SendMessage(message);
        }
        /// <summary>
        /// 获取当前聊天窗口的标题
        /// </summary>
        /// <returns>当前聊天窗口的标题</returns>
        public string GetCurrentChatTitle()
        {
            return this.ChatContent.ChatHeader.Title;
        }
        //打开的子窗口中有没有此用户
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
        //此用户是否是当前聊天窗口,如果当前聊天窗口是此用户，则发送消息
        private bool _IsCurrentChat(string who, string message, bool isOpenChat)
        {
            var currentChatTitle = this.GetCurrentChatTitle();
            if (string.IsNullOrEmpty(currentChatTitle))
            {
                return false;
            }
            if (currentChatTitle == who)
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    this.SendMessageCore(who, message, isOpenChat).Wait();
                }
                else
                {
                    this.__SendCurrentMessage(message);
                }
                return true;
            }
            return false;
        }
        //此用户是否是当前聊天窗口,如果当前聊天窗口是此用户，则发送文件
        private bool _IsCurrentChatFile(string who, string[] files)
        {
            var currentChatTitle = this.GetCurrentChatTitle();
            if (string.IsNullOrEmpty(currentChatTitle))
            {
                return false;
            }
            if (currentChatTitle == who)
            {
                this.ChatContent.ChatBody.Sender.SendFile(files);
                return true;
            }
            return false;
        }
        //此用户是否在会话列表中，如果存在，则打开或者点击此会话，并且发送消息
        private async Task<bool> _IsInConversation(string who, string message, bool isOpenChat)
        {
            var conversations = this.Conversations.GetVisibleConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    // this.Conversations.ClickFirstConversation();
                    await SendMessageCore(who, message, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.__SendCurrentMessage(message);
                    return true;
                }
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
                    this.ChatContent.ChatBody.Sender.SendFile(files);
                    return true;
                }
            }
            return false;
        }
        //此用户是否在搜索结果中
        private async Task<bool> _IsSearch(string who, string message, bool isOpenChat)
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
                    await SendMessageCore(who, message, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.__SendCurrentMessage(message);
                    return true;
                }
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
                    this.ChatContent.ChatBody.Sender.SendFile(files);
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
        /// <param name="who">好友名称</param>
        /// <param name="file">文件路径</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public void SendFile(string who, OneOf<string, string[]> file, bool isOpenChat = false)
        {
            ChatActionMessage msg = new ChatActionMessage();
            msg.Type = ActionType.发送文件;
            msg.ToUser = who;
            msg.Payload = file;
            msg.IsOpenSubWin = isOpenChat;
            _actionQueueChannel.Put(msg);
        }
        /// <summary>
        /// 给多个好友发送文件
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        /// <param name="file">文件路径</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public void SendFiles(string[] whos, OneOf<string, string[]> file, bool isOpenChat = false)
        {
            whos.ToList().ForEach(who => SendFile(who, file, isOpenChat));
        }


        #endregion
        #region 发送表情操作
        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <param name="emoji">表情名称</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public void SendEmoji(string who, OneOf<int, string> emoji, bool isOpenChat = false)
        {
            ChatActionMessage msg = new ChatActionMessage();
            msg.Type = ActionType.自定义表情;
            msg.ToUser = who;
            msg.Payload = emoji;
            msg.IsOpenSubWin = isOpenChat;
            _actionQueueChannel.Put(msg);
        }

        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        /// <param name="emoji">表情名称</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        public void SendEmojis(string[] whos, OneOf<int, string> emoji, bool isOpenChat = false)
        {
            whos.ToList().ForEach(who => SendEmoji(who, emoji, isOpenChat));
        }
        #endregion
        #region 实际发送消息、文件、表情操作
        /// <summary>
        /// 发送消息核心方法
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        private async Task SendMessageCore(string who, string message, bool isOpenChat = false)
        {
            try
            {
                if (string.IsNullOrEmpty(who))
                {
                    //发送给当前聊天窗口
                    this.__SendCurrentMessage(message);
                    return;
                }
                else
                {
                    //发送给指定好友
                    //步骤：
                    //1.首先查询此用户是否在弹出窗口列表中
                    //2.如果存在，则用弹出窗口发出消息
                    if (_SubWindowIsOpen(who, message, subWin => subWin.ChatContent.ChatBody.Sender.SendMessage(message)))
                    {
                        return;
                    }
                    //3.如果不存在，则查询当前聊天窗口是否是此用户(即who)
                    //4.如果是，则发送消息
                    if (_IsCurrentChat(who, message, isOpenChat))
                    {
                        return;
                    }
                    //5.如果不是，则查询此用户是否在会话列表中
                    //6.如果存在，则打开或者点击此会话，并且发送消息
                    if (await _IsInConversation(who, message, isOpenChat))
                    {
                        return;
                    }
                    //7.如果不存在，则进行查询,如果查询到有此用户，则打开或者点击此会话，并且发送消息
                    //8.如果查询不到，则提示用户不存在.
                    if (await _IsSearch(who, message, isOpenChat))
                    {
                        return;
                    }

                    System.Windows.MessageBox.Show($"错误：用户[{who}]不存在,请检查您的输入是否正确",
                        "错误",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error);

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
            if (_IsCurrentChatFile(who, files))
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

            System.Windows.MessageBox.Show($"用户{who}不存在,请检查您的输入是否正确",
                "错误",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
        // 发送表情核心方法
        private async Task SendEmojiCore(ChatActionMessage msg)
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

                msg.Message = message;
                await SendMessageCore(msg.ToUser, message, msg.IsOpenSubWin);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("发送表情失败：" + ex.Message);
                _logger.Error("发送表情失败：" + ex.Message);
            }
        }
        private async Task OpenSubWinCore(ChatActionMessage msg)
        {
            TaskCompletionSource<object> tcs = (TaskCompletionSource<object>)msg.Tcs;
            try
            {
                _Navigation.SwitchNavigation(NavigationType.聊天);
                string who = msg.ToUser;
                if (await _IsInConversation(who))
                {
                    tcs.SetResult(true);
                    return;
                }
                if (await _IsSearch(who))
                {
                    tcs.SetResult(true);
                    return;
                }
                tcs.SetResult(false);
            }
            catch (Exception ex)
            {
                _logger.Error("添加好友失败：" + ex.Message);
                tcs.SetException(ex);
            }

            await tcs.Task;
        }
        private async Task AddFriendCore(ChatActionMessage msg)
        {
            TaskCompletionSource<object> tcs = (TaskCompletionSource<object>)msg.Tcs;
            FriendListenerOptions options = (FriendListenerOptions)msg.Payload;
            try
            {
                if (options != null)
                {
                    //自动通过新好友后返回
                    var list = _AddressBook.PassedAllNewFriend(options.KeyWord, options.Suffix, options.Label);
                    tcs.SetResult(list);
                }
                else
                {
                    //仅获取所有新加好友列表，不自动通过，需要用户手动通过
                    var list = _AddressBook.GetAllFriends();
                    tcs.SetResult(list);
                }

                await tcs.Task;
            }
            catch (Exception ex)
            {
                _logger.Error("添加好友失败：" + ex.Message);
                Trace.WriteLine("添加好友失败：" + ex.Message);
                tcs.SetException(ex);
                throw;
            }
        }
        #endregion

        #region 监听消息
        /// <summary>
        /// 添加消息监听，用户需要提供一个回调函数，当有消息时，会调用回调函数
        /// callBack回调函数参数：
        /// 1.新消息气泡<see cref="MessageBubble"/>
        /// 2.包含新消息气泡的列表<see cref="List{MessageBubble}"/>，适用于给LLM大模型提供上下文
        /// 3.发送者<see cref="Sender"/>，适用于本子窗口操作，如发送消息、发送文件、发送表情等
        /// 4.当前微信窗口对象<see cref="WeChatMainWindow"/>，适用于全部操作，如给指定好友发送消息、发送文件、发送表情等
        /// 5.服务提供者<see cref="IServiceProvider"/>，适用于使用者传入服务提供者，用于有户获取自己注入的服务
        /// </summary>
        /// <param name="nickName">好友名称</param>
        /// <param name="callBack">回调函数,由好友提供</param>
        public async Task AddMessageListener(string nickName, Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork, IServiceProvider> callBack)
        {
            await _SubWinList.CheckSubWinExistAndOpen(nickName);
            await Task.Delay(500);
            _SubWinList.RegisterMonitorSubWin(nickName);
            await _SubWinList.AddMessageListener(callBack, nickName);
        }
        /// <summary>
        /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会调用回调函数
        /// 此方法需要自行处理好友是否通过，如果需要自动通过，请使用<see cref="AddNewFriendAutoPassedListener"/>
        /// </summary>
        /// <param name="callBack">回调函数</param>
        public void AddNewFriendCustomPassedListener(Action<List<string>> callBack)
        {
            _AddNewFriendListener(callBack, null);
        }
        /// <summary>
        /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会调用回调函数
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
        /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会自动通过此用户，并且将此用户打开到子窗口，当有新消息时，会调用回调函数
        /// </summary>
        /// <param name="callBack">回调函数</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="suffix">后缀</param>
        /// <param name="label">标签</param>
        public void AddNewFriendAutoPassedAndOpenSubWinListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork, IServiceProvider> callBack, string keyWord = null, string suffix = null, string label = null)
        {
            _AddNewFriendListener(nickNameList =>
            {
                nickNameList.ForEach(async nickName =>
                {
                    await this.AddMessageListener(nickName, callBack);
                });
            }, new FriendListenerOptions() { KeyWord = keyWord, Suffix = suffix, Label = label });
        }

        /// <summary>
        /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会调用回调函数
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
        /// <param name="nickName">好友名称</param>
        public void StopMessageListener(string nickName)
        {
            _SubWinList.StopMessageListener(nickName);
        }
        /// <summary>
        /// 移除添加新用户监听
        /// </summary>
        public void StopNewUserListener()
        {
            _newUserActionList.Clear();
        }
        #endregion
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            _actionQueueChannel.Close();
            _uiThreadInvoker.Dispose();
            _newUserListenerCancellationTokenSource.Cancel();
            if (_newUserListenerThread.IsAlive)
            {
                _newUserListenerThread.Join(1000);
            }
            _newUserListenerCancellationTokenSource.Dispose();
        }

        #region 群聊操作
        #region 群基础操作，适用于自有群与他有群
        /// <summary>
        /// 更新群聊选项
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="action">更新群聊选项的Action<see cref="ChatGroupOptions"/></param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public async Task<ChatResponse> UpdateChatGroupOptions(string groupName, Action<ChatGroupOptions> action)
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
        /// <returns>是否是群聊成员</returns>
        /// <returns>是否是群主</returns>
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
        /// 清空群聊历史
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
        /// 创建群聊
        /// 如果存在，则打开
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse CreateOwnerChatGroup(string groupName, OneOf<string, string[]> memberName)
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
                var flag = this.AddressBook.LocateFriend(groupName);
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
                return result;
            }
        }

        private ChatResponse _CreateChatGroupCore(string groupName, ChatResponse result, List<string> list)
        {
            result = _uiThreadInvoker.Run((automation) =>
            {
                var xPath = "/Pane/Pane/Pane/Pane/Pane/Button[Name='发起群聊']";
                var button = Retry.WhileNull(() => _Window.FindFirstByXPath(xPath)?.AsButton(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (button != null)
                {
                    button.Click();
                    Thread.Sleep(600);
                    var AddMemberWnd = Retry.WhileNull(() => _Window.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("AddMemberWnd"))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                    if (AddMemberWnd != null)
                    {
                        var searchTextBox = AddMemberWnd.FindFirstByXPath("//Edit[@Name='搜索']")?.AsTextBox();
                        if (searchTextBox != null)
                        {
                            foreach (var member in list)
                            {
                                searchTextBox.Focus();
                                searchTextBox.Click();
                                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                                Keyboard.Type(member);
                                Keyboard.Press(VirtualKeyShort.RETURN);
                                Wait.UntilInputIsProcessed();
                                //选择的列表中打上勾
                                var listBox = AddMemberWnd.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                                if (listBox != null)
                                {
                                    var subList = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).ToList();
                                    subList = subList.Where(item => !string.IsNullOrWhiteSpace(item.Name) && item.Name == member).ToList();
                                    foreach (var subItem in subList)
                                    {
                                        var checkBox = subItem.AsCheckBox();
                                        checkBox.ToggleState = ToggleState.On;
                                        Thread.Sleep(300);
                                    }
                                }
                            }
                            var finishButton = AddMemberWnd.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                            if (finishButton != null)
                            {
                                finishButton.Focus();
                                finishButton.WaitUntilClickable();
                                finishButton.Click();
                                Thread.Sleep(600);
                                //修改名字
                                xPath = "//List[@Name='会话']";
                                var cListItemBox = _Window.FindFirstByXPath(xPath)?.AsListBox();
                                var cListItems = cListItemBox?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))?.ToList();
                                cListItems = cListItems?.Where(item => !item.Name.EndsWith("已置顶"))?.ToList();
                                var firstItem = cListItems?.FirstOrDefault();
                                if (firstItem != null)
                                {
                                    var tempName = firstItem.Name;
                                    UpdateChatGroupOptions(tempName, options =>
                                    {
                                        options.GroupName = groupName;
                                    }).Wait();

                                    result.Success = true;
                                    result.Message = "创建群聊成功";
                                    return result;
                                }
                            }
                        }
                    }
                }
                return result;
            }).Result;
            return result;
        }

        /// <summary>
        /// 添加群聊成员，适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
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
                return result;
            }

        }
        /// <summary>
        /// 删除群聊，适用于自有群,与退出群聊不同，退出群聊是退出群聊，删除群聊会删除自有群的所有好友，然后退出群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果</returns>
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
                return result;
            }
        }

        /// <summary>
        /// 发送群聊公告
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="notice">公告内容</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> PublishOwnerChatGroupNotice(string groupName, string notice)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.PublishOwnerChatGroupNotice(notice);
        }
        /// <summary>
        /// 移除群聊成员,适用于自有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> RemoveOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.RemoveOwnerChatGroupMember(memberName);
        }
        /// <summary>
        /// 邀请群聊成员,适用于他有群
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> InviteChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.InviteChatGroupMember(memberName);
        }

        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于从他有群中添加好友为自己的好友
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public async Task<ChatResponse> AddChatGroupMemberToFriends(string groupName, OneOf<string, string[]> memberName)
        {
            await _SubWinList.CheckSubWinExistAndOpen(groupName);
            await Task.Delay(500);
            var subWin = _SubWinList.GetSubWin(groupName);
            return subWin.AddChatGroupMemberToFriends(memberName);
        }

        #endregion
        #endregion
    }
}
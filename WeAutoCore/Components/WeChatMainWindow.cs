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



namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端窗口,封装的微信窗口，包含工具栏、导航栏、搜索、会话列表、通讯录、聊天窗口等
    /// </summary>
    public class WeChatMainWindow : IWeChatWindow, IDisposable
    {
        private UIThreadInvoker _uiThreadInvoker;
        private readonly ActionQueueChannel<ChatMessage> _actionQueueChannel = new ActionQueueChannel<ChatMessage>();
        private Window _Window;
        private ToolBar _ToolBar;  // 工具栏
        private SubWinList _SubWinList;  // 弹出窗口列表
        private Navigation _Navigation;  // 导航栏
        private Search _Search;  // 搜索
        private ConversationList _Conversations;  // 会话列表
        private AddressBookList _AddressBook;  // 通讯录
        private ChatContent _WxChatContent;  // 聊天窗口
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

        public Window SelfWindow { get => _Window; set => _Window = value; }

        /// <summary>
        /// 微信客户端窗口构造函数
        /// </summary>
        /// <param name="window">微信窗口<see cref="Window"/></param>
        /// <param name="notifyIcon">微信通知图标<see cref="WeChatNotifyIcon"/></param>
        public WeChatMainWindow(Window window, WeChatNotifyIcon notifyIcon, UIThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
            _InitSubscription();
            _Window = window;
            ProcessId = window.Properties.ProcessId;
            _InitWxWindow(notifyIcon);
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
        /// <see cref="ChatMessage"/>
        /// </summary>
        /// <param name="msg"></param>
        private async Task _DispatchMessage(ChatMessage msg)
        {
            switch (msg.Type)
            {
                case ChatMsgType.发送消息:
                    await this.SendMessageCore(msg.ToUser, msg.Message, msg.IsOpenSubWin);
                    break;
                case ChatMsgType.自定义表情:
                    await this.SendEmojiCore(msg);
                    break;
                case ChatMsgType.发送文件:
                    await this.SendFileCore(msg);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化微信窗口的各种组件
        /// </summary>
        private void _InitWxWindow(WeChatNotifyIcon notifyIcon)
        {
            _ToolBar = new ToolBar(_Window, notifyIcon, _uiThreadInvoker);  // 工具栏
            _Navigation = new Navigation(_Window, this, _uiThreadInvoker);  // 导航栏
            _Search = new Search(this, _uiThreadInvoker, _Window);  // 搜索
            _Conversations = new ConversationList(_Window, this, _uiThreadInvoker);  // 会话列表
            _SubWinList = new SubWinList(_Window, this, _uiThreadInvoker);
            _WxChatContent = new ChatContent(_Window, ChatContentType.Inline, "/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane", this, _uiThreadInvoker);
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
                        message = $"{atUserString} {message}";
                    }
                );
            }
            _actionQueueChannel.Put(new ChatMessage()
            {
                Type = ChatMsgType.发送消息,
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
            _actionQueueChannel.Put(new ChatMessage()
            {
                Type = ChatMsgType.发送消息,
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
            _actionQueueChannel.Put(new ChatMessage()
            {
                Type = ChatMsgType.发送消息,
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
            var conversations = this.Conversations.GetConversationTitles();
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
        //此用户是否在会话列表中，如果存在，则打开或者点击此会话，并且发送文件
        private async Task<bool> _IsInConversationFile(string who, string[] files, bool isOpenChat)
        {
            var conversations = this.Conversations.GetConversationTitles();
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
            var conversations = this.Conversations.GetConversationTitles();
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
        //此用户是否在搜索结果中，如果存在，则打开或者点击此会话，并且发送文件
        private async Task<bool> _IsSearchFile(string who, string[] files, bool isOpenChat)
        {
            this.Search.SearchChat(who);
            await Task.Delay(1000);
            var conversations = this.Conversations.GetConversationTitles();
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
            ChatMessage msg = new ChatMessage();
            msg.Type = ChatMsgType.发送文件;
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
            ChatMessage msg = new ChatMessage();
            msg.Type = ChatMsgType.自定义表情;
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
                Console.WriteLine("发送消息失败：" + ex.Message);
            }
        }
        //发送文件核心方法
        private async Task SendFileCore(ChatMessage msg)
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
                Console.WriteLine("发送文件失败：" + ex.Message);
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
        private async Task SendEmojiCore(ChatMessage msg)
        {
            try
            {
                OneOf<int, string> emoji = (OneOf<int, string>)msg.Payload;
                msg.Type = ChatMsgType.发送消息;
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
                Console.WriteLine("发送表情失败：" + ex.Message);
            }
        }
        #endregion

        public void Dispose()
        {
            _actionQueueChannel.Close();
        }
    }
}
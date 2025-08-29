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



namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端窗口,封装的微信窗口，包含工具栏、导航栏、搜索、会话列表、通讯录、聊天窗口等
    /// </summary>
    public class WeChatMainWindow : IWeChatWindow, IDisposable
    {
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
        public string NickName => _Window.FindFirstByXPath($"/Pane/Pane/ToolBar[@Name='{WeChatConstant.WECHAT_NAVIGATION_NAVIGATION}'][@IsEnabled='true']").FindFirstChild().Name;
        public Window Window => _Window;

        public Window SelfWindow { get => _Window; set => _Window = value; }

        /// <summary>
        /// 微信客户端窗口构造函数
        /// </summary>
        /// <param name="window">微信窗口<see cref="Window"/></param>
        /// <param name="notifyIcon">微信通知图标<see cref="WeChatNotifyIcon"/></param>
        public WeChatMainWindow(Window window, WeChatNotifyIcon notifyIcon)
        {
            _InitSubscription();
            _Window = window;
            ProcessId = window.Properties.ProcessId;
            _InitWxWindow(notifyIcon);
        }
        private void _InitSubscription()
        {
            Task.Run(async () =>
            {
                while (await _actionQueueChannel.WaitToReadAsync())
                {
                    var msg = await _actionQueueChannel.ReadAsync();
                    await SendMessageCore(msg);
                }
            });
        }
        /// <summary>
        /// 发送消息核心方法
        /// <see cref="ChatMessage"/>
        /// </summary>
        /// <param name="msg"></param>
        private async Task SendMessageCore(ChatMessage msg)
        {
            switch (msg.Type)
            {
                case ChatMsgType.发送消息:
                    await this.SendWhoCore(msg.ToUser, msg.Message, msg.IsOpenChat);
                    break;
                case ChatMsgType.自定义表情:
                    break;
                case ChatMsgType.发送文件:
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
            _ToolBar = new ToolBar(_Window, notifyIcon);  // 工具栏
            _Navigation = new Navigation(_Window, this);  // 导航栏
            _Search = new Search(this);  // 搜索
            _Conversations = new ConversationList(_Window, this);  // 会话列表
            _SubWinList = new SubWinList(_Window, this);
            _WxChatContent = new ChatContent(_Window, ChatContentType.Inline, "/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane", this);
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
                IsOpenChat = false
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
                IsOpenChat = true
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
        /// 给当前聊天窗口发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void SendMessage(string message, string atUser = null)
        {
            if (atUser != null)
            {
                message = $"@{atUser} {message}";
            }
            ChatMessage msg = new ChatMessage()
            {
                Type = ChatMsgType.发送消息,
                ToUser = this.GetCurrentChatTitle(),
                Message = message,
                IsOpenChat = false
            };
            if (string.IsNullOrEmpty(msg.ToUser))
            {
                return;
            }
            _actionQueueChannel.Put(msg);
        }
        /// <summary>
        /// 获取当前聊天窗口的标题
        /// </summary>
        /// <returns>当前聊天窗口的标题</returns>
        public string GetCurrentChatTitle()
        {
            return this.ChatContent.ChatHeader.Title;
        }
        /// <summary>
        /// 发送消息核心方法
        /// </summary>
        /// <param name="who">好友名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="isOpenChat">是否打开子聊天窗口</param>
        private async Task SendWhoCore(string who, string message, bool isOpenChat = false)
        {
            //步骤：
            //1.首先查询此用户是否在弹出窗口列表中
            //2.如果存在，则用弹出窗口发出消息
            if (SubWindowIsOpen(who, message))
            {
                return;
            }
            //3.如果不存在，则查询当前聊天窗口是否是此用户(即who)
            //4.如果是，则发送消息
            if (IsCurrentChat(who, message))
            {
                return;
            }
            //5.如果不是，则查询此用户是否在会话列表中
            //6.如果存在，则打开或者点击此会话，并且发送消息
            if (await IsConversation(who, message, isOpenChat))
            {
                return;
            }
            //7.如果不存在，则进行查询,如果查询到有此用户，则打开或者点击此会话，并且发送消息
            //8.如果查询不到，则提示用户不存在.
            if (await IsSearch(who, message, isOpenChat))
            {
                return;
            }

            System.Windows.MessageBox.Show($"用户{who}不存在,请检查您的输入是否正确",
                "错误",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);

        }
        private bool SubWindowIsOpen(string who, string message)
        {
            var subWin = this.SubWinList.GetSubWin(who);
            if (subWin != null)
            {
                subWin.ChatContent.ChatBody.Sender.SendMessage(message);
                return true;
            }
            return false;
        }

        private bool IsCurrentChat(string who, string message)
        {
            var currentChatTitle = this.GetCurrentChatTitle();
            if (string.IsNullOrEmpty(currentChatTitle))
            {
                return false;
            }
            if (currentChatTitle == who)
            {
                this.SendMessage(message);
                return true;
            }
            return false;
        }
        private async Task<bool> IsConversation(string who, string message, bool isOpenChat)
        {
            var conversations = this.Conversations.GetConversationTitles();
            if (conversations.Contains(who))
            {
                if (isOpenChat)
                {
                    this.Conversations.DoubleClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    await SendWhoCore(who, message, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.SendMessage(message);
                    return true;
                }
            }
            return false;
        }
        private async Task<bool> IsSearch(string who, string message, bool isOpenChat)
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
                    await SendWhoCore(who, message, isOpenChat); // 由于是双击会话,弹出窗口实例已经存在，所以需要从弹出窗口重新发送消息
                    return true;
                }
                else
                {
                    this.Conversations.ClickConversation(who);
                    Wait.UntilInputIsProcessed();
                    this.SendMessage(message);
                    return true;
                }
            }
            this.Search.ClearText();
            return false;
        }
        #endregion

        #region 发送文件操作
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="file">文件路径</param>
        public void SendFile(string file)
        {
        }
        #endregion
        #region 发送表情操作
        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="emoji">表情名称</param>
        public void SendEmoji(int emojiId)
        {
        }
        #endregion

        public void Dispose()
        {
            _actionQueueChannel.Close();
        }
    }
}
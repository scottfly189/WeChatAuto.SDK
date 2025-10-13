using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using OneOf;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private ChatContent _ChatContent;
        private WeChatMainWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window
        private int _ProcessId;
        private UIThreadInvoker _uiThreadInvoker;
        private SubWinList _SubWinList;
        private ChatBody _ChatBodyCache;
        public Window SelfWindow { get => _SelfWindow; set => _SelfWindow = value; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public ChatContent ChatContent => _ChatContent;
        public int ProcessId => _ProcessId;
        private volatile bool _disposed = false;


        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口的微信窗口对象</param>
        /// <param name="subWinList">子窗口列表</param>
        /// <param name="serviceProvider">服务提供者</param>
        public SubWin(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, string title, SubWinList subWinList, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiThreadInvoker = uiThreadInvoker;
            _SubWinList = subWinList;
            NickName = title;
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane", this, uiThreadInvoker, this._MainWxWindow, _serviceProvider);
            _ProcessId = _SelfWindow.Properties.ProcessId.Value;
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack"></param>
        public void AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork, IServiceProvider> callBack)
        {
            if (_disposed)
            {
                return;
            }
            if (_ChatBodyCache == null)
            {
                _ChatBodyCache = _ChatContent.ChatBody;
            }
            _ChatBodyCache.AddListener(callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            if (_disposed)
            {
                return;
            }

            _ChatBodyCache?.StopListener();
        }

        #region 群聊操作

        #region 群基础操作，适用于自有群与他有群
        /// <summary>
        /// 更新群聊选项
        /// </summary>
        /// <param name="action">更新群聊选项的Action</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse UpdateChatGroupOptions(Action<ChatGroupOptions> action)
        {
            ChatResponse result = new ChatResponse();
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            Thread.Sleep(500);
            _FocuseSearchText();
            ChatGroupOptions options = _InitChatGroupOptions();
            options.Reset();
            Trace.WriteLine(options.ToString());

            //action(options);
            //_UpdateChatGroupOptions(options);
            return result;
        }
        /// <summary>
        /// 初始化群聊选项
        /// </summary>
        /// <returns></returns>
        private ChatGroupOptions _InitChatGroupOptions()
        {
            ChatGroupOptions options = new ChatGroupOptions();
            _uiThreadInvoker.Run(automation =>
            {
                var rootXPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane";
                var root = _SelfWindow.FindFirstByXPath(rootXPath);   //根节点
                var button = root.FindFirstByXPath("/Pane/Pane/Pane/Button[@Name='群聊名称']")?.AsButton();
                _FetchGroupName(options, button);
                var element = button.GetParent().GetParent().GetSibling(1);
                _FetchGroupNotice(options, element);
                element = element.GetSibling(1); //获取群聊备注
                _FetchGroupMemo(options, element);
                element = element.GetSibling(1); //群昵称
                _FetchMyGroupNickName(options, element);
                element = element.GetSibling(2); //是否显示群昵称
                _FetchShowGroupNickName(options, element);
                element = element.GetSibling(1); //是否免打扰
                _FetchNoDisturb(options, element);
                element = element.GetSibling(1); //是否置顶
                _FetchTop(options, element);
                element = element.GetSibling(1); //是否保存至通讯录
                _FetchSaveToAddressBook(options, element);

            }).Wait();
            return options;
        }

        //获取是否保存至通讯录
        private void _FetchSaveToAddressBook(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='保存到通讯录']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.SaveToAddressBook = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否置顶
        private void _FetchTop(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='置顶聊天']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.Top = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否免打扰
        private void _FetchNoDisturb(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='消息免打扰']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.NoDisturb = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否显示群昵称
        private void _FetchShowGroupNickName(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='显示群成员昵称']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.ShowGroupNickName = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取群聊昵称
        private void _FetchMyGroupNickName(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//Button[@Name='我在本群的昵称']";
            var button = element.FindFirstByXPath(xPath)?.AsButton();
            if (button.Patterns.Value.IsSupported)
            {
                var pattern = button.Patterns.Value.Pattern;
                options.MyGroupNickName = pattern.Value.Value;
            }
        }
        //获取群聊备注
        private void _FetchGroupMemo(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "/Pane/Button[@Name='备注']";
            var button = element.FindFirstByXPath(xPath)?.AsButton();
            if (button.Patterns.Value.IsSupported)
            {
                var pattern = button.Patterns.Value.Pattern;
                options.GroupMemo = pattern.Value.Value;
                if (!string.IsNullOrWhiteSpace(options.GroupMemo))
                {
                    options.GroupMemo = options.GroupMemo == "群聊的备注仅自己可见" ? "" : options.GroupMemo;
                }
            }
        }
        //获取群聊公告
        private void _FetchGroupNotice(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "/Pane/Text[@Name='群公告']";
            var text = element.FindFirstByXPath(xPath)?.AsTextBox();
            if (text.Patterns.Value.IsSupported)
            {
                var pattern = text.Patterns.Value.Pattern;
                options.GroupNotice = pattern.Value.Value;
            }
        }

        //获取群聊名称
        private void _FetchGroupName(ChatGroupOptions options, Button button)
        {
            if (button != null)
            {
                if (button.Patterns.Value.IsSupported)
                {
                    var pattern = button.Patterns.Value.Pattern;
                    options.GroupName = pattern.Value.Value;
                }
                else
                {
                    var element = button.GetSibling(-1);
                    if (element != null && element.ControlType == ControlType.Text)
                    {
                        options.GroupName = element.AsLabel().Name;
                    }
                }
            }
        }

        private void _UpdateChatGroupOptions(ChatGroupOptions options)
        {
        }
        /// <summary>
        /// 获取群聊成员列表
        /// </summary>
        /// <returns>群聊成员列表</returns>
        public List<string> GetChatGroupMemberList()
        {
            return new List<string>();
        }
        private void _FocuseSearchText()
        {
            var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='搜索群成员']";
            _uiThreadInvoker.Run(automation =>
            {
                var edit = _SelfWindow.FindFirstByXPath(xPath)?.AsTextBox();
                if (edit != null)
                {
                    edit.Focus();
                    edit.Click();
                }
            }).Wait();
        }
        /// <summary>
        /// 是否打开侧边栏
        /// </summary>
        /// <returns>是否打开侧边栏</returns>
        private bool _IsSidebarOpen()
        {
            bool result = _uiThreadInvoker.Run(automation =>
            {
                var pane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd"))
                    .And(cf.ByName("SessionChatRoomDetailWnd")));
                return pane != null;
            }).Result;
            return result;
        }
        private void _OpenSidebar()
        {
            var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='聊天信息']";
            _uiThreadInvoker.Run(automation =>
            {
                var button = _SelfWindow.FindFirstByXPath(xPath)?.AsButton();
                if (button != null)
                {
                    button.WaitUntilClickable();
                    button.Focus();
                    button.Click();
                }
            }).Wait();

        }
        /// <summary>
        /// 搜索群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>成员元素</returns>
        private AutomationElement SearchChatGroupMember(string memberName)
        {
            return null;
        }
        /// <summary>
        /// 是否是群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>是否是群聊成员</returns>
        private bool IsChatGroupMember(string memberName)
        {
            return SearchChatGroupMember(memberName) != null;
        }
        /// <summary>
        /// 是否是自有群
        /// </summary>
        /// <returns>是否是自有群</returns>
        private bool IsOwnerChatGroup()
        {
            return false;
        }
        #endregion

        #region 自有群操作
        /// <summary>
        /// 更新群聊名称
        /// </summary>
        /// <param name="newGroupName">新群聊名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse UpdateOwnerChatGroupName(string groupName, string newGroupName)
        {
            return null;
        }
        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse CreateOwnerChatGroup(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }

        /// <summary>
        /// 删除群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse DeleteOwnerChatGroup(string groupName)
        {
            return null;
        }
        /// <summary>
        /// 发送群聊公告
        /// </summary>
        /// <param name="notice">公告内容</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse SendOwnerChatGroupNotice(string notice)
        {
            return null;
        }
        /// <summary>
        /// 添加群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddOwnerChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        /// <summary>
        /// 移除群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse RemoveOwnerChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }

        #endregion
        #region 他有群特定操作
        /// <summary>
        /// 邀请群聊成员,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse InviteChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        /// <summary>
        /// 添加群聊成员,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        #endregion
        #endregion

        /// <summary>
        /// 关闭子窗口
        /// </summary>
        public void Close()
        {
            _SelfWindow.Close();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            if (_ChatBodyCache != null)
            {
                _ChatBodyCache.Dispose();
            }
        }
    }
}
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;


namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public class WxClient
    {
        private Window _Window;
        private ToolBar _ToolBar;  // 工具栏
        private PopWinList _PopWinList;  // 弹出窗口列表
        private Navigation _Navigation;  // 导航栏
        private Search _Search;  // 搜索
        private ConversationList _Conversations;  // 会话列表
        private AddressBookList _AddressBook;  // 通讯录
        private ChatContent _WxChat;  // 聊天窗口
        public ToolBar ToolBar => _ToolBar;  // 工具栏
        public Navigation Navigation => _Navigation;  // 导航栏
        public ConversationList Conversations => _Conversations;  // 会话列表
        public AddressBookList AddressBook => _AddressBook;  // 通讯录
        public ChatContent WxChat => _WxChat;  // 聊天窗口
        public int ProcessId { get; private set; }
        public WxNotifyIcon WxNotifyIcon { get; private set; }
        public string NickName => _Navigation.NavigationButtons[0].Name;



        /// <summary>
        /// 微信客户端构造函数
        /// </summary>
        /// <param name="window">微信客户端窗口类</param>
        /// <param name="processId">微信客户端进程ID</param>
        /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
        /// <param name="notifyIconProcessId">微信客户端通知图标进程ID</param>
        public WxClient(Window window, int processId, Button wxNotifyIcon, int notifyIconProcessId)
        {
            _Window = window;
            ProcessId = processId;
            WxNotifyIcon = new WxNotifyIcon(wxNotifyIcon, notifyIconProcessId);
            _InitFramework();
            _VariableControlsInit();
        }

        /// <summary>
        /// 初始化固定控件
        /// </summary>
        private void _InitFramework()
        {
            _ToolBar = new ToolBar(_Window);  // 工具栏
            _Navigation = new Navigation(_Window);  // 导航栏
            _Search = new Search(_Window);  // 搜索
        }

        /// <summary>
        /// 初始化变量控件
        /// </summary>
        private void _VariableControlsInit()
        {
            _PopWinList = new PopWinList(_Window);
        }

        /// <summary>
        /// 点击通知图标
        /// </summary>
        public void ClickNotifyIcon()
        {
            WxNotifyIcon.Click();
        }

        #region 窗口操作

        /// <summary>
        /// 窗口置顶
        /// </summary>
        /// <param name="isTop">是否置顶</param>
        public void WindowTop(bool isTop = true)
        {
            ToolBar.Top(isTop);
        }
        /// <summary>
        /// 窗口最小化
        /// </summary>
        public void WindowMin()
        {
            ToolBar.Min();
        }

        /// <summary>
        /// 窗口最大化
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
    }
}
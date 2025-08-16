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
    public class WxWindow
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
        public string NickName => _Navigation.NavigationButtons[0].Name;

        public WxWindow(Window window)
        {
            _Window = window;
            _InitWxWindow();
            _VariableControlsInit();
        }
        /// <summary>
        /// 初始化固定控件
        /// </summary>
        private void _InitWxWindow()
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

        public void WindowTop(bool isTop = true)
        {
            ToolBar.Top(isTop);
        }

        public void WindowMin()
        {
            ToolBar.Min();
        }

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
    }
}
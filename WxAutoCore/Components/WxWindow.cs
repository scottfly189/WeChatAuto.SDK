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


namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端窗口,封装的微信窗口，包含工具栏、导航栏、搜索、会话列表、通讯录、聊天窗口等
    /// </summary>
    public class WxWindow : IChatContentAction
    {
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
        public ChatContent WxChat => _WxChatContent;  // 聊天窗口
        public SubWinList SubWinList => _SubWinList;  // 子窗口列表
        public int ProcessId { get; private set; }
        public string NickName => _Window.FindFirstByXPath($"/Pane/Pane/ToolBar[@Name='{WeChatConstant.WECHAT_NAVIGATION_NAVIGATION}'][@IsEnabled='true']").FindFirstChild().Name;
        public Window Window => _Window;

        /// <summary>
        /// 微信客户端窗口构造函数
        /// </summary>
        /// <param name="window"></param>
        public WxWindow(Window window, WxNotifyIcon notifyIcon)
        {
            _Window = window;
            ProcessId = window.Properties.ProcessId;
            _InitWxWindow(notifyIcon);
        }

        /// <summary>
        /// 初始化固定控件
        /// </summary>
        private void _InitWxWindow(WxNotifyIcon notifyIcon)
        {
            _ToolBar = new ToolBar(_Window, notifyIcon);  // 工具栏
            _Navigation = new Navigation(_Window);  // 导航栏
            _Search = new Search(this);  // 搜索
            _Conversations = new ConversationList(_Window, this);  // 会话列表
            _SubWinList = new SubWinList(_Window, this);
            _WxChatContent = new ChatContent(_Window, ChatContentType.Inline, "/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane");
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

        #region 好友查询操作
        /// <summary>
        /// 单个查询，查询单个好友
        /// 注意：此方法不会打开子聊天窗口
        /// </summary>
        /// <param name="who">好友名称</param>
        public void SearchWho(string who)
        {

        }
        /// <summary>
        /// 批量查询，查询多个好友
        /// 注意：此方法不会打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        public void SearchWhos(string[] whos)
        {
            whos.ToList().ForEach(who => SearchWho(who));
        }
        /// <summary>
        /// 单个查询，查询单个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="who">好友名称</param>
        public void SearchWhoAndOpenChat(string who)
        {
            SearchWho(who);

        }
        /// <summary>
        /// 批量查询，查询多个好友，并打开子聊天窗口
        /// </summary>
        /// <param name="whos">好友名称列表</param>
        public void SearchWhosAndOpenChat(string[] whos)
        {
            whos.ToList().ForEach(who => SearchWhoAndOpenChat(who));
        }
        #endregion
    }
}
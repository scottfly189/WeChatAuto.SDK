using System;
using Microsoft.Extensions.Logging;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 记录一些异变化的UI Tree路径.
    /// </summary>
    public static class UITreeGlobal
    {
        //消息列表的根listbox - 主窗口
        public static string MessageRootPath = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Group/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Group/Group/Custom/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView']";
        //弹出窗口 - 消息列表的根Listbox
        public static string SubWinMessageRootPath = "/Group/Group/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView']";
        //消息列表弹出的复制菜单xpath
        public static string MessagePopupMenu_Copy = "/Window[@Name='Weixin']/MenuItem[@Name='复制'][@ClassName='mmui::XMenuView'] | /Window[@Name='Weixin']/MenuItem[@Name='复制'][@ClassName='mmui::XMenu']";
        //消息列表弹出的下载菜单xpath
        public static string MessagePopupMenu_Download = "/Window[@Name='Weixin']/MenuItem[@Name='下载'][@ClassName='mmui::XMenuView'] | /Window[@Name='Weixin']/MenuItem[@Name='下载'][@ClassName='mmui::XMenu']";
        //主窗口消息列表的 聊天记录 按钮
        public static string MessageChatHistory = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='聊天记录'][@ClassName='mmui::XButton']";
        //搜索
        public static string Search_Bar_Edit = "/Group/Custom/Group/Group/Group/Custom/Custom/Group/Group/Group/Group/Custom/Group/Edit[@Name='搜索'] | /Group/Custom/Group/Group/Group/Custom/Custom/Group/Group/Group/Group/Group/Edit[@Name='搜索']";
        public static string Search_Bar_PopupMenu = "/Window[@Name='Weixin']/Group/Group/List[@AutomationId='search_list'] | /Window[@Name='Weixin']/Group/List[@AutomationId='search_list']";
        //会话列表 - ListBox
        public static string ConversationRootPath = @"/Group/Custom/Group/Group/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/List[@Name='会话'][@AutomationId='session_list']";

        //弹出窗口 - 标题root的path
        public static string SubWinTitleRootPath = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group[@AutomationId='content_view.top_content_view.title_h_view.left_v_view.left_content_v_view.left_ui_'][@ClassName='mmui::XView']";
        //弹出窗口 - 标题root下面的text组Path
        public static string SubWinTitleTextGroupPath = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Text[@AutomationId='content_view.top_content_view.title_h_view.left_v_view.left_content_v_view.left_ui_.big_title_line_h_view'][@ClassName='mmui::XHBoxView']";
        //弹出窗口 - 标题栏 的 聊天记录 按钮 path
        public static string SubWinTitleHistoryButtonPath = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='聊天记录']";
        //弹出窗口 - 标题栏 的 语音通话 按钮 path
        public static string SubWinTitleVoipButtonPath = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='语音通话']";
        /// <summary>
        /// 导航栏 - 首个按钮 - 微信按钮.
        /// </summary>
        public static string Navigate_wechat = "/Group/Custom/Group/ToolBar/Button[@Name='微信'][@ClassName='mmui::XTabBarItem']";

        
    }
}
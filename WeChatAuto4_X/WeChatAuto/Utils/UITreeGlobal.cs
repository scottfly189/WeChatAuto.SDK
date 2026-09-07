using System;
using Microsoft.Extensions.Logging;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 记录一些异变化的UI Tree路径.
    /// </summary>
    public static class UITreeGlobal
    {
        public static string MessageRootPath = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Group/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Group/Group/Custom/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView']";
    }
}
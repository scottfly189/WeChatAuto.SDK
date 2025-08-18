using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using System.Linq;
using WxAutoCommon.Utils;
using FlaUI.Core.Definitions;
using WxAutoCommon.Models;
using WxAutoCommon.Enums;
using System;
using FlaUI.Core.Tools;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 会话列表
    /// </summary>
    public class ConversationList
    {
        private Window _Window;
        private List<string> _TitleTypeList = new List<string> { WeChatConstant.WECHAT_CONVERSATION_WX_TEAM,
            WeChatConstant.WECHAT_CONVERSATION_SERVICE_NOTICE,
            WeChatConstant.WECHAT_CONVERSATION_WX_PAY,
            WeChatConstant.WECHAT_CONVERSATION_TX_NEWS,
            WeChatConstant.WECHAT_CONVERSATION_SUBSCRIPTION,
            WeChatConstant.WECHAT_CONVERSATION_FILE_TRANSFER,
            WeChatConstant.WECHAT_CONVERSATION_COLLAPSED_GROUP
        };
        private readonly string _titleSuffix = WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP;
        private List<ListBoxItem> _Conversations = new List<ListBoxItem>();
        public ConversationList(Window window)
        {
            _Window = window;
        }
        /// <summary>
        /// 获取会话列表可见会话
        /// 会话信息包含：会话名称、会话类型、会话状态、会话时间、会话未读消息数、会话头像<see cref="Conversation"/>
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetVisibleConversations()
        {
            var items = _GetVisibleConversatItem();
            List<Conversation> conversations = new List<Conversation>();
            foreach (var item in items)
            {
                Conversation conversation = new Conversation();
                conversation.ConversationTitle = item.Name.EndsWith(_titleSuffix) ? item.Name.Substring(0, item.Name.Length - _titleSuffix.Length) : item.Name;
                conversation.ConversationType = _GetConversationType(conversation.ConversationTitle);
                conversation.ConversationContent = _GetConversationContent(item);
                conversation.IsCompanyGroup = _IsCompanyGroup(item);
                conversation.ImageButton = _GetConversationImageButton(item);
                conversation.HasNotRead = _GetConversationHasNotRead(item);
                conversation.Time = _GetConversationTime(item);
                conversation.IsDoNotDisturb = _IsDoNotDisturb(item);
                conversations.Add(conversation);
            }
            return conversations;
        }
        /// <summary>
        /// 获取会话列表根节点
        /// </summary>
        /// <returns></returns>
        private ListBox _GetConversationRoot()
        {
            string xPath = $"/Pane/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_SESSION_BOX_CONVERSATION}'][@IsOffscreen='false']";
            var root = _Window.FindFirstByXPath(xPath).AsListBox();
            return root;
        }
        /// <summary>
        /// 获取会话列表可见项
        /// </summary>
        /// <returns></returns>
        private List<ListBoxItem> _GetVisibleConversatItem()
        {
            var root = _GetConversationRoot();
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            return items.Select(item => item.AsListBoxItem()).ToList();
        }
        /// <summary>
        /// 获取会话类型
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private ConversationType _GetConversationType(string title)
        {
            if (!_TitleTypeList.Any(t => title.Contains(t)))
            {
                return ConversationType.好友或群聊;
            }
            else
            {
                return Enum.TryParse<ConversationType>(title, true, out ConversationType conversationType) ? conversationType : ConversationType.好友或群聊;
            }
        }
        /// <summary>
        /// 是否是企业群
        /// </summary>
        /// <param name="item"></param>
        /// <param name="conversation"></param>
        /// <returns></returns>
        private bool _IsCompanyGroup(ListBoxItem item)
        {
            var xPath = "/Pane/Pane/Pane[1]/Pane[2]";
            var retryElement = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
            return retryElement.Success;
        }
        /// <summary>
        /// 获取会话内容
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string _GetConversationContent(ListBoxItem item)
        {
            var xPath = "/Pane/Pane[1]/Pane[2]/Text";
            var retryElement = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
            if (retryElement.Success)
            {
                var lable = retryElement.Result.AsLabel();
                return lable.Name;
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取会话头像按钮
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Button _GetConversationImageButton(ListBoxItem item)
        {
            return item.FindFirstByXPath("/Pane/Button").AsButton();
        }
        /// <summary>
        /// 获取会话是否有未读消息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool _GetConversationHasNotRead(ListBoxItem item)
        {
            var xPath = "//Pane/Pane[2]";
            var retryElement = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
            return retryElement.Success;
        }
        /// <summary>
        /// 获取会话时间
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string _GetConversationTime(ListBoxItem item)
        {
            var xPath = "/Pane/Pane[1]/Pane[1]/Text";
            var retryElement = Retry.WhileNull(() => item.FindAllByXPath(xPath));
            if (retryElement.Success)
            {
                AutomationElement[] elements = retryElement.Result;
                return elements[elements.Length - 1].Name;
            }
            return string.Empty;
        }
        /// <summary>
        /// 是否是免打扰
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool _IsDoNotDisturb(ListBoxItem item)
        {
            var xPath = "/Pane/Pane/Pane[2]/Pane";
            var retryElement = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
            return retryElement.Success;
        }
    }
}
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using System.Linq;
using WxAutoCommon.Utils;
using FlaUI.Core.Definitions;
using WxAutoCommon.Models;
using WxAutoCommon.Enums;
using System;
using FlaUI.Core.Tools;
using FlaUI.Core.Conditions;
using System.Text.RegularExpressions;
using WxAutoCore.Utils;
using WxAutoCore.Extentions;
using System.Threading;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 会话列表
    /// </summary>
    public class ConversationList
    {
        private UIThreadInvoker _uiThreadInvoker;
        private Window _Window;
        private WeChatMainWindow _WxWindow;
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
        public ConversationList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
            _Window = window;
            _WxWindow = wxWindow;
        }
        /// <summary>
        /// 获取会话列表可见会话
        /// 会话信息包含：会话名称、会话类型、会话状态、会话时间、会话未读消息数、会话头像<see cref="Conversation"/>
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetVisibleConversations()
        {
            var items = _GetVisibleConversatItems();
            List<Conversation> conversations = new List<Conversation>();
            foreach (var item in items)
            {
                Conversation conversation = new Conversation();
                conversation.ConversationTitle = _GetConversationTitle(item, conversation);
                conversation.IsTop = _GetConversationIsTop(item);
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
        /// 获取会话列表所有会话
        /// </summary>
        /// <returns></returns>
        public List<Conversation> GetAllConversations()
        {
            var items = _GetAllConversatItems();
            List<Conversation> conversations = new List<Conversation>();
            foreach (var item in items)
            {
                Conversation conversation = new Conversation();
                conversation.ConversationTitle = _GetConversationTitle(item, conversation);
                conversation.IsTop = _GetConversationIsTop(item);
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
        /// 点击会话
        /// </summary>
        /// <param name="title">会话标题</param>
        public void ClickConversation(string title)
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation => root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).Result;
            var item = items.FirstOrDefault(c => c.Name.Contains(title));
            if (item != null)
            {
                DoConversionClick(item);
            }
        }

        private void DoConversionClick(AutomationElement item)
        {
            var xPath = "/Pane/Button";
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
            if (retryElement.Success)
            {
                var button = retryElement.Result.AsButton();
                DrawHightlightHelper.DrawHightlight(button, _uiThreadInvoker);
                _WxWindow.SilenceClickExt(button);
            }
        }
        /// <summary>
        /// 点击第一个会话
        /// </summary>
        public void ClickFirstConversation()
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation => root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).Result;
            var item = items.FirstOrDefault();
            if (item != null)
            {
                DoConversionClick(item);
            }
        }
        /// <summary>
        /// 双击会话
        /// </summary>
        /// <param name="title">会话标题</param>
        public void DoubleClickConversation(string title)
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation => root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).Result;
            var item = items.FirstOrDefault(c => c.Name.Contains(title));
            if (item != null)
            {
                var xPath = "/Pane/Button";
                var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
                if (retryElement.Success)
                {
                    var button = retryElement.Result.AsButton();
                    DrawHightlightHelper.DrawHightlight(button, _uiThreadInvoker);
                    button.DoubleClick();
                }
            }
        }
        /// <summary>
        /// 获取会话列表可见会话标题
        /// </summary>
        /// <returns></returns>
        public List<string> GetVisibleConversationTitles()
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation => root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).Result;
            return items.Select(item => item.Name.Replace(WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP, "")).ToList();
        }

        /// <summary>
        /// 获取会话标题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string _GetConversationTitle(ListBoxItem item, Conversation conversation)
        {
            var xPath = "/Pane/Button";
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
            if (retryElement.Success)
            {
                var button = retryElement.Result.AsButton();
                var title = DoAnotherLogic(button.Name);
                return title;
            }
            return string.Empty;
        }

        private bool _GetConversationIsTop(ListBoxItem item) => item.Name.Contains(_titleSuffix);

        private string DoAnotherLogic(string title)
        {
            var result = title.Replace("SessionListItem", "订阅号");
            return result;
        }
        /// <summary>
        /// 获取会话列表根节点
        /// </summary>
        /// <returns></returns>
        public ListBox GetConversationRoot()
        {
            return _uiThreadInvoker.Run(automation =>
            {
                string xPath = $"/Pane/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_SESSION_BOX_CONVERSATION}'][@IsOffscreen='false']";
                var root = _Window.FindFirstByXPath(xPath).AsListBox();
                return root;
            }).Result;
        }
        /// <summary>
        /// 获取会话列表可见项
        /// </summary>
        /// <returns></returns>
        private List<ListBoxItem> _GetVisibleConversatItems()
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation => root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))).Result;
            return items.Select(item => item.AsListBoxItem()).ToList();
        }
        private List<ListBoxItem> _GetAllConversatItems()
        {
            var listBox = GetConversationRoot();

            List<ListBoxItem> list = _uiThreadInvoker.Run(automation =>
            {
                var subList = new List<ListBoxItem>();
                var scrollPattern = listBox.Patterns.Scroll.Pattern;
                if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                {
                    scrollPattern.SetScrollPercent(0, 0);
                    // while (scrollPattern.VerticalScrollPercent < 1)
                    // {
                    //     var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                    //     foreach (var item in items)
                    //     {
                    //         if (!subList.Any(u => u.Name == item.Name))
                    //         {
                    //             subList.Add(item.AsListBoxItem());
                    //         }
                    //     }
                    //     scrollPattern.Scroll(ScrollAmount.NoAmount, ScrollAmount.LargeIncrement);
                    //     Thread.Sleep(600); // 给 UI 反应时间
                    // }
                    // return subList;
                    for (double p = 0; p <= 1; p += 0.015)
                    {
                        scrollPattern.SetScrollPercent(0, p);
                        Thread.Sleep(600);
                        var i = scrollPattern.VerticalScrollPercent;

                        var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        foreach (var item in items)
                        {
                            var index = subList.FindIndex(u => u.Name == item.Name);
                            if (index < 0)
                            {
                                subList.Add(item.AsListBoxItem());
                            }
                        }
                    }
                    return subList;
                }
                else
                {
                    var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                    subList.AddRange(items.Select(item => item.AsListBoxItem()).ToList());
                    return subList;
                }
            }).Result;

            return list;
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
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
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
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
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
            return _uiThreadInvoker.Run(automation => item.FindFirstByXPath("/Pane/Button").AsButton()).Result;
        }
        /// <summary>
        /// 获取会话是否有未读消息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool _GetConversationHasNotRead(ListBoxItem item)
        {
            var xPath = "/Pane/Pane[2] | /Pane/Text";
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindFirstByXPath(xPath))).Result;
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
            var retryElement = _uiThreadInvoker.Run(automation => Retry.WhileNull(() => item.FindAllByXPath(xPath))).Result;
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
            return _uiThreadInvoker.Run(automation =>
            {
                var xPath = "/Pane/Pane/Pane[2]/Pane";
                var retryElement = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
                return retryElement.Success;
            }).Result;
        }
    }
}
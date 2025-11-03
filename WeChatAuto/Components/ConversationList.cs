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
using WeChatAuto.Utils;
using WeChatAuto.Extentions;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using WeAutoCommon.Utils;
using WxAutoCommon.Simulator;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 会话列表
    /// </summary>
    public class ConversationList
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<ConversationList> _logger;
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
        public ConversationList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<ConversationList>>();
            _uiThreadInvoker = uiThreadInvoker;
            _Window = window;
            _WxWindow = wxWindow;
            _serviceProvider = serviceProvider;
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
        /// 获取会话列表所有会话的名称
        /// 考虑到效率，只返回名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllConversations()
        {
            var items = _GetAllConversatItems();
            return items;
        }
        /// <summary>
        /// 定位会话
        /// 定位会话的用途：可以将会话列表滚动到指定会话的位置，使指定会话可见
        /// </summary>
        /// <param name="title">会话标题</param>
        /// <returns>如果找到会话，则返回true，否则返回false</returns>
        public bool LocateConversation(string title) => _LocateConversation(title);

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
                DoConversionClick(item, root);
            }
            else
            {
                _logger.Trace($"未找到会话：{title}");
            }
        }

        private void DoConversionClick(AutomationElement item, AutomationElement root)
        {
            var xPath = "/Pane/Button";
            var buttonElement = _uiThreadInvoker.Run(automation =>
            {
                var buttonResult = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
                var button = buttonResult.Result;
                if (button != null)
                {
                    //确保按钮可见
                    while (button.Properties.IsOffscreen.Value)
                    {
                        var scrollPattern = root.Patterns.Scroll.Pattern;
                        if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                        {
                            double currentPercent = scrollPattern.VerticalScrollPercent;
                            double newPercent = Math.Min(currentPercent + scrollPattern.VerticalViewSize, 1);
                            scrollPattern.SetScrollPercent(0, newPercent);
                            Thread.Sleep(600);
                            buttonResult = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
                            button = buttonResult.Result;
                        }
                        else
                        {
                            _logger.Trace($"会话列表不可滚动，无法定位会话按钮元素");
                            break;
                        }
                    }
                }
                else
                {
                    _logger.Trace($"未找到会话按钮元素");
                }
                return button;
            }).Result;
            if (buttonElement != null)
            {
                var button = buttonElement.AsButton();
                DrawHightlightHelper.DrawHightlight(button, _uiThreadInvoker);
                if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                {
                    var point = DpiHelper.GetDpiAwarePoint(_Window, button);
                    ClickHighlighter.ShowClick(point);
                    KMSimulatorService.LeftClick(point);
                }
                else
                {
                    _WxWindow.SilenceClickExt(button);
                }
            }
            else
            {
                _logger.Trace($"未找到会话按钮元素");
            }
        }
        /// <summary>
        /// 点击第一个会话
        /// </summary>
        public void ClickFirstConversation()
        {
            var root = GetConversationRoot();
            var items = _uiThreadInvoker.Run(automation =>
            {
                return root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
            }).Result;
            var item = items.FirstOrDefault();
            if (item != null)
            {
                DoConversionClick(item, root);
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
                    _uiThreadInvoker.Run(automation =>
                    {
                        //使按钮可见
                        while (button.Properties.IsOffscreen.Value)
                        {
                            var scrollPattern = root.Patterns.Scroll.Pattern;
                            if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                            {
                                double currentPercent = scrollPattern.VerticalScrollPercent;
                                double newPercent = Math.Min(currentPercent + scrollPattern.VerticalViewSize, 1);
                                scrollPattern.SetScrollPercent(0, newPercent);
                                Thread.Sleep(600);
                                var retryElementInner = Retry.WhileNull(() => item.FindFirstByXPath(xPath));
                                if (retryElementInner.Success)
                                {
                                    button = retryElementInner.Result.AsButton();
                                }
                                else
                                {
                                    _logger.Trace($"未找到会话按钮元素");
                                    break;
                                }
                            }
                            else
                            {
                                _logger.Trace($"会话列表不可滚动，无法定位会话按钮元素");
                                break;
                            }
                        }
                        DrawHightlightHelper.DrawHighlightExt(button);
                        if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                        {
                            var point = DpiHelper.GetDpiAwarePoint(_Window, button);
                            ClickHighlighter.ShowClick(point);
                            KMSimulatorService.LeftDoubleClick(point);
                            Thread.Sleep(300);
                            return;
                        }
                        _Window.SetForeground();
                        button.DoubleClick();
                    }).Wait();
                }
                else
                {
                    _logger.Trace($"未找到会话按钮元素");
                }
            }
            else
            {
                _logger.Trace($"未找到会话：{title}");
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
                root.Focus();
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
        //获取所有会话列表核心方法
        private List<string> _GetAllConversatItems()
        {
            var listBox = GetConversationRoot();
            List<string> list = _uiThreadInvoker.Run(automation =>
            {
                var subList = new List<string>();
                var scrollPattern = listBox.Patterns.Scroll.Pattern;
                if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                {
                    for (double p = 0; p <= 1; p += scrollPattern.VerticalViewSize)
                    {
                        scrollPattern.SetScrollPercent(0, p);
                        Thread.Sleep(600);

                        var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        foreach (var item in items)
                        {
                            if (!subList.Contains(item.Name))
                            {
                                subList.Add(item.Name.Replace(WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP, ""));  //去除置顶标记
                            }
                        }
                    }
                    return subList;
                }
                else
                {
                    var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                    subList.AddRange(items.Select(item => item.Name.Replace(WeChatConstant.WECHAT_SESSION_BOX_HAS_TOP, "")).ToList()); //去除置顶标记
                    return subList;
                }
            }).Result;

            return list;
        }
        private bool _LocateConversation(string title)
        {
            var listBox = GetConversationRoot();
            listBox.Focus();
            bool result = _uiThreadInvoker.Run(automation =>
            {
                var existTag = false;
                var scrollPattern = listBox.Patterns.Scroll.Pattern;
                if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                {
                    for (double p = 0; p <= 1; p += scrollPattern.VerticalViewSize)
                    {
                        scrollPattern.SetScrollPercent(0, p);
                        Thread.Sleep(600);
                        var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        var item = items.FirstOrDefault(c => c.Name.Contains(title));
                        if (item != null)
                        {
                            existTag = true;
                            break;
                        }
                    }
                }
                else
                {
                    var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                    var item = items.FirstOrDefault(c => c.Name.Contains(title));
                    if (item != null)
                    {
                        existTag = true;
                    }
                }
                return existTag;
            }).Result;

            return result;
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
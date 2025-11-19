using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using System.Text.RegularExpressions;
using WxAutoCommon.Interface;
using WeChatAuto.Extentions;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using WeChatAuto.Utils;
using WeChatAuto.Models;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Patterns;
using System.Drawing;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区气泡列表
    /// </summary>
    public class MessageBubbleList
    {
        private Window _SelfWindow;
        private IServiceProvider _serviceProvider;
        private IWeChatWindow _WxWindow;
        private AutoLogger<MessageBubbleList> _logger;
        private string _Title;
        private AutomationElement _BubbleListRoot;
        private UIThreadInvoker _uiThreadInvoker;
        public List<MessageBubble> Bubbles => GetVisibleBubbles();
        public List<ChatSimpleMessage> ChatSimpleMessages => GetVisibleChatSimpleMessages();
        public ListBox BubbleListRoot => _BubbleListRoot.AsListBox();
        private ChatBody _ChatBody;
        public MessageBubbleList(Window selfWindow, AutomationElement bubbleListRoot, IWeChatWindow wxWindow, string title, UIThreadInvoker uiThreadInvoker, ChatBody chatBody, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<AutoLogger<MessageBubbleList>>();
            _SelfWindow = selfWindow;
            _BubbleListRoot = bubbleListRoot;
            _WxWindow = wxWindow;
            _Title = title;
            _uiThreadInvoker = uiThreadInvoker;
            _ChatBody = chatBody;
        }

        /// <summary>
        /// 获取聊天类型
        /// </summary>
        /// <returns>聊天类型<see cref="ChatType"/></returns>
        public ChatType GetChatType()
        {
            if (Regex.IsMatch(_Title, @"\s\([\d]+\)$"))
            {
                return ChatType.群聊;
            }
            else
            {
                return ChatType.好友;
            }
        }
        /// <summary>
        /// 获取最后一个气泡
        /// </summary>
        /// <returns>最后一个气泡</returns>
        public MessageBubble GetLastBubble()
        {
            MessageBubble[] bubbles = GetVisibleBubbles().ToArray();
            return bubbles.Count() > 0 ? bubbles.Last() : null;
        }

        /// <summary>
        /// 获取所有气泡标题列表
        /// 注意：可能速度比较慢,但是信息比较全
        /// </summary>
        /// <returns>所有气泡标题列表<see cref="ChatSimpleMessage"/></returns>
        public List<ChatSimpleMessage> GetAllChatHistory()
          => _ChatBody.GetAllChatHistory();


        /// <summary>
        /// 加载更多
        /// </summary>
        public void LoadMore()
        {
            var lookMoreButton = _uiThreadInvoker.Run(automation => _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)))).GetAwaiter().GetResult();
            if (lookMoreButton != null)
            {
                _uiThreadInvoker.Run(automation =>
                {
                    var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                    if (pattern != null)
                    {
                        pattern.SetScrollPercent(0, 0);
                    }
                    RandomWait.Wait(300, 1000);
                    lookMoreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    lookMoreButton.Click();
                    RandomWait.Wait(100, 1000);
                }).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// 是否有加载更多按钮
        /// </summary>
        /// <returns>是否有加载更多按钮</returns>
        public bool IsLoadingMore()
        {
            var lookMoreButton = _uiThreadInvoker.Run(automation => _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            return lookMoreButton != null;
        }

        /// <summary>
        /// 获取气泡列表,不包括系统消息
        /// 注意：可能速度比较慢,但是信息比较全
        /// </summary>
        public List<MessageBubble> GetVisibleBubbles()
        {
            var bubbles = GetVisibleNativeBubbles();
            return bubbles.Where(item => item.MessageSource != MessageSourceType.系统消息 &&
                                       item.MessageSource != MessageSourceType.其他消息).ToList();
        }
        /// <summary>
        /// 获取可见气泡列表,仅返回气泡标题
        /// </summary>
        /// <returns>可见气泡列表,仅返回气泡标题</returns>
        public List<ChatSimpleMessage> GetVisibleChatSimpleMessages()
        {
            var bubbles = GetVisibleBubbles();
            return bubbles.Select(item => item.ToChatSimpleMessage()).ToList();
        }
        /// <summary>
        /// 获取气泡列表,包括系统消息
        /// 注意：速度比较慢，但是信息比较全
        /// </summary>
        /// <returns>气泡列表<see cref="MessageBubble"/></returns>
        public List<MessageBubble> GetVisibleNativeBubbles()
        {
            var listItemList = _uiThreadInvoker.Run(automation => _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).GetAwaiter().GetResult();
            List<MessageBubble> bubbles = new List<MessageBubble>();
            DateTime? dateTime = null;
            for (int i = 0; i < listItemList.Count; i++)
            {
                var bubble = ParseBubble(listItemList[i], ref dateTime);
                if (bubble != null)
                {
                    if (bubble is MessageBubble)
                    {
                        bubbles.Add(bubble as MessageBubble);
                    }
                    else
                    {
                        bubbles.AddRange(bubble as List<MessageBubble>);
                    }
                }
            }
            return bubbles;
        }
        /// <summary>
        /// 转发单条消息
        /// 流程：
        /// 1. 找到这一条消息,倒序找，这里注意一点，如果找不到消息，往前翻三页找不到，则不会转发此消息,日志显示错误，但不会报错.
        /// 2. 右键点击这一条消息
        /// 3. 找到菜单
        /// 4. 找到发送人
        /// </summary>
        /// <param name="to">要转发给谁</param>
        /// <param name="chatSimpleMessage">要转发的消息<see cref="ChatSimpleMessage"/></param>
        public void ForwardSingleMessage(ChatSimpleMessage chatSimpleMessage, string to)
        {
            _uiThreadInvoker.Run(automation =>
            {
                var listItem = _LocateSingleMessage(chatSimpleMessage);
            })
            .GetAwaiter().GetResult();
        }

        private List<AutomationElement> _GetListItemList() => _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();

        private ListBoxItem _LocateSingleMessage(ChatSimpleMessage chatSimpleMessage)
        {
            int index = 0; //向前翻页的索引
            ListBoxItem result = null;
            IScrollPattern pattern = null;
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            while (index < 3)
            {
                var listItems = _GetListItemList();
                listItems.Reverse();
                var items = listItems.Where(item => item.Name.Contains(chatSimpleMessage.Message)).ToList();
                foreach (var item in items)
                {
                    var subItems = item.FindAllByXPath("/Pane[1]/*");
                    if (subItems != null && subItems.Length == 3)
                    {
                        ListBoxItem subItem = _SameMessageAndMove_(item.AsListBoxItem(), chatSimpleMessage);
                        if (subItem != null)
                        {
                            result = subItem;
                            break;
                        }
                    }
                }

                //往上翻页
                var (flowControl, nextIndex) = _PrevPageSearch(chatSimpleMessage, index);
                if (!flowControl)
                {
                    break;
                }
                index = nextIndex;
            }
            return result;
        }

        private ListBoxItem _SameMessageAndMove_(ListBoxItem item, ChatSimpleMessage chatSimpleMessage)
        {
            var subItems = item.FindAllByXPath("/Pane[1]/*");
            var button = item.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
                return null;
            var who = button.Name;
            if (_ChatBody.ChatType == ChatType.群聊)
            {
                if (subItems[0].ControlType == ControlType.Button)
                {
                    var pane = subItems[0].GetSibling(1);
                    if (pane != null && pane.ControlType == ControlType.Pane)
                    {
                        who = pane.FindFirstByXPath(@"//Text")?.Name;
                    }
                }
            }
            if (item.Name.Contains(chatSimpleMessage.Message) && who == chatSimpleMessage.Who)
            {
                //检查item的可见性
                _BubbleListRoot = _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("消息")));
                if (_BubbleListRoot == null)
                    throw new Exception("消息列表根节点获取失败");
                var baseRect = item.BoundingRectangle;
                var listItems = _GetListItemList();
                listItems.Reverse();
                var foundItem = listItems.FirstOrDefault(u => u.Name == item.Name)?.AsListBoxItem();

                while (foundItem != null && foundItem.BoundingRectangle.Top < baseRect.Top)
                {
                    if (_BubbleListRoot.Patterns.Scroll.IsSupported)
                    {
                        var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                        if (pattern != null)
                        {
                            pattern.SetScrollPercent(0, System.Math.Max(pattern.VerticalScrollPercent - pattern.VerticalViewSize, 0));
                            listItems = _GetListItemList();
                            listItems.Reverse();
                            foundItem = listItems.FirstOrDefault(u => u.Name == item.Name)?.AsListBoxItem();
                        }
                    }
                    else
                    {
                        _logger.Error("消息列表不可滚动，无法定位消息");
                        break;
                    }
                }
                foundItem.DrawHighlightExt();

                return item;
            }
            return null;
        }

        /// <summary>
        /// 往上翻页查找消息
        /// </summary>
        /// <param name="chatSimpleMessage">要查找的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="index">当前索引</param>
        /// <returns>是否找到消息，是否继续翻页</returns>
        private (bool flowControl, int nextIndex) _PrevPageSearch(ChatSimpleMessage chatSimpleMessage, int index)
        {
            //往上翻页
            index++;
            var loadMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)))?.AsButton();
            if (loadMoreButton != null)
            {
                loadMoreButton.Click();
                RandomWait.Wait(100, 1000);
                return (flowControl: true, nextIndex: index);
            }
            else
            {
                _logger.Error($"不存在who={chatSimpleMessage.Who},message={chatSimpleMessage.Message}的消息");
                return (flowControl: false, nextIndex: index);
            }
        }

        /// <summary>
        /// 转发单条消息
        /// </summary>
        /// <param name="who">要转发的好友昵称</param>
        /// <param name="message">要转发的消息内容</param>
        /// <param name="to">要转发给谁</param>
        public void ForwardSingleMessage(string who, string message, string to)
          => ForwardSingleMessage(new ChatSimpleMessage { Who = who, Message = message }, to);
        /// <summary>
        /// 解析气泡为Bubble对象,Bubble对象可能为空
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns>Bubble对象,可能为空,也可能为List<Bubble>对象;<see cref="MessageBubble"/></returns>
        private Object ParseBubble(AutomationElement listItem, ref DateTime? dateTime)
        {
            var listItemChildren = _uiThreadInvoker.Run(automation => listItem.FindAllChildren()).GetAwaiter().GetResult();
            if (listItemChildren.Count() == 0)
            {
                return null;
            }

            if (listItemChildren.Count() == 1)
            {

                if (string.IsNullOrEmpty(listItemChildren[0].Name))
                {
                    //非时间消息
                    return _ParseMessage(listItem, dateTime);
                }
                else
                {
                    //系统消息，并且是时间消息
                    return _ParseTimeMessage(listItemChildren[0], ref dateTime);
                }

            }

            throw new Exception("气泡解析失败");
        }

        #region 解释消息各类私有方法
        /// <summary>
        /// 解析除消息以外的气泡
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns>Bubble对象,可能为空;<see cref="MessageBubble"/></returns>
        private Object _ParseMessage(AutomationElement listItem, DateTime? dateTime)
        {
            if (listItem.Name.Trim() == WeChatConstant.MESSAGES_PICK_UP)
            {
                return _ParsePickUp(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_IMAGE)
            {
                return _ParseImage(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LOCATION)
            {
                return _ParseLocation(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CHAT_RECORD)
            {
                return _ParseChatRecord(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CARD)
            {
                return _ParseCard(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_FILE)
            {
                return _ParseFile(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO)
            {
                return _ParseVideo(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER)
            {
                return _ParseVideoNumber(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER_LIVE)
            {
                return _ParseVideoNumberLive(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LINK)
            {
                return _ParseLink(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_NOTE)
            {
                return _ParseNote(listItem, dateTime);
            }
            if (listItem.Name.Contains(WeChatConstant.MESSAGES_VOICE))
            {
                return _ParseVoice(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_RED_PACKET_SEND || listItem.Name == WeChatConstant.MESSAGES_RED_PACKET_RECEIVE)
            {
                return _ParseRedPacket(listItem, dateTime);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_WECHAT_TRANSFER ||
                listItem.Name == "[转账待你接收，可在手机上查看]" ||
                listItem.Name == "[你发起了一笔转账]" ||
                listItem.Name == "[已收款]"
                )
            {
                return _ParseWeChatTransfer(listItem, dateTime);
            }
            if (Regex.IsMatch(listItem.Name, @"^(\[[^\]]+\])+$"))
            {
                return _ParseExpressionMessage(listItem, dateTime);
            }
            return _ParseTextMessage(listItem, dateTime);
        }

        /// <summary>
        /// 解析表情消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseExpressionMessage(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleExpressionMessage(listItem, dateTime);
            }
            else
            {
                return _ParseGroupExpressionMessage(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleExpressionMessage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("表情消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            if (children.Count() != 3)
            {
                throw new Exception("表情消息解析失败");
            }
            MessageBubble bubble = new MessageBubble();
            bubble.MessageType = MessageType.表情;
            bubble.MessageContent = listItem.Name;
            bubble.MessageTime = dateTime;
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            return bubble;
        }
        private MessageBubble _ParseGroupExpressionMessage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("表情消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            if (children.Count() != 3)
            {
                throw new Exception("表情消息解析失败");
            }
            MessageBubble bubble = new MessageBubble();
            bubble.MessageType = MessageType.表情;
            bubble.MessageContent = listItem.Name;
            bubble.MessageTime = dateTime;
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析文本消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseTextMessage(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleTextMessage(listItem, dateTime);
            }
            else
            {
                return _ParseGroupTextMessage(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleTextMessage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("文本消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            if (children.Count() != 3)
            {
                bubble.MessageType = MessageType.文字;
                bubble.MessageSource = MessageSourceType.系统消息;
                bubble.Who = MessageSourceType.系统消息.ToString();
                bubble.MessageContent = listItem.Name;
                return bubble;
            }
            bubble.MessageType = MessageType.文字;
            bubble.MessageContent = listItem.Name.Trim();
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                if (children[2].ControlType == ControlType.Button)
                {
                    bubble.Who = "我";
                    bubble.MessageSource = MessageSourceType.自己发送消息;
                }
                else
                {
                    _ProcessAnotherMessage(listItem, children, bubble);
                }
            }

            if (_ParseMiniProgram(children[1]))
            {
                bubble.MessageType = MessageType.小程序;
                var miniButton = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
                if (miniButton != null)
                {
                    bubble.ClickActionButton = miniButton.AsButton();
                }
                return bubble;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button != null)
            {
                bubble.ClickActionButton = button.AsButton();
            }

            //处理、解析引用消息
            _ParseReferencedMessage(children[1], listItem.Name, bubble);
            return bubble;
        }
        private MessageBubble _ParseGroupTextMessage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("文本消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            if (children.Count() != 3)
            {
                bubble.MessageType = MessageType.文字;
                bubble.MessageSource = MessageSourceType.系统消息;
                bubble.Who = MessageSourceType.系统消息.ToString();
                bubble.MessageContent = listItem.Name;
                return bubble;
            }
            bubble.MessageType = MessageType.文字;
            bubble.MessageContent = listItem.Name.Trim();
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
                bubble.GroupNickName = __GetGroupNickName(children);
            }
            else
            {
                if (children[2].ControlType == ControlType.Button)
                {
                    bubble.Who = "我";
                    bubble.MessageSource = MessageSourceType.自己发送消息;
                    bubble.GroupNickName = bubble.Who;
                }
                else
                {
                    _ProcessAnotherMessage(listItem, children, bubble);
                }
            }

            if (_ParseMiniProgram(children[1]))
            {
                bubble.MessageType = MessageType.小程序;
                var miniButton = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
                if (miniButton != null)
                {
                    bubble.ClickActionButton = miniButton.AsButton();
                }
                return bubble;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button != null)
            {
                bubble.ClickActionButton = button.AsButton();
            }

            //处理、解析引用消息
            _ParseReferencedMessage(children[1], listItem.Name, bubble);
            return bubble;
        }

        /// <summary>
        /// 解析引用消息
        /// </summary>
        /// <param name="rootPaneElement"></param>
        /// <param name="title"></param>
        /// <param name="parentBubble"></param>
        private void _ParseReferencedMessage(AutomationElement rootPaneElement, string title, MessageBubble parentBubble)
        {
            if (Regex.IsMatch(title, $@"(\n{WeChatConstant.MESSAGES_REFERENCE}\s)"))
            {
                var count = _uiThreadInvoker.Run(automation => rootPaneElement.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).Count()).GetAwaiter().GetResult();
                switch (count)
                {
                    case 1:
                        _ProcessPersionReferenceMesssage(rootPaneElement, parentBubble);
                        break;
                    case 2:
                    case 3:
                        _ProcessGroupReferenceMesssage(rootPaneElement, parentBubble, count);
                        break;
                    default:
                        throw new Exception("引用消息解析失败");
                }
            }
        }
        /// <summary>
        /// 处理个人引用消息
        /// </summary>
        /// <param name="rootPaneElement"></param>
        /// <param name="parentBubble"></param>
        private void _ProcessPersionReferenceMesssage(AutomationElement rootPaneElement, MessageBubble parentBubble)
        {
            var paneList = _uiThreadInvoker.Run(automation => rootPaneElement.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            paneList = _uiThreadInvoker.Run(automation => paneList.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            var subPaneList = _uiThreadInvoker.Run(automation => paneList.FindAllChildren()).GetAwaiter().GetResult();
            if (subPaneList.Count() != 1)
            {
                int index = 0;
                if (subPaneList.Count() > 2)
                {
                    index = 1;
                }
                var texts = _uiThreadInvoker.Run(automation => subPaneList[index].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
                var result = "";
                foreach (var text in texts)
                {
                    result += text.Name;
                }
                parentBubble.MessageContent = result;
                var refText = _uiThreadInvoker.Run(automation => subPaneList[index + 1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
                if (refText != null)
                {
                    result = "";
                    foreach (var text in refText)
                    {
                        result += text.Name;
                    }
                    parentBubble.BeReferencedPersion = Regex.Match(result, @"^(.*?)\s*:").Groups[1].Value;
                    parentBubble.BeReferencedMessage = Regex.Match(result, @":\s*(.*)$").Groups[1].Value;
                }
            }
        }
        /// <summary>
        /// 处理群引用消息
        /// 分为：
        /// 2：不显示群昵称
        /// 3: 显示群昵称
        /// </summary>
        /// <param name="rootPaneElement"></param>
        /// <param name="parentBubble"></param>
        /// <param name="count"></param>
        private void _ProcessGroupReferenceMesssage(AutomationElement rootPaneElement, MessageBubble parentBubble, int count)
        {
            int index = 0;
            parentBubble.GroupNickName = parentBubble.Who;
            var paneList = _uiThreadInvoker.Run(automation => rootPaneElement.FindAllChildren(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            var nickName = _uiThreadInvoker.Run(automation => paneList[0].FindFirstChild(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (nickName != null)
            {
                parentBubble.GroupNickName = nickName.Name;
            }
            if (count == 3)
            {
                index = 1;
            }
            var texts = _uiThreadInvoker.Run(automation => paneList[index].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                result += text.Name;
            }
            parentBubble.MessageContent = result;
            var refTexts = _uiThreadInvoker.Run(automation => paneList[index + 1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (refTexts != null)
            {
                result = "";
                foreach (var refText in refTexts)
                {
                    result += refText.Name;
                }
                parentBubble.BeReferencedPersion = Regex.Match(result, @"^(.*?)\s*:").Groups[1].Value;
                parentBubble.BeReferencedMessage = Regex.Match(result, @":\s*(.*)$").Groups[1].Value;
            }
        }

        /// <summary>
        /// 处理撤回消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="children"></param>
        /// <param name="bubble"></param>
        private static void _ProcessAnotherMessage(AutomationElement listItem, AutomationElement[] children, MessageBubble bubble)
        {
            if (children[2].ControlType == ControlType.Pane)
            {
                //处理撤回消息
                if (listItem.Name.StartsWith(WeChatConstant.MESSAGES_YOU))
                {
                    bubble.Who = "我";
                }
                else
                {
                    bubble.Who = Regex.Match(listItem.Name, @"^\""([^\""]+)\""").Success ? Regex.Match(listItem.Name, @"^\""([^\""]+)\""").Groups[1].Value : "系统消息";
                }
                bubble.MessageSource = MessageSourceType.系统消息;
                if (children[2].ControlType == ControlType.Pane && listItem.Name.Contains(WeChatConstant.MESSAGES_RECALL))
                {
                    bubble.MessageType = MessageType.撤回消息;
                }
            }
        }

        /// <summary>
        /// 解析小程序消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private bool _ParseMiniProgram(AutomationElement root)
        {
            var children = _uiThreadInvoker.Run(automation => root.FindAllDescendants(cf => cf.ByControlType(ControlType.Text)).ToList().Select(item => item.AsLabel()).ToList()).GetAwaiter().GetResult();
            var result = children.FirstOrDefault(item => item.Name == WeChatConstant.MESSAGES_MINI_PROGRAM);
            if (result != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 解析红包消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseRedPacket(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleRedPacket(listItem, dateTime);
            }
            else
            {
                return _ParseGroupRedPacket(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleRedPacket(AutomationElement listItem, DateTime? dateTime)
        {
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.红包;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Who = "系统消息";
            bubble.MessageContent = listItem.Name;
            return bubble;
        }
        private MessageBubble _ParseGroupRedPacket(AutomationElement listItem, DateTime? dateTime)
        {
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.红包;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.MessageTime = dateTime;
            bubble.Who = "系统消息";
            bubble.MessageContent = listItem.Name;
            bubble.GroupNickName = "";
            return bubble;
        }
        /// <summary>
        /// 解析微信转账消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseWeChatTransfer(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleWeChatTransfer(listItem, dateTime);
            }
            else
            {
                return _ParseGroupWeChatTransfer(listItem, dateTime);
            }
        }
        private MessageBubble _ParseSingleWeChatTransfer(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("微信转账消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.微信转账;
            if (children.Count() != 3)
            {
                throw new Exception("微信转账消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var textList = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in textList)
            {
                if (text.Name != WeChatConstant.MESSAGES_WECHAT_TRANSFER)
                {
                    result += text.Name;
                }
            }
            bubble.MessageContent = result;
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button != null)
            {
                bubble.ClickActionButton = button.AsButton();
            }
            return bubble;
        }
        private MessageBubble _ParseGroupWeChatTransfer(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("微信转账消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.微信转账;
            bubble.GroupNickName = __GetGroupNickName(children);
            if (children.Count() != 3)
            {
                throw new Exception("微信转账消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var textList = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in textList)
            {
                if (text.Name != WeChatConstant.MESSAGES_WECHAT_TRANSFER && text.Name.Trim() != bubble.Who.Trim())
                {
                    result += text.Name;
                }
            }
            bubble.MessageContent = result.Trim();
            return bubble;
        }

        /// <summary>
        /// 解析语音消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseVoice(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVoice(listItem, dateTime);
            }
            else
            {
                return _ParseGroupVoice(listItem, dateTime);
            }
        }
        private MessageBubble _ParseSingleVoice(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("语音消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.语音;
            if (children.Count() != 3)
            {
                throw new Exception("语音消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button.Count() != 1)
            {
                throw new Exception("语音消息解析失败");
            }
            bubble.ClickActionButton = button[0].AsButton();
            bubble.MessageContent = listItem.Name;
            return bubble;
        }
        private MessageBubble _ParseGroupVoice(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("语音消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.语音;
            if (children.Count() != 3)
            {
                throw new Exception("语音消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button.Count() != 1)
            {
                throw new Exception("语音消息解析失败");
            }
            bubble.ClickActionButton = button[0].AsButton();
            bubble.MessageContent = listItem.Name;
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析笔记消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseNote(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleNote(listItem, dateTime);
            }
            else
            {
                return _ParseGroupNote(listItem, dateTime);
            }
        }
        private MessageBubble _ParseSingleNote(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.笔记;
            if (children.Count() != 3)
            {
                throw new Exception("笔记消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            //笔记消息内容
            var textList = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in textList)
            {
                if (text.AsLabel().Name != WeChatConstant.MESSAGES_NOTE_TEXT && text.AsLabel().Name != WeChatConstant.MESSAGES_COLLECT)
                {
                    result = text.AsLabel().Name;
                    break;
                }
            }
            bubble.MessageContent = string.IsNullOrEmpty(result) ? "笔记" : result;
            return bubble;
        }
        private MessageBubble _ParseGroupNote(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.笔记;
            if (children.Count() != 3)
            {
                throw new Exception("笔记消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            //笔记消息内容
            var textList = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in textList)
            {
                if (text.AsLabel().Name != WeChatConstant.MESSAGES_NOTE_TEXT && text.AsLabel().Name != WeChatConstant.MESSAGES_COLLECT
                && text.AsLabel().Name != bubble.Who.Trim())
                {
                    result = text.AsLabel().Name;
                    break;
                }
            }
            bubble.MessageContent = string.IsNullOrEmpty(result) ? "笔记" : result;
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析链接消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseLink(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleLink(listItem, dateTime);
            }
            else
            {
                return _ParseGroupLink(listItem, dateTime);
            }
        }
        private MessageBubble _ParseSingleLink(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("链接消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.链接;
            if (children.Count() != 3)
            {
                throw new Exception("链接消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("链接消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (texts != null)
            {
                bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name)).Trim();
            }
            return bubble;
        }
        private MessageBubble _ParseGroupLink(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("链接消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.链接;
            if (children.Count() != 3)
            {
                throw new Exception("链接消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("链接消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析视频号直播消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideoNumberLive(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideoNumberLive(listItem, dateTime);
            }
            else
            {
                return _ParseGroupVideoNumberLive(listItem, dateTime);
            }
        }
        private MessageBubble _ParseSingleVideoNumberLive(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频号直播;
            if (children.Count() != 3)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name)).Trim();
            return bubble;
        }
        private MessageBubble _ParseGroupVideoNumberLive(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频号直播;
            if (children.Count() != 3)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }
        /// <summary>
        /// 解析视频号消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideoNumber(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideoNumber(listItem, dateTime);
            }
            else
            {
                return _ParseGroupVideoNumber(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleVideoNumber(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频号消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频号;
            if (children.Count() != 3)
            {
                throw new Exception("视频号消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (texts != null)
            {
                bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name)).Trim();
            }

            return bubble;
        }
        private MessageBubble _ParseGroupVideoNumber(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频号消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频号;
            if (children.Count() != 3)
            {
                throw new Exception("视频号消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析视频消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideo(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideo(listItem, dateTime);
            }
            else
            {
                return _ParseGroupVideo(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleVideo(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频;
            if (children.Count() != 3)
            {
                throw new Exception("视频消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name)).Trim();
            return bubble;
        }
        private MessageBubble _ParseGroupVideo(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("视频消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.视频;
            if (children.Count() != 3)
            {
                throw new Exception("视频消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("视频消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }
        /// <summary>
        /// 解析图片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseImage(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleImage(listItem, dateTime);
            }
            else
            {
                return _ParseGroupImage(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleImage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("图片消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.图片;
            if (children.Count() != 3)
            {
                throw new Exception("图片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "图片";
            return bubble;
        }

        private MessageBubble _ParseGroupImage(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("图片消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.图片;
            if (children.Count() != 3)
            {
                throw new Exception("图片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            bubble.GroupNickName = __GetGroupNickName(children);
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "图片";
            return bubble;
        }

        private string __GetGroupNickName(AutomationElement[] rootPanelChildren)
        {
            if (rootPanelChildren.Length != 3)
            {
                throw new Exception("消息解析失败");
            }
            if (rootPanelChildren[0].ControlType == ControlType.Button)
            {
                //这里要判断群聊窗口是否打开昵称
                var wxName = rootPanelChildren[0].AsButton().Name;
                var label = _uiThreadInvoker.Run(automation => rootPanelChildren[1].FindFirstChild(cf => cf.ByControlType(ControlType.Pane)).FindFirstChild(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
                if (label != null)
                {
                    return label.AsLabel().Name;
                }
                else
                {
                    return wxName;
                }
            }
            else
            {
                return "我";
            }
        }

        /// <summary>
        /// 解析位置消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseLocation(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleLocation(listItem, dateTime);
            }
            else
            {
                return _ParseGroupLocation(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleLocation(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult() ?? throw new Exception("位置消息解析失败");
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.位置;
            if (children.Count() != 3)
            {
                throw new Exception("位置消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var lable = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var locationContent = "";
            foreach (var item in lable)
            {
                locationContent += " " + item.AsLabel().Name.Trim();
            }
            if (string.IsNullOrEmpty(locationContent))
            {
                bubble.MessageContent = "位置";
            }
            else
            {
                bubble.MessageContent = locationContent.Trim();
            }
            return bubble;
        }
        private MessageBubble _ParseGroupLocation(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult() ?? throw new Exception("位置消息解析失败");
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.位置;
            if (children.Count() != 3)
            {
                throw new Exception("位置消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult() ?? throw new Exception("位置消息解析失败");
            bubble.ClickActionButton = button.AsButton();
            var lable = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var locationContent = "";
            foreach (var item in lable)
            {
                if (item.Name.Trim() != bubble.Who.Trim())
                {
                    locationContent += " " + item.AsLabel().Name.Trim();
                }
            }
            if (string.IsNullOrEmpty(locationContent))
            {
                bubble.MessageContent = "位置";
            }
            else
            {
                bubble.MessageContent = locationContent.Trim();
            }
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }



        /// <summary>
        /// 解析聊天记录消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseChatRecord(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleChatRecord(listItem, dateTime);
            }
            else
            {
                return _ParseGroupChatRecord(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleChatRecord(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.聊天记录;
            if (children.Count() != 3)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult() ?? throw new Exception("聊天记录消息解析失败");
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (texts != null)
            {
                bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name));
            }
            else
            {
                bubble.MessageContent = "聊天记录";
            }
            return bubble;
        }
        private MessageBubble _ParseGroupChatRecord(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.聊天记录;
            if (children.Count() != 3)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult() ?? throw new Exception("聊天记录消息解析失败");
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }
        /// <summary>
        /// 解析名片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseCard(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleCard(listItem, dateTime);
            }
            else
            {
                return _ParseGroupCard(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleCard(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("名片消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.个人名片;
            if (children.Count() != 3)
            {
                throw new Exception("名片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("名片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            if (texts != null)
            {
                bubble.MessageContent = string.Join(" ", texts.Select(t => t.AsLabel().Name));
            }
            else
            {
                bubble.MessageContent = "个人名片";
            }

            return bubble;
        }
        private MessageBubble _ParseGroupCard(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("名片消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.个人名片;
            if (children.Count() != 3)
            {
                throw new Exception("名片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            if (button == null)
            {
                throw new Exception("名片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);

            return bubble;
        }

        /// <summary>
        /// 解析文件消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseFile(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleFile(listItem, dateTime);
            }
            else
            {
                return _ParseGroupFile(listItem, dateTime);
            }
        }

        private MessageBubble _ParseSingleFile(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("文件消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.文件;
            if (children.Count() != 3)
            {
                throw new Exception("文件消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult().AsButton() ?? throw new Exception("文件消息解析失败");
            if (button == null)
            {
                throw new Exception("文件消息解析失败");
            }
            bubble.ClickActionButton = button;
            bubble.MessageContent = "文件";
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != "微信电脑版")
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            return bubble;
        }
        private MessageBubble _ParseGroupFile(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("文件消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            var bubble = new MessageBubble();
            bubble.MessageTime = dateTime;
            bubble.MessageType = MessageType.文件;
            if (children.Count() != 3)
            {
                throw new Exception("文件消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Who = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Who = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = _uiThreadInvoker.Run(automation => children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult().AsButton() ?? throw new Exception("文件消息解析失败");
            if (button == null)
            {
                throw new Exception("文件消息解析失败");
            }
            bubble.ClickActionButton = button;
            bubble.MessageContent = "文件";
            var texts = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
            var result = "";
            foreach (var text in texts)
            {
                if (text.Name.Trim() != "微信电脑版" && text.Name.Trim() != bubble.Who.Trim())
                {
                    result += " " + text.AsLabel().Name.Trim();
                }
            }
            bubble.MessageContent = result.Trim();
            bubble.GroupNickName = __GetGroupNickName(children);
            return bubble;
        }

        /// <summary>
        /// 解析拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParsePickUp(AutomationElement listItem, DateTime? dateTime)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSinglePickUp(listItem, dateTime);
            }
            else
            {
                return _ParseGroupPickUp(listItem, dateTime);
            }
        }

        /// <summary>
        /// 解析群聊拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParseGroupPickUp(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            if (children.Count() != 3)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var items = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.ListItem))).GetAwaiter().GetResult();
            var list = new List<MessageBubble>();
            foreach (var item in items)
            {
                var bubbleItem = new MessageBubble();
                bubbleItem.MessageType = MessageType.拍一拍;
                var title = item.Name;
                if (title.StartsWith(WeChatConstant.MESSAGES_I))
                {
                    bubbleItem.MessageSource = MessageSourceType.自己发送消息;
                    bubbleItem.Who = "我";
                    bubbleItem.BeClapPerson = Regex.Match(title, @"""([^""]+)""").Groups[1].Value;
                }
                else
                {
                    if (title.Contains("我"))
                    {
                        bubbleItem.MessageSource = MessageSourceType.好友消息;
                        bubbleItem.Who = Regex.Match(title, @"^""([^""]+)""").Groups[1].Value;
                        bubbleItem.GroupNickName = bubbleItem.Who;
                        bubbleItem.BeClapPerson = "我";
                    }
                    else
                    {
                        bubbleItem.MessageSource = MessageSourceType.好友消息;
                        int index = 0;
                        var matches = Regex.Matches(title, "\"([^\"]+)\"");
                        foreach (Match m in matches)
                        {
                            if (index == 0)
                            {
                                bubbleItem.Who = m.Groups[1].Value;
                                bubbleItem.GroupNickName = bubbleItem.Who;
                                index++;
                            }
                            else if (index == 1)
                            {
                                bubbleItem.BeClapPerson = m.Groups[1].Value;
                            }
                        }
                    }
                }
                bubbleItem.MessageContent = title;
                list.Add(bubbleItem);
            }
            return list;
        }


        /// <summary>
        /// 解析单聊拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParseSinglePickUp(AutomationElement listItem, DateTime? dateTime)
        {
            var paneElement = _uiThreadInvoker.Run(automation => listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            if (paneElement == null)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var children = _uiThreadInvoker.Run(automation => paneElement.FindAllChildren()).GetAwaiter().GetResult();
            if (children.Count() != 3)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var items = _uiThreadInvoker.Run(automation => children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.ListItem))).GetAwaiter().GetResult();
            var list = new List<MessageBubble>();
            foreach (var item in items)
            {
                var bubbleItem = new MessageBubble();
                bubbleItem.MessageType = MessageType.拍一拍;
                var title = item.Name;
                if (title.StartsWith(WeChatConstant.MESSAGES_I))
                {
                    bubbleItem.MessageSource = MessageSourceType.自己发送消息;
                    bubbleItem.Who = "我";
                    bubbleItem.BeClapPerson = Regex.Match(title, @"""([^""]+)""$").Groups[1].Value;
                }
                else
                {
                    bubbleItem.MessageSource = MessageSourceType.好友消息;
                    bubbleItem.Who = Regex.Match(title, @"^""([^""]+)""").Groups[1].Value;
                    bubbleItem.BeClapPerson = "我";
                }
                bubbleItem.MessageContent = title;
                list.Add(bubbleItem);
            }
            return list;
        }

        /// <summary>
        /// 解析时间消息
        /// </summary>
        /// <param name="listItemChild"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private MessageBubble _ParseTimeMessage(AutomationElement listItemChild, ref DateTime? dateTime)
        {
            var text = listItemChild.AsLabel();
            MessageBubble bubble = new MessageBubble();
            bubble.MessageType = MessageType.时间;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Who = "系统消息";
            bubble.MessageContent = text.Name;
            dateTime = _ProcessDateStr(text.Name);
            bubble.MessageTime = dateTime;
            return bubble;
        }

        private DateTime? __ParseStringToDateTime(string dateStr)
        {
            // 定义支持的中文日期格式
            string[] formats = new string[]
            {
                "yyyy年M月d日 H:m",
                "yyyy年M月d日 H:m:s",
                "yyyy年MM月dd日 HH:mm:ss",
                "yyyy年M月d日",
                "yyyy年MM月dd日",
                "yyyy年M月d日 H:mm",
                "yyyy年M月d日 HH:mm",
                "yyyy年M月d日 H:mm:ss",
            };
            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                return dt;
            }
            else
            {
                throw new Exception("日期字符串解析失败");
            }
        }

        /// <summary>
        /// 日期字符串处理方法
        /// </summary>
        /// <param name="date">原始日期字符串</param>
        /// <returns>标准化后的日期字符串</returns>
        private DateTime? _ProcessDateStr(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return null;
            }

            // 直接匹配"xxxx年xx月xx日 xx:xx"格式，直接返回
            if (Regex.IsMatch(date, @"^\d{4}年\d{1,2}月\d{1,2}日\s+\d{1,2}:\d{1,2}$"))
            {
                return __ParseStringToDateTime(date);
            }

            // 匹配"昨天 xx:xx"或"前天 xx:xx"
            var match = Regex.Match(date, @"^(昨天|前天)\s*(\d{1,2}:\d{1,2})$");
            if (match.Success)
            {
                int daysAgo = match.Groups[1].Value == "昨天" ? 1 : 2;
                string ymd = DateTime.Now.AddDays(-daysAgo).ToString("yyyy年MM月dd日");
                string hm = match.Groups[2].Value;
                return __ParseStringToDateTime($"{ymd} {hm}");
            }

            // 匹配"xx:xx"格式，补全为今天
            if (Regex.IsMatch(date, @"^\d{1,2}:\d{1,2}$"))
            {
                string ymd = DateTime.Now.ToString("yyyy年MM月dd日");
                return __ParseStringToDateTime($"{ymd} {date}");
            }
            //匹配: 星期二 15:53 星期二 6:52 星期三 0:01这种格式
            DateTime? refData = null;
            if (MatchWeekFormat(date, ref refData))
            {
                return refData;
            }

            // 匹配"xxxx年xx月xx日"但无时间，补全为00:00
            match = Regex.Match(date, @"^(\d{4}年\d{1,2}月\d{1,2}日)$");
            if (match.Success)
            {
                return __ParseStringToDateTime($"{match.Groups[1].Value} 00:00");
            }

            // 其他情况直接返回原字符串
            throw new Exception("日期字符串解析失败,收到的日期字符串为:" + date);
        }

        private bool MatchWeekFormat(string date, ref DateTime? refData)
        {
            var regex = new Regex(@"星期(?<day>[一二三四五六天])\s+(?<hour>\d{1,2}):(?<minute>\d{2})");
            var dayMap = new Dictionary<string, DayOfWeek>
            {
                { "一", DayOfWeek.Monday },
                { "二", DayOfWeek.Tuesday },
                { "三", DayOfWeek.Wednesday },
                { "四", DayOfWeek.Thursday },
                { "五", DayOfWeek.Friday },
                { "六", DayOfWeek.Saturday },
                { "天", DayOfWeek.Sunday }
            };

            var match = regex.Match(date);
            if (match.Success)
            {
                string dayStr = match.Groups["day"].Value;
                int hour = int.Parse(match.Groups["hour"].Value);
                int minute = int.Parse(match.Groups["minute"].Value);

                DayOfWeek targetDay = dayMap[dayStr];
                DateTime now = DateTime.Now;

                // 计算本周目标日期的日期
                int currentDayOfWeek = (int)now.DayOfWeek;
                int targetDayOfWeek = (int)targetDay;

                // 计算本周目标日期
                int daysToAdd = (targetDayOfWeek - currentDayOfWeek + 7) % 7;
                DateTime thisWeekTarget = now.Date.AddDays(daysToAdd).AddHours(hour).AddMinutes(minute);

                // 如果本周的目标日期还没有到（即目标日期在未来），则认为是上周的日期
                if (thisWeekTarget > now)
                {
                    // 使用上周的日期
                    refData = thisWeekTarget.AddDays(-7);
                }
                else
                {
                    // 使用本周的日期
                    refData = thisWeekTarget;
                }

                return true;
            }
            return false;
        }
        #endregion 解释消息各类私有方法
    }
}
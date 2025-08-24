using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using System.Text.RegularExpressions;
using WxAutoCommon.Interface;
using WxAutoCore.Extentions;
using Microsoft.Win32.SafeHandles;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区气泡列表
    /// </summary>
    public class MessageBubbleList
    {
        private Window _SelfWindow;
        private IWeChatWindow _WxWindow;
        private string _Title;
        private AutomationElement _BubbleListRoot;
        public List<MessageBubble> Bubbles => GetBubbles();
        public ListBox BubbleListRoot => _BubbleListRoot.AsListBox();   //用于订阅事件
        public MessageBubbleList(Window selfWindow, AutomationElement bubbleListRoot, IWeChatWindow wxWindow, string title)
        {
            _SelfWindow = selfWindow;
            _BubbleListRoot = bubbleListRoot;
            _WxWindow = wxWindow;
            _Title = title;
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
            MessageBubble[] bubbles = GetBubbles().ToArray();
            return bubbles.Count() > 0 ? bubbles.Last() : null;
        }

        /// <summary>
        /// 加载更多
        /// </summary>
        public void LoadMore()
        {
            var lookMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)));
            if (lookMoreButton != null)
            {
                _WxWindow.SilenceClickExt(lookMoreButton.AsButton());
            }
        }

        /// <summary>
        /// 是否有加载更多按钮
        /// </summary>
        /// <returns>是否有加载更多按钮</returns>
        public bool IsLoadingMore()
        {
            var lookMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button));
            return lookMoreButton != null;
        }

        /// <summary>
        /// 获取气泡列表
        /// </summary>
        public List<MessageBubble> GetBubbles()
        {
            var listItemList = _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
            List<MessageBubble> bubbles = new List<MessageBubble>();
            for (int i = 0; i < listItemList.Count; i++)
            {
                var bubble = ParseBubble(listItemList[i]);
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
        /// 解析气泡为Bubble对象,Bubble对象可能为空
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns>Bubble对象,可能为空,也可能为List<Bubble>对象;<see cref="MessageBubble"/></returns>
        private Object ParseBubble(AutomationElement listItem)
        {
            var listItemChildren = listItem.FindAllChildren();
            if (listItemChildren.Count() == 0)
            {
                return null;
            }
            var dateStr = "";

            if (listItemChildren.Count() == 1)
            {

                if (string.IsNullOrEmpty(listItemChildren[0].Name))
                {
                    //非时间消息
                    return _ParseMessage(listItem, dateStr);
                }
                else
                {
                    //系统消息，并且是时间消息
                    return _ParseTimeMessage(listItemChildren[0], ref dateStr);
                }

            }

            throw new Exception("气泡解析失败");
        }
        /// <summary>
        /// 解析除消息以外的气泡
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns>Bubble对象,可能为空;<see cref="MessageBubble"/></returns>
        private Object _ParseMessage(AutomationElement listItem, string dateStr)
        {
            if (listItem.Name.Trim() == WeChatConstant.MESSAGES_PICK_UP)
            {
                return _ParsePickUp(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_IMAGE)
            {
                return _ParseImage(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LOCATION)
            {
                return _ParseLocation(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CHAT_RECORD)
            {
                return _ParseChatRecord(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CARD)
            {
                return _ParseCard(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_FILE)
            {
                return _ParseFile(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO)
            {
                return _ParseVideo(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER)
            {
                return _ParseVideoNumber(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER_LIVE)
            {
                return _ParseVideoNumberLive(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LINK)
            {
                return _ParseLink(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_NOTE)
            {
                return _ParseNote(listItem, dateStr);
            }
            if (listItem.Name.Contains(WeChatConstant.MESSAGES_VOICE))
            {
                return _ParseVoice(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_RED_PACKET_SEND || listItem.Name == WeChatConstant.MESSAGES_RED_PACKET_RECEIVE)
            {
                return _ParseRedPacket(listItem, dateStr);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_WECHAT_TRANSFER)
            {
                return _ParseWeChatTransfer(listItem, dateStr);
            }
            if (Regex.IsMatch(listItem.Name, @"^(\[[^\]]+\])+$"))
            {
                return _ParseExpressionMessage(listItem, dateStr);
            }
            var bubble = _ParseTextMessage(listItem, dateStr);
            return bubble;
        }

        /// <summary>
        /// 解析表情消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseExpressionMessage(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleExpressionMessage(listItem, dateStr);
            }
            else
            {
                return _ParseGroupExpressionMessage(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleExpressionMessage(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
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
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            return bubble;
        }
        private MessageBubble _ParseGroupExpressionMessage(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }



        /// <summary>
        /// 解析文本消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseTextMessage(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleTextMessage(listItem, dateStr);
            }
            else
            {
                return _ParseGroupTextMessage(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleTextMessage(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("文本消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            if (children.Count() != 3)
            {
                bubble.MessageType = MessageType.文字;
                bubble.MessageSource = MessageSourceType.系统消息;
                bubble.Sender = MessageSourceType.系统消息.ToString();
                bubble.MessageContent = listItem.Name;
                return bubble;
            }
            bubble.MessageType = MessageType.文字;
            bubble.MessageContent = listItem.Name;
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                if (children[2].ControlType == ControlType.Button)
                {
                    bubble.Sender = "我";
                    bubble.MessageSource = MessageSourceType.自己发送消息;
                }
                else
                {
                    _ProcessRecallMessage(listItem, children, bubble);
                }
            }

            if (_ParseMiniProgram(children[1]))
            {
                bubble.MessageType = MessageType.小程序;
                var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
                if (button != null)
                {
                    bubble.ClickActionButton = button.AsButton();
                }
                return bubble;
            }

            //处理、解析引用消息
            _ParseReferencedMessage(children[1], listItem.Name, bubble);
            return bubble;
        }
        private MessageBubble _ParseGroupTextMessage(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }



        /// <summary>
        /// 解析引用消息
        /// </summary>
        /// <param name="rootPaneElement"></param>
        /// <param name="title"></param>
        /// <param name="parentBubble"></param>
        private void _ParseReferencedMessage(AutomationElement rootPaneElement, string title, MessageBubble parentBubble)
        {
            parentBubble.GroupNickName = parentBubble.Sender;
            if (Regex.IsMatch(title, $@"(\n{WeChatConstant.MESSAGES_REFERENCE}\s)"))
            {
                var count = rootPaneElement.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).Count();
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
            var paneList = rootPaneElement.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            paneList = paneList.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            var subPaneList = paneList.FindAllChildren();
            if (subPaneList.Count() != 1)
            {
                int index = 0;
                if (subPaneList.Count() > 2)
                {
                    index = 1;
                }
                var texts = subPaneList[index].FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
                var result = "";
                foreach (var text in texts)
                {
                    result += text.Name;
                }
                parentBubble.MessageContent = result;
                var refText = subPaneList[index + 1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
                if (refText != null)
                {
                    parentBubble.BeReferencedPersion = Regex.Match(refText.Name, @"^(.*?)\s*:").Groups[1].Value;
                    parentBubble.BeReferencedMessage = Regex.Match(refText.Name, @":\s*(.*)$").Groups[1].Value;
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
            parentBubble.GroupNickName = parentBubble.Sender;
            var paneList = rootPaneElement.FindAllChildren(cf => cf.ByControlType(ControlType.Pane));
            var nickName = paneList[0].FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
            if (nickName != null)
            {
                parentBubble.GroupNickName = nickName.Name;
            }
            if (count == 3)
            {
                index = 1;
            }
            var texts = paneList[index].FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var result = "";
            foreach (var text in texts)
            {
                result += text.Name;
            }
            parentBubble.MessageContent = result;
            var refText = paneList[index + 1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
            if (refText != null)
            {
                parentBubble.BeReferencedPersion = Regex.Match(refText.Name, @"^(.*?)\s*:").Groups[1].Value;
                parentBubble.BeReferencedMessage = Regex.Match(refText.Name, @":\s*(.*)$").Groups[1].Value;
            }
        }

        /// <summary>
        /// 处理撤回消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="children"></param>
        /// <param name="bubble"></param>
        private static void _ProcessRecallMessage(AutomationElement listItem, AutomationElement[] children, MessageBubble bubble)
        {
            if (children[2].ControlType == ControlType.Pane)
            {
                //处理撤回消息
                if (listItem.Name.StartsWith(WeChatConstant.MESSAGES_YOU))
                {
                    bubble.Sender = "我";
                }
                else
                {
                    bubble.Sender = Regex.Match(listItem.Name, @"^\""([^\""]+)\""").Success ? Regex.Match(listItem.Name, @"^\""([^\""]+)\""").Groups[1].Value : "系统消息";
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
            var children = root.FindAllDescendants(cf => cf.ByControlType(ControlType.Text)).ToList().Select(item => item.AsLabel()).ToList();
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
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseRedPacket(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleRedPacket(listItem, dateStr);
            }
            else
            {
                return _ParseGroupRedPacket(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleRedPacket(AutomationElement listItem, string dateStr)
        {
            MessageBubble bubble = new MessageBubble();
            bubble.MessageType = MessageType.红包;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Sender = "系统消息";
            bubble.MessageContent = listItem.Name;
            return bubble;
        }
        private MessageBubble _ParseGroupRedPacket(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }
        /// <summary>
        /// 解析微信转账消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseWeChatTransfer(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleWeChatTransfer(listItem, dateStr);
            }
            else
            {
                return _ParseGroupWeChatTransfer(listItem, dateStr);
            }
        }
        private MessageBubble _ParseSingleWeChatTransfer(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("微信转账消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.微信转账;
            if (children.Count() != 3)
            {
                throw new Exception("微信转账消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var textList = children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var result = "";
            foreach (var text in textList)
            {
                if (text.Name != WeChatConstant.MESSAGES_WECHAT_TRANSFER)
                {
                    result += text.Name;
                }
            }
            bubble.MessageContent = result;
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button != null)
            {
                bubble.ClickActionButton = button.AsButton();
            }
            return bubble;
        }
        private MessageBubble _ParseGroupWeChatTransfer(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }

        /// <summary>
        /// 解析语音消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseVoice(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVoice(listItem, dateStr);
            }
            else
            {
                return _ParseGroupVoice(listItem, dateStr);
            }
        }
        private MessageBubble _ParseSingleVoice(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("语音消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.语音;
            if (children.Count() != 3)
            {
                throw new Exception("语音消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            if (button.Count() != 1)
            {
                throw new Exception("语音消息解析失败");
            }
            bubble.ClickActionButton = button[0].AsButton();
            bubble.MessageContent = listItem.Name;
            return bubble;
        }
        private MessageBubble _ParseGroupVoice(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }

        /// <summary>
        /// 解析笔记消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseNote(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleNote(listItem, dateStr);
            }
            else
            {
                return _ParseGroupNote(listItem, dateStr);
            }
        }
        private MessageBubble _ParseSingleNote(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.笔记;
            if (children.Count() != 3)
            {
                throw new Exception("笔记消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            //笔记消息内容
            var textList = children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
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
        private MessageBubble _ParseGroupNote(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }

        /// <summary>
        /// 解析链接消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseLink(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleLink(listItem, dateStr);
            }
            else
            {
                return _ParseGroupLink(listItem, dateStr);
            }
        }
        private MessageBubble _ParseSingleLink(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("链接消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.链接;
            if (children.Count() != 3)
            {
                throw new Exception("链接消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("链接消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            //笔记消息内容
            var text = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Text");
            if (text != null)
            {
                bubble.MessageContent = text.AsLabel().Name;
            }
            else
            {
                bubble.MessageContent = "链接";
            }
            return bubble;
        }
        private MessageBubble _ParseGroupLink(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }

        /// <summary>
        /// 解析视频号直播消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideoNumberLive(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideoNumberLive(listItem, dateStr);
            }
            else
            {
                return _ParseGroupVideoNumberLive(listItem, dateStr);
            }
        }
        private MessageBubble _ParseSingleVideoNumberLive(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.视频号直播;
            if (children.Count() != 3)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }

            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var textRoot = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane");
            var text = textRoot.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var result = "";
            foreach (var item in text)
            {
                if (item.AsLabel().Name != WeChatConstant.MESSAGES_VIDEO_NUMBER_LIVE_END &&
                    item.AsLabel().Name != WeChatConstant.MESSAGES_VIDEO_NUMBER_LIVE_ING)
                {
                    result = item.AsLabel().Name;
                    break;
                }
            }
            bubble.MessageContent = string.IsNullOrEmpty(result) ? "视频号直播" : result;
            return bubble;
        }
        private MessageBubble _ParseGroupVideoNumberLive(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }
        /// <summary>
        /// 解析视频号消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideoNumber(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideoNumber(listItem, dateStr);
            }
            else
            {
                return _ParseGroupVideoNumber(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleVideoNumber(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频号消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.视频号;
            if (children.Count() != 3)
            {
                throw new Exception("视频号消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var text = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/Text");
            if (text != null)
            {
                bubble.MessageContent = text.AsLabel().Name;
            }
            else
            {
                bubble.MessageContent = "视频号";
            }

            return bubble;
        }
        private MessageBubble _ParseGroupVideoNumber(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }

        /// <summary>
        /// 解析视频消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseVideo(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleVideo(listItem, dateStr);
            }
            else
            {
                return _ParseGroupVideo(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleVideo(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.视频;
            if (children.Count() != 3)
            {
                throw new Exception("视频消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
            {
                throw new Exception("视频消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "视频";
            return bubble;
        }
        private MessageBubble _ParseGroupVideo(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }



        /// <summary>
        /// 解析图片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseImage(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleImage(listItem, dateStr);
            }
            else
            {
                return _ParseGroupImage(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleImage(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("图片消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.图片;
            if (children.Count() != 3)
            {
                throw new Exception("图片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "图片";
            return bubble;
        }

        private MessageBubble _ParseGroupImage(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }
        /// <summary>
        /// 解析位置消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseLocation(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleLocation(listItem, dateStr);
            }
            else
            {
                return _ParseGroupLocation(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleLocation(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("位置消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.位置;
            if (children.Count() != 3)
            {
                throw new Exception("位置消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var lable = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
            if (lable != null)
            {
                bubble.MessageContent = lable.AsLabel().Name;
            }
            else
            {
                bubble.MessageContent = "位置";
            }
            return bubble;
        }
        private MessageBubble _ParseGroupLocation(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }



        /// <summary>
        /// 解析聊天记录消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseChatRecord(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleChatRecord(listItem, dateStr);
            }
            else
            {
                return _ParseGroupChatRecord(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleChatRecord(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.聊天记录;
            if (children.Count() != 3)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "群聊的聊天记录";
            return bubble;
        }
        private MessageBubble _ParseGroupChatRecord(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }




        /// <summary>
        /// 解析名片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseCard(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleCard(listItem, dateStr);
            }
            else
            {
                return _ParseGroupCard(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleCard(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("名片消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.个人名片;
            if (children.Count() != 3)
            {
                throw new Exception("名片消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button == null)
            {
                throw new Exception("名片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            var text = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
            if (text != null)
            {
                bubble.MessageContent = text.AsLabel().Name;
            }
            else
            {
                bubble.MessageContent = "个人名片";
            }

            return bubble;
        }
        private MessageBubble _ParseGroupCard(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }




        /// <summary>
        /// 解析文件消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private MessageBubble _ParseFile(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSingleFile(listItem, dateStr);
            }
            else
            {
                return _ParseGroupFile(listItem, dateStr);
            }
        }

        private MessageBubble _ParseSingleFile(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("文件消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new MessageBubble();
            bubble.MessageType = MessageType.文件;
            if (children.Count() != 3)
            {
                throw new Exception("文件消息解析失败");
            }
            if (children[0].ControlType == ControlType.Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Button[1]").AsButton();
            if (button == null)
            {
                throw new Exception("文件消息解析失败");
            }
            bubble.ClickActionButton = button;
            bubble.MessageContent = "文件";
            var text = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane[1]/Text").AsLabel();
            if (text != null)
            {
                bubble.MessageContent = text.Name;
            }
            else
            {
                bubble.MessageContent = "文件";
            }
            return bubble;
        }
        private MessageBubble _ParseGroupFile(AutomationElement listItem, string dateStr)
        {
            return new MessageBubble();
        }




        /// <summary>
        /// 解析拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParsePickUp(AutomationElement listItem, string dateStr)
        {
            if (GetChatType() == ChatType.好友)
            {
                return _ParseSinglePickUp(listItem, dateStr);
            }
            else
            {
                return _ParseGroupPickUp(listItem, dateStr);
            }
        }

        /// <summary>
        /// 解析群聊拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParseGroupPickUp(AutomationElement listItem, string dateStr)
        {
            return new List<MessageBubble>();
        }


        /// <summary>
        /// 解析单聊拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private List<MessageBubble> _ParseSinglePickUp(AutomationElement listItem, string dateStr)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            if (children.Count() != 3)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var items = children[1].FindAllDescendants(cf => cf.ByControlType(ControlType.ListItem));
            var list = new List<MessageBubble>();
            foreach (var item in items)
            {
                var bubbleItem = new MessageBubble();
                bubbleItem.MessageType = MessageType.拍一拍;
                var title = item.Name;
                if (title.StartsWith(WeChatConstant.MESSAGES_I))
                {
                    bubbleItem.MessageSource = MessageSourceType.自己发送消息;
                    bubbleItem.Sender = "我";
                    bubbleItem.BeClapPerson = Regex.Match(title, @"""([^""]+)""$").Groups[1].Value;
                }
                else
                {
                    bubbleItem.MessageSource = MessageSourceType.好友消息;
                    bubbleItem.Sender = Regex.Match(title, @"^""([^""]+)""").Groups[1].Value;
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
        /// <returns></returns>
        private MessageBubble _ParseTimeMessage(AutomationElement listItemChild, ref string dateStr)
        {
            var text = listItemChild.AsLabel();
            MessageBubble bubble = new MessageBubble();
            bubble.MessageType = MessageType.时间;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Sender = "系统消息";
            bubble.MessageContent = text.Name;
            dateStr = _ProcessDateStr(text.Name);
            return bubble;
        }

        /// <summary>
        /// 日期字符串处理方法
        /// </summary>
        /// <param name="date">原始日期字符串</param>
        /// <returns>标准化后的日期字符串</returns>
        private string _ProcessDateStr(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return string.Empty;
            }

            // 直接匹配“xxxx年xx月xx日 xx:xx”格式，直接返回
            if (Regex.IsMatch(date, @"^\d{4}年\d{1,2}月\d{1,2}日\s+\d{1,2}:\d{1,2}$"))
            {
                return date;
            }

            // 匹配“昨天 xx:xx”或“前天 xx:xx”
            var match = Regex.Match(date, @"^(昨天|前天)\s*(\d{1,2}:\d{1,2})$");
            if (match.Success)
            {
                int daysAgo = match.Groups[1].Value == "昨天" ? 1 : 2;
                string ymd = DateTime.Now.AddDays(-daysAgo).ToString("yyyy年MM月dd日");
                string hm = match.Groups[2].Value;
                return $"{ymd} {hm}";
            }

            // 匹配“xx:xx”格式，补全为今天
            if (Regex.IsMatch(date, @"^\d{1,2}:\d{1,2}$"))
            {
                string ymd = DateTime.Now.ToString("yyyy年MM月dd日");
                return $"{ymd} {date}";
            }

            // 匹配“xxxx年xx月xx日”但无时间，补全为00:00
            match = Regex.Match(date, @"^(\d{4}年\d{1,2}月\d{1,2}日)$");
            if (match.Success)
            {
                return $"{match.Groups[1].Value} 00:00";
            }

            // 其他情况直接返回原字符串
            return date;
        }
    }
}
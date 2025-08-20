using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using System.Text.RegularExpressions;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区气泡列表
    /// </summary>
    public class BubbleList
    {
        private Window _SelfWindow;
        private AutomationElement _BubbleListRoot;
        public List<Bubble> Bubbles => GetBubbles();
        public ListBox BubbleListRoot => _BubbleListRoot.AsListBox();   //用于订阅事件
        public BubbleList(Window selfWindow, AutomationElement bubbleListRoot)
        {
            _SelfWindow = selfWindow;
            _BubbleListRoot = bubbleListRoot;
        }

        /// <summary>
        /// 获取气泡列表
        /// </summary>
        public List<Bubble> GetBubbles()
        {
            var listItemList = _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
            List<Bubble> bubbles = new List<Bubble>();
            for (int i = 0; i < listItemList.Count; i++)
            {
                var bubble = ParseBubble(listItemList[i]);
                if (bubble != null)
                {
                    if (bubble is Bubble)
                    {
                        bubbles.Add(bubble as Bubble);
                    }
                    else
                    {
                        bubbles.AddRange(bubble as List<Bubble>);
                    }
                }
            }
            return bubbles;
        }
        /// <summary>
        /// 解析气泡为Bubble对象,Bubble对象可能为空
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns>Bubble对象,可能为空,也可能为List<Bubble>对象;<see cref="Bubble"/></returns>
        private Object ParseBubble(AutomationElement listItem)
        {
            var listItemChildren = listItem.FindAllChildren();
            if (listItemChildren.Count() == 0)
            {
                return null;
            }
            if (listItemChildren.Count() == 1)
            {
                //系统消息，并且是时间消息
                return _ParseTimeMessage(listItemChildren[0]);
            }
            else
            {
                //消息
                return _ParseMessage(listItem);
            }

            throw new Exception("气泡解析失败");
        }
        /// <summary>
        /// 解析除消息以外的气泡
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns>Bubble对象,可能为空;<see cref="Bubble"/></returns>
        private Object _ParseMessage(AutomationElement listItem)
        {
            if (listItem.Name.Trim() == WeChatConstant.MESSAGES_PICK_UP)
            {
                return _ParsePickUp(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_IMAGE)
            {
                return _ParseImage(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LOCATION)
            {
                return _ParseLocation(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CHAT_RECORD)
            {
                return _ParseChatRecord(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_CARD)
            {
                return _ParseCard(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_FILE)
            {
                return _ParseFile(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO)
            {
                return _ParseVideo(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER)
            {
                return _ParseVideoNumber(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_VIDEO_NUMBER_LIVE)
            {
                return _ParseVideoNumberLive(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_LINK)
            {
                return _ParseLink(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_NOTE)
            {
                return _ParseNote(listItem);
            }
            if (listItem.Name.Contains(WeChatConstant.MESSAGES_VOICE))
            {
                return _ParseVoice(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_RED_PACKET)
            {
                return _ParseRedPacket(listItem);
            }
            if (listItem.Name == WeChatConstant.MESSAGES_WECHAT_TRANSFER)
            {
                return _ParseWeChatTransfer(listItem);
            }
            if (Regex.IsMatch(listItem.Name, @"^(\[[^\]]+\])+$"))
            {
                return _ParseExpressionMessage(listItem);
            }
            var bubble = _ParseTextMessage(listItem);
            return bubble;
        }

        /// <summary>
        /// 解析表情消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseExpressionMessage(AutomationElement listItem)
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
            Bubble bubble = new Bubble();
            bubble.MessageType = MessageType.表情;
            bubble.MessageContent = listItem.Name;
            if (children[0] is Button)
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
        /// <summary>
        /// 解析文本消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseTextMessage(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("文本消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
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
            if (children[0] is Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
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
        private void _ParseReferencedMessage(AutomationElement rootPaneElement, string title, Bubble parentBubble)
        {
            if (Regex.IsMatch(title, @"(\n引用\s)"))
            {
                var paneList = rootPaneElement.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
                paneList = paneList.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
                var subPaneList = paneList.FindAllChildren();
                if (subPaneList.Count() != 1)
                {
                    var pane = subPaneList[1];
                    var text = pane.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
                    var bubble = new Bubble();
                    bubble.MessageType = MessageType.引用;
                    bubble.MessageSource = MessageSourceType.被引用的消息;
                    bubble.Sender = Regex.Match(text.Name, @"^(.*?)\s*:").Groups[1].Value;
                    bubble.MessageContent = Regex.Match(text.Name, @":\s*(.*)$").Groups[1].Value;
                    parentBubble.ReferencedBubble = bubble;
                }
            }
        }
        /// <summary>
        /// 解析红包消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseRedPacket(AutomationElement listItem)
        {
            Bubble bubble = new Bubble();
            bubble.MessageType = MessageType.红包;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Sender = "系统消息";
            bubble.MessageContent = listItem.Name;
            return bubble;
        }
        /// <summary>
        /// 解析微信转账消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseWeChatTransfer(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("微信转账消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.微信转账;
            if (children.Count() != 3)
            {
                throw new Exception("微信转账消息解析失败");
            }
            if (children[0] is Button)
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
                if (Regex.IsMatch(text.Name, @"^￥[\d\.]*$"))
                {
                    result += Regex.Match(text.Name, @"^￥([\d\.]*)$").Groups[0].Value;
                }
            }
            bubble.MessageContent = "微信转账:" + result;
            var button = children[1].FindFirstDescendant(cf => cf.ByControlType(ControlType.Button));
            if (button != null)
            {
                bubble.ClickActionButton = button.AsButton();
            }
            return bubble;
        }

        /// <summary>
        /// 解析语音消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseVoice(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("语音消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.语音;
            if (children.Count() != 3)
            {
                throw new Exception("语音消息解析失败");
            }
            if (children[0] is Button)
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

        /// <summary>
        /// 解析笔记消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseNote(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("笔记消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.笔记;
            if (children.Count() != 3)
            {
                throw new Exception("笔记消息解析失败");
            }
            if (children[0] is Button)
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

        /// <summary>
        /// 解析链接消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseLink(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("链接消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.链接;
            if (children.Count() != 3)
            {
                throw new Exception("链接消息解析失败");
            }
            if (children[0] is Button)
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

        /// <summary>
        /// 解析视频号直播消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseVideoNumberLive(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.视频号直播;
            if (children.Count() != 3)
            {
                throw new Exception("视频号直播消息解析失败");
            }
            if (children[0] is Button)
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
            var text = children[1].FindAllByXPath("/Pane/Pane/Pane/Pane/Text");
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
        /// <summary>
        /// 解析视频号消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseVideoNumber(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频号消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.视频号;
            if (children.Count() != 3)
            {
                throw new Exception("视频号消息解析失败");
            }
            if (children[0] is Button)
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

        /// <summary>
        /// 解析视频消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseVideo(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("视频消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.视频;
            if (children.Count() != 3)
            {
                throw new Exception("视频消息解析失败");
            }
            if (children[0] is Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("视频消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "视频";
            return bubble;
        }
        /// <summary>
        /// 解析图片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseImage(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("图片消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.图片;
            if (children.Count() != 3)
            {
                throw new Exception("图片消息解析失败");
            }
            if (children[0] is Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Button");
            if (button == null)
            {
                throw new Exception("图片消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "图片";
            return bubble;
        }
        /// <summary>
        /// 解析位置消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseLocation(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("位置消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.位置;
            if (children.Count() != 3)
            {
                throw new Exception("位置消息解析失败");
            }
            if (children[0] is Button)
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
        /// <summary>
        /// 解析聊天记录消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseChatRecord(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("位置消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.聊天记录;
            if (children.Count() != 3)
            {
                throw new Exception("聊天记录消息解析失败");
            }
            if (children[0] is Button)
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
                throw new Exception("聊天记录消息解析失败");
            }
            bubble.ClickActionButton = button.AsButton();
            bubble.MessageContent = "群聊的聊天记录";
            return bubble;
        }
        /// <summary>
        /// 解析名片消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseCard(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("名片消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.个人名片;
            if (children.Count() != 3)
            {
                throw new Exception("名片消息解析失败");
            }
            if (children[0] is Button)
            {
                bubble.Sender = children[0].AsButton().Name;
                bubble.MessageSource = MessageSourceType.好友消息;
            }
            else
            {
                bubble.Sender = "我";
                bubble.MessageSource = MessageSourceType.自己发送消息;
            }
            var button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Button").AsButton();
            if (button == null)
            {
                throw new Exception("名片消息解析失败");
            }
            bubble.ClickActionButton = button;
            button = children[1].FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane[1]/Button").AsButton();
            if (button != null)
            {
                bubble.MessageContent = button.Name;
            }
            else
            {
                bubble.MessageContent = "个人名片";
            }

            return bubble;
        }
        /// <summary>
        /// 解析文件消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private Bubble _ParseFile(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("文件消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.文件;
            if (children.Count() != 3)
            {
                throw new Exception("文件消息解析失败");
            }
            if (children[0] is Button)
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
        /// <summary>
        /// 解析拍一拍消息
        /// </summary>
        /// <param name="listItem"></param>
        /// <returns></returns>
        private List<Bubble> _ParsePickUp(AutomationElement listItem)
        {
            var paneElement = listItem.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            if (paneElement == null)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var children = paneElement.FindAllChildren();
            var bubble = new Bubble();
            bubble.MessageType = MessageType.拍一拍;
            if (children.Count() != 3)
            {
                throw new Exception("拍一拍消息解析失败");
            }
            var items = children[1].FindAllByXPath("/Pane");
            var list = new List<Bubble>();
            foreach (var item in items)
            {
                var bubbleItem = new Bubble();
                bubbleItem.MessageType = MessageType.拍一拍;
                if (item.Name.StartsWith(WeChatConstant.MESSAGES_I))
                {
                    bubbleItem.MessageSource = MessageSourceType.自己发送消息;
                    bubbleItem.Sender = "我";
                }
                else
                {
                    bubbleItem.MessageSource = MessageSourceType.好友消息;
                    bubbleItem.Sender = Regex.Match(item.Name, @"^""([^""]*)""").Groups[1].Value;
                }
                var lable = item.FindFirstByXPath("/Pane/Text");
                if (lable != null)
                {
                    bubbleItem.MessageContent = lable.AsLabel().Name;
                }
                else
                {
                    bubbleItem.MessageContent = "拍一拍";
                }
                list.Add(bubbleItem);
            }
            return list;
        }
        /// <summary>
        /// 解析时间消息
        /// </summary>
        /// <param name="listItemChild"></param>
        /// <returns></returns>
        private Bubble _ParseTimeMessage(AutomationElement listItemChild)
        {
            var text = listItemChild.AsLabel();
            Bubble bubble = new Bubble();
            bubble.MessageType = MessageType.时间;
            bubble.MessageSource = MessageSourceType.系统消息;
            bubble.Sender = "系统消息";
            bubble.MessageContent = text.Name;
            return bubble;
        }
        /// <summary>
        /// 获取最后一个气泡
        /// </summary>
        /// <returns>最后一个气泡</returns>
        public Bubble GetLastBubble()
        {
            Bubble[] bubbles = GetBubbles().ToArray();
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
                lookMoreButton.AsButton().Invoke();  //可能要改成Click()
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
    }
}
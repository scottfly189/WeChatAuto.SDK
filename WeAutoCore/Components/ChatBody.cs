
using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;
using WxAutoCommon.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using WxAutoCommon.Configs;

namespace WxAutoCore.Components
{
    public class ChatBody
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private string _Title;
        private AutomationElement _ChatBodyRoot;
        private WeChatMainWindow _MainWxWindow;
        private UIThreadInvoker _uiThreadInvoker;
        public MessageBubbleList BubbleList => GetBubbleList();
        public Sender Sender => GetSender();
        private System.Threading.Timer _pollingTimer;
        private int _lastMessageCount = 0;
        private List<MessageBubble> _lastBubbles = new List<MessageBubble>();
        public ChatBody(Window window, AutomationElement chatBodyRoot, IWeChatWindow wxWindow, string title, UIThreadInvoker uiThreadInvoker, WeChatMainWindow mainWxWindow)
        {
            _Window = window;
            _ChatBodyRoot = chatBodyRoot;
            _WxWindow = wxWindow;
            _Title = title;
            _uiThreadInvoker = uiThreadInvoker;
            _MainWxWindow = mainWxWindow;
        }
        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack">回调函数,参数：新消息气泡<see cref="MessageBubble"/>,包含新消息气泡的列表<see cref="List{MessageBubble}"/>,当前窗口发送者<see cref="Sender"/>,当前微信窗口对象<see cref="WeChatMainWindow"/></param>
        public void AddListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork> callBack)
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListBox = _uiThreadInvoker.Run(automation =>
            {
                var listBox = _ChatBodyRoot.FindFirstByXPath(xPath);
                return listBox;
            }).Result;
            StartMessagePolling(callBack, bubbleListBox);
        }

        /// <summary>
        /// 启动消息轮询检测
        /// </summary>
        private void StartMessagePolling(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork> callBack, AutomationElement bubbleListRoot)
        {
            // 初始化消息数量和内容哈希
            (int count, List<MessageBubble> bubbles) = GetCurrentMessage();
            _lastMessageCount = count;
            _lastBubbles = bubbles;
            // 启动定时器
            _pollingTimer = new System.Threading.Timer(_ =>
            {
                try
                {
                    (int currentCount, List<MessageBubble> currentBubbles) = GetCurrentMessage();
                    if (currentCount != _lastMessageCount)
                    {
                        Trace.WriteLine($"检测到新消息(数量变化): 上次{_lastMessageCount}条，现在{currentCount}条");
                        System.Threading.Thread.Sleep(200); // 等待消息完全加载
                        ProcessNewMessages(callBack, currentBubbles);
                        _lastMessageCount = currentCount;
                        _lastBubbles = currentBubbles;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"轮询检测异常: {ex.Message}");
                }
            }, null, WeChatConfig.ListenInterval * 1000, WeChatConfig.ListenInterval * 1000);
        }

        /// <summary>
        /// 获取当前消息数量
        /// </summary>
        private (int count, List<MessageBubble> bubbles) GetCurrentMessage()
        {
            var bubbles = BubbleList.GetVisibleBubbles();
            _lastBubbles = bubbles;
            return (bubbles.Count, bubbles);
        }


        /// <summary>
        /// 处理新消息
        /// </summary>
        private void ProcessNewMessages(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork> callBack, List<MessageBubble> currentBubbles)
        {
            try
            {
                var lastHashList = _lastBubbles.Select(item => item.BubbleHash).ToList();
                var currentHashList = currentBubbles.Select(item => item.BubbleHash).ToList();
                var exceptList = currentHashList.Except(lastHashList).ToList();
                if (exceptList.Count > 0)
                {
                    List<MessageBubble> newBubbles = currentBubbles.Where(item => exceptList.Contains(item.BubbleHash)).ToList();
                    newBubbles.ForEach(item => { item.IsNew = true; item.MessageTime = DateTime.Now; });
                    callBack(newBubbles, currentBubbles, Sender, _MainWxWindow, _MainWxWindow.WeChatFramwork);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"处理新消息异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            _pollingTimer?.Dispose();
            _pollingTimer = null;
        }
        /// <summary>
        /// 获取聊天内容区可见气泡列表
        /// </summary>
        /// <returns>聊天内容区可见气泡列表<see cref="BubbleList"/></returns>
        public MessageBubbleList GetBubbleList()
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListRoot = _uiThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).Result;
            DrawHightlightHelper.DrawHightlight(bubbleListRoot, _uiThreadInvoker);
            MessageBubbleList bubbleList = new MessageBubbleList(_Window, bubbleListRoot, _WxWindow, _Title, _uiThreadInvoker);
            return bubbleList;
        }
        /// <summary>
        /// 获取聊天内容区所有气泡列表,如果消息没有显示全，则会滚动消息至最顶部，然后获取所有气泡标题
        /// 速度会比较快
        /// </summary>
        /// <returns>聊天内容区所有气泡列表,仅返回气泡标题</returns>
        public List<string> GetAllBubbleTitleList()
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var rList = _uiThreadInvoker.Run(automation =>
            {
                var listBox = _ChatBodyRoot.FindFirstByXPath(xPath);
                var list = new List<string>();
                Button moreButton = listBox.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("查看更多消息"))).AsButton();
                //显示全部消息
                while (moreButton != null)
                {
                    var pattern = listBox.Patterns.Scroll.Pattern;
                    if (pattern != null)
                    {
                        pattern.SetScrollPercent(0, 0);
                    }
                    moreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    moreButton.Click();
                    Thread.Sleep(600);
                    moreButton = listBox.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("查看更多消息"))).AsButton();
                }
                var listItems = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                foreach (var item in listItems)
                {
                    xPath = @"/Pane/Button";
                    var subButton = item.FindFirstByXPath(xPath);
                    if (subButton != null)
                    {
                        list.Add(item.Name ?? "未读取到气泡标题");
                    }
                }

                return list;
            }).Result;

            return rList;
        }
        /// <summary>
        /// 获取聊天内容区发送者
        /// </summary>
        /// <returns>聊天内容区发送者<see cref="Sender"/></returns>
        public Sender GetSender()
        {
            var xPath = "/Pane[2]";
            var senderRoot = _uiThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).Result;
            DrawHightlightHelper.DrawHightlight(senderRoot, _uiThreadInvoker);
            var sender = new Sender(_Window, senderRoot, _WxWindow, _Title, _uiThreadInvoker);
            return sender;
        }
    }
}
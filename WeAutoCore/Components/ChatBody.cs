
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
        public MessageBubbleList BubbleList => GetVisibleBubbleList();
        public Sender Sender => GetSender();
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
        public void AddListener(Action<MessageBubble, List<MessageBubble>, Sender, WeChatMainWindow> callBack)
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            _uiThreadInvoker.Run(automation =>
            {
                var bubbleListRoot = _ChatBodyRoot.FindFirstByXPath(xPath);

                // 方案1：使用轮询检测（推荐）
                StartMessagePolling(callBack, bubbleListRoot);

                // 方案2：保留原有事件监听作为补充
                bubbleListRoot.RegisterStructureChangedEvent(TreeScope.Descendants, (element, changeType, changeIds) =>
                {
                    Trace.WriteLine("StructureChanged - changeType:" + changeType.ToString());
                    Trace.WriteLine("StructureChanged - changeIds:" + changeIds.ToString());
                    Trace.WriteLine("StructureChanged - element:" + element.Name ?? "没有名字");

                    // 即使事件触发，也使用轮询来确保捕获到新消息
                    if (changeType == StructureChangeType.ChildAdded)
                    {
                        System.Threading.Thread.Sleep(100); // 等待元素完全构建
                        ProcessNewMessages(callBack);
                    }
                });
            });
        }

        private System.Threading.Timer _pollingTimer;
        private int _lastMessageCount = 0;
        private string _lastContentHash = "";

        /// <summary>
        /// 启动消息轮询检测
        /// </summary>
        private void StartMessagePolling(Action<MessageBubble, List<MessageBubble>, Sender, WeChatMainWindow> callBack, AutomationElement bubbleListRoot)
        {
            // 初始化消息数量和内容哈希
            _lastMessageCount = GetCurrentMessageCount();
            _lastContentHash = GetCurrentContentHash();

            // 启动定时器，每300ms检查一次
            _pollingTimer = new System.Threading.Timer(_ =>
            {
                try
                {
                    // 方法1：检查消息数量变化
                    int currentCount = GetCurrentMessageCount();
                    if (currentCount > _lastMessageCount)
                    {
                        Trace.WriteLine($"检测到新消息(数量变化): 上次{_lastMessageCount}条，现在{currentCount}条");
                        System.Threading.Thread.Sleep(200); // 等待消息完全加载
                        ProcessNewMessages(callBack);
                        _lastMessageCount = currentCount;
                        _lastContentHash = GetCurrentContentHash();
                        return;
                    }

                    // 方法2：检查内容哈希变化（更精确）
                    string currentHash = GetCurrentContentHash();
                    if (!string.IsNullOrEmpty(currentHash) && currentHash != _lastContentHash)
                    {
                        Trace.WriteLine("检测到新消息(内容变化)");
                        System.Threading.Thread.Sleep(100); // 等待消息完全加载
                        ProcessNewMessages(callBack);
                        _lastContentHash = currentHash;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"轮询检测异常: {ex.Message}");
                }
            }, null, 300, 300); // 300ms后开始，每300ms执行一次
        }

        /// <summary>
        /// 获取当前消息数量
        /// </summary>
        private int GetCurrentMessageCount()
        {
            try
            {
                var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
                var bubbleListRoot = _ChatBodyRoot.FindFirstByXPath(xPath);
                var listItems = bubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                return listItems.Length;
            }
            catch
            {
                return _lastMessageCount; // 出错时返回上次的值
            }
        }

        /// <summary>
        /// 获取当前内容哈希值（用于检测内容变化）
        /// </summary>
        private string GetCurrentContentHash()
        {
            try
            {
                var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
                var bubbleListRoot = _ChatBodyRoot.FindFirstByXPath(xPath);
                var listItems = bubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));

                // 只取最后几条消息的内容作为哈希依据，避免计算量过大
                var recentItems = listItems.Skip(Math.Max(0, listItems.Length - 5));
                var contentBuilder = new System.Text.StringBuilder();

                foreach (var item in recentItems)
                {
                    contentBuilder.Append(item.Name ?? "");
                    contentBuilder.Append("|");
                }

                // 计算哈希值
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contentBuilder.ToString()));
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch
            {
                return _lastContentHash; // 出错时返回上次的值
            }
        }

        /// <summary>
        /// 处理新消息
        /// </summary>
        private void ProcessNewMessages(Action<MessageBubble, List<MessageBubble>, Sender, WeChatMainWindow> callBack)
        {
            try
            {
                var bubbles = BubbleList.GetBubbles();
                // 过滤掉系统消息
                bubbles = bubbles.Where(item => item.MessageSource != WxAutoCommon.Enums.MessageSourceType.系统消息).ToList();
                if (bubbles.Count > 0)
                {
                    callBack(bubbles.Last(), bubbles, Sender, _MainWxWindow);
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
        public MessageBubbleList GetVisibleBubbleList()
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListRoot = _uiThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).Result;
            DrawHightlightHelper.DrawHightlight(bubbleListRoot, _uiThreadInvoker);
            MessageBubbleList bubbleList = new MessageBubbleList(_Window, bubbleListRoot, _WxWindow, _Title, _uiThreadInvoker);
            return bubbleList;
        }
        /// <summary>
        /// 获取聊天内容区所有气泡列表
        /// </summary>
        /// <returns>聊天内容区所有气泡列表,仅返回气泡标题</returns>
        public List<string> GetAllBubbleList()
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
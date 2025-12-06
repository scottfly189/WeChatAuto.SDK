using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Utils;
using WeChatAuto.Utils;
using WxAutoCommon.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using WxAutoCommon.Configs;
using WxAutoCommon.Enums;
using WeChatAuto.Services;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Models;
using WeChatAuto.Extentions;

namespace WeChatAuto.Components
{
    public class ChatBody : IDisposable
    {
        private readonly AutoLogger<ChatBody> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private string _Title;
        private AutomationElement _ChatBodyRoot;
        private WeChatMainWindow _MainWxWindow;
        private UIThreadInvoker _uiMainThreadInvoker;
        public MessageBubbleList BubbleListObject => GetBubbleListObject();
        public Sender Sender => GetSender();
        private System.Threading.Timer _pollingTimer;
        private int _lastMessageCount = 0;
        private List<MessageBubble> _lastBubbles = new List<MessageBubble>();
        private volatile bool _disposed = false;
        private CancellationTokenSource _pollingTimerCancellationTokenSource = new CancellationTokenSource();
        private volatile bool _isProcessing = false; // 标记是否正在处理消息
        private ChatType _ChatType;
        private ChatContent _ChatContent;
        public ChatContent ChatContent => _ChatContent;
        public ChatType ChatType => _ChatType;
        public ChatBody(Window window, AutomationElement chatBodyRoot, IWeChatWindow wxWindow, string title, ChatType chatType,
          UIThreadInvoker uiThreadInvoker, WeChatMainWindow mainWxWindow, IServiceProvider serviceProvider,ChatContent chatContent)
        {
            _Window = window;
            _logger = serviceProvider.GetRequiredService<AutoLogger<ChatBody>>();
            _ChatBodyRoot = chatBodyRoot;
            _WxWindow = wxWindow;
            _ChatContent = chatContent;
            _Title = title;
            _ChatType = chatType;
            _uiMainThreadInvoker = uiThreadInvoker;
            _MainWxWindow = mainWxWindow;
            _serviceProvider = serviceProvider;
            _logger.Info($"本次ChatBody的窗口名称:{_Window.Name},ProcessId:{_WxWindow.ProcessId},运行线程名称:{uiThreadInvoker.ThreadName}");
        }
        /// <summary>
        /// 添加消息监听
        /// 注意：消息回调函数会在新线程中执行，请注意线程安全，如果在回调函数中操作UI，请切换到UI线程.
        /// </summary>
        /// <param name="callBack">回调函数,参数：消息上下文<see cref="MessageContext"/></param>
        public void AddListener(Action<MessageContext> callBack)
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListBox = _uiMainThreadInvoker.Run(automation =>
            {
                var listBox = _ChatBodyRoot.FindFirstByXPath(xPath);
                return listBox;
            }).GetAwaiter().GetResult();
            StartMessagePolling(callBack, bubbleListBox);
        }

        /// <summary>
        /// 启动消息轮询检测
        /// </summary>
        private void StartMessagePolling(Action<MessageContext> callBack, AutomationElement bubbleListRoot)
        {
            // 初始化消息数量和内容哈希
            (int count, List<MessageBubble> bubbles) = GetCurrentMessage();
            _lastMessageCount = count;
            _lastBubbles = bubbles;
            // 启动定时器
            _pollingTimer = new System.Threading.Timer(_ =>
            {
                if (_disposed)
                {
                    return;
                }
                try
                {
                    if (_pollingTimerCancellationTokenSource.Token.IsCancellationRequested)
                        return;
                    // 如果正在处理中，跳过本次执行
                    if (_isProcessing)
                    {
                        _logger.Trace("上一次消息处理尚未完成，跳过本次检测");
                        return;
                    }

                    _isProcessing = true; // 标记开始处理
                    if (!_subWinIsOpen())
                    {
                        _logger.Trace("子窗口未打开，跳过本次检测");
                        return;
                    }
                    (int currentCount, List<MessageBubble> currentBubbles) = GetCurrentMessage();
                    if (currentCount != _lastMessageCount || !_CompareBabbleHash(currentBubbles, _lastBubbles))
                    {
                        System.Threading.Thread.Sleep(300); // 等待消息完全加载
                        //WillDo: 处理新消息
                        //ProcessNewMessages(callBack, currentBubbles);
                        _lastMessageCount = currentCount;
                        _lastBubbles = currentBubbles;
                        return;
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.Info("轮询检测线程已停止，正常取消,不做处理");
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Trace($"轮询检测异常: {ex.Message}");
                }
                finally
                {
                    _isProcessing = false; // 标记处理完成
                }
            }, null, WeAutomation.Config.ListenInterval * 1000, WeAutomation.Config.ListenInterval * 1000);
        }
        /// <summary>
        /// 比较两个气泡列表的哈希值是否相同
        /// </summary>
        /// <param name="currentBubbles">当前气泡列表</param>
        /// <param name="lastBubbles">上次气泡列表</param>
        /// <returns>是否相同</returns>
        private bool _CompareBabbleHash(List<MessageBubble> currentBubbles, List<MessageBubble> lastBubbles)
        {
            var currentHashList = currentBubbles.Skip(Math.Max(0, currentBubbles.Count - 5)).Select(item => item.BubbleHash).ToList();
            var lastHashList = lastBubbles.Skip(Math.Max(0, lastBubbles.Count - 5)).Select(item => item.BubbleHash).ToList();
            return currentHashList.SequenceEqual(lastHashList);
        }

        /// <summary>
        /// 获取当前消息数量
        /// </summary>
        private (int count, List<MessageBubble> bubbles) GetCurrentMessage()
        {
            try
            {
                var bubbles = BubbleListObject.GetVisibleBubbles();
                return (bubbles.Count, bubbles);
            }
            catch (Exception ex)
            {
                _logger.Trace($"获取当前消息异常: {ex.Message}");
                return (_lastMessageCount, _lastBubbles);
            }
        }
        /// <summary>
        /// 处理新消息
        /// </summary>
        private void ProcessNewMessages(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider> callBack, List<MessageBubble> currentBubbles)
        {
            try
            {
                var lastFriendMessageList = GetFirendMessageList(_lastBubbles);
                var currentFriendMessageList = GetFirendMessageList(currentBubbles);
                List<MessageBubble> newBubbles = new List<MessageBubble>();
                if (lastFriendMessageList.Count <= 3)
                {
                    newBubbles = currentFriendMessageList.Skip(lastFriendMessageList.Count).ToList();
                }
                else
                {
                    var currentCompareList = currentFriendMessageList.Take(3).Select(item => item.MessageContent).ToList();
                    var index = 0;
                    for (int i = 0; i < lastFriendMessageList.Count; i++)
                    {
                        var tempBubbles = lastFriendMessageList.Skip(i).Take(3).Select(item => item.MessageContent).ToList();
                        if (tempBubbles.SequenceEqual(currentCompareList))
                        {
                            index = i;
                            break;
                        }
                    }
                    var skip = lastFriendMessageList.Count - index;
                    newBubbles = currentFriendMessageList.Skip(skip).ToList();
                }
                if (newBubbles.Count > 0)
                {
                    callBack(newBubbles, currentBubbles, Sender, _MainWxWindow, _MainWxWindow.weChatClientFactory, _serviceProvider);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace($"处理新消息异常: {ex.Message}");
            }
        }

        private bool _subWinIsOpen()
        {
            var subWinIsOpen = _uiMainThreadInvoker.Run(automation =>
            {
                try
                {
                    var desktop = automation.GetDesktop();
                    var isOpen = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                            .And(cf.ByControlType(ControlType.Window)
                            .And(cf.ByProcessId(_WxWindow.ProcessId))
                            .And(cf.ByName(_Title)))),
                            timeout: TimeSpan.FromSeconds(5),
                            interval: TimeSpan.FromMilliseconds(200));
                    return isOpen.Success && isOpen.Result != null;
                }
                catch (Exception ex)
                {
                    _logger.Trace($"判断子窗口是否打开异常: {ex.Message}");
                    return false;
                }
            }).GetAwaiter().GetResult();

            return subWinIsOpen;
        }

        private List<MessageBubble> GetFirendMessageList(List<MessageBubble> bubbles)
        {
            return bubbles.Where(item => item.MessageSource != MessageSourceType.系统消息 &&
                                               item.MessageSource != MessageSourceType.其他消息 &&
                                               item.MessageSource != MessageSourceType.自己发送消息).ToList();
        }

        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            if (_disposed)
            {
                return;
            }
            _isProcessing = true;
            _pollingTimer?.Dispose();
            _pollingTimer = null;
            _isProcessing = false; // 重置处理标志
        }
        /// <summary>
        /// 获取聊天内容区可见气泡列表
        /// </summary>
        /// <returns>聊天内容区可见气泡列表对象<see cref="MessageBubbleList"/></returns>
        public MessageBubbleList GetBubbleListObject()
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListRoot = _uiMainThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).GetAwaiter().GetResult();
            MessageBubbleList bubbleListObject = new MessageBubbleList(_Window, bubbleListRoot, _WxWindow, _Title, _uiMainThreadInvoker,this, _serviceProvider);
            return bubbleListObject;
        }
        /// <summary>
        /// 获取聊天内容区所有气泡列表,如果消息没有显示全，则会滚动消息至最顶部，然后获取所有气泡标题
        /// 速度会比较快
        /// </summary>
        /// <returns>聊天内容区所有气泡列表,仅返回气泡标题</returns>
        public List<ChatSimpleMessage> GetAllChatHistory()
        {
            if (_ChatType != ChatType.好友 && _ChatType != ChatType.群聊)
            {
                _logger.Warn($"聊天类型为{_ChatType.ToString()}，不支持获取聊天内容区所有气泡列表");
                return new List<ChatSimpleMessage>();
            }
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var rList = _uiMainThreadInvoker.Run(automation =>
            {
                var listBox = _ChatBodyRoot.FindFirstByXPath(xPath);
                var list = new List<ChatSimpleMessage>();
                Button moreButton = listBox.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("查看更多消息"))).AsButton();
                //显示全部消息
                while (moreButton != null)
                {
                    var pattern = listBox.Patterns.Scroll.Pattern;
                    if (pattern != null)
                    {
                        pattern.SetScrollPercent(0, 0);
                    }
                    RandomWait.Wait(300, 1000);
                    moreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    moreButton.Click();
                    RandomWait.Wait(100, 3000);
                    moreButton = listBox.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("查看更多消息"))).AsButton();
                }
                var listItems = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                foreach (var item in listItems)
                {
                    xPath = @"/Pane/Button";
                    var subButton = item.FindFirstByXPath(xPath);
                    if (subButton != null)
                    {
                        if (string.IsNullOrWhiteSpace(item.Name))
                        {
                            continue;
                        }
                        string who = subButton.Name;
                        if (_ChatType == ChatType.群聊)
                        {
                            var subButtons = item.FindAllByXPath("/Pane[1]/*");
                            if (subButtons.Length == 3)
                            {
                                if (subButtons[0].ControlType == ControlType.Button)
                                {
                                    var pane = subButtons[0].GetSibling(1);
                                    if (pane != null && pane.ControlType == ControlType.Pane)
                                    {
                                        who = pane.FindFirstByXPath(@"//Text")?.Name;
                                    }
                                }
                            }
                        }
                        list.Add(new ChatSimpleMessage { Who = subButton.Name == _MainWxWindow.NickName ? "我" : who, Message = item.Name });
                    }
                }

                return list;
            }).GetAwaiter().GetResult();

            return rList;
        }
        /// <summary>
        /// 获取聊天内容区发送者
        /// </summary>
        /// <returns>聊天内容区发送者<see cref="Sender"/></returns>
        public Sender GetSender()
        {
            var xPath = "/Pane[2]";
            var senderRoot = _uiMainThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).GetAwaiter().GetResult();
            DrawHightlightHelper.DrawHightlight(senderRoot, _uiMainThreadInvoker);
            var sender = new Sender(_Window, senderRoot, _WxWindow, _Title, _uiMainThreadInvoker, _serviceProvider);
            return sender;
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _pollingTimerCancellationTokenSource?.Cancel();
                _isProcessing = true;
                _pollingTimer?.Dispose();
                _pollingTimer = null;
                _isProcessing = false; // 重置处理标志
                _pollingTimerCancellationTokenSource?.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ChatBody()
        {
            Dispose(false);
        }

    }
}
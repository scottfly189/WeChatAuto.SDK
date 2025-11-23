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
using FlaUI.Core.Tools;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

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
                var bubble = _ParseBubble(listItemList[i], ref dateTime);
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
        /// 收藏消息
        /// </summary>
        /// <param name="chatSimpleMessage">要收藏的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void CollectMessage(ChatSimpleMessage chatSimpleMessage, int prevPageCount = 3)
        {
            _uiThreadInvoker.Run(automation =>
            {
                _PopupContextMenuCore(chatSimpleMessage, _CollectMessageCore, prevPageCount);
            })
            .GetAwaiter().GetResult();
        }
        /// <summary>
        /// 收藏指定的消息
        /// 注意，只能收藏有的消息，不会翻页，如果消息不在当前页，则不会收藏
        /// </summary>
        /// <param name="lastRowIndex">要收藏的消息的索引</param>
        public void CollectMessage(int lastRowIndex)
        {
            _uiThreadInvoker.Run(automation =>
            {
                //首先定位并找到此消息
                var listItems = _GetListItems();
                listItems.Reverse();
                listItems = _FilterSystemMessage(listItems);
                var item = listItems.ElementAt(lastRowIndex - 1).AsListBoxItem();
                if (item == null)
                {
                    _logger.Error($"找不到消息：index={lastRowIndex}，停止转发");
                    return;
                }
                _PopupIndexContextMenuCore(item, lastRowIndex, _CollectMessageCore);
            })
            .GetAwaiter().GetResult();
        }
        /// <summary>
        /// 拍一拍
        /// 注意：此动作仅适用于群聊中,并且只能拍别人，不适用于单聊
        /// </summary>
        /// <param name="who">要拍一拍的好友昵称</param>
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void TapWho(string who, int prevPageCount = 3)
        {
            _uiThreadInvoker.Run(automation =>
            {
                _PopupWhoMenuCore(who, _TapWhoCore, prevPageCount);
            })
            .GetAwaiter().GetResult();
        }
        private void _PopupWhoMenuCore(string who, Action<Menu> action, int prevPageCount = 3)
        {
            var listItem = _LocateWhoMessage(who, prevPageCount);
            if (listItem == null)
            {
                _logger.Error($"找不到消息：who={who}，停止拍一拍");
                return;
            }
            Menu menu = _GetPopupWhoMenu(listItem);
            if (menu == null)
            {
                _logger.Error($"找不到菜单：who={who}，停止拍一拍");
                return;
            }
            action(menu);
        }
        private ListBoxItem _LocateWhoMessage(string who, int prevPageCount)
        {
            int index = 0; //向前翻页的索引
            ListBoxItem result = null;
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            while (index < prevPageCount)
            {
                var listItems = _GetListItems();
                listItems.Reverse();
                var items = listItems.ToList();
                foreach (var item in items)
                {
                    var subItems = item.FindAllByXPath("/Pane[1]/*");
                    if (subItems != null && subItems.Length == 3)
                    {
                        ListBoxItem subItem = _SameWhoAndMove_(item.AsListBoxItem(), who);
                        if (subItem != null)
                        {
                            result = subItem;
                            break;
                        }
                    }
                }
                RandomWait.Wait(100, 800);
                if (result != null)
                {
                    break;
                }

                //往上翻页
                var (flowControl, nextIndex) = _PrevPageSearchWho(who, index);
                if (!flowControl)
                {
                    break;
                }
                index = nextIndex;
                _logger.Trace($"往上翻页{index}次，继续查找消息");
            }
            if (index >= prevPageCount)
            {
                _logger.Error($"往上翻页{index}次，仍然找不到消息，停止查找");
            }
            return result;
        }
        private ListBoxItem _SameWhoAndMove_(ListBoxItem selectItem, string who)
        {
            var subItems = selectItem.FindAllByXPath("/Pane[1]/*");
            var button = subItems.FirstOrDefault(cf => cf.ControlType == ControlType.Button);
            if (button == null)
                return null;
            var seachWho = button.Name;
            if (_ChatBody.ChatType == ChatType.群聊)
            {
                if (subItems[0].ControlType == ControlType.Button)
                {
                    var pane = subItems[0].GetSibling(1);
                    if (pane != null && pane.ControlType == ControlType.Pane)
                    {
                        seachWho = pane.FindFirstByXPath(@"//Text")?.Name;
                    }
                }
            }
            else
            {
                _logger.Error($"不是群聊，无法拍一拍");
                return null;
            }
            if (seachWho == who)
            {
                var baseRect = _BubbleListRoot.BoundingRectangle;
                var listItems = _GetListItems();
                listItems.Reverse();
                var foundItem = listItems.FirstOrDefault(u => u.Name == selectItem.Name && u.Properties.RuntimeId.Value.SequenceEqual(selectItem.Properties.RuntimeId.Value))?.AsListBoxItem();
                _logger.Trace($"foundItem的RuntimeId：{string.Join("-", foundItem.Properties.RuntimeId.Value)}");
                while (foundItem != null && foundItem.BoundingRectangle.Top < baseRect.Top)
                {
                    //调整位置
                    foundItem = _FindAndLocation_(ref foundItem);
                }

                foundItem?.DrawHighlightExt();
                return selectItem;
            }
            return null;
        }

        private Menu _GetPopupWhoMenu(ListBoxItem listItem)
        {
            RandomWait.Wait(100, 800);
            //点击右键
            var subItems = listItem.FindAllByXPath("/Pane[1]/*");
            var button = subItems[0];
            if (button != null && button.ControlType == ControlType.Button)
            {
                button.DrawHighlightExt();
                button.WaitUntilClickable(TimeSpan.FromSeconds(5));
                button.RightClick();
                RandomWait.Wait(100, 1500);
                var menu = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMilliseconds(200));
                if (menu.Success)
                {
                    menu.Result.DrawHighlightExt();
                    return menu.Result;
                }
                else
                {
                    _logger.Error($"找不到菜单");
                }
            }
            else
            {
                _logger.Error($"找不到第一个位置是Button的元素");
            }
            return null;
        }

        private void _TapWhoCore(Menu menu)
        {
            var menuItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("拍一拍")));
            if (menuItem != null)
            {
                menuItem.DrawHighlightExt();
                menuItem.WaitUntilClickable(TimeSpan.FromSeconds(5));
                menuItem.ClickEnhance(_SelfWindow);
                RandomWait.Wait(100, 800);
            }
            else
            {
                _logger.Error($"找不到拍一拍菜单项，停止拍一拍");
                return;
            }
        }
        /// <summary>
        /// 收藏消息
        /// </summary>
        /// <param name="who">要收藏的好友昵称</param>
        /// <param name="message">要收藏的消息内容</param>
        public void CollectMessage(string who, string message, int prevPageCount = 3)
          => CollectMessage(new ChatSimpleMessage { Who = who, Message = message }, prevPageCount);
        /// <summary>
        /// 收藏消息核心
        /// </summary>
        /// <param name="menu">菜单<see cref="Menu"/></param>
        private void _CollectMessageCore(Menu menu)
        {
            var menuItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("收藏")));
            if (menuItem != null)
            {
                menuItem.DrawHighlightExt();
                menuItem.WaitUntilClickable(TimeSpan.FromSeconds(5));
                menuItem.ClickEnhance(_SelfWindow);
                RandomWait.Wait(100, 800);
            }
            else
            {
                _logger.Error($"找不到收藏菜单项，停止收藏");
            }
        }
        /// <summary>
        /// 引用消息
        /// </summary>
        /// <param name="chatSimpleMessage">要引用的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void ReferencedMessage(ChatSimpleMessage chatSimpleMessage, int prevPageCount = 3)
        {
            _uiThreadInvoker.Run(automation =>
            {
                _PopupContextMenuCore(chatSimpleMessage, _ReferencedMessageCore, prevPageCount);
            })
            .GetAwaiter().GetResult();
        }
        /// <summary>
        /// 引用最后一条消息
        /// 注意，只能引用有的消息，不会翻页，如果消息不在当前页，则不会引用
        /// </summary>
        /// <param name="lastRowIndex">最后一条消息的索引</param>
        public void ReferencedMessage(int lastRowIndex)
        {
            _uiThreadInvoker.Run(automation =>
            {
                //首先定位并找到此消息
                var listItems = _GetListItems();
                listItems.Reverse();
                listItems = _FilterSystemMessage(listItems);
                var item = listItems.ElementAt(lastRowIndex - 1).AsListBoxItem();
                if (item == null)
                {
                    _logger.Error($"找不到消息：index={lastRowIndex}，停止转发");
                    return;
                }
                _PopupIndexContextMenuCore(item, lastRowIndex, _ReferencedMessageCore);

            })
            .GetAwaiter().GetResult();
        }
        /// <summary>
        /// 引用消息
        /// </summary>
        /// <param name="who">要引用的好友昵称</param>
        /// <param name="message">要引用的消息内容</param>
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void ReferencedMessage(string who, string message, int prevPageCount = 3)
          => ReferencedMessage(new ChatSimpleMessage { Who = who, Message = message }, prevPageCount);
        private void _ReferencedMessageCore(Menu menu)
        {
            var menuItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("引用")));
            if (menuItem != null)
            {
                menuItem.DrawHighlightExt();
                menuItem.WaitUntilClickable(TimeSpan.FromSeconds(5));
                menuItem.ClickEnhance(_SelfWindow);
                RandomWait.Wait(100, 800);
            }
            else
            {
                _logger.Error($"找不到引用菜单项，停止引用");
            }
        }
        /// <summary>
        /// 转发多条消息,默认转发最后5条消息，可以自行指定转发多少条消息
        /// 注意：
        /// 转发会做如下预处理：
        /// 1、图片，会自动测试是否能够转发，直到能转发为止;
        /// 2、视频，会自动下载，并且测试是否能够转发，直到能转发为止
        /// 3、语单，会自行语音转文字
        /// </summary>
        /// <param name="to">要转发给谁</param>
        /// <param name="isCapture">是否要转发的内容进行截图，默认是true</param>
        /// <param name="rowCount">要转发多少条消息，默认是最后的5条消息,如果当前没有十条，则转发所有消息</param>
        public void ForwardMultipleMessage(string to, bool isCapture = true, int rowCount = 5)
        {
            _uiThreadInvoker.Run(automation =>
            {
                List<ListBoxItem> _WillProcessItems = _GetWillForwardMessageList(rowCount);  //得到所有要转发的消息
                _PreImageVedioMessage(_WillProcessItems);      //前置操作，如果有图片、视频、语音，则先处理
                _SelectMultipleMessage(_WillProcessItems);  //选择要转发多少条消息
                _ForwardMessageCore(to);  //转发消息
                _ProcessMaybeError();
                // if (isCapture)
                // {
                //     _CaptureMultipleMessage(_WillProcessItems, to);
                // }
            })
            .GetAwaiter().GetResult();
        }

        private void _ProcessMaybeError()
        {
            var alertWin = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("AlertDialog")).And(cf.ByProcessId(_SelfWindow.Properties.ProcessId))),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromMilliseconds(200));
            if (alertWin.Success)
            {
                RandomWait.Wait(100, 1500);
                alertWin.Result.DrawHighlightExt();
                alertWin.Result.AsWindow().Close();
            }
            else
            {
                _logger.Info($"没有找到可能的错误弹窗,正常退出");
            }
        }

        private void _CaptureMultipleMessage(List<ListBoxItem> selectItems, string to)
        {
            throw new NotImplementedException();
        }
        //前置处理图片、视频、语音消息
        private void _PreImageVedioMessage(List<ListBoxItem> selectItems)
        {
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            foreach (ListBoxItem item in selectItems)
            {
                _LocateSingleMessageAndProcess(item, _TestImageVedioCore);
            }
        }
        //定位消息并处理
        private void _LocateSingleMessageAndProcess(ListBoxItem item, Action<ListBoxItem> action)
        {
            var newItem = _GetItemNewestVersion_(item);
            if (newItem == null)
            {
                _logger.Error($"找不到消息：{item.Name}，停止处理");
                return;
            }
            newItem = _FindAndLocation_(ref newItem);
            if (newItem != null)
            {
                action(newItem);
            }
            else
            {
                _logger.Error($"消息：{item.Name}，怎么调整都不在屏幕上，停止处理");
            }
        }
        private CheckBox _FindAndLocationCheckItem_(ref CheckBox item)
        {
            Rectangle rect = new Rectangle(item.BoundingRectangle.X, item.BoundingRectangle.Y, item.BoundingRectangle.Width, item.BoundingRectangle.Height < 100 ? item.BoundingRectangle.Height : 100); ;
            if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]"))
            {
                rect = item.BoundingRectangle;
            }

            while (rect.Top < _BubbleListRoot.BoundingRectangle.Top || rect.Bottom > _BubbleListRoot.BoundingRectangle.Bottom)
            {
                if (_BubbleListRoot.Patterns.Scroll.IsSupported)
                {
                    var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                    if (pattern != null && pattern.VerticallyScrollable)
                    {
                        if (rect.Top < _BubbleListRoot.BoundingRectangle.Top)
                        {
                            _logger.Trace($"消息被遮挡，往上滚动");
                            pattern.SetScrollPercent(0, System.Math.Max(pattern.VerticalScrollPercent - pattern.VerticalViewSize, 0));
                        }
                        if (rect.Bottom > _BubbleListRoot.BoundingRectangle.Bottom)
                        {
                            _logger.Trace($"消息被遮挡，往下滚动");
                            pattern.SetScrollPercent(0, System.Math.Min(pattern.VerticalScrollPercent + pattern.VerticalViewSize / 3, 1));
                        }
                    }
                }

                RandomWait.Wait(100, 800);
                item = _GetCheckBoxItemNewestVersion_(item);
                rect = new Rectangle(item.BoundingRectangle.X, item.BoundingRectangle.Y, item.BoundingRectangle.Width, item.BoundingRectangle.Height < 100 ? item.BoundingRectangle.Height : 100); ;
                if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]"))
                {
                    rect = item.BoundingRectangle;
                }
            }
            return item;
        }
        //找到并定位消息,适用于长短文本消息
        private ListBoxItem _FindAndLocation_(ref ListBoxItem item)
        {
            Rectangle rect = new Rectangle(item.BoundingRectangle.X, item.BoundingRectangle.Y, item.BoundingRectangle.Width, item.BoundingRectangle.Height < 100 ? item.BoundingRectangle.Height : 100); ;
            if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]"))
            {
                rect = item.BoundingRectangle;
            }

            while (rect.Top < _BubbleListRoot.BoundingRectangle.Top || rect.Bottom > _BubbleListRoot.BoundingRectangle.Bottom)
            {
                if (_BubbleListRoot.Patterns.Scroll.IsSupported)
                {
                    var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                    if (pattern != null && pattern.VerticallyScrollable)
                    {
                        if (rect.Top < _BubbleListRoot.BoundingRectangle.Top)
                        {
                            _logger.Trace($"消息被遮挡，往上滚动");
                            pattern.SetScrollPercent(0, System.Math.Max(pattern.VerticalScrollPercent - pattern.VerticalViewSize, 0));
                        }
                        if (rect.Bottom > _BubbleListRoot.BoundingRectangle.Bottom)
                        {
                            _logger.Trace($"消息被遮挡，往下滚动");
                            pattern.SetScrollPercent(0, System.Math.Min(pattern.VerticalScrollPercent + pattern.VerticalViewSize / 3, 1));
                        }
                    }
                }

                RandomWait.Wait(100, 800);
                item = _GetItemNewestVersion_(item);
                rect = new Rectangle(item.BoundingRectangle.X, item.BoundingRectangle.Y, item.BoundingRectangle.Width, item.BoundingRectangle.Height < 100 ? item.BoundingRectangle.Height : 100); ;
                if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]"))
                {
                    rect = item.BoundingRectangle;
                }
            }
            return item;
        }
        //获取这条消息的最新版本
        private ListBoxItem _GetItemNewestVersion_(ListBoxItem item)
        {
            var newListItems = _GetListItems();
            var newItem = newListItems.FirstOrDefault(u => u.Name == item.Name && u.Properties.RuntimeId.Value.SequenceEqual(item.Properties.RuntimeId.Value))?.AsListBoxItem();
            return newItem;
        }
        private CheckBox _GetCheckBoxItemNewestVersion_(CheckBox item)
        {
            var newListItems = _GetCheckListItems();
            var newItem = newListItems.FirstOrDefault(u => u.Name == item.Name && u.Properties.RuntimeId.Value.SequenceEqual(item.Properties.RuntimeId.Value))?.AsCheckBox();
            return newItem;
        }
        //测试图片、视频、语音消息是否能够转发
        private void _TestImageVedioCore(ListBoxItem item)
        {
            if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]") || item.Name.Contains("[语音]"))
            {
                RandomWait.Wait(100, 800);
                //点击右键
                var subItems = item.FindAllByXPath("/Pane[1]/*");
                var pane = subItems[1];
                if (pane != null && pane.ControlType == ControlType.Pane)
                {
                    var xPath = "//Button";
                    var isReferenced = item.Name.Contains("\n引用  ");
                    var button = pane.FindFirstByXPath(xPath)?.AsButton();
                    if (button != null && !isReferenced)
                    {
                        button.GetParent().DrawHighlightExt();
                        button.GetParent().WaitUntilClickable(TimeSpan.FromSeconds(5));
                        button.GetParent().RightClick();
                        _ImageVedioPreProcess(button, item);
                        _SwtichFocus(item);
                    }
                }
            }
        }
        //得到所有要转发的消息
        //这里要注意处理将系统消息排除掉
        private List<ListBoxItem> _GetWillForwardMessageList(int rowCount)
        {
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            var listItems = _GetListItems();
            listItems = _FilterSystemMessage(listItems);
            listItems.Reverse();
            if (listItems.Count() > rowCount)
            {
                listItems = listItems.Take(rowCount).ToList();
            }
            return listItems.Select(item => item.AsListBoxItem()).ToList();
        }
        //过滤系统消息
        private List<AutomationElement> _FilterSystemMessage(List<AutomationElement> listItems)
        {
            List<AutomationElement> result = new List<AutomationElement>();
            foreach (var item in listItems)
            {
                var xPath = "/Pane[1]/*";
                var children = item.FindAllByXPath(xPath);
                if (children.Length == 3)
                {
                    if (children[1].ControlType == ControlType.Pane && (children[0].ControlType == ControlType.Button || children[2].ControlType == ControlType.Button))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        private void _SelectMultipleMessage(List<ListBoxItem> _SelectItems)
        {
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            this._PopupMultipleForwardMenuCore(_SelectItems);
            var menu = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
              TimeSpan.FromSeconds(3),
              TimeSpan.FromMilliseconds(200));
            if (menu.Success)
            {
                menu.Result.DrawHighlightExt();
                var menuItem = menu.Result.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("多选")));
                if (menuItem != null)
                {
                    menuItem.DrawHighlightExt();
                    menuItem.WaitUntilClickable(TimeSpan.FromSeconds(3));
                    menuItem.ClickEnhance(_SelfWindow);
                    RandomWait.Wait(100, 800);
                }
                else
                {
                    _logger.Error($"找不到多选菜单项");
                    return;
                }
            }
            else
            {
                _logger.Error($"找不到菜单");
            }
            foreach (ListBoxItem item in _SelectItems)
            {
                _LocateMessageAndSelect(item);
            }
            this._ConfirmMultipleForwardCore();
        }

        private void _LocateMessageAndSelect(ListBoxItem item)
        {
            var checkItems = _GetCheckListItems();
            var checkBox = checkItems.FirstOrDefault(u => u.Properties.RuntimeId.Value.SequenceEqual(item.Properties.RuntimeId.Value))?.AsCheckBox();
            checkBox = _FindAndLocationCheckItem_(ref checkBox);
            if (checkBox != null)
            {
                if (checkBox.ToggleState != ToggleState.On)
                {
                    checkBox.DrawHighlightExt();
                    _ClickAutomationMessageEnhance(checkBox);
                }
                else
                {
                    _logger.Error($"消息：{item.Name}，已选择，停止选择");
                }
            }
            else
            {
                _logger.Error($"找不到消息：{item.Name}，停止选择");
            }
        }

        //弹出多条转发菜单
        private void _PopupMultipleForwardMenuCore(List<ListBoxItem> _SelectItems)
        {
            var firstItem = _SelectItems.FirstOrDefault();
            if (firstItem != null)
            {
                firstItem = _FindAndLocation_(ref firstItem);
                if (firstItem != null)
                {
                    _RightClickAutomationMessageEnhance(firstItem, _SelfWindow, _BubbleListRoot);
                    RandomWait.Wait(500, 1500);
                }
            }
        }
        //确认多条转发
        private void _ConfirmMultipleForwardCore()
        {
            var pane = _BubbleListRoot.GetParent().GetParent().GetSibling(1);
            if (pane != null && pane.ControlType == ControlType.Pane)
            {
                pane.DrawHighlightExt();
                var button = pane.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("合并转发")));
                if (button != null)
                {
                    button.DrawHighlightExt();
                    button.WaitUntilClickable(TimeSpan.FromSeconds(3));
                    button.ClickEnhance(_SelfWindow);
                    RandomWait.Wait(100, 800);
                }
                else
                {
                    _logger.Error($"找不到合并转发按钮");
                }
            }
            else
            {
                _logger.Error($"找不到转发Pane");
            }
        }


        /// <summary>
        /// 自动化右键单击增强
        /// 最主要修复窗口中长消息无法右键的问题
        /// </summary>
        public void _RightClickAutomationMessageEnhance(ListBoxItem element, Window window, AutomationElement parentElement)
        {
            if (element.BoundingRectangle.Bottom <= parentElement.BoundingRectangle.Bottom)
            {
                _GetPopupMenuShortMessageContent(element);
            }
            else
            {
                // var rect = new Rectangle(element.BoundingRectangle.X, element.BoundingRectangle.Y, element.BoundingRectangle.Width, element.BoundingRectangle.Height < 100 ? element.BoundingRectangle.Height : 100);
                // var centerPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + 10);
                // window.Focus();
                // Mouse.Position = centerPoint;
                // RandomWait.Wait(100, 500);
                // Mouse.RightClick();

                var rect = new Rectangle(element.BoundingRectangle.X, element.BoundingRectangle.Y, element.BoundingRectangle.Width, element.BoundingRectangle.Height < 100 ? element.BoundingRectangle.Height : 100);
                var centerPoint = new Point(rect.X + 120, rect.Y + 50);
                _SelfWindow.Focus();
                Mouse.Position = centerPoint;
                RandomWait.Wait(100, 500);
                Mouse.RightClick();
                RandomWait.Wait(500, 1000);
            }
        }

        public void _ClickAutomationMessageEnhance(CheckBox element)
        {
            if (element.BoundingRectangle.Bottom <= _BubbleListRoot.BoundingRectangle.Bottom)
            {
                element.ClickEnhance(_SelfWindow);
            }
            else
            {
                var rect = new Rectangle(element.BoundingRectangle.X, element.BoundingRectangle.Y, element.BoundingRectangle.Width, element.BoundingRectangle.Height < 100 ? element.BoundingRectangle.Height : 100);
                var centerPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + 10);
                _SelfWindow.Focus();
                Mouse.Position = centerPoint;
                RandomWait.Wait(100, 500);
                Mouse.LeftClick();
            }
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
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void ForwardSingleMessage(ChatSimpleMessage chatSimpleMessage, string to, int prevPageCount = 3)
        {
            _uiThreadInvoker.Run(automation =>
            {
                _PopupContextMenuCore(chatSimpleMessage, (menu) => _ForwardSingleMessageCore(menu, to), prevPageCount);
            })
            .GetAwaiter().GetResult();
        }
        /// <summary>
        /// 转发最后的第index条消息,1表示最后一条消息，2表示倒数第二条消息
        /// 注意，只能转发有的消息，不会翻页，如果消息不在当前页，则不会转发
        /// </summary>
        /// <param name="lastRowIndex">最后一条消息的索引</param>
        /// <param name="to"></param>
        public void ForwardSingleMessage(int lastRowIndex, string to)
        {
            _uiThreadInvoker.Run(automation =>
            {
                //首先定位并找到此消息
                var listItems = _GetListItems();
                listItems.Reverse();
                listItems = _FilterSystemMessage(listItems);
                var item = listItems.ElementAtOrDefault(lastRowIndex - 1)?.AsListBoxItem();
                if (item == null)
                {
                    _logger.Error($"找不到消息：index={lastRowIndex}，停止转发");
                    return;
                }
                _PopupIndexContextMenuCore(item, lastRowIndex, (menu) => _ForwardSingleMessageCore(menu, to));
            })
            .GetAwaiter().GetResult();
        }
        private string _GetWhoFromListItem_(ListBoxItem item)
        {
            var subItems = item.FindAllByXPath("/Pane[1]/*");
            if (subItems != null && subItems.Length == 3)
            {
                var button = subItems.FirstOrDefault(cf => cf.ControlType == ControlType.Button);
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

                return who;
            }
            return null;
        }
        /// <summary>
        /// 转发单条消息
        /// </summary>
        /// <param name="who">要转发的好友昵称</param>
        /// <param name="message">要转发的消息内容</param>
        /// <param name="to">要转发给谁</param>
        /// <param name="prevPageCount">如果当前页找不到，往前翻页的次数</param>
        public void ForwardSingleMessage(string who, string message, string to, int prevPageCount = 3)
          => ForwardSingleMessage(new ChatSimpleMessage { Who = who, Message = message }, to, prevPageCount);
        private void _ForwardSingleMessageCore(Menu menu, string to)
        {
            var menuItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("转发...")));
            if (menuItem != null)
            {
                menuItem.DrawHighlightExt();
                menuItem.WaitUntilClickable(TimeSpan.FromSeconds(5));
                RandomWait.Wait(100, 800);
                menuItem.ClickEnhance(_SelfWindow);
                _ForwardMessageCore(to);
            }
            else
            {
                _logger.Error($"找不到转发菜单项，停止转发");
            }
        }

        private void _ForwardMessageCore(string to)
        {
            RandomWait.Wait(100, 800);
            var windowResult = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("SelectContactWnd"))),
              TimeSpan.FromSeconds(5),
              TimeSpan.FromMilliseconds(200));
            if (windowResult.Success)
            {
                windowResult.Result.DrawHighlightExt();
                windowResult.Result.WaitUntilClickable(TimeSpan.FromSeconds(5));
                var window = windowResult.Result;
                var searchTextBox = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("搜索")));
                searchTextBox.Focus();
                searchTextBox.DrawHighlightExt();
                searchTextBox.ClickEnhance(window.AsWindow());
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                RandomWait.Wait(100, 800);
                Keyboard.Type(to);
                RandomWait.Wait(100, 800);
                Keyboard.Press(VirtualKeyShort.RETURN);
                RandomWait.Wait(100, 800);
                var sendButton = window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("发送")));
                sendButton.DrawHighlightExt();
                sendButton.ClickEnhance(window.AsWindow());
            }
            else
            {
                _logger.Error($"找不到转发窗口，停止转发");
            }
        }

        private void _PopupIndexContextMenuCore(ListBoxItem listItem, int index, Action<Menu> action)
        {
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            listItem = _FindAndLocation_(ref listItem);

            if (listItem == null)
            {
                _logger.Error($"找不到消息：index={index}，停止转发");
                return;
            }
            Menu menu = _GetPopupMenuExt(listItem);
            if (menu == null)
            {
                _logger.Error($"找不到菜单：index={index}，停止转发");
                return;
            }
            action(menu);
        }

        private void _PopupContextMenuCore(ChatSimpleMessage chatSimpleMessage, Action<Menu> action, int prevPageCount = 3)
        {
            var listItem = _LocateSingleMessage(chatSimpleMessage, prevPageCount);
            if (listItem == null)
            {
                _logger.Error($"找不到消息：who={chatSimpleMessage.Who},message={chatSimpleMessage.Message}，停止转发");
                return;
            }
            Menu menu = _GetPopupMenuExt(listItem);
            if (menu == null)
            {
                _logger.Error($"找不到菜单：who={chatSimpleMessage.Who},message={chatSimpleMessage.Message}，停止转发");
                return;
            }
            action(menu);
        }
        //仅适用于长消息,得到消息框内部的点击点
        private Point _GetInnerClickablePoint(Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + 10);
        }
        private Menu _GetPopupMenuExt(ListBoxItem listItem)
        {
            if (listItem.BoundingRectangle.Bottom <= _BubbleListRoot.BoundingRectangle.Bottom)
            {
                return _GetPopupMenuShortMessageContent(listItem);
            }
            else
            {
                return _RightClickLongMessage_(listItem);
            }
        }

        private Menu _RightClickLongMessage_(ListBoxItem listItem)
        {
            _RightClickLongMessageCore_(listItem);

            var menuResult = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromMilliseconds(200));
            if (menuResult.Success)
            {
                menuResult.Result.DrawHighlightExt();
                return menuResult.Result;
            }
            else
            {
                _logger.Error($"找不到菜单");
            }
            return menuResult.Result;
        }

        private void _RightClickLongMessageCore_(AutomationElement listItem)
        {
            var rect = new Rectangle(listItem.BoundingRectangle.X, listItem.BoundingRectangle.Y, listItem.BoundingRectangle.Width, listItem.BoundingRectangle.Height < 100 ? listItem.BoundingRectangle.Height : 100);
            var centerPoint = new Point(rect.X + 120, rect.Y + 50);
            _SelfWindow.Focus();
            Mouse.Position = centerPoint;
            RandomWait.Wait(100, 500);
            Mouse.RightClick();
            RandomWait.Wait(500, 1500);
        }

        /// <summary>
        /// 获取短消息的右键菜单
        /// </summary>
        /// <param name="listItem">消息项</param>
        /// <returns>右键菜单</returns>
        private Menu _GetPopupMenuShortMessageContent(ListBoxItem listItem)
        {
            RandomWait.Wait(100, 800);
            //点击右键
            var subItems = listItem.FindAllByXPath("/Pane[1]/*");
            var pane = subItems[1];
            if (pane != null && pane.ControlType == ControlType.Pane)
            {
                var xPath = "//Button";
                var isReferenced = listItem.Name.Contains("\n引用  ");
                var button = pane.FindFirstByXPath(xPath)?.AsButton();
                if (button != null && !isReferenced)
                {
                    button.GetParent().DrawHighlightExt();
                    button.GetParent().WaitUntilClickable(TimeSpan.FromSeconds(5));
                    button.GetParent().RightClick();
                    _ImageVedioPreProcess(button, listItem);
                }
                else
                {
                    xPath = "//Text";
                    var texts = pane.FindAllByXPath(xPath);

                    AutomationElement text = null;
                    if (texts != null && texts.Length > 1)
                    {
                        if (subItems[0].ControlType == ControlType.Button && _ChatBody.ChatType == ChatType.群聊)
                        {
                            text = texts[1];
                        }
                        else
                        {
                            text = texts[0];
                        }
                    }
                    else if (texts != null && texts.Length == 1)
                    {
                        text = texts[0];
                    }
                    else
                    {
                        _logger.Error($"找不到文本控件，texts.Length={texts.Length},停止转发");
                    }

                    if (text != null)
                    {
                        var parentPane = text.GetParent();
                        parentPane.DrawHighlightExt();
                        parentPane.WaitUntilClickable(TimeSpan.FromSeconds(5));
                        parentPane.RightClick();
                    }
                }
                RandomWait.Wait(100, 1500);
                var menu = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMilliseconds(200));
                if (menu.Success)
                {
                    menu.Result.DrawHighlightExt();
                    return menu.Result;
                }
                else
                {
                    _logger.Error($"找不到菜单");
                }
            }
            else
            {
                _logger.Error($"找不到第二个位置是Pane的元素");
            }
            return null;
        }



        /// <summary>
        /// 图片和视频、语音预处理
        /// </summary>
        /// <param name="button"></param>
        /// <param name="listItem"></param>
        private void _ImageVedioPreProcess(AutomationElement button, ListBoxItem listItem)
        {
            //如果出现"图片查看"，则关闭
            _CloseImageVedioPopupWin();
            RandomWait.Wait(100, 1500);
            var menu = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromMilliseconds(200));
            if (menu.Success)
            {
                if (listItem.Name.Contains("[语音]"))
                {
                    RandomWait.Wait(100, 1500);
                    var menuItem = menu.Result.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("取消转文字")));
                    if (menuItem != null)
                    {
                        return;
                    }
                }
                else
                {
                    RandomWait.Wait(100, 1500);
                    var menuItem = menu.Result.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("引用")));
                    if (menuItem != null)
                    {
                        return;
                    }
                }
            }
            else
            {
                _logger.Error($"找不到菜单");
            }
            _SwtichFocus(listItem);
            if (listItem.Name.Contains("[图片]"))
            {
                button.GetParent().DrawHighlightExt();
                button.GetParent().WaitUntilClickable(TimeSpan.FromSeconds(5));
                button.GetParent().RightClick();
                RandomWait.Wait(100, 800);
            }
            else if (listItem.Name.Contains("[视频]"))
            {
                var index = 0;
                button.Click();
                RandomWait.Wait(500, 1500);
                //如果出现"图片查看"，则关闭
                _CloseImageVedioPopupWin();
                _SelfWindow.Focus();
                while (true && index < 20)
                {
                    button.GetParent().DrawHighlightExt();
                    button.GetParent().WaitUntilClickable(TimeSpan.FromSeconds(5));
                    button.GetParent().RightClick();
                    RandomWait.Wait(100, 800);
                    var menuResult = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromMilliseconds(200));
                    if (menuResult.Success)
                    {
                        var testMenu = menuResult.Result;
                        var menuItem = testMenu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("引用")));
                        if (menuItem != null)
                        {
                            break;
                        }
                        else
                        {
                            _SwtichFocus(listItem);
                            RandomWait.Wait(500, 1000);
                        }
                    }
                    index++;
                }
            }
            else if (listItem.Name.Contains("[语音]"))
            {
                button.RightClick();
                RandomWait.Wait(500, 1000);
                var voiceMenu = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromMilliseconds(200));
                if (voiceMenu.Success)
                {
                    var voiceMenuItem = voiceMenu.Result.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("语音转文字")));
                    if (voiceMenuItem != null)
                    {
                        voiceMenuItem.DrawHighlightExt();
                        RandomWait.Wait(500, 1000);
                        voiceMenuItem.Click();
                        var index = 0;
                        while (true && index < 20)
                        {
                            RandomWait.Wait(500, 800);
                            button.WaitUntilClickable(TimeSpan.FromSeconds(3));
                            button.RightClick();
                            RandomWait.Wait(100, 1500);
                            var menuResult = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.Menu()).AsMenu(),
                                TimeSpan.FromSeconds(2),
                                TimeSpan.FromMilliseconds(200));
                            if (menuResult.Success)
                            {
                                var testMenu = menuResult.Result;
                                var menuItem = testMenu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("取消转文字")));
                                if (menuItem != null)
                                {
                                    break;
                                }
                                else
                                {
                                    _SwtichFocus(listItem);
                                    RandomWait.Wait(500, 1000);
                                }
                            }

                            index++;
                        }
                    }
                }
                else
                {
                    _logger.Error($"找不到语音菜单");
                }
            }
        }
        //关闭图片和视频弹窗
        private void _CloseImageVedioPopupWin()
        {
            var win = Retry.WhileNull(() => _SelfWindow.Automation.GetDesktop().FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("ImagePreviewWnd")).And(cf.ByProcessId(_SelfWindow.Properties.ProcessId))),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromMilliseconds(200));
            if (win.Success)
            {
                win.Result.AsWindow().Close();
            }
        }
        private void _SwtichFocus(ListBoxItem listItem)
        {
            var pane = listItem.GetParent().GetParent().GetParent().GetSibling(1);
            var edit = pane.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
            if (edit != null)
            {
                // edit.Click();
                _SelfWindow.Focus();
                Point point = new Point(edit.BoundingRectangle.X + 10, edit.BoundingRectangle.Y + 15);
                Mouse.Position = point;
                Mouse.LeftClick();
                RandomWait.Wait(100, 800);
            }
        }
        private List<AutomationElement> _GetListItems()
        {
            _BubbleListRoot = _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("消息")));
            if (_BubbleListRoot == null)
                throw new Exception("消息列表根节点获取失败");
            return _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
        }

        private List<AutomationElement> _GetCheckListItems()
        {
            _BubbleListRoot = _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("消息")));
            if (_BubbleListRoot == null)
                throw new Exception("消息列表根节点获取失败");
            return _BubbleListRoot.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).ToList();
        }


        private ListBoxItem _LocateSingleMessage(ChatSimpleMessage chatSimpleMessage, int prevPageCount)
        {
            int index = 0; //向前翻页的索引
            ListBoxItem result = null;
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            while (index < prevPageCount)
            {
                var listItems = _GetListItems();
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
                RandomWait.Wait(100, 800);
                if (result != null)
                {
                    break;
                }

                //往上翻页
                var (flowControl, nextIndex) = _PrevPageSearch(chatSimpleMessage, index);
                if (!flowControl)
                {
                    break;
                }
                index = nextIndex;
                _logger.Trace($"往上翻页{index}次，继续查找消息");
            }
            if (index >= prevPageCount)
            {
                _logger.Error($"往上翻页{index}次，仍然找不到消息，停止查找");
            }
            return result;
        }

        private ListBoxItem _SameMessageAndMove_(ListBoxItem selectItem, ChatSimpleMessage chatSimpleMessage)
        {
            var who = _GetWhoFromListItem_(selectItem);
            if (selectItem.Name.Contains(chatSimpleMessage.Message) && who == chatSimpleMessage.Who)
            {
                var baseRect = _BubbleListRoot.BoundingRectangle;
                var listItems = _GetListItems();
                listItems.Reverse();
                var foundItem = listItems.FirstOrDefault(u => u.Name == selectItem.Name && u.Properties.RuntimeId.Value.SequenceEqual(selectItem.Properties.RuntimeId.Value))?.AsListBoxItem();
                foundItem = _FindAndLocation_(ref foundItem);
                foundItem?.DrawHighlightExt();

                return selectItem;
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
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 0);
                }
                loadMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)))?.AsButton();
                loadMoreButton.DrawHighlightExt();
                loadMoreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
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

        private (bool flowControl, int nextIndex) _PrevPageSearchWho(string who, int index)
        {
            //往上翻页
            index++;
            var loadMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)))?.AsButton();
            if (loadMoreButton != null)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 0);
                }
                loadMoreButton = _BubbleListRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_LOOK_MORE)))?.AsButton();
                loadMoreButton.DrawHighlightExt();
                loadMoreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                loadMoreButton.Click();
                RandomWait.Wait(100, 1000);
                return (flowControl: true, nextIndex: index);
            }
            else
            {
                _logger.Error($"不存在who={who}的消息,停止查找");
                return (flowControl: false, nextIndex: index);
            }
        }

        /// <summary>
        /// 解析气泡为Bubble对象,Bubble对象可能为空
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="dateTime"></param>
        /// <returns>Bubble对象,可能为空,也可能为List<Bubble>对象;<see cref="MessageBubble"/></returns>
        private Object _ParseBubble(AutomationElement listItem, ref DateTime? dateTime)
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
                    bubbleItem.BeTap = Regex.Match(title, @"""([^""]+)""").Groups[1].Value;
                }
                else
                {
                    if (title.Contains("我"))
                    {
                        bubbleItem.MessageSource = MessageSourceType.好友消息;
                        bubbleItem.Who = Regex.Match(title, @"^""([^""]+)""").Groups[1].Value;
                        bubbleItem.GroupNickName = bubbleItem.Who;
                        bubbleItem.BeTap = "我";
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
                                bubbleItem.BeTap = m.Groups[1].Value;
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
                    bubbleItem.BeTap = Regex.Match(title, @"""([^""]+)""$").Groups[1].Value;
                }
                else
                {
                    bubbleItem.MessageSource = MessageSourceType.好友消息;
                    bubbleItem.Who = Regex.Match(title, @"^""([^""]+)""").Groups[1].Value;
                    bubbleItem.BeTap = "我";
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
            if (_MatchWeekFormat(date, ref refData))
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

        private bool _MatchWeekFormat(string date, ref DateTime? refData)
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
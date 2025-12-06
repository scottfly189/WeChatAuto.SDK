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
using System.Globalization;
using WeChatAuto.Utils;
using WeChatAuto.Models;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Patterns;
using System.Drawing;
using FlaUI.Core.Tools;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.Capturing;
using WeAutoCommon.Models;

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
        public List<MessageBubble> GetVisibleBubblesByPolling(UIThreadInvoker privateThreadInvoker)
        {
            var bubbles = GetVisibleNativeBubblesByPolling(privateThreadInvoker);
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
            MessageBubbleParser messageBubbleParser = new MessageBubbleParser(_uiThreadInvoker, _BubbleListRoot, _Title);
            for (int i = 0; i < listItemList.Count; i++)
            {
                var bubble = messageBubbleParser._ParseBubble(listItemList[i], ref dateTime);
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
        private AutomationElement _GetPrivateBubbleListRoot(UIThreadInvoker privateThreadInvoker)
        {
            return null;
        }
        //通过私有线程获取气泡列表
        public List<MessageBubble> GetVisibleNativeBubblesByPolling(UIThreadInvoker privateThreadInvoker)
        {
            var listItemList = privateThreadInvoker.Run(automation => _GetPrivateBubbleListRoot(privateThreadInvoker).FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()).GetAwaiter().GetResult();
            List<MessageBubble> bubbles = new List<MessageBubble>();
            // DateTime? dateTime = null;
            // for (int i = 0; i < listItemList.Count; i++)
            // {
            //     var bubble = _ParseBubble(listItemList[i], ref dateTime);
            //     if (bubble != null)
            //     {
            //         if (bubble is MessageBubble)
            //         {
            //             bubbles.Add(bubble as MessageBubble);
            //         }
            //         else
            //         {
            //             bubbles.AddRange(bubble as List<MessageBubble>);
            //         }
            //     }
            // }
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
        /// 3、语音，会自行语音转文字
        /// </summary>
        /// <param name="to">要转发给谁</param>
        /// <param name="isCapture">是否要转发的内容进行截图，默认是true</param>
        /// <param name="rowCount">要转发多少条消息，默认是最后的5条消息,如果当前没有十条，则转发所有消息</param>
        public void ForwardMultipleMessage(string to, bool isCapture = true, int rowCount = 5)
        {
            var result = _uiThreadInvoker.Run(automation =>
            {
                List<ListBoxItem> _WillProcessItems = _GetWillForwardMessageList(rowCount);  //得到所有要转发的消息

                // 前置操作，如果有图片、视频、语音，则先处理
                var r = EnsureSuccess(_PreImageVedioMessage(_WillProcessItems));
                if (!r.Success) return r;

                // 选择要转发多少条消息
                r = EnsureSuccess(_SelectMultipleMessage(_WillProcessItems));
                if (!r.Success) return r;

                r = EnsureSuccess(_ProcessMaybeError());
                if (!r.Success) return r;

                // 转发消息
                r = EnsureSuccess(_ForwardMessageCore(to));
                if (!r.Success) return r;

                r = EnsureSuccess(_ProcessMaybeError());
                if (!r.Success) return r;

                // 如果需要截图，则进行截图
                if (isCapture)
                {
                    r = EnsureSuccess(_CaptureMultipleMessage(_WillProcessItems, to));
                    if (!r.Success) return r;
                }

                return Result.Ok();
            })
            .GetAwaiter().GetResult();
            if (result.Success && isCapture)
            {
                var from = this._ChatBody.ChatContent.ChatHeader.Title; //得到发送者
                this._ChatBody.ChatContent.MainWxWindow.PasteContentToWho(to).GetAwaiter().GetResult();
                //转回from
                this._ChatBody.ChatContent.MainWxWindow.FocusWho(from);
            }
            else
            {
                _logger.Error($"转发失败: {result.Error}");
            }
        }

        /// <summary>
        /// 检查结果，如果失败则返回失败，否则返回成功的结果以便继续链式调用
        /// </summary>
        private Result EnsureSuccess(Result result)
        {
            return result.Success ? Result.Ok() : Result.Fail(result.Error);
        }

        private Result _ProcessMaybeError()
        {
            var alertWin = Retry.WhileNull(() => _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("AlertDialog")).And(cf.ByProcessId(_SelfWindow.Properties.ProcessId))),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromMilliseconds(200));
            if (alertWin.Success)
            {
                RandomWait.Wait(100, 1500);
                alertWin.Result.DrawHighlightExt();
                alertWin.Result.AsWindow().Close();
                return Result.Fail($"找到可能的错误弹窗，停止转发");
            }
            else
            {
                _logger.Info($"没有找到可能的错误弹窗,正常退出");
                return Result.Ok();
            }
        }

        private Result _CaptureMultipleMessage(List<ListBoxItem> selectItems, string to)
        {
            var lastItem = selectItems.LastOrDefault();
            List<Image> images = new List<Image>();
            lastItem = _GetItemNewestVersion_(lastItem);
            if (lastItem != null)
            {
                lastItem = _FindAndLocation_(ref lastItem);  //定位
                var image = FlaUI.Core.Capturing.Capture.Element(_ChatBody.ChatContent.NewChatContentRoot);
                image.ApplyOverlays(new MouseOverlay(image), new InfoOverlay(image));
                images.Add(image.Bitmap);
            }
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    while (pattern.VerticalScrollPercent < 1)
                    {
                        pattern.SetScrollPercent(0, System.Math.Min(pattern.VerticalScrollPercent + pattern.VerticalViewSize, 1));
                        RandomWait.Wait(100, 800);
                        var image = FlaUI.Core.Capturing.Capture.Element(_ChatBody.ChatContent.NewChatContentRoot);
                        image.ApplyOverlays(new MouseOverlay(image), new InfoOverlay(image));
                        if (images.Count < 9)
                        {
                            images.Add(image.Bitmap);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return _PasteImagesToWho(to, images);
        }

        private Result _PasteImagesToWho(string to, List<Image> images)
        {
            if (images == null || images.Count == 0)
            {
                _logger.Error("没有可粘贴的图片（Bitmap）到剪切板");
                return Result.Fail("没有可粘贴的图片（Bitmap）到剪切板");
            }

            if (_PutToClipboard(images))
            {
                _logger.Info($"将图片放入剪贴板成功");
                return Result.Ok();
            }
            else
            {
                _logger.Error("将图片放入剪贴板失败");
                return Result.Fail("将图片放入剪贴板失败");
            }
        }

        private bool _PutToClipboard(List<Image> images)
        {
            try
            {
                // 构建一个DataObject保存多张图片
                System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject();

                // 创建一个包含所有Bitmap的Image数组
                var bitmaps = images.OfType<System.Drawing.Bitmap>().ToArray();

                if (bitmaps.Length == 1)
                {
                    // 单张图片直接设置
                    dataObject.SetData(System.Windows.Forms.DataFormats.Bitmap, true, bitmaps[0]);
                }
                else
                {
                    // 多张图片放入FileDrop（构造虚拟文件流，部分微信支持粘贴多图）
                    // 临时保存到磁盘，再以FileDrop放入剪贴板
                    var tempFiles = new List<string>();
                    foreach (var bmp in bitmaps)
                    {
                        // 创建临时文件
                        string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.png");
                        bmp.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);
                        tempFiles.Add(tempPath);
                    }
                    dataObject.SetData(System.Windows.Forms.DataFormats.FileDrop, tempFiles.ToArray());
                }

                // 设置到剪贴板（在UI线程内执行更安全）
                System.Windows.Forms.Clipboard.SetDataObject(dataObject, true);
                _logger.Info($"已将{images.Count}张图片放入剪贴板");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"粘贴图片到剪贴板出错: {ex}");
                return false;
            }
        }

        //前置处理图片、视频、语音消息
        private Result _PreImageVedioMessage(List<ListBoxItem> selectItems)
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
            return Result.Ok();
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
            if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]") || item.Name.Contains("[语音]"))
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
                if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]") || item.Name.Contains("[语音]"))
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
            if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]") || item.Name.Contains("[语音]"))
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
                if (item.Name.Contains("[图片]") || item.Name.Contains("[视频]") || item.Name.Contains("[语音]"))
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

        private Result _SelectMultipleMessage(List<ListBoxItem> _WillProcessItems)
        {
            if (_BubbleListRoot.Patterns.Scroll.IsSupported)
            {
                var pattern = _BubbleListRoot.Patterns.Scroll.Pattern;
                if (pattern != null && pattern.VerticallyScrollable)
                {
                    pattern.SetScrollPercent(0, 1);
                }
            }
            this._PopupMultipleForwardMenuCore(_WillProcessItems);  //弹出转发菜单
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
                    return Result.Fail($"找不到多选菜单项");
                }
            }
            else
            {
                _logger.Error($"找不到菜单");
                return Result.Fail($"找不到菜单");
            }
            foreach (ListBoxItem item in _WillProcessItems)
            {
                _LocateMessageAndSelect(item);
            }

            var result = this._ConfirmMultipleForwardCore();
            if (!result.Success)
            {
                return Result.Fail(result.Error);
            }
            return Result.Ok();
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

        //弹出转发菜单
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
        private Result _ConfirmMultipleForwardCore()
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
                    return Result.Ok();
                }
                else
                {
                    _logger.Error($"找不到合并转发按钮");
                    return Result.Fail($"找不到合并转发按钮");
                }
            }
            else
            {
                _logger.Error($"找不到转发Pane");
                return Result.Fail($"找不到转发Pane");
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
                element.Click();
            }
            else
            {
                var rect = new Rectangle(element.BoundingRectangle.X, element.BoundingRectangle.Y, element.BoundingRectangle.Width, element.BoundingRectangle.Height < 100 ? element.BoundingRectangle.Height : 100);
                var centerPoint = new Point(rect.X + (rect.Width - 180) / 2, rect.Y + (rect.Height / 2) + 10);
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

        private Result _ForwardMessageCore(string to)
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
                return Result.Ok();
            }
            else
            {
                _logger.Error($"找不到转发窗口，停止转发");
                return Result.Fail($"找不到转发窗口，停止转发");
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


    }
}
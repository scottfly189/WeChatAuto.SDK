using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WeAutoCommon.Enums;
using WeAutoCommon.Utils;
using System.Text.RegularExpressions;
using WeAutoCommon.Interface;
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
using FlaUI.UIA3;
using System.Threading.Tasks;
using System.Net.Http;
using WeAutoCommon.Extentions;
using WeChatAuto.Services;
using MessagePack.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using Emgu.CV;
using System.Reflection.PortableExecutable;
using System.Threading;
using OneOf.Types;
using NAudio.SoundFont;
using FlaUI.Core.Exceptions;
using System.Threading.Channels;
using OneOf;
using WeChatAuto.Options;
using Dm.util;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区气泡列表
    /// </summary>
    public class MessageBubbleList
    {
        private IServiceProvider _serviceProvider;
        private AutoLogger<MessageBubbleList> _logger;
        private UIThreadInvoker _uiThreadInvoker;
        private ChatContent _ChatContent;
        private WeChatClient _Client;
        internal Button HistoryButton => _GetHistoryButton();   //实时获取聊天记录按钮
        internal ListBox MessageRoot => _GetMessageRoot();

        internal MessageBubbleList(WeChatClient client, UIThreadInvoker uiThreadInvoker, ChatContent content, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<AutoLogger<MessageBubbleList>>();
            this._Client = client;
            _uiThreadInvoker = uiThreadInvoker;
            _ChatContent = content;
        }

        /// <summary>
        /// 聊天消息根,要注意:4.1.9.xx与4.1.10.xx的UI Tree结构不同
        /// </summary>
        internal ListBox _GetMessageRoot()
        {
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView'] | /Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/List[@Name='消息'][@AutomationId='chat_message_list'][@ClassName='mmui::RecyclerListView']";
            var rootRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            return rootRetry.Success ? rootRetry.Result.AsListBox() : null;
        }

        internal Button _GetHistoryButton()
        {
            var buttonRetry = Retry.WhileNull(() =>
            {
                var button = _Client.MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("聊天记录")).And(cf.ByClassName("mmui::XButton")));
                return button == null ? null : button.AsButton();
            }, timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            return buttonRetry.Success ? buttonRetry.Result : null;
        }

        /// <summary>
        /// 根据日期获取聊天历史
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称</param>
        /// <param name="date">查询日期,如果不传，则是当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime date = default)
        {
            if (string.IsNullOrWhiteSpace(who))
            {
                var chatInfo = await _Client.ChatContent.ChatHeader.GetTitle();
                if (!chatInfo.CanTalk())
                {
                    return new List<ChatSimpleMessage>();
                }
            }
            else
            {
                await _Client.SearchFriend(who);
            }
            RandomWait.Wait(300, 1000);
            return await GetChatHistory(date);
        }
        /// <summary>
        /// 获取一段时间的聊天历史记录，包括日期-时间，其他的获取消息历史的api仅支持日期
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称</param>
        /// <param name="startDate">开始日期,包括日期与时间</param>
        /// <param name="endDate">结束日期，包括日期与时间</param>
        /// <returns></returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("结束日期不能小于开始日期");
            if (endDate.Date > DateTime.Today)
            {
                endDate = DateTime.Today.AddDays(1).AddTicks(-1);
            }
            if (string.IsNullOrWhiteSpace(who))
            {
                //可能没有选择聊天对象，如果没有选择聊天对象，则不发送.
                var chatInfo = await _Client.ChatContent.ChatHeader.GetTitle();
                if (!chatInfo.CanTalk())
                {
                    return new List<ChatSimpleMessage>();
                }
            }
            else
            {
                await _Client.Conversations.Search(who);
            }
            RandomWait.Wait(300, 1200);
            return await WeChatInvoker.Call(GetAllChatHistoryCore, startDate, endDate);
        }

        /// <summary>
        /// 根据日期获取当前聊天窗口的聊天历史
        /// </summary>
        /// <param name="date">查询日期,如果为空则为当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(DateTime date = default)
        {
            if (date == default || date.Date > DateTime.Today.Date)
            {
                date = DateTime.Today;
            }
            var start = date.Date;
            var end = date.Date.AddDays(1).AddTicks(-1);

            return await WeChatInvoker.Call(GetAllChatHistoryCore, start, end);
        }

        internal List<ChatSimpleMessage> GetAllChatHistoryCore(UIA3Automation automation, DateTime starDate, DateTime endDate)
        {
            var invokeButton = HistoryButton;
            if (invokeButton == null)
                return new List<ChatSimpleMessage>();
            HeaderInfo title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
            if (!title.CanTalk())
                return new List<ChatSimpleMessage>();
            List<string> whoList = default;
            if (title.HeaderType == ChatType.群聊)
            {
                whoList = _Client.OuterGroup.GetChatGroupMemberListCore(automation);
            }
            else
            {
                if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
                {
                    whoList = new List<string>() { title.Title };
                }
                else
                {
                    throw new Exception($"错误：好友 {title.Title} 不是聊天内容，不能获取历史记录");
                }
            }
            __ClickChatHistoryButton(invokeButton);
            return __FetchChatHistoryList(automation, starDate, endDate, title, whoList);
        }

        private List<ChatSimpleMessage> __FetchChatHistoryList(UIA3Automation automation, DateTime startDate, DateTime endDate, HeaderInfo title, List<string> whoList)
        {
            var desktop = automation.GetDesktop();
            var winResult = Retry.WhileNull(() => desktop.FindAllChildren(cf => cf.ByClassName("mmui::SearchMsgUniqueChatWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_Client.MainWindow.Properties.ProcessId)))), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            if (winResult.Success)
            {
                var subWins = winResult.Result;
                var subWin = subWins.FirstOrDefault(u =>
                {
                    var name = u.Name.Replace("“", "").Replace("”", "");
                    if (name.Contains($"{title.Title}"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }).AsWindow();
                if (subWin == null)
                    return new List<ChatSimpleMessage>();
                subWin.Focus();
                int targetX = _Client.MainWindow.BoundingRectangle.X + (int)((_Client.MainWindow.BoundingRectangle.Width - subWin.BoundingRectangle.Width) / 2);
                int targetY = _Client.MainWindow.BoundingRectangle.Y + (int)((_Client.MainWindow.BoundingRectangle.Height - subWin.BoundingRectangle.Height) / 2);
                subWin.Move(targetX, targetY);  //移动子窗口至主窗口中间
                RandomWait.Wait(100, 600);
                subWin.DrawHighlightExt();
                try
                {
                    List<ChatSimpleMessage> list;
                    if (_IsToday(startDate))
                    {
                        //获取当天日期
                        list = __FetchHistoryDataFromToday(subWin, startDate, endDate, title, whoList);
                    }
                    else
                    {
                        //选择日期筛选....获取历史记录
                        list = __FetchHistoryDataFromFilterDate(subWin, startDate, endDate, title, whoList);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    _logger.Error($"{nameof(MessageBubbleList)} - {nameof(__FetchChatHistoryList)}:{ex.ToString()}");
                    return new List<ChatSimpleMessage>();
                }
                finally
                {
                    subWin?.Close();
                }
            }
            else
            {
                return new List<ChatSimpleMessage>();
            }
        }

        private List<string> _GetWhoList(HeaderInfo title)
        {
            if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
            {
                return new List<string>
                {
                    title.Title
                };
            }
            if (title.HeaderType == ChatType.群聊)
            {

                return null;
            }
            throw new Exception($"错误：好友 {title.Title} 不是聊天类型！");
        }

        private List<ChatSimpleMessage> __FetchHistoryDataFromFilterDate(Window subWin, DateTime startDate, DateTime endDate, HeaderInfo title, List<string> whoList)
        {
            List<ChatSimpleMessage> result = new List<ChatSimpleMessage>();
            //日期筛选
            var isSelected = _SelectDate(DateOnly.FromDateTime(startDate), subWin);
            if (!isSelected)
            {
                isSelected = _SelectDate(DateOnly.FromDateTime(endDate), subWin);
                if (!isSelected)
                    return result;
            }
            //获取历史记录.
            __FetchHistoryDataFromFilterDateCore(subWin, startDate, endDate, result, title, whoList);

            return result;
        }

        private void __FetchHistoryDataFromFilterDateCore(Window subWin, DateTime startDate, DateTime endDate, List<ChatSimpleMessage> result, HeaderInfo info, List<string> whoList)
        {
            ListBox root = __ChangeToCheckBoxState(subWin, startDate, endDate);   //改成checkbox状态
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
            if (items.Count() == 0)
                return;
            var index = 0;
            var scrollPoint = root.BoundingRectangle.SafeRandomPoint();
            var oldSnap = new List<string>();
            while (index < WeAutomation.Config.HistoryRetryNumber)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                var newSnap = items.Select(u => u.Name.Trim() + u.Properties.RuntimeId.ToUniqueString()).ToList();
                var exceptList = newSnap.Except(oldSnap).ToList();
                oldSnap = newSnap;
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    var exitFlag = __ScrollFetchContent(exceptList, subWin, root, startDate, endDate, result, info, whoList);
                    if (exitFlag)
                        break;
                }

                index++;
                MouseScrollHelper.DownStep(scrollPoint.Confusion(10, 6), 5);
            }
        }

        private bool __ScrollFetchContent(List<string> exceptList, Window subWin, ListBox root, DateTime startDate, DateTime endDate, List<ChatSimpleMessage> result, HeaderInfo info, List<string> whoList)
        {
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).Where(u => exceptList.Contains(u.Name.Trim() + u.Properties.RuntimeId.ToUniqueString())).ToList();
            foreach (var item in items)
            {
                DateTime date = __GetDateFromName(item.Name.Trim());
                if (date > endDate)
                    return true;
                if (date < startDate)
                    continue;
                if (date >= startDate && date <= endDate)
                {
                    __GenerateMessage(result, date, item.Name, info, whoList);
                }
            }

            return false;
        }

        private void __GenerateMessage(List<ChatSimpleMessage> result, DateTime date, string content, HeaderInfo info, List<string> whoList)
        {
            ChatSimpleMessage message = new ChatSimpleMessage();
            message.DateTime = date;
            message.SendDateTime = date.ToString("yyyy年M月d日 HH:mm");
            message.UniqueString = GetMd5(content);

            var pattern = @"^(.*?)\s+((?:星期[一二三四五六日天]|昨天|前天|\d{1,2}月\d{1,2}日)?\s*\d{1,2}:\d{2})$";
            var match = Regex.Match(content, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                var prefix = match.Groups[1].Value.Trim();
                string who = __GetWhoCore(prefix.Trim(), whoList);
                message.Who = who;
                message.Message = prefix.substring(message.Who.Length).Trim();

                result.Add(message);
            }
        }

        private string __GetWhoCore(string prefix, List<string> whoList)
        {
            var nickName = whoList.OrderByDescending(x => x.Length).FirstOrDefault(x => prefix.StartsWith(x + " ") || prefix == x);
            return nickName;
        }

        private List<ChatSimpleMessage> __FetchHistoryDataFromToday(Window subWin, DateTime startDate, DateTime endDate, HeaderInfo title, List<string> whoList)
        {
            var root = subWin.FindFirstDescendant(cf => cf.ByAutomationId("chat_log_message_list").And(cf.ByClassName("mmui::RecyclerListView")).And(cf.ByControlType(ControlType.List))).AsListBox();
            if (root.Items.Count() == 0)
                return new List<ChatSimpleMessage>();
            var chatItems = root.Items.Where(x => x.ControlType == ControlType.ListItem && !string.IsNullOrWhiteSpace(x.Name) && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
            if (chatItems.Count() == 0)
            {
                Mouse.Position = root.BoundingRectangle.SafeRandomPoint();
                Mouse.Scroll(-5);
                RandomWait.Wait(100, 300);
                chatItems = root.Items.Where(x => x.ControlType == ControlType.ListItem && !string.IsNullOrWhiteSpace(x.Name) && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
            }
            ListBoxItem chatItem = null;
            foreach (var item in chatItems)
            {
                var value = item.Name;
                var pattern = @"\s\d{4}年\d{1,2}月\d{1,2}日\s+\d{1,2}:\d{2}$";
                if (Regex.Match(value, pattern).Success)
                {
                    chatItem = item;
                    break;
                }
            }
            if (chatItem == null)
                return new List<ChatSimpleMessage>();
            chatItem.DrawHighlightExt();
            var dpi = DpiHelper.GetScaleForWindow(_Client.MainWindow.Properties.NativeWindowHandle);
            var initOffsetX = (int)(WeAutomation.Config.HistoryMessageOffset_X * dpi + chatItem.BoundingRectangle.X);
            var initOffsetY = (int)(WeAutomation.Config.HistoryMessageOffset_Y * dpi) + chatItem.BoundingRectangle.Y;
            var point = new Point(initOffsetX, initOffsetY);
            Mouse.Position = point;
            RandomWait.Wait(100, 400);

            Mouse.RightClick();  //得到完整的UI Tree列表
            RandomWait.Wait(100, 300);
            var menuWinRetry = Retry.WhileNull(() => subWin.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Weixin").And(cf.ByClassName("mmui::XMenu")))).AsWindow(), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            if (menuWinRetry.Success)
            {
                menuWinRetry.Result.DrawHighlightExt();
                var menuWin = menuWinRetry.Result;
                var selectItem = menuWin.FindFirstChild(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("多选")));
                if (selectItem == null)
                    return new List<ChatSimpleMessage>();
                selectItem.DrawHighlightExt();
                RandomWait.Wait(300, 900);
                selectItem.Click();
                RandomWait.Wait(300, 900);
                root = subWin.FindFirstDescendant(cf => cf.ByAutomationId("chat_log_message_list").And(cf.ByClassName("mmui::RecyclerListView")).And(cf.ByControlType(ControlType.List))).AsListBox();
                return __FetchChatHistoryListCore(root, startDate, endDate, whoList, title);
            }
            else
            {
                return new List<ChatSimpleMessage>();
            }

        }

        private bool _SelectDate(DateOnly date, Window window)
        {
            var yearStr = date.Year.ToString() + "年";
            var monthStr = date.Month.ToString() + "月";
            var path = "/Group/Group/Group/Group/Group/Group/Tab/TabItem[@AutomationId='qt_scrollarea_viewport.button_container.record_type_datetime'][@Name='日期']";
            var tabItemRetry = Retry.WhileNull(() => window.FindFirstByXPath(path), timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));
            if (tabItemRetry.Success)
            {
                var item = tabItemRetry.Result;
                var point = item.BoundingRectangle.SafeRandomPoint();
                SupperMouseKey.LeftClick(point);
                RandomWait.Wait(300, 900);
                var popupWinRetry = Retry.WhileNull(() => window.Automation.GetDesktop().FindFirstByXPath("/Window[@Name='Weixin']/Group/Text[@Name='选择发送日期']"), timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));
                if (popupWinRetry.Success)
                {
                    var smallTitle = popupWinRetry.Result;
                    var popWin = smallTitle.GetParent().GetParent();
                    popWin.DrawHighlightExt();
                    //检查年份与月份，如果月份不合适，则选择月份.
                    var yearButton = smallTitle.GetSibling(1);  //目前暂时实现不跨年，会员提示再改
                    var monthButton = yearButton.GetSibling(1);
                    var currentMonth = monthButton.FindFirstChild(cf => cf.ByControlType(ControlType.Text)).Name.Trim();
                    if (currentMonth != monthStr)
                    {
                        point = monthButton.BoundingRectangle.SafeRandomPoint();
                        SupperMouseKey.MoveTo(point);
                        RandomWait.Wait(800, 1500);
                        SupperMouseKey.LeftClick();
                        //等候需要的月份出现.
                        RandomWait.Wait(600, 1200);
                        var monthWinRetry = Retry.WhileNull(() => popWin.FindFirstChild(cf => cf.ByName("Weixin")), timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));
                        if (monthWinRetry.Success)
                        {
                            var childItem = monthWinRetry.Result.FindFirstChild(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName(monthStr)));
                            if (childItem != null)
                            {
                                point = childItem.BoundingRectangle.SafeRandomPoint();
                                SupperMouseKey.MoveTo(point);
                                RandomWait.Wait(150, 900);
                                SupperMouseKey.LeftClick();
                                RandomWait.Wait(100, 300);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    //ocr识别日期，然后点击,注意：有些日期可能没有记录，点击不进去
                    var container = monthButton.GetSibling(1);
                    if (container != null)
                    {
                        using Mat mat1 = this._Client.OcrEngee.GetMatFromElement(container);
                        var dpi100Top = 20;   //dpi在100%时候顶部距离
                        var baseTop = (int)(dpi100Top * DpiHelper.GetScaleForWindow(this._Client.MainWindow.Properties.NativeWindowHandle));
                        Rectangle rectangle = new Rectangle(0, baseTop, mat1.Width, mat1.Height - baseTop);
                        using Mat mat2 = new Mat(mat1, rectangle);  //将上面的裁剪25,以保持日期列
                        var clickPoint = this._Client.OcrEngee.GetPointFromDateTime(mat2, date, 15, false);
                        clickPoint = new Point(clickPoint.X + container.BoundingRectangle.X, clickPoint.Y + baseTop + container.BoundingRectangle.Y);
                        SupperMouseKey.MoveTo(clickPoint);
                        RandomWait.Wait(800, 1200);
                        SupperMouseKey.LeftClick();
                        RandomWait.Wait(200, 800);
                        //检查日期是否可以点击.
                        var findResult = Retry.WhileNotNull(() => window.Automation.GetDesktop().FindFirstByXPath("/Window[@Name='Weixin']/Group/Text[@Name='选择发送日期']"), timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));

                        return findResult.Success ? findResult.Result : false;
                    }
                    return false;
                }
            }
            return false;
        }

        //往下翻页，得到所有日期的字段.
        private List<ChatSimpleMessage> __FetchChatHistoryListCore(ListBox root, DateTime startDate, DateTime endDate, List<string> whoList, HeaderInfo info)
        {
            var list = new List<ChatSimpleMessage>();
            var scollPoint = root.BoundingRectangle.SafeRandomPoint();
            Mouse.Position = scollPoint;
            RandomWait.Wait(100, 600);
            var index = 0;
            var exit = false;
            var oldSnap = new List<string>();
            while (index < WeAutomation.Config.HistoryRetryNumber && !exit)
            {
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                if (items.Count() == 0)
                {
                    MouseScrollHelper.DownStep(scollPoint, 2);
                    index++;
                    continue;
                }
                var newSnap = items.Select(x => x.Name).ToList();
                var exceptList = newSnap.Except(oldSnap).ToList();
                oldSnap = newSnap;
                if (exceptList.Count() == 0)
                {
                    //处理长文本.
                    items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                    if (items.Count() == 1)
                    {
                        var longIndex = 0;
                        while (longIndex < 20)
                        {
                            MouseScrollHelper.DownStep(scollPoint, 2);
                            longIndex++;
                            items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                            if (items.Count() == 1)
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        MouseScrollHelper.DownStep(scollPoint, 2);
                        index++;
                        continue;
                    }
                }

                index = 0;
                //获取数据.
                foreach (var name in exceptList)
                {
                    var item = items.FirstOrDefault(x => x.Name.Equals(name));
                    RandomWait.Wait(30, 80);
                    item.DrawHighlightExt();

                    exit = __ProcessWithTime(list, item.Name, startDate, endDate, whoList, info);
                    if (exit)
                        break;
                }

                RandomWait.Wait(30, 80);
                //滚动
                MouseScrollHelper.DownStep(scollPoint, 3);
            }

            list.Reverse();
            return list;
        }

        private bool __ProcessWithTime(List<ChatSimpleMessage> list, string input, DateTime startDate, DateTime endDate, List<string> whoList, HeaderInfo info)
        {
            var pattern = @"^(.+?)\s(.*?)\s(\d{4}年\d{1,2}月\d{1,2}日\s\d{1,2}:\d{2})$";
            var match = Regex.Match(input, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                var fullDateStr = match.Groups[3].Value;
                pattern = @"(\d{4}年\d{1,2}月\d{1,2}日\s\d{1,2}:\d{2})";
                var dateStr = Regex.Match(fullDateStr, pattern).Groups[1].Value;
                var date = DateTime.ParseExact(dateStr, "yyyy年M月d日 HH:mm", CultureInfo.InvariantCulture);
                if (date < startDate)
                    return true;

                if (date >= startDate && date <= endDate)
                {
                    ChatSimpleMessage item = new ChatSimpleMessage();
                    var prefix = _GetPrefix(match.Groups[0].Value);
                    var who = __GetWhoCore(prefix, whoList);
                    item.Who = who;
                    item.Message = prefix.substring(who.Length);

                    item.SendDateTime = match.Groups[3].Value;
                    item.DateTime = date;
                    item.UniqueString = GetMd5(input);
                    list.Add(item);
                }
            }
            else
            {
                _logger.Error($"格式分析错误，未能加进消息列表，input={input}");
            }
            return false;
        }

        private string _GetPrefix(string value)
        {
            var pattern = @"^(.*?)\s+(\d{4}年\d{1,2}月\d{1,2}日\s\d{1,2}:\d{2})$";
            var match = Regex.Match(value, pattern, RegexOptions.Singleline);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }

        private ListBox __ChangeToCheckBoxState(Window subWin, DateTime startDate, DateTime endDate)
        {
            var root = subWin.FindFirstDescendant(cf => cf.ByAutomationId("search_message_list").And(cf.ByClassName("mmui::RecyclerListView")).And(cf.ByControlType(ControlType.List))).AsListBox();
            var chatItems = root.Items.Where(u => u.ControlType == ControlType.ListItem && !string.IsNullOrWhiteSpace(u.Name) && u.BoundingRectangle.Y >= root.BoundingRectangle.Y);
            if (chatItems.Count() == 0)
            {
                Mouse.Position = root.BoundingRectangle.SafeRandomPoint();
                Mouse.Scroll(5);
                RandomWait.Wait(100, 300);
                chatItems = root.Items.Where(x => x.ControlType == ControlType.ListItem && !string.IsNullOrWhiteSpace(x.Name) && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
            }
            ListBoxItem chatItem = null;
            foreach (var item in chatItems)
            {
                var value = item.Name;
                if (IsWechatDateText(item.Name.Trim()))
                {
                    chatItem = item;
                    break;
                }
            }
            if (chatItem == null)
                throw new Exception("未能弹出右键菜单，请联系作者....");

            chatItem.DrawHighlightExt();
            var dpi = DpiHelper.GetScaleForWindow(_Client.MainWindow.Properties.NativeWindowHandle);
            var initOffsetX = (int)(WeAutomation.Config.HistoryMessageOffset_X * dpi + chatItem.BoundingRectangle.X);
            var initOffsetY = (int)(WeAutomation.Config.HistoryMessageOffset_Y * dpi) + chatItem.BoundingRectangle.Y;
            var point = new Point(initOffsetX, initOffsetY);
            Mouse.Position = point;
            RandomWait.Wait(100, 400);

            Mouse.RightClick();  //得到完整的UI Tree列表
            RandomWait.Wait(100, 300);
            var menuWinRetry = Retry.WhileNull(() => subWin.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("Weixin").And(cf.ByClassName("mmui::XMenu")))).AsWindow(), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            if (menuWinRetry.Success)
            {
                menuWinRetry.Result.DrawHighlightExt();
                var menuWin = menuWinRetry.Result;
                var selectItem = menuWin.FindFirstChild(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("多选")));
                selectItem.DrawHighlightExt();
                RandomWait.Wait(300, 900);
                selectItem.Click();
                RandomWait.Wait(300, 900);
                root = subWin.FindFirstDescendant(cf => cf.ByAutomationId("search_message_list").And(cf.ByClassName("mmui::RecyclerListView")).And(cf.ByControlType(ControlType.List))).AsListBox();
            }
            return root;
        }

        private bool IsWechatDateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;
            Regex DateRegex = new(
                @"(?:星期[一二三四五六日天]|昨天|前天|\d{1,2}月\d{1,2}日)?\s*\d{1,2}:\d{2}$",
                RegexOptions.Compiled);
            return DateRegex.IsMatch(text.Trim());
        }
        private DateTime __GetDateFromName(string content)
        {
            var pattern = "";
            var prefix = "";
            var time = "";
            //先检查昨天、前天
            pattern = @"(昨天|前天)\s*(\d{1,2}:\d{2})$";
            Match match = Regex.Match(content, pattern);
            if (match.Success)
            {
                prefix = match.Groups[1].Value.Trim();
                time = match.Groups[2].Value.Trim();
                if (prefix.Equals("昨天"))
                {
                    var dateStr = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " " + time;
                    var date = DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date) ? _date : DateTime.MinValue;
                    return date;
                }
                if (prefix.Equals("前天"))
                {
                    var dateStr = DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd") + " " + time;
                    var date = DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date) ? _date : DateTime.MinValue;
                    return date;
                }
            }
            //再检查周一...周日
            pattern = @"(星期[一二三四五六日天])\s*(\d{1,2}:\d{2})$";
            match = Regex.Match(content, pattern);
            if (match.Success)
            {
                prefix = match.Groups[1].Value.Trim();
                time = match.Groups[2].Value.Trim();
                var dateStr = GetDateByWeekday(prefix, DateTime.Now).ToString("yyyy-MM-dd") + " " + time;
                var date = DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date) ? _date : DateTime.MinValue;
                return date;
            }
            //再检查M月d日 HH:mm
            pattern = @"(\d{1,2}月\d{1,2}日)\s*(\d{1,2}:\d{2})$";
            match = Regex.Match(content, pattern);
            if (match.Success)
            {
                prefix = match.Groups[1].Value.Trim();
                time = match.Groups[2].Value.Trim();
                var dateStr = DateTime.Today.Year + "年" + prefix + " " + time;
                var date = DateTime.TryParseExact(dateStr, "yyyy年M月d日 HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date) ? _date : DateTime.MinValue;
                return date;
            }

            //最后为今天日期
            pattern = @"\s*(\d{1,2}:\d{2})$";
            match = Regex.Match(content, pattern);
            if (match.Success)
            {
                time = match.Groups[1].Value.Trim();
                var dateStr = DateTime.Now.ToString("yyyy年M月d日") + " " + time;
                var date = DateTime.TryParseExact(dateStr, "yyyy年M月d日 HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _date) ? _date : DateTime.MinValue;
                return date;
            }
            throw new ArgumentException($"未知日期格式，请联系作者改正....");
        }

        private DateTime GetDateByWeekday(string weekDay, DateTime? baseDate = null)
        {
            var today = (baseDate ?? DateTime.Now).Date;

            DayOfWeek target = weekDay switch
            {
                "星期一" => DayOfWeek.Monday,
                "星期二" => DayOfWeek.Tuesday,
                "星期三" => DayOfWeek.Wednesday,
                "星期四" => DayOfWeek.Thursday,
                "星期五" => DayOfWeek.Friday,
                "星期六" => DayOfWeek.Saturday,
                "星期日" => DayOfWeek.Sunday,
                "星期天" => DayOfWeek.Sunday,
                _ => throw new ArgumentException($"未知星期格式: {weekDay}")
            };

            // 找到本周周一
            int offset = today.DayOfWeek == DayOfWeek.Sunday
                ? -6
                : DayOfWeek.Monday - today.DayOfWeek;

            var monday = today.AddDays(offset);

            return monday.AddDays(target switch
            {
                DayOfWeek.Monday => 0,
                DayOfWeek.Tuesday => 1,
                DayOfWeek.Wednesday => 2,
                DayOfWeek.Thursday => 3,
                DayOfWeek.Friday => 4,
                DayOfWeek.Saturday => 5,
                DayOfWeek.Sunday => 6,
                _ => 0
            });
        }
        private bool _IsToday(DateTime startDate)
        {
            return startDate.Date == DateTime.Today;
        }

        public static string GetMd5(string input)
        {
            // 转成字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // 创建 MD5 对象
            using (MD5 md5 = MD5.Create())
            {
                // 计算哈希
                byte[] hashBytes = md5.ComputeHash(bytes);

                // 转成16进制字符串
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // 小写
                }

                return sb.ToString();
            }
        }

        private void __ClickChatHistoryButton(Button invokeButton)
        {
            RandomWait.Wait(100, 800);
            Mouse.Position = invokeButton.BoundingRectangle.SafeRandomPoint();
            RandomWait.Wait(100, 400);
            _Client.MainWindow.Focus();
            Mouse.Click();
            RandomWait.Wait(300, 900);
        }

        /// <summary>
        /// 拍一拍
        /// 注意：只能拍一拍当前聊天窗口的好友
        /// 只有两个地方可以拍一拍：一个是群聊中，一个是好友聊天窗口（非企业微信).
        /// </summary>
        /// <param name="who">要拍一拍的好友昵称</param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        /// <returns>是否成功拍一拍</returns>
        public async Task<bool> TapWho(string who, int prevScrollNumber = 30)
        {
            return await WeChatInvoker.Call(TapWhoCore, who, prevScrollNumber);
        }

        internal bool TapWhoCore(UIA3Automation automation, string who, int prevScrollNumber)
        {
            if (string.IsNullOrWhiteSpace(who))
                return false;
            if (who.Trim().Equals(this._Client.NickName))
                return false;
            var root = this.MessageRoot;
            if (root == null)
                return false;
            ClickIfExistNewMessage(root);
            var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
            if (!title.CanTalk())
                return false;
            var index = 0; //调整位置
            AutomationElement item = null;
            AutomationElement[] items = null;
            var point = root.BoundingRectangle.SafeRandomPoint();
            while (index < 6)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                item = items.FirstOrDefault(x => !x.ClassName.Equals("mmui::ChatItemView") && !x.ClassName.Equals("mmui::ChatFolderItemView") && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
                if (item != null)
                {
                    break;
                }
                MouseScrollHelper.UpStep(point, 3);
                index++;
            }
            if (item == null)
                return false;
            var menuFlag = SelectMultiMenu(automation, title, item);
            if (!menuFlag)
                return false;
            index = 0;
            while (index < prevScrollNumber)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox).And(cf.ByClassName("mmui::ChatItemView").Not()));
                var result = false;
                foreach (var x in items)
                {
                    var aryContent = x.Name.Split(" ");
                    if (aryContent[0].Equals(who) && x.BoundingRectangle.Y >= root.BoundingRectangle.Y)
                    {
                        var runtime = x.Properties.RuntimeId.Value;
                        CloseMultiMenu();
                        RandomWait.Wait(100, 300);
                        TapCore(runtime, root);
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    CloseMultiMenu();
                    return true;
                }
                MouseScrollHelper.UpStep(point.Confusion(5, 10), 3);
                index++;
            }

            return false;
        }

        private void TapCore(int[] runtimeId, ListBox root)
        {
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByClassName("mmui::ChatItemView").Not()));
            var item = items.FirstOrDefault(cf => cf.Properties.RuntimeId.Value.SequenceEqual(runtimeId));
            if (item == null)
                return;
            var index = 0;
            var baseX = 37;
            var baseY = 26;
            var ratio = DpiHelper.GetScaleForWindow(this._Client.MainWindow.Properties.NativeWindowHandle);
            var maybeY = (int)(46 * ratio);
            //调整位置.
            while (index < 3)
            {
                if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && (item.BoundingRectangle.Y + maybeY <= root.BoundingRectangle.Y + root.BoundingRectangle.Height))
                {
                    break;
                }
                if (item.BoundingRectangle.Y < root.BoundingRectangle.Y)
                {
                    MouseScrollHelper.UpStep(root.BoundingRectangle.SafeRandomPoint(), 3);
                }
                else
                {
                    if (item.BoundingRectangle.Y + maybeY > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                    {
                        MouseScrollHelper.DownStep(root.BoundingRectangle.SafeRandomPoint(), 3);
                    }
                }

                index++;
            }
            var point = new Point(item.BoundingRectangle.X + (int)(baseX * ratio), item.BoundingRectangle.Y + (int)(baseY * ratio));
            Mouse.Position = point;
            RandomWait.Wait(100, 300);
            SupperMouseKey.MoveTo(point.Confusion(5, 5));
            RandomWait.Wait(300, 900);
            SupperMouseKey.RightClick();
            RandomWait.Wait(300, 1200);
            var path = "/Window[@Name='Weixin'][@ClassName='mmui::XMenu']";
            var menuRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (menuRetry.Success)
            {
                var menu = menuRetry.Result;
                var menuItem = menu.FindFirstChild(cf => cf.ByName("拍一拍")).AsMenuItem();
                if (menuItem != null)
                {
                    menuItem.Click();
                    RandomWait.Wait(300, 1200);
                }
            }
        }

        /// <summary>
        /// 弹出并选择右键---》选择多选
        /// </summary>
        internal bool SelectMultiMenu(UIA3Automation automation, HeaderInfo title, AutomationElement item)
        {
            var result = false;
            if (title.HeaderType == ChatType.群聊)
            {
                //先左边尝试
                result = _TryLeftGroupChatMenu(automation, item);
                if (result)
                    return true;
                //再右边尝试
                return _TryRightChatMenu(automation, item);
            }
            else
            {
                if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
                {
                    //先左边尝试
                    result = TryLeftFriendChatMenu(automation, item);
                    if (result)
                        return true;
                    //再右边尝试
                    return _TryRightChatMenu(automation, item);
                }
            }
            return result;
        }

        private bool _TryLeftGroupChatMenu(UIA3Automation automation, AutomationElement item)
            => TryLeftRightClick(item, 90, 47);

        private bool TryLeftFriendChatMenu(UIA3Automation automation, AutomationElement item)
            => TryLeftRightClick(item, 90, 26);

        private bool TryLeftRightClick(AutomationElement item, int baseX, int baseY)
        {
            RandomWait.Wait(300, 1200);
            var ratio = DpiHelper.GetScaleForWindow(this._Client.MainWindow.Properties.NativeWindowHandle);
            var point = new Point(item.BoundingRectangle.X + (int)(baseX * ratio), item.BoundingRectangle.Y + (int)(baseY * ratio));
            Mouse.Position = point;
            RandomWait.Wait(100, 300);
            SupperMouseKey.MoveTo(point.Confusion(4, 2));
            RandomWait.Wait(300, 900);
            SupperMouseKey.RightClick();
            RandomWait.Wait(300, 1200);
            //检查菜单是否打开.
            var menuRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath("/Window[@Name='Weixin'][@ClassName='mmui::XMenu']"), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (menuRetry.Success)
            {
                var menuRoot = menuRetry.Result;
                var menu = menuRoot.FindFirstChild(cf => cf.ByName("多选"));
                menu.Click();
                return true;
            }

            return false;
        }

        private bool _TryRightChatMenu(UIA3Automation automation, AutomationElement item)
        {
            var ratio = DpiHelper.GetScaleForWindow(this._Client.MainWindow.Properties.NativeWindowHandle);
            var baseX = 90;
            var baseY = 26;
            var point = new Point(item.BoundingRectangle.X + item.BoundingRectangle.Width - (int)(baseX * ratio), item.BoundingRectangle.Y + (int)(baseY * ratio));
            Mouse.Position = point;
            RandomWait.Wait(100, 300);
            SupperMouseKey.MoveTo(point.Confusion(4, 2));
            RandomWait.Wait(300, 900);
            SupperMouseKey.RightClick();
            RandomWait.Wait(300, 1200);
            //检查菜单是否打开.
            var menuRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath("/Window[@Name='Weixin'][@ClassName='mmui::XMenu']"), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (menuRetry.Success)
            {
                var menuRoot = menuRetry.Result;
                var menu = menuRoot.FindFirstChild(cf => cf.ByName("多选"));
                menu.Click();
                return true;
            }

            return false;
        }

        internal void CloseMultiMenu()
        {
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/ToolBar/Group/Button[@Name='取消多选'][@ClassName='mmui::XButton']";
            var buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (buttonRetry.Success)
            {
                var button = buttonRetry.Result.AsButton();
                button.Click();
                RandomWait.Wait(300, 900);
            }
        }

        internal void ClickIfExistNewMessage(ListBox root)
        {
            //如果有最新消息
            var existNewMessageButton = root.GetSibling(1);
            if (existNewMessageButton != null && existNewMessageButton.ControlType == ControlType.Button && existNewMessageButton.Name.Contains("新消息"))
            {
                //检查是不是最下面
                var centerPointY = root.BoundingRectangle.Center().Y;
                if (existNewMessageButton.BoundingRectangle.Y > centerPointY)
                {
                    existNewMessageButton.AsButton().Click();
                    RandomWait.Wait(300, 900);
                }
            }
        }


        /// <summary>
        /// 引用消息
        /// </summary>
        /// <param name="chatSimpleMessage">要引用的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        public async Task<bool> ReferencedMessage(ChatSimpleMessage chatSimpleMessage, int prevScrollNumber = 30)
            => await ReferencedMessage(chatSimpleMessage.Who, chatSimpleMessage.Message, prevScrollNumber);
        /// <summary>
        /// 引用最后一条消息
        /// 注意，只能引用有的消息，不会翻页，如果消息不在当前页，则不会引用
        /// </summary>
        public async Task<bool> ReferencedLastMessage()
        {
            return await Task.FromResult(true);
        }
        /// <summary>
        /// 引用消息
        /// </summary>
        /// <param name="who">要引用的好友昵称</param>
        /// <param name="message">要引用的消息内容</param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        public async Task<bool> ReferencedMessage(string who, string message, int prevScrollNumber = 30)
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 转发多条消息,默认转发最后5条消息，可以自行指定转发多少条消息
        /// </summary>
        /// <param name="who">被转发消息的好友/群聊,可以为空，则转发本窗口的消息</param>
        /// <param name="to">要转发给谁</param>
        /// <param name="fType">消息转发类型，详情请参见<see cref="ForwardMessageTypeEnums"/></param>
        /// <param name="rowCount">要转发多少条消息，默认是最后的5条消息,如果当前没有5条，则转发所有消息</param>
        public async Task<bool> ForwardMultipleMessage(string who, OneOf<string, string[]> to, ForwardMessageTypeEnums fType = ForwardMessageTypeEnums.ForwardMerge, int rowCount = 5)
        {
            if (string.IsNullOrWhiteSpace(who))
            {
                var chatInfo = await _Client.ChatContent.ChatHeader.GetTitle();
                if (!chatInfo.CanTalk())
                {
                    return false;
                }
            }
            else
            {
                await _Client.SearchFriend(who);
            }
            RandomWait.Wait(300, 1000);
            return await WeChatInvoker.Call(ForwardMultipleMessageCore, to, fType, rowCount);
        }

        /// <summary>
        /// 转发多条消息,默认转发最后5条消息，可以自行指定转发多少条消息
        /// 注意：只能转发本窗口的消息
        /// </summary>
        /// <param name="to">要转发给谁</param>
        /// <param name="fType">消息转发类型，详情请参见<see cref="ForwardMessageTypeEnums"/></param>
        /// <param name="rowCount">要转发多少条消息，默认是最后的5条消息,如果当前没有5条，则转发所有消息</param>
        public async Task<bool> ForwardMultipleMessage(OneOf<string, string[]> to, ForwardMessageTypeEnums fType = ForwardMessageTypeEnums.ForwardMerge, int rowCount = 5)
        {
            return await WeChatInvoker.Call(ForwardMultipleMessageCore, to, fType, rowCount);
        }

        private bool ForwardMultipleMessageCore(UIA3Automation automation, OneOf<string, string[]> to, ForwardMessageTypeEnums fType, int rowCount)
        {
            var toWhos = to.IsT0 ? new List<string> { to.AsT0 } : to.AsT1.ToList();
            if (toWhos.Count == 0)
                return false;
            var root = this.MessageRoot;
            if (root == null)
                return false;
            ClickIfExistNewMessage(root);
            var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
            if (!title.CanTalk())
                return false;
            _ToEndPosition(root);
            var index = 0; //调整位置
            AutomationElement item = null;
            AutomationElement[] items = null;
            var point = root.BoundingRectangle.SafeRandomPoint();
            while (index < 12)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                item = items.FirstOrDefault(x => !x.ClassName.Equals("mmui::ChatItemView") && !x.ClassName.Equals("mmui::ChatFolderItemView") && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
                if (item != null)
                {
                    break;
                }
                MouseScrollHelper.UpStep(point.Confusion(10, 10), 3);
                index++;
            }
            if (item == null)
                return false;
            var menuFlag = SelectMultiMenu(automation, title, item);
            if (!menuFlag)
                return false;
            //去掉当前checkbox选择
            var itemCheck = root.FindAllChildren(x => x.ByControlType(ControlType.CheckBox)).Where(x => x.Properties.RuntimeId.Value.SequenceEqual(item.Properties.RuntimeId.Value)).FirstOrDefault();
            if (itemCheck != null)
            {
                if (itemCheck.Patterns.Toggle.Pattern.ToggleState == ToggleState.On)
                {
                    var clkPoint = itemCheck.BoundingRectangle.SafeRandomPoint();
                    Mouse.Position = clkPoint;
                    RandomWait.Wait(100, 300);
                    SupperMouseKey.MoveTo(Mouse.Position.Confusion(5, 5));
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.LeftClick();
                    RandomWait.Wait(300, 900);
                }
            }
            //checkbox打上勾
            index = 0;
            var count = 0;
            var oldSnap = new List<string>();
            while (index < 30)
            {
                var itemChecks = root.FindAllChildren(x => x.ByControlType(ControlType.CheckBox)).Where(x => x.BoundingRectangle.Y >= root.BoundingRectangle.Y && x.BoundingRectangle.Y + 40 <= root.BoundingRectangle.Y + root.BoundingRectangle.Height);
                var newSnap = itemChecks.Select(x => x.Name + "_" + x.Properties.RuntimeId.ToUniqueString()).ToList();
                var exceptList = newSnap.Except(oldSnap).ToList();
                var result = false;
                if (exceptList.Count > 0)
                {
                    index = 0;
                    oldSnap = newSnap;
                    exceptList.Reverse();
                    //从下往上点.
                    foreach (var str in exceptList)
                    {
                        itemCheck = itemChecks.Where(x => (x.Name + "_" + x.Properties.RuntimeId.ToUniqueString()).Equals(str)).FirstOrDefault();
                        if (itemCheck != null)
                        {
                            var clkPoint = Point.Empty;
                            if (itemCheck.BoundingRectangle.Y + itemCheck.BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                            {
                                var rect = new Rectangle(itemCheck.BoundingRectangle.X, itemCheck.BoundingRectangle.Y, itemCheck.BoundingRectangle.Width, root.BoundingRectangle.Y + root.BoundingRectangle.Height - itemCheck.BoundingRectangle.Y);
                                clkPoint = rect.SafeRandomPoint();
                            }
                            else
                            {
                                clkPoint = itemCheck.BoundingRectangle.SafeRandomPoint();
                            }
                            Mouse.Position = clkPoint;
                            RandomWait.Wait(100, 300);
                            SupperMouseKey.MoveTo(Mouse.Position.Confusion(5, 5));
                            RandomWait.Wait(300, 900);
                            SupperMouseKey.LeftClick();
                            RandomWait.Wait(300, 900);
                            count++;
                            if (count >= rowCount)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                if (result)
                    break;

                MouseScrollHelper.UpStep(point.Confusion(10, 10), 3);
                index++;
            }
            //转发
            var flag = OnlyForwardSingleMessageCore(automation, to, fType);
            if (!flag)
            {
                CloseMultiMenu();  //关闭多选窗口
            }
            return flag;
        }


        private void _ToEndPosition(ListBox root)
        {
            var index = 0;
            var point = root.BoundingRectangle.SafeRandomPoint();
            var oldSnapshot = new List<string>();
            while (index < 3)
            {
                var newSnapshot = root.FindAllChildren().Select(x => x.Name + "_" + x.Properties.RuntimeId.ToUniqueString()).ToList();
                var excptList = newSnapshot.Except(oldSnapshot);
                if (excptList.Count() > 0)
                {
                    oldSnapshot = newSnapshot;
                    MouseScrollHelper.DownStep(point.Confusion(10, 10), 5);
                    index = 0;
                    continue;
                }
                MouseScrollHelper.DownStep(point.Confusion(10, 10), 5);
                index++;
            }
        }
        #region xx
        // var result = _uiThreadInvoker.Run(automation =>
        // {
        //     List<ListBoxItem> _WillProcessItems = _GetWillForwardMessageList(rowCount);  //得到所有要转发的消息

        //     // 前置操作，如果有图片、视频、语音，则先处理
        //     var r = EnsureSuccess(_PreImageVedioMessage(_WillProcessItems));
        //     if (!r.Success) return r;

        //     // 选择要转发多少条消息
        //     r = EnsureSuccess(_SelectMultipleMessage(_WillProcessItems));
        //     if (!r.Success) return r;

        //     r = EnsureSuccess(_ProcessMaybeError());
        //     if (!r.Success) return r;

        //     // 转发消息
        //     r = EnsureSuccess(_ForwardMessageCore(to));
        //     if (!r.Success) return r;

        //     r = EnsureSuccess(_ProcessMaybeError());
        //     if (!r.Success) return r;

        //     // 如果需要截图，则进行截图
        //     if (isCapture)
        //     {
        //         r = EnsureSuccess(_CaptureMultipleMessage(_WillProcessItems, to));
        //         if (!r.Success) return r;
        //     }

        //     return Result.Ok();
        // })
        // .GetAwaiter().GetResult();
        // if (result.Success && isCapture)
        // {
        //     var from = this._ChatBody.ChatContent.ChatHeader.Title.Title; //得到发送者
        //     this._ChatBody.ChatContent.MainWxWindow.PasteContentToWho(to).GetAwaiter().GetResult();
        //     //转回from
        //     this._ChatBody.ChatContent.MainWxWindow.FocusWho(from);
        // }
        // else
        // {
        //     _logger.Error($"转发失败: {result.Error}");
        // }
        // /// <summary>
        // /// 检查结果，如果失败则返回失败，否则返回成功的结果以便继续链式调用
        // /// </summary>
        // private Result EnsureSuccess(Result result)
        // {
        //     return result.Success ? Result.Ok() : Result.Fail(result.Error);
        // }
        #endregion

        /// <summary>
        /// 转发单条消息
        /// 注意：仅限于本窗口单条转发消息
        /// 流程：
        /// 1. 找到这一条消息,倒序找，这里注意一点，如果找不到消息，自动往前滚动，如果找不到，则不会转发此消息,日志显示错误，但不会报错.
        /// 2. 右键点击这一条消息
        /// 3. 找到菜单
        /// 4. 找到发送人
        /// </summary>
        /// <param name="to">要转发给谁</param>
        /// <param name="chatSimpleMessage">要转发的消息<see cref="ChatSimpleMessage"/></param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前翻页的次数</param>
        public async Task<bool> ForwardSingleMessage(ChatSimpleMessage chatSimpleMessage, OneOf<string, string[]> to, int prevScrollNumber = 30)
        => await ForwardSingleMessage(chatSimpleMessage.Who, chatSimpleMessage.Message, to, prevScrollNumber);

        /// <summary>
        /// 转发单条消息
        /// </summary>
        /// <param name="who">要转发的好友昵称</param>
        /// <param name="message">要转发的消息内容</param>
        /// <param name="to">要转发给谁,可以多人/群</param>
        /// <param name="prevScrollNumber">如果当前页找不到，往前滚动的次数</param>
        public async Task<bool> ForwardSingleMessage(string who, string message, OneOf<string, string[]> to, int prevScrollNumber = 30)
        {
            return await WeChatInvoker.Call(ForwardSingleMessageCore, who, message, to, prevScrollNumber);
        }

        private bool ForwardSingleMessageCore(UIA3Automation automation, string who, string message, OneOf<string, string[]> to, int prevScrollNumber)
        {
            if (string.IsNullOrWhiteSpace(who))
                return false;
            var root = this.MessageRoot;
            if (root == null)
                return false;
            ClickIfExistNewMessage(root);
            var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
            if (!title.CanTalk())
                return false;
            var index = 0; //调整位置
            AutomationElement item = null;
            AutomationElement[] items = null;
            var point = root.BoundingRectangle.SafeRandomPoint();
            while (index < 6)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                item = items.FirstOrDefault(x => !x.ClassName.Equals("mmui::ChatItemView") && !x.ClassName.Equals("mmui::ChatFolderItemView") && x.BoundingRectangle.Y >= root.BoundingRectangle.Y);
                if (item != null)
                {
                    break;
                }
                MouseScrollHelper.UpStep(point, 3);
                index++;
            }
            if (item == null)
                return false;
            var menuFlag = SelectMultiMenu(automation, title, item);
            if (!menuFlag)
                return false;
            index = 0;
            while (index < prevScrollNumber)
            {
                items = root.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                foreach (var subItem in items)
                {
                    var subCheckBox = subItem.AsCheckBox();
                    if (subCheckBox.Patterns.Toggle.IsSupported)
                    {
                        var pattern = subCheckBox.Patterns.Toggle;
                        if (pattern.Pattern.ToggleState == ToggleState.On)
                        {
                            var clkPoint = subItem.BoundingRectangle.SafeRandomPoint();
                            Mouse.Position = clkPoint;
                            RandomWait.Wait(100, 300);
                            SupperMouseKey.MoveTo(Mouse.Position.Confusion(5, 5));
                            RandomWait.Wait(300, 900);
                            SupperMouseKey.LeftClick();
                        }
                    }
                }
                foreach (var subItem in items)
                {
                    var subCheckBox = subItem.AsCheckBox();
                    var aryContent = subItem.Name.Split(' ');
                    if (aryContent.Length < 2)
                        continue;
                    if (aryContent[0].Equals(who) && aryContent[1].Equals(message))
                    {
                        //调整位置
                        var count = 0;
                        while (count < 6)
                        {
                            if (subCheckBox.BoundingRectangle.Y >= root.BoundingRectangle.Y)
                            {
                                break;
                            }
                            MouseScrollHelper.UpStep(point, 3);
                            count++;
                        }
                        var pattern = subCheckBox.Patterns.Toggle;
                        if (pattern.Pattern.ToggleState != ToggleState.On)
                        {
                            var clkPoint = subItem.BoundingRectangle.SafeRandomPoint();
                            Mouse.Position = clkPoint;
                            RandomWait.Wait(100, 300);
                            SupperMouseKey.MoveTo(Mouse.Position.Confusion(5, 5));
                            RandomWait.Wait(300, 900);
                            SupperMouseKey.LeftClick();
                        }
                        var result = OnlyForwardSingleMessageCore(automation, to, ForwardMessageTypeEnums.ForwardOneByOne);
                        if (!result)
                        {
                            CloseMultiMenu();  //关闭多选窗口
                        }
                        return result;
                    }
                }

                MouseScrollHelper.UpStep(point, 3);
                index++;
            }

            CloseMultiMenu();  //关闭多选窗口
            return false;
        }

        internal bool OnlyForwardSingleMessageCore(UIA3Automation automation, OneOf<string, string[]> to, ForwardMessageTypeEnums fType)
        {
            var toWhos = to.IsT0 ? new List<string> { to.AsT0 } : to.AsT1.ToList();
            var path = fType == ForwardMessageTypeEnums.ForwardOneByOne ?
            "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/ToolBar/Group/Button[@Name='逐条转发'][@ClassName='mmui::MultiSelectToolIButtonTexttem']"
            :
            "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Custom/Group/ToolBar/Group/Button[@Name='合并转发'][@ClassName='mmui::MultiSelectToolIButtonTexttem']";
            var buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (buttonRetry.Success)
            {
                var button = buttonRetry.Result.AsButton();
                Mouse.Position = button.BoundingRectangle.SafeRandomPoint();
                RandomWait.Wait(100, 300);
                SupperMouseKey.MoveTo(Mouse.Position.Confusion(5, 5));
                RandomWait.Wait(300, 900);
                SupperMouseKey.LeftClick();
                RandomWait.Wait(300, 900);
                path = "/Window[@Name='微信发送给'][@ClassName='mmui::SessionPickerWindow']/Group/Group/Group/Edit[@Name='搜索'][@ClassName='mmui::XValidatorTextEdit']";
                var edit = this._Client.MainWindow.FindFirstByXPath(path);
                if (edit != null)
                {
                    foreach (var who in toWhos)
                    {
                        path = "/Window[@Name='微信发送给'][@ClassName='mmui::SessionPickerWindow']/Group/Group/Group/Edit[@Name='搜索'][@ClassName='mmui::XValidatorTextEdit']";
                        edit = this._Client.MainWindow.FindFirstByXPath(path);
                        var groupRoot = edit.GetParent();
                        var clearButton = groupRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("清空")).And(cf.ByClassName("mmui::XButton")));
                        if (clearButton != null)
                        {
                            clearButton.Click();
                            RandomWait.Wait(300, 900);
                        }
                        edit = this._Client.MainWindow.FindFirstByXPath(path);
                        edit.AsTextBox().Text = who;
                        RandomWait.Wait(800, 2000);
                        path = "/Window[@Name='微信发送给'][@ClassName='mmui::SessionPickerWindow']/Group/Group/List[@Name='请勾选需要添加的联系人'][@AutomationId='sp_search_result_list']";
                        var searchRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                        if (searchRetry.Success)
                        {
                            var searchList = searchRetry.Result.AsListBox();
                            var items = searchList.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                            var item = items.FirstOrDefault(x => x.Name.Trim().Equals(who.Trim()));
                            if (item != null)
                            {
                                if (item.Patterns.Toggle.IsSupported)
                                {
                                    if (item.Patterns.Toggle.Pattern.ToggleState != ToggleState.On)
                                    {
                                        var point = item.BoundingRectangle.SafeRandomPoint();
                                        Mouse.Position = point;
                                        RandomWait.Wait(100, 300);
                                        SupperMouseKey.MoveTo(point.Confusion(10, 5));
                                        RandomWait.Wait(300, 900);
                                        SupperMouseKey.LeftClick();
                                        RandomWait.Wait(600, 1500);
                                    }
                                }
                            }
                        }
                    }
                    //查询是否有数据
                    path = "/Window[@Name='微信发送给'][@ClassName='mmui::SessionPickerWindow']/Group/Group/Button[@AutomationId='confirm_btn']";
                    var confirmRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                    if (confirmRetry.Success)
                    {
                        var confirm = confirmRetry.Result;
                        if (confirm.IsEnabled)
                        {
                            var point = confirm.BoundingRectangle.SafeRandomPoint();
                            Mouse.Position = point;
                            RandomWait.Wait(100, 300);
                            SupperMouseKey.MoveTo(point.Confusion(10, 5));
                            RandomWait.Wait(300, 900);
                            SupperMouseKey.LeftClick();
                            RandomWait.Wait(600, 1500);
                            return true;
                        }
                    }
                    else
                    {
                        SupperMouseKey.TypeSimultaneously(VirtualKeyShort.ESC);
                        RandomWait.Wait(600, 1500);
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 获取开始日期到结束日期之间的所有日期（包含首尾）
        /// </summary>
        private List<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            var result = new List<DateTime>();

            // 只保留日期部分
            var checkStartDate = startDate.Date;
            var checkEndDate = endDate.Date;

            // 防止开始日期大于结束日期
            if (checkStartDate > checkEndDate)
                return result;
            result.Add(startDate);
            result.Add(endDate);


            return result;
        }

    }
}
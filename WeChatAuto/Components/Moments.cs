using System;
using FlaUI.Core.AutomationElements;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Definitions;
using WeChatAuto.Utils;
using WxAutoCommon.Utils;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;
using System.Collections.Generic;
using WxAutoCommon.Models;
using System.Threading;
using System.Diagnostics;
using FlaUI.Core.Input;
using WeChatAuto.Extentions;
using FlaUI.UIA3;
using OneOf;
using System.Threading.Tasks;
using System.Linq;
using FlaUI.Core.Patterns;
using WeAutoCommon.Utils;
using WeChatAuto.Services;
using WxAutoCommon.Simulator;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 朋友圈
    /// </summary>
    public class Moments : IDisposable
    {
        private const double SCROLL_STEP = 0.05;
        private readonly AutoLogger<Moments> _logger;
        private Window _MainWindow;
        private WeChatMainWindow _WxMainWindow;
        private volatile bool _disposed = false;
        private readonly UIThreadInvoker _SelfUiThreadInvoker;
        private readonly UIThreadInvoker _MainUIThreadInvoker;
        private CancellationTokenSource _ListenerCancellationTokenSource;
        private System.Threading.Timer _pollingTimer;
        private volatile bool _isProcessing = false;
        private IServiceProvider _ServiceProvider;


        public Moments(Window window, WeChatMainWindow wxWindow, UIThreadInvoker mainUIThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<Moments>>();
            _MainWindow = window;
            _WxMainWindow = wxWindow;
            _MainUIThreadInvoker = mainUIThreadInvoker;
            _SelfUiThreadInvoker = new UIThreadInvoker();
            _ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 判断朋友圈是否打开
        /// </summary>
        /// <returns>是否打开</returns>
        public bool IsMomentsOpen()
        {
            try
            {
                bool result = _SelfUiThreadInvoker.Run(automation =>
                            {
                                var deskTop = automation.GetDesktop();
                                var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                                    timeout: TimeSpan.FromSeconds(3),
                                    interval: TimeSpan.FromMilliseconds(200));
                                if (window.Success && window.Result != null)
                                {
                                    return true;
                                }
                                return false;
                            }).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }
        }
        /// <summary>
        /// 打开朋友圈
        /// </summary>
        public void OpenMoments()
        {
            if (this.IsMomentsOpen())
                return;
            try
            {
                this._WxMainWindow.Navigation.SwitchNavigation(NavigationType.朋友圈);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                this._WxMainWindow.Navigation.SwitchNavigation(NavigationType.聊天);
            }
        }
        /// <summary>
        /// 获取朋友圈内容列表
        /// 注意：此方法会让朋友圈窗口获取焦点,可能会导致其他窗口失去焦点.
        /// </summary>
        /// <param name="count">鼠标滚动次数,一次滚动5行</param>
        /// <returns>朋友圈内容列表<see cref="MomentItem"/></returns>
        public List<MomentItem> GetMomentsList(int count = 20)
        {
            if (_disposed)
                return null;
            this.OpenMoments();
            var result = _MainUIThreadInvoker.Run(automation =>
            {
                var deskTop = automation.GetDesktop();
                var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                    timeout: TimeSpan.FromSeconds(3),
                    interval: TimeSpan.FromMilliseconds(200));
                var momentWindow = window.Success ? window.Result : null;
                momentWindow.DrawHighlightExt();
                momentWindow.Focus();
                var momentsList = new List<MomentItem>();
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                if (rootListBox.Patterns.Scroll.IsSupported)
                {
                    var pattern = rootListBox.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 0);
                    Thread.Sleep(600);
                    Mouse.Position = rootListBox.BoundingRectangle.Center();
                    var i = 1;
                    MomentsHelper momentsHelper = new MomentsHelper();
                    while (true && i <= count)
                    {
                        momentWindow.Focus();
                        Mouse.Position = rootListBox.BoundingRectangle.Center();
                        Mouse.Scroll(-5);
                        Thread.Sleep(600);
                        var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        foreach (var item in items)
                        {
                            var monentItem = momentsHelper.ParseMonentItem(item.AsListBoxItem(), _WxMainWindow.NickName);
                            if (monentItem != null && !momentsList.Exists(m => m.Who == monentItem.Who && m.Content == monentItem.Content && m.Time == monentItem.Time))
                            {
                                momentsList.Add(monentItem);
                            }
                        }
                        i++;
                    }
                }

                return momentsList;
            }).Result;
            return result;
        }
        /// <summary>
        /// 获取朋友圈内容列表,静默模式
        /// </summary>
        /// <returns>朋友圈内容列表<see cref="MomentItem"/></returns>
        public List<MomentItem> GetMomentsListSilence()
        {
            if (_disposed)
                return null;
            this.OpenMoments();
            var result = _SelfUiThreadInvoker.Run(automation =>
            {
                var deskTop = automation.GetDesktop();
                var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                    timeout: TimeSpan.FromSeconds(3),
                    interval: TimeSpan.FromMilliseconds(200));
                var momentWindow = window.Success ? window.Result : null;

                var momentsList = new List<MomentItem>();
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                if (rootListBox.Patterns.Scroll.IsSupported)
                {
                    var pattern = rootListBox.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 0);
                    Thread.Sleep(600);
                    double scrollAmount = 0;
                    while (true)
                    {
                        var isEnd = this.AddMomentsItemAndReturn(momentsList, rootListBox);
                        if (isEnd)
                            break;
                        scrollAmount += SCROLL_STEP;
                        pattern.SetScrollPercent(0, scrollAmount);
                        Thread.Sleep(600);
                    }
                }

                return momentsList;
            }).Result;
            return result;
        }

        /// <summary>
        /// 获取朋友圈内容列表,静默模式,简单模式
        /// 从性能角度考虑，仅比较有不同.
        /// </summary>
        /// <returns>朋友圈内容列表<see cref="MomentItem"/></returns>
        public List<MomentItem> GetShortMomentsList()
        {
            if (_disposed)
                return null;
            this.OpenMoments();
            var result = _SelfUiThreadInvoker.Run(automation =>
            {
                var deskTop = automation.GetDesktop();
                var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                    timeout: TimeSpan.FromSeconds(3),
                    interval: TimeSpan.FromMilliseconds(200));
                var momentWindow = window.Success ? window.Result : null;

                //首先点击刷新
                _ClickRefreshButton(momentWindow);
                //获取当前朋友圈列表
                var momentsList = new List<MomentItem>();
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                this.AddMomentsItemAndReturn(momentsList, rootListBox);

                return momentsList;
            }).Result;
            return result;
        }

        private void _ClickRefreshButton(Window momentWindow)
        {
            var xPath = "/Pane[2]/ToolBar";
            var toolBar = momentWindow.FindFirstByXPath(xPath);
            if (toolBar != null)
            {
                var refreshButton = toolBar?.FindFirstByXPath("//Button[@Name='刷新']")?.AsButton();
                refreshButton?.DrawHighlightExt();
                if (refreshButton != null)
                {
                    Thread.Sleep(600);
                    momentWindow.SilenceClickExt(refreshButton);   //静默点击刷新按钮
                    Thread.Sleep(600);
                }
                else
                {
                    _logger.Error("刷新朋友圈失败，刷新按钮未找到");
                    throw new Exception("刷新朋友圈失败，刷新按钮未找到");
                }
            }
            else
            {
                _logger.Error("刷新朋友圈失败，工具栏未找到");
                throw new Exception("刷新朋友圈失败，工具栏未找到");
            }
        }

        private bool AddMomentsItemAndReturn(List<MomentItem> momentsList, ListBox rootListBox)
        {
            var result = false;
            MomentsHelper momentsHelper = new MomentsHelper();
            var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            if (items.Any(item => string.IsNullOrWhiteSpace(item.Name)))
            {
                result = true;
                items = items.Where(item => !string.IsNullOrWhiteSpace(item.Name)).ToArray();
            }
            foreach (var item in items)
            {
                var monentItem = momentsHelper.ParseMonentItem(item.AsListBoxItem(), _WxMainWindow.NickName);
                if (monentItem != null && !momentsList.Exists(m => m.Who == monentItem.Who && m.Content == monentItem.Content && m.Time == monentItem.Time))
                {
                    momentsList.Add(monentItem);
                }
            }
            return result;
        }

        private (List<MomentItem> list, bool isEnd) _GetCurentMomentsItems(ListBox rootListBox)
        {
            bool isEnd = false;
            var list = new List<MomentItem>();
            MomentsHelper momentsHelper = new MomentsHelper();
            var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            if (items.Any(item => string.IsNullOrWhiteSpace(item.Name)))
            {
                isEnd = true;
                items = items.Where(item => !string.IsNullOrWhiteSpace(item.Name)).ToArray();
            }
            foreach (var item in items)
            {
                var monentItem = momentsHelper.ParseMonentItem(item.AsListBoxItem(), _WxMainWindow.NickName);
                list.Add(monentItem);
            }
            return (list, isEnd);
        }

        private void AddMomentsItem(List<MomentItem> momentsList, ListBox rootListBox)
        {
            MomentsHelper momentsHelper = new MomentsHelper();
            var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            items = items.Where(item => !string.IsNullOrWhiteSpace(item.Name)).ToArray();
            foreach (var item in items)
            {
                var monentItem = momentsHelper.ParseMonentItem(item.AsListBoxItem(), _WxMainWindow.NickName);
                if (monentItem != null && !momentsList.Exists(m => m.Who == monentItem.Who && m.Content == monentItem.Content && m.Time == monentItem.Time))
                {
                    momentsList.Add(monentItem);
                }
            }
        }

        /// <summary>
        /// 刷新朋友圈
        /// </summary>
        public void RefreshMomentsList()
        {
            if (_disposed)
                return;
            _logger.Info("刷新朋友圈开始...");
            _SelfUiThreadInvoker.Run(automation =>
            {
                try
                {
                    var momentWindow = _GetMomentsWindow(automation);
                    if (momentWindow == null)
                        throw new Exception("朋友圈窗口未找到");

                    var xPath = "//ToolBar";
                    var toolBar = momentWindow.FindFirstByXPath(xPath);
                    if (toolBar != null)
                    {
                        var refreshButton = toolBar?.FindFirstByXPath("//Button[@Name='刷新']")?.AsButton();
                        refreshButton?.DrawHighlightExt();
                        if (refreshButton != null)
                        {
                            Thread.Sleep(600);
                            momentWindow.SilenceClickExt(refreshButton);   //静默点击刷新按钮
                            Thread.Sleep(600);
                        }
                        else
                        {
                            _logger.Error("刷新朋友圈失败，刷新按钮未找到");
                        }
                    }
                    else
                    {
                        _logger.Error("刷新朋友圈失败，工具栏未找到");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("刷新朋友圈失败，" + ex.Message, ex);
                    throw;
                }
            }).GetAwaiter().GetResult();
            _logger.Info("刷新朋友圈结束...");
        }


        private void _RefreshMomentsListCore(Window momentWindow)
        {
            var xPath = "//ToolBar";
            var toolBar = momentWindow.FindFirstByXPath(xPath);
            if (toolBar != null)
            {
                var refreshButton = toolBar?.FindFirstByXPath("//Button[@Name='刷新']")?.AsButton();
                refreshButton?.DrawHighlightExt();
                if (refreshButton != null)
                {
                    refreshButton.WaitUntilClickable();
                    momentWindow.SilenceClickExt(refreshButton);   //静默点击刷新按钮
                    Thread.Sleep(1000);
                }
                else
                {
                    _logger.Error("刷新朋友圈失败，刷新按钮未找到");
                }
            }
        }

        private void LikeMomentsItem(List<MomentItem> currentMomentsList, string myNickName, string[] searchNickNames, ref double scrollAmount, ListBox rootListBox, IScrollPattern pattern, Window momentWindow, List<MomentItem> willDoList = null)
        {
            foreach (var moment in currentMomentsList)
            {
                Mouse.Position = momentWindow.BoundingRectangle.Center();
                if (searchNickNames.Contains(moment.Who) && (willDoList == null || willDoList.Contains(moment)))
                {
                    if (!moment.IsMyLiked)
                    {
                        var name = moment.ListItemKey;
                        var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                        var item = items.FirstOrDefault(u => MomentItem.GetListItemKey(u.Name) == name).AsListBoxItem();
                        if (item != null)
                        {
                            var xPath = "//Button[@Name='评论']";
                            var button = item.FindFirstByXPath(xPath)?.AsButton();
                            var index = 0;
                            while (button.IsOffscreen && index < 3)
                            {
                                scrollAmount += SCROLL_STEP;
                                pattern.SetScrollPercent(0, scrollAmount);
                                Thread.Sleep(600);
                                button = item.FindFirstByXPath(xPath)?.AsButton();
                                index++;
                            }
                            button.WaitUntilClickable();
                            momentWindow.Focus();
                            button.DrawHighlightExt();
                            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                            {
                                KMSimulatorService.LeftClick(momentWindow, button);
                            }
                            else
                            {
                                button.Click();
                            }
                            Thread.Sleep(600);
                            xPath = "//Button[1][@Name='赞']";
                            var linkButtonResult = Retry.WhileNull(() => momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                                timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                            if (linkButtonResult.Success && linkButtonResult.Result != null)
                            {
                                var linkButton = linkButtonResult.Result;
                                linkButton.WaitUntilClickable();
                                linkButton.DrawHighlightExt();
                                if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                                {
                                    var point = DpiHelper.GetDpiAwarePoint(momentWindow,linkButton);
                                    ClickHighlighter.ShowClick(point);
                                    KMSimulatorService.LeftClick(point);
                                }
                                else
                                {
                                    linkButton.Click();
                                }
                                Thread.Sleep(600);
                            } else
                            {
                                _logger.Trace("点赞按钮未找到");
                            }
                        }
                        else
                        {
                            _logger.Trace("未找到指定的朋友圈列表项:" + name);
                        }
                    }
                }
            }
        }

        private Window _GetMomentWindow(UIA3Automation automation)
        {
            var deskTop = automation.GetDesktop();
            var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                timeout: TimeSpan.FromSeconds(3),
                interval: TimeSpan.FromMilliseconds(200));
            var momentWindow = window.Success ? window.Result : null;
            momentWindow.DrawHighlightExt();
            return momentWindow;
        }

        /// <summary>
        /// 点赞朋友圈
        /// </summary>
        /// <param name="nickNames">好友名称或好友名称列表</param>
        public void LikeMoments(OneOf<string, string[]> nickNames) => this._LikeMomentsCore(nickNames);

        /// <summary>
        /// 点赞朋友圈
        /// </summary>
        /// <param name="nickNames">好友名称或好友名称列表</param>
        /// <param name="willDoList">待处理列表</param>
        private void _LikeMomentsCore(OneOf<string, string[]> nickNames, List<MomentItem> willDoList = null)
        {
            if (_disposed)
                return;
            _logger.Info("点赞朋友圈开始...");
            if (IsMomentsOpen())
            {
                _SelfUiThreadInvoker.Run(automation =>
                {
                    var win = _GetMomentWindow(automation);
                    win.Close();
                    Thread.Sleep(600);
                }).GetAwaiter().GetResult();
            }
            OpenMoments();
            var myNickName = _WxMainWindow.NickName;
            var searchNickNames = nickNames.Value is string nickName ? new string[] { nickName } : nickNames.Value as string[];
            _SelfUiThreadInvoker.Run(automation =>
            {
                Window momentWindow = _GetMomentWindow(automation);
                //先刷新朋友圈列表
                this._RefreshMomentsListCore(momentWindow);
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                if (rootListBox.Patterns.Scroll.IsSupported)
                {
                    var pattern = rootListBox.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 0);
                    Thread.Sleep(600);
                    double scrollAmount = 0;
                    while (true)
                    {
                        var (mList, isEnd) = this._GetCurentMomentsItems(rootListBox);
                        var flag = willDoList == null ? mList.Any(m => searchNickNames.Contains(m.Who)) : mList.Any(m => searchNickNames.Contains(m.Who) && willDoList.Contains(m));
                        if (flag)
                        {
                            this.LikeMomentsItem(mList, myNickName, searchNickNames, ref scrollAmount, rootListBox, pattern, momentWindow, willDoList);
                        }
                        if (isEnd)
                            break;
                        scrollAmount += SCROLL_STEP;
                        pattern.SetScrollPercent(0, scrollAmount);
                        Thread.Sleep(600);
                    }

                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 回复朋友圈
        /// </summary>
        /// <param name="nickNames">好友名称或好友名称列表</param>
        /// <param name="replyContent">回复内容</param>
        public void ReplyMoments(OneOf<string, string[]> nickNames, string replyContent)
        {
            if (_disposed)
                return;
            _logger.Info("回复朋友圈开始...");
            var myNickName = _WxMainWindow.NickName;
            var searchNickNames = nickNames.Value is string nickName ? new string[] { nickName } : nickNames.Value as string[];
            _SelfUiThreadInvoker.Run(automation =>
            {
                Window momentWindow = _GetMomentWindow(automation);
                //先刷新朋友圈列表
                this._RefreshMomentsListCore(momentWindow);
                var momentsList = new List<MomentItem>();
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                if (rootListBox.Patterns.Scroll.IsSupported)
                {
                    var pattern = rootListBox.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 0);
                    Thread.Sleep(600);
                    double scrollAmount = 0;
                    while (true)
                    {
                        var (mList, isEnd) = this._GetCurentMomentsItems(rootListBox);
                        var flag = mList.Any(m => searchNickNames.Contains(m.Who));
                        if (flag)
                        {
                            this.ReplyMomentsItem(mList, myNickName, searchNickNames, ref scrollAmount, rootListBox, pattern, momentWindow, replyContent);
                        }
                        if (isEnd)
                            break;
                        scrollAmount += SCROLL_STEP;
                        pattern.SetScrollPercent(0, scrollAmount);
                        Thread.Sleep(600);
                    }

                }
            }).GetAwaiter().GetResult();
        }

        private void ReplyMomentsItem(List<MomentItem> currentMomentsList, string myNickName, string[] searchNickNames, ref double scrollAmount, ListBox rootListBox, IScrollPattern pattern, Window momentWindow, string replyContent)
        {
            foreach (var moment in currentMomentsList)
            {
                Mouse.Position = momentWindow.BoundingRectangle.Center();
                if (searchNickNames.Contains(moment.Who))
                {
                    if (!moment.IsMyEndReply)
                    {
                        var name = moment.ListItemName;
                        var item = rootListBox.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName(name)))?.AsListBoxItem();
                        if (item != null)
                        {
                            var xPath = "//Button[@Name='评论']";
                            var button = item.FindFirstByXPath(xPath)?.AsButton();
                            var index = 0;
                            while (button.IsOffscreen && index < 3)
                            {
                                scrollAmount += SCROLL_STEP;
                                pattern.SetScrollPercent(0, scrollAmount);
                                Thread.Sleep(600);
                                button = item.FindFirstByXPath(xPath)?.AsButton();
                                index++;
                            }
                            button.WaitUntilClickable();
                            momentWindow.Focus();
                            button.DrawHighlightExt();
                            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                            {
                                KMSimulatorService.LeftClick(momentWindow, button);
                            }
                            else
                            {
                                button.Click();
                            }
                            Thread.Sleep(600);
                            xPath = "//Button[2][@Name='评论']";
                            var replyButtonResult = Retry.WhileNull(() => momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                                timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                            if (replyButtonResult.Success && replyButtonResult.Result != null)
                            {
                                var replyButton = replyButtonResult.Result;
                                replyButton.WaitUntilClickable();
                                replyButton.DrawHighlightExt();
                                if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                                {
                                    KMSimulatorService.LeftClick(momentWindow, replyButton);
                                }
                                else
                                {
                                    replyButton.Click();
                                }
                                Thread.Sleep(600);
                                ReplyContentCore(momentWindow, ref scrollAmount, rootListBox, pattern, replyContent);
                            }
                        }
                    }
                }
            }
        }

        private void ReplyContentCore(Window momentWindow, ref double scrollAmount, ListBox rootListBox, IScrollPattern pattern, string replyContent)
        {
            var sendButtonResult = Retry.WhileNull(() => momentWindow.FindFirstByXPath("//Button[@Name='发送']")?.AsButton(),
            timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
            if (sendButtonResult.Success && sendButtonResult.Result != null)
            {
                var sendButton = sendButtonResult.Result;
                var index = 0;
                while (sendButton.IsOffscreen && index < 3)
                {
                    scrollAmount += SCROLL_STEP;
                    pattern.SetScrollPercent(0, scrollAmount);
                    Thread.Sleep(600);
                    sendButton = momentWindow.FindFirstByXPath("//Button[@Name='发送']")?.AsButton();
                    index++;
                }
                var parentPane = sendButton.GetParent().GetParent().GetParent();
                var contentArea = parentPane.FindFirstByXPath("//Edit[@Name='评论']")?.AsTextBox();
                contentArea?.WaitUntilClickable();
                contentArea?.DrawHighlightExt();
                momentWindow.SilenceEnterText(contentArea, replyContent);
                momentWindow.SilenceReturn(contentArea);
                _logger.Info("回复内容输入完成...");
                Thread.Sleep(600);
            }
            else
            {
                _logger.Error("回复朋友圈失败，发送按钮未找到");
            }
        }

        /// <summary>
        /// 添加朋友圈监听,当监听到指定的好友发朋友圈时，可以自动点赞，或者执行其他操作，如：回复评论等
        /// </summary>
        /// <param name="nickNameOrNickNames">监听的好友名称或好友名称列表</param>
        /// <param name="autoLike">是否自动点赞</param>
        /// <param name="action">回调函数,参数：朋友圈上下文<see cref="MomentsContext"/>,服务提供者<see cref="IServiceProvider"/>，适用于使用者获取自己注入的服务</param>
        public void AddMomentsListener(OneOf<string, List<string>> nickNameOrNickNames, bool autoLike = true,
          Action<MomentsContext, IServiceProvider> action = null)
        {
            if (_disposed)
                return;
            _logger.Info("添加朋友圈监听开始...");
            this.StopMomentsListener();
            _ListenerCancellationTokenSource = new CancellationTokenSource();
            this.OpenMoments();
            List<MomentItem> oldMomentsList = _GetCurrentMomentsList();
            List<MomentItem> oldShortMomentsList = _GetCurrentShortMomentsList();
            try
            {
                _pollingTimer = new System.Threading.Timer(_ =>
                {
                    _ListenerCancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (!_WxMainWindow.Client.AppRunning)
                    {
                        _logger.Info("微信客户端已关闭，朋友圈监听线程暂停");
                        return;
                    }
                    if (_isProcessing)
                    {
                        _logger.Trace("朋友圈监听上一次处理尚未完成，跳过本次检测");
                        return;
                    }
                    try
                    {
                        _isProcessing = true;
                        this.OpenMoments();
                        if (!this.checkShortMomentsChanged(ref oldShortMomentsList))
                        {
                            _ListenerCancellationTokenSource.Token.ThrowIfCancellationRequested();
                            _isProcessing = false;
                            return;
                        }
                        this.AddMomentsListenerCore(nickNameOrNickNames, ref oldMomentsList, autoLike, action);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.Info("朋友圈监听线程已停止，正常取消,不做处理");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("朋友圈监听异常:" + ex.Message, ex);
                        _isProcessing = false;
                    }
                }, null, WeAutomation.Config.MomentsListenInterval * 1000, WeAutomation.Config.MomentsListenInterval * 1000);
            }
            catch (OperationCanceledException)
            {
                _logger.Info("朋友圈监听线程已停止，正常取消,不做处理");
            }
            catch (Exception ex)
            {
                _logger.Error("朋友圈监听异常:" + ex.Message, ex);
                throw new Exception("添加朋友圈监听失败，" + ex.Message, ex);
            }
        }
        private bool checkShortMomentsChanged(ref List<MomentItem> oldShortMomentsList)
        {
            var newShortMomentsList = this._GetCurrentShortMomentsList();
            var exceptList = newShortMomentsList.Except(oldShortMomentsList).ToList();
            if (exceptList.Count > 0)
            {
                oldShortMomentsList = newShortMomentsList;
                _logger.Info("朋友圈内容发生变化，触发全面刷新朋友圈列表...");
                return true;
            }
            _logger.Trace("朋友圈内容未发生变化，跳过全面刷新朋友圈列表");
            return false;
        }
        /// <summary>
        /// 添加朋友圈监听核心逻辑
        /// 1.得到最新的朋友圈列表
        /// 2.得到待处理列表
        /// 3.递交给实际点赞与回复核心逻辑
        /// </summary>
        /// <param name="nickNameOrNickNames"></param>
        /// <param name="oldMomentsList"></param>
        /// <param name="autoLike"></param>
        /// <param name="action"></param>
        private void AddMomentsListenerCore(OneOf<string, List<string>> nickNameOrNickNames, ref List<MomentItem> oldMomentsList, bool autoLike = true, Action<MomentsContext, IServiceProvider> action = null)
        {
            this.OpenMoments();
            _ListenerCancellationTokenSource.Token.ThrowIfCancellationRequested();
            var newMomentsList = this._GetCurrentMomentsList();
            var willDoList = newMomentsList.Except(oldMomentsList).ToList();
            var nickList = nickNameOrNickNames.Value is string nickName ? new List<string> { nickName } : nickNameOrNickNames.Value as List<string>;
            willDoList = willDoList.Where(item => nickList.Contains(item.Who)).ToList();
            _isProcessing = false;
            if (willDoList.Count == 0)
                return;

            this._LikeMomentsAndReplyCore(willDoList.Select(item => item.Who).ToArray(), willDoList, action, autoLike);

            oldMomentsList = newMomentsList;
        }

        /// <summary>
        /// 点赞朋友圈并回复评论
        /// </summary>
        /// <param name="nickNames">好友名称或好友名称列表</param>
        /// <param name="willDoList">待处理列表</param>
        /// <param name="action">回调函数,参数：朋友圈上下文<see cref="MomentsContext"/>,服务提供者<see cref="IServiceProvider"/>，适用于使用者获取自己注入的服务</param>
        /// <param name="autoLike">是否自动点赞</param>
        private void _LikeMomentsAndReplyCore(OneOf<string, string[]> nickNames, List<MomentItem> willDoList = null,
            Action<MomentsContext, IServiceProvider> action = null, bool autoLike = true)
        {
            if (_disposed)
                return;
            _logger.Info("点赞朋友圈并回复评论开始...");
            var myNickName = _WxMainWindow.NickName;
            var searchNickNames = nickNames.Value is string nickName ? new string[] { nickName } : nickNames.Value as string[];
            _SelfUiThreadInvoker.Run(automation =>
            {
                Window momentWindow = _GetMomentWindow(automation);
                //先刷新朋友圈列表
                this._RefreshMomentsListCore(momentWindow);
                var momentsList = new List<MomentItem>();
                var rootListBox = momentWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
                rootListBox.DrawHighlightExt();
                if (rootListBox.Patterns.Scroll.IsSupported)
                {
                    var pattern = rootListBox.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 0);
                    Thread.Sleep(600);
                    double scrollAmount = 0;
                    var cloneList = willDoList.Select(item => item.Clone() as MomentItem).ToList();
                    while (true && cloneList.Count > 0)
                    {
                        var (mList, isEnd) = this._GetCurentMomentsItems(rootListBox);
                        var flag = willDoList == null ? mList.Any(m => searchNickNames.Contains(m.Who)) : mList.Any(m => searchNickNames.Contains(m.Who) && willDoList.Contains(m));
                        if (flag)
                        {
                            this._LikeAndReplayMomentsItemCore(mList, myNickName, searchNickNames, ref scrollAmount, rootListBox, pattern, momentWindow, willDoList, cloneList, action, autoLike);
                        }
                        if (isEnd)
                            break;
                        if (cloneList.Count == 0)
                            break;
                        scrollAmount += SCROLL_STEP;
                        pattern.SetScrollPercent(0, scrollAmount);
                        Thread.Sleep(600);
                    }

                }
            }).GetAwaiter().GetResult();
            _logger.Info("点赞朋友圈并回复评论结束...");
        }

        private void _LikeAndReplayMomentsItemCore(List<MomentItem> currentMomentsList, string myNickName, string[] searchNickNames, ref double scrollAmount,
          ListBox rootListBox, IScrollPattern pattern, Window momentWindow, List<MomentItem> willDoList = null,
          List<MomentItem> cloneList = null, Action<MomentsContext, IServiceProvider> action = null, bool autoLike = true)
        {
            foreach (var moment in currentMomentsList)
            {
                if (searchNickNames.Contains(moment.Who) && (willDoList == null || willDoList.Contains(moment)))
                {
                    cloneList.Remove(moment);
                    MomentsContext momentsContext = new MomentsContext(moment, scrollAmount, rootListBox, pattern, momentWindow, _ServiceProvider);
                    if (autoLike)
                    {
                        momentsContext.DoLike();
                    }
                    action?.Invoke(momentsContext, _ServiceProvider);
                    scrollAmount = momentsContext.GetScrollAmount();
                }
            }
        }

        private static void _DoLikeCore(ref double scrollAmount, ListBox rootListBox, IScrollPattern pattern, Window momentWindow, MomentItem moment)
        {
            if (!moment.IsMyLiked)
            {
                var items = rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                var item = items.FirstOrDefault(subItem => MomentItem.GetListItemKey(subItem.Name) == moment.ListItemKey);
                if (item != null)
                {
                    var xPath = "//Button[@Name='评论']";
                    var button = item.FindFirstByXPath(xPath)?.AsButton();
                    var index = 0;
                    while (button.IsOffscreen && index < 3)
                    {
                        scrollAmount += SCROLL_STEP;
                        pattern.SetScrollPercent(0, scrollAmount);
                        Thread.Sleep(600);
                        button = item.FindFirstByXPath(xPath)?.AsButton();
                        index++;
                    }
                    button.WaitUntilClickable();
                    momentWindow.Focus();
                    button.DrawHighlightExt();
                    if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                    {
                        KMSimulatorService.LeftClick(momentWindow, button);
                    }
                    else
                    {
                        button.Click();
                    }
                    Thread.Sleep(600);
                    xPath = "//Button[1][@Name='赞']";
                    var linkButtonResult = Retry.WhileNull(() => momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                        timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                    if (linkButtonResult.Success && linkButtonResult.Result != null)
                    {
                        momentWindow.Focus();
                        var linkButton = linkButtonResult.Result;
                        linkButton.WaitUntilClickable();
                        linkButton.DrawHighlightExt();
                        if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                        {
                            KMSimulatorService.LeftClick(momentWindow, linkButton);
                        }
                        else
                        {
                            linkButton.Click();
                        }
                        Thread.Sleep(600);
                    }
                }

            }
        }

        private List<MomentItem> _GetCurrentMomentsList()
        {
            //首先刷新朋友圈列表
            this.RefreshMomentsList();
            //获取最近的当前朋友圈列表并返回
            var momentsList = this.GetMomentsListSilence();
            return momentsList;
        }

        private List<MomentItem> _GetCurrentShortMomentsList()
        {
            var momentsList = this.GetShortMomentsList();
            return momentsList;
        }

        private Window _GetMomentsWindow(UIA3Automation automation)
        {
            if (_disposed)
                return null;
            var deskTop = automation.GetDesktop();
            var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                timeout: TimeSpan.FromSeconds(3),
                interval: TimeSpan.FromMilliseconds(200));
            return window.Success ? window.Result : null;
        }

        public void StopMomentsListener()
        {
            if (_disposed)
                return;
            _logger.Info("开始移除朋友圈监听...");
            _isProcessing = true;
            _ListenerCancellationTokenSource?.Cancel();
            if (_pollingTimer != null)
            {
                Thread.Sleep(3000);
            }
            _pollingTimer?.Dispose();
            _ListenerCancellationTokenSource = null;
            _pollingTimer = null;
            _isProcessing = false;

            _logger.Info("移除朋友圈监听成功...");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Moments()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _SelfUiThreadInvoker?.Dispose();
            }
            StopMomentsListener();
            _SelfUiThreadInvoker?.Dispose();
            _disposed = true;
        }
    }
}
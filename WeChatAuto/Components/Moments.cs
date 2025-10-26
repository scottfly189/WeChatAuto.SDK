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

namespace WeChatAuto.Components
{
    /// <summary>
    /// 朋友圈
    /// </summary>
    public class Moments : IDisposable
    {
        private readonly AutoLogger<Moments> _logger;
        private Window _MainWindow;
        private WeChatMainWindow _WxMainWindow;
        private volatile bool _disposed = false;
        private readonly UIThreadInvoker _SelfUiThreadInvoker;
        private readonly UIThreadInvoker _MainUIThreadInvoker;
        private Thread _ListenerThread;
        private CancellationTokenSource _ListenerCancellationTokenSource;
        private TaskCompletionSource<bool> _ListenerStarted;

        public Moments(Window window, WeChatMainWindow wxWindow, UIThreadInvoker mainUIThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<Moments>>();
            _MainWindow = window;
            _WxMainWindow = wxWindow;
            _MainUIThreadInvoker = mainUIThreadInvoker;
            _SelfUiThreadInvoker = new UIThreadInvoker();
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
                return false;
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
        /// <returns>朋友圈内容列表<see cref="MonentItem"/></returns>
        public List<MonentItem> GetMomentsList(int count = 20)
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
                var momentsList = new List<MonentItem>();
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
                            if (monentItem != null && !momentsList.Exists(m => m.From == monentItem.From && m.Content == monentItem.Content && m.Time == monentItem.Time))
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
        /// 刷新朋友圈
        /// </summary>
        public void RefreshMomentsList(Action<UIA3Automation,Window> action = null)
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
                    momentWindow.DrawHighlightExt();

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
                            Thread.Sleep(600);
                            action?.Invoke(automation, momentWindow);   //执行回调
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
                }
            }).Wait();
            _logger.Info("刷新朋友圈结束...");
        }

        /// <summary>
        /// 添加朋友圈监听,当监听到指定的好友发朋友圈时，可以自动点赞，或者执行其他操作，如：回复评论等
        /// </summary>
        /// <param name="nickNameOrNickNames">好友名称或好友名称列表</param>
        /// <param name="autoLike">是否自动点赞</param>
        /// <param name="action">回调函数,参数：朋友圈内容列表<see cref="List{MonentItem}"/>,朋友圈对象<see cref="Moments"/>,可以通过Monents对象调用回复评论等操作,服务提供者<see cref="IServiceProvider"/>，适用于使用者获取自己注入的服务</param>
        public void AddMomentsListener(OneOf<string, List<string>> nickNameOrNickNames, bool autoLike = true, Action<List<MonentItem>, Moments, IServiceProvider> action = null)
        {
            if (_disposed)
                return;
            _logger.Info("添加朋友圈监听开始...");
            this.StopMomentsListener();
            _ListenerThread = new Thread(() =>
            {
                while (!_ListenerCancellationTokenSource.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                }
            });
            _ListenerThread.Start();
        }
        /// <summary>
        /// 点赞朋友圈
        /// </summary>
        /// <param name="monentItem">朋友圈内容<see cref="MonentItem"/></param>
        public void LikeMoments(MonentItem monentItem)
        {
            if (_disposed)
                return;
            _logger.Info("点赞朋友圈开始...");
            _SelfUiThreadInvoker.Run(automation =>
            {
            }).Wait();
        }

        /// <summary>
        /// 回复朋友圈
        /// </summary>
        /// <param name="monentItem">朋友圈内容<see cref="MonentItem"/></param>
        /// <param name="replyContent">回复内容</param>
        public void ReplyMoments(MonentItem monentItem, string replyContent)
        {
            if (_disposed)
                return;
            _logger.Info("回复朋友圈开始...");
            _SelfUiThreadInvoker.Run(automation =>
            {
            }).Wait();
        }

        public void StopMomentsListener()
        {
            if (_disposed)
                return;
            if (_ListenerThread != null && _ListenerThread.IsAlive)
            {
                _ListenerCancellationTokenSource.Cancel();
                _ListenerThread.Join(5000);
                _ListenerThread = null;
                _ListenerCancellationTokenSource = null;
                _ListenerStarted.TrySetResult(false);
                _ListenerStarted = null;
            }
            _logger.Info("移除朋友圈监听成功...");
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
            }
            _SelfUiThreadInvoker.Dispose();
            _disposed = true;
        }
    }
}
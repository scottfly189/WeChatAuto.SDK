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
        private Window _MomentsWindow;

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
                                    _MomentsWindow = window.Result;
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
            this.OpenMoments();
            var result = _MainUIThreadInvoker.Run(automation =>
            {
                var deskTop = automation.GetDesktop();
                var window = Retry.WhileNull(() => deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWindow.Properties.ProcessId)).And(cf.ByClassName("SnsWnd"))).AsWindow(),
                    timeout: TimeSpan.FromSeconds(3),
                    interval: TimeSpan.FromMilliseconds(200));
                if (window.Success)
                {
                    _MomentsWindow = window.Result;
                }
                _MomentsWindow.DrawHighlightExt();
                _MomentsWindow.Focus();
                var momentsList = new List<MonentItem>();
                var rootListBox = _MomentsWindow.FindFirstByXPath("//List[@Name='朋友圈']")?.AsListBox();
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
                        rootListBox.Focus();
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
                    momentsList.ForEach(item =>
                    {
                            Trace.WriteLine(item.ToString());
                    });
                }

                return momentsList;
            }).Result;
            return result;
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
                if (_MomentsWindow != null)
                {
                    _MomentsWindow.Close();
                    _MomentsWindow = null;
                }
            }
            _SelfUiThreadInvoker.Dispose();
            _disposed = true;
        }
    }
}
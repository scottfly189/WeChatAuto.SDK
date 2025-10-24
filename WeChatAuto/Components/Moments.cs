using System;
using FlaUI.Core.AutomationElements;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Definitions;
using WeChatAuto.Utils;
using WxAutoCommon.Utils;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;

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
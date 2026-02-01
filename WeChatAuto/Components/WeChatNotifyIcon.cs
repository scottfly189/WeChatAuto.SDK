using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using WeAutoCommon.Utils;
using System.Threading;
using WeChatAuto.Utils;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端通知图标,封装的微信客户端通知图标，包含通知图标、通知图标点击事件等
    /// </summary>
    public class WeChatNotifyIcon : IDisposable
    {
        private WeChatMainWindow _wxMainWindow;
        private Button _NotifyIcon;
        private IServiceProvider _serviceProvider;
        public Button NotifyIcon => _NotifyIcon;

        public WeChatNotifyIcon(Button notifyIcon, IServiceProvider serviceProvider, WeChatMainWindow wxMainWindow)
        {
            _wxMainWindow = wxMainWindow;
            _NotifyIcon = notifyIcon;
            _serviceProvider = serviceProvider;
        }
        
        /// <summary>
        /// 点击通知图标
        /// </summary>s
        public void Click()
        {
            var actionThreaderInvoker = _wxMainWindow.UiMainThreadInvoker;
            actionThreaderInvoker.Run(automation =>
            {
                _NotifyIcon.DrawHighlightExt();
                _NotifyIcon.Invoke();
                Thread.Sleep(300);
            }).GetAwaiter().GetResult();
        }

        public void Dispose()
        {

        }
    }
}
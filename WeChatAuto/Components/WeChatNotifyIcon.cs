using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using WxAutoCommon.Utils;
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
            _NotifyIcon = _GetNotifyIcon(notifyIcon);
            _serviceProvider = serviceProvider;
        }

        // 获取通知图标
        private Button _GetNotifyIcon(Button refToNotifyIcon)
        {
            var actionThreaderInvoker = _wxMainWindow.UiMainThreadInvoker;
            var returnButton = actionThreaderInvoker.Run(automation =>
            {
                var taskBarRoot = automation.GetDesktop().FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd")));
                var button = Retry.WhileNull(() => taskBarRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                        .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar))))
                        .FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME).And(cf.ByControlType(ControlType.Button))
                        .And(cf.ByAutomationId(refToNotifyIcon.AutomationId))),
                        timeout: TimeSpan.FromSeconds(5));
                return button.Result.AsButton();
            }).GetAwaiter().GetResult();

            return returnButton;
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
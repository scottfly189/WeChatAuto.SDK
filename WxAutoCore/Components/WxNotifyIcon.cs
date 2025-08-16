using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端通知图标,封装的微信客户端通知图标，包含通知图标、通知图标点击事件等
    /// </summary>
    public class WxNotifyIcon
    {
        private Button _NotifyIcon;
        public Button NotifyIcon => _NotifyIcon;

        public WxNotifyIcon(Button notifyIcon)
        {
            _NotifyIcon = notifyIcon;
        }
        /// <summary>
        /// 点击通知图标
        /// </summary>s
        public void Click()
        {
            _NotifyIcon.Invoke();
        }
    }
}
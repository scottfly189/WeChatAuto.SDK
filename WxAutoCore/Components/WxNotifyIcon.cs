using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
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
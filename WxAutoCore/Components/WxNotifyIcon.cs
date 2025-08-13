using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    public class WxNotifyIcon
    {
        private Button _NotifyIcon;
        private int _ProcessId;
        public Button NotifyIcon => _NotifyIcon;
        public int ProcessId => _ProcessId;

        public WxNotifyIcon(Button notifyIcon, int processId)
        {
            _NotifyIcon = notifyIcon;
            _ProcessId = processId;
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
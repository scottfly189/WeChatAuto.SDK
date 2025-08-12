using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;


namespace WxAutoCore.Pages
{
    /// <summary>
    /// 微信窗口管理
    /// </summary>
    public class WindowHelper
    {
        private readonly UIA3Automation _automation;
        private readonly Window _wxWindow;
        private readonly AutomationElement _root;

        public WindowHelper()
        {
            _automation = new UIA3Automation();
            _root = _automation.GetDesktop();
        }

        /// <summary>
        /// 显示微信
        /// </summary>
        public void Show()
        {

        }
    }
}
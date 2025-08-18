using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class WxSubWindow
    {
        private WxWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window

        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口</param>
        public WxSubWindow(Window window, WxWindow wxWindow)
        {
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
        }

        public void Close()
        {
            _SelfWindow.Close();
        }

        public void Minimize()
        {

        }
        public void WindowMin()
        {

        }

        public void Maximize()
        {

        }

        public void Restore()
        {

        }

        public void WindowTop()
        {

        }
    }
}
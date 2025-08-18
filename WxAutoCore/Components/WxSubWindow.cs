using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class WxSubWindow : IChatContentAction
    {
        private ChatContent _ChatContent;
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
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane");
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
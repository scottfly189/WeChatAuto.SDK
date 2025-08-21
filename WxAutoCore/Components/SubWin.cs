using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow
    {
        private ChatContent _ChatContent;
        private WxMainWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window
        private int _ProcessId;

        public Window SelfWindow { get => _SelfWindow; set => _SelfWindow = value; }

        public ChatContent ChatContent => _ChatContent;

        public int ProcessId => _ProcessId;

        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口的微信窗口对象</param>
        public SubWin(Window window, WxMainWindow wxWindow)
        {
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane",this);
            _ProcessId = _SelfWindow.Properties.ProcessId.Value;
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
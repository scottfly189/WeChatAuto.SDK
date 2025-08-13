using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 弹出窗口
    /// </summary>
    public class PopWin
    {
        private Window _Window;
        private string _Name;
        public PopWin(Window window, string name)
        {
            _Window = window;
            _Name = name;
        }
    }
}
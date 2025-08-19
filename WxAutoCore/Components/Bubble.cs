using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区气泡
    /// </summary>
    public class Bubble
    {
        private Window _Window;
        private AutomationElement _BubbleRoot;
        public MessageType MessageType { get; set; }
        public MessageSourceType MessageSourceType { get; set; }
        public Bubble(Window window, AutomationElement bubbleRoot)
        {
            _Window = window;
            _BubbleRoot = bubbleRoot;
        }
    }
}
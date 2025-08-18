using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区发送者
    /// </summary>
    public class Sender
    {
        private Window _Window;
        private AutomationElement _SenderRoot;
        public Button[] ToolBarButtons { get; set; }
        public TextBox ContentArea { get; set; }
        public Button SendButton { get; set; }
        /// <summary>
        /// 聊天内容区发送者构造函数
        /// </summary>
        public Sender(Window window, AutomationElement senderRoot)
        {
            _Window = window;
            _SenderRoot = senderRoot;
        }
    }
}
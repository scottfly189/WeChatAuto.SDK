
using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    public class ChatBody
    {
        private Window _Window;
        private AutomationElement _ChatBodyRoot;
        public BubbleList BubbleList => GetBubbleList();
        public Sender Sender => GetSender();
        public ChatBody(Window window,AutomationElement chatBodyRoot)
        {
            _Window = window;
            _ChatBodyRoot = chatBodyRoot;
        }
        /// <summary>
        /// 获取聊天内容区气泡列表
        /// </summary>
        /// <returns>聊天内容区气泡列表<see cref="BubbleList"/></returns>
        public BubbleList GetBubbleList()
        {
            return null;
        }
        /// <summary>
        /// 获取聊天内容区发送者
        /// </summary>
        /// <returns>聊天内容区发送者<see cref="Sender"/></returns>
        public Sender GetSender()
        {
            return null;
        }
    }
} 

using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;
using WxAutoCommon.Interface;

namespace WxAutoCore.Components
{
    public class ChatBody
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private string _Title;
        private AutomationElement _ChatBodyRoot;
        private UIThreadInvoker _uiThreadInvoker;
        public MessageBubbleList BubbleList => GetBubbleList();
        public Sender Sender => GetSender();
        public ChatBody(Window window, AutomationElement chatBodyRoot, IWeChatWindow wxWindow, string title, UIThreadInvoker uiThreadInvoker)
        {
            _Window = window;
            _ChatBodyRoot = chatBodyRoot;
            _WxWindow = wxWindow;
            _Title = title;
            _uiThreadInvoker = uiThreadInvoker;
        }
        /// <summary>
        /// 获取聊天内容区气泡列表
        /// </summary>
        /// <returns>聊天内容区气泡列表<see cref="BubbleList"/></returns>
        public MessageBubbleList GetBubbleList()
        {
            var xPath = $"/Pane/Pane/List[@Name='{WeChatConstant.WECHAT_CHAT_BOX_MESSAGE}']";
            var bubbleListRoot = _uiThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).Result;
            DrawHightlightHelper.DrawHightlight(bubbleListRoot, _uiThreadInvoker);
            MessageBubbleList bubbleList = new MessageBubbleList(_Window, bubbleListRoot, _WxWindow,_Title,_uiThreadInvoker);
            return bubbleList;
        }
        /// <summary>
        /// 获取聊天内容区发送者
        /// </summary>
        /// <returns>聊天内容区发送者<see cref="Sender"/></returns>
        public Sender GetSender()
        {
            var xPath = "/Pane[2]";
            var senderRoot = _uiThreadInvoker.Run(automation => _ChatBodyRoot.FindFirstByXPath(xPath)).Result;
            DrawHightlightHelper.DrawHightlight(senderRoot, _uiThreadInvoker);
            var sender = new Sender(_Window, senderRoot, _WxWindow,_Title,_uiThreadInvoker);
            return sender;
        }
    }
}
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;

namespace WxAutoCore.Components
{
    public class ChatContent
    {
        private Window _Window;
        private ChatContentType _ChatContentType;
        private string _XPath;
        private AutomationElement _ChatContentRoot;
        public ChatHeader ChatHeader => GetChatHeader();
        public ChatBody ChatBody => GetChatBody();
        public ChatContent(Window window, ChatContentType chatContentType, string xPath)
        {
            _Window = window;
            _ChatContentType = chatContentType;
            _XPath = xPath;
            _ChatContentRoot = _Window.FindFirstByXPath(_XPath);
        }
        /// <summary>
        /// 获取聊天标题
        /// </summary>
        /// <returns>聊天标题区<see cref="ChatHeader"/></returns>
        public ChatHeader GetChatHeader()
        {
            var header =_ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            var titles = header.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var title = "";
            foreach (var item in titles)
            {
                title += item.Name;
            }
            var buttons = header.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            Button chatInfoButton = null;
            if (buttons.Count() > 0)
            {
                chatInfoButton = buttons.ToList().First().AsButton();
            }
            return new ChatHeader(title, chatInfoButton);
        }
        /// <summary>
        /// 获取聊天内容
        /// </summary>
        /// <returns>聊天内容</returns>
        public ChatBody GetChatBody()
        {
            var chatBodyRoot = _ChatContentRoot.FindFirstByXPath("/Pane[2]");
            var chatBody = new ChatBody(_Window, chatBodyRoot);
            return chatBody;
        }
    }
}
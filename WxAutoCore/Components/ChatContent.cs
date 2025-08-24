using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    public class ChatContent
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private ChatContentType _ChatContentType;
        private string _XPath;
        private AutomationElement _ChatContentRoot;
        public AutomationElement ChatContentRoot
        {
            get
            {
                if (_ChatContentRoot == null)
                {
                    _ChatContentRoot = _Window.FindFirstByXPath(_XPath);
                }
                return _ChatContentRoot;
            }
        }
        public ChatHeader ChatHeader => GetChatHeader();
        public ChatBody ChatBody => GetChatBody();
        public ChatContent(Window window, ChatContentType chatContentType, string xPath, IWeChatWindow wxWindow)
        {
            _Window = window;
            _ChatContentType = chatContentType;
            _XPath = xPath;
            _WxWindow = wxWindow;
        }
        /// <summary>
        /// 获取聊天标题
        /// </summary>
        /// <returns>聊天标题区<see cref="ChatHeader"/></returns>
        public ChatHeader GetChatHeader()
        {
            var title = GetFullTitle();
            if (string.IsNullOrEmpty(title))
            {
                return new ChatHeader(string.Empty, null);
            }
            if (Regex.IsMatch(title, @"^([^\(]+) \("))
            {
                title = Regex.Match(title, @"^([^\(]+) \(").Groups[1].Value;
            }
            else
            {
                title = title.Trim();
            }
            var header = ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            var buttons = header.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            Button chatInfoButton = null;
            if (buttons.Count() > 0)
            {
                chatInfoButton = buttons.ToList().First().AsButton();
            }
            return new ChatHeader(title, chatInfoButton);
        }
        /// <summary>
        /// 获取聊天标题(不做处理，直接返回)
        /// </summary>
        /// <returns></returns>
        public string GetFullTitle()
        {
            var header = ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
            DrawHightlightHelper.DrawHightlight(header);
            var titles = header.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text));
            if (titles == null)
            {
                return "";
            }
            var title = titles.Name;
            return title;
        }
        /// <summary>
        /// 获取聊天内容组件
        /// </summary>
        /// <returns>聊天内容组件</returns>
        public ChatBody GetChatBody()
        {
            var title = GetFullTitle();
            var chatBodyRoot = ChatContentRoot.FindFirstByXPath("/Pane[2]");
            DrawHightlightHelper.DrawHightlight(chatBodyRoot);
            var chatBody = new ChatBody(_Window, chatBodyRoot, _WxWindow,title);
            return chatBody;
        }
    }
}
using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCore.Utils;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    public class ChatContent
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private ChatContentType _ChatContentType;
        private string _XPath;
        private AutomationElement _ChatContentRoot;
        private UIThreadInvoker _uiThreadInvoker;
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
        public ChatContent(Window window, ChatContentType chatContentType, string xPath, IWeChatWindow wxWindow, UIThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
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
            var header = _uiThreadInvoker.Run(automation => ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).Result;
            var buttons = _uiThreadInvoker.Run(automation => header.FindAllDescendants(cf => cf.ByControlType(ControlType.Button))).Result;
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
            var header = _uiThreadInvoker.Run(automation => ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).Result;
            DrawHightlightHelper.DrawHightlight(header, _uiThreadInvoker);
            var titles = _uiThreadInvoker.Run(automation => header.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text))).Result;
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
            var chatBodyRoot = _uiThreadInvoker.Run(automation => ChatContentRoot.FindFirstByXPath("/Pane[2]")).Result;
            DrawHightlightHelper.DrawHightlight(chatBodyRoot, _uiThreadInvoker);
            var chatBody = new ChatBody(_Window, chatBodyRoot, _WxWindow,title, _uiThreadInvoker);
            return chatBody;
        }
    }
}
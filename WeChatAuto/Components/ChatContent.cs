using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WeChatAuto.Utils;
using WxAutoCommon.Utils;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatAuto.Components
{
    public class ChatContent : IDisposable
    {
        private readonly AutoLogger<ChatContent> _logger;
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private ChatContentType _ChatContentType;
        private string _XPath;
        private AutomationElement _ChatContentRoot;
        private UIThreadInvoker _uiMainThreadInvoker;
        private readonly IServiceProvider _serviceProvider;
        private WeChatMainWindow _MainWxWindow;    //主窗口对象
        private volatile bool _disposed = false;
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
        private ChatHeader _SubWinChatHeader;
        private ChatBody _SubWinChatBody;
        //注意：下面代码：如果是子窗口，则会返回固定的ChatHeader和ChatBody，不会重新获取，
        //如果是主窗口的ChatContent,则每次都会重新获取ChatHeader和ChatBody,因为主窗口的ChatHeader和ChatBody会随着聊天内容的变化而变化
        public ChatHeader ChatHeader => GetChatHeader();
        public ChatBody ChatBody => GetChatBody();
        public ChatContent(Window window, ChatContentType chatContentType, string xPath, IWeChatWindow wxWindow, UIThreadInvoker uiThreadInvoker, WeChatMainWindow mainWxWindow, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<ChatContent>>();
            _uiMainThreadInvoker = uiThreadInvoker;
            _Window = window;
            _ChatContentType = chatContentType;
            _XPath = xPath;
            _WxWindow = wxWindow;
            _MainWxWindow = mainWxWindow;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获取聊天标题
        /// </summary>
        /// <returns>聊天标题区<see cref="ChatHeader"/></returns>
        public ChatHeader GetChatHeader()
        {
            if (_ChatContentType == ChatContentType.SubWindow && _SubWinChatHeader != null)
            {
                //如果是子窗口，则返回子窗口的ChatHeader
                return _SubWinChatHeader;
            }
            var title = GetFullTitle();
            if (string.IsNullOrEmpty(title))
            {
                return new ChatHeader(string.Empty, null, _serviceProvider);
            }
            if (Regex.IsMatch(title, @"^([^\(]+) \("))
            {
                title = Regex.Match(title, @"^([^\(]+) \(").Groups[1].Value;
            }

            title = title.Trim();

            var header = _uiMainThreadInvoker.Run(automation => ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            var buttons = _uiMainThreadInvoker.Run(automation => header.FindAllDescendants(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            Button chatInfoButton = null;
            if (buttons.Count() > 0)
            {
                chatInfoButton = buttons.ToList().First().AsButton();
            }
            var cHeader = new ChatHeader(title, chatInfoButton, _serviceProvider);
            if (_ChatContentType == ChatContentType.SubWindow)
            {
                _SubWinChatHeader = cHeader;
            }
            return cHeader;
        }
        /// <summary>
        /// 获取聊天标题(不做处理，直接返回)
        /// </summary>
        /// <returns></returns>
        public string GetFullTitle()
        {
            if (_disposed)
            {
                return "";
            }
            var header = _uiMainThreadInvoker.Run(automation => ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            DrawHightlightHelper.DrawHightlight(header, _uiMainThreadInvoker);
            var titles = _uiMainThreadInvoker.Run(automation => header.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text))).GetAwaiter().GetResult();
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
            if (_disposed)
            {
                return null;
            }
            if (_ChatContentType == ChatContentType.SubWindow && _SubWinChatBody != null)
            {
                return _SubWinChatBody;
            }
            var title = GetFullTitle();
            var chatBodyRoot = _uiMainThreadInvoker.Run(automation => ChatContentRoot.FindFirstByXPath("/Pane[2]")).GetAwaiter().GetResult();
            DrawHightlightHelper.DrawHightlight(chatBodyRoot, _uiMainThreadInvoker);
            var chatBody = new ChatBody(_Window, chatBodyRoot, _WxWindow, title, _uiMainThreadInvoker, this._MainWxWindow, _serviceProvider);
            if (_ChatContentType == ChatContentType.SubWindow)
            {
                _SubWinChatBody = chatBody;
            }
            return chatBody;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            _SubWinChatBody?.Dispose();
        }
    }
}
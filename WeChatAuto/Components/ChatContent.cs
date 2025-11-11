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
            var title = GetFullTitle();
            if (string.IsNullOrEmpty(title))
            {
                return new ChatHeader(string.Empty, null, _serviceProvider);
            }
            if (Regex.IsMatch(title, @"^([^\(]+) \("))
            {
                title = Regex.Match(title, @"^([^\(]+) \(").Groups[1].Value;
            }
            else
            {
                title = title.Trim();
            }
            var header = _uiMainThreadInvoker.Run(automation => ChatContentRoot.FindFirstChild(cf => cf.ByControlType(ControlType.Pane))).GetAwaiter().GetResult();
            var buttons = _uiMainThreadInvoker.Run(automation => header.FindAllDescendants(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            Button chatInfoButton = null;
            if (buttons.Count() > 0)
            {
                chatInfoButton = buttons.ToList().First().AsButton();
            }
            return new ChatHeader(title, chatInfoButton, _serviceProvider);
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
            var title = GetFullTitle();
            var chatBodyRoot = _uiMainThreadInvoker.Run(automation => ChatContentRoot.FindFirstByXPath("/Pane[2]")).GetAwaiter().GetResult();
            DrawHightlightHelper.DrawHightlight(chatBodyRoot, _uiMainThreadInvoker);
            var chatBody = new ChatBody(_Window, chatBodyRoot, _WxWindow, title, _uiMainThreadInvoker, this._MainWxWindow, _serviceProvider);
            return chatBody;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
        }
    }
}
using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WeAutoCommon.Enums;
using WeAutoCommon.Interface;
using WeChatAuto.Utils;
using WeAutoCommon.Utils;
using System;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Tools;
using System.Drawing;
using FlaUI.Core.Capturing;
using WeChatAuto.Extentions;

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
        public WeChatMainWindow MainWxWindow => _MainWxWindow;
        //private AutomationElement OwerChatContentRoot => _GetChatContentRoot_();
        private volatile bool _disposed = false;
        public AutomationElement ChatContentRoot
        {
            get
            {
                if (_ChatContentRoot == null)
                {
                    _ChatContentRoot = _GetChatContentRoot_();
                }
                return _ChatContentRoot;
            }
        }
        public AutomationElement NewChatContentRoot => Retry.WhileNull(() => _Window.FindFirstByXPath(_XPath), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200)).Result;
        private ChatHeader _SubWinCacheChatHeader;
        private ChatBody _SubWinCacheChatBody;
        //注意：下面代码：如果是子窗口，则会返回固定的ChatHeader和ChatBody，不会重新获取，
        //如果是主窗口的ChatContent,则每次都会重新获取ChatHeader和ChatBody,因为主窗口的ChatHeader和ChatBody会随着聊天内容的变化而变化
        public ChatHeader ChatHeader => GetChatHeader();
        public ChatBody ChatBody => GetChatBody();
        public ChatType ChatType => GetChatType();

        private AutomationElement _GetChatContentRoot_()
        {
            var listbox = Retry.WhileNull(() => _Window.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("消息"))),
              TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200)).Result;
            if (listbox == null)
            {
                _logger.Info("消息列表根节点获取失败,此窗口可能不是聊天窗口");
                return null;
            }
            RandomWait.Wait(100, 500);
            return listbox.GetParent().GetParent().GetParent().GetParent();
        }

        /// <summary>
        /// 聊天标题
        /// </summary>
        public string FullTitle => GetFullTitle();
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
        // /// <summary>
        // /// 获取聊天内容截图
        // /// </summary>
        // /// <param name="inThread">调用是否需要在UI线程中执行,默认是True，单独调用应该为true，内部调用应该设定为false,因为内部调用已经在UI线程中执行了</param>
        // /// <returns>聊天内容截图</returns>
        // public Image GetOwerChatContentImage(bool inThread = true)
        // {
        //     Func<Image> func = () =>
        //     {
        //         var image = FlaUI.Core.Capturing.Capture.Element(OwerChatContentRoot);
        //         image.ApplyOverlays(new MouseOverlay(image), new InfoOverlay(image));
        //         return image.Bitmap;
        //     };
        //     if (inThread)
        //     {
        //         return _uiMainThreadInvoker.Run(automation => func()).GetAwaiter().GetResult();
        //     }
        //     else
        //     {
        //         return func();
        //     }
        // }
        /// <summary>
        /// 聊天人数
        /// </summary>
        public int ChatMemberCount => GetChatMemberCount();

        private ChatType GetChatType()
        {
            var compareTitles = new string[] { "订阅号", "服务通知", "腾讯新闻", "微信团队" };
            var title = GetFullTitle();
            if (string.IsNullOrWhiteSpace(title))
                return ChatType.其他;
            if (compareTitles.Any(t => t.Equals(title)))
            {
                return Enum.TryParse<ChatType>(title, true, out ChatType chatType) ? chatType : ChatType.其他;
            }
            if (Regex.IsMatch(title, @"\((\d+)\)$"))
            {
                return ChatType.群聊;
            }
            if (IsMPChat())
            {
                return ChatType.公众号;
            }
            return ChatType.好友;
        }

        /// <summary>
        /// 是否是公众号聊天
        /// </summary>
        /// <returns>是否是公众号聊天,True:是,False:否</returns>
        private bool IsMPChat()
        {
            var result = _uiMainThreadInvoker.Run(automation =>
            {
                var item = _Window.FindFirstByXPath("//Button[@Name='公众号主页']");
                return item == null ? false : true;
            }).GetAwaiter().GetResult();
            return result;
        }

        /// <summary>
        /// 获取聊天人数
        /// </summary>
        /// <returns>聊天人数</returns>
        private int GetChatMemberCount()
        {
            var title = GetFullTitle();
            if (string.IsNullOrWhiteSpace(title))
                return 1;
            if (Regex.IsMatch(title, @"\((\d+)\)$"))
            {
                return int.Parse(Regex.Match(title, @"\((\d+)\)$").Groups[1].Value);
            }
            return 1;
        }
        /// <summary>
        /// 获取聊天标题
        /// </summary>
        /// <returns>聊天标题区<see cref="ChatHeader"/></returns>
        private ChatHeader GetChatHeader()
        {
            if (_ChatContentType == ChatContentType.SubWindow && _SubWinCacheChatHeader != null)
            {
                //如果是子窗口，则返回子窗口的ChatHeader
                return _SubWinCacheChatHeader;
            }

            var cHeader = new ChatHeader(_serviceProvider, this._Window, _ChatContentType,_uiMainThreadInvoker);
            if (_ChatContentType == ChatContentType.SubWindow)
            {
                _SubWinCacheChatHeader = cHeader;
            }
            return cHeader;
        }
        /// <summary>
        /// 获取聊天标题(不做处理，直接返回)
        /// </summary>
        /// <returns></returns>
        private string GetFullTitle()
        {
            if (_disposed)
            {
                return "";
            }
            var (isSubscriptionAccount, subTitle) = IsSubscriptionAccount();
            if (isSubscriptionAccount)
            {
                return subTitle;
            }
            if (ChatContentRoot == null)
                return "";
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
        /// 是否订阅号
        /// </summary>
        /// <returns>是否订阅号,标题</returns>
        private (bool isSubscriptionAccount, string title) IsSubscriptionAccount()
        {
            return _uiMainThreadInvoker.Run(automation =>
            {
                var item = _Window.FindFirstByXPath("/Pane[1]");
                if (item != null && item.Name.Equals("CWebviewHostWnd") && item.ClassName.Equals("CWebviewControlHostWnd"))
                {
                    return (true, "订阅号");
                }

                AutomationElement root = null;
                if (_ChatContentType == ChatContentType.Inline)
                {
                    root = _Window.FindFirstByXPath("/Pane[2]/Pane/Pane[2]");
                }
                else
                {
                    root = _Window;
                }
                item = root.FindFirstByXPath("//Text[@Name='腾讯新闻']");
                if (item != null)
                {
                    return (true, "腾讯新闻");
                }
                item = root.FindFirstByXPath("//Text[@Name='服务通知']");
                if (item != null)
                {
                    return (true, "服务通知");
                }

                item = root.FindFirstByXPath("//Text[@Name='微信团队']");
                if (item != null)
                {
                    return (true, "微信团队");
                }
                item = root.FindFirstByXPath("//Text[@Name='文件传输助手']");
                if (item != null)
                {
                    return (true, "文件传输助手");
                }
                item = root.FindFirstByXPath("//Button[@Name='公众号主页']");
                if (item != null)
                {
                    var fetchRoot = item.GetParent().GetParent().GetSibling(-1);
                    item = fetchRoot.FindFirstByXPath("/Pane/Pane[1]/Pane/Text[1]");
                    return (true, item == null ? "公众号" : item.Name.Trim());
                }
                return (false, "");
            }).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 获取聊天内容组件
        /// </summary>
        /// <returns>聊天内容组件</returns>
        private ChatBody GetChatBody()
        {
            if (_disposed)
            {
                return null;
            }
            if (_ChatContentType == ChatContentType.SubWindow && _SubWinCacheChatBody != null)
            {
                return _SubWinCacheChatBody;
            }
            var title = GetFullTitle();
            var chatBody = new ChatBody(_Window, _WxWindow, title, this.ChatType, _uiMainThreadInvoker, this._MainWxWindow, _serviceProvider, this);
            if (_ChatContentType == ChatContentType.SubWindow)
            {
                _SubWinCacheChatBody = chatBody;
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
            _SubWinCacheChatBody?.Dispose();
        }
    }
}
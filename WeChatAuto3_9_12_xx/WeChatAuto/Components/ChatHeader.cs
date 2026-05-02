using System;
using FlaUI.Core.AutomationElements;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Enums;
using WeAutoCommon.Models;
using System.Text.RegularExpressions;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using WeAutoCommon.Utils;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区标题区
    /// </summary>
    public class ChatHeader
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<ChatContent> _logger;
        private Window _window;
        private ChatContentType chatContentType;

        private UIThreadInvoker _uiMainThreadInvoker;

        /// <summary>
        /// 聊天内容区标题区构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供者<see cref="IServiceProvider"/></param>
        /// <param name="chatContentType">窗口是inline还是子窗口</param>
        /// <param name="window">Header所归属的Window</param>
        /// <param name="_uiMainThreadInvoker"></param>
        public ChatHeader(IServiceProvider serviceProvider, Window window, ChatContentType chatContentType, UIThreadInvoker _uiMainThreadInvoker)
        {
            this._uiMainThreadInvoker = _uiMainThreadInvoker;
            this.chatContentType = chatContentType;
            _logger = serviceProvider.GetRequiredService<AutoLogger<ChatContent>>();
            this._window = window;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 聊天标题
        /// </summary>
        public HeaderInfo Title
        {
            get
            {
                var infoResult = _uiMainThreadInvoker.Run(automation =>
                {
                    HeaderInfo info = new HeaderInfo()
                    {
                        Title = "",
                        HeaderType = ChatType.其他,
                    };
                    var result = TryCheckFriend(info);
                    if (!result)
                    {
                        result = TryCheckSubscription(info);
                        if (!result)
                        {
                            result = TryCheckAnother(info);
                        }
                    }
                    return info;
                }).GetAwaiter().GetResult();
                return infoResult;
            }
        }
        private bool TryCheckFriend(HeaderInfo info)
        {
            var item = _window.FindFirstByXPath("//Button[@Name='聊天信息']");
            if (item != null)
            {
                var root = item.GetParent().GetParent().GetSibling(-1);
                item = root.FindFirstByXPath("/Pane/Pane[1]/Pane/Text[1]");
                if (item != null)
                {
                    var label = item.Name;
                    var pattern = @"(.+)\s*\(([\d]+)\)$";
                    var match = Regex.Match(label, pattern);
                    if (match.Success)
                    {
                        info.HeaderType = ChatType.群聊;
                        info.Title = match.Groups[1].Value.Trim();
                        info.ChatNumber = int.Parse(match.Groups[2].Value.Trim());
                    }
                    else
                    {
                        var checkCompanyWx = item.GetSibling(1);
                        if (checkCompanyWx != null)
                        {
                            if (checkCompanyWx.Name.StartsWith("@"))
                            {
                                info.HeaderType = ChatType.企业微信;
                                info.Title = label;
                                return true;
                            }
                        }
                        info.HeaderType = ChatType.好友;
                        info.Title = label;
                    }
                    return true;
                }
                else
                {
                    _logger.Error("WeChatAuto.SDK发生了错误,作者没有考虑到一些情况,请通知作者修改");
                    throw new Exception("WeChatAuto.SDK发生了错误,作者没有考虑到一些情况,请通知作者修改");
                }
            }
            return false;
        }
        private bool TryCheckSubscription(HeaderInfo info)
        {
            var item = _window.FindFirstByXPath("/Pane[1]");
            if (item != null && item.Name.Equals("CWebviewHostWnd") && item.ClassName.Equals("CWebviewControlHostWnd"))
            {
                info.Title = ChatType.订阅号.ToString();
                info.HeaderType = ChatType.订阅号;
                return true;
            }
            return false;
        }
        private bool TryCheckAnother(HeaderInfo info)
        {
            var item = _window.FindFirstByXPath("/Pane[1]");
            if (item != null && !string.IsNullOrWhiteSpace(item.Name))
                return false;
            AutomationElement root = null;
            if (chatContentType == ChatContentType.Inline)
            {
                root = _window.FindFirstByXPath("/Pane[2]/Pane/Pane[2]");
            }
            else
            {
                root = _window;
            }
            item = root.FindFirstByXPath("//Text[@Name='腾讯新闻']");
            if (item != null)
            {
                info.Title = "腾讯新闻";
                info.HeaderType = ChatType.腾讯新闻;
                return true;
            }
            item = root.FindFirstByXPath("//Text[@Name='服务通知']");
            if (item != null)
            {
                info.Title = "服务通知";
                info.HeaderType = ChatType.服务通知;
                return true;
            }

            item = root.FindFirstByXPath("//Text[@Name='微信团队']");
            if (item != null)
            {
                info.Title = "微信团队";
                info.HeaderType = ChatType.微信团队;
                return true;
            }
            item = root.FindFirstByXPath("//Text[@Name='文件传输助手']");
            if (item != null)
            {
                info.Title = "文件传输助手";
                info.HeaderType = ChatType.文件传输助手;
                return true;
            }

            item = item = root.FindFirstByXPath("//Button[@Name='公众号主页']");
            if (item != null)
            {
                var fetchRoot = item.GetParent().GetParent().GetSibling(-1);
                item = fetchRoot.FindFirstByXPath("/Pane/Pane[1]/Pane/Text[1]");
                info.Title = item.Name;
                info.HeaderType = ChatType.公众号;
            }

            return false;
        }
        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>聊天标题和聊天信息按钮名称</returns>
        public override string ToString()
        {
            return $"Title: {Title.Title} - HeaderType:{Title.ChatNumber.ToString()} - ChatNumber:{Title.ChatNumber}";
        }
    }
}
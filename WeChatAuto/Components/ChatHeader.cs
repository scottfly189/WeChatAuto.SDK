using System;
using FlaUI.Core.AutomationElements;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Enums;
using WeAutoCommon.Models;
using System.Text.RegularExpressions;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using System.Reflection.PortableExecutable;

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
        /// <summary>
        /// 聊天标题
        /// </summary>
        public HeaderInfo Title
        {
            get
            {
                HeaderInfo info = new HeaderInfo()
                {
                    Title = "",
                    HeaderType = ChatType.其他,
                };
                var result = TryCheckFriend(info);
                if (!result)
                {
                    result = TryCheckMp(info);
                    if (!result)
                    {
                        result = TryCheckAnother(info);
                    }
                }
                return info;
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
                        info.HeaderType = ChatType.好友;
                        info.Title = label;
                    }
                }
                else
                {
                    _logger.Error("WeChatAuto.SDK发生了错误,作者没有考虑到一些情况,请通知作者修改");
                    throw new Exception("WeChatAuto.SDK发生了错误,作者没有考虑到一些情况,请通知作者修改");
                }
            }
            return false;
        }
        private bool TryCheckMp(HeaderInfo info)
        {
            var item = _window.FindFirstByXPath("//Button[@Name='公众号主页']");
            if (item != null)
            {
                info.Title = "公众号";
                info.HeaderType = ChatType.公众号;
            }
            return false;
        }
        private bool TryCheckAnother(HeaderInfo info)
        {
            var item = _window.FindFirstByXPath("//Text[@Name='订阅号']");
            if (item != null)
            {
                info.Title = "订阅号";
                info.HeaderType = ChatType.订阅号;
                return true;
            }
            item = _window.FindFirstByXPath("//Text[@Name='腾讯新闻']");
            if (item != null)
            {
                info.Title = "腾讯新闻";
                info.HeaderType = ChatType.腾讯新闻;
                return true;
            }
            item = _window.FindFirstByXPath("//Test[@Name='服务通知']");
            if (item != null)
            {
                info.Title = "服务通知";
                info.HeaderType = ChatType.服务通知;
                return true;
            }

            item = _window.FindFirstByXPath("//Test[@Name='微信团队']");
            if (item != null)
            {
                info.Title = "微信团队";
                info.HeaderType = ChatType.微信团队;
                return true;
            }

            return false;
        }
        /// <summary>
        /// 聊天信息按钮
        /// </summary>
        public Button ChatInfoButton { get; set; }
        /// <summary>
        /// 聊天内容区标题区构造函数
        /// </summary>
        /// <param name="chatInfoButton">聊天信息按钮</param>
        /// <param name="serviceProvider">服务提供者<see cref="IServiceProvider"/></param>
        /// <param name="window">Header所归属的Window</param>
        public ChatHeader(Button chatInfoButton, IServiceProvider serviceProvider, Window window)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<ChatContent>>();
            this._window = window;
            ChatInfoButton = chatInfoButton;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 点击聊天信息按钮
        /// </summary>
        public void ClickChatInfoButton()
        {
            ChatInfoButton?.Invoke();
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
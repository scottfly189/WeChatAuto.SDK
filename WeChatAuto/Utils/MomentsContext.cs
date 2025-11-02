using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using FlaUI.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Extentions;
using WeChatAuto.Services;
using WxAutoCommon.Models;
using WxAutoCommon.Simulator;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 朋友圈上下文
    /// 最主要提供给终端用户使用，用于获取朋友圈内容列表，并进行点赞、回复评论等操作
    /// </summary>
    public class MomentsContext
    {
        private readonly AutoLogger<MomentsContext> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly MomentItem _moment;
        private readonly ListBox _rootListBox;
        private readonly IScrollPattern _pattern;
        private readonly Window _momentWindow;
        private double _scrollAmount;
        public MomentItem Moment => _moment;
        private const double SCROLL_STEP = 0.05;
        public MomentsContext(MomentItem moment, double scrollAmount,
            ListBox rootListBox, IScrollPattern pattern, Window momentWindow, IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._logger = serviceProvider.GetRequiredService<AutoLogger<MomentsContext>>();
            this._moment = moment;
            this._scrollAmount = scrollAmount;
            this._rootListBox = rootListBox;
            this._pattern = pattern;
            this._momentWindow = momentWindow;
        }
        /// <summary>
        /// 获取历史回复列表
        /// 此历史回复列表可以做为给大模型提供上下文信息。
        /// </summary>
        /// <returns>历史回复列表<see cref="ReplyItem"/></returns>
        public List<ReplyItem> GetHistoryReplyItems()
        {
            return _moment.ReplyItems;
        }
        /// <summary>
        /// 获取朋友圈内容项
        /// 此内容项可以做为大模型提供上下文信息。
        /// 如：朋友圈发表内容、朋友圈时间等。
        /// </summary>
        /// <returns>朋友圈内容项<see cref="MomentItem"/></returns>
        public MomentItem GetMomentItem() => _moment;
        /// <summary>
        /// 获取朋友圈内容
        /// 此内容可以做为大模型提供上下文信息。
        /// 如：朋友圈发表内容、朋友圈时间等。
        /// </summary>
        /// <returns>朋友圈内容</returns>
        public string GetMomentContent()
        {
            return _moment.ListItemName;
        }
        /// <summary>
        /// 获取朋友圈列表项唯一标识
        /// 此标识可以做为大模型提供上下文信息。
        /// 如：朋友圈记录唯一标识。
        /// </summary>
        /// <returns>朋友圈列表项唯一标识</returns>
        public string GetMomentKey()
        {
            return _moment.ListItemKey;
        }
        /// <summary>
        /// 是否是我最后一个回复的人，供大模型自动回复时参考。
        /// </summary>
        /// <returns>是否是我最后一个回复的人</returns>
        public bool IsMyEndReply()
        {
            return _moment.IsMyEndReply;
        }
        /// <summary>
        /// 是否包含我的回复，供大模型自动回复时参考。
        /// </summary>
        /// <returns>是否包含我的回复</returns>
        public bool IsIncludeMyReply()
        {
            return _moment.IsIncludeMyReply;
        }
        /// <summary>
        /// 是否是我点赞的，供大模型自动回复时参考。
        /// </summary>
        /// <returns>是否是我点赞的</returns>
        public bool IsMyLiked()
        {
            return _moment.IsMyLiked;
        }
        /// <summary>
        /// 回复朋友圈
        /// </summary>
        /// <param name="replyContent">回复内容</param>
        public void DoReply(string replyContent)
        {
            _logger.Info("回复朋友圈开始...");
            if (!_IsPoupMenuShown())
            {
                _ClickPopupMenuButton();
            }

            var xPath = "//Button[2][@Name='评论']";
            var replyButtonResult = Retry.WhileNull(() => _momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
            if (replyButtonResult.Success && replyButtonResult.Result != null)
            {
                var replyButton = replyButtonResult.Result;
                replyButton.WaitUntilClickable();
                replyButton.DrawHighlightExt();
                if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                {
                    KMSimulatorService.LeftClick(_momentWindow, replyButton);
                }
                else
                {
                    replyButton.Click();
                }
                Thread.Sleep(600);
                var sendButtonResult = Retry.WhileNull(() => _momentWindow.FindFirstByXPath("//Button[@Name='发送']")?.AsButton(),
                   timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                if (sendButtonResult.Success && sendButtonResult.Result != null)
                {
                    var sendButton = sendButtonResult.Result;
                    var index = 0;
                    while (sendButton.IsOffscreen && index < 3)
                    {
                        _scrollAmount += SCROLL_STEP;
                        _pattern.SetScrollPercent(0, _scrollAmount);
                        Thread.Sleep(600);
                        sendButton = _momentWindow.FindFirstByXPath("//Button[@Name='发送']")?.AsButton();
                        index++;
                    }
                    var parentPane = sendButton.GetParent().GetParent().GetParent();
                    var contentArea = parentPane.FindFirstByXPath("//Edit[@Name='评论']")?.AsTextBox();
                    contentArea?.WaitUntilClickable();
                    contentArea?.DrawHighlightExt();
                    _momentWindow.SilenceEnterText(contentArea, replyContent);
                    _momentWindow.SilenceReturn(contentArea);
                    _logger.Info("回复内容输入完成...");
                    Thread.Sleep(600);
                }
            }
            _logger.Info("回复朋友圈结束...");
        }
        /// <summary>
        /// 点赞朋友圈
        /// </summary>
        public void DoLike()
        {
            if (!_moment.IsMyLiked)
            {
                _logger.Info("点赞朋友圈开始...");
                if (!_IsPoupMenuShown())
                {
                    _ClickPopupMenuButton();
                }
                var xPath = "//Button[1][@Name='赞']";
                var linkButtonResult = Retry.WhileNull(() => _momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                    timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                if (linkButtonResult.Success && linkButtonResult.Result != null)
                {
                    var linkButton = linkButtonResult.Result;
                    linkButton.WaitUntilClickable();
                    linkButton.DrawHighlightExt();
                    if (WeAutomation.Config.EnableMouseKeyboardSimulator)
                    {
                        KMSimulatorService.LeftClick(_momentWindow, linkButton);
                    }
                    else
                    {
                        linkButton.Click();
                    }
                    Thread.Sleep(600);
                }
                _logger.Info("点赞朋友圈结束...");

            }
            else
            {
                _logger.Info("已经点赞过朋友圈，不再点赞...");
            }
        }
        /// <summary>
        /// 浮动菜单是否显示
        /// </summary>
        /// <returns>是否显示</returns>
        private bool _IsPoupMenuShown()
        {
            var xPath = "//Button[1][@Name='赞']";
            var linkButtonResult = Retry.WhileNull(() => _momentWindow.FindFirstByXPath(xPath)?.AsButton(),
                timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
            return linkButtonResult.Success && linkButtonResult.Result != null;
        }
        /// <summary>
        /// 点击浮动菜单按钮
        /// </summary>
        private void _ClickPopupMenuButton()
        {
            _logger.Info("点击浮动菜单按钮开始...");
            var items = _rootListBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            var item = items.FirstOrDefault(subItem => MomentItem.GetListItemKey(subItem.Name) == _moment.ListItemKey);
            var xPath = "//Button[@Name='评论']";
            var button = item.FindFirstByXPath(xPath)?.AsButton();
            var index = 0;
            while (button.IsOffscreen && index < 3)
            {
                _scrollAmount += SCROLL_STEP;
                _pattern.SetScrollPercent(0, _scrollAmount);
                Thread.Sleep(600);
                button = item.FindFirstByXPath(xPath)?.AsButton();
                index++;
            }
            button.WaitUntilClickable();
            _momentWindow.Focus();
            button.DrawHighlightExt();
            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
            {
                KMSimulatorService.LeftClick(_momentWindow, button);
            }
            else
            {
                button.Click();
            }
            Thread.Sleep(600);
            _logger.Info("点击浮动菜单按钮结束...");
        }

        public double GetScrollAmount() => _scrollAmount;
    }
}
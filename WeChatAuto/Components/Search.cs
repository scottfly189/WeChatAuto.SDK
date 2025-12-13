using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using WeChatAuto.Utils;
using WxAutoCommon.Interface;
using WeChatAuto.Extentions;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatAuto.Components
{
    public class Search
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<Search> _logger;
        private UIThreadInvoker _uiMainThreadInvoker;
        private WeChatMainWindow _WxWindow;
        public Search(WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, Window window, IServiceProvider serviceProvider)
        {
            _uiMainThreadInvoker = uiThreadInvoker;
            _WxWindow = wxWindow;
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<AutoLogger<Search>>();
        }


        /// <summary>
        /// 输入搜索内容，并回车
        /// </summary>
        /// <param name="text">搜索内容</param>
        /// <param name="isClear">是否清空搜索框,默认是False:不清空,True:清空</param>
        public void SearchSomething(string text, bool isClear = false)
        {
            var searchEdit = _uiMainThreadInvoker.Run(automation=>Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"//Edit[@Name='{WeChatConstant.WECHAT_SESSION_SEARCH}']"),
                timeout: TimeSpan.FromSeconds(5),
                interval: TimeSpan.FromMilliseconds(200))).GetAwaiter().GetResult();
            if (searchEdit.Success)
            {
                WaitHelper.WaitTextBoxReady(searchEdit.Result, TimeSpan.FromSeconds(5), _uiMainThreadInvoker);
                var textBox = searchEdit.Result.AsTextBox();
                textBox.Focus();
                DrawHightlightHelper.DrawHightlight(textBox, _uiMainThreadInvoker);
                if (isClear)
                {
                    ClearText();
                }
                _WxWindow.SilenceEnterText(textBox, text);
                Wait.UntilInputIsProcessed(TimeSpan.FromSeconds(1));
                _WxWindow.SilenceReturn(textBox);
            }else{
                _logger.Error($"没有找到搜索框,搜索内容: {text}");
            }
        }
        /// <summary>
        /// 清空搜索框
        /// </summary>
        public void ClearText()
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                var clearButton = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='{WeChatConstant.WECHAT_SESSION_CLEAR}']"),
                timeout: TimeSpan.FromSeconds(5),
                interval: TimeSpan.FromMilliseconds(200));
                if (clearButton.Success)
                {
                    _WxWindow.SilenceClickExt(clearButton.Result);
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 搜索聊天
        /// </summary>
        /// <param name="who">好友名称,可以是群聊名称也可以是好友名称</param>
        public void SearchChat(string who)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.聊天);
            SearchSomething(who);
        }

        /// <summary>
        /// 在通讯录页面搜索联系人
        /// </summary>
        /// <param name="who">联系人名称</param>
        public void SearchContact(string who)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.通讯录);
            SearchSomething(who);
        }

        /// <summary>
        /// 在收藏页面搜索收藏的内容
        /// </summary>
        /// <param name="content">搜索内容</param>
        public void SearchCollection(string content)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.收藏);
            SearchSomething(content, true);
        }
    }
}
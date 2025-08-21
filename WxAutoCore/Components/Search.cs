using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;
using WxAutoCommon.Interface;
using WxAutoCore.Extentions;

namespace WxAutoCore.Components
{
    public class Search
    {
        private WxMainWindow _WxWindow;
        public Search(WxMainWindow wxWindow)
        {
            _WxWindow = wxWindow;
        }


        /// <summary>
        /// 输入搜索内容，并回车
        /// </summary>
        /// <param name="text"></param>
        public void SearchSomething(string text, bool isClear = false)
        {
            var searchEdit = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='{WeChatConstant.WECHAT_SESSION_SEARCH}']"),
            timeout: TimeSpan.FromSeconds(10),
            interval: TimeSpan.FromMilliseconds(200));
            if (searchEdit.Success)
            {
                WaitHelper.WaitTextBoxReady(searchEdit.Result, TimeSpan.FromSeconds(5));
                var textBox = searchEdit.Result.AsTextBox();
                DrawHightlightHelper.DrawHightlight(textBox);
                if (isClear)
                {
                    ClearText();
                }
                _WxWindow.SilenceEnterText(textBox, text);
                // _WxWindow.SilenceClickExt(textBox);
                Wait.UntilInputIsProcessed(TimeSpan.FromSeconds(1));
                // Keyboard.Press(VirtualKeyShort.RETURN);
                _WxWindow.SilenceReturn(textBox);
            }
        }
        /// <summary>
        /// 清空搜索框
        /// </summary>
        public void ClearText()
        {
            var clearButton = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='{WeChatConstant.WECHAT_SESSION_CLEAR}']"),
            timeout: TimeSpan.FromSeconds(5),
            interval: TimeSpan.FromMilliseconds(200));
            if (clearButton.Success)
            {
                _WxWindow.SilenceClickExt(clearButton.Result);
            }
        }

        /// <summary>
        /// 搜索聊天
        /// 更多导航类型请参考<see cref="NavigationType"/>
        /// </summary>
        /// <param name="text">搜索内容</param>
        public void SearchChat(string text)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.聊天);
            SearchSomething(text);
        }

        /// <summary>
        /// 搜索联系人
        /// 更多导航类型请参考<see cref="NavigationType"/>
        /// </summary>
        /// <param name="text">搜索内容</param>
        public void SearchContact(string text)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.通讯录);
            SearchSomething(text);
        }

        /// <summary>
        /// 搜索收藏
        /// 更多导航类型请参考<see cref="NavigationType"/>
        /// </summary>
        /// <param name="text">搜索内容</param>
        public void SearchCollection(string text)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.收藏);
            SearchSomething(text, true);
        }
    }
}
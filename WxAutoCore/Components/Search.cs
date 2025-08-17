using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    public class Search
    {
        private WxWindow _WxWindow;
        private TextBox _SearchEdit;
        public Search(WxWindow wxWindow)
        {
            _WxWindow = wxWindow;
        }


        /// <summary>
        /// 输入搜索内容，并回车
        /// </summary>
        /// <param name="text"></param>
        public void SearchSomething(string text,bool isClear = false)
        {
            var searchEdit = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='{WeChatConstant.WECHAT_SESSION_SEARCH}']"),
            timeout: TimeSpan.FromSeconds(10),
            interval: TimeSpan.FromMilliseconds(200));
            if (searchEdit.Success)
            {
                Wait.UntilResponsive(searchEdit.Result, TimeSpan.FromSeconds(5));
                var textBox = searchEdit.Result.AsTextBox();
                DrawHightlightHelper.DrawHightlight(textBox);
                if (isClear)
                {
                    ClearText();
                }
                textBox.FocusNative();
                textBox.Focus();
                textBox.Click();
                textBox.Enter(text);
                _WxWindow.Window.Focus();
                textBox.Focus();
                Wait.UntilInputIsProcessed(TimeSpan.FromSeconds(1));
                Keyboard.Press(VirtualKeyShort.RETURN);
            }
        }
        /// <summary>
        /// 清空搜索框
        /// </summary>
        private void ClearText()
        {
            var clearButton = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath($"/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='{WeChatConstant.WECHAT_SESSION_CLEAR}']"),
            timeout: TimeSpan.FromSeconds(5),
            interval: TimeSpan.FromMilliseconds(200));
            if (clearButton.Success)
            {
                clearButton.Result.Click();
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
            SearchSomething(text,true);
        }
    }
}
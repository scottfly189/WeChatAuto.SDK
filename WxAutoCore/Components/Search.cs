using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Enums;
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
        public void SearchSomething(string text)
        {
            var searchEdit = Retry.WhileNull(() => _WxWindow.Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='搜索']"),
            timeout: TimeSpan.FromSeconds(10),
            interval: TimeSpan.FromMilliseconds(200));
            if (searchEdit.Success)
            {
                Wait.UntilResponsive(searchEdit.Result, TimeSpan.FromSeconds(5));
                var textBox = searchEdit.Result.AsTextBox();
                DrawHightlightHelper.DrawHightlight(textBox);
                textBox.FocusNative();
                textBox.Focus();
                textBox.Click();
                textBox.Enter(text);
                Keyboard.Type(VirtualKeyShort.RETURN);
            }
        }

        /// <summary>
        /// 搜索聊天
        /// </summary>
        /// <param name="text">搜索内容</param>
        public void SearchChat(string text)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.聊天);
            SearchSomething(text);
        }

        /// <summary>
        /// 搜索联系人
        /// </summary>
        /// <param name="text">搜索内容</param>
        public void SearchContact(string text)
        {
            _WxWindow.Navigation.SwitchNavigation(NavigationType.通讯录);
            SearchSomething(text);
        }
    }
}
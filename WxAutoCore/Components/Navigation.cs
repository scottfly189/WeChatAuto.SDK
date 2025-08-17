using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    public class Navigation
    {
        public WxLocationCaches _wxLocationCaches = new WxLocationCaches();
        public AutomationElement CurrentNavigationElement { get; private set; }
        private Window _Window;
        public Navigation(Window window)
        {
            _Window = window;
            _InitNavigation();
        }
        private void _InitNavigation()
        {
            var navigationRoot = _Window.FindFirstByXPath($"/Pane/Pane/ToolBar[@Name='{WeChatConstant.WECHAT_NAVIGATION_NAVIGATION}'][@IsEnabled='true']");
            _wxLocationCaches.AddXPathLocation(NavigationType.聊天.ToString(),
                parentElement: navigationRoot,
                xPath: $"/Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_CHAT}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.通讯录.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_CONTACT}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.收藏.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_COLLECT}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.聊天文件.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_FILE}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.朋友圈.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_MOMENT}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.视频号.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_VIDEO}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.看一看.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_READ}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.搜一搜.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_SEARCH}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.小程序面板.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_APP}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.手机.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_PHONE}'][@IsEnabled='true']"
            );
            _wxLocationCaches.AddXPathLocation(NavigationType.设置及其他.ToString(),
                parentElement: navigationRoot,
                xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_SETTING}'][@IsEnabled='true']"
            );
            CurrentNavigationElement = _wxLocationCaches.GetElement(NavigationType.聊天.ToString());
            CurrentNavigationElement.Click();
        }
        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void SwitchNavigation(NavigationType navigationType)
        {
            var name = navigationType.ToString();
            var button = _wxLocationCaches.GetElement(name)?.AsButton();
            if (button != null)
            {
                if (Wait.UntilResponsive(button, timeout: TimeSpan.FromSeconds(5)))
                {
                    DrawHightlightHelper.DrawHightlight(button);
                    if (CurrentNavigationElement.Name != button.Name)
                    {
                        button.Click();
                        CurrentNavigationElement = button;
                    }
                }
            }
        }


    }
}
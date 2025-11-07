using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatAuto.Components
{
    public class Navigation
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<Navigation> _logger;
        private UIThreadInvoker _uiThreadInvoker;
        private IWeChatWindow _WxWindow;
        public WxLocationCaches _wxLocationCaches = new WxLocationCaches();
        private Window _Window;
        /// <summary>
        /// 导航栏构造函数
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="wxWindow">微信窗口</param>
        /// <param name="uiThreadInvoker">UI线程执行器</param>
        /// <param name="serviceProvider">服务提供者</param>
        public Navigation(Window window, IWeChatWindow wxWindow, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<Navigation>>();
            _uiThreadInvoker = uiThreadInvoker;
            _Window = window;
            _WxWindow = wxWindow;
            _serviceProvider = serviceProvider;
            _InitNavigation();
        }
        private void _InitNavigation()
        {
            _uiThreadInvoker.Run(automation =>
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
                    xPath: $"//Button[@Name='{WeChatConstant.WECHAT_NAVIGATION_VIDEO}'][@IsEnabled='true']  "
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
            }).GetAwaiter().GetResult();
            SwitchNavigation(NavigationType.聊天);
        }
        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void SwitchNavigation(NavigationType navigationType)
        {
            _uiThreadInvoker.Run(automation =>
            {
                var name = navigationType.ToString();
                var button = _wxLocationCaches.GetElement(name)?.AsButton();
                if (button != null)
                {
                    if (Wait.UntilResponsive(button, timeout: TimeSpan.FromSeconds(5)))
                    {
                        button.Focus();
                        button.DrawHighlightExt();
                        button.Click();
                        _logger.Info($"切换到导航栏：{navigationType.ToString()}");
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 关闭导航栏
        /// 仅支持聊天文件、朋友圈、视频号、看一看、搜一搜、小程序面板
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void CloseNavigation(NavigationType navigationType)
        {
            _uiThreadInvoker.Run(automation =>
            {
                RetryResult<AutomationElement> window = null;
                switch (navigationType)
                {
                    case NavigationType.聊天文件:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_NAVIGATION_FILE}'][@ClassName='FileListMgrWnd'][@ProcessId={_Window.Properties.ProcessId}]"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    case NavigationType.朋友圈:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_NAVIGATION_MOMENT}'][@ClassName='SnsWnd'][@ProcessId={_Window.Properties.ProcessId}]"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    case NavigationType.视频号:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_SYSTEM_NAME}'][@ClassName='Chrome_WidgetWin_0'][@IsEnabled='true']"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    case NavigationType.看一看:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_SYSTEM_NAME}'][@ClassName='Chrome_WidgetWin_0'][@IsEnabled='true']"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    case NavigationType.搜一搜:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_SYSTEM_NAME}'][@ClassName='Chrome_WidgetWin_0'][@IsEnabled='true']"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    case NavigationType.小程序面板:
                        window = Retry.WhileNull(checkMethod: () => _Window.Automation.GetDesktop()
                            .FindFirstByXPath($"/Window[@Name='{WeChatConstant.WECHAT_SYSTEM_NAME}'][@ClassName='Chrome_WidgetWin_0'][@IsEnabled='true']"),
                            timeout: TimeSpan.FromSeconds(10),
                            interval: TimeSpan.FromMilliseconds(200)
                        );
                        break;
                    default:
                        break;
                }
                if (window.Success)
                {
                    window.Result.AsWindow().Close();
                }
            }).GetAwaiter().GetResult();
        }

    }
}
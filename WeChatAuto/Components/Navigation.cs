using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using WeAutoCommon.Enums;
using WeAutoCommon.Interface;
using WeAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Models;
using System.Threading.Tasks;
using System.Drawing;

namespace WeChatAuto.Components
{
    public class Navigation
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<Navigation> _logger;
        private UIThreadInvoker _uiMainThreadInvoker;
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
            _uiMainThreadInvoker = uiThreadInvoker;
            _Window = window;
            _WxWindow = wxWindow;
            _serviceProvider = serviceProvider;
            _InitNavigation();
        }
        private void _InitNavigation()
        {
            _uiMainThreadInvoker.Run(automation =>
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
        /// 通过导航栏获得窗口的wxid
        /// </summary>
        /// <returns>个人信息<see cref="FriendInfo"/></returns>
        public async Task<FriendInfo> GetWxId()
        {
            var info = await _uiMainThreadInvoker.Run<FriendInfo>(automation =>
            {
                var path = "/Pane/Pane/ToolBar/Button[1]";
                var button = _Window.FindFirstByXPath(path).AsButton();
                button.ClickEnhance(_Window);
                RetryResult<FriendInfo> retryResult = Retry.WhileNull(() =>
                {
                    FriendInfo result = new FriendInfo();
                    path = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane[1]/Text";
                    var label = _Window.FindFirstByXPath(path).AsLabel();
                    result.NickName = label.Name;
                    path = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Text[2]";
                    label = _Window.FindFirstByXPath(path).AsLabel();
                    result.WxId = label.Name;
                    return result;
                }, timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                return retryResult.Success ? retryResult.Result : null;
            });
            Random rand = new Random(DateTime.Now.Millisecond);
            await Task.Delay(rand.Next(300, 1000));
            SwitchNavigation(NavigationType.聊天);
            return info;
        }
        /// <summary>
        /// 保存个人头像
        /// </summary>
        /// <param name="savePath">保存的目录与文件名，如: c:\temp\avator.jpg</param>
        /// <returns></returns>
        public async Task SaveOwnerAvator(string savePath)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            await _uiMainThreadInvoker.Run(async automation =>
            {
                var path = "/Pane/Pane/ToolBar/Button[1]";
                var button = _Window.FindFirstByXPath(path).AsButton();
                button.ClickEnhance(_Window);
                RetryResult<Button> retryResult = Retry.WhileNull(() =>
                {
                    path = "/Pane[1]/Pane[2]/Pane/Pane/Pane/Pane[1]/Button";
                    button = _Window.FindFirstByXPath(path).AsButton();
                    return button;
                }, timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                if (retryResult.Success)
                {
                    RandomWait.Wait(300, 1000);
                    button = retryResult.Result;
                    button.Click();
                    RetryResult<Window> retryWindow = Retry.WhileNull(() =>
                    {
                        var desktop = automation.GetDesktop();
                        var window = desktop.FindFirstChild(x => x.ByControlType(ControlType.Window).And(x.ByProcessId(_Window.Properties.ProcessId).And(x.ByName("图片查看")))).AsWindow();
                        return window;
                    }, timeout: TimeSpan.FromSeconds(5), interval: TimeSpan.FromMilliseconds(200));
                    if (retryWindow.Success)
                    {
                        var win = retryWindow.Result;
                        path = "/Pane[2]/Pane[1]/Pane[2]/Pane[2]/Button[1]";
                        button = win.FindFirstByXPath(path).AsButton();
                        button.Click();
                        //选择第一个菜单
                        var menuRetry = Retry.WhileNull(() => win.FindFirstChild(cf => cf.Menu()).AsMenu(),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromMilliseconds(200));
                        if (menuRetry.Success)
                        {
                            var menuItem = menuRetry.Result.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("复制")));
                            if (menuItem != null)
                            {
                                menuItem.DrawHighlightExt();
                                menuItem.WaitUntilClickable(TimeSpan.FromSeconds(3));
                                menuItem.ClickEnhance(win);
                                RandomWait.Wait(1000, 2000);
                                if (System.Windows.Clipboard.ContainsImage())
                                {
                                    var bitmap = System.Windows.Forms.Clipboard.GetImage();
                                    bitmap.Save(savePath);
                                }                                
                            }
                            else
                            {
                                _logger.Error($"找不到多选菜单项");
                            }
                        }
                        RandomWait.Wait(100, 800);
                        win.Close();
                    }
                }
            });
            await Task.Delay(random.Next(300, 1000));
            SwitchNavigation(NavigationType.聊天);
        }

        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void SwitchNavigation(NavigationType navigationType)
        {
            _uiMainThreadInvoker.Run(automation =>
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
            _uiMainThreadInvoker.Run(automation =>
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
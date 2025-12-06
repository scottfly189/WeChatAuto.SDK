using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Utils;
using WeChatAuto.Utils;
using FlaUI.Core.Definitions;
using System.Threading;
using WeChatAuto.Extentions;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信窗口工具栏封装
    /// </summary>
    public class ToolBar
    {
        private readonly IServiceProvider _serviceProvider;
        private UIThreadInvoker _uiMainThreadInvoker;
        private AutomationElement _ToolBar;
        private Window _MainWindow;
        private Button _TopButton;
        private Button _MinButton;
        private Button _MaxButton;   //最大化或者还原
        private Button _CloseButton;
        private AutoLogger<ToolBar> _logger;

        public AutomationElement ToolBarInfo => _ToolBar;
        /// <summary>
        /// 工具栏构造函数
        /// </summary>
        /// <param name="mainWindow">主窗口</param>
        /// <param name="notifyIcon">通知图标</param>
        /// <param name="uiThreadInvoker">UI线程执行器</param>
        /// <param name="serviceProvider">服务提供者</param>
        /// <param name="serviceProvider">服务提供者</param>
        public ToolBar(Window mainWindow, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _MainWindow = mainWindow;
            _uiMainThreadInvoker = uiThreadInvoker;
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<AutoLogger<ToolBar>>();
        }

        private void RefreshToolBar()
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                _ToolBar = _MainWindow.FindFirstByXPath("/Pane/Pane/Pane[2]/ToolBar");
                var childen = _ToolBar.FindAllChildren();
                _TopButton = childen[0].AsButton();
                _MinButton = childen[1].AsButton();
                _MaxButton = childen[2].AsButton();   // 最大化或者还原
                _CloseButton = childen[3].AsButton();
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 置顶
        /// </summary>
        public void Top(bool isTop = true)
        {
            RefreshToolBar();
            SetVisible();
            if (isTop)
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_TOP_BUTTON))
                {
                    _uiMainThreadInvoker.Run(automation =>
                    {
                        _MainWindow.Focus();
                        Wait.UntilResponsive(_TopButton);
                        _TopButton.ClickEnhance(_MainWindow);
                    }).GetAwaiter().GetResult();
                }
            }
            else
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_UNTOP_BUTTON))
                {
                    _MainWindow.Focus();
                    Wait.UntilResponsive(_TopButton);
                    _TopButton.ClickEnhance(_MainWindow);
                }
            }
        }

        private bool IsHide()
        {
            return _uiMainThreadInvoker.Run(automation =>
            {
                if (_MainWindow.Patterns.Window.IsSupported)
                {
                    var windowPattern = _MainWindow.Patterns.Window.Pattern;
                    if (windowPattern != null)
                    {
                        var windowState = windowPattern.WindowVisualState;
                        if (windowState == WindowVisualState.Minimized)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }).GetAwaiter().GetResult();
        }

        private void SetVisible()
        {
            _uiMainThreadInvoker.Run(automation =>
            {
                if (_MainWindow.Patterns.Window.IsSupported)
                {
                    var windowPattern = _MainWindow.Patterns.Window.Pattern;
                    if (windowPattern != null)
                    {
                        var windowState = windowPattern.WindowVisualState;
                        if (windowState == WindowVisualState.Minimized)
                        {
                            windowPattern.SetWindowVisualState(WindowVisualState.Normal);
                            Thread.Sleep(1_000);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        public void Min()
        {
            if (IsHide())
            {
                _logger.Info("窗口已最小化，无需最小化");
                return;
            }
            RefreshToolBar();
            _uiMainThreadInvoker.Run(automation =>
            {
                _MainWindow.Focus();
                Wait.UntilResponsive(_MinButton);
                _MinButton.ClickEnhance(_MainWindow);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public void Max()
        {
            RefreshToolBar();
            SetVisible();
            if (isMax())
            {
                _logger.Info("窗口已最大化，无需最大化");
                return;
            }
            _uiMainThreadInvoker.Run(automation =>
            {
                if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_MAX_BUTTON))
                {
                    _MainWindow.Focus();
                    Wait.UntilResponsive(_MaxButton);
                    _MaxButton.ClickEnhance(_MainWindow);
                }
            }).GetAwaiter().GetResult();
        }
        private bool isMax()
        {
            return _uiMainThreadInvoker.Run(automation =>
            {
                if (_MainWindow.Patterns.Window.IsSupported)
                {
                    var windowPattern = _MainWindow.Patterns.Window.Pattern;
                    if (windowPattern != null)
                    {
                        var windowState = windowPattern.WindowVisualState;
                        return windowState == WindowVisualState.Maximized;
                    }
                }
                return false;
            }).GetAwaiter().GetResult();
        }
        private bool isNormal()
        {
            return _uiMainThreadInvoker.Run(automation =>
            {
                if (_MainWindow.Patterns.Window.IsSupported)
                {
                    var windowPattern = _MainWindow.Patterns.Window.Pattern;
                    if (windowPattern != null)
                    {
                        var windowState = windowPattern.WindowVisualState;
                        return windowState == WindowVisualState.Normal;
                    }
                }
                return false;
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 还原
        /// </summary>
        public void Restore()
        {
            RefreshToolBar();
            SetVisible();
            if (isNormal())
            {
                _logger.Info("窗口已还原，无需还原");
                return;
            }
            _uiMainThreadInvoker.Run(automation =>
            {
                if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_RESTORE_BUTTON))
                {
                    _MainWindow.Focus();
                    Wait.UntilResponsive(_MaxButton);
                    _MaxButton.Click();
                }
            }).GetAwaiter().GetResult();
        }
    }
}
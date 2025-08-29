using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信窗口工具栏封装
    /// </summary>
    public class ToolBar
    {
        private UIThreadInvoker _uiThreadInvoker;
        private WeChatNotifyIcon _NotifyIcon;
        private AutomationElement _ToolBar;
        private Window _Window;
        private Button _TopButton;
        private Button _MinButton;
        private Button _MaxButton;   //最大化或者还原
        private Button _CloseButton;

        public AutomationElement ToolBarInfo => _ToolBar;
        public ToolBar(Window window, WeChatNotifyIcon notifyIcon, UIThreadInvoker uiThreadInvoker)
        {
            _Window = window;
            _NotifyIcon = notifyIcon;
            _uiThreadInvoker = uiThreadInvoker;
        }

        private void RefreshToolBar()
        {
            _uiThreadInvoker.Run(automation =>
            {
                _ToolBar = _Window.FindFirstByXPath("/Pane/Pane/Pane[2]/ToolBar");
                var childen = _ToolBar.FindAllChildren();
                _TopButton = childen[0].AsButton();
                _MinButton = childen[1].AsButton();
                _MaxButton = childen[2].AsButton();   // 最大化或者还原
                _CloseButton = childen[3].AsButton();
            }).Wait();
        }

        /// <summary>
        /// 置顶
        /// </summary>
        public void Top(bool isTop = true)
        {
            RefreshToolBar();
            if (isTop)
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_TOP_BUTTON))
                {
                    _uiThreadInvoker.Run(automation =>
                    {
                        Wait.UntilResponsive(_TopButton);
                        _TopButton.Click();
                    }).Wait();
                }
            }
            else
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_UNTOP_BUTTON))
                {
                    Wait.UntilResponsive(_TopButton);
                    _TopButton.Click();
                }
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        public void Min()
        {
            RefreshToolBar();
            _uiThreadInvoker.Run(automation =>
            {
                Wait.UntilResponsive(_MinButton);
                _MinButton.Click();
            }).Wait();
        }

        /// <summary>
        /// 最小化后的还原操作
        /// </summary>
        public void MinRestore()
        {
            _uiThreadInvoker.Run(automation =>
            {
                Wait.UntilResponsive(_MinButton);
                _MinButton.Click();
            }).Wait();
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public void Max()
        {
            RefreshToolBar();
            _uiThreadInvoker.Run(automation =>
            {
                if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_MAX_BUTTON))
                {
                    Wait.UntilResponsive(_MaxButton);
                    _MaxButton.Click();
                }
            }).Wait();
        }

        /// <summary>
        /// 还原
        /// </summary>
        public void Restore()
        {
            RefreshToolBar();
            _uiThreadInvoker.Run(automation =>
            {
                if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_RESTORE_BUTTON))
                {
                    Wait.UntilResponsive(_MaxButton);
                    _MaxButton.Click();
                }
            }).Wait();
        }
    }
}
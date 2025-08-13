using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;


namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信窗口管理
    /// </summary>
    public class WxClient
    {
        private readonly UIA3Automation _automation;
        private readonly Window _wxWindow;
        private readonly Dictionary<string, (Window window, int processId, int notifyIconId, Button notifiIconButton)> _wxWindows = new Dictionary<string, (Window window, int processId, int notifyIconId, Button button)>();
        private readonly AutomationElement _desktop;

        public AutomationElement Desktop => _desktop;
        public UIA3Automation Automation => _automation;

        public WxClient()
        {
            _automation = new UIA3Automation();
            _desktop = _automation.GetDesktop();
        }

        /// <summary>
        /// 初始化微信窗口
        /// </summary>
        public void Init()
        {
            _RefreshWxWindows();
        }

        /// <summary>
        /// 重新获取微信窗口
        /// </summary>
        private void _RefreshWxWindows()
        {
            _wxWindows.Clear();
            var wxInstances = _desktop.FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                            .And(cf.ByClassName("WeChatMainWndForPC")
                            .And(cf.ByControlType(ControlType.Window))));
            var notifyIconRoot = _desktop.FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd")));
            var wxNotifyList = notifyIconRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar))))
                .FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME).And(cf.ByControlType(ControlType.Button)));
            for (int i = 0; i < wxNotifyList.Length; i++)
            {
                var wxNotify = wxNotifyList[i];
                var wxInstance = wxInstances[i];
                var button = wxInstance.FindFirstByXPath("/Pane[2]/Pane/ToolBar/Button[1]").AsButton();
                _wxWindows.Add(button.Name, (wxInstance.AsWindow(), wxInstance.Properties.ProcessId.Value, wxNotify.Properties.ProcessId.Value, wxNotify.AsButton()));
            }
        }

        /// <summary>
        /// 显示微信
        /// 如果wxNickName为空，则显示第一个微信窗口
        /// 如果wxNickName不为空，则显示指定微信窗口
        /// </summary>
        /// <param name="wxNickName">登录的微信昵称</param>
        public void Show(string wxNickName = "")
        {
            var firstKey = _wxWindows.Keys.ToList().FirstOrDefault();
            Button button = null;
            Window wxWindow = null;
            if (string.IsNullOrEmpty(wxNickName))
            {
                button = _wxWindows[firstKey].notifiIconButton;
                wxWindow = _wxWindows[firstKey].window;
            }
            else
            {
                button = _wxWindows[wxNickName].notifiIconButton;
                wxWindow = _wxWindows[wxNickName].window;
            }
            if (button != null && wxWindow != null)
            {
                button.Click();
                wxWindow.Focus();
                wxWindow.SetForeground();
            }
        }
    }
}
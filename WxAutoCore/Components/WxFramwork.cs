using System.Collections.Generic;
using System.Windows.Forms;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    public class WxFramwork
    {
        private readonly UIA3Automation _automation;
        private readonly Dictionary<string, WxClient> _wxClientList = new Dictionary<string, WxClient>();
        private readonly AutomationElement _desktop;

        public AutomationElement Desktop => _desktop;
        public UIA3Automation Automation => _automation;
        public WxFramwork()
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
        /// 获取微信客户端
        /// </summary>
        /// <param name="name">微信客户端名称</param>
        /// <returns></returns>
        public WxClient GetWxClient(string name)
        {
            if (_wxClientList.ContainsKey(name))
            {
                return _wxClientList[name];
            }
            MessageBox.Show($"微信客户端{name}不存在，请检查微信是否运行");
            return null;
        }
        /// <summary>
        /// 重新获取微信窗口
        /// </summary>
        private void _RefreshWxWindows()
        {
            _wxClientList.Clear();
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
                _wxClientList.Add(button.Name, new WxClient(wxInstance.AsWindow(), wxInstance.Properties.ProcessId, button, button.Properties.ProcessId));
            }
        }
    }
}
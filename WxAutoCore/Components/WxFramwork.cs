using System.Collections.Generic;
using MessageBox = System.Windows.Forms.MessageBox;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WxAutoCommon.Utils;
using System;
using System.Windows.Forms;
using System.Linq;

namespace WxAutoCore.Components
{
    public class WxFramwork : IDisposable
    {
        private bool _IsInit = false;
        private readonly UIA3Automation _automation;
        private readonly Dictionary<string, WxClient> _wxClientList = new Dictionary<string, WxClient>();
        private readonly AutomationElement _desktop;
        public AutomationElement Desktop => _desktop;
        public UIA3Automation Automation => _automation;
        /// <summary>
        /// 微信客户端列表
        /// </summary>
        public Dictionary<string, WxClient> WxClientList
        {
            get
            {
                Init();
                return _wxClientList;
            }
        }
        /// <summary>
        /// 获取客户端名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetWxClientNames()
        {
            Init();
            return _wxClientList.Keys.ToList();
        }

        public WxFramwork()
        {
            _automation = new UIA3Automation();
            _desktop = _automation.GetDesktop();
        }

        /// <summary>
        /// 初始化微信窗口
        /// </summary>
        private void Init()
        {
            if (!_IsInit)
            {
                RefreshWxWindows();
                _IsInit = true;
            }
        }
        /// <summary>
        /// 清除所有事件
        /// </summary>
        public void ClearAllEvent()
        {
            _automation.UnregisterAllEvents();
        }
        /// <summary>
        /// 获取微信客户端
        /// </summary>
        /// <param name="name">微信客户端名称</param>
        /// <returns></returns>
        public WxClient GetWxClient(string name)
        {
            Init();
            if (_wxClientList.ContainsKey(name))
            {
                return _wxClientList[name];
            }
            MessageBox.Show($"微信客户端[{name}]不存在，请检查微信是否打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        /// <summary>
        /// 重新获取微信窗口
        /// </summary>
        public void RefreshWxWindows()
        {
            _wxClientList.Clear();
            var notifyIconRoot = _desktop.FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd")));
            var wxNotifyList = notifyIconRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar))))
                .FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME).And(cf.ByControlType(ControlType.Button)));

            var wxInstances = _desktop.FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                            .And(cf.ByClassName("WeChatMainWndForPC")
                            .And(cf.ByControlType(ControlType.Window))));
            for (int i = 0; i < wxNotifyList.Length; i++)
            {
                var wxNotify = wxNotifyList[i].AsButton();
                var wxInstance = wxInstances[i];  //这里可能有错误，因为微信notifyicon与实例并不是按索引一一对应
                var button = wxInstance.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]").AsButton();
                _wxClientList.Add(button.Name, new WxClient(wxInstance.AsWindow(), wxInstance.Properties.ProcessId.Value, wxNotify, wxNotify.Properties.ProcessId.Value));
            }
        }

        public void Dispose()
        {
            if (_automation != null)
            {
                ClearAllEvent();
                _automation.Dispose();
            }
        }
    }
}
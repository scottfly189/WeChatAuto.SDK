using System.Collections.Generic;
using MessageBox = System.Windows.Forms.MessageBox;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WxAutoCommon.Utils;
using System;
using System.Windows.Forms;
using System.Linq;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信自动化框架,封装的微信自动化框架，支持多微信实例
    /// </summary>
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
        /// <summary>
        /// 微信自动化框架构造函数
        /// </summary>
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
            var taskBarRoot = _desktop.FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd")));
            var wxNotifyList = taskBarRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar))))
                .FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME).And(cf.ByControlType(ControlType.Button)));
            foreach (var wxNotify in wxNotifyList)
            {
                DrawHightlightHelper.DrawHightlight(wxNotify);
                wxNotify.AsButton().Invoke();
                var topWindowProcessId = (int)WinApi.GetTopWindowProcessIdByClassName("WeChatMainWndForPC");
                var wxInstances = _desktop.FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                            .And(cf.ByClassName("WeChatMainWndForPC")
                            .And(cf.ByControlType(ControlType.Window))
                            .And(cf.ByProcessId(topWindowProcessId)))).AsWindow();
                DrawHightlightHelper.DrawHightlight(wxInstances);
                WxNotifyIcon wxNotifyIcon = new WxNotifyIcon(wxNotify.AsButton());
                WxWindow wxWindow = new WxWindow(wxInstances);
                var NickNameButton = wxInstances.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]").AsButton();
                _wxClientList.Add(NickNameButton.Name, new WxClient(wxNotifyIcon, wxWindow));
            }
            this._IsInit = true;
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
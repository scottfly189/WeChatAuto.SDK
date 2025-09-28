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
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信自动化框架,封装的微信自动化框架，支持多微信实例
    /// </summary>
    public class WeChatFramwork : IDisposable
    {
        private bool _IsInit = false;
        private readonly Dictionary<string, WeChatClient> _wxClientList = new Dictionary<string, WeChatClient>();
        private bool _disposed = false;
        /// <summary>
        /// 微信自动化框架构造函数
        /// </summary>
        public WeChatFramwork()
        {
            IMEHelper.DisableImeForCurrentThread();
        }
        /// <summary>
        /// 微信客户端列表
        /// </summary>
        public Dictionary<string, WeChatClient> WxClientList
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
            if (_disposed)
            {
                return;
            }
            foreach (var client in _wxClientList)
            {
                client.Value.WxMainWindow.Dispose();
            }
        }
        /// <summary>
        /// 获取微信客户端
        /// </summary>
        /// <param name="name">微信客户端名称</param>
        /// <returns></returns>
        public WeChatClient GetWxClient(string name)
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
        /// 获取微信客户端列表
        /// 微信客户端请参见<see cref="WeChatClient"/>
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, WeChatClient> GetWxClientList()
        {
            Init();
            return _wxClientList;
        }

        /// <summary>
        /// 重新获取微信窗口
        /// </summary>
        public void RefreshWxWindows()
        {
            if (_disposed)
            {
                return;
            }
            _wxClientList.Clear();
            UIThreadInvoker _uiThreadInvoker = new UIThreadInvoker();
            var taskBarRoot = _uiThreadInvoker.Run(automation =>
                automation.GetDesktop().FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd")))
            ).Result;
            var wxNotifyList = _uiThreadInvoker.Run(automation =>
                Retry.WhileNull(() => taskBarRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                    .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar))))
                    .FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME).And(cf.ByControlType(ControlType.Button))),
                    timeout: TimeSpan.FromSeconds(10))
            ).Result;
            if (wxNotifyList.Success)
            {
                foreach (var wxNotify in wxNotifyList.Result)
                {
                    DrawHightlightHelper.DrawHightlight(wxNotify, _uiThreadInvoker);
                    _uiThreadInvoker.Run(automation => wxNotify.AsButton().Invoke()).Wait();
                    var topWindowProcessId = Retry.WhileException(() => WinApi.GetTopWindowProcessIdByClassName("WeChatMainWndForPC"), timeout: TimeSpan.FromSeconds(10));
                    var wxInstances = _uiThreadInvoker.Run(automation =>
                        automation.GetDesktop().FindFirstChild(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                                .And(cf.ByClassName("WeChatMainWndForPC")
                                .And(cf.ByControlType(ControlType.Window))
                                .And(cf.ByProcessId(topWindowProcessId.Result)))).AsWindow()
                    ).Result;
                    DrawHightlightHelper.DrawHightlight(wxInstances, _uiThreadInvoker);
                    WeChatNotifyIcon wxNotifyIcon = new WeChatNotifyIcon(wxNotify.AsButton());
                    WeChatMainWindow wxWindow = new WeChatMainWindow(wxInstances, wxNotifyIcon);

                    var client = new WeChatClient(wxNotifyIcon, wxWindow);
                    wxWindow.Client = client;
                    var NickNameButton = wxInstances.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]").AsButton();
                    _wxClientList.Add(NickNameButton.Name, client);
                }
                this._IsInit = true;
            }
            else
            {
                throw new Exception("微信客户端不存在，请检查微信是否打开");
            }
            if (_wxClientList.Count == 0)
            {
                throw new Exception("微信客户端不存在，请检查微信是否打开");
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            ClearAllEvent();
        }
    }
}
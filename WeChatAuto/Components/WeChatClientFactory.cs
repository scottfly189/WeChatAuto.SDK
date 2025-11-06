using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Utils;
using System;
using System.Linq;
using WeChatAuto.Utils;
using FlaUI.Core.Tools;
using WeChatAuto.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端工厂,封装的微信客户端工厂，支持多微信实例
    /// </summary>
    public class WeChatClientFactory : IDisposable
    {
        private bool _IsInit = false;
        private readonly AutoLogger<WeChatClientFactory> _logger;
        private IServiceProvider _serviceProvider;
        private readonly Dictionary<string, WeChatClient> _wxClientList = new Dictionary<string, WeChatClient>();
        private bool _disposed = false;
        /// <summary>
        /// 微信自动化框架构造函数
        /// </summary>
        public WeChatClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<AutoLogger<WeChatClientFactory>>();
            _logger.Trace("微信客户端工厂初始化完成");
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
        /// 获取微信客户端
        /// </summary>
        /// <param name="name">微信客户端名称</param>
        /// <returns>微信客户端<see cref="WeChatClient"/></returns>
        /// <exception cref="Exception"></exception>
        public WeChatClient GetWeChatClient(string name)
        {
            Init();
            if (_wxClientList.ContainsKey(name))
            {
                return _wxClientList[name];
            }
            _logger.Error($"微信客户端[{name}]不存在，请检查微信是否打开");
            throw new Exception($"微信客户端[{name}]不存在，请检查微信是否打开");
        }

        /// <summary>
        /// 获取微信客户端列表
        /// 微信客户端请参见<see cref="WeChatClient"/>
        /// </summary>
        /// <returns>微信客户端列表<see cref="Dictionary{string, WeChatClient}"/></returns>
        public Dictionary<string, WeChatClient> GetWxClientList()
        {
            Init();
            return _wxClientList;
        }

        /// <summary>
        /// 重新获取微信窗口
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void RefreshWxWindows()
        {
            if (_disposed)
            {
                return;
            }
            _wxClientList.Clear();
            _logger.Trace("开始重新获取微信窗口");
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
                    WeChatNotifyIcon wxNotifyIcon = new WeChatNotifyIcon(wxNotify.AsButton(), _serviceProvider);
                    WeChatMainWindow wxWindow = new WeChatMainWindow(wxInstances, wxNotifyIcon, this, _serviceProvider);

                    var client = new WeChatClient(wxNotifyIcon, wxWindow, _serviceProvider, WeAutomation.Config.EnableCheckAppRunning);
                    wxWindow.Client = client;
                    var NickNameButton = wxInstances.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]").AsButton();
                    _wxClientList.Add(NickNameButton.Name, client);
                    _logger.Trace($"微信客户端[{NickNameButton.Name}]获取完成,当前微信客户端数量:_{_wxClientList.Count}");
                }
                this._IsInit = true;
            }
            else
            {
                _logger.Error("微信客户端不存在，请检查微信是否打开");
                throw new Exception("微信客户端不存在，请检查微信是否打开");
            }
            if (_wxClientList.Count == 0)
            {
                _logger.Error("微信客户端不存在，请检查微信是否打开");
                throw new Exception("微信客户端不存在，请检查微信是否打开");
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~WeChatClientFactory()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (_wxClientList != null && _wxClientList.Count > 0)
            {
                foreach (var client in _wxClientList)
                {
                    client.Value?.Dispose();
                }
            }
            if (WeAutomation.Config.EnableMouseKeyboardSimulator && KMSimulatorService.DeviceData != IntPtr.Zero)
            {
                KMSimulatorService.CloseDevice();
            }
            _disposed = true;
        }
    }
}
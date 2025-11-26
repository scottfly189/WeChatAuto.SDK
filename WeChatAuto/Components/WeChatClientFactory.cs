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
using System.Threading;
using FlaUI.UIA3;
using OneOf.Types;
using WeAutoCommon.Models;

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
        private readonly WeChatRecordVideo _recordVideo;
        /// <summary>
        /// 微信自动化框架构造函数
        /// </summary>
        public WeChatClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<AutoLogger<WeChatClientFactory>>();
            _recordVideo = _serviceProvider.GetRequiredService<WeChatRecordVideo>();
            if (WeAutomation.Config.EnableRecordVideo)
            {
                var videoPath = _recordVideo.RecordVideo().GetAwaiter().GetResult();
                _logger.Trace($"开始录制视频,保存路径: {videoPath}");
            }
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
                _FetchAllWxWindows();
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
        private void _FetchAllWxWindows()
        {
            if (_disposed)
            {
                return;
            }
            _wxClientList.Clear();
            _logger.Trace("开始重新获取微信窗口");
            UIThreadInvoker _uiTempThreadInvoker = new UIThreadInvoker("RefreshWxWindows");
            try
            {
                _uiTempThreadInvoker.Run(automation =>
                {
                    _GetTaskBarRoot(automation)
                    .Bind(taskBarRoot => _GetToolBar(taskBarRoot))
                    .Bind(toolBar => _GetNotifyButtons(toolBar))
                    .Map(buttons => _ProcessNotifyButtons<AutomationElement[]>(automation, buttons));
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.Error($"获取微信窗口失败: {ex.Message}");
                throw new Exception($"获取微信窗口失败: {ex.Message}");
            }
            finally
            {
                _uiTempThreadInvoker?.Dispose();
            }
        }
        private Result _ProcessNotifyButtons<U>(UIA3Automation automation, AutomationElement[] buttons)
        {
            foreach (var wxNotifyButton in buttons)
            {
                _InitWechatAutomationFramework(automation, wxNotifyButton);
            }
            this._IsInit = true;
            _logger.Trace($"当前微信客户端数量: 共{_wxClientList.Count}个");
            return Result.Ok();
        }
        /// <summary>
        /// 初始化微信自动化整个框架
        /// </summary>
        /// <param name="automation"></param>
        /// <param name="wxNotifyButton"></param>
        private void _InitWechatAutomationFramework(UIA3Automation automation, AutomationElement wxNotifyButton)
        {
            DrawHightlightHelper.DrawHighlightExt(wxNotifyButton);
            wxNotifyButton.AsButton().Invoke();
            RandomWait.Wait(100, 800);
            var topWindowProcessId = _GetTopWindowProcessIdResult();
            var wxTempwindow = _GetTopWindow(topWindowProcessId.Result, automation);
            DrawHightlightHelper.DrawHighlightExt(wxTempwindow);
            WeChatMainWindow wxMainWindow = new WeChatMainWindow(this, _serviceProvider, topWindowProcessId.Result);
            WeChatNotifyIcon wxNotifyIcon = new WeChatNotifyIcon(wxNotifyButton.AsButton(), _serviceProvider, wxMainWindow);

            var client = new WeChatClient(wxNotifyIcon, wxMainWindow, _serviceProvider, WeAutomation.Config.EnableCheckAppRunning);
            wxMainWindow.Client = client;
            var NickNameButton = Retry.WhileNull(() => wxTempwindow.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]")?.AsButton(),
              timeout: TimeSpan.FromSeconds(5),
              interval: TimeSpan.FromMilliseconds(200)).Result;
            _wxClientList.Add(NickNameButton.Name, client);
        }
        /// <summary>
        /// 获取任务栏根元素
        /// </summary>
        /// <param name="automation"></param>
        /// <returns></returns>
        private Maybe<AutomationElement> _GetTaskBarRoot(UIA3Automation automation)
        {
            var result = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf =>
                          cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd"))),
                          timeout: TimeSpan.FromSeconds(5),
                          interval: TimeSpan.FromMilliseconds(200)).Result;
            return result.ToMaybe();
        }
        /// <summary>
        /// 获取工具栏元素
        /// </summary>
        /// <param name="taskBarRoot"></param>
        /// <returns></returns>
        private Maybe<AutomationElement> _GetToolBar(AutomationElement taskBarRoot)
        {
            var result = Retry.WhileNull(() => taskBarRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                          .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar)))),
                          timeout: TimeSpan.FromSeconds(5)).Result;
            return result.ToMaybe();
        }
        /// <summary>
        /// 获取通知按钮元素
        /// </summary>
        /// <param name="toolBarResult"></param>
        /// <returns></returns>
        private Maybe<AutomationElement[]> _GetNotifyButtons(AutomationElement toolBar)
        {
            RandomWait.Wait(100, 600);
            var result = Retry.WhileNull(() => toolBar.FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                          .And(cf.ByControlType(ControlType.Button))),
                          timeout: TimeSpan.FromSeconds(5),
                          interval: TimeSpan.FromMilliseconds(200)).Result;
            return result.ToMaybe();
        }
        /// <summary>
        /// 获取顶部窗口进程ID
        /// </summary>
        /// <returns></returns>
        private RetryResult<int> _GetTopWindowProcessIdResult() =>
          Retry.WhileException(() => WinApi.GetTopWindowProcessIdByClassName("WeChatMainWndForPC"),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
        /// <summary>
        /// 获取顶部窗口元素
        /// </summary>
        /// <param name="topWindowProcessId"></param>
        /// <param name="automation"></param>
        /// <returns></returns>
        private Window _GetTopWindow(int topWindowProcessId, UIA3Automation automation) =>
          Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByClassName("WeChatMainWndForPC")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(topWindowProcessId)))).AsWindow(),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200)).Result;

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
            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
            {
                KMSimulatorService.CloseDevice();
            }

            if (WeAutomation.Config.EnableRecordVideo)
            {
                _recordVideo?._VideoRecorder?.Stop();
                _recordVideo?._VideoRecorder?.Dispose();
            }
            if (_wxClientList != null && _wxClientList.Count > 0)
            {
                foreach (var client in _wxClientList)
                {
                    client.Value?.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
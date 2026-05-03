using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WeAutoCommon.Utils;
using System;
using System.Linq;
using WeChatAuto.Utils;
using FlaUI.Core.Tools;
using WeChatAuto.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using FlaUI.UIA3;
using WeAutoCommon.Models;
using WeChatAuto.Extentions;
using FlaUI.Core.Input;
using Microsoft.Win32;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端工厂,封装的微信客户端工厂，支持多微信实例
    /// </summary>
    public class WeChatClientFactory : IDisposable
    {
        private bool _IsInit = false;
        public static UIThreadInvoker ThreadInvoker;   //就是多微信的情况下，也是启用一个线程.
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
            ThreadInvoker = new UIThreadInvoker("wechatauto.sdk");
        }
        /// <summary>
        /// 微信客户端列表
        /// </summary>
        public Dictionary<string, WeChatClient> WeChatClientList
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
        public List<string> GetWeChatClientNames()
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
                _FetchAllWxClients();
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
        /// <returns>微信客户端列表</returns>
        public Dictionary<string, WeChatClient> GetWeChatClientList()
        {
            Init();
            return _wxClientList;
        }

        /// <summary>
        /// 获取微信客户端
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void _FetchAllWxClients()
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
                DragVisibleIfWechatHidden(_uiTempThreadInvoker);
                _uiTempThreadInvoker.Run(automation => _GetTaskBarRoot(automation)
                    .Bind(taskBarRoot => _GetToolBar(taskBarRoot))
                    .BindOrElse(toolBar => _GetNotifyButtons(toolBar), () => _GetNotifyButtonsVersion2(automation))
                    .Bind(buttons => _ProcessNotifyButtons(automation, buttons))
                ).GetAwaiter().GetResult();
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

        private void DragVisibleIfWechatHidden(UIThreadInvoker _uiTempThreadInvoker)
        {
            _uiTempThreadInvoker.Run(automation =>
            {
                //则点击倒三角按钮，如果存在，则拖动溢出区域到任务栏
                var desktop = automation.GetDesktop();
                var path = "/Pane/Pane/Button[@Name='通知 V 形']";
                var button = desktop.FindFirstByXPath(path)?.AsButton();
                if (button == null)
                    return;
                button.Click();
                RandomWait.Wait(100, 300);
                var root = button.GetParent().GetParent();
                var overflowAreaRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf =>
                cf.ByControlType(ControlType.Pane).And(cf.ByName("通知溢出"))), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                if (overflowAreaRetry.Success)
                {
                    var overflowArea = overflowAreaRetry.Result;
                    var list = new List<AutomationElement>();
                    var buttons = overflowArea.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).Where(u => u.Name == "微信");
                    list.AddRange(buttons);
                    buttons = overflowArea.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).Where(u => u.Name == "WeChat");
                    list.AddRange(buttons);
                    var statusBar = desktop.FindFirstByXPath("/Pane[@Name='任务栏']/Pane/Pane/ToolBar[@Name='用户提示通知区域']");
                    if (statusBar != null)
                    {
                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                var source = item.BoundingRectangle.Center();
                                var target = statusBar.BoundingRectangle.Center();
                                Mouse.MoveTo(source);
                                Mouse.Down(MouseButton.Left);
                                Mouse.MoveTo(target);
                                Mouse.Up(MouseButton.Left);
                            }
                        }
                    }

                }
            }).GetAwaiter().GetResult();
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
            if (result == null)
            {
                _logger.Error($"{nameof(WeChatClientFactory)} - {nameof(_GetTaskBarRoot)}:本系统的UI Tree可能不被支持，因为找不到任务栏");
            }
            return result.ToMaybe();
        }
        /// <summary>
        /// 获取工具栏元素
        /// </summary>
        /// <param name="taskBarRoot"></param>
        /// <returns></returns>
        private Maybe<AutomationElement> _GetToolBar(AutomationElement taskBarRoot)
        {
            //用户提示通知区域
            var result = Retry.WhileNull(() => taskBarRoot.FindFirstDescendant(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NOTIFY_ICON)
                          .And(cf.ByClassName("ToolbarWindow32").And(cf.ByControlType(ControlType.ToolBar)))),
                          timeout: TimeSpan.FromSeconds(5));
            //window操作系统不同，可能存在元素结构的不一样,要注意处理
            if (!result.Success)
            {
                _logger.Error($"{nameof(WeChatClientFactory)}-{nameof(_GetToolBar)}:本系统UI Tree不支持，获取不到工具栏元素");
            }
            return result.Success ? Maybe<AutomationElement>.Some(result.Result) : Maybe<AutomationElement>.None();
        }
        /// <summary>
        /// 获取通知按钮元素
        /// </summary>
        /// <param name="toolBar">工具栏元素<see cref="AutomationElement"/></param>
        /// <returns></returns>
        private Maybe<AutomationElement[]> _GetNotifyButtons(AutomationElement toolBar)
        {
            RandomWait.Wait(100, 600);
            //微信
            var result = Retry.WhileNull(() => toolBar.FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                          .And(cf.ByControlType(ControlType.Button))),
                          timeout: TimeSpan.FromSeconds(5),
                          interval: TimeSpan.FromMilliseconds(200)).Result;
            //处理第二种异常情况
            if (result == null || result.Length == 0)
            {
                WeChatConstant.WECHAT_SYSTEM_NAME = "WeChat";
                result = Retry.WhileNull(() => toolBar.FindAllChildren(cf => cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)
                          .And(cf.ByControlType(ControlType.Button))),
                          timeout: TimeSpan.FromSeconds(5),
                          interval: TimeSpan.FromMilliseconds(200)).Result;
            }
            if (result == null || result.Length == 0)
            {
                _logger.Error($"{nameof(WeChatClientFactory)} - {nameof(_GetNotifyButtons)}: 本系统的UI Tree可能不被支持,请联系作者予以支持");
                throw new Exception("本系统的UI Tree可能不被支持，请联系作者予以支持");
            }
            return result.ToMaybe();
        }

        /// <summary>
        /// 另外一个window版本获取状态栏通知按钮元素的版本
        /// 
        /// window11的不同版本，对于微信桌面版的通知按钮元素的获取方式不同，所以需要根据不同的版本获取不同的元素
        /// </summary>
        /// <param name="automation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Maybe<AutomationElement[]> _GetNotifyButtonsVersion2(UIA3Automation automation)
        {
            //第一次UI Tree异常情况获取
            (bool Success, AutomationElement[] elements) itemResult = __GetNotifyButtons_2(automation);
            if (itemResult.Success)
            {
                return Maybe<AutomationElement[]>.Some(itemResult.elements);
            }
            else
            {
                //对第三种UI Tree异常情况支持
                itemResult = __GetNotifyButtons_3(automation);
                if (itemResult.Success)
                {
                    return Maybe<AutomationElement[]>.Some(itemResult.elements);
                }
            }

            throw new Exception($"{nameof(WeChatClientFactory)}-{nameof(_GetNotifyButtonsVersion2)}:获取任务栏微信按钮失败");
        }

        //refactor: 添加对2019 server 系统的微信支持，ver=10.0.17763.973
        private (bool Success, AutomationElement[] elements) __GetNotifyButtons_3(UIA3Automation automation)
        {
            var deskTop = automation.GetDesktop();
            var root = deskTop.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar).And(cf.ByName("用户提示通知区域")));
            if (root == null)
                return (false, null);
            WeChatConstant.WECHAT_SYSTEM_NAME = "WeChat";
            var elements = root.FindAllChildren(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(WeChatConstant.WECHAT_SYSTEM_NAME)));
            if (elements != null && elements.Length > 0)
            {
                return (true, elements);
            }
            return (false, null);
        }

        private (bool Success, AutomationElement[] elements) __GetNotifyButtons_2(UIA3Automation automation)
        {
            var taskBarRootRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf =>
                cf.ByName(WeChatConstant.WECHAT_SYSTEM_TASKBAR).And(cf.ByClassName("Shell_TrayWnd"))),
                timeout: TimeSpan.FromSeconds(5),
                interval: TimeSpan.FromMilliseconds(200));
            if (taskBarRootRetry.Success)
            {
                var taskBarRoot = taskBarRootRetry.Result;
                var xPath = "/Pane[@ClassName='Windows.UI.Input.InputSite.WindowClass']/Button[@ClassName='SystemTray.NormalButton'][@Name='微信']";
                var result = Retry.WhileNull(() => taskBarRoot.FindAllByXPath(xPath),
                          timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                // return result.Success ? Maybe<AutomationElement[]>.Some(result.Result) : throw new Exception("获取通知按钮元素失败");
                if (result.Success)
                {
                    return (true, result.Result);
                }
            }
            return (false, null);
        }
        private Maybe<bool> _ProcessNotifyButtons(UIA3Automation automation, AutomationElement[] buttons)
        {
            foreach (var wxNotifyButton in buttons)
            {
                _InitWechatAutomationFramework(automation, wxNotifyButton);
            }
            this._IsInit = true;
            _logger.Trace($"当前微信客户端数量: 共{_wxClientList.Count}个");
            return _IsInit.ToMaybe();
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
            var topWindowProcessId = _GetTopWindowProcessIdResult();  //当前微信的processid
            var wxTempwindow = _GetTopWindow(topWindowProcessId.Result, automation);  //当前微信的automation window.
            DrawHightlightHelper.DrawHighlightExt(wxTempwindow);

            var client = new WeChatClient(topWindowProcessId.Result,_serviceProvider,this,wxTempwindow,ThreadInvoker);
            var NickNameButton = Retry.WhileNull(() => wxTempwindow.FindFirstByXPath("/Pane/Pane/ToolBar/Button[1]")?.AsButton(),
              timeout: TimeSpan.FromSeconds(5),
              interval: TimeSpan.FromMilliseconds(200)).Result;
            _wxClientList.Add(NickNameButton.Name, client);
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
            _disposed = true;
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
            ThreadInvoker.Dispose();   //将微信自动化的线程释放.
        }
    }
}
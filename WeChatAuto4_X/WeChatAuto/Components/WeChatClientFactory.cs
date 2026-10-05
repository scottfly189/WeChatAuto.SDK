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
using WeChatAuto.Exceptions;
using FlaUI.Core.Input;
using System.Drawing;
using WeChatAuto.Models;
using System.IO;
using WeAutoCommon.Extentions;
using System.Windows.Controls;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端工厂,封装的微信客户端工厂，支持多微信实例
    /// </summary>
    public class WeChatClientFactory : IDisposable
    {
        // TODO: 每次微信版本变化应该检查这里是否需要修改.
        private const string CURRENT_WEIXIN_CLASSNAME = "Qt51514QWindowIcon";
        private bool _IsInit = false;
        private readonly AutoLogger<WeChatClientFactory> _logger;
        private IServiceProvider _serviceProvider;
        private readonly Dictionary<string, WeChatClient> _wxClientList = new Dictionary<string, WeChatClient>();
        private bool _disposed = false;
        private readonly WeChatRecordVideo _recordVideo;
        private SemaphoreSlim monitorEvent = new SemaphoreSlim(1, 1);    //所有自动监听都应该受这个约束

        public readonly static string MainActionThreadName = "wechatauto.sdk";
        public static UIThreadInvoker MainActionThreadInvoker;   //就是多微信的情况下，也是启用一个线程,因为这样虽然慢，但能实现更强的功能.

        /// <summary>
        /// 微信自动化客户端工厂
        /// </summary>
        /// <remarks>
        /// 注意：不应该自行调用，而是通过依赖注入获取实例。
        ///
        /// 示例：
        /// <![CDATA[
        /// var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
        /// ]]>
        /// </remarks>
        public WeChatClientFactory(IServiceProvider serviceProvider)
        {
            MainActionThreadInvoker = new UIThreadInvoker(WeChatClientFactory.MainActionThreadName);
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<AutoLogger<WeChatClientFactory>>();
            _recordVideo = _serviceProvider.GetRequiredService<WeChatRecordVideo>();
            if (WeAutomation.Config.EnableRecordVideo)
            {
                var videoPath = _recordVideo.RecordVideo().GetAwaiter().GetResult();
                _logger.Trace($"开始录制视频,保存路径: {videoPath}");
            }
            _logger.Trace("微信客户端工厂初始化完成");
            InitOCRService();
        }

        /// <summary>
        /// 初始化OCR引擎
        /// </summary>
        private void InitOCRService()
        {
            OCRService oCRService = _serviceProvider.GetRequiredService<OCRService>();
            oCRService.InitOCREngin(WeAutomation.Config);
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
            throw new WechatClientNotExistException($"微信客户端[{name}]不存在，请检查微信是否打开");
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
            try
            {
                MainActionThreadInvoker.Run(automation =>
                {
                    _GetAllWechatProcessId(automation)
                    .Bind(processIds => _InitWechatFramework(automation, processIds));
                }).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.Error($"获取微信窗口失败: {ex.Message}");
                throw new Exception($"获取微信窗口失败: {ex.Message}");
            }
        }

        private Maybe<List<int>> _GetAllWechatProcessId(UIA3Automation automation)
        {
            var result = new List<int>();
            var desktop = automation.GetDesktop();
            var wechatWins = desktop.FindAllChildren(u => u.ByClassName("mmui::MainWindow").And(u.ByControlType(ControlType.Window)).And(u.ByFrameworkType(FlaUI.Core.FrameworkType.Qt)));
            if (wechatWins.Count() == 0)
            {
                _logger.Error("错误：检查到系统中未打开微信窗口 或者 微信窗口处于隐藏状态，请先打开微信，并且保持微信窗口处于显示状态！");
                throw new WechatNotOpenedException("错误：检查到系统中未打开微信窗口 或者 微信窗口处于隐藏状态，请先打开微信，并且保持微信窗口处于显示状态！");
            }
            foreach (var win in wechatWins)
            {
                result.Add(win.Properties.ProcessId);
            }

            return result.ToMaybe();
        }


        private Maybe<bool> _InitWechatFramework(UIA3Automation automation, List<int> processList)
        {
            try
            {
                var index = 0;
                var wechatList = new List<string>();
                Window beforeWin = null;
                processList = processList.Order().ToList();
                __ForceOpenUITree__(processList);
                foreach (var process in processList)
                {
                    index++;
                    _InitWechatAutomationFrameworkWithProcessId(automation, process, index, ref beforeWin);
                }
                this._IsInit = true;
                _logger.Trace("************************************************************");
                _logger.Trace("*                POWER BY WECHATAUTO.SDK                   *");
                _logger.Trace($"* 微信客户端: 共 {_wxClientList.Count} 个" + "                                       *");
                _logger.Trace($"* 客户端列表: [{string.Join(", ", _wxClientList.Keys)}]");
                _logger.Trace("************************************************************");
                return _IsInit.ToMaybe();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"获取UI Tree时出错，错误原因:{ex.ToString()}");
                throw;
            }
        }

        /// <summary>
        /// 如果微信没有公开UI Tree,则强制打开UI Tree
        /// </summary>
        private void __ForceOpenUITree__(List<int> processList)
        {
            
        }

        private void _InitWechatAutomationFrameworkWithProcessId(UIA3Automation automation, int processId, int index, ref Window beforeWin)
        {
            //首先置顶微信
            WinApi.ActivateProcess(processId);
            //关闭输入法,以方便Keyboard.Type函数正确
            WindowsInputHelper.ForceEnglishInput((uint)processId);
            RandomWait.Wait(100, 800);
            (OwerInfo info, Window window) result = __GetCurrentWxNickName(processId, automation);
            result.window.Focus();
            var client = new WeChatClient(processId, _serviceProvider, this, result.window, MainActionThreadInvoker, result.info, index, monitorEvent);
            _wxClientList.Add(result.info.NickName, client);
            __MoveAndResize__(automation, processId, result.window, client, ref beforeWin, index);
        }
        /// <summary>
        /// 重新排列微信窗口
        /// </summary>
        private void __MoveAndResize__(UIA3Automation automation, int processId, Window currentWin, WeChatClient client, ref Window beforeWin, int index)
        {
            if (currentWin.Patterns.Window.Pattern.WindowVisualState == WindowVisualState.Maximized)
            {
                currentWin.Patterns.Window.Pattern.SetWindowVisualState(WindowVisualState.Normal);
            }
            //先改变尺寸.
            var ratio = DpiHelper.GetScaleForWindow(currentWin.Properties.NativeWindowHandle);
            var width = (int)(728 * ratio);
            var height = (int)(784 * ratio);
            Win32WindowHelper.ResizeWindow(currentWin.Properties.NativeWindowHandle, width, height);
            RandomWait.Wait(800, 1500);
            if (index < 3)
            {
                var workArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var point = Point.Empty;

                if (beforeWin == null)
                {
                    point.X = workArea.Width - currentWin.BoundingRectangle.Width - 30;
                    point.Y = (int)((workArea.Height - currentWin.BoundingRectangle.Height) / 2);
                }
                else
                {
                    point.X = beforeWin.BoundingRectangle.X - beforeWin.BoundingRectangle.Width - 20;
                    point.Y = beforeWin.BoundingRectangle.Y;
                }
                currentWin.Move(point.X, point.Y);
                RandomWait.Wait(800, 1500);
            }
            beforeWin = currentWin;
        }


        //得到最新窗口的nickName等信息.
        private (OwerInfo info, Window window) __GetCurrentWxNickName(int processID, UIA3Automation automation)
        {
            try
            {
                var desktop = automation.GetDesktop();
                var windowRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("mmui::MainWindow")).And(cf.ByProcessId(processID))),
                                     timeout: TimeSpan.FromSeconds(5),
                                     interval: TimeSpan.FromMilliseconds(200));
                if (windowRetry.Success)
                {
                    var wxTempwindow = windowRetry.Result.AsWindow();
                    RandomWait.Wait(300, 600);
                    OwerInfo info = new OwerInfo();
                    wxTempwindow.Focus();
                    RandomWait.Wait(300, 600);
                    var toolBar = wxTempwindow.FindFirstDescendant(cf => cf.ByAutomationId("MainView.main_tabbar").And(cf.ByName("导航")).And(cf.ByControlType(ControlType.ToolBar)));
                    var button = toolBar.FindFirstChild(cf => cf.ByName("微信").And(cf.ByClassName("mmui::XTabBarItem")).And(cf.ByControlType(ControlType.Button)));
                    var point1 = button.GetClickablePoint();
                    var topInterval = (int)(WeAutomation.Config.AvatorToWeixinButtonOffsetY * DpiHelper.GetScaleForWindow(wxTempwindow.Properties.NativeWindowHandle));
                    var point2 = new Point(point1.X, point1.Y - topInterval);
                    Mouse.Position = point2;
                    Mouse.LeftClick();
                    RandomWait.Wait(300, 800);
                    var windowResult = Retry.WhileNull<AutomationElement>(() => desktop.FindFirstChild(cf => cf.ByName("Weixin").
                        And(cf.ByProcessId(wxTempwindow.Properties.ProcessId))),
                        timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                    if (windowResult.Success)
                    {
                        var window = windowResult.Result.AsWindow();
                        window.DrawHighlightExt();
                        button = window.FindFirstDescendant(cf => cf.ByAutomationId("head_image_v_view.head_view_").And(cf.ByControlType(ControlType.Button))
                            .And(cf.ByProcessId(wxTempwindow.Properties.ProcessId)));
                        button?.DrawHighlightExt();
                        if (button != null)
                        {
                            var wxid = button.GetParent().GetParent().FindFirstDescendant(cf => cf.ByName("微信号：").And(cf.ByControlType(ControlType.Text)));
                            if (wxid != null)
                            {
                                wxid.DrawHighlightExt();
                                wxid = wxid.GetSibling(1);
                                if (wxid != null)
                                {
                                    info.WxId = wxid.Name.Trim();
                                    info.NickName = button.Name.Trim();
                                    var avatorPath = Path.Combine(AppContext.BaseDirectory, "Avator");
                                    if (!Directory.Exists(avatorPath))
                                    {
                                        Directory.CreateDirectory(avatorPath);
                                    }
                                    avatorPath = Path.Combine(avatorPath, $"{info.WxId}.png");
                                    button.CaptureToFile(avatorPath);
                                    info.AvatorPath = avatorPath;
                                }
                            }
                            RandomWait.Wait(50, 600);
                        }
                    }
                    return (info, wxTempwindow);
                }

                throw new Exception("没有获取到窗口");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw new WechatNotSupportUITreeException($"错误: 腾迅没有公开 processID={processID} 的微信的UI Tree,请参照链接解决：https://github.com/scottfly189/WeChatAuto.SDK/issues/3");
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
            MainActionThreadInvoker.Dispose();   //将微信自动化的线程释放.
        }
    }
}
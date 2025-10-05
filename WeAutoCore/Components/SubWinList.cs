using System.Collections.Generic;
using System.Collections.Concurrent;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core;
using System.Linq;
using FlaUI.Core.Tools;
using System;
using WxAutoCommon.Utils;
using System.Threading;
using System.Threading.Tasks;
using WxAutoCommon.Configs;
using WxAutoCommon.Enums;
using WxAutoCommon.Models;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口列表
    /// </summary>
    public class SubWinList
    {
        private ConcurrentBag<string> _MonitorSubWinNames = new ConcurrentBag<string>();
        private CancellationTokenSource _MonitorSubWinCancellationTokenSource = new CancellationTokenSource();
        private TaskCompletionSource<bool> _MonitorSubWinTaskCompletionSource = new TaskCompletionSource<bool>();
        private Thread _MonitorSubWinThread;
        private WeChatMainWindow _MainWxWindow;
        private Window _MainFlaUIWindow;
        private UIThreadInvoker _uiThreadInvoker;
        /// <summary>
        /// 子窗口列表构造函数
        /// </summary>
        /// <param name="window">主窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口对象<see cref="WeChatMainWindow"/></param>
        public SubWinList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
            _MainWxWindow = wxWindow;
            _MainFlaUIWindow = window;
            _InitMonitorSubWinThread();
            _MonitorSubWinTaskCompletionSource.Task.Wait();
        }
        /// <summary>
        /// 初始化监听子窗口线程
        /// </summary>
        private void _InitMonitorSubWinThread()
        {
            _MonitorSubWinThread = new Thread(async () =>
            {
                _MonitorSubWinTaskCompletionSource.SetResult(true);
                while (!_MonitorSubWinCancellationTokenSource.IsCancellationRequested)
                {
                    var subWinNames = GetAllSubWinNames();
                    if (!_MonitorSubWinNames.IsEmpty)
                    {
                        var notExistSubWinNames = _MonitorSubWinNames.Except(subWinNames);
                        if (notExistSubWinNames.Any())
                        {
                            foreach (var notExistSubWinName in notExistSubWinNames)
                            {
                                await this.CheckSubWinExistAndOpen(notExistSubWinName);
                            }
                        }
                    }
                    await Task.Delay(WeChatConfig.MonitorSubWinInterval * 1000);
                }
            });
            _MonitorSubWinThread.IsBackground = true;
            _MonitorSubWinThread.Priority = ThreadPriority.Lowest;
            _MonitorSubWinThread.Start();
        }
        /// <summary>
        /// 得到所有子窗口名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllSubWinNames()
        {
            var desktop = _uiThreadInvoker.Run(automation => automation.GetDesktop()).Result;
            var subWinRetry = _uiThreadInvoker.Run(automation =>
            {
                return Retry.WhileNull(() => desktop.FindAllChildren(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId)))),
                        timeout: TimeSpan.FromSeconds(10),
                        interval: TimeSpan.FromMilliseconds(200));
            }).Result;
            if (subWinRetry.Success)
            {
                Thread.Sleep(1000);
                return subWinRetry.Result.ToList().Select(subWin => subWin.Name).ToList();
            }
            return new List<string>();
        }
        /// <summary>
        /// 判断子窗口是否打开
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns></returns>
        public bool CheckSubWinIsOpen(string name)
        {
            var subWin = _uiThreadInvoker.Run(automation =>
             {
                 var desktop = automation.GetDesktop();
                 return Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                         .And(cf.ByControlType(ControlType.Window)
                         .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId))
                         .And(cf.ByName(name)))),
                         timeout: TimeSpan.FromSeconds(5),
                         interval: TimeSpan.FromMilliseconds(200));
             }).Result;

            return subWin.Success;
        }

        /// <summary>
        /// 判断子窗口是否存在，如果未存在，则打开
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task CheckSubWinExistAndOpen(string name)
        {
            if (!CheckSubWinIsOpen(name))
            {
                await _MainWxWindow.ActionQueueChannel.PutAndWaitAsync(new ChatActionMessage()
                {
                    Type = ActionType.打开子窗口,
                    ToUser = name,
                    IsOpenSubWin = true
                });
            }
        }

        /// <summary>
        /// 根据名称获取子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns>子窗口对象<see cref="SubWin"/></returns>
        public SubWin GetSubWin(string name)
        {
            var subWin = _uiThreadInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                return Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId))
                        .And(cf.ByName(name)))),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
            }).Result;
            if (subWin.Success)
            {
                return new SubWin(subWin.Result.AsWindow(), _MainWxWindow, _uiThreadInvoker, name);
            }
            return null;
        }

        /// <summary>
        /// 关闭所有子窗口
        /// </summary>
        public void CloseAllSubWins()
        {
            var subWinNames = GetAllSubWinNames();
            foreach (var subWinName in subWinNames)
            {
                GetSubWin(subWinName).Close();
            }
        }
        /// <summary>
        /// 关闭指定子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        public void CloseSubWin(string name)
        {
            GetSubWin(name).Close();
        }
        /// <summary>
        /// 注册监听子窗口
        /// </summary>
        /// <param name="name"></param>
        public void RegisterMonitorSubWin(string name)
        {
            if (!_MonitorSubWinNames.Contains(name))
            {
                _MonitorSubWinNames.Add(name);
            }
        }

        /// <summary>
        /// 取消监听子窗口
        /// </summary>
        /// <param name="name"></param>
        public void UnregisterMonitorSubWin(string name)
        {
            _MonitorSubWinNames = new ConcurrentBag<string>(_MonitorSubWinNames.Where(item => item != name));
        }
    }
}
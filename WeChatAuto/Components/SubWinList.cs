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
using System.Diagnostics;
using WeChatAuto.Services;
using WeChatAuto.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 子窗口列表
    /// </summary>
    public class SubWinList
    {
        private ConcurrentBag<string> _MonitorSubWinNames = new ConcurrentBag<string>();     //守护子窗口名称列表
        private Dictionary<string, SubWin> _SubWins = new Dictionary<string, SubWin>();      //所有子窗口列表,事实上手动关闭子窗口，这里并不会变化.
        private Dictionary<string, Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider>> _SubWinMessageListeners = new Dictionary<string, Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider>>();  //所有子窗口消息监听器列表
        private CancellationTokenSource _MonitorSubWinCancellationTokenSource = new CancellationTokenSource();
        private TaskCompletionSource<bool> _MonitorSubWinTaskCompletionSource = new TaskCompletionSource<bool>();
        private Thread _MonitorSubWinThread;
        private WeChatMainWindow _MainWxWindow;   //主窗口对象
        private Window _MainFlaUIWindow;   //主窗口FlaUI的window
        private UIThreadInvoker _uiThreadInvoker;
        private AutoLogger<SubWinList> _logger;
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 子窗口列表构造函数
        /// </summary>
        /// <param name="window">主窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口对象<see cref="WeChatMainWindow"/></param>
        /// <param name="uiThreadInvoker">UI线程执行器</param>
        /// <param name="serviceProvider">服务提供者</param>
        public SubWinList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<SubWinList>>();
            _uiThreadInvoker = uiThreadInvoker;
            _MainWxWindow = wxWindow;
            _MainFlaUIWindow = window;
            _serviceProvider = serviceProvider;
            _InitMonitorSubWinThread();
            _MonitorSubWinTaskCompletionSource.Task.GetAwaiter().GetResult();
        }
        /// <summary>
        /// 初始化监听子窗口线程
        /// </summary>
        private void _InitMonitorSubWinThread()
        {
            _MonitorSubWinThread = new Thread(async () =>
            {
                _MonitorSubWinTaskCompletionSource.SetResult(true);
                while (!_MonitorSubWinCancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        if (_MainWxWindow != null && _MainWxWindow.Client != null && !_MainWxWindow.Client.AppRunning)
                            return;
                        var subWinNames = GetAllSubWinNames();   //获取所有打开的子窗口名称
                        if (!_MonitorSubWinNames.IsEmpty)
                        {
                            var notExistSubWinNames = _MonitorSubWinNames.Except(subWinNames);
                            if (notExistSubWinNames.Any())
                            {
                                foreach (var notExistSubWinName in notExistSubWinNames)
                                {
                                    //取消监听子窗口
                                    var subWin = this.GetSubWin(notExistSubWinName);
                                    subWin.Dispose();
                                    _SubWins.Remove(notExistSubWinName);
                                }
                                foreach (var notExistSubWinName in notExistSubWinNames)
                                {
                                    //重新打开前将监听子窗口取消
                                    //如果子窗口不存在，则打开
                                    await this.CheckSubWinExistAndOpen(notExistSubWinName);
                                    var subWin = this.GetSubWin(notExistSubWinName);
                                    if (_SubWinMessageListeners.ContainsKey(notExistSubWinName))
                                    {
                                        subWin.AddMessageListener(_SubWinMessageListeners[notExistSubWinName]);
                                    }
                                }
                            }
                        }
                        await Task.Delay(WeAutomation.Config.MonitorSubWinInterval * 1000);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("线程发生错误:" + ex.ToString());
                        _logger.Error(ex.StackTrace);
                        throw;
                    }
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
            var subWinRetry = _uiThreadInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                var list = Retry.WhileNull(() => desktop.FindAllChildren(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId)))),
                        timeout: TimeSpan.FromSeconds(10),
                        interval: TimeSpan.FromMilliseconds(200));

                return list.Result.ToList().Select(subWin => subWin.Name).ToList();
            }).GetAwaiter().GetResult();
            if (subWinRetry != null)
            {
                Thread.Sleep(1000);
                return subWinRetry;
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
             }).GetAwaiter().GetResult();

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
            if (_SubWins.ContainsKey(name))
            {
                return _SubWins[name];
            }
            var subWin = _uiThreadInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                return Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId))
                        .And(cf.ByName(name)))),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
            }).GetAwaiter().GetResult();
            if (subWin.Success)
            {
                var subWinObject = new SubWin(subWin.Result.AsWindow(), _MainWxWindow, _uiThreadInvoker, name, this, _serviceProvider);
                _SubWins.Add(name, subWinObject);
                return subWinObject;
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
                foreach (var subWin in _SubWins)
                {
                    subWin.Value.Dispose();
                }
                _SubWins.Clear();
            }
        }
        /// <summary>
        /// 关闭指定子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        public void CloseSubWin(string name)
        {
            GetSubWin(name).Close();
            _SubWins[name].Dispose();
            _SubWins.Remove(name);
        }
        /// <summary>
        /// 注册守护子窗口监听
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
        /// 取消守护子窗口监听
        /// </summary>
        /// <param name="name"></param>
        public void UnregisterMonitorSubWin(string name)
        {
            _MonitorSubWinNames = new ConcurrentBag<string>(_MonitorSubWinNames.Where(item => item != name));
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack">回调函数</param>
        /// <param name="nickName">好友名称</param>
        public async Task AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider> callBack, string nickName)
        {
            await this.CheckSubWinExistAndOpen(nickName);
            await Task.Delay(500);
            var subWin = this.GetSubWin(nickName);
            subWin.AddMessageListener(callBack);
            _SubWinMessageListeners.Add(nickName, callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        /// <param name="nickName">好友名称</param>
        public void StopMessageListener(string nickName)
        {
            _SubWinMessageListeners.Remove(nickName);
            GetSubWin(nickName).StopListener();
        }
    }
}
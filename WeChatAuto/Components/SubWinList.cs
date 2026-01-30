using System.Collections.Generic;
using System.Collections.Concurrent;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core;
using System.Linq;
using FlaUI.Core.Tools;
using System;
using WeAutoCommon.Utils;
using System.Threading;
using System.Threading.Tasks;
using WeAutoCommon.Configs;
using WeAutoCommon.Enums;
using WeAutoCommon.Models;
using System.Diagnostics;
using WeChatAuto.Services;
using WeChatAuto.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using WeChatAuto.Models;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 子窗口列表
    /// </summary>
    public class SubWinList : IDisposable
    {
        private volatile bool _disposed = false;
        private ConcurrentBag<string> _MonitorSubWinNames = new ConcurrentBag<string>();     //守护子窗口名称列表
        private Dictionary<string, SubWin> _SubWinsCache = new Dictionary<string, SubWin>();      //所有子窗口列表缓存,事实上手动关闭子窗口，这里并不会变化.
        private Dictionary<string, Action<MessageContext>> _SubWinMessageListeners
            = new Dictionary<string, Action<MessageContext>>();  //所有子窗口消息监听器列表
        private CancellationTokenSource _MonitorSubWinCancellationTokenSource = new CancellationTokenSource();
        private TaskCompletionSource<bool> _MonitorSubWinTaskCompletionSource = new TaskCompletionSource<bool>();
        private Thread _MonitorSubWinThread;
        private WeChatMainWindow _MainWxWindow;   //主窗口对象
        private Window _MainWindow;   //主窗口FlaUI的window
        private UIThreadInvoker _uiMainThreadInvoker;
        private AutoLogger<SubWinList> _logger;
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 子窗口列表构造函数
        /// </summary>
        /// <param name="mainWindow">主窗口FlaUI的window</param>
        /// <param name="mainWeChatMainWindow">主窗口对象<see cref="WeChatMainWindow"/></param>
        /// <param name="uiMainThreadInvoker">主窗口UI线程执行器<see cref="UIThreadInvoker"/></param>
        /// <param name="serviceProvider">服务提供者<see cref="IServiceProvider"/></param>
        public SubWinList(Window mainWindow, WeChatMainWindow mainWeChatMainWindow, UIThreadInvoker uiMainThreadInvoker,
          IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<SubWinList>>();
            _uiMainThreadInvoker = uiMainThreadInvoker;
            _MainWxWindow = mainWeChatMainWindow;
            _MainWindow = mainWindow;
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
                while (!_disposed && !_MonitorSubWinCancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        if (_disposed)
                            break;

                        if (_MainWxWindow != null && _MainWxWindow.Client != null && !_MainWxWindow.Client.AppRunning)
                            return;
                        if (!_MonitorSubWinNames.IsEmpty)
                        {
                            var allOpenedSubWinNames = GetAllOpenedSubWinNames();   //获取所有打开的子窗口名称，事实上还是推送到主窗口的UI线程中获取，所以并没有提高效率。
                            await _MonitorSubWinCore(allOpenedSubWinNames);
                        }
                        await Task.Delay(WeAutomation.Config.MonitorSubWinInterval * 1000, _MonitorSubWinCancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.Info($"监听子窗口线程[{_MonitorSubWinThread.Name}]已停止，正常取消,不做处理");
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"监听子窗口线程[{_MonitorSubWinThread.Name}]发生错误,但是不退出,异常信息:" + ex.ToString());
                        _logger.Error(ex.StackTrace);
                    }
                }
            });
            _MonitorSubWinThread.Name = "MonitorSubWinThread";
            _MonitorSubWinThread.IsBackground = true;
            _MonitorSubWinThread.Priority = ThreadPriority.Lowest;
            _MonitorSubWinThread.Start();
        }
        /// <summary>
        /// 监听子窗口核心逻辑
        /// </summary>
        /// <param name="allOpenedSubWinNames">所有子窗口名称列表</param>
        private async Task _MonitorSubWinCore(List<string> allOpenedSubWinNames)
        {
            var willOpenSubWin = _MonitorSubWinNames.Except(allOpenedSubWinNames);  //去掉已经打开的子窗口,剩下的就是要打开的子窗口
            if (willOpenSubWin.Any())
            {
                foreach (var subWinName in willOpenSubWin)
                {
                    //取消监听子窗口,并从列表中移除
                    var subWin = this.GetSubWin(subWinName);
                    subWin.Dispose();
                    _SubWinsCache.Remove(subWinName);
                }
                foreach (var notExistSubWinName in willOpenSubWin)
                {
                    //重新打开前将监听子窗口
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

        /// <summary>
        /// 得到所有子窗口名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllOpenedSubWinNames()
        {
            var subWinRetry = _uiMainThreadInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                var list = Retry.WhileNull(() => desktop.FindAllChildren(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainWindow.Properties.ProcessId)))),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
                if (list.Success && list.Result != null)
                {
                    return list.Result.ToList().Select(subWin => subWin.Name).ToList();
                }
                return new List<string>();
            }).GetAwaiter().GetResult();
            return subWinRetry;
        }
        /// <summary>
        /// 判断子窗口是否打开
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns></returns>
        public bool CheckSubWinIsOpen(string name)
        {
            var subWin = _uiMainThreadInvoker.Run(automation =>
             {
                 var desktop = automation.GetDesktop();
                 var result = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                         .And(cf.ByControlType(ControlType.Window)
                         .And(cf.ByProcessId(_MainWindow.Properties.ProcessId))
                         .And(cf.ByName(name)))),
                         timeout: TimeSpan.FromSeconds(5),
                         interval: TimeSpan.FromMilliseconds(200));
                 if (result.Success)
                 {
                     if (result.Result.AsWindow() != null)
                     {
                         result.Result.AsWindow().Focus();
                     }
                 }
                 return result;
             }).GetAwaiter().GetResult();

            return subWin.Success;
        }

        /// <summary>
        /// 判断子窗口是否存在，如果未存在，则打开
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CheckSubWinExistAndOpen(string name)
        {
            if (!CheckSubWinIsOpen(name))
            {
                return await _MainWxWindow.OpenSubWinDispatch(new ChatActionMessage()
                {
                    Type = ActionType.打开子窗口,
                    ToUser = name,
                    IsOpenSubWin = true
                });
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据名称获取子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns>子窗口对象<see cref="SubWin"/></returns>
        public SubWin GetSubWin(string name)
        {
            if (_SubWinsCache.ContainsKey(name))
            {
                if (!CheckSubWinIsOpen(name))
                {
                    _SubWinsCache.Remove(name);
                    _MainWxWindow.OpenSubWinDispatch(new ChatActionMessage()
                    {
                        Type = ActionType.打开子窗口,
                        ToUser = name,
                        IsOpenSubWin = true
                    }).GetAwaiter().GetResult();
                    return GetSubWin(name);
                }
                return _SubWinsCache[name];
            }
            var subWin = _uiMainThreadInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                return Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainWindow.Properties.ProcessId))
                        .And(cf.ByName(name)))),
                        timeout: TimeSpan.FromSeconds(5),
                        interval: TimeSpan.FromMilliseconds(200));
            }).GetAwaiter().GetResult();
            if (subWin.Success)
            {
                var subWinObject = new SubWin(subWin.Result.AsWindow(), _MainWxWindow, _uiMainThreadInvoker, name, this, _serviceProvider);
                _SubWinsCache.Add(name, subWinObject);
                return subWinObject;
            }
            return null;
        }

        /// <summary>
        /// 关闭所有子窗口
        /// </summary>
        public void CloseAllSubWins()
        {
            var subWinNames = GetAllOpenedSubWinNames();
            foreach (var subWinName in subWinNames)
            {
                GetSubWin(subWinName).Close();
            }
            foreach (var subWin in _SubWinsCache)
            {
                subWin.Value.Dispose();
            }
            _SubWinsCache.Clear();
        }
        /// <summary>
        /// 关闭指定子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        public void CloseSubWin(string name)
        {
            GetSubWin(name).Close();
            _SubWinsCache[name].Dispose();
            _SubWinsCache.Remove(name);
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
        /// <param name="who">好友或者群聊名称</param>
        /// <param name="firstMessageAction">适用于当开始消息监听时,发送一些信息（如：发送文字、表情、文件等）给好友的场景,参数：发送者<see cref="Sender"/></param>
        public async Task AddMessageListener(Action<MessageContext> callBack, string who, Action<Sender> firstMessageAction = null)
        {
            await this.CheckSubWinExistAndOpen(who);
            await Task.Delay(500);
            var subWin = this.GetSubWin(who);
            subWin.AddMessageListener(callBack, firstMessageAction);
            _SubWinMessageListeners.Add(who, callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        /// <param name="who">好友昵称</param>
        public void StopMessageListener(string who)
        {
            UnregisterMonitorSubWin(who); //取消守护子窗口监听
            _logger.Info($"停止守护子窗口监听: {who}");
            GetSubWin(who).StopListener(); //停止消息监听
            if (_SubWinsCache.ContainsKey(who))
            {
                _SubWinsCache.Remove(who);
            }
            _logger.Info($"停止消息监听: {who}");
            _SubWinMessageListeners.Remove(who); //移除消息监听
            _logger.Info($"移除SubWinList的监听列表成员: {who}");
        }

        #region 释放资源
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~SubWinList()
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
            if (disposing)
            {
                _MonitorSubWinCancellationTokenSource?.Cancel();
                _MonitorSubWinTaskCompletionSource?.TrySetCanceled();
                if (!_MonitorSubWinThread?.Join(3000) ?? false)
                {
                    _MonitorSubWinThread?.Interrupt();
                }
                _MonitorSubWinCancellationTokenSource?.Dispose();
                //will do: 释放所有子窗口
                foreach (var subWin in _SubWinsCache)
                {
                    subWin.Value.Dispose();
                }
                _SubWinsCache.Clear();
            }
        }
        #endregion
    }
}
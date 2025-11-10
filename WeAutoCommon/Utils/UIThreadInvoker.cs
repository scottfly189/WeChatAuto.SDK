using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using System.Diagnostics;

namespace WxAutoCommon.Utils
{
    /// <summary>
    /// 用于在UI线程中执行操作的类
    /// </summary>
    public class UIThreadInvoker : IDisposable
    {
        private readonly Thread _uiThread;
        private readonly BlockingCollection<Func<UIA3Automation, Task>> _queue = new BlockingCollection<Func<UIA3Automation, Task>>();
        private readonly TaskCompletionSource<bool> _started = new TaskCompletionSource<bool>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private UIA3Automation _automation;
        private volatile bool _disposed = false;

        public UIA3Automation Automation => _automation;

        public UIThreadInvoker()
        {
            _uiThread = new Thread(ThreadMain);
            _uiThread.SetApartmentState(ApartmentState.STA);
            _uiThread.Priority = ThreadPriority.Normal;
            _uiThread.IsBackground = false;
            _uiThread.Start();
            _started.Task.GetAwaiter().GetResult();
        }

        private void ThreadMain()
        {
            try
            {
                using (_automation = new UIA3Automation())
                {
                    _started.SetResult(true);

                    foreach (var action in _queue.GetConsumingEnumerable(_cts.Token))
                    {
                        if (_disposed) break;
                        try
                        {
                            action(_automation).GetAwaiter().GetResult();
                        }
                        catch (Exception ex)
                        {
                            // 记录异常，但不中断整个线程
                            Trace.WriteLine($"Action execution failed: {ex}");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消，不需要记录
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"UIThreadInvoker thread failed: {ex}");
                _started.TrySetException(ex);
            }
        }
        /// <summary>
        /// 在UI线程中执行操作，返回结果
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">要执行的操作</param>
        /// <returns>返回结果</returns>
        public Task<T> Run<T>(Func<UIA3Automation, T> func)
        {
            if (_disposed)
                return Task.FromResult(default(T));

            var tcs = new TaskCompletionSource<T>();
            _queue.Add(automation =>
            {
                try
                {
                    var result = func(automation);
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
                return Task.CompletedTask;
            });
            return tcs.Task;
        }
        /// <summary>
        /// 在UI线程中执行操作，不返回结果
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>返回结果</returns>
        public Task Run(Action<UIA3Automation> action)
        {
            if (_disposed)
                return Task.CompletedTask;
            return Run<object>(automation =>
            {
                action(automation);
                return null;
            });
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _cts.Cancel();
                _queue.CompleteAdding();

                // 等待线程结束
                if (_uiThread.IsAlive)
                {
                    _uiThread.Join(TimeSpan.FromSeconds(5)); // 最多等待5秒
                }

                _cts.Dispose();
                _queue.Dispose();
            }
            _automation?.Dispose();
            _disposed = true;
        }

        ~UIThreadInvoker()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using WeAutoCommon.Enums;
using WeAutoCommon.Interface;
using WeAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Models;
using System.Threading.Tasks;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.Identifiers;
using FlaUI.Core.Conditions;
using System.IO;
using FlaUI.UIA3;
using WeAutoCommon.Extentions;
using System.Threading;
using System.Diagnostics;
using WeChatAuto.Models;
using OneOf;
using System.Collections.Concurrent;
using WeChatAuto.Services;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using WeChatAuto.Options;
using MessagePack.Internal;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 消息监听器
    /// </summary>
    public class NewFriendMonitor : IDisposable
    {
        private readonly Channel<int> channel = Channel.CreateBounded<int>(new BoundedChannelOptions(1)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
            SingleReader = true,
            SingleWriter = true,
        });
        private int _disposed = 0;
        private readonly WeChatClient _Client;
        private readonly Random random = new Random((int)DateTime.Now.Ticks);
        private readonly IServiceProvider serviceProvider;
        private readonly UIThreadInvoker _MainThreadInvoker;
        private readonly SemaphoreSlim noticeEvent;
        private readonly AutoLogger<NewFriendMonitor> _Logger;
        #region 好友监听字段
        private int newFriendMonitorStarted = 0;   //消息监听启用标识
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task fetchNumberTask;
        private Task automationTask;
        private Action<string> UIInvoker;
        private int _IsContinue = 1;
        private FriendRequestAutoAcceptOptions options;
        private int _isProcessing = 0;
        #endregion


        /// <summary>
        /// <para>构造器，不应该自行调用</para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="resetEvent"></param>
        /// <param name="_uiMainThreadInvoker"></param>
        internal NewFriendMonitor(WeChatClient client, IServiceProvider serviceProvider, UIThreadInvoker _uiMainThreadInvoker, SemaphoreSlim resetEvent)
        {
            this._Client = client;
            this.serviceProvider = serviceProvider;
            this._MainThreadInvoker = _uiMainThreadInvoker;
            this.noticeEvent = resetEvent;

            _Logger = serviceProvider.GetRequiredService<AutoLogger<NewFriendMonitor>>();
            InitConsume();
        }

        private void InitConsume()
        {
            var token = cts.Token;
            automationTask = Task.Run(async () =>
            {
                try
                {
                    await foreach (var item in channel.Reader.ReadAllAsync(token))
                    {
                        if (Interlocked.Exchange(ref this._isProcessing, 1) == 1)
                        {
                            continue;
                        }
                        await noticeEvent.WaitAsync(token);
                        try
                        {
                            var list = await WeChatInvoker.Call(AutoAcceptFriendCore, this.options, token);
                            if (list.Count > 0)
                            {
                                await options.PassedCallBack(list, this._Client, serviceProvider);
                            }
                        }
                        finally
                        {
                            Volatile.Write(ref this._isProcessing, 0);
                            noticeEvent.Release();
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    _Logger.Error($"发生错误:{ex.ToString()}");
                }
            }, token);
        }

        internal List<NewFriendBackItem> AutoAcceptFriendCore(UIA3Automation automation, FriendRequestAutoAcceptOptions options, CancellationToken token)
        {
            _SwtichUI(token);  //做多微信的切换工作.
            List<NewFriendBackItem> result = this._Client.AddressBookList.PassedAllNewFriendCore(automation, options, token);
            return result;
        }


        #region 好友监听
        /// <summary>
        /// <para>自动通过加好友申请监听</para>
        /// <para>实现的功能</para>
        /// <para>1. 通过好友申请</para>
        /// <para>2. 根据设定的关键词过滤好友申请的打招呼文本，只有包含关键词的打招呼内容才会被通过</para>
        /// <para>3. 通过好友申请时，可以设置后缀,以区分不同类型的好友,方便后续的自动化实现</para>
        /// <para>4. 通过好友申请时，可以设置特定的微信标签，以方便后续的自动化与好友管理</para>
        /// <para>5. 也可以通过好友申请后，删除申请记录</para>
        /// </summary>
        /// <param name="options">配置选项，请参考<see cref="FriendRequestAutoAcceptOptions"/>类</param>
        /// <param name="token">取消令版</param>
        /// <param name="UIInvoker">UI线程调度器,适用于把微信嵌入UI的场景使用，如：多微信切换Tab页等,SDK会给调用者注入一个微信名称</param>
        /// <returns></returns>
        public async Task AddFriendRequestAutoAcceptListener(FriendRequestAutoAcceptOptions options, CancellationToken token = default, Action<string> UIInvoker = null)
        {
            this.options = options;
            await AddFriendRequestAutoAcceptListener(
                options.PassedCallBack,
                options.PassedDelete,
                options.KeyWord,
                options.Suffix,
                options.Label,
                token,
                UIInvoker);
        }

        /// <summary>
        /// 暂停好友申请监听
        /// </summary>
        /// <returns></returns>
        public async Task PauseNewFriendListener()
        {
            Volatile.Write(ref this._IsContinue, 0);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 恢复好友申请监听
        /// </summary>
        /// <returns></returns>
        public async Task ResumeNewFriendListener()
        {
            Volatile.Write(ref this._IsContinue, 1);
            await Task.CompletedTask;
        }

        /// <summary>
        /// <para>自动通过加好友申请监听</para>
        /// <para>实现的功能</para>
        /// <para>1. 通过好友申请</para>
        /// <para>2. 根据设定的关键词过滤好友申请的打招呼文本，只有包含关键词的打招呼内容才会被通过</para>
        /// <para>3. 通过好友申请时，可以设置后缀,以区分不同类型的好友,方便后续的自动化实现</para>
        /// <para>4. 通过好友申请时，可以设置特定的微信标签，以方便后续的自动化与好友管理</para>
        /// <para>5. 也可以通过好友申请后，删除申请记录</para>
        /// </summary>
        /// <param name="passedCallBack">
        /// <para>通过后的回调事件,SDK提供使用者三种信息:</para>
        /// <para>1. 通过的好友昵称列表</para>
        /// <para>2. 一个<see cref="WeChatClient"/>对象，可以通过好友申请后，通过此对象给好友发消息，注册自动监听等操作</para>
        /// <para>3. 一个<see cref="IServiceProvider"/>依赖注入容器提供者，可以通过依赖注入获取自己的业务对象</para>
        /// </param>
        /// <param name="passedDelete">通过好友申请后是否删除申请记录，默为删除</param>
        /// <param name="keyWord">关键词，如果设置关键词，只通过打招呼含有关键词的好友申请</param>
        /// <param name="suffix">后缀，如果设置后缀，被通过的好友会自动加上此后缀,如:AI.Net_Test</param>
        /// <param name="label">标签，给好友设置微信标签</param>
        /// <param name="userToken">取消令牌，可以取消监听,<see cref="CancellationToken"/></param>
        /// <param name="UIInvoker">UI的调度器，适用于把微信嵌入UI的场景使用，如：多微信切换Tab页等,SDK会给调用者注入一个微信名称</param>
        private async Task AddFriendRequestAutoAcceptListener(Func<List<NewFriendBackItem>, WeChatClient, IServiceProvider, Task> passedCallBack, bool passedDelete = true, OneOf<string, string[], List<string>> keyWord = default, string suffix = null, string label = null, CancellationToken userToken = default, Action<string> UIInvoker = null)
        {
            if (Interlocked.CompareExchange(ref this.newFriendMonitorStarted, 1, 0) == 1)
            {
                return;
            }
            this.UIInvoker = UIInvoker;
            CancellationToken token;
            if (userToken != default)
            {
                using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, userToken);
                token = linkedCts.Token;
            }
            else
            {
                token = cts.Token;
            }
            try
            {
                var startTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                fetchNumberTask = Task.Run(async () =>
                {
                    using UIThreadInvoker newFriendInvoker = new UIThreadInvoker("new-friends-fetch");
                    startTcs.TrySetResult(true);
                    using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(WeAutomation.Config.MonitorNewFriendRequestInterval));  //20秒循环一次.
                    bool firstTag = true;
                    while (firstTag || await timer.WaitForNextTickAsync(token).ConfigureAwait(false))
                    {
                        try
                        {
                            if (firstTag)
                                firstTag = false;
                            if (_IsContinue != 1)  //如果暂停了
                                continue;
                            if (Volatile.Read(ref _isProcessing) == 1)  //因为正在自动化，所以先不取数据，自动化也会将此次Session的消息全部清除.
                                continue;
                            await __FetchNewFriendNumber(token, newFriendInvoker);
                        }
                        catch (OperationCanceledException)
                        {
                            if (!startTcs.Task.IsCompleted)
                                startTcs.TrySetCanceled();
                        }
                        catch (Exception ex)
                        {
                            if (!startTcs.Task.IsCompleted)
                                startTcs.SetException(ex);
                            _Logger.Error($"监听消息发生错误：{ex.ToString()}");
                        }
                    }
                }, token);
                await startTcs.Task;
            }
            catch (OperationCanceledException)
            {
                //do nothing.
            }
            catch (Exception ex)
            {
                _Logger.Error($"监听好友申请发生错误：{ex.ToString()}");
            }
            finally
            {
            }
        }

        private async Task __FetchNewFriendNumber(CancellationToken token, UIThreadInvoker newFriendInvoker)
        {
            await newFriendInvoker.Run(automation =>
            {
                var desktop = automation.GetDesktop();
                var windowRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("mmui::MainWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                if (windowRetry.Success)
                {
                    var window = windowRetry.Result;
                    var path = @"/Group/Custom/Group/ToolBar/Button[@Name='通讯录'][@ClassName='mmui::XTabBarItem']";
                    var buttonRetry = Retry.WhileNull(() => window.FindFirstByXPath(path), timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));
                    if (buttonRetry.Success)
                    {
                        var value = buttonRetry.Result.Properties.FullDescription;
                        if (string.IsNullOrWhiteSpace(value) || value.Equals("通讯录"))
                            return;
                        var pattern = @"^([\d]+)\s*条新朋友申请$";
                        var match = Regex.Match(value, pattern);
                        if (match.Success)
                        {
                            int number = int.TryParse(match.Groups[1].Value, out var result) ? result : 0;
                            if (number > 0)
                                channel.Writer.TryWrite(number);
                        }
                    }
                }
            });
        }




        /// <summary>
        /// 允许调用者做一些UI操作.
        /// </summary>
        /// <param name="token"></param>
        private void _SwtichUI(CancellationToken token)
        {
            if (this.UIInvoker != null)
            {
                token.ThrowIfCancellationRequested();
                WeAutomation.currentContext.Send((state) => this.UIInvoker.Invoke(state.ToString()), this._Client.NickName);
            }
        }


        #endregion
        ~NewFriendMonitor()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
                return;
            if (disposing)
            {
                cts?.Cancel();
                channel.Writer.TryComplete();
                if (fetchNumberTask != null)
                {
                    if (!fetchNumberTask.IsCompleted)
                    {
                        try
                        {
                            fetchNumberTask.Wait(TimeSpan.FromSeconds(3));
                        }
                        catch (AggregateException) { }
                        catch (Exception) { }
                    }
                }
                if (automationTask != null)
                {
                    if (!automationTask.IsCompleted)
                    {
                        try
                        {
                            automationTask.Wait(TimeSpan.FromSeconds(3));
                        }
                        catch (AggregateException) { }
                        catch (Exception) { }
                    }
                }

                cts?.Dispose();
            }
        }
    }
}
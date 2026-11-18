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
using WeChatAuto.Options;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using Emgu.CV;
using System.Reflection.PortableExecutable;
using Microsoft.ML.OnnxRuntime;
using Dm.util;
using Microsoft.Windows.Themes;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 消息监听器
    /// </summary>
    public partial class MessageMonitor : IDisposable
    {
        private int _disposed = 0;
        #region 消息监听字段
        //消息监听
        private int openListnerStartedFlag = 0;   //消息监听启用标识
        private bool conversationStarted = true;
        private int totalNewMessage = 0;   //新消息数量
        private readonly ConcurrentDictionary<string, bool> _MonitorList = new ConcurrentDictionary<string, bool>();
        private readonly ConcurrentDictionary<string, bool> _FriendSession = new ConcurrentDictionary<string, bool>();  //首次与非首次session.
        private CancellationTokenSource messageGlobalCts = new CancellationTokenSource();
        private CancellationTokenSource systemLinkedCts;
        private Task messageRunningTask;
        private Action<string> UIInvoker;
        private int _IsContinue = 1;  //暂停与恢复
        private int pauseTime = Random.Shared.Next(6 * 60 * 1_000, 10 * 60 * 1_000);  //6与10分钟的某个时间段
        //系统消息监听
        private int systemListnerStartedFlag = 0;   //系统消息监听启用标识
        private int systemMonitorRunningCount = 0;  //仍在运行的系统消息监听线程数，全部退出后才复位上面的标识
        private readonly List<Thread> _SystemMonitors = new List<Thread>();
        #endregion


        #region 消息监听
        #region 系统消息监听
        /// <summary>
        /// <para>添加系统消息监听，以实现如： 检测到群主邀请好友后发送欢迎消息等功能</para>
        /// <para>注意：仅适用于群聊，不适用个人,个人请使用下面的开放式/固定式监听，另外，不支持注册监听后再新增待监听的群聊</para>
        /// <para>多线程监听变化，但是操作等，还得在微信单线程中执行.</para>
        /// </summary>
        /// <param name="nickNames">群聊昵称，可以多个</param>
        /// <param name="callBack">回调函数,由用户提供,参数：消息上下文<see cref="SystemMessageContext"/></param>
        /// <param name="userToken">取消令牌,请参考<see cref="CancellationToken"/>,可以自行取消消息监听</param>
        public async Task AddGroupSystemMessageListener(OneOf<string, List<string>, string[]> nickNames, Func<SystemMessageContext, Task> callBack, CancellationToken userToken = default)
        {
            await AddGroupSystemMessageListenerCore(nickNames, callBack, userToken);
        }
        private async Task AddGroupSystemMessageListenerCore(OneOf<string, List<string>, string[]> nickNames, Func<SystemMessageContext, Task> callBack, CancellationToken userToken)
        {
            if (Interlocked.CompareExchange(ref this.systemListnerStartedFlag, 1, 0) == 1)
            {
                return;
            }
            var token = CancellationToken.None;
            try
            {
                if (userToken != default)
                {
                    systemLinkedCts = CancellationTokenSource.CreateLinkedTokenSource(messageGlobalCts.Token, userToken);
                    token = systemLinkedCts.Token;
                }
                else
                {
                    token = messageGlobalCts.Token;
                }

                await this._Client._InitializeSystemMonitorConsumption();  //初始化系统消息消费者.

                var subWinTitles = nickNames.IsT0 ? new List<string>() { nickNames.AsT0 } : nickNames.IsT1 ? nickNames.AsT1 : nickNames.AsT2.ToList();
                foreach (var who in subWinTitles)
                {
                    SystemMonitorOption options = new SystemMonitorOption();
                    options.Who = who;
                    options.Token = token;
                    options.CallBack = callBack;

                    Thread thread = new Thread(new ParameterizedThreadStart((obj) => __GroupSystemMonitorRun(obj)));
                    thread.Priority = ThreadPriority.Normal;
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.IsBackground = true;
                    _SystemMonitors.Add(thread);
                    thread.Start(options);

                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                _Logger.Error($"监听系统消息发生错误：{ex.ToString()}");
            }
        }
        //群聊系统监控运行代码
        private void __GroupSystemMonitorRun(object obj)
        {
            SystemMonitorOption options = obj as SystemMonitorOption;
            var thradName = options.Who + "_GroupSystemMontitorRun";
            try
            {
                Interlocked.Increment(ref this.systemMonitorRunningCount);
                var token = options.Token;
                using UIA3Automation automation = new UIA3Automation();
                List<string> oldSnapshot = new List<string>();
                bool firstFlag = true;
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        RandomWait.Wait(1000, 3000);
                        if (Volatile.Read(ref _IsContinue) != 1)
                        {
                            continue;
                        }
                        //首先检查是否打开窗口
                        var subWin = __GetSubWinWithThread__(automation, options.Who);
                        if (subWin == null)
                        {
                            var subWinMain = this._Client.Conversations.OpenSubWin(options.Who).GetAwaiter().GetResult();
                            if (subWinMain == null)
                            {
                                continue;
                            }
                            else
                            {
                                subWin = __GetSubWinWithThread__(automation, options.Who);
                            }
                        }

                        if (subWin == null)
                            continue;
                        var newMessages = __FetchSystemMessageSession(subWin, ref oldSnapshot, token);
                        if (firstFlag)
                        {
                            //首次不取值.
                            firstFlag = false;
                            continue;
                        }
                        if (newMessages.Count > 0)
                        {
                            this._Client.SystemMonitorChannel.Writer.WriteAsync((options, newMessages), token).GetAwaiter().GetResult();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        //do nothing.
                    }
                    catch (Exception ex)
                    {
                        _Logger.Error($"{thradName}出现错误:{ex.ToString()}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //do nothing.
            }
            catch (Exception ex)
            {
                _Logger.Error($"{thradName}出现错误:{ex.ToString()}");
            }
            finally
            {
                //所有群聊监听线程都退出后，才复位启动标识，避免“部分线程还在跑却被判定为已停止”的竞态.
                if (Interlocked.Decrement(ref this.systemMonitorRunningCount) == 0)
                {
                    Interlocked.CompareExchange(ref this.systemListnerStartedFlag, 0, 1);
                }
            }
        }

        private List<string> __FetchSystemMessageSession(Window subWin, ref List<string> oldSnapshot, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var thisMessageSnapshot = new List<string>();
            var result = new List<string>();
            var path = UITreeGlobal.MessageRootPath;
            var rootRetry = Retry.WhileNull(() => subWin.FindFirstByXPath(path), TimeSpan.FromSeconds(3), TimeSpan.FromMilliseconds(200));
            if (rootRetry.Success)
            {
                var root = rootRetry.Result.AsListBox();
                //如果有最新消息
                var existNewMessageButton = root.GetSibling(1);
                if (existNewMessageButton != null && existNewMessageButton.ControlType == ControlType.Button && existNewMessageButton.Name.Contains("新消息"))
                {
                    //检查是不是最下面
                    var centerPointY = root.BoundingRectangle.Center().Y;
                    if (existNewMessageButton.BoundingRectangle.Y > centerPointY)
                    {
                        existNewMessageButton.AsButton().Click();
                        RandomWait.Wait(300, 900);
                    }
                }
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByClassName("mmui::ChatSystemInfoItemView")));
                //过滤掉时间
                foreach (var item in items)
                {
                    token.ThrowIfCancellationRequested();
                    var pattern = @"\s*\d{2}:\d{2}$";
                    Match match = Regex.Match(item.Name, pattern);
                    if (!match.Success)
                    {
                        thisMessageSnapshot.Add($"{item.Name}|{item.Properties.RuntimeId.ToUniqueString()}");
                    }
                }
                var exceptList = thisMessageSnapshot.Except(oldSnapshot).ToList();
                oldSnapshot = thisMessageSnapshot;
                if (exceptList.Count > 0)
                {
                    result = exceptList.Select(r => r.Split('|', StringSplitOptions.RemoveEmptyEntries)[0]).ToList();
                }
            }
            else
            {
                _Logger.Warn($"错误：没有获取到消息根.");
            }

            return result;
        }
        #endregion 系统消息监听

        #region 非弹出窗口监听/开放式监听接口
        /// <summary>
        /// <para>添加消息监听，用户需要提供一个回调函数，当有消息时，会调用此回调函数</para>
        /// <para>参考<see cref="MessageContext"/></para>
        /// 
        /// <para>使用规则：</para> 
        /// <para>1. 仅能监听“允许消息通知”的好友/群聊,所以需要监听的好友/群聊不要设置为“消息免打扰”;</para>
        /// <para>2. 如果要避免太多消息影响，请将不监听的好友/群设置为“消息免打扰”，以提高监听性能;</para>
        /// <para>3. 此方法可以多次添加，第一次添加时会启动消息监听，第二次（类似的第三次等）添加效果等同<see cref="AddListeningFriend"/>方法</para>
        /// <para>4. 为了减少会话窗口的滚动，建议不要添加太多消息“置顶”，以提高监听效率.</para>
        /// <para>执行逻辑:</para>
        /// <para>1. 启动消息监听器时做做一次全量扫描，即：SDK会将会话列表整个循环一编，会自动点击并回调用户设定的方法，以防止遗漏消息</para>
        /// <para>2. 以后的监听过程会增量监听，以提高效率.</para>
        /// </summary>
        /// <param name="nickNames">好友昵称,可以是一个，也可以是多个好友/群聊 </param>
        /// <param name="callBack">回调函数,由用户提供,参数：消息上下文<see cref="MessageContext"/></param>
        /// <param name="IsOpenMonitor">是否开启开放式监听，默认不开放(值为false）,如果开启开放式监听，前面的nickNames可以为空，所谓的开放式监听的含义是：无须固定好友/群监听，只要此好友/群没有设置“消息免打挠”就可以监听,而固定式监听为只监听设定的好友/群</param>
        /// <param name="userToken">取消令牌,请参考<see cref="CancellationToken"/>,可以自行取消消息监听</param>
        /// <param name="UIInvoker">UI的调度器，适用于把微信嵌入UI的场景使用，如：多微信切换Tab页等,SDK会给调用者注入一个微信名称</param>
        /// <param name="options">监听选项，具体请参见<see cref="MessageMonitorOptions"/></param>
        public async Task AddMessageListener(OneOf<string, List<string>, string[]> nickNames, Func<MessageContext, Task> callBack, bool IsOpenMonitor = false, CancellationToken userToken = default, Action<string> UIInvoker = null, MessageMonitorOptions options = null)
        => await AddMessageListenerCore(nickNames, callBack, IsOpenMonitor, userToken, UIInvoker, new DateTimeRange(), options);

        /// <summary>
        /// 添加一个从什么时候开始，什么时候结束的消息监听，用户需要提供一个回调函数，当有消息时，会调用此回调函数
        /// 参考<see cref="MessageContext"/>
        /// 添加此监听，在设定的开始时间开始监听，并且在设定的结束时间暂停监听
        /// 
        /// <para>使用规则：</para>
        /// <para>1. 仅能监听“允许消息通知”的好友/群聊,所以需要监听的好友/群聊不要设置为“消息免打扰”;</para>
        /// <para>2. 如果要避免太多消息影响，请将不监听的好友/群设置为“消息免打扰”，以提高监听性能;</para>
        /// <para>3. 此方法可以多次添加，第一次添加时会启动消息监听，第二次（类似的第三次等）添加效果等同<see cref="AddListeningFriend"/>方法</para>
        /// <para>4. 为了减少会话窗口的滚动，建议不要添加太多消息“置顶”，以提高监听效率.</para>
        /// <para>执行逻辑:</para>
        /// <para>1. 启动消息监听器时做做一次全量扫描，即：SDK会将会话列表整个循环一编，会自动点击并回调用户设定的方法，以防止遗漏消息</para>
        /// <para>2. 以后的监听过程会增量监听，以提高效率.</para>
        /// </summary>
        /// <param name="nickNames">好友昵称,可以是一个，也可以是多个好友/群聊 </param>
        /// <param name="callBack">回调函数,由用户提供,参数：消息上下文<see cref="MessageContext"/></param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param> 
        /// <param name="IsOpenMonitor">是否开启开放式监听，默认不开放(值为false）,如果开启开放式监听，前面的nickNames可以为空，所谓的开放式监听的含义是：无须固定好友/群监听，只要此好友/群没有设置“消息免打挠”就可以监听</param>
        /// <param name="userToken">取消令牌,请参考<see cref="CancellationToken"/>,可以自行取消消息监听</param>
        /// <param name="UIInvoker">UI的调度器，适用于把微信嵌入UI的场景使用，如：多微信切换Tab页等,SDK会给调用者注入一个微信名称</param>
        /// <param name="options">监听选项，具体请参见<see cref="MessageMonitorOptions"/></param>
        public async Task AddMessageListener(OneOf<string, List<string>, string[]> nickNames, Func<MessageContext, Task> callBack, TimeOnly startTime, TimeOnly endTime, bool IsOpenMonitor = false, CancellationToken userToken = default, Action<string> UIInvoker = null, MessageMonitorOptions options = null)
        => await AddMessageListenerCore(nickNames, callBack, IsOpenMonitor, userToken, UIInvoker, new DateTimeRange()
        {
            IsCheckDate = true,
            TimeList = new List<TimeOnlyRange>(){
                new TimeOnlyRange()
                {
                    StarTime = startTime,
                    EndTime = endTime,
                }
            }
        }, options);
        /// <summary>
        /// 添加一天中多个时间段的消息监听，用户需要提供一个回调函数，当有消息时，会调用此回调函数
        /// 参考<see cref="MessageContext"/>
        /// 
        /// <para>使用规则：</para>
        /// <para>1. 仅能监听“允许消息通知”的好友/群聊,所以需要监听的好友/群聊不要设置为“消息免打扰”;</para>
        /// <para>2. 如果要避免太多消息影响，请将不监听的好友/群设置为“消息免打扰”，以提高监听性能;</para>
        /// <para>3. 此方法可以多次添加，第一次添加时会启动消息监听，第二次（类似的第三次等）添加效果等同<see cref="AddListeningFriend"/>方法</para>
        /// <para>4. 为了减少会话窗口的滚动，建议不要添加太多消息“置顶”，以提高监听效率.</para>
        /// <para>执行逻辑:</para>
        /// <para>1. 启动消息监听器时做做一次全量扫描，即：SDK会将会话列表整个循环一编，会自动点击并回调用户设定的方法，以防止遗漏消息</para>
        /// <para>2. 以后的监听过程会增量监听，以提高效率.</para>
        /// </summary>
        /// <param name="nickNames">好友昵称,可以是一个，也可以是多个好友/群聊 </param>
        /// <param name="callBack">回调函数,由用户提供,参数：消息上下文<see cref="MessageContext"/></param>
        /// <param name="range">一天中的多个时间段,如果设定多个时间段，监听器在这些时间段内开始/结束监听,时间段类请参考:<see cref="TimeOnlyRange"/>,另注意：可以跨天，如设置为:23:00 ~ 02:00</param>
        /// <param name="IsOpenMonitor">是否开启开放式监听，默认不开放(值为false）,如果开启开放式监听，前面的nickNames可以为空，所谓的开放式监听的含义是：无须固定好友/群监听，只要此好友/群没有设置“消息免打挠”就可以监听</param>
        /// <param name="userToken">取消令牌,请参考<see cref="CancellationToken"/>,可以自行取消消息监听</param>
        /// <param name="UIInvoker">UI的调度器，适用于把微信嵌入UI的场景使用，如：多微信切换Tab页等,SDK会给调用者注入一个微信名称</param>
        /// <param name="options">监听选项，具体请参见<see cref="MessageMonitorOptions"/></param>
        public async Task AddMessageListener(OneOf<string, List<string>, string[]> nickNames, Func<MessageContext, Task> callBack, List<TimeOnlyRange> range, bool IsOpenMonitor = false, CancellationToken userToken = default, Action<string> UIInvoker = null, MessageMonitorOptions options = null)
        => await AddMessageListenerCore(nickNames, callBack, IsOpenMonitor, userToken, UIInvoker, new DateTimeRange()
        {
            IsCheckDate = true,
            TimeList = range,
        }, options);

        /// <summary>
        /// 暂停消息监听
        /// </summary>
        /// <returns></returns>
        public async Task PauseMessageListener()
        {
            Volatile.Write(ref _IsContinue, 0);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 恢复消息监听
        /// </summary>
        /// <returns></returns>
        public async Task ResumeMessageListener()
        {
            Volatile.Write(ref _IsContinue, 1);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 监听过程中动态添加待监听的好友/群聊
        /// </summary>
        /// <param name="who">好友/群聊名称</param>
        /// <returns></returns>
        public async Task AddListeningFriend(string who)
        {
            if (openListnerStartedFlag != 1)
                throw new Exception("错误：请先启动消息监听器");
            if (!_MonitorList.Keys.Contains(who))
            {
                _MonitorList.TryAdd(who, false);
            }
            await Task.CompletedTask;
        }
        /// <summary>
        /// 监听过程中动态移除被监听的好友/群聊
        /// </summary>
        /// <param name="who">好友/群聊</param>
        /// <returns></returns>
        public async Task RemoveListeningFriend(string who)
        {
            if (openListnerStartedFlag != 1)
                throw new Exception("错误：请先启动消息监听器");
            _MonitorList.TryRemove(who, out _);
            await Task.CompletedTask;
        }
        #endregion 非弹出窗口监听/开放式监听接口

        private async Task AddMessageListenerCore(OneOf<string, List<string>, string[]> nickNames, Func<MessageContext, Task> callBack, bool IsOpenMonitor, CancellationToken userToken, Action<string> UIInvoker, DateTimeRange range, MessageMonitorOptions options)
        {
            if (Interlocked.CompareExchange(ref openListnerStartedFlag, 1, 0) == 1)
            {
                List<string> list = nickNames.IsT0 ? new List<string>() { nickNames.AsT0 } : nickNames.IsT1 ? nickNames.AsT1 : nickNames.AsT2.ToList();
                foreach (var item in list)
                {
                    await AddListeningFriend(item);
                }
                return;
            }
            this.UIInvoker = UIInvoker;
            CancellationToken token;
            CancellationTokenSource linkedCts = default;
            if (userToken != default)
            {
                linkedCts = CancellationTokenSource.CreateLinkedTokenSource(messageGlobalCts.Token, userToken);
                token = linkedCts.Token;
            }
            else
            {
                token = messageGlobalCts.Token;
            }
            (nickNames.IsT0 ? new List<string>() { nickNames.AsT0 } : nickNames.IsT1 ? nickNames.AsT1.ToHashSet().ToList() : nickNames.AsT2.ToHashSet().ToList()).ForEach(u => _MonitorList.TryAdd(u, false));  //监听列表初始化.
            try
            {
                var startTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                if (options == null)
                    options = new MessageMonitorOptions();
                messageRunningTask = Task.Run(async () =>
                {
                    startTcs.TrySetResult(true);
                    using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(WeAutomation.Config.MonitorInterval));
                    Stopwatch riskWatch = new Stopwatch();
                    riskWatch.Start();
                    try
                    {
                        while (await timer.WaitForNextTickAsync(token).ConfigureAwait(false))
                        {
                            try
                            {
                                if (Volatile.Read(ref _IsContinue) != 1)  //如果暂停了
                                    continue;
                                if (range.IsCheckDate) //时间段检查
                                {
                                    var flag = _CheckRuntime(range.TimeList);
                                    if (!flag)
                                        continue;
                                }
                                if (options.IsRiskPrevention)
                                {
                                    //如果启用了风控选项
                                    await __RunPreventionRisk(options, riskWatch);
                                }
                                await noticeEvent.WaitAsync(token);
                                try
                                {
                                    await WeChatInvoker.Call(AddMessageListenerAction, callBack, token, IsOpenMonitor, options);
                                }
                                finally
                                {
                                    noticeEvent.Release();
                                }
                            }
                            catch (OperationCanceledException)
                            {
                                if (!startTcs.Task.IsCompleted)
                                    startTcs.TrySetCanceled();
                                _Logger.Debug("开试式监听正常退出....");
                            }
                            catch (Exception ex)
                            {
                                if (!startTcs.Task.IsCompleted)
                                    startTcs.SetException(ex);
                                _Logger.Error($"开放式监听：{ex.ToString()}");
                            }
                        }
                    }
                    finally
                    {
                        //恢复到初始值，以便可以重新发起消息监听.
                        Interlocked.CompareExchange(ref openListnerStartedFlag, 0, 1);
                        if (linkedCts != default)
                        {
                            linkedCts?.Dispose();
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
                _Logger.Error($"监听消息发生错误：{ex.ToString()}");
            }
            finally
            {

            }
        }
        /// <summary>
        /// 运行预防风控行为
        /// </summary>
        /// <param name="options">监听选项</param>
        /// <param name="riskWatch">7</param>
        /// <exception cref="NotImplementedException"></exception>
        private async Task __RunPreventionRisk(MessageMonitorOptions options, Stopwatch riskWatch)
        {
            try
            {
                var timespan = riskWatch.Elapsed;
                if (timespan.TotalMilliseconds >= pauseTime)  //条件满足
                {
                    System.Diagnostics.Debug.WriteLine($"===== 开始运行风控行为代码 ======");
                    try
                    {
                        await options?.RiskPreventionAction?.Invoke(this._Client);  //运行预防风控行为
                        riskWatch.Restart();  //重新计时
                    }
                    finally
                    {
                        await this._Client.SwitchNavigation(NavigationType.微信);
                    }
                    this.pauseTime = Random.Shared.Next(6 * 60 * 1_000, 10 * 60 * 1_000);   //重新计算下一次停顿时间.
                    //await WeChatInvoker.Call(__FixedSideEffect);  //暂时取消副作用，因为有些打开的子窗口监听是不需要关闭的.
                    System.Diagnostics.Debug.WriteLine($"===== 运行风控行为代码结束 ======");
                }
            }
            catch (Exception ex)
            {
                _Logger.Error("消息监听中运行预防风控行为出错:" + ex.ToString());
            }
        }

        /// <summary>
        /// 开放式监听主方法
        /// </summary>
        private void AddMessageListenerAction(UIA3Automation automation, Func<MessageContext, Task> callBack, CancellationToken token, bool IsOpenMonitor, MessageMonitorOptions options)
        {
            var root = this._Client.Conversations.ConversationRoot;
            if (root == null)
            {
                _Logger.Error("开放式监听没有获取到 会话列表的 根！");
                RandomWait.Wait(100, 1200);
                return;
            }
            token.ThrowIfCancellationRequested();
            if (conversationStarted)
            {
                //一开始对会话列表遍历一次.
                conversationStarted = false;
                _SwtichUI(token);
                _TravelConversationList(automation, callBack, IsOpenMonitor, token, root, options);
                return;
            }
            //获取待处理消息数量.
            this.totalNewMessage = _GetTotalMessage(token);
            if (this.totalNewMessage == 0)
            {
                return;
            }
            //增量执行监听
            _SwtichUI(token);
            token.ThrowIfCancellationRequested();
            _MessageClickScheduling(automation, callBack, token, IsOpenMonitor, root, options);
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

        private int _GetTotalMessage(CancellationToken token)
        {
            try
            {
                var root = this._Client.Navigation.rootElement;
                if (root == null)
                {
                    _Logger.Error("错误：未能获取到Navigation的根，可能Navigation的UI Tree有改变!");
                    return 0;
                }
                var item = root.FindFirstChild(cf => cf.ByName("微信").And(cf.ByControlType(ControlType.Button)));
                if (item == null)
                {
                    _Logger.Error("错误：未能获取到Navigation的聊天按钮，可能聊天按钮的名字:微信 有改变！请检查！");
                    return 0;
                }
                item.WaitUntilClickable();
                var title = item.Properties.FullDescription;
                if (string.IsNullOrEmpty(title) || title == "微信")
                    return 0;
                var pattern = @"^([\d]+)条新消息$";
                var match = Regex.Match(title, pattern);
                if (match.Success)
                {
                    return int.TryParse(match.Groups[1].Value, out var result) ? result : 0;
                }
                else
                {
                    _Logger.Error("错误：微信提示待读消息格式有变化，正则表达式失效，请处理！");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error($"开放式监听：{nameof(MessageMonitor)} - {nameof(_GetTotalMessage)}发生错误:{ex.ToString()}");
                return 0;
            }
        }

        private void _TravelConversationList(UIA3Automation automation, Func<MessageContext, Task> callBack, bool IsOpenMonitor, CancellationToken token, ListBox root, MessageMonitorOptions options)
        {
            //重新获取会话列表根，避免 UI Tree 变化导致 root 引用失效而抛异常
            root = this._Client.Conversations.ConversationRoot;
            if (root == null)
            {
                _Logger.Error("开放式监听：会话列表根已失效，本轮跳过");
                return;
            }
            //当前的消息列表处理
            _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, root.BoundingRectangle, token, options);
            //先往上翻到顶.
            this._Client.Conversations.UpCore(automation, (els, rootRect) =>
            {
                _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, rootRect, token, options);
                return true;
            });
            //再往下翻到底
            this._Client.Conversations.DownCore(automation, (el, rootRect) =>
            {
                _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, rootRect, token, options);
                return true;
            });
            //再往上翻到顶
            this._Client.Conversations.UpCore(automation, (els, rootRect) => true);
        }

        private void _MessageClickScheduling(UIA3Automation automation, Func<MessageContext, Task> callBack, CancellationToken token, bool IsOpenMonitor, ListBox root, MessageMonitorOptions options)
        {
            this._Client.MainWindow.Focus();
            Mouse.Position = _Client.MainWindow.BoundingRectangle.Center();

            var index = 0;
            token.ThrowIfCancellationRequested();

            //重新获取会话列表根，避免 UI Tree 变化导致 root 引用失效而抛异常
            root = this._Client.Conversations.ConversationRoot;
            if (root == null)
            {
                _Logger.Error("开放式监听：会话列表根已失效，本轮跳过");
                return;
            }
            //当前的消息列表处理
            _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, root.BoundingRectangle, token, options);
            if (_GetTotalMessage(token) == 0)
                return;

            //先往下滚动
            this._Client.Conversations.DownCore(automation, (el, rootRect) =>
            {
                index++;
                _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, rootRect, token, options);
                if (index <= WeAutomation.Config.MonitorMessageMaxDownInterval)
                {
                    return true;
                }
                return false;
            });
            //再往上翻到顶.
            this._Client.Conversations.UpCore(automation, (els, rootRect) =>
            {
                _ProcessVisibleConversation(automation, callBack, IsOpenMonitor, rootRect, token, options);
                return true;
            });

            token.ThrowIfCancellationRequested();
        }

        //未设置“免打扰”,有未读消息数字
        private void _ProcessVisibleConversation(UIA3Automation automation, Func<MessageContext, Task> callBack, bool IsOpenMonitor, Rectangle rootRect, CancellationToken token, MessageMonitorOptions options)
        {
            List<SimpleConversation> visibleConvesationList = _Client.Conversations.GetVisibleConversationsCore(automation);  //可见的会话列表项目，但也会滚动不见
            RandomWait.Wait(100, 350);
            token.ThrowIfCancellationRequested();
            var filterObjList = visibleConvesationList.Where(item => !item.IsDoNotDisturb && item.NotReadNumbr > 0).ToList();  //取出没有设置“免打扰”的好友,并且未读数>0
            var elementList = _Client.Conversations.GetVisibleConversationElements(automation);  //取所有可见的会话元素
            List<AutomationElement> clickList = new List<AutomationElement>();  //待点击的列表
            var tmpList = filterObjList.Select(x => x.ConversationTitle);  //可点击的元素标题 列表
            foreach (var item in elementList)
            {
                string[] aryItems = item.Name.Split('\n');
                var name = aryItems[0].Trim();
                if (tmpList.Contains(name))
                {
                    clickList.Add(item);
                }
            }
            var willParseList = new List<string>();  //待获取元素并要求返回消息回调s
            if (IsOpenMonitor)
            {
                willParseList.AddRange(clickList.Select(u => u.GetName()));
            }
            else
            {
                var l1 = clickList.Select(u => u.GetName().Trim()).ToList(); //本次获取的对象。
                var l2 = this._MonitorList.Keys;  //所有监听对象
                willParseList.AddRange(l2.Intersect(l1));
            }


            foreach (var item in clickList)   //丢失了点击没有关系，下一轮又会点击，关键不能失去点击的能力.
            {
                token.ThrowIfCancellationRequested();
                if (item.IsAvailable && item.BoundingRectangle.IsClickSafe(rootRect))
                {
                    var point = item.BoundingRectangle.SafeRandomPoint();
                    Mouse.Position = point;
                    RandomWait.Wait(50, 200);
                    SupperMouseKey.LeftClick();   //点击
                    RandomWait.Wait(200, 600);
                    var clickBackResult = _ProcessClickBack();  //点击了服务号需要进行返回
                    if (clickBackResult)
                        continue;
                    __CheckClickError(item);
                    if (willParseList.Contains(item.GetName()))
                    {
                        var excludeResult = ExcludeUnParserItems(item);
                        if (excludeResult)
                            continue;
                        var messageListRoot = this._Client.MainWindow.FindFirstByXPath(UITreeGlobal.MessageRootPath);
                        if (messageListRoot == null)
                        {
                            _Logger.Error($"抓取的名字为[{item.Name}]-未找到消息列表根元素，无法获取消息");
                            continue;
                        }
                        _ParserMessaageCore(automation, callBack, token, item, options, messageListRoot);
                    }
                }
            }
        }

        private void __CheckClickError(AutomationElement item)
        {
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/Group/Group/Group/Text[@AutomationId='content_view.top_content_view.title_h_view.left_v_view.left_content_v_view.left_ui_.big_title_line_h_view'][@ClassName='mmui::XHBoxView']";
            var retry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromMilliseconds(800), TimeSpan.FromMilliseconds(100));
            if (!retry.Success)
            {
                item.Click();
                RandomWait.Wait(100, 500);
            }
        }

        private bool ExcludeUnParserItems(AutomationElement item)
        {
            string[] names = new string[]
            {
              "文件传输助手",
              "公众号",
              "服务通知",
              "腾讯新闻",
              "元宝",
              "微信支付",
              "QQ邮箱提醒"
            };
            foreach (var name in names)
            {
                if (item.Name.Trim().StartsWith(name))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 消息获取核心方法
        /// </summary>
        private void _ParserMessaageCore(UIA3Automation automation, Func<MessageContext, Task> callBack, CancellationToken token, AutomationElement clickConversionItem, MessageMonitorOptions options, AutomationElement messageListRoot)
        {
            token.ThrowIfCancellationRequested();
            var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
            if (title == null)
                return;
            if (!_FriendSession.Keys.Contains(title.Title))
            {
                _FriendSession.TryAdd(title.Title, false);
            }

            if (options != null && options.FetchFriendInfo)
            {
                //先获取用户信息后再获取消息
                if (title.HeaderType == ChatType.好友)  //只有普通好友才有wxid.
                {
                    //session的首次取一下，不影响未来的100%准确性.
                    if (!_FriendSession[title.Title])
                    {
                        _FetchFriendInfo(automation, token, title);
                        _FriendSession[title.Title] = true;  //一次session中，下次不用再取.
                    }
                }
            }
            __FetchMessaageWithOpen(automation, callBack, clickConversionItem, options, messageListRoot, title, token);
        }
        /// <summary>
        /// 获取数据核心方法
        /// </summary>
        private void __FetchMessaageWithOpen(UIA3Automation automation, Func<MessageContext, Task> callBack, AutomationElement clickConversionItem, MessageMonitorOptions options, AutomationElement messageListRoot, HeaderInfo title, CancellationToken token)
        {
            //至底部，并且返回checkpoint，方便知道第一次ocr是否变化。
            MessageCheckpointItem checkPoint = __GoBottomWithOpen__(automation, options, messageListRoot, token);
            if (checkPoint == null)
                return;

            //获取本次的消息列表
            var allThisSessionMessageList = new List<SimpleMessageBubble>();
            var result = _UpScrollAndFetchThisSessionWithOpen(automation, options, messageListRoot, title, token, checkPoint, allThisSessionMessageList);
            __FixeThisSessionDate__(result, allThisSessionMessageList);
            //处理日期
            __ProcessThisSessionDate__(result, title, token);
            //做去重工作并保存进入cache与数据库
            var diffList = __SaveToCacheAndDb__(result, title, options);
            //回调用户提供函数
            if (callBack != null && diffList.Count() > 0)
            {
                var historyList = MessageCacheHelper.GetLastMessages(title.Title, WeAutomation.Config.MaxHistoryMessageFetchNumber, this._Client.Alias);
                MessageContext sessionContext = new MessageContext(diffList, historyList, this._Client.ChatContent.Sender, this._Client, this._Client.Factory, this.serviceProvider, this._Client.NickName, title.Title);

                callBack.Invoke(sessionContext).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// 保存至缓存与数据库的核心方法，适用于开放式监听与弹窗式监听.
        /// </summary>
        private List<SimpleMessageBubble> __SaveToCacheAndDb__(List<SimpleMessageBubble> result, HeaderInfo info, OneOf<PullMessagesOptions, MessageMonitorOptions> options)
        {
            List<SimpleMessageBubble> oldOriList = MessageCacheHelper.GetLastMessages(info.Title, result.Count(), this._Client.Alias);
            var diffs = LcsHelper.Diff<SimpleMessageBubble>(oldOriList, result, new MessageComparer());
            // System.Diagnostics.Debug.WriteLine($"===== 差异比较 ======");
            // foreach (var item in diffs)
            // {
            //     System.Diagnostics.Debug.WriteLine($"{item.Type,-6}  {item.Value}");
            // }
            // System.Diagnostics.Debug.WriteLine($"===== 结束本次差异比较 ======");
            var diffsList = LcsHelper.GetNewItems(oldOriList, result, new MessageComparer());
            MessageCacheHelper.AddMessageCaches(info.Title, diffsList, this._Client.Alias);
            if (diffsList.Count() > 0)
            {
                __SaveToDbCore__(options, diffsList, info);
            }
            //测试差异增加
            System.Diagnostics.Debug.WriteLine($"===== 差异消息: {info.Title} 的消息 ======");
            foreach (var item in diffsList)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
            System.Diagnostics.Debug.WriteLine($"共有记录:{diffsList.Count()} 条,其中系统消息:{diffsList.Where(u => u.MessageType == MessageType.系统).Count()}条,非系统消息:{diffsList.Where(u => u.MessageType != MessageType.系统).Count()}条");
            System.Diagnostics.Debug.WriteLine($"===== 结束输出差异 {info.Title} 的消息 ======");

            return diffsList;
        }

        /// <summary>
        /// 到底部，并且返回CheckPoint
        /// </summary>
        private MessageCheckpointItem __GoBottomWithOpen__(UIA3Automation automation, MessageMonitorOptions options, AutomationElement root, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            MessageCheckpointItem result = null;
            if (root.AsListBox().Items.Count() == 0)
                return result;
            this._Client.MainWindow.Focus();
            //如果有最新跳转按钮
            var index = 0;
            while (index < 3)
            {
                token.ThrowIfCancellationRequested();
                var button = root.GetSibling(1);
                if (button != null && button.ControlType == ControlType.Button)
                {
                    index = 0;
                    button.Click();
                    RandomWait.Wait(100, 600);
                    continue;
                }

                index++;
                RandomWait.Wait(50, 200);
            }

            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return result;
            this._Client.MainWindow.Focus();
            if (__CheckNotBottomState__(listBox))
            {
                __QuickGoBottom__(this._Client.MainWindow, root); //快速下移
            }

            index = 0;
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            IEnumerable<string> oldSnapshot = new List<string>();
            this._Client.MainWindow.Focus();
            while (index < options.BottomRetryNumber)
            {
                token.ThrowIfCancellationRequested();
                this._Client.MainWindow.Focus();
                var newSnapshot = listBox.Items.Select(u => u.Name + "|" + u.Properties.RuntimeId.ToUniqueString());
                var exceptList = newSnapshot.Except(oldSnapshot);
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapshot = newSnapshot;
                }
                else
                {
                    if (!listBox.Items[listBox.Items.Count() - 1].IsAvailable || (listBox.Items[listBox.Items.Count() - 1].BoundingRectangle.Y + listBox.Items[listBox.Items.Count() - 1].BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1))
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                this._Client.MainWindow.Focus();
                MouseScrollHelper.DownStep(safePoint, 3);
            }
            result = _GetBottomElement(listBox, root);
            return result;
        }

        private bool __CheckNotBottomState__(ListBox listBox)
        {
            var lastItem = listBox.Items[listBox.Items.Count() - 1];
            return lastItem.BoundingRectangle.Y + lastItem.BoundingRectangle.Height > listBox.BoundingRectangle.Y + listBox.BoundingRectangle.Height + 1;
        }

        /// <summary>
        /// 向上滚动，以获取所有的上下文消息列表.
        /// </summary>
        private List<SimpleMessageBubble> _UpScrollAndFetchThisSessionWithOpen(UIA3Automation automation, MessageMonitorOptions options, AutomationElement root, HeaderInfo title, CancellationToken token, MessageCheckpointItem checkPoint, List<SimpleMessageBubble> allThisSessionMessageList)
        {
            token.ThrowIfCancellationRequested();
            var result = new List<SimpleMessageBubble>();
            //首屏ocr,如果位置有移动，则又重新进行gobottom操作,不断的进行gobottom操作
            //并且所有内容重置
            if (checkPoint == null)
                return result;
            var firstPageOCRCaches = new Dictionary<string, SimpleMessageBubble>();//左边是runtime id,右边是获取的值,通过runtime id来得到ocr识别到的SimpleMessageBubble对象。
            var oldSnapList = new List<string>();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            while (true)
            {
                //本loop的目的是为了稳定消息.
                token.ThrowIfCancellationRequested();
                this._Client.MainWindow.Focus();
                _FetchFirstPageMessage(automation, root, title, firstPageOCRCaches, result, oldSnapList, token, options, checkPoint, allThisSessionMessageList);
                if (checkPoint == null)
                    return result;
                if (!checkPoint.IsChange())
                {
                    break;
                }
                //如果变化了，重新来过
                result.clear();
                oldSnapList.clear();
                checkPoint = __GoBottomWithOpen__(automation, options, root, token);
            }
            if (result.Count() >= options.ContextFetchNumber)
                return result;
            //首页处理对结果的影响： 
            //1. 可能是长文本，不含在首页识别的内容块中;
            //2. 可能都是短文本，存在两种情况：1. 已经判断可以退出,无须下面的滚动操作; 2. 不能判断退出条件，需要执行下面的操作.
            if (_CheckFirstReturnCondition(oldSnapList, result, title))
                return result;
            //快速向上移动
            var index = 0;
            while (checkPoint.Element.IsElementInTree(root) && index < 4)
            {
                token.ThrowIfCancellationRequested();
                MouseScrollHelper.UpStep(safePoint.Confusion(3, 3), 1);
                index++;
            }

            //继续往上获取消息,这个时候会话的小圆点数字不受影响，后来加入的新消息算做下一轮.
            _UpContinueFetchCore(automation, root, title, oldSnapList, result, token, options, allThisSessionMessageList);

            return result;
        }


        //首页OCR识别，并且给后面的操作提供上下文数据.
        private void _FetchFirstPageMessage(UIA3Automation automation, AutomationElement root, HeaderInfo title, Dictionary<string, SimpleMessageBubble> firstPageDic, List<SimpleMessageBubble> result, List<string> oldSnapList, CancellationToken token, MessageMonitorOptions options, MessageCheckpointItem checkPoint, List<SimpleMessageBubble> allThisPageMessageList)
        {
            token.ThrowIfCancellationRequested();
            //可能是第一次进入，也可能是第N次，区别在于第N次进来时tempFirstDic是有数据的， 为了缩短ocr时间.
            //而oldSnapList为最终确定时给下一屏OCR识别的旧快照
            var firstPageBubbleList = new List<SimpleMessageBubble>();
            var thisPageElementList = new List<AutomationElement>();
            //得到待识别的对象
            foreach (var item in root.AsListBox().Items)
            {
                token.ThrowIfCancellationRequested();
                this._Client.MainWindow.Focus();
                if (checkPoint.IsChange())
                    return;
                if (item.IsAvailable && item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + item.BoundingRectangle.Height <= root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1)
                {
                    thisPageElementList.add(item);
                    oldSnapList.add(item.Properties.RuntimeId.ToUniqueString());
                }
            }
            if (thisPageElementList.Count == 0)
                return;
            //倒序进行ocr识别.
            thisPageElementList.Reverse();
            //整页ocr.
            foreach (var item in thisPageElementList)
            {
                token.ThrowIfCancellationRequested();
                this._Client.MainWindow.Focus();
                if (checkPoint.IsChange())
                    return;
                SimpleMessageBubble bubble = __OcrFirstPageItem__(automation, item, root, title, firstPageDic, token, options, checkPoint, allThisPageMessageList);
                if (checkPoint.IsChange())
                    return;
                if (bubble != null)
                {
                    firstPageBubbleList.add(bubble);
                }
            }
            firstPageBubbleList.Reverse();  //因为是倒序ocr识别
            if (firstPageBubbleList.Count() > 0)
            {
                result.AddRange(firstPageBubbleList);
            }
        }

        /// <summary>
        /// 首页OCR识别.
        /// </summary>
        private SimpleMessageBubble __OcrFirstPageItem__(UIA3Automation automation, AutomationElement item, AutomationElement root, HeaderInfo title, Dictionary<string, SimpleMessageBubble> firstPageDic, CancellationToken token, MessageMonitorOptions options, MessageCheckpointItem checkPoint, List<SimpleMessageBubble> allFirstPageBubbleList)
        {
            token.ThrowIfCancellationRequested();
            if (!item.IsAvailable)
                return null;
            if (checkPoint.IsChange())
                return null;
            if (firstPageDic.Keys.Contains(item.Properties.RuntimeId.ToUniqueString()))
            {
                return firstPageDic[item.Properties.RuntimeId.ToUniqueString()];
            }
            SimpleMessageBubble result = __OcrFirstPageItemCore__(automation, item, root, title, options, checkPoint, token, allFirstPageBubbleList);
            if (result != null && !firstPageDic.Keys.Contains(item.Properties.RuntimeId.ToUniqueString()))
            {
                firstPageDic.Add(item.Properties.RuntimeId.ToUniqueString(), result);
            }

            return result;
        }

        private SimpleMessageBubble __OcrFirstPageItemCore__(UIA3Automation automation, AutomationElement item, AutomationElement root, HeaderInfo title, MessageMonitorOptions options, MessageCheckpointItem checkPoint, CancellationToken token, List<SimpleMessageBubble> allFirstPageBubbleList)
        {
            token.ThrowIfCancellationRequested();
            if (checkPoint.IsChange())
                return null;
            if (!item.IsAvailable)
                return null;
            SimpleMessageBubble bubble = null;
            DateTime date = default;
            if (!IsSystemMessage(item, ref date))
            {
                var imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", $"left-item-{(int)(this.ratio * 100)}.png");
                if (!File.Exists(imagePath))
                {
                    throw new Exception("错误：左边鹰嘴模板文件不存在!");
                }
                using var leftImage = new Mat(imagePath);
                imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", $"right-item-{(int)(ratio * 100)}.png");
                if (!File.Exists(imagePath))
                {
                    throw new Exception("错误：右边鹰嘴模板文件不存在!");
                }
                using var rightImage = new Mat(imagePath);
                using var mat = this._Client.OcrEngee.GetMatFromElement(item);

                //先尝试昵称图片缓存，命中则直接组装，跳过鹰嘴方向识别与OCR
                var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                if (nickName == null)
                {
                    var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, this._Client.MainWindow);
                    nickName = _GetNickNameFromOCRCore__(item, ratio, CancellationToken.None, title, automation, direction, options, this._Client.MainWindow);
                }
                bubble = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, this._Client.MainWindow);
                allFirstPageBubbleList.add(bubble);
            }
            else
            {
                //系统消息处理
                var systemMessage = _GetSystemMessageFromElement(item, ref date, title);
                if (systemMessage != null)
                {
                    if (WeChatTimeParser.ParseWeChatTime(item.Name) == null)  //不为日期,日期不包含在消息列表中
                    {
                        bubble = systemMessage;
                    }
                    allFirstPageBubbleList.add(systemMessage);
                }
            }
            return bubble;
        }

        /// <summary>
        /// 在鹰嘴方向识别之前，先用昵称图片缓存尝试直接命中昵称。
        /// 命中时无须再做鹰嘴模板匹配与OCR，即可直接组装 SimpleMessageBubble；
        /// 未命中（好友/企业微信、右侧自己消息、或未知群成员）返回 null，回落到原有流程。
        /// 说明：昵称图片缓存仅对群聊生效（64×32 dHash，稳定不串号）；右侧自己消息昵称区为空白，无法计算哈希，天然不会误命中。
        /// </summary>
        /// <param name="oriMat">消息条目的整图（调用方已通过 GetMatFromElement 获取，本方法只读取不释放）</param>
        /// <param name="title">会话标题信息</param>
        /// <param name="ratio">DPI 缩放比例</param>
        /// <returns>命中返回昵称，否则返回 null</returns>
        private string _TryGetNickNameFromImageCache__(Mat oriMat, HeaderInfo title, decimal ratio)
        {
            try
            {
                //只有群聊才按昵称图片缓存识别；好友/企业微信昵称即会话标题，右侧自己消息缓存区为空白，均无须（也无法）命中缓存。
                if (title.HeaderType != ChatType.群聊)
                    return null;
                //裁剪昵称区域（与 _GetNickNameFromOCRCore__ 保持一致，保证命中同一套缓存）
                int top = 0;
                var left = (int)(64 * ratio);
                var height = (int)(28 * ratio);
                var width = (int)(oriMat.Width - left);
                using var mat = new Mat(oriMat, new Rectangle(left, top, width - 100, height));
                var cacheItem = ImageHashCacheHelper.GetImageCacheItem(title.Title, mat);
                return string.IsNullOrWhiteSpace(cacheItem?.NickName) ? null : cacheItem.NickName;
            }
            catch (Exception ex)
            {
                this._Logger.Error($"昵称图片缓存识别失败，回落原有流程:{ex}");
                return null;
            }
        }

        private string _GetNickNameFromOCRCore__(AutomationElement item, decimal ratio, CancellationToken token, HeaderInfo title, UIA3Automation automation, string direction, MessageMonitorOptions options, Window win)
        {
            try
            {
                if (direction.Equals("right"))
                    return "我";
                if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
                    return title.Title;
                //缓存初始化.
                using var oriMat = this._Client.OcrEngee.GetMatFromElement(item);
                RandomWait.Wait(100, 400);
                int top = 0;   //计算-算上dpi的顶端
                var left = (int)(64 * ratio);  //计算-算上dpi的左边
                var height = (int)(28 * ratio);  //计算上dpi的高度
                var width = (int)(oriMat.Width - left);
                using var mat = new Mat(oriMat, new Rectangle(left, top, width - 100, height));
                if (options.IsCaptureOriImage)
                {
                    _WriteToTempCacheForTest(mat);
                }
                //进行预处理
                var mat2 = this._Client.OcrEngee.ProcessImageForOcr(mat);
                RandomWait.Wait(100, 400);
                if (options.IsCaptureOriImage)
                {
                    _WriteToTempCacheForTest(mat2);
                }
                var cacheItem = ImageHashCacheHelper.GetImageCacheItem(title.Title, mat);
                if (cacheItem != null)
                {
                    return cacheItem.NickName;
                }
                if (options.UseUITreeRollbackPlan)
                {

                    //通过ui tree点击方案获取昵称.
                    var nickName = __GetNickNameFromUITreeClick__(item, win);
                    if (!string.IsNullOrEmpty(nickName))
                    {
                        //通过ui tree的方式获取昵称应该也将图片缓存应用起来
                        ImageCacheItem cache = new ImageCacheItem();
                        cache.HashCode = ImageHashCacheHelper.ComputeDHash(mat);
                        cache.NickName = nickName;
                        ImageHashCacheHelper.AddImageCacheItem(title.Title, cache);
                        return nickName;
                    }
                    else
                    {
                        _Logger.Error($"错误：通过UI Tree获取昵称为空!");
                        return "";
                    }
                }
                else
                {
                    //通过ocr获取昵称
                    var result = this._Client.OcrEngee.Detect(mat2.ToBitmap(), options.OcrPadding, 1600, 0.25f, 0.2f, 1.8f, false, false, options.IsOCRDebug);
                    var returnResult = string.IsNullOrWhiteSpace(result.StrRes) ? "" : result.StrRes.Trim();
                    returnResult = _CheckCharScores_(result, returnResult, item, win, options.CharScoreThresh);
                    if (options.FallbackList.Keys.Contains(returnResult))
                    {
                        returnResult = options.FallbackList[returnResult].toString().Trim();
                    }
                    RandomWait.Wait(100, 400);
                    if (!string.IsNullOrEmpty(returnResult))
                    {
                        ImageCacheItem cache = new ImageCacheItem();
                        cache.HashCode = ImageHashCacheHelper.ComputeDHash(mat);
                        cache.NickName = returnResult;
                        ImageHashCacheHelper.AddImageCacheItem(title.Title, cache);
                        return returnResult;
                    }
                    else
                    {
                        _Logger.Error($"错误：通过OCR方式获取昵称为空!");
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                this._Logger.Error("开放式消息监听在进行OCR识别时出现错误，错误原因:\r\n" + ex.ToString() + "\r\n错误堆栈:" + ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 持续向上获取消息.
        /// </summary>
        private void _UpContinueFetchCore(UIA3Automation automation, AutomationElement root, HeaderInfo title, List<string> oldSnapList, List<SimpleMessageBubble> result, CancellationToken token, MessageMonitorOptions options, List<SimpleMessageBubble> allThisPageMessageList)
        {
            token.ThrowIfCancellationRequested();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            if (root.AsListBox().Items.Count() == 0)
                return;

            //要考虑长文本的情况
            var index = 0;
            while (index < options.BottomRetryNumber)
            {
                token.ThrowIfCancellationRequested();
                this._Client.MainWindow.Focus();

                if (result.Count >= options.ContextFetchNumber)
                {
                    break;
                }

                var newSnapshot = new List<string>();
                __GetNewSnapshot__(root, newSnapshot, token);   //得到所有newsnapshot.
                var exceptList = newSnapshot.Except(oldSnapList).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapList = newSnapshot;
                    bool exitFlag = _FetchThisPageMessges_(automation, options, root, result, exceptList, title, token, this._Client.MainWindow, allThisPageMessageList);
                    if (exitFlag)
                        break;
                }
                else
                {
                    if (root.AsListBox().Items[0].IsAvailable && root.AsListBox().Items[0].BoundingRectangle.Y < root.BoundingRectangle.Y)
                    {
                        //长文本支持.
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }

                MouseScrollHelper.UpStep(safePoint.Confusion(3, 6), 3);  //随机向上滚动.
            }
        }

        private bool _FetchThisPageMessges_(UIA3Automation automation, MessageMonitorOptions options, AutomationElement root, List<SimpleMessageBubble> result, List<string> exceptList, HeaderInfo title, CancellationToken token, Window win, List<SimpleMessageBubble> allThisPageMessageList)
        {
            token.ThrowIfCancellationRequested();
            var returnFlag = false;
            var pageResult = new List<SimpleMessageBubble>();

            // var height = (int)(73 * ratio);        //考虑dpi
            var willProcessList = root.AsListBox().Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
            willProcessList.Reverse(); //从下面的开始处理

            DateTime date = default;

            var imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", $"left-item-{(int)(this.ratio * 100)}.png");
            if (!File.Exists(imagePath))
            {
                throw new Exception("错误：左边鹰嘴模板文件不存在!");
            }
            using var leftImage = new Mat(imagePath);
            imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", $"right-item-{(int)(ratio * 100)}.png");
            if (!File.Exists(imagePath))
            {
                throw new Exception("错误：右边鹰嘴模板文件不存在!");
            }
            using var rightImage = new Mat(imagePath);
            foreach (var item in willProcessList)
            {
                if (!item.IsElementInTree(root))
                    continue;
                win.Focus();
                token.ThrowIfCancellationRequested();
                if (result.Count() >= options.ContextFetchNumber)
                {
                    returnFlag = true;
                    break;
                }
                if (!IsSystemMessage(item, ref date))
                {
                    using var mat = this._Client.OcrEngee.GetMatFromElement(item);
                    var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                    if (nickName == null)
                    {
                        var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, this._Client.MainWindow);
                        nickName = _GetNickNameFromOCRCore__(item, ratio, CancellationToken.None, title, automation, direction, options, this._Client.MainWindow);
                    }
                    var normalMessage = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, win);
                    if (normalMessage != null)
                    {
                        pageResult.Add(normalMessage);
                        allThisPageMessageList.add(normalMessage);
                    }
                }
                else
                {
                    //系统消息处理
                    var systemMessage = _GetSystemMessageFromElement(item, ref date, title);
                    if (systemMessage != null)
                    {
                        if (WeChatTimeParser.ParseWeChatTime(item.Name) == null)  //不为日期,日期不包含在消息列表中
                        {
                            pageResult.Add(systemMessage);
                        }
                        allThisPageMessageList.add(systemMessage);
                    }
                }
            }

            if (pageResult.Count > 0)
            {
                pageResult.Reverse();
                result.InsertRange(0, pageResult);
                returnFlag = _CheckReturnCondition_(result, title);
            }

            return returnFlag;
        }
        /// <summary>
        /// 判断首页返回条件
        /// </summary>
        private bool _CheckFirstReturnCondition(List<string> oldSnapList, List<SimpleMessageBubble> result, HeaderInfo info)
        {
            if (oldSnapList.Count() > 0)
            {
                var lastItems = MessageCacheHelper.GetLastMessages(info.Title, result.Count(), this._Client.Alias);
                var comparer = new MessageComparer();
                return LcsHelper.IsSameIncludeSequence(lastItems, result, comparer);
            }
            return false;
        }
        private bool _CheckReturnCondition_(List<SimpleMessageBubble> result, HeaderInfo info)
        {
            if (result.Count() > 0)
            {
                var lastItems = MessageCacheHelper.GetLastMessages(info.Title, result.Count(), this._Client.Alias);
                var comparer = new MessageComparer();
                return LcsHelper.IsSameIncludeSequence(lastItems, result, comparer);
            }
            return false;
        }

        private void __GetNewSnapshot__(AutomationElement root, List<string> newSnapshot, CancellationToken token)
        {
            var height = (int)(73 * ratio);        //考虑dpi影响,为群消息的最小高度
            var height1 = (int)(56 * ratio);       //考虑右边是自己的情况
            var items = root.AsListBox().Items;
            foreach (var item in items)
            {
                token.ThrowIfCancellationRequested();
                if (item.ClassName.equals("mmui::ChatItemView") || item.ClassName.equals("mmui::ChatSystemInfoItemView") || item.ClassName.equals("mmui::ChatFolderItemView"))
                {
                    //系统消息,直接加入
                    newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
                }
                else
                {
                    if (businessMessageClassNames.Contains(item.Properties.ClassName))
                    {
                        var realHeight = height;
                        if (item.BoundingRectangle.Height <= height)
                        {
                            realHeight = item.BoundingRectangle.Height;
                        }
                        if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + realHeight <= root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                        {
                            newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
                        }
                        else
                        {
                            realHeight = height1;
                            if (item.BoundingRectangle.Height <= height1)
                            {
                                realHeight = item.BoundingRectangle.Height;
                            }
                            if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + realHeight <= root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1)
                            {
                                newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"错误：终于发现一个以前不存在的消息ClassName={item.Properties.ClassName},请告之作者！");
                    }
                }
            }
        }

        private MessageCheckpointItem _GetBottomElement(ListBox listBox, AutomationElement root)
        {
            MessageCheckpointItem result = null;
            if (listBox.Items.LastOrDefault() != null)
            {
                result = new MessageCheckpointItem();
                result.Element = listBox.Items.LastOrDefault();
                result.CurrentSnapTop = result.Element != null ? result.Element.BoundingRectangle.Y : 0;
                result.RunTimeId = result.Element.Properties.RuntimeId.Value;
            }
            return result;
        }

        // private void _DoCallBackAction(string title, List<SimpleMessageBubble> newMessages, Action<MessageContext> callBack)
        // {
        //     if (callBack == null)
        //         return;
        //     if (newMessages == null || newMessages.Count == 0)
        //         return;
        //     var historyList = MessageCacheHelper.GetLastMessages(title, WeAutomation.Config.MaxHistoryMessageFetchNumber, this._Client.WxId);
        //     MessageContext context = new MessageContext(newMessages, historyList, this._Client.ChatContent.Sender, this._Client, this._Client.Factory, this.serviceProvider, this._Client.NickName, title);
        //     callBack.Invoke(context);
        // }




        //仅用于测试.
        public async Task FetchFriendInfo(string who)
        {
            await this._Client.SearchFriend(who);
            RandomWait.Wait(500, 1200);
            var chatInfo = await this._Client.ChatContent.ChatHeader.GetTitle();
            await WeChatInvoker.Call(_FetchFriendInfo, CancellationToken.None, chatInfo);
        }

        public async Task FetchFriendInfoPure(string who, HeaderInfo chatInfo)
        {
            await WeChatInvoker.Call(_FetchFriendInfo, CancellationToken.None, chatInfo);
        }
        /// <summary>
        /// 点击聊天窗口右上角的“聊天信息”按钮，进入好友信息界面获取好友信息，获取完后会自动关闭好友信息界面
        /// </summary>
        /// <param name="automation"></param>
        /// <param name="token"></param>
        /// <param name="headerInfo"></param>
        internal void _FetchFriendInfo(UIA3Automation automation, CancellationToken token, HeaderInfo headerInfo)
        {
            //首先判断在缓存中有没有此用户
            var result = this._Client.GetFriendFromCache(headerInfo.Title);
            token.ThrowIfCancellationRequested();
            if (result != null)
            {
                return;
            }
            //如果没有此用户，则取用户wxid
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='聊天信息'][@AutomationId='content_view.top_content_view.title_h_view.right_v_view.right_content_h_view.right_content_v_view.right_ui_.more_button']";
            var buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!buttonRetry.Success)
                return;
            var button = buttonRetry.Result; //点击侧边栏按钮.
            var point = button.BoundingRectangle.Center();
            var point1 = point.Confusion(5, 3);
            Mouse.Position = point1;
            RandomWait.Wait(100, 300);
            point1 = point.Confusion(5, 3);
            SupperMouseKey.MoveTo(point1);
            RandomWait.Wait(200, 800);
            SupperMouseKey.LeftClick();
            //点击后找到右边侧图标
            path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group[2]/Group[2]/Group/Group/Group/Group[1]/Button[1]";
            buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!buttonRetry.Success)
                return;
            token.ThrowIfCancellationRequested();
            button = buttonRetry.Result;
            button = button.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByClassName("mmui::ContactHeadView")));
            point = button.BoundingRectangle.Center();
            point1 = point.Confusion(5, 5);
            Mouse.Position = point1;
            RandomWait.Wait(100, 300);
            point1 = point.Confusion(5, 3);
            SupperMouseKey.MoveTo(point1);
            RandomWait.Wait(200, 800);
            SupperMouseKey.LeftClick();
            token.ThrowIfCancellationRequested();

            //获取信息
            FriendInfo friendInfo = GetFriendInfoCore(automation);
            if (friendInfo != null)
            {
                this._Client.AddOrUpdateFriendFromCache(friendInfo);
            }
            token.ThrowIfCancellationRequested();
            this._Client.ChatContent.Sender.FocuseSenderCore(automation);  //获取完信息后把焦点切回消息输入框
            RandomWait.Wait(100, 300);

            //再次检查一下是否关闭侧边栏，如果没有关闭，则关闭一下，避免影响下一次获取信息
            var addButtonRetry = Retry.WhileNotNull(() => this._Client.MainWindow.FindFirstByXPath("/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/Group/Button[@Name='添加'][@AutomationId='single_chat_member_add'][@ClassName='mmui::ChatMemberActionView']"), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (!addButtonRetry.Result)
            {
                this._Client.ChatContent.Sender.FocuseSenderCore(automation);  //获取完信息后把焦点切回消息输入框
                RandomWait.Wait(100, 300);
            }
        }

        /// <summary>
        /// 单纯获取好友信息
        /// </summary>
        /// <param name="automation"></param>
        /// <returns></returns>
        internal FriendInfo GetFriendInfoCore(UIA3Automation automation)
        {
            FriendInfo friendInfo = new FriendInfo();
            var winRetry = Retry.WhileNull(() => automation.GetDesktop().FindFirstChild(cf => cf.ByName("Weixin").And(cf.ByClassName("mmui::ProfileUniquePop").And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!winRetry.Success)
                return null;
            var win = winRetry.Result.AsWindow();
            var path = "/Group/Group/Group/Group/Group/Group/Group/Text[@Name='微信号：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            var textRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!textRetry.Success)
                return null;
            //微信号
            var text = textRetry.Result.GetSibling(1);
            friendInfo.WxId = text.Name;
            //名称、昵称、备注
            path = "/Group/Group/Group/Group/Group/Group/Text[@AutomationId='right_v_view.nickname_button_view.display_name_text']";
            text = win.FindFirstByXPath(path);
            friendInfo.NickName = text.Name;
            friendInfo.MemoName = text.Name;
            //昵称
            path = "/Group/Group/Group/Group/Group/Group/Group/Text[@Name='昵称：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            text = win.FindFirstByXPath(path)?.GetSibling(1);
            if (text != null)
            {
                friendInfo.NickName = text.Name;
            }
            //地区
            path = "/Group/Group/Group/Group/Group/Group/Group/Text[@Name='地区：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            text = win.FindFirstByXPath(path)?.GetSibling(1);
            if (text != null)
            {
                friendInfo.Area = text.Name;
            }
            //共同群聊
            path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button/Group/Group/Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.chatroom_intersection.value_normal_view.content_view.value_reader_view.value_reader_'][@ClassName='mmui::ProfileTextView']";
            text = win.FindFirstByXPath(path);
            if (text != null)
            {
                friendInfo.SameGroupNumber = text.Name;
            }
            //签名
            path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button/Group/Group/Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.sign.value_normal_view.content_view.value_reader_view.value_reader_'][@ClassName='mmui::ProfileTextView']";
            text = win.FindFirstByXPath(path);
            if (text != null)
            {
                friendInfo.Signature = text.Name;
            }
            //来源与添加时间略掉，因为没啥意义.
            //标签
            path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button/Group/Group/Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.tag_line.value_normal_view.content_view.value_reader_view.value_reader_'][@ClassName='mmui::ProfileTextView']";
            text = win.FindFirstByXPath(path);
            if (text != null)
            {
                string[] tags = text.Name.Split(',', StringSplitOptions.RemoveEmptyEntries);
                friendInfo.Lable = tags.ToList();
            }
            path = "/Group/Group/Group/Group/Group/Button[@AutomationId='head_image_v_view.head_view_'][@ClassName='mmui::ContactHeadView']";
            var button = win.FindFirstByXPath(path);
            if (button != null)
            {
                path = Path.Combine(AppContext.BaseDirectory, "Avator", $"{friendInfo.WxId}.png");
                button.CaptureToFile(path);
                friendInfo.AvatarPath = path;
            }
            win?.Close();
            return friendInfo;
        }
        private bool _ProcessClickBack()
        {
            var backButtonRetry = Retry.WhileNull(() => _Client.MainWindow.FindFirstDescendant(cf => cf.ByName("返回").And(cf.ByAutomationId("button")).And(cf.ByClassName("mmui::ChatBackwardView")).And(cf.ByControlType(ControlType.Button).And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))), timeout: TimeSpan.FromSeconds(0.5), interval: TimeSpan.FromMilliseconds(100));
            if (backButtonRetry.Success)
            {
                var button = backButtonRetry.Result;
                button.WaitUntilClickable();
                button.Click();
                RandomWait.Wait(300, 600);
                return true;
            }
            return false;
        }

        #endregion
        private bool _CheckRuntime(List<TimeOnlyRange> timeList)
        {
            var now = TimeOnly.FromDateTime(DateTime.Now);

            foreach (var time in timeList)
            {
                if (_IsInRange(now, time.StarTime, time.EndTime))
                    return true;
            }

            return false;
        }

        private bool _IsInRange(TimeOnly now, TimeOnly start, TimeOnly end)
        {
            // 普通区间
            if (start <= end)
            {
                return now >= start && now <= end;
            }

            // 跨天区间
            return now >= start || now <= end;
        }

        #region 释放器
        ~MessageMonitor()
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
                messageGlobalCts?.Cancel();
                if (messageRunningTask != null)
                {
                    if (!messageRunningTask.IsCompleted)
                    {
                        try
                        {
                            messageRunningTask.Wait(TimeSpan.FromSeconds(3));
                        }
                        catch (AggregateException) { }
                        catch (Exception) { }
                    }
                }
                //系统消息监听 线程 释放
                foreach (var subThread in _SystemMonitors)
                {
                    try
                    {
                        if (subThread != null && subThread.IsAlive)
                            subThread.Join(TimeSpan.FromSeconds(3));
                    }
                    catch (AggregateException) { }
                    catch (Exception) { }
                }
                // 弹窗式监听线程释放
                try
                {
                    if (subWinThread != null && subWinThread.IsAlive)
                        subWinThread.Join(TimeSpan.FromSeconds(3));
                }
                catch (AggregateException) { }
                catch (Exception) { }
                subWinLinkedCts?.Dispose();
                subWinLinkedCts = null;

                systemLinkedCts?.Dispose();
                systemLinkedCts = null;

                messageGlobalCts?.Dispose();
                messageRunningTask?.Dispose();
                Interlocked.CompareExchange(ref this._IsContinue, 1, 0);  //如果有暂停状态，则取消暂停状态.
            }

        }
        #endregion

    }
}



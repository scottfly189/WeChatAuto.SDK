using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Enums;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Extentions;
using WeChatAuto.Models;
using WeChatAuto.Services;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 会话列表切换监听器
    /// </summary>
    public class ConversationChangeListener : IDisposable, IAsyncDisposable
    {
        private UIThreadInvoker uiThreadInvoker;
        private Window _Window;
        private IServiceProvider serviceProvider;
        private WeChatMainWindow _MainChat;
        private Task listenerTask;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private CancellationTokenSource userCts;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);  //信号量，用以实现暂停，继续业务逻辑.
        private AutoLogger<ConversationChangeListener> _Logger;
        private volatile string oldTitle = "";  //原来的title.

        public ConversationChangeListener(UIThreadInvoker uIThreadInvoker, Window window, WeChatMainWindow ownerMainChat, IServiceProvider serviceProvider)
        {
            this.uiThreadInvoker = uIThreadInvoker;
            this._Window = window;
            this._MainChat = ownerMainChat;
            this.serviceProvider = serviceProvider;
            _Logger = serviceProvider.GetRequiredService<AutoLogger<ConversationChangeListener>>();
        }
        /// <summary>
        /// 开启会话列表切换监听器
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="syncContext">SynchronizationContext对象</param>
        /// <returns></returns>
        public void Star(Action<ChatContext, CancellationToken> callBack, SynchronizationContext syncContext)
        {
            if (listenerTask != null)
                return;
            cts?.Cancel();
            cts = new CancellationTokenSource();
            listenerTask = Task.Run(async () =>
            {
                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        _pauseEvent.Wait(cts.Token);
                        try
                        {
                            //业务方法
                            await _CheckConversionChange(callBack, cts.Token, syncContext);
                        }
                        catch (OperationCanceledException)
                        {
                            //do nothing.
                        }
                        catch (Exception ex)
                        {
                            _Logger.Error(ex.ToString());
                        }
                        //暂停监测时间.
                        if (WeAutomation.Config.ConversationChangeListenerInterval <= 0)
                            WeAutomation.Config.ConversationChangeListenerInterval = 3;
                        await Task.Delay(WeAutomation.Config.ConversationChangeListenerInterval * 1000,
                                cts.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    //do nothing.
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.ToString());
                }
            }, cts.Token);
        }

        private async Task _CheckConversionChange(Action<ChatContext, CancellationToken> callBack, CancellationToken token, SynchronizationContext syncContext)
        {
            var exceptTitle = new List<string> { "订阅号", "腾讯新闻", "服务通知", "微信团队", "文件传输助手", "其他" };
            token.ThrowIfCancellationRequested();
            await uiThreadInvoker.Run(automation =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    var navigateRoot = _Window.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar).And(cf.ByLocalizedControlType("工具栏"))
                    .And(cf.ByName("导航")));
                    var conversionRootPane = navigateRoot.GetSibling(1);
                    var list = conversionRootPane.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("会话")));
                    if (list == null)
                    {
                        list = conversionRootPane.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByName("折叠的群聊")));
                    }
                    if (list == null)
                        return;
                    var listItems = list.FindAllChildren().ToList().Select(u => u.AsListBoxItem()).ToList();
                    var listItem = listItems.FirstOrDefault(u => u.Patterns.SelectionItem.IsSupported && u.Patterns.SelectionItem.Pattern.IsSelected.Value);
                    if (listItem == null)
                        return;
                    var checkListItemLabel = listItem.Name.Trim();
                    if (checkListItemLabel.Equals(this.oldTitle))   //如果标签没有变化,则不触发下面的事件
                        return;
                    userCts?.Cancel();
                    this.oldTitle = checkListItemLabel;
                    token.ThrowIfCancellationRequested();
                    //如果标签名在排除列表中，则退出.
                    var item = exceptTitle.Find(u => checkListItemLabel.Contains(u));
                    if (item != null)
                        return;
                    //排除公众号
                    var contentPaneRoot = conversionRootPane.GetSibling(1);
                    var mpButton = contentPaneRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("公众号主页")));
                    if (mpButton != null)
                        return;
                    var chatButton = contentPaneRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("聊天信息")));
                    if (chatButton == null)
                        return;

                    //触发事件.
                    token.ThrowIfCancellationRequested();
                    var listItemLabel = checkListItemLabel.EndsWith("已置顶") ? checkListItemLabel.Substring(0, checkListItemLabel.IndexOf("已置顶")).Trim() : checkListItemLabel.Trim();
                    var texts = contentPaneRoot.FindAllDescendants(cf => cf.ByControlType(ControlType.Text)).ToList();
                    var textElement = texts.Find(u => u.Name.Contains(listItemLabel));
                    if (textElement == null)
                    {
                        return;
                    }
                    userCts = new CancellationTokenSource();
                    var name = textElement.Name.Trim();
                    HeaderInfo info = new HeaderInfo()
                    {
                        Title = "",
                        HeaderType = ChatType.其他,
                    };
                    //分出是群聊还是个人
                    var pattern = @"(.+)\s*\(([\d]+)\)$";
                    var match = Regex.Match(name, pattern);
                    if (match.Success)
                    {
                        info.HeaderType = ChatType.群聊;
                        info.Title = match.Groups[1].Value.Trim();
                        info.ChatNumber = int.Parse(match.Groups[2].Value.Trim());
                    }
                    else
                    {
                        //如果是个人，区分出是个人微信还是企业微信
                        var checkCompanyWx = textElement.GetSibling(1);
                        if (checkCompanyWx != null)
                        {
                            if (checkCompanyWx.Name.StartsWith("@"))
                            {
                                info.HeaderType = ChatType.企业微信;
                                info.Title = name;
                            }
                        }
                        else
                        {
                            info.HeaderType = ChatType.好友;
                            info.Title = name;
                        }
                    }
                    if (info.HeaderType == ChatType.好友 ||
                        info.HeaderType == ChatType.企业微信 ||
                        info.HeaderType == ChatType.群聊)
                    {
                        ChatContext context = new ChatContext();
                        context.TitleInfo = info;
                        context.MainWindow = _MainChat;
                        context.ServiceProvider = serviceProvider;
                        if (syncContext != null)
                        {
                            syncContext.Post(_ =>
                            {
                                userCts.Token.ThrowIfCancellationRequested();
                                callBack(context, userCts.Token);
                            }, null);
                        }
                        else
                        {
                            Task.Run(() =>
                            {
                                userCts.Token.ThrowIfCancellationRequested();
                                callBack(context, userCts.Token);
                            }, userCts.Token);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    //do nothing...
                }
                catch (Exception ex)
                {
                    _Logger.Error(nameof(ConversationChangeListener) + " - " + nameof(_CheckConversionChange) + ":" + ex.ToString());
                }
            });
        }

        /// <summary>
        /// 暂停监听
        /// </summary>
        /// <returns></returns>
        public void Pause() => _pauseEvent.Reset();
        /// <summary>
        /// 恢复监听
        /// </summary>
        public void Resume() => _pauseEvent.Set();

        /// <summary>
        /// 停止监听
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            Dispose();
        }


        public void Dispose()
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        public async ValueTask DisposeAsync()
        {
            cts?.Cancel();
            if (listenerTask != null)
            {
                await Task.WhenAny(listenerTask, Task.Delay(1000));
            }

            cts?.Dispose();
            cts = null;
            listenerTask = null;
        }
    }
}
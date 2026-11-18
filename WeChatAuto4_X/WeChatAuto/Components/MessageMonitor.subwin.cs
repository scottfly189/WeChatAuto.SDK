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
using System.Text;
using Microsoft.ML.OnnxRuntime;
using WeAutoCommon.Configs;
using Emgu.CV.Ocl;
using Dm.util;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 弹窗式监听的类
    /// </summary>
    public partial class MessageMonitor : IDisposable
    {
        /// <summary>
        /// 被监听的 好友/群 字典
        /// </summary>
        private ConcurrentBag<string> fixMonitorList = new ConcurrentBag<string>();

        /// <summary>
        /// 是否启动 弹窗式 监听,以防止多次启动
        /// </summary>
        private int fixMonitorFlag = 0;
        private Func<MessageContext, Task> CallBack;
        private MessageMonitorOptions Options;
        private CancellationToken Token;
        private CancellationTokenSource subWinLinkedCts;
        private Thread subWinThread = null;
        /// <summary>
        /// 增加弹窗式监听待监听的好友、群
        /// </summary>
        /// <param name="who">好友/群的昵称</param>
        /// <returns></returns>
        public async Task AddSubWinListeningFriend(string who)
        {
            if (fixMonitorFlag == 0)
                throw new Exception("错误：请先运行 AddMessageListenerWithSubWin 方法");

            if (fixMonitorList.Contains(who))
                return;
            fixMonitorList.Add(who);
        }

        /// <summary>
        /// 从弹窗式监听中移除待监听的好友、群,并关闭打开的子窗口.
        /// </summary>
        /// <param name="who">好友/群的昵称</param>
        /// <returns></returns>
        public async Task RemoveSubWinListeningFriend(string who)
        {
            if (fixMonitorFlag == 0)
                throw new Exception("错误：请先运行 AddMessageListenerWithSubWin 方法");
            fixMonitorList = new ConcurrentBag<string>(fixMonitorList.Where(u => !u.Equals(who)));
        }

        /// <summary>
        /// 弹窗式消息监听器 - 会弹窗 - 消息监听器
        /// 注意：1.弹窗式消息监听 也能实现系统消息监听，所以如果使用了 弹窗式 监听，就没有必要启用系统消息监听，请参见<seealso cref="AddGroupSystemMessageListener"/>
        /// 2. 最好不要与开放式监听、轮询式消息拉取混合，弹窗式消息监听、开放式消息监听、轮询式消息拉取择其一即可.
        /// </summary>
        /// <param name="nickNames">待监听的好友/群名</param>
        /// <param name="callBack">消息到达时的回调</param>
        /// <param name="options">监听选项，具体请参考<seealso cref="MessageMonitorOptions"/></param>
        /// <param name="userToken">用户取消token</param>
        /// <returns></returns>
        public async Task AddMessageListenerWithSubWin(OneOf<string, List<string>, string[]> nickNames, Func<MessageContext, Task> callBack, MessageMonitorOptions options, CancellationToken userToken)
        {
            if (Interlocked.CompareExchange(ref this.fixMonitorFlag, 1, 0) == 1)
            {
                //防止多次启动.
                return;
            }
            var token = CancellationToken.None;
            try
            {
                if (userToken != default)
                {
                    subWinLinkedCts = CancellationTokenSource.CreateLinkedTokenSource(messageGlobalCts.Token, userToken);
                    token = subWinLinkedCts.Token;
                }
                else
                {
                    token = messageGlobalCts.Token;
                }

                var subWinTitles = nickNames.IsT0 ? new List<string>() { nickNames.AsT0 } : nickNames.IsT1 ? nickNames.AsT1 : nickNames.AsT2.ToList();
                this.fixMonitorList = new ConcurrentBag<string>(subWinTitles);

                if (options == null)
                    options = new MessageMonitorOptions();

                this.CallBack = callBack;
                this.Options = options;
                this.Token = token;    //这里的token是关联了 主控制token 与 用户token的联合token.

                subWinThread = new Thread(() => __FixedMonitorRunning().ConfigureAwait(false).GetAwaiter().GetResult());

                subWinThread.Priority = ThreadPriority.Lowest;
                subWinThread.Name = "_FixedMontitorRun_";
                subWinThread.IsBackground = true;
                subWinThread.SetApartmentState(ApartmentState.STA);
                subWinThread.Start();
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                _Logger.Error($"固定式监听消息发生错误 {ex.ToString()}");
            }
        }

        //线程运行时
        public async Task __FixedMonitorRunning()
        {
            try
            {
                Dictionary<string, BaseMessageCheckpointItem> checkPointList = new Dictionary<string, BaseMessageCheckpointItem>();
                using UIA3Automation subWinAutomation = new UIA3Automation();   // 弹出窗口的 UIA3Automation
                try
                {
                    while (!this.Token.IsCancellationRequested)
                    {
                        if (Volatile.Read(ref this._IsContinue) != 1)
                        {
                            RandomWait.Wait(100, 800);  //循环检查一轮，休眠100..800毫秒，准备下一轮
                            continue;
                        }
                        foreach (var who in this.fixMonitorList)
                        {
                            this.Token.ThrowIfCancellationRequested();
                            try
                            {
                                var title = await _CheckOpenState(who);            //检查打开状态，如果没有打开，则打开它
                                if (!checkPointList.Keys.Contains(who))
                                {
                                    await _InitializeWithSubWin(who, checkPointList, title);
                                }
                                else
                                {
                                    await _CheckDiffMessage(who, checkPointList, subWinAutomation, title);
                                }
                            }
                            catch (OperationCanceledException)
                            {
                                throw;
                            }
                            catch (Exception ex)
                            {
                                _Logger.Error($"弹窗式监听处理好友/群 [{who}] 时出错，已跳过：{ex.ToString()}");
                            }
                        }
                        RandomWait.Wait(100, 800);  //循环检查一轮，休眠100..800毫秒，准备下一轮
                    }
                }
                finally
                {
                    //先释放关联的取消源，再恢复标志，避免重启时复用到已释放的取消源.
                    var cts = subWinLinkedCts;
                    subWinLinkedCts = null;
                    cts?.Dispose();
                    //恢复到初始值，以便可以重新发起消息监听.
                    Interlocked.CompareExchange(ref this.fixMonitorFlag, 0, 1);
                }
            }
            catch (OperationCanceledException)
            {
                // do nothing.
            }
            catch (Exception ex)
            {
                _Logger.Error($"弹窗式监听消息发生错误:{ex.ToString()}");
            }
        }

        private async Task<HeaderInfo> _CheckOpenState(string who)
        {
            return await SearchAndOpen(who, this.Options);
        }

        private async Task _InitializeWithSubWin(string who, Dictionary<string, BaseMessageCheckpointItem> checkPointList, HeaderInfo title)
        {
            this.Token.ThrowIfCancellationRequested();
            //初始化.
            var result = await WeChatInvoker.Call(_InitializeWithSubWinCore, who, checkPointList, title);
            if (result == null)
            {
                //子窗口未就绪或会话为空，本轮跳过，下一轮重试.
                return;
            }
            //做去重操作，并保存入数据库
            __ProcessThisSessionDate__(result, title, this.Token);
            var diffList = __SaveToCacheAndDb__(result, title, this.Options);
            if (this.CallBack != null && diffList.Count() > 0)
            {
                var historyList = MessageCacheHelper.GetLastMessages(title.Title, WeAutomation.Config.MaxHistoryMessageFetchNumber, this._Client.Alias);
                MessageContext sessionContext = new MessageContext(diffList, historyList, this._Client.ChatContent.Sender, this._Client, this._Client.Factory, this.serviceProvider, this._Client.NickName, title.Title);

                await this.CallBack.Invoke(sessionContext);
            }
            this._Client.MainWindow.Focus();
        }
        /// <summary>
        /// 首次初始化操作
        /// </summary>
        private List<SimpleMessageBubble> _InitializeWithSubWinCore(UIA3Automation automation, string who, Dictionary<string, BaseMessageCheckpointItem> checkPointList, HeaderInfo title)
        {
            this.Token.ThrowIfCancellationRequested();
            var subWin = __GetSubWinWithThread__(automation, who);
            if (subWin == null)
                return null;
            subWin.Focus();
            var root = __GoBottomWithSubWin__(automation, subWin, true);   //到最底端
            _QuickGoUpByNumber(Options.ContextFetchNumber, root, subWin);   //到上部固定数量的记录
            var result = _FetchToBottomWithSubwin(root, checkPointList, who, subWin, automation, title);
            return result;
        }
        /// <summary>
        /// 滚动到未尾
        /// </summary>
        private AutomationElement __GoBottomWithSubWin__(UIA3Automation automation, Window subWin, bool isQuickKey = false)
        {
            this.Token.ThrowIfCancellationRequested();
            var path = UITreeGlobal.SubWinMessageRootPath;
            var root = subWin.FindFirstByXPath(path);
            subWin.Focus();
            __ClickGoBottomButton__(subWin, root);
            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return root;
            // 快速滚动到底部
            if (isQuickKey)
                __QuickGoBottom__(subWin, root);
            //重试往下滚动
            var index = 0;
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            IEnumerable<string> oldSnapshot = new List<string>();
            while (index < 3)
            {
                this.Token.ThrowIfCancellationRequested();
                subWin.Focus();
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
                subWin.Focus();
                MouseScrollHelper.DownStep(safePoint, 3);
            }

            return root;
        }
        /// <summary>
        /// 如果下面有按钮，则点击向下按钮.
        /// 由于按钮可能出现多次，所以循环点击.
        /// </summary>
        private void __ClickGoBottomButton__(Window subWin, AutomationElement root)
        {
            var compareTop = root.BoundingRectangle.Y + (root.BoundingRectangle.Height / 2);
            var button = root.GetSibling(1);
            //如果有按钮，则先点击按钮
            while (button != null && button.ClassName.Equals("mmui::UnreadBarView") && button.ControlType == ControlType.Button && button.BoundingRectangle.Y + button.BoundingRectangle.Height > compareTop)
            {
                subWin.Focus();
                this.Token.ThrowIfCancellationRequested();
                button.Click();
                RandomWait.Wait(50, 200);

                button = root.GetSibling(1);
                RandomWait.Wait(50, 200);
            }
        }

        private void __QuickGoBottom__(Window win, AutomationElement root)
        {
            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return;
            win.Focus();
            var testLeft = (int)((int)(20 * (this.ratio)) / 2);
            var testTop = (int)((int)(24 * this.ratio));
            var firstVisibleElement = listBox.Items.FirstOrDefault(u => u.BoundingRectangle.Y >= root.BoundingRectangle.Y);
            if (firstVisibleElement != null)
            {
                var point = new Point(firstVisibleElement.BoundingRectangle.X + testLeft,
                                      firstVisibleElement.BoundingRectangle.Y + testTop);
                Mouse.Position = point;
                RandomWait.Wait(100, 300);
                SupperMouseKey.LeftClick();
                RandomWait.Wait(100, 300);
                SupperMouseKey.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.END);
                RandomWait.Wait(200, 600);
            }
        }

        //快速滚动至固定数量的消息.
        private void _QuickGoUpByNumber(int contextFetchNumber, AutomationElement root, Window subWin)
        {
            this.Token.ThrowIfCancellationRequested();

            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return;

            var count = 0;
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            // 要考虑长文本的情况
            var index = 0;
            List<string> oldSnapshot = new List<string>();
            while (index < 3)
            {
                if (count >= contextFetchNumber)
                    break;

                this.Token.ThrowIfCancellationRequested();
                subWin.Focus();

                var newSnapshot = new List<string>();
                __GetNewSnapshot__(root, newSnapshot, this.Token);
                var exceptList = newSnapshot.Except(oldSnapshot).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapshot = newSnapshot;
                    int addNumber = _GetAddNumber(listBox, exceptList);
                    count = count + addNumber;
                    if (count >= contextFetchNumber)
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

                MouseScrollHelper.UpStep(safePoint.Confusion(3, 6), 3);  //向上滚动.
            }
        }

        private int _GetAddNumber(ListBox listBox, List<string> exceptList)
        {
            var items = listBox.Items;
            var elements = items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
            var count = elements.Where(u => businessMessageClassNames.Contains(u.Properties.ClassName)).Count();
            return count;
        }

        /// <summary>
        /// 从上往下ocr,并且保存diff去数据库中,并且得到CheckpointList.
        /// </summary>
        private List<SimpleMessageBubble> _FetchToBottomWithSubwin(AutomationElement root, Dictionary<string, BaseMessageCheckpointItem> checkPointList, string who, Window subWin, UIA3Automation automation, HeaderInfo title)
        {
            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return null;
            this.Token.ThrowIfCancellationRequested();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            var index = 0;
            var oldSnapshot = new List<string>();
            var result = new List<SimpleMessageBubble>();  //初始化得到的聊天记录.
            DateTime date = default;
            while (index < 3)
            {
                this.Token.ThrowIfCancellationRequested();
                subWin.Focus();
                var newSnapshot = new List<string>();
                __GetNewSnapshot__(root, newSnapshot, this.Token);
                var exceptList = newSnapshot.Except(oldSnapshot).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapshot = newSnapshot;
                    _FetchThisPageMessgesFirstWithSubWin_(automation, this.Options, root, result, exceptList, title, this.Token, subWin, ref date);
                }
                else
                {
                    if (listBox.Items[listBox.Items.Count() - 1].IsElementInTree(root) &&
                        listBox.Items[listBox.Items.Count() - 1].BoundingRectangle.Y + listBox.Items[listBox.Items.Count() - 1].BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1)
                    {
                        //处理长文本
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }

                MouseScrollHelper.DownStep(safePoint.Confusion(3, 6), 3);
            }

            //组装CheckPoint
            BaseMessageCheckpointItem item = new BaseMessageCheckpointItem();
            var lastItem = listBox.Items[listBox.Items.Count() - 1];
            item.CurrentSnapTop = lastItem.BoundingRectangle.Y;
            item.RunTimeId = lastItem.Properties.RuntimeId;
            checkPointList[who] = item;

            return result;
        }

        private void _FetchThisPageMessgesFirstWithSubWin_(UIA3Automation automation, MessageMonitorOptions options, AutomationElement root, List<SimpleMessageBubble> result, List<string> exceptList, HeaderInfo title, CancellationToken token, Window subWin, ref DateTime date)
        {
            token.ThrowIfCancellationRequested();
            var pageResult = new List<SimpleMessageBubble>();

            var willProcessList = root.AsListBox().Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();

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
                subWin.Focus();
                token.ThrowIfCancellationRequested();
                if (!IsSystemMessage(item, ref date))
                {
                    using var mat = this._Client.OcrEngee.GetMatFromElement(item);
                    var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                    if (nickName == null)
                    {
                        var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, subWin);
                        nickName = _GetNickNameFromOCRCore__(item, ratio, CancellationToken.None, title, automation, direction, options, subWin);
                    }
                    var normalMessage = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, subWin);
                    if (normalMessage != null)
                    {
                        pageResult.Add(normalMessage);
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
                    }
                }
            }

            if (pageResult.Count > 0)
            {
                result.AddRange(pageResult);
            }
        }


        /// <summary>
        /// 检查checkpoint是否改变，如果改变，则重新获取最新数据.
        /// 算法:
        /// 1. 先向上找到与cache的重复记录
        /// 2. 再向下ocr
        /// 3. 然后保存最后ocr时候的checkpoint,以方便下一次进行检查
        /// 4. ocr尽量使用ocr缓存.
        /// </summary>
        private async Task _CheckDiffMessage(string who, Dictionary<string, BaseMessageCheckpointItem> checkPointList, UIA3Automation subWinAutomation, HeaderInfo title)
        {
            this.Token.ThrowIfCancellationRequested();
            var subWin = __GetSubWinWithThread__(subWinAutomation, who);
            if (subWin == null)
                return;
            var path = UITreeGlobal.SubWinMessageRootPath;
            var root = subWin.FindFirstByXPath(path);
            if (root == null)
                return;
            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return;
            var checkItem = listBox.Items[listBox.Items.Count() - 1];
            var checkPoint = checkPointList[who];
            var isChange = checkPoint.IsChange(checkItem.Properties.RuntimeId.Value, checkItem.BoundingRectangle.Y);
            if (isChange)
            {
                //如果checkpoint有变化
                var result = await WeChatInvoker.Call(_FetchNewMessagesWithSubwinLoop, checkPointList, who, title, listBox);
                if (result.Count() == 0)
                    return;
                //做去重操作，并保存入数据库
                __ProcessThisSessionDate__(result, title, this.Token);
                var diffList = __SaveToCacheAndDb__(result, title, this.Options);
                if (this.CallBack != null && diffList.Count() > 0)
                {
                    var historyList = MessageCacheHelper.GetLastMessages(title.Title, WeAutomation.Config.MaxHistoryMessageFetchNumber, this._Client.Alias);
                    MessageContext sessionContext = new MessageContext(diffList, historyList, this._Client.ChatContent.Sender, this._Client, this._Client.Factory, this.serviceProvider, this._Client.NickName, title.Title);

                    await this.CallBack.Invoke(sessionContext);
                }
            }
        }
        /// <summary>
        /// 循环获取最新消息
        /// </summary>
        private List<SimpleMessageBubble> _FetchNewMessagesWithSubwinLoop(UIA3Automation automation, Dictionary<string, BaseMessageCheckpointItem> checkPointList, string who, HeaderInfo title, ListBox listBox)
        {
            var result = new List<SimpleMessageBubble>();
            var subWin = __GetSubWinWithThread__(automation, who);
            if (subWin == null)
                return result;
            subWin.Focus();
            __GoBottomWithSubWin__(automation, subWin, false);  //到底部，准备向上翻滚.

            //先上向翻，逐个ocr识别,LCS/Diff block有相同值，则停止.
            List<SimpleMessageBubble> tmpResult = new List<SimpleMessageBubble>();
            _UpRetryMessages(subWin, who, title, listBox, automation, tmpResult);
            //然后向下翻,不漏掉任何一个消息
            _DownFetchMessages(subWin, result, who, title, listBox, checkPointList, automation);

            return result;
        }

        private void _UpRetryMessages(Window subWin, string who, HeaderInfo title, ListBox root, UIA3Automation automation, List<SimpleMessageBubble> tmpResult)
        {
            this.Token.ThrowIfCancellationRequested();
            var index = 0;
            var oldSnapList = new List<string>();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            if (root.Items.Count() == 0)
                return;
            while (index < 3)
            {
                this.Token.ThrowIfCancellationRequested();
                subWin.Focus();
                var newSnapshot = new List<string>();
                __GetNewSnapshot__(root, newSnapshot, this.Token);
                var exceptList = newSnapshot.Except(oldSnapList).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapList = newSnapshot;
                    bool exitFlag = _FetchThisPageMessgesWithSubWin_(automation, this.Options, root, exceptList, title, this.Token, subWin, tmpResult);
                    if (exitFlag)
                        break;
                }
                else
                {
                    if (root.Items[0].IsAvailable && root.Items[0].BoundingRectangle.Y < root.BoundingRectangle.Y)
                    {
                        //长文本支持.
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                MouseScrollHelper.UpStep(safePoint.Confusion(3, 6), 3);
            }
        }

        private bool _FetchThisPageMessgesWithSubWin_(UIA3Automation automation, MessageMonitorOptions options, ListBox root, List<string> exceptList, HeaderInfo title, CancellationToken token, Window subWin, List<SimpleMessageBubble> tmpResult)
        {
            token.ThrowIfCancellationRequested();
            var returnFlag = false;
            var pageResult = new List<SimpleMessageBubble>();
            var willProcessList = root.Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
            willProcessList.Reverse();  //从下面开始处理

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
            DateTime date = default;
            //先对本页进行ocr识别
            foreach (var item in willProcessList)
            {
                if (!item.IsElementInTree(root))
                    continue;
                subWin.Focus();
                token.ThrowIfCancellationRequested();
                if (!IsSystemMessage(item, ref date))
                {
                    var checkUnique = item.Name + "|" + item.Properties.RuntimeId.ToUniqueString();

                    using var mat = this._Client.OcrEngee.GetMatFromElement(item);
                    var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                    if (nickName == null)
                    {
                        var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, subWin);
                        nickName = _GetNickNameFromOCRCore__(item, ratio, CancellationToken.None, title, automation, direction, options, subWin);
                    }
                    var normalMessage = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, subWin);
                    if (normalMessage != null)
                    {
                        pageResult.Add(normalMessage);
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
                    }
                }
            }
            //判断是否退出
            if (pageResult.Count > 0)
            {
                pageResult.Reverse();
                tmpResult.InsertRange(0, pageResult);
                returnFlag = _CheckReturnCondition_(tmpResult, title);
            }

            return returnFlag;
        }

        /// <summary>
        /// 往下真实获取数据
        /// 注意： 1. 尽量从缓存中取数据.
        /// 2. 要最后修改checkpointList的记录.
        /// </summary>
        private void _DownFetchMessages(Window subWin, List<SimpleMessageBubble> result, string who, HeaderInfo title, ListBox root, Dictionary<string, BaseMessageCheckpointItem> checkPointList, UIA3Automation automation)
        {
            this.Token.ThrowIfCancellationRequested();
            var index = 0;
            var oldSnapList = new List<string>();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            if (root.Items.Count() == 0)
                return;
            DateTime date = default;
            while (index < 3)
            {
                this.Token.ThrowIfCancellationRequested();
                subWin.Focus();
                var newSnapshot = new List<string>();
                __GetNewSnapshot__(root, newSnapshot, this.Token);
                var exceptList = newSnapshot.Except(oldSnapList).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapList = newSnapshot;
                    _FetchThisPageMessagesRecycle(automation, this.Options, root, exceptList, title, this.Token, subWin, ref date, result);
                }
                else
                {
                    if (!root.Items[root.Items.Count() - 1].IsAvailable || (root.Items[root.Items.Count() - 1].BoundingRectangle.Y + root.Items[root.Items.Count() - 1].BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1))
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                MouseScrollHelper.DownStep(safePoint.Confusion(3, 6), 3);
            }
            //更新checkpoint为最新
            var lastItem = root.Items[root.Items.Count() - 1];
            BaseMessageCheckpointItem item = new BaseMessageCheckpointItem();
            item.CurrentSnapTop = lastItem.BoundingRectangle.Y;
            item.RunTimeId = lastItem.Properties.RuntimeId.Value;
            checkPointList[who] = item;
        }

        private void _FetchThisPageMessagesRecycle(UIA3Automation automation, MessageMonitorOptions options, ListBox root, List<string> exceptList, HeaderInfo title, CancellationToken token, Window subWin, ref DateTime date, List<SimpleMessageBubble> result)
        {
            token.ThrowIfCancellationRequested();
            var willProcessList = root.Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
            // willProcessList.Reverse();  //从下面开始处理

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
            //先对本页进行ocr识别
            foreach (var item in willProcessList)
            {
                if (!item.IsElementInTree(root))
                    continue;
                subWin.Focus();
                token.ThrowIfCancellationRequested();
                if (!IsSystemMessage(item, ref date))
                {
                    //全新消息.
                    using var mat = this._Client.OcrEngee.GetMatFromElement(item);
                    var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                    if (nickName == null)
                    {
                        var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, subWin);
                        nickName = _GetNickNameFromOCRCore__(item, ratio, CancellationToken.None, title, automation, direction, options, subWin);
                    }
                    var normalMessage = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, subWin);
                    if (normalMessage != null)
                    {
                        result.Add(normalMessage);
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
                            result.Add(systemMessage);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 得到每个线程的子窗口对象
        /// </summary>
        private Window __GetSubWinWithThread__(UIA3Automation automation, string who)
        {
            var desktop = automation.GetDesktop();
            var windowRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByName(who).And(cf.ByControlType(ControlType.Window)).And(cf.ByProcessId(this._Client.WeChatProcessID)).And(cf.ByClassName("mmui::ChatSingleWindow"))).AsWindow(), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));

            return windowRetry.Result;
        }
        /// <summary>
        /// 查找到好友，并检查是否需要获取wxid.
        /// </summary>
        private async Task<HeaderInfo> SearchAndOpen(string who, MessageMonitorOptions options)
        {
            return await WeChatInvoker.Call(SearchAndOpenCore, who, options);
        }

        private HeaderInfo SearchAndOpenCore(UIA3Automation automation, string who, MessageMonitorOptions options)
        {
            var checkWin = __GetSubWinWithThread__(automation, who);
            if (checkWin == null)
            {
                var findFlag = this._Client.Conversations.SearchWhoCore(automation, who);
                if (!findFlag)
                    throw new Exception($"错误：没有发现微信昵称为: {who} 的好友/群.");
                RandomWait.Wait(100, 600);
                var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);

                //是否检查wxid
                if (title.HeaderType == ChatType.好友 && options.FetchFriendInfo)
                {
                    //检查 昵称 对应的 Wxid是否在缓存中.
                    var friendInfo = this._Client.GetFriendFromCache(title.Title);
                    if (friendInfo == null)
                    {
                        //如果不在缓存中，则再获取一次.
                        _FetchFriendInfo(automation, CancellationToken.None, title);
                    }
                }
                //打开子窗口.
                var root = this._Client.Conversations.ConversationRoot;
                AutomationElement item = null;
                foreach (var subItem in root.Items)
                {
                    var splitNames = subItem.Name.Trim().Split("\n");
                    if (splitNames[0].Trim().Equals(who.Trim()))
                    {
                        item = subItem;
                    }
                }
                if (item == null)
                    throw new Exception($"错误：没有找到名字为 {who} 的会话项目！");
                this._Client.MainWindow.Focus();
                while (item.BoundingRectangle.Y < root.BoundingRectangle.Top)
                {
                    this._Client.MainWindow.Focus();
                    MouseScrollHelper.UpStep(root.BoundingRectangle.SafeRandomPoint(), 1);
                    RandomWait.Wait(100, 400);
                }
                while (item.BoundingRectangle.Y + item.BoundingRectangle.Height > root.BoundingRectangle.Top + root.BoundingRectangle.Height)
                {
                    this._Client.MainWindow.Focus();
                    MouseScrollHelper.DownStep(root.BoundingRectangle.SafeRandomPoint(), 1);
                    RandomWait.Wait(100, 400);
                }
                item.DoubleClick();   //双击，弹出子窗口
                RandomWait.Wait(300, 900);
                //居中显示
                var subWin = __GetSubWinWithThread__(automation, who);
                this._Client.MoveWinToMainCenter(subWin);
                //检查子窗口对应主窗口区域是否存在.
                this._Client.MainWindow.Focus();
                var path = UITreeGlobal.MessageChatHistory;
                var historyButtonTry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                if (!historyButtonTry.Success)
                {
                    item.Click();
                    RandomWait.Wait(50, 300);
                }
                return title;
            }

            return _GetHeaderInfoFromSubwin(checkWin, who);
        }

        /// <summary>
        /// 从弹出子窗口中获取HeaderInfo对象
        /// </summary>
        private HeaderInfo _GetHeaderInfoFromSubwin(Window checkWin, string who)
        {
            HeaderInfo headerInfo = new HeaderInfo();
            var rootPath = UITreeGlobal.SubWinTitleRootPath;
            var root = checkWin.FindFirstByXPath(rootPath);
            if (root == null)
                return headerInfo;
            var textRoot = root.FindFirstChild(cf => cf.ByAutomationId("content_view.top_content_view.title_h_view.left_v_view.left_content_v_view.left_ui_.big_title_line_h_view").And(cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text))).AsLabel();
            var fullTitle = textRoot.Name.Trim();
            var texts = textRoot.FindAllChildren(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
            bool hasHistoryButton = IsHasHistoryButton(checkWin);
            if (texts.Length == 2 && hasHistoryButton)
            {
                //群聊
                headerInfo.HeaderType = ChatType.群聊;
                headerInfo.Title = texts[0].Name.Trim();
                headerInfo.ChatNumber = int.TryParse(texts[1].Name.Trim().Substring(1, texts[1].Name.Trim().Length - 2), out var result) ? result : 0;
                return headerInfo;
            }

            //非群聊,可能是好友，也可能不是.
            if (texts.Length == 1)
            {
                headerInfo.Title = texts[0].Name.Trim();
                if (IsHasVoIpButton(checkWin) && IsHasHistoryButton(checkWin))
                {
                    //这里区分出是普通好友还是企业微信.
                    var atText = root.FindFirstDescendant(cf => cf.ByAutomationId("content_view.top_content_view.title_h_view.left_v_view.left_content_v_view.left_ui_.openim_name_view.current_chat_openim_name").And(cf.ByClassName("mmui::XTextView").And(cf.ByControlType(ControlType.Text))));
                    if (atText != null && atText.Name.StartsWith("@"))
                    {
                        headerInfo.HeaderType = ChatType.企业微信;
                        return headerInfo;
                    }
                    headerInfo.HeaderType = ChatType.好友;
                    return headerInfo;
                }
                if (headerInfo.Title.Equals("文件传输助手") && !IsHasVoIpButton(checkWin))
                {
                    headerInfo.HeaderType = ChatType.文件传输助手;
                    return headerInfo;
                }

                if (headerInfo.Title.Equals("公众号") && !IsHasVoIpButton(checkWin))
                {
                    headerInfo.HeaderType = ChatType.公众号;
                    return headerInfo;
                }

                if (headerInfo.Title.Equals("服务通知") && !IsHasVoIpButton(checkWin))
                {
                    headerInfo.HeaderType = ChatType.服务通知;
                    return headerInfo;
                }
                if (headerInfo.Title.Equals("腾讯新闻") && !IsHasVoIpButton(checkWin))
                {
                    headerInfo.HeaderType = ChatType.腾讯新闻;
                    return headerInfo;
                }
            }


            return headerInfo;
        }

        private bool IsHasHistoryButton(Window subWin)
        {
            var path = UITreeGlobal.SubWinTitleHistoryButtonPath;
            var button = subWin.FindFirstByXPath(path);
            return button == null ? false : true;
        }

        private bool IsHasVoIpButton(Window subWin)
        {
            var path = UITreeGlobal.SubWinTitleVoipButtonPath;
            var button = subWin.FindFirstByXPath(path);
            return button == null ? false : true;
        }

        //处理红包
        private void __ProcessRedEnvelope(AutomationElement item, UIA3Automation automation)
        {
            int[] runtimeId = item.Properties.RuntimeId.Value;
        }
        //处理转账
        private void __ProcessTransfer(AutomationElement item, UIA3Automation automation)
        {

        }



        private string _GetDirectionByClick(Window subWin, AutomationElement item, HeaderInfo title)
        {
            if (!title.CanTalk())
                throw new Exception($"错误：监听只能监听群聊、好友与企业微信三类，你监听的 {title.Title} 可能不在三类里面");
            var result = "right";
            var x = 0;
            var y = 0;
            if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
            {
                x = 37;
                y = 27;
            }
            if (title.HeaderType == ChatType.群聊)
            {
                x = 37;
                y = 24;
            }
            x = item.BoundingRectangle.X + (int)(x * this.ratio);
            y = item.BoundingRectangle.Y + (int)(y * this.ratio);
            Mouse.Position = new Point(x, y);
            RandomWait.Wait(100, 300);
            SupperMouseKey.LeftClick();
            RandomWait.Wait(200, 600);

            var desktop = item.Automation.GetDesktop();
            //查看是否有弹窗
            var path = $"/Window[@Name='Weixin'][@ProcessId={subWin.Properties.ProcessId.Value}][@ClassName='mmui::ProfileUniquePop']";
            var popMenuRetry = Retry.WhileNull(() => desktop.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (popMenuRetry.Success)
            {
                result = "left";
                popMenuRetry.Result.AsWindow().Close();
                RandomWait.Wait(100, 500);
            }

            return result;
        }
    }
}

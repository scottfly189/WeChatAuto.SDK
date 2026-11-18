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
using Emgu.CV;
using Dm.util;
using SqlSugar;
using RapidOCRLib.Models;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 轮询式获取消息聊天信息的类
    /// </summary>
    public partial class MessageMonitor : IDisposable
    {
        public List<string> businessMessageClassNames = new List<string>()
        {
            "mmui::ChatBubbleReferItemView",
            "mmui::ChatBubbleItemView",
            "mmui::ChatVoiceItemView",
            "mmui::ChatPersonalCardItemView",
            "mmui::ChatNoteCardItemView",
            "mmui::ChatTextItemView",
            "mmui::ChatImageGroupItemView",
            "mmui::ChatImageItemView",
        };
        /// <summary>
        /// 轮询式获取聊天信息，适用于轮询中 - 轮询式监听
        /// 应用场景： 如：隔 10-20秒获取指定好友/群的聊天记录,适用于wechat bot获取最新消息的场景
        /// </summary>
        /// <param name="who">好友/群</param>
        /// <param name="maxFetchNumber">最大获取记录数</param>
        /// <param name="IsIncludeSystemMessage">是否包含系统消息，默认为True（默认包含），如果不需要，请设置为False</param>
        /// <param name="options">控制选项，参考<see cref="PullMessagesOptions"/></param>
        /// <returns>这里获取的消息列表为指的消息列表，需要自己进行去重等，可以用LCS Block算法去重</returns>
        public async Task<List<SimpleMessageBubble>> PullMessages(string who, int maxFetchNumber = 40, bool IsIncludeSystemMessage = true, PullMessagesOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(who))
                throw new Exception("错误：好友/群 不能为空！");

            var result = await WeChatInvoker.Call(PullMessagesCore, who, maxFetchNumber, options, IsIncludeSystemMessage);

            return result;
        }

        /// <summary>
        /// 获取微信消息核心方法
        /// </summary>
        /// <returns>微信消息列表</returns>
        private List<SimpleMessageBubble> PullMessagesCore(UIA3Automation automation, string who, int maxFetchNumber, PullMessagesOptions options, bool IsIncludeSystemMessage)
        {
            var result = new List<SimpleMessageBubble>();
            try
            {
                if (Volatile.Read(ref _IsContinue) != 1)
                {
                    //设置了暂停.
                    return result;
                }

                if (options == null)
                    options = new PullMessagesOptions();
                //首先定位who
                this._Client.Conversations.SearchWhoCore(automation, who);
                var title = this._Client.ChatContent.ChatHeader.GetTitleCore(automation);
                var path = UITreeGlobal.MessageRootPath;
                var rootRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (rootRetry.Success)
                {
                    var token = messageGlobalCts.Token;
                    token.ThrowIfCancellationRequested();
                    var root = rootRetry.Result.AsListBox();
                    if (title.HeaderType.equals(ChatType.好友))
                    {
                        //如果是企业微信或者普通好友聊天，根据 options的设置获取wxid.
                        if (options.FetchFriendInfo)
                        {
                            //检查 昵称 对应的 Wxid是否在缓存中.
                            var friendInfo = this._Client.GetFriendFromCache(title.Title);
                            if (friendInfo == null)
                            {
                                //如果不在缓存中，则再获取一次.
                                _FetchFriendInfo(automation, token, title);
                            }
                        }
                    }
                    //确认至最底部
                    __GoBottom__(automation, options, root, token);
                    //往上查找至指定数量的消息
                    var allThisSessionMessageList = new List<SimpleMessageBubble>();
                    result = _UpScrollAndFetchMessages(automation, maxFetchNumber, options, root, title, IsIncludeSystemMessage, token, allThisSessionMessageList);
                    __FixeThisSessionDate__(result, allThisSessionMessageList);
                    __ProcessThisSessionDate__(result, title, token);  //处理日期问题
                    __SaveToCacheAndDbWithPull__(result, title, options);  //保存消息列表缓存中
                    System.Diagnostics.Debug.WriteLine($"===== 来自 {title.Title} 的消息 ======");
                    foreach (var item in result)
                    {
                        token.ThrowIfCancellationRequested();
                        System.Diagnostics.Debug.WriteLine(item.ToString());
                    }
                    System.Diagnostics.Debug.WriteLine($"共有记录:{result.Count()} 条,其中系统消息:{result.Where(u => u.MessageType == MessageType.系统).Count()}条,非系统消息:{result.Where(u => u.MessageType != MessageType.系统).Count()}条");
                    System.Diagnostics.Debug.WriteLine($"===== 结束输出 {title.Title} 的消息 ======");

                    return result;
                }
                throw new Exception("错误：可能此版本的微信的UI Tree有变化，请联系作者！");
            }
            catch (OperationCanceledException)
            {
                // 正常退出
                return result;
            }
            catch (Exception ex)
            {
                _Logger.Error($"错误: 获取最新消息列表时出错，错误原因:{ex.toString()}");
                throw;
            }
        }
        /// <summary>
        /// pull模式的保存缓存与数据库专用方法.
        /// </summary>
        private void __SaveToCacheAndDbWithPull__(List<SimpleMessageBubble> result, HeaderInfo info, OneOf<PullMessagesOptions, MessageMonitorOptions> options)
        {
            List<SimpleMessageBubble> oldOriList = MessageCacheHelper.GetLastMessages(info.Title, result.Count(), this._Client.Alias);
            var diffs = LcsHelper.Diff<SimpleMessageBubble>(oldOriList, result, new MessageComparer());
            var diffsList = new List<SimpleMessageBubble>();
            System.Diagnostics.Debug.WriteLine($"===== 差异比较 ======");
            foreach (var item in diffs)
            {
                System.Diagnostics.Debug.WriteLine($"{item.Type,-6}  {item.Value}");
            }
            System.Diagnostics.Debug.WriteLine($"===== 结束本次差异比较 ======");
            diffsList = LcsHelper.GetNewItems(oldOriList, result, new MessageComparer());
            MessageCacheHelper.AddMessageCaches(info.Title, diffsList, this._Client.Alias);
            __SaveToDbCore__(options, diffsList, info);
            //测试差异增加
            System.Diagnostics.Debug.WriteLine($"===== 差异消息: {info.Title} 的消息 ======");
            foreach (var item in diffsList)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
            System.Diagnostics.Debug.WriteLine($"共有记录:{diffsList.Count()} 条,其中系统消息:{diffsList.Where(u => u.MessageType == MessageType.系统).Count()}条,非系统消息:{diffsList.Where(u => u.MessageType != MessageType.系统).Count()}条");
            System.Diagnostics.Debug.WriteLine($"===== 结束输出差异 {info.Title} 的消息 ======");
        }

        private void __SaveToDbCore__(OneOf<PullMessagesOptions, MessageMonitorOptions> options, List<SimpleMessageBubble> diffsList, HeaderInfo info)
        {
            try
            {
                // 写库统一走全局 WeChatConfig，监听 Options 不再各自带连接串
                var connectionString = WeAutomation.Config.ConnectionString;
                SqlSugar.DbType type = WeAutomation.Config.DbTppe;

                if (!string.IsNullOrWhiteSpace(connectionString) && (type != DbType.Custom))
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig
                    {
                        ConnectionString = connectionString,
                        DbType = type,
                        IsAutoCloseConnection = true,
                    });
                    db.DbMaintenance.CreateDatabase();
                    db.CodeFirst.InitTables<WeChatMessage>();
                    db.CodeFirst.InitTables<WeChatChatRoomMember>();
                    db.CodeFirst.InitTables<WeChatSnsPost>();
                    db.CodeFirst.InitTables<WeChatSnsMedia>();
                    db.CodeFirst.InitTables<WeChatSnsLike>();
                    db.CodeFirst.InitTables<WeChatSnsComment>();

                    foreach (var item in diffsList)
                    {
                        var row = new WeChatMessage();
                        row.Content = item.Message;
                        row.CreateTime = DateTime.Now;
                        row.FromWechat = this._Client.NickName;
                        row.FilePath = item.FilePath;
                        row.IsBotProcessed = false;
                        row.IsSelf = item.Who == "我" ? true : false;
                        row.MessageTime = item.SendDate.ToString("yyyy-MM-dd HH:mm");
                        row.MessageType = item.MessageType.toString();
                        db.Insertable<WeChatMessage>(row).ExecuteCommand();
                    }
                }
                else
                {
                    //自定义
                    if (type == DbType.Custom)
                    {
                        _ = options.IsT0 ? options.AsT0.WriteDataBaseCallback.Invoke(diffsList) :
                            options.AsT1.WriteDataBaseCallback.Invoke(diffsList);

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"错误：写数据库发生错误，错误原因:{ex.toString()}");
                _Logger.Error($"错误：写数据库发生错误，错误原因:{ex.toString()}");
            }
        }

        private void __FixeThisSessionDate__(List<SimpleMessageBubble> result, List<SimpleMessageBubble> allThisSessionMessageList)
        {
            DateTime date = default;
            var hasDate = false;
            allThisSessionMessageList.Reverse();
            foreach (var baseItem in allThisSessionMessageList)
            {
                if (baseItem.UIClassName.Equals("mmui::ChatItemView"))
                {
                    var checkDate = WeChatTimeParser.ParseWeChatTime(baseItem.Message);
                    if (checkDate is { } parsed && parsed != default)
                    {
                        date = parsed;
                        hasDate = true;
                        baseItem.SendDate = date;
                        continue;
                    }
                }

                baseItem.SendDate = hasDate ? date : default;
            }
            foreach (var item in result)
            {
                var baseItem = allThisSessionMessageList.FirstOrDefault(u => u.UIClassName.equals(item.UIClassName) && u.MessageType.equals(item.MessageType) && u.Message.equals(item.Message));
                if (baseItem is { } itemObj && itemObj != default)
                {
                    item.SendDate = baseItem.SendDate;
                }
            }
        }
        private void __ProcessThisSessionDate__(List<SimpleMessageBubble> result, HeaderInfo info, CancellationToken token)
        {
            List<SimpleMessageBubble> oldList = MessageCacheHelper.GetLastMessages(info.Title, result.Count(), this._Client.Alias);
            DateTime oldLast = default;
            if (oldList.Count() > 0)
            {
                var lastItem = oldList.FindLast(u => u.SendDate != default);
                if (lastItem != null)
                    oldLast = lastItem.SendDate;
            }
            DateTime date = default;
            var first = true;
            foreach (var item in result)
            {
                token.ThrowIfCancellationRequested();
                if (first)
                {
                    if (item.SendDate == default)
                    {
                        item.SendDate = oldLast;
                        date = oldLast;
                    }
                    else
                    {
                        date = item.SendDate;
                    }
                    first = false;
                }
                else
                {
                    if (item.SendDate == default)
                    {
                        item.SendDate = date;
                    }
                    else
                    {
                        date = item.SendDate;
                    }
                }
            }
        }

        private List<SimpleMessageBubble> _UpScrollAndFetchMessages(UIA3Automation automation, int maxFetchNumber, PullMessagesOptions options, ListBox root, HeaderInfo title, bool IsIncludeSystemMessage, CancellationToken token, List<SimpleMessageBubble> allThisSessionMessageList)
        {
            token.ThrowIfCancellationRequested();
            var result = new List<SimpleMessageBubble>();
            if (root.Items.Count() == 0)
                return result;
            var index = 0;  //设置index的目的最主要考虑可能刚开始聊天的场景，如果条数不够，但到顶了，则退出。
            var oldSnapshot = new List<string>();
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            var messageCount = 0;
            var height = (int)(73 * ratio);        //考虑dpi影响
            var height1 = (int)(56 * ratio);       //考虑右边是自己的情况
            //这里的快照要支持长文本
            while (index < options.BottomRetryNumber)
            {
                token.ThrowIfCancellationRequested();
                if (root.Items.Count() == 0)
                    break;

                if (result.Where(u => u.MessageType != MessageType.系统).Count() >= maxFetchNumber)
                    break;

                this._Client.MainWindow.Focus();

                //得到所有待处理的item
                var newSnapshot = new List<string>();
                foreach (var item in root.Items)
                {
                    token.ThrowIfCancellationRequested();
                    this._Client.MainWindow.Focus();
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
                var exceptList = newSnapshot.Except(oldSnapshot).ToList();
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapshot = newSnapshot;
                    //这里应该设置退出逻辑,超出设定的最大条数，或者去重临界点
                    var exitFlag = _FetchThisPageMessgesCore(automation, maxFetchNumber, options, root, result, exceptList, ref messageCount, title, IsIncludeSystemMessage, token, allThisSessionMessageList);
                    if (exitFlag)
                        break;
                }
                else
                {
                    //长文本支持
                    if (root.AsListBox().Items[0].IsAvailable && root.Items[0].BoundingRectangle.Y + 1 < root.BoundingRectangle.Y)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }

                MouseScrollHelper.UpStep(safePoint.Confusion(3, 6), 3);
            }

            return result;
        }

        //读取本页的数据
        //这里注意退出条件： 如果达到设定的最大值，或者去重临界点
        private bool _FetchThisPageMessgesCore(UIA3Automation automation, int maxFetchNumber, PullMessagesOptions options, ListBox root, List<SimpleMessageBubble> resultList, List<string> exceptList, ref int messageCount, HeaderInfo title, bool IsIncludeSystemMessage, CancellationToken token, List<SimpleMessageBubble> allThisSessionMessageList)
        {
            token.ThrowIfCancellationRequested();
            var result = false;
            var pageResult = new List<SimpleMessageBubble>();

            var height = (int)(73 * ratio);        //考虑dpi

            var willProcessList = root.Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
            willProcessList.Reverse(); //从下面的开始处理

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
            foreach (var item in willProcessList)
            {
                this._Client.MainWindow.Focus();
                token.ThrowIfCancellationRequested();
                if (!item.IsElementInTree(root))
                    continue;
                if (businessMessageClassNames.Contains(item.ClassName))
                {
                    var realHeight = item.BoundingRectangle.Height > height ? height : item.BoundingRectangle.Height;
                    while (!item.IsAvailable || item.BoundingRectangle.Y + realHeight < root.BoundingRectangle.Y)
                    {
                        MouseScrollHelper.UpStep(root.BoundingRectangle.SafeRandomPoint(), 3);
                    }
                }
                if (messageCount >= maxFetchNumber)
                {
                    result = true;
                    break;
                }
                if (!IsSystemMessage(item, ref date))
                {
                    //非系统消息.
                    using var mat = this._Client.OcrEngee.GetMatFromElement(item);
                    var nickName = _TryGetNickNameFromImageCache__(mat, title, ratio);
                    if (nickName == null)
                    {
                        var direction = __GetMessageDirectionCore__(leftImage, rightImage, mat, item, title, this._Client.MainWindow);
                        nickName = _GetNickNameFromOCRWithPull(item, ratio, CancellationToken.None, title, automation, direction, options, mat);
                    }
                    var normalMessage = _GetNormalMessageCore__(nickName, item, title, automation, ref date, options, root, this._Client.MainWindow);
                    if (normalMessage != null)
                    {
                        pageResult.Add(normalMessage);
                        allThisSessionMessageList.add(normalMessage);
                        messageCount++;
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
                            if (IsIncludeSystemMessage)
                            {
                                pageResult.Add(systemMessage);
                            }
                        }
                        allThisSessionMessageList.add(systemMessage);
                    }
                }
            }

            if (pageResult.Count > 0)
            {
                pageResult.Reverse();
                resultList.InsertRange(0, pageResult);
                if (options.usingMessageCacheFill)
                {
                    result = _CheckThisPageExitFromCache_(resultList, maxFetchNumber, title, options);
                    if (result)
                        messageCount = maxFetchNumber;
                }
            }

            return result;
        }

        /// <summary>
        /// 从历史数据中补足
        /// 自动取历史缓存中的数据来补足，无须重新ocr.
        /// </summary>
        private bool _CheckThisPageExitFromCache_(List<SimpleMessageBubble> resultList, int maxFetchNumber, HeaderInfo info, PullMessagesOptions options)
        {
            var lastItems = MessageCacheHelper.GetLastMessages(info.Title, maxFetchNumber, this._Client.Alias);
            var comparer = new MessageComparer();
            var flag = LcsHelper.IsSameIncludeSequence(lastItems, resultList, comparer, options.DiffCount);
            if (flag)
            {
                // 补足
                var newItems = LcsHelper.GetNewItems(lastItems, resultList, comparer, options.DiffCount);
                var diff = LcsHelper.Diff(lastItems, resultList, comparer);
                var equalItems = LcsHelper.ToDiffBlocks(diff).FindLast(u => u.Type == Utils.DiffType.Equal && u.Count >= options.DiffCount).Items;
                var fetchCount = maxFetchNumber - newItems.Where(u => u.MessageType != MessageType.系统).Count() - equalItems.Where(u => u.MessageType != MessageType.系统).Count();  //往回抓取的内容数量
                var skipCount = equalItems.Count;
                _TryFillList(resultList, fetchCount, skipCount, info, maxFetchNumber);
            }

            return flag;
        }
        private void _TryFillList(List<SimpleMessageBubble> resultList, int willFetchCount, int skipCount, HeaderInfo info, int maxFetchNumber)
        {
            // 与 _CheckThisPageExitFromCache_ 保持一致：读取同一个“最近 maxFetchNumber 条”窗口，
            // 这样 skipCount 恒 <= oriList.Count，不会出现 new Range(0, 负数) 的越界。
            var oriList = MessageCacheHelper.GetLastMessages(info.Title, maxFetchNumber, this._Client.Alias);
            var willFetchList = oriList.Take(new Range(0, oriList.Count - skipCount)).ToList();
            var index = 0;
            for (int i = willFetchList.Count - 1; i >= 0; i--)
            {
                var item = willFetchList[i];
                if (item.MessageType != MessageType.系统)
                    index++;
                if (index > willFetchCount)
                    break;
                resultList.Insert(0, item);
            }
        }

        private SimpleMessageBubble _GetSystemMessageFromElement(AutomationElement item, ref DateTime checkDate, HeaderInfo title)
        {
            SimpleMessageBubble message = new SimpleMessageBubble();
            message.Who = "系统消息";
            message.Message = item.Name;  //可能是时间，也可能是拍一拍消息、撤回消息、邀请消息等
            message.SendDate = checkDate;
            message.MessageType = MessageType.系统;
            message.UIClassName = item.ClassName;
            message.TempUniqueString = item.Properties.RuntimeId.ToUniqueString();

            if (string.IsNullOrWhiteSpace(item.Name) && item.ClassName.equals("mmui::ChatFolderItemView"))
            {
                message.Message = "展开全部";
            }

            return message;
        }

        private SimpleMessageBubble _GetNormalMessageCore__(string nickName, AutomationElement item, HeaderInfo title, UIA3Automation automation, ref DateTime checkDate, OneOf<PullMessagesOptions, MessageMonitorOptions> optionSource, AutomationElement messageListRoot, Window win)
        {
            //options
            SimpleMessageBubble message = new SimpleMessageBubble();

            message.Who = nickName;
            message.UIClassName = item.ClassName;
            message.SendDate = checkDate;
            message.TempUniqueString = item.Properties.RuntimeId.ToUniqueString();
            // 个人名片
            if (item.ClassName.Equals("mmui::ChatPersonalCardItemView"))
            {
                message.MessageType = MessageType.个人名片;
                message.Message = item.Name;
                return message;
            }
            // 笔记
            if (item.ClassName.Equals("mmui::ChatNoteCardItemView"))
            {
                message.MessageType = MessageType.笔记;
                message.Message = item.Name;
                return message;
            }
            //语音
            if (item.ClassName.Equals("mmui::ChatVoiceItemView"))
            {
                message.MessageType = MessageType.语音;
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    message.Message = "";
                }
                else
                {
                    //得到语音
                    string pattern = @"^语音\d+[""”]?秒?";
                    message.Message = Regex.Replace(item.Name, pattern, "");
                }
                return message;
            }

            if (item.ClassName.Equals("mmui::ChatBubbleItemView"))
            {
                //微信红包
                if (item.Name.EndsWith("微信红包"))
                {
                    message.Message = item.Name;
                    message.MessageType = MessageType.红包;
                    __ProcessRedEnvelope(item, automation);
                    return message;
                }
                //微信转账
                if (item.Name.EndsWith("微信转账"))
                {
                    message.Message = item.Name;
                    message.MessageType = MessageType.微信转账;
                    __ProcessTransfer(item, automation);
                    return message;
                }
                //文件
                if (item.Name.StartsWith("文件"))
                {
                    message.MessageType = MessageType.文件;
                    message.Message = item.Name;
                    __FetchFile(message, item, optionSource, automation, messageListRoot, win);
                    return message;
                }
                //聊天记录
                if (item.Name.StartsWith("聊天记录"))
                {
                    message.MessageType = MessageType.聊天记录;
                    message.Message = item.Name;
                    return message;
                }
                //位置
                if (item.Name.StartsWith("位置"))
                {
                    message.MessageType = MessageType.位置;
                    message.Message = item.Name.Substring(2);
                    return message;
                }
                //视频号
                if (item.Name.StartsWith("视频号") && !item.Name.Contains("直播中") && !item.Name.Contains("直播已结束"))
                {
                    message.MessageType = MessageType.视频号;
                    message.Message = item.Name.Substring(3);
                    return message;
                }
                if (item.Name.StartsWith("视频号") && (item.Name.Contains("直播中") || item.Name.Contains("直播已结束")))
                {
                    message.MessageType = MessageType.视频号直播;
                    message.Message = item.Name.Substring(3);
                    return message;
                }
                if (item.Name.StartsWith("小程序"))
                {
                    message.MessageType = MessageType.小程序;
                    message.Message = item.Name.Substring(3);
                    return message;
                }
                if (item.Name.StartsWith("[链接]"))
                {
                    message.MessageType = MessageType.链接;
                    message.Message = item.Name.Substring(4);
                    return message;
                }
                if (item.Name.StartsWith("语音通话"))
                {
                    message.MessageType = MessageType.语音通话;
                    message.Message = item.Name;
                    return message;
                }
            }
            if (item.ClassName.Equals("mmui::ChatImageGroupItemView"))
            {
                //图片组 暂时当图片 来处理
                //如果会员有提，再处理.
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    message.MessageType = MessageType.图片;
                    message.Message = item.Name;
                    __FetchImage(message, item, optionSource, automation, messageListRoot, win);
                    return message;
                }
            }
            if (item.ClassName.Equals("mmui::ChatBubbleReferItemView") || item.ClassName.Equals("mmui::ChatImageItemView"))
            {
                //图片
                if (item.Name.StartsWith("图片"))
                {
                    message.MessageType = MessageType.图片;
                    message.Message = item.Name;
                    __FetchImage(message, item, optionSource, automation, messageListRoot, win);
                    return message;
                }
            }
            if (item.ClassName.Equals("mmui::ChatBubbleReferItemView"))
            {
                //视频
                if (item.Name.StartsWith("视频"))
                {
                    message.MessageType = MessageType.视频;
                    message.Message = item.Name;
                    __FetchVideo(message, item, optionSource, automation, messageListRoot, win);
                    return message;
                }
                //动画表情
                if (item.Name.StartsWith("动画表情"))
                {
                    message.MessageType = MessageType.动画表情;
                    message.Message = item.Name;
                    return message;
                }
            }

            if (item.ClassName.Equals("mmui::ChatTextItemView"))
            {
                message.MessageType = MessageType.文字;
                message.Message = item.Name;
                return message;
            }

            message.MessageType = MessageType.文字;
            message.Message = item.Name;

            return message;
        }



        private void __FetchImage(SimpleMessageBubble message, AutomationElement item, OneOf<PullMessagesOptions, MessageMonitorOptions> options, UIA3Automation automation, AutomationElement messageListRoot, Window win)
        {
            if (options.IsT0)
            {
                var check = options.AsT0;
                if (!check.FetchImage)
                    return;
            }
            if (options.IsT1)
            {
                var check = options.AsT1;
                if (!check.FetchImage)
                    return;
            }

            bool flowControl = __ClickCopyImageMenu__(item, message, win);
            if (!flowControl)
            {
                return;
            }

            if (System.Windows.Clipboard.ContainsFileDropList())
            {
                var files = System.Windows.Clipboard.GetFileDropList();
                if (files.Count > 0)
                {
                    message.FilePath = files[0];
                    message.Base64Str = Convert.ToBase64String(File.ReadAllBytes(message.FilePath));
                }
            }
        }

        private void __FetchFile(SimpleMessageBubble message, AutomationElement item, OneOf<PullMessagesOptions, MessageMonitorOptions> options, UIA3Automation automation, AutomationElement messageListRoot, Window win)
        {
            if (options.IsT0)
            {
                var check = options.AsT0;
                if (!check.FetchAttachment)
                    return;
            }
            if (options.IsT1)
            {
                var check = options.AsT1;
                if (!check.FetchAttachment)
                    return;
            }

            bool flowControl = __ClickCopyMenuWithAttach__(item, message, win);
            if (!flowControl)
            {
                return;
            }

            if (System.Windows.Clipboard.ContainsFileDropList())
            {
                var files = System.Windows.Clipboard.GetFileDropList();
                if (files.Count > 0)
                {
                    message.FilePath = files[0];
                    message.Base64Str = Convert.ToBase64String(File.ReadAllBytes(message.FilePath));
                }
            }
        }

        private bool __ClickCopyMenuWithAttach__(AutomationElement item, SimpleMessageBubble message, Window win)
        {
            int x = 0;
            int y = 0;
            if (message.Who.equals("我"))
            {
                //点击 右边 复制
                x = item.BoundingRectangle.X + (item.BoundingRectangle.Width - (int)(93 * ratio));
                y = item.BoundingRectangle.Y + (int)(25 * ratio);
            }
            else
            {
                //点击 左边 复制
                x = item.BoundingRectangle.X + (int)(85 * ratio);
                y = item.BoundingRectangle.Y + (int)(44 * ratio);
            }
            while (item.IsAvailable && item.Name.contains("上传中"))
            {
                RandomWait.Wait(500, 1200);
            }
            if (!item.IsAvailable)
                return false;
            //如果没有下载，则先下载，因为视频并不会自动下载
            if (item.Name.contains("未下载"))
            {
                //弹出右键
                Mouse.Position = (new Point(x, y)).Confusion(3, 2);
                RandomWait.Wait(100, 300);
                SupperMouseKey.RightClick();
                RandomWait.Wait(100, 500);
                //点击下载按钮
                var downloadPath = UITreeGlobal.MessagePopupMenu_Download;
                var downLoadRetry = Retry.WhileNull(() => win.FindFirstByXPath(downloadPath), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (!downLoadRetry.Success)
                    return false;
                var downLoadItem = downLoadRetry.Result;
                var dPoint = downLoadItem.BoundingRectangle.Center().Confusion(10, 3);
                SupperMouseKey.MoveTo(dPoint);
                RandomWait.Wait(100, 300);
                SupperMouseKey.LeftClick();

                while (item.IsAvailable && item.Name.contains("下载"))
                {
                    RandomWait.Wait(500, 900);
                }
                RandomWait.Wait(300, 900);
            }
            if (!item.IsAvailable)
                return false;
            message.Message = item.Name;
            RandomWait.Wait(100, 400);
            //点击复制按钮，将图片、文件等复制进剪切板
            Mouse.Position = (new Point(x, y)).Confusion(3, 2);
            RandomWait.Wait(100, 300);
            SupperMouseKey.RightClick();
            RandomWait.Wait(100, 600);
            var path = UITreeGlobal.MessagePopupMenu_Copy;
            var menuItemRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!menuItemRetry.Success)
            {
                // SupperMouseKey.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.ESC);
                return false;
            }
            var menuItem = menuItemRetry.Result;
            var point = menuItem.BoundingRectangle.Center().Confusion(10, 3);
            SupperMouseKey.MoveTo(point);
            RandomWait.Wait(100, 300);
            SupperMouseKey.LeftClick();
            WaitClipboard(() => System.Windows.Clipboard.ContainsFileDropList(), 1000);
            //处理有可能弹窗
            var dialogPath = "/Window[@ClassName='mmui::XDialog'][@Name='Weixin']";
            try
            {
                var dialogRetry = Retry.WhileNull(() => win.FindFirstByXPath(dialogPath), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100));
                if (dialogRetry.Success)
                {
                    var dialog = dialogRetry.Result;
                    dialog.AsWindow().Close();
                }
            }
            catch (Exception)
            {
                // 菜单/弹窗正在销毁，遍历 UI 树可能读到陈旧元素，忽略即可
            }
            return true;
        }

        public bool WaitClipboard(
            Func<bool> condition,
            int timeoutMs = 1000,
            int intervalMs = 50)
        {
            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                try
                {
                    if (condition())
                        return true;
                }
                catch
                {
                    // Clipboard 此时可能正被微信/其他进程占用
                }

                Thread.Sleep(intervalMs);
            }

            // 最后再检查一次
            try
            {
                return condition();
            }
            catch
            {
                return false;
            }
        }

        private void __FetchVideo(SimpleMessageBubble message, AutomationElement item, OneOf<PullMessagesOptions, MessageMonitorOptions> options, UIA3Automation automation, AutomationElement messageListRoot, Window win)
        {
            if (options.IsT0)
            {
                var check = options.AsT0;
                if (!check.FetchVideo)
                    return;
            }
            if (options.IsT1)
            {
                var check = options.AsT1;
                if (!check.FetchVideo)
                    return;
            }
            bool flowControl = __ClickCopyMenuWithVideo__(item, message, win);
            if (!flowControl)
            {
                return;
            }

            if (System.Windows.Clipboard.ContainsFileDropList())
            {
                var files = System.Windows.Clipboard.GetFileDropList();
                if (files.Count > 0)
                {
                    message.FilePath = files[0];
                    message.Base64Str = Convert.ToBase64String(File.ReadAllBytes(message.FilePath));
                }
            }
        }
        //点击消息窗口的复制按钮
        private bool __ClickCopyMenuWithVideo__(AutomationElement item, SimpleMessageBubble message, Window win)
        {
            int x = 0;
            int y = 0;
            if (message.Who.equals("我"))
            {
                //点击 右边 复制
                x = item.BoundingRectangle.X + (item.BoundingRectangle.Width - (int)(93 * ratio));
                y = item.BoundingRectangle.Y + (int)(25 * ratio);
            }
            else
            {
                //点击 左边 复制
                x = item.BoundingRectangle.X + (int)(85 * ratio);
                y = item.BoundingRectangle.Y + (int)(44 * ratio);
            }
            while (item.IsAvailable && item.Name.contains("进度:"))
            {
                RandomWait.Wait(500, 1200);
            }
            if (!item.IsAvailable)
                return false;
            //如果没有下载，则先下载，因为视频并不会自动下载
            if (item.IsAvailable && item.Name.contains("下载"))
            {
                Mouse.Position = (new Point(x, y)).Confusion(3, 2);
                RandomWait.Wait(100, 300);
                SupperMouseKey.LeftClick();
                RandomWait.Wait(100, 500);
                while (item.IsAvailable && item.Name.contains("进度:"))
                {
                    RandomWait.Wait(500, 1200);
                }
                RandomWait.Wait(300, 1200);
            }

            if (!item.IsAvailable)
                return false;
            message.Message = item.Name;

            RandomWait.Wait(100, 400);

            //点击复制按钮，将图片、文件等复制进剪切板
            Mouse.Position = (new Point(x, y)).Confusion(3, 2);
            RandomWait.Wait(100, 300);
            SupperMouseKey.RightClick();
            RandomWait.Wait(100, 800);
            var path = UITreeGlobal.MessagePopupMenu_Copy;
            var menuItemRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!menuItemRetry.Success)
            {
                SupperMouseKey.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.ESC);
                return false;
            }
            var menuItem = menuItemRetry.Result;
            var point = menuItem.BoundingRectangle.Center().Confusion(10, 3);
            SupperMouseKey.MoveTo(point);
            RandomWait.Wait(100, 300);
            SupperMouseKey.LeftClick();
            WaitClipboard(() => System.Windows.Clipboard.ContainsFileDropList(), 2000);
            //处理有可能弹窗
            var dialogPath = "/Window[@ClassName='mmui::XDialog'][@Name='Weixin']";
            try
            {
                var dialogRetry = Retry.WhileNull(() => win.FindFirstByXPath(dialogPath), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100));
                if (dialogRetry.Success)
                {
                    var dialog = dialogRetry.Result;
                    dialog.AsWindow().Close();
                }
            }
            catch (Exception)
            {
                // 菜单/弹窗正在销毁，遍历 UI 树可能读到陈旧元素，忽略即可
            }

            return true;
        }
        //点击消息窗口的复制按钮
        private bool __ClickCopyImageMenu__(AutomationElement item, SimpleMessageBubble message, Window win)
        {
            int x = 0;
            int y = 0;
            if (message.Who.equals("我"))
            {
                //点击 右边 复制
                x = item.BoundingRectangle.X + (item.BoundingRectangle.Width - (int)(93 * ratio));
                y = item.BoundingRectangle.Y + (int)(25 * ratio);
            }
            else
            {
                //点击 左边 复制
                x = item.BoundingRectangle.X + (int)(85 * ratio);
                y = item.BoundingRectangle.Y + (int)(44 * ratio);
            }
            while (item.Name.contains("上传中"))
            {
                RandomWait.Wait(500, 1200);
            }
            //先清空剪切板内容,不至于本次如果获取文件失败得到的是上次文件内容。
            RandomWait.Wait(100, 400);

            Mouse.Position = (new Point(x, y)).Confusion(3, 2);
            RandomWait.Wait(100, 300);
            SupperMouseKey.RightClick();
            RandomWait.Wait(100, 800);
            var path = UITreeGlobal.MessagePopupMenu_Copy;
            var menuItemRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (!menuItemRetry.Success)
            {
                SupperMouseKey.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.ESC);
                return false;
            }
            var menuItem = menuItemRetry.Result;
            var point = menuItem.BoundingRectangle.Center().Confusion(10, 3);
            SupperMouseKey.MoveTo(point);
            RandomWait.Wait(100, 300);
            SupperMouseKey.LeftClick();
            // RandomWait.Wait(300, 900);
            WaitClipboard(() => System.Windows.Clipboard.ContainsFileDropList(), 1000);
            //处理有可能弹窗
            var dialogPath = "/Window[@ClassName='mmui::XDialog'][@Name='Weixin']";
            try
            {
                var dialogRetry = Retry.WhileNull(() => win.FindFirstByXPath(dialogPath), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100));
                if (dialogRetry.Success)
                {
                    var dialog = dialogRetry.Result;
                    dialog.AsWindow().Close();
                }
            }
            catch (Exception)
            {
                // 菜单/弹窗正在销毁，遍历 UI 树可能读到陈旧元素，忽略即可
            }
            return true;
        }

        private string _GetNickNameFromOCRWithPull(AutomationElement item, decimal ratio, CancellationToken token, HeaderInfo title, UIA3Automation automation, string direction, PullMessagesOptions options, Mat oriMat)
        {
            try
            {
                if (direction.Equals("right"))
                    return "我";
                if (title.HeaderType == ChatType.好友 || title.HeaderType == ChatType.企业微信)
                    return title.Title;
                //缓存初始化
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
                if (options.IsCaptureOriImage)
                {
                    _WriteToTempCacheForTest(mat2);
                }
                //检查缓存中是否命中.
                var cacheItem = ImageHashCacheHelper.GetImageCacheItem(title.Title, mat);
                if (cacheItem != null)
                    return cacheItem.NickName;

                if (options.UseUITreeRollbackPlan)
                {
                    //通过ui tree点击方案获取昵称.
                    var nickName = __GetNickNameFromUITreeClick__(item, this._Client.MainWindow);
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
                        _Logger.Error($"错误：询式消息获取通过UI Tree获取昵称为空!");
                        return "";
                    }
                }
                //通过ocr获取昵称
                var returnResult = "";
                //放大2倍
                if (mat2.Height < 40)
                {
                    using Mat resized = new Mat();
                    CvInvoke.Resize(mat2, resized, new Size(mat2.Width * 2, mat2.Height * 2), 0, 0, Emgu.CV.CvEnum.Inter.Cubic);
                    mat2 = resized.Clone();
                }

                //识别
                var result = this._Client.OcrEngee.Detect(mat2.ToBitmap(), options.OcrPadding, 1600, 0.25f, 0.2f, 1.8f, false, false, options.IsOCRDebug);
                returnResult = string.IsNullOrWhiteSpace(result.StrRes) ? "" : result.StrRes.Trim();
                mat2.Dispose();
                returnResult = _CheckCharScores_(result, returnResult, item, this._Client.MainWindow, options.CharScoreThresh);
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
                    _Logger.Error($"错误：轮询式消息获取通过OCR方式获取昵称为空!");
                    return "";
                }
            }
            catch (Exception ex)
            {
                this._Logger.Error("固定式消息监听在进行OCR识别时出现错误，错误原因:\r\n" + ex.ToString() + "\r\n错误堆栈:" + ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 检查OCR结果的可信度，如果少于0.51,则采用回退方案，并获取群昵称.
        /// </summary>
        private string _CheckCharScores_(OcrResult result, string returnResult, AutomationElement item, Window win, float charScores)
        {
            if (result.TextBlocks.Count >= 0)
            {
                var checkFlag = result.TextBlocks.Count == 0 || result.TextBlocks[0].CharScores.Any(u => u < charScores);
                if (checkFlag)
                {
                    //加上控制台消息
                    System.Diagnostics.Debug.WriteLine($"由于SDK对OCR的识别结果判断信心不足，所以将通过回退方案获取昵称！ ");
                    //回退方案,必须返回 真正的群昵称
                    var value = __GetNickNameFromUITreeClick__(item, win);
                    return value;
                }
            }
            // if (result.TextBlocks)
            return returnResult;
        }

        private string __GetNickNameFromUITreeClick__(AutomationElement item, Window win)
        {
            //右键菜单兜底方案容易受鼠标抖动/菜单未及时弹出影响，昵称为空时重试一次，减少漏读与后续重复
            for (int attempt = 0; attempt < 2; attempt++)
            {
                var nickName = __GetNickNameFromUITreeClickCore__(item, win);
                if (!string.IsNullOrEmpty(nickName))
                    return nickName;
                RandomWait.Wait(200, 600);
            }
            return "";
        }

        private string __GetNickNameFromUITreeClickCore__(AutomationElement item, Window win)
        {
            var x = (int)(39 * this.ratio);
            var y = (int)(27 * this.ratio);
            x = item.BoundingRectangle.Left + x;
            y = item.BoundingRectangle.Top + y;
            win.Focus();
            Mouse.Position = new Point(x, y);
            RandomWait.Wait(50, 150);
            Mouse.MoveTo(Mouse.Position.Confusion(3, 3));  //抖动.
            RandomWait.Wait(30, 100);
            SupperMouseKey.RightClick();
            RandomWait.Wait(100, 400);
            var path = $"/Window[@ProcessId={win.Properties.ProcessId}][@ClassName='mmui::XMenu']/MenuItem[1]";
            var menuItemRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (menuItemRetry.Success)
            {
                //@  犟种
                var text = menuItemRetry.Result.Name;
                //关闭右键菜单窗口本身，而不是菜单项的 Parent，避免 Parent 链变化时误关微信主窗口
                var menuWinPath = $"/Window[@ProcessId={win.Properties.ProcessId}][@ClassName='mmui::XMenu']";
                var menuWinRetry = Retry.WhileNull(() => win.FindFirstByXPath(menuWinPath), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                if (menuWinRetry.Success)
                {
                    menuWinRetry.Result.AsWindow().Close();
                    RandomWait.Wait(100, 400);
                }
                var match = Regex.Match(text, @"@\s*(.+)");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }

            return "";
        }

        private void _WriteToTempCacheForTest(Mat mat)
        {
            //图片日志
            var path = Path.Combine(AppContext.BaseDirectory, "Temp");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"{Guid.NewGuid():N}.png");
            mat.Save(path);
        }

        //根据鹰嘴做图像模式匹配得到消息的方向
        private string __GetMessageDirectionCore__(Mat leftImage, Mat rightImage, Mat oriImage, AutomationElement item, HeaderInfo title, Window win)
        {
            var result = this._Client.OcrEngee.ContainsTemplate(oriImage, leftImage);
            if (result)
                return "left";
            result = this._Client.OcrEngee.ContainsTemplate(oriImage, rightImage);
            if (result)
                return "right";

            // 如果识别不出鹰嘴，或者没有鹰嘴，进行回退方案，通过点击方式 来获取
            return _GetDirectionByClick(win, item, title);
        }

        private bool IsSystemMessage(AutomationElement element, ref DateTime date)
        {
            if (element.ClassName.Equals("mmui::ChatItemView") || element.ClassName.Equals("mmui::ChatSystemInfoItemView") || element.ClassName.equals("mmui::ChatFolderItemView"))
            {
                if (element.ClassName.Equals("mmui::ChatItemView"))
                {
                    var result = WeChatTimeParser.ParseWeChatTime(element.Name);
                    if (result != null)
                    {
                        date = result.Value;
                    }
                }
                return true;
            }
            return false;
        }

        private void __GoBottom__(UIA3Automation automation, PullMessagesOptions options, ListBox root, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (root.Items.Count() == 0)
                return;
            this._Client.MainWindow.Focus();
            //如果有最新跳转按钮-->点掉，影响ocr
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
                RandomWait.Wait(50, 300);
            }
            //快速到最后面
            var listBox = root.AsListBox();
            if (listBox.Items.Count() == 0)
                return;
            this._Client.MainWindow.Focus();
            if (__CheckNotBottomState__(listBox))
            {
                __QuickGoBottom__(this._Client.MainWindow, root); //快速下移
            }
            //确认到底
            index = 0;
            var oldSnapshot = root.Items.Select(u => u.Name + "|" + u.Properties.RuntimeId.ToUniqueString());
            if (oldSnapshot.Count() == 0)
                return;
            var safePoint = root.BoundingRectangle.SafeRandomPoint();
            this._Client.MainWindow.Focus();
            oldSnapshot = new List<string>();
            while (index < options.BottomRetryNumber)
            {
                token.ThrowIfCancellationRequested();
                var newSnapshot = root.Items.Select(u => u.Name + "|" + u.Properties.RuntimeId.ToUniqueString());
                var exceptList = newSnapshot.Except(oldSnapshot);
                if (exceptList.Count() > 0)
                {
                    index = 0;
                    oldSnapshot = newSnapshot;
                }
                else
                {
                    if (root.Items[root.Items.Count() - 1].BoundingRectangle.Y + root.Items[root.Items.Count() - 1].BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                this._Client.MainWindow.Focus();
                MouseScrollHelper.DownStep(safePoint.Confusion(3, 6), 3);
            }
        }
    }
}

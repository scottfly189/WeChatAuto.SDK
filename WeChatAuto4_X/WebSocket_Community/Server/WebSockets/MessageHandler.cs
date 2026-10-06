using System.Diagnostics;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeAutoCommon.Enums;
using WeChatAuto.Components;
using WeChatAuto.Models;
using WeChatAuto.Options;
using WeChatAuto.Services;

public class MessageHandler : IDisposable
{
    private readonly WeChatClientFactory factory;
    private readonly ILogger<MessageHandler> logger;
    private CancellationTokenSource systemMonitorCts = new CancellationTokenSource();
    private CancellationTokenSource monitorCts = new CancellationTokenSource();
    private Task? messageMonitorTask;
    private Task? groupSysMessageMonitorTask;
    private Task? newFriendsMonitorTask;

    public MessageHandler(WeChatClientFactory factory, ILogger<MessageHandler> logger)
    {
        this.factory = factory;
        this.logger = logger;
    }
    public async Task<RequestData> HandleAsync(MessagePackageWrapper wrapper, CancellationToken token = default)
    {
        try
        {
            var client = factory.GetWeChatClient(wrapper.FromWechat);
            RequestData response = new RequestData
            {
                Type = "echo",
                RequestId = wrapper.RequestId,
                Data = "",
            };
            switch (wrapper.FuncName)
            {
                case "GetOwerInfo":
                    await _ProcessOwnerInfo(client, response);
                    break;
                case "Max":
                    await _ProcessMaxWindow(client, response);
                    break;
                case "Restore":
                    await _RestoreWindow(client, response);
                    break;
                case "Pinned":
                    await _PinedWidnow(client, response);
                    break;
                case "UnPinned":
                    await _UnPinnedWindow(client, response);
                    break;
                case "Focus":
                    await _FocusWindow(client, response);
                    break;
                case "CloseSearchWindow":
                    await _CloseSearchWindow(client, response, wrapper.Options!);
                    break;
                case "OpenSubWin":
                    await client.OpenSubWin(wrapper.Options);
                    break;
                case "GetHandler":
                    var handler = client.GetHandler();
                    response.Data = handler.ToString();
                    break;
                case "GetProcessId":
                    var processId = client.GetProcessId();
                    response.Data = processId.ToString();
                    break;
                case "SwitchNavigation":
                    await client.SwitchNavigation((NavigationType)Enum.Parse(typeof(NavigationType), wrapper.Options!));
                    break;
                case "CloseNavWin":
                    await client.CloseNavWin((NavigationType)Enum.Parse(typeof(NavigationType), wrapper.Options!));
                    break;
                case "ClickNotifyIcon":
                    if (int.TryParse(wrapper.Options!, out var result))
                    {
                        await client.ClickNotifyIcon(result);
                    }
                    else
                    {
                        await client.ClickNotifyIcon(wrapper.Options!);
                    }
                    break;
                case "GetAllConversations":
                    var rList = await client.GetAllConversations();
                    response.Data = JsonConvert.SerializeObject(rList);
                    break;
                case "GetVisibleConversationTitles":
                    var vList = await client.GetVisibleConversationTitles();
                    response.Data = JsonConvert.SerializeObject(vList);
                    break;
                case "GetVisibleConversations":
                    var cobjList = await client.GetVisibleConversations();
                    response.Data = JsonConvert.SerializeObject(cobjList);
                    break;
                case "SearchFriend":
                    var searchResult = await client.SearchFriend(wrapper.Options!);
                    response.Data = searchResult.ToString();
                    break;
                case "LocateConversation":
                    var locateResult = await client.LocateConversation(wrapper.Options!);
                    response.Data = locateResult.ToString();
                    break;
                case "SetDoNotDisturb":
                    var options = JsonConvert.DeserializeObject<Dictionary<string, object>>(wrapper.Options!);
                    var donotDisturbResult = await client.SetDoNotDisturb(options!["who"].ToString(), bool.Parse(options!["setting"].ToString()!));
                    response.Data = donotDisturbResult.ToString();
                    break;
                case "SetTopMost":
                    var topMostOptions = JsonConvert.DeserializeObject<Dictionary<string, object>>(wrapper.Options!);
                    var setTopMostResult = await client.SetTopMost(topMostOptions!["who"].ToString(), bool.Parse(topMostOptions!["setting"].ToString()!));
                    response.Data = setTopMostResult.ToString();
                    break;
                case "GetTitle":
                    var headInfo = await client.GetTitle();
                    response.Data = JsonConvert.SerializeObject(headInfo);
                    break;
                case "FocuseSenderInput":
                    await client.FocuseSenderInput();
                    break;
                case "GetOnlyTitle":
                    var title = await client.GetOnlyTitle();
                    response.Data = title;
                    break;
                case "SendMessage":
                    var sendMessageOptions = wrapper.Options;
                    var optionsMessage = JsonConvert.DeserializeObject<Dictionary<string, string>>(sendMessageOptions!);
                    await client.SendMessage(optionsMessage!["who"], optionsMessage!["message"], JsonConvert.DeserializeObject<List<string>>(optionsMessage!["atUser"]), JsonConvert.DeserializeObject<ChatRefer>(optionsMessage!["refer"]));
                    break;
                case "SendEmoji":
                    var emojiOptions = wrapper.Options;
                    var emojiDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(emojiOptions!);
                    var atUser = emojiDict!["atUser"].ToString().Equals("null") ? null : JsonConvert.DeserializeObject<List<string>>(emojiDict!["atUser"]);
                    if (int.TryParse(emojiDict!["emoji"], out var emojiValue))
                    {
                        await client.SendEmoji(emojiDict!["who"], emojiValue, atUser);
                    }
                    else
                    {
                        await client.SendEmoji(emojiDict!["who"], emojiDict!["emoji"].ToString(), atUser);
                    }

                    break;
                case "SendFile":
                    await _SendFile(wrapper, client);
                    break;
                case "SendVoiceChat":
                    var who = wrapper.Options!;
                    await client.SendVoiceChat(who);
                    break;
                case "SendVedioChat":
                    who = wrapper.Options!;
                    await client.SendVedioChat(who);
                    break;
                case "SendVoiceChats":
                    var payload = wrapper.Options!;
                    var dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload!);
                    await client.SendVoiceChats(dicPayload!["who"].ToString(), JsonConvert.DeserializeObject<string[]>(dicPayload!["partner"].ToString()));
                    break;
                case "SendVoiceMessage":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload!); ;
                    who = dicPayload!["who"].ToString();
                    //处理声音文件
                    var localFileName = dicPayload["filePath"].ToString();
                    var fileName = Path.GetFileName(localFileName);
                    var pathRoot = Path.Combine(AppContext.BaseDirectory, "temp");
                    if (!Directory.Exists(pathRoot))
                        Directory.CreateDirectory(pathRoot);
                    pathRoot = Path.Combine(pathRoot, fileName);
                    var bytes = Convert.FromBase64String(dicPayload!["upload"].ToString());
                    await File.WriteAllBytesAsync(pathRoot, bytes);
                    await Task.Delay(1000);
                    //这里处理语音数据.
                    if (string.IsNullOrEmpty(who))
                    {
                        await client.SendVoiceMessage(pathRoot);
                    }
                    else
                    {
                        await client.SendVoiceMessage(who, pathRoot);
                    }
                    break;
                case "GetChatHistory_Current_Window":
                    payload = wrapper.Options!;
                    var date = DateOnly.Parse(payload!);
                    var fetchDate = date.ToDateTime(TimeOnly.Parse("00:00:00"));
                    var list = await client.GetChatHistory(fetchDate);
                    response.Data = JsonConvert.SerializeObject(list);
                    break;
                case "GetChatHistory_Who":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                    who = dicPayload!["who"];
                    var fDate = DateTime.Parse(dicPayload!["fetch_date"]);
                    list = await client.GetChatHistory(who, fDate);
                    response.Data = JsonConvert.SerializeObject(list);
                    break;
                case "TapWho":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                    who = dicPayload!["who"];
                    var prevNextNumber = int.Parse(dicPayload!["prev_scroll_number"]);
                    var tapResult = await client.TapWho(who, prevNextNumber);
                    response.Data = tapResult.ToString();
                    break;
                case "ForwardMultipleMessage":
                    // vip 用户独有
                    // payload = wrapper.Options!;
                    // dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                    // who = dicPayload!["who"];
                    // var to = JsonConvert.DeserializeObject<List<string>>(dicPayload!["to"]);
                    // var fType = (ForwardMessageTypeEnums)Enum.Parse(typeof(ForwardMessageTypeEnums), dicPayload!["f_type"]);
                    // var rowCount = int.Parse(dicPayload["row_count"]);
                    // response.Data = (await client.ForwardMultipleMessage(who, to!.ToArray(), fType, rowCount)).ToString();
                    break;
                case "ForwardSingleMessage":
                    // vip 用户独有
                    // payload = wrapper.Options!;
                    // dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    // who = dicPayload["who"];
                    // var message = dicPayload["message"];
                    // to = JsonConvert.DeserializeObject<List<string>>(dicPayload["to"])!;
                    // var prev_scroll_number = int.Parse(dicPayload["prev_scroll_number"]);
                    // response.Data = (await client.ForwardSingleMessage(who, message, to.ToArray(), prev_scroll_number)).ToString();
                    break;
                case "OpenAddFriensWin":
                    await client.OpenAddFriensWin();
                    break;
                case "CloseAddFriendWin":
                    await client.CloseAddFriendWin();
                    break;
                case "AddFriends":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    var friends = JsonConvert.DeserializeObject<List<string>>(dicPayload["friends"]);
                    var myOptions = JsonConvert.DeserializeObject<AddFriendsOptions>(dicPayload["options"]);
                    var fetch_result = await client.AddFriends(friends!.ToArray(), myOptions);
                    response.Data = JsonConvert.SerializeObject(fetch_result);
                    break;
                case "IsOwnerChatGroup":
                    var strResult = wrapper.Options!;
                    response.Data = (await client.IsOwnerChatGroup(strResult)).ToString();
                    break;
                case "GetGroupOwner":
                    var groupName = wrapper.Options!;
                    response.Data = await client.GetGroupOwner(groupName);
                    break;
                case "AddOwnerChatGroupMember":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"].ToString();
                    var member_name = JsonConvert.DeserializeObject<List<string>>(dicPayload["member_name"]);
                    await client.AddOwnerChatGroupMember(groupName, member_name!.ToArray());
                    break;
                case "CreateOwnerChatGroup":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    var memberList = JsonConvert.DeserializeObject<List<string>>(dicPayload["member_name"]);
                    var r = await client.CreateOwnerChatGroup(dicPayload["group_name"], dicPayload["first_who"], memberList!.ToArray());
                    response.Data = r.Success.ToString();
                    break;
                case "ChangeOwnerChatGroupName":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    r = await client.ChangeOwnerChatGroupName(dicPayload["old_group_name"], dicPayload["new_group_name"]);
                    response.Data = r.Success.ToString();
                    break;
                case "ChangeChatGroupNickName":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    var nick_name = dicPayload["nick_name"];
                    response.Data = (await client.ChangeChatGroupNickName(groupName, nick_name)).Success.ToString();
                    break;
                case "ChangeChatGroupMemo":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    var new_memo = dicPayload["new_memo"];
                    response.Data = (await client.ChangeChatGroupMemo(groupName, new_memo)).Success.ToString();
                    break;
                case "UpdateGroupNotice":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    var group_notice = dicPayload["group_notice"];
                    response.Data = (await client.UpdateGroupNotice(groupName, group_notice)).Success.ToString();
                    break;
                case "GetChatGroupMemberList":
                    payload = wrapper.Options!;
                    rList = await client.GetChatGroupMemberList(payload);
                    response.Data = JsonConvert.SerializeObject(rList);
                    break;
                case "RemoveOwnerChatGroupMember":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    member_name = JsonConvert.DeserializeObject<List<string>>(dicPayload["member_name"].ToString());
                    var removeResult = await client.RemoveOwnerChatGroupMember(groupName, member_name!.ToArray());
                    response.Data = removeResult.Success.ToString();
                    break;
                case "QuitChatGroup":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"]!;
                    var clear_history = bool.Parse(dicPayload["clear_history"].ToString());
                    await client.QuitChatGroup(groupName, clear_history);
                    break;
                case "InviteChatGroupMember":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    member_name = JsonConvert.DeserializeObject<List<string>>(dicPayload["members"].ToString());
                    var invite_reason_if_need = dicPayload["invite_reason_if_need"];
                    var inviteResult = await client.InviteChatGroupMember(groupName, member_name, inviteReasonIfNeed: invite_reason_if_need);
                    response.Data = inviteResult.Success.ToString();
                    break;
                case "AddChatGroupMemberToFriends":
                    payload = wrapper.Options!;
                    dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    groupName = dicPayload["group_name"];
                    member_name = JsonConvert.DeserializeObject<List<string>>(dicPayload["member_name"].ToString());
                    var addOptions = JsonConvert.DeserializeObject<AddFriendsOptions>(dicPayload["options"].ToString());
                    var addResult = await client.AddChatGroupMemberToFriends(groupName, member_name, addOptions);
                    response.Data = JsonConvert.SerializeObject(addResult);
                    break;
                case "GetAllFriends":
                    payload = wrapper.Options!;
                    var friendList = await client.GetAllFriends(bool.Parse(payload));
                    foreach (var item in friendList)
                    {
                        bytes = await File.ReadAllBytesAsync(item.AvatarPath);
                        item.AvatarImageBase64 = Convert.ToBase64String(bytes);
                    }
                    response.Data = JsonConvert.SerializeObject(friendList);
                    break;
                case "GetAllFriendNames":
                    var allFriendName = await client.GetAllFriendNames();
                    response.Data = JsonConvert.SerializeObject(allFriendName);
                    break;
                case "PassedAllNewFriend":
                    payload = wrapper.Options!;
                    var passedOptions = JsonConvert.DeserializeObject<FriendRequestAutoAcceptOptions>(payload);
                    var newFriends = await client.PassedAllNewFriend(passedOptions);
                    response.Data = JsonConvert.SerializeObject(newFriends);
                    break;
                case "OpenMoments":
                    // vip 用户独有
                    // var win = await client.OpenMoments();
                    // response.Data = (win != null).ToString();
                    break;
                case "CloseMoments":
                // vip 用户独有
                // await client.CloseMoments();
                // break;
                case "AddMoments":
                    // vip 用户独有
                    // payload = wrapper.Options!;
                    // dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    // var image_files = JsonConvert.DeserializeObject<List<string>>(dicPayload["image_files"])!;
                    // var content = dicPayload["content"]!;
                    // var upload = JsonConvert.DeserializeObject<Dictionary<string, string>>(dicPayload["upload"])!;
                    // var momentOptions = JsonConvert.DeserializeObject<MomentsOptions>(dicPayload["options"]);
                    // //将base64转成本地图片
                    // var imageList = new List<string>();
                    // foreach (var file in image_files)
                    // {
                    //     fileName = Path.GetFileName(file);
                    //     if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "temp")))
                    //     {
                    //         Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "temp"));
                    //     }
                    //     fileName = Path.Combine(AppContext.BaseDirectory, "temp", fileName);
                    //     var base64Str = upload[file]!;
                    //     await File.WriteAllBytesAsync(fileName, Convert.FromBase64String(base64Str));
                    //     imageList.Add(fileName);
                    // }
                    // var mementsResult = await client.AddMoments(imageList, content, momentOptions);
                    // response.Data = mementsResult!.ToString();
                    break;
                case "RemoveMoments":
                // vip用户独有
                // payload = wrapper.Options!;
                // var remove_result = await client.RemoveMoments(payload);
                // response.Data = remove_result.ToString();
                // break;
                case "AddGroupSystemMessageListener":
                    //关闭掉以前的线程
                    // vip 用户 独有
                    // if (groupSysMessageMonitorTask != null)
                    // {
                    //     systemMonitorCts.Cancel();
                    //     if (groupSysMessageMonitorTask.Status == TaskStatus.Running)
                    //     {
                    //         await groupSysMessageMonitorTask.WaitAsync(TimeSpan.FromSeconds(WeAutomation.Config.MonitorGroupInterval));
                    //     }
                    //     systemMonitorCts = new CancellationTokenSource();
                    // }
                    // payload = wrapper.Options!;
                    // var nickNameList = JsonConvert.DeserializeObject<List<string>>(payload);
                    // TaskCompletionSource tcs = new TaskCompletionSource();
                    // CancellationToken cancellationToken = systemMonitorCts.Token;
                    // if (token != default)
                    // {
                    //     cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(systemMonitorCts.Token, token).Token;
                    // }
                    // groupSysMessageMonitorTask = Task.Run(async () =>
                    // {
                    //     tcs.SetResult();
                    //     Func<SystemMessageContext, Task> func = async (context) =>
                    //     {
                    //         var part_response = new RequestData
                    //         {
                    //             Type = "echo",
                    //             RequestId = wrapper.RequestId,
                    //             Data = JsonConvert.SerializeObject(context.NewMessages),
                    //         };
                    //         await wrapper!.handler!.SendAsync(part_response);
                    //     };
                    //     await client.AddGroupSystemMessageListener(nickNameList, func, cancellationToken);
                    // });
                    // await tcs.Task;
                    break;
                case "AddMessageListener":
                    // vip 用户专享
                    // payload = wrapper.Options!;
                    // dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    // nickNameList = JsonConvert.DeserializeObject<List<string>>(dicPayload["nick_names"]);
                    // var is_open_monitor = bool.Parse(dicPayload["is_open_monitor"]);
                    // MessageMonitorOptions? addMessageOtpions = null;
                    // if (dicPayload["options"] != null)
                    // {
                    //     addMessageOtpions = JsonConvert.DeserializeObject<MessageMonitorOptions>(dicPayload["options"].ToString())!;
                    // }
                    // TaskCompletionSource messageTcs = new TaskCompletionSource();
                    // cancellationToken = monitorCts.Token;
                    // if (token != default)
                    // {
                    //     cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(monitorCts.Token, token).Token;
                    // }
                    // this.messageMonitorTask = Task.Run(async () =>
                    // {
                    //     messageTcs.SetResult();
                    //     Action<MessageContext> callBack = async (context) =>
                    //     {
                    //         Dictionary<string, string> result = new Dictionary<string, string>();
                    //         result["new_message"] = JsonConvert.SerializeObject(context.NewMessages);
                    //         result["history_messages"] = JsonConvert.SerializeObject(context.HistoryMessages);

                    //         var part_response = new RequestData
                    //         {
                    //             Type = "echo",
                    //             RequestId = wrapper.RequestId,
                    //             Data = JsonConvert.SerializeObject(result),
                    //         };
                    //         await wrapper!.handler!.SendAsync(part_response);
                    //     };
                    //     await client.AddMessageListener(nickNameList, callBack, is_open_monitor, userToken: cancellationToken, options: addMessageOtpions);
                    // });
                    // await messageTcs.Task;
                    break;
                case "AddMessageListener_With_Time":
                    // vip 用户 专享
                    //     payload = wrapper.Options!;
                    //     dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    //     nickNameList = JsonConvert.DeserializeObject<List<string>>(dicPayload["nick_names"]);
                    //     is_open_monitor = bool.Parse(dicPayload["is_open_monitor"]);
                    //     addMessageOtpions = null;
                    //     if (dicPayload["options"] != null)
                    //     {
                    //         addMessageOtpions = JsonConvert.DeserializeObject<MessageMonitorOptions>(dicPayload["options"].ToString())!;
                    //     }
                    //     var start_time = TimeOnly.Parse(dicPayload["start_time"]);
                    //     var end_time = TimeOnly.Parse(dicPayload["end_time"]);
                    //     messageTcs = new TaskCompletionSource();
                    //     cancellationToken = monitorCts.Token;
                    //     if (token != default)
                    //     {
                    //         cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(monitorCts.Token, token).Token;
                    //     }
                    //     this.messageMonitorTask = Task.Run(async () =>
                    //     {
                    //         messageTcs.SetResult();
                    //         Action<MessageContext> callBack = async (context) =>
                    //         {
                    //             Dictionary<string, string> result = new Dictionary<string, string>();
                    //             result["new_message"] = JsonConvert.SerializeObject(context.NewMessages);
                    //             result["history_messages"] = JsonConvert.SerializeObject(context.HistoryMessages);

                    //             var part_response = new RequestData
                    //             {
                    //                 Type = "echo",
                    //                 RequestId = wrapper.RequestId,
                    //                 Data = JsonConvert.SerializeObject(result),
                    //             };
                    //             await wrapper!.handler!.SendAsync(part_response);
                    //         };
                    //         await client.AddMessageListener(nickNameList, callBack, start_time, end_time, is_open_monitor, userToken: cancellationToken, options: addMessageOtpions);
                    //     });
                    //     await messageTcs.Task;
                    //     break;
                    // case "AddMessageListener_With_Range":
                    //     payload = wrapper.Options!;
                    //     dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload)!;
                    //     nickNameList = JsonConvert.DeserializeObject<List<string>>(dicPayload["nick_names"]);
                    //     is_open_monitor = bool.Parse(dicPayload["is_open_monitor"]);
                    //     addMessageOtpions = null;
                    //     if (dicPayload["options"] != null)
                    //     {
                    //         addMessageOtpions = JsonConvert.DeserializeObject<MessageMonitorOptions>(dicPayload["options"].ToString())!;
                    //     }
                    //     var range = JsonConvert.DeserializeObject<List<TimeOnlyRange>>(dicPayload["range"]);
                    //     messageTcs = new TaskCompletionSource();
                    //     cancellationToken = monitorCts.Token;
                    //     if (token != default)
                    //     {
                    //         cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(monitorCts.Token, token).Token;
                    //     }
                    //     this.messageMonitorTask = Task.Run(async () =>
                    //     {
                    //         messageTcs.SetResult();
                    //         Action<MessageContext> callBack = async (context) =>
                    //         {
                    //             Dictionary<string, string> result = new Dictionary<string, string>();
                    //             result["new_message"] = JsonConvert.SerializeObject(context.NewMessages);
                    //             result["history_messages"] = JsonConvert.SerializeObject(context.HistoryMessages);

                    //             var part_response = new RequestData
                    //             {
                    //                 Type = "echo",
                    //                 RequestId = wrapper.RequestId,
                    //                 Data = JsonConvert.SerializeObject(result),
                    //             };
                    //             await wrapper!.handler!.SendAsync(part_response);
                    //         };
                    //         await client.AddMessageListener(nickNameList, callBack, range, is_open_monitor, userToken: cancellationToken, options: addMessageOtpions);
                    //     });
                    //     await messageTcs.Task;
                    break;
                case "AddFriendRequestAutoAcceptListener":
                    // vip 用户独有
                    // payload = wrapper.Options!;
                    // var newFriendOptionPrefix = JsonConvert.DeserializeObject<FriendRequestAutoAcceptOptionsPrefix>(payload)!;
                    // var newFriendOptions = FriendRequestAutoAcceptOptions.CreateFriendRequestAutoAcceptOptions(newFriendOptionPrefix);
                    // cancellationToken = monitorCts.Token;
                    // if (token != default)
                    // {
                    //     cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(monitorCts.Token, token).Token;
                    // }
                    // var newFriendTcs = new TaskCompletionSource();

                    // newFriendsMonitorTask = Task.Run(async () =>
                    // {
                    //     newFriendTcs.SetResult();
                    //     newFriendOptions.PassedCallBack = async (items, _, _) =>
                    //     {
                    //         var part_response = new RequestData
                    //         {
                    //             Type = "echo",
                    //             RequestId = wrapper.RequestId,
                    //             Data = JsonConvert.SerializeObject(items),
                    //         };
                    //         await wrapper!.handler!.SendAsync(part_response);
                    //     };
                    //     await client.AddFriendRequestAutoAcceptListener(newFriendOptions, cancellationToken);
                    // });
                    // await newFriendTcs.Task;
                    break;
                case "PauseMessageListener":
                    // vip 用户独有
                    // await client.PauseMessageListener();
                    break;
                case "ResumeMessageListener":
                    // vip 用户独有
                    // await client.ResumeMessageListener();
                    break;
                case "AddListeningFriend":
                    // vip 用户独有
                    // who = wrapper.Options!;
                    // await client.AddListeningFriend(who);
                    break;
                case "RemoveListeningFriend":
                    // vip 用户独有
                    // who = wrapper.Options!;
                    // await client.RemoveListeningFriend(who);
                    break;
                case "PauseNewFriendListener":
                    // vip 用户独有
                    // await client.PauseNewFriendListener();
                    break;
                case "ResumeNewFriendListener":
                    // vip 用户独有
                    // await client.ResumeNewFriendListener();
                    break;
                case "RemoveFriend":
                    payload = wrapper!.Options!;
                    var remove_friend_result = await client.RemoveFriend(payload);
                    response.Data = remove_friend_result.ToString();
                    break;
                default:
                    throw new Exception("不支持的函数名!");
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new WeChatAutoWebSocketServerExcpetion(ex.ToString(), wrapper.RequestId!);
        }
    }

    private static async Task _SendFile(MessagePackageWrapper wrapper, WeChatClient client)
    {
        var payload = wrapper.Options!;
        var dicPayload = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload!);
        var localFileNames = JsonConvert.DeserializeObject<List<string>>(dicPayload!["files"]);
        var localFiles = JsonConvert.DeserializeObject<Dictionary<string, string>>(dicPayload["upload"]);
        var pathRoot = Path.Combine(AppContext.BaseDirectory, "temp");
        if (!Directory.Exists(pathRoot))
            Directory.CreateDirectory(pathRoot);
        var sendFiles = new List<string>();
        foreach (var file in localFileNames!)
        {
            var fileName = Path.GetFileName(file);
            var path = Path.Combine(pathRoot, fileName);
            var base64 = localFiles![file];
            var bytes = Convert.FromBase64String(base64);
            await File.WriteAllBytesAsync(path, bytes);
            sendFiles.Add(path);
        }
        await client.SendFile(dicPayload!["who"], sendFiles.ToArray());
    }

    private async Task _CloseSearchWindow(WeChatClient client, RequestData response, string who)
    {
        await client.CloseSearchWindow(who);
        response.Data = "";
    }

    private async Task _FocusWindow(WeChatClient client, RequestData response)
    {
        await client.Focus();
        response.Data = "";
    }

    private async Task _UnPinnedWindow(WeChatClient client, RequestData response)
    {
        await client.Focus();
        await client.UnPinned();
        response.Data = "";
    }

    private async Task _PinedWidnow(WeChatClient client, RequestData response)
    {
        await client.Focus();
        await client.Pinned();
        response.Data = "";
    }

    private async Task _RestoreWindow(WeChatClient client, RequestData response)
    {
        await client.Focus();
        await client.Restore();
        response.Data = "";
    }

    //Max
    private async Task _ProcessMaxWindow(WeChatClient client, RequestData response)
    {
        await client.Focus();
        await client.Max();
        response.Data = "";
    }

    //GetOwerInfo()
    private async Task _ProcessOwnerInfo(WeChatClient client, RequestData response)
    {
        var info = client.GetOwerInfo();
        var bytes = await File.ReadAllBytesAsync(info.AvatorPath);
        var upload = Convert.ToBase64String(bytes);
        OwerInfoExt infoExt = OwerInfoExt.CreateObject(info);
        infoExt.upload = upload;
        response.Data = JsonConvert.SerializeObject(infoExt);
    }

    public void Dispose()
    {
        systemMonitorCts?.Cancel();
        monitorCts?.Cancel();
    }


    private class OwerInfoExt : OwerInfo
    {
        public string? upload { get; set; }
        public static OwerInfoExt CreateObject(OwerInfo info)
        {
            return new OwerInfoExt()
            {
                AvatorPath = info.AvatorPath,
                NickName = info.NickName,
                WxId = info.WxId,
            };
        }
    }
}
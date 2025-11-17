using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using WxAutoCommon.Utils;
using System;
using WxAutoCommon.Models;
using OneOf;
using WeChatAuto.Utils;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using FlaUI.Core.Tools;
using WxAutoCommon.Exceptions;
using FlaUI.UIA3;
using WxAutoCommon.Simulator;
using System.Threading.Tasks;
using WeChatAuto.Models;
using FlaUI.Core.Capturing;
using WxAutoCommon.Enums;


namespace WeChatAuto.Components
{
  /// <summary>
  /// 微信客户端,一个微信客户端包含一个通知图标和一个窗口
  /// 适用于单个微信客户端的自动化操作
  /// </summary>
  public class WeChatClient : IDisposable
  {
    private IServiceProvider _serviceProvider;
    public WeChatNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
    public WeChatMainWindow WxMainWindow { get; private set; }  // 微信客户端主窗口
    public string NickName => WxMainWindow.NickName;   // 微信昵称
    public volatile bool _AppRunning = true;
    private volatile bool _disposed = false;
    public bool AppRunning => _AppRunning;
    private readonly AutoLogger<WeChatClient> _logger;
    private readonly UIThreadInvoker _CheckAppRunningUIThreadInvoker;
    private System.Threading.Timer _CheckAppRunningTimer;
    private volatile bool _CheckRunningFlag = false;
    private volatile int _RetryCount = 0;  //由于UI Automation的不稳定性，所以增加重试机制，防止因为UI Automation的不稳定性导致程序退出。
    private volatile bool _RequireRetryLogin = false;
    private readonly CancellationTokenSource _CheckAppRunningCancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// 微信客户端构造函数
    /// </summary>
    /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
    /// <param name="wxWindow">微信客户端窗口类</param>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="enableCheckAppRunning">是否启用运行检查风控退出监听</param>
    public WeChatClient(WeChatNotifyIcon wxNotifyIcon, WeChatMainWindow wxWindow, IServiceProvider serviceProvider, bool enableCheckAppRunning = true)
    {
      _logger = serviceProvider.GetRequiredService<AutoLogger<WeChatClient>>();
      WxNotifyIcon = wxNotifyIcon;
      WxMainWindow = wxWindow;
      _serviceProvider = serviceProvider;
      _CheckAppRunningUIThreadInvoker = new UIThreadInvoker("CheckAppRunning");
      if (enableCheckAppRunning)
      {
        addAppRunningCheckListener();
      }
    }

    #region NotifyIcon操作
    /// <summary>
    /// 点击通知图标,以使微信客户端窗口显示或者获取焦点
    /// </summary>
    public void ClickNotifyIcon()
    {
      WxNotifyIcon.Click();
    }
    #endregion

    #region 搜索
    /// <summary>
    /// 输入搜索内容，并回车
    /// </summary>
    /// <param name="text">搜索内容</param>
    /// <param name="isClear">是否清空搜索框,默认是False:不清空,True:清空</param>
    public void SearchSomething(string text, bool isClear = false) => WxMainWindow.Search.SearchSomething(text, isClear);
    /// <summary>
    /// 清空搜索框
    /// </summary>
    public void ClearText() => WxMainWindow.Search.ClearText();
    /// <summary>
    /// 搜索聊天
    /// </summary>
    /// <param name="who">好友名称,可以是群聊名称也可以是好友名称</param>
    public void SearchChat(string who) => WxMainWindow.Search.SearchChat(who);
    /// <summary>
    /// 在通讯录页面搜索联系人
    /// </summary>
    /// <param name="text">搜索内容</param>
    public void SearchContact(string text) => WxMainWindow.Search.SearchContact(text);
    /// <summary>
    /// 在收藏页面搜索收藏的内容
    /// </summary>
    /// <param name="content">搜索内容</param>
    public void SearchCollection(string content) => WxMainWindow.Search.SearchCollection(content);
    #endregion

    #region 客户端工具
    /// <summary>
    /// 屏幕截图
    /// </summary>
    /// <param name="fileName">文件名,默认不指定,如果不指定,则使用Capture_20251109_123456.png作为文件名</param>
    /// <returns>截图文件路径</returns>
    public string CaptureUI(string fileName = "")
    {
      return _serviceProvider.GetRequiredService<WeChatCaptureImage>().CaptureUI(fileName);
    }
    #endregion

    #region 主窗口操作
    /// <summary>
    /// 切换导航栏
    /// </summary>
    /// <param name="navigationType">导航栏类型<see cref="NavigationType"/></param>
    public void NavigationSwitch(NavigationType navigationType) => WxMainWindow.Navigation.SwitchNavigation(navigationType);

    /// <summary>
    /// 将微信主窗口置顶
    /// </summary>
    /// <param name="isTop">是否置顶,默认是True:置顶,False:取消置顶</param>
    public void WindowTop(bool isTop = true) => WxMainWindow.WindowTop(isTop);
    /// <summary>
    /// 将微信主窗口最小化
    /// </summary>
    public void WindowMin() => WxMainWindow.WindowMin();
    /// <summary>
    /// 将微信主窗口还原
    /// </summary>
    public void WindowRestore() => WxMainWindow.WindowRestore();
    /// <summary>
    /// 将微信主窗口最大化
    /// </summary>
    public void WindowMax() => WxMainWindow.WindowMax();
    #endregion

    #region 朋友圈操作
    /// <summary>
    /// 判断朋友圈是否打开
    /// </summary>
    /// <returns>是否打开</returns>
    public bool IsMomentsOpen() => WxMainWindow.Moments.IsMomentsOpen();
    /// <summary>
    /// 打开朋友圈
    /// </summary>
    public void OpenMoments() => WxMainWindow.Moments.OpenMoments();
    /// <summary>
    /// 获取朋友圈内容列表
    /// 注意：此方法会让朋友圈窗口获取焦点,可能会导致其他窗口失去焦点.
    /// </summary>
    /// <param name="count">鼠标滚动次数,一次滚动5行</param>
    /// <returns>朋友圈内容列表<see cref="MomentItem"/></returns>
    public List<MomentItem> GetMomentsList(int count = 20) => WxMainWindow.Moments.GetMomentsList(count);
    /// <summary>
    /// 静默模式获取朋友圈内容列表
    /// </summary>
    /// <returns>朋友圈内容列表<see cref="MomentItem"/></returns>
    public List<MomentItem> GetMomentsListSilence() => WxMainWindow.Moments.GetMomentsListSilence();
    /// <summary>
    /// 刷新朋友圈
    /// </summary>
    public void RefreshMomentsList() => WxMainWindow.Moments.RefreshMomentsList();
    /// <summary>
    /// 朋友圈点赞
    /// </summary>
    /// <param name="nickNames">好友名称或好友名称列表</param>
    public void LikeMoments(OneOf<string, string[]> nickNames) => WxMainWindow.Moments.LikeMoments(nickNames);
    /// <summary>
    /// 回复朋友圈
    /// </summary>
    /// <param name="nickNames">好友名称或好友名称列表</param>
    /// <param name="replyContent">回复内容</param>
    public void ReplyMoments(OneOf<string, string[]> nickNames, string replyContent)
      => WxMainWindow.Moments.ReplyMoments(nickNames, replyContent);
    #endregion

    #region 消息操作
    /// <summary>
    /// 单个发送消息，发送消息给单个好友
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="message">消息内容</param>
    /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，他有群不能@所有人</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是True:打开,False:不打开</param>
    public async Task SendWho(string who, string message, OneOf<string, string[]> atUser = default, bool isOpenChat = true)
    {
      if (isOpenChat)
      {
        await WxMainWindow.SendWhoAndOpenChat(who, message, atUser);
      }
      else
      {
        await WxMainWindow.SendWho(who, message, atUser);
      }
    }
    /// <summary>
    /// 批量发送消息
    /// </summary>
    /// <param name="whos">好友名称列表</param>
    /// <param name="message">消息内容</param>
    /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，他有群不能@所有人</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是True:打开,False:不打开</param>
    public async Task SendWhos(string[] whos, string message, OneOf<string, string[]> atUser = default, bool isOpenChat = true)
    {
      if (isOpenChat)
      {
        await WxMainWindow.SendWhosAndOpenChat(whos, message, atUser);
      }
      else
      {
        await WxMainWindow.SendWhos(whos, message, atUser);
      }
    }
    /// <summary>
    /// 发送表情
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="emoji">表情名称或者描述或者索引,具体索引或者描述等请参考<see cref="EmojiListHelper"/>或者<see cref="EmojiItem"/></param>
    /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，他有群不能@所有人</param>
    /// <param name="isOpenChat">是否打开子聊天窗口</param>
    public async Task SendEmoji(string who, OneOf<int, string> emoji, OneOf<string, string[]> atUser = default, bool isOpenChat = false)
      => await WxMainWindow.SendEmoji(who, emoji, atUser, isOpenChat);
    /// <summary>
    /// 发送表情
    /// </summary>
    /// <param name="whos">好友名称列表</param>
    /// <param name="emoji">表情名称或者描述或者索引,具体索引或者描述等请参考<see cref="EmojiListHelper"/>或者<see cref="EmojiItem"/></param>
    /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，他有群不能@所有人</param>
    /// <param name="isOpenChat">是否打开子聊天窗口</param>
    public async Task SendEmojis(string[] whos, OneOf<int, string> emoji, OneOf<string, string[]> atUser = default, bool isOpenChat = false)
      => await WxMainWindow.SendEmojis(whos, emoji, atUser, isOpenChat);
    /// <summary>
    /// 发起语音聊天,适用于单个好友
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
    public void SendVoiceChat(string who, bool isOpenChat = false) => WxMainWindow.SendVoiceChat(who, isOpenChat);
    /// <summary>
    /// 发起语音聊天,适用于群聊中发起语音聊天
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="whos">好友名称列表</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
    public void SendVoiceChats(string groupName, string[] whos, bool isOpenChat = true) => WxMainWindow.SendVoiceChats(groupName, whos, isOpenChat);
    /// <summary>
    /// 发起视频聊天,适用于单个好友,群聊没有视频聊天功能
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
    public void SendVideoChat(string who, bool isOpenChat = false) => WxMainWindow.SendVideoChat(who, isOpenChat);
    /// <summary>
    /// 发起直播,适用于群聊中发起直播，单个好友没有直播功能
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="isOpenChat">是否打开子聊天窗口,默认是False:不打开,True:打开</param>
    public void SendLiveStreaming(string groupName, bool isOpenChat = false) => WxMainWindow.SendLiveStreaming(groupName, isOpenChat);
    /// <summary>
    /// 获取当前聊天窗口的标题
    /// </summary>
    /// <returns>当前聊天窗口的标题</returns>
    public string GetCurrentChatTitle() => WxMainWindow.GetCurrentChatTitle();

    /// <summary>
    /// 给指定好友发送文件
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="files">文件路径,可以是单个文件路径，也可以是多个文件路径</param>
    /// <param name="isOpenChat">是否打开子聊天窗口</param>
    public async Task SendFile(string who, OneOf<string, string[]> files, bool isOpenChat = false)
      => await WxMainWindow.SendFile(who, files, isOpenChat);
    /// <summary>
    /// 给多个好友发送文件
    /// </summary>
    /// <param name="whos">好友名称列表</param>
    /// <param name="files">文件路径,可以是单个文件路径，也可以是多个文件路径</param>
    /// <param name="isOpenChat">是否打开子聊天窗口</param>
    public async Task SendFiles(string[] whos, OneOf<string, string[]> files, bool isOpenChat = false)
      => await WxMainWindow.SendFiles(whos, files, isOpenChat);


    #endregion

    #region 会话操作
    /// <summary>
    /// 获取会话列表可见会话
    /// 会话信息包含：会话名称、会话类型、会话状态、会话时间、会话未读消息数、会话头像<see cref="Conversation"/>
    /// </summary>
    /// <returns>返回<see cref="Conversation"/>列表</returns>
    public List<Conversation> GetVisibleConversations()
      => WxMainWindow.Conversations.GetVisibleConversations();
    /// <summary>
    /// 获取会话列表所有会话的名称
    /// 考虑到效率，只返回名称列表
    /// </summary>
    /// <returns>返回会话标题名称列表</returns>
    public List<string> GetAllConversations()
      => WxMainWindow.Conversations.GetAllConversations();
    /// <summary>
    /// 定位会话
    /// 定位会话的用途：可以将会话列表滚动到指定会话的位置，使指定会话可见
    /// </summary>
    /// <param name="title">会话标题</param>
    /// <returns>如果找到会话，则返回true，否则返回false</returns>
    public bool LocateConversation(string title)
      => WxMainWindow.Conversations.LocateConversation(title);
    /// <summary>
    /// 点击会话,使会话窗口获取焦点
    /// </summary>
    /// <param name="title">会话标题</param>
    public void ClickConversation(string title)
      => WxMainWindow.Conversations.ClickConversation(title);
    /// <summary>
    /// 点击第一个会话,使第一个会话窗口获取焦点
    /// </summary>
    public void ClickFirstConversation()
      => WxMainWindow.Conversations.ClickFirstConversation();
    /// <summary>
    /// 双击会话,使会话窗口获取焦点
    /// </summary>
    /// <param name="title">会话标题</param>
    public void DoubleClickConversation(string title)
      => WxMainWindow.Conversations.DoubleClickConversation(title);
    /// <summary>
    /// 获取会话列表可见会话标题
    /// </summary>
    /// <returns>返回会话标题列表</returns>
    public List<string> GetVisibleConversationTitles()
      => WxMainWindow.Conversations.GetVisibleConversationTitles();
    #endregion

    #region 群聊操作
    /// <summary>
    /// 获取群聊成员列表
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <returns>群聊成员列表</returns>
    public async Task<List<string>> GetChatGroupMemberList(string groupName)
      => await WxMainWindow.GetChatGroupMemberList(groupName);
    /// <summary>
    /// 是否是群聊成员
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <returns>是否是群聊成员</returns>
    public async Task<bool> IsChatGroupMember(string groupName, string memberName)
      => await WxMainWindow.IsChatGroupMember(groupName, memberName);
    /// <summary>
    /// 是否是自有群
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <returns>是否是自有群</returns>
    public async Task<bool> IsOwnerChatGroup(string groupName)
      => await WxMainWindow.IsOwnerChatGroup(groupName);
    /// <summary>
    /// 获取群主
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <returns>群主昵称</returns>
    public async Task<string> GetGroupOwner(string groupName)
      => await WxMainWindow.GetGroupOwner(groupName);
    /// <summary>
    /// 清空群聊历史
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    public async Task ClearChatGroupHistory(string groupName)
      => await WxMainWindow.ClearChatGroupHistory(groupName);
    /// <summary>
    /// 退出群聊
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    public async Task QuitChatGroup(string groupName)
      => await WxMainWindow.QuitChatGroup(groupName);
    /// <summary>
    /// 设置消息免打扰
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="isMessageWithoutInterruption">是否消息免打扰,默认是True:消息免打扰,False:取消消息免打扰</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public ChatResponse SetMessageWithoutInterruption(string groupName, bool isMessageWithoutInterruption = true)
      => WxMainWindow.SetMessageWithoutInterruption(groupName, isMessageWithoutInterruption);
    /// <summary>
    /// 设置保存到通讯录
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="isSaveToAddress">是否保存到通讯录,默认是True:保存,False:取消保存</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public ChatResponse SetSaveToAddress(string groupName, bool isSaveToAddress = true)
      => WxMainWindow.SetSaveToAddress(groupName, isSaveToAddress);
    /// <summary>
    /// 设置聊天置顶
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="isTop">是否置顶,默认是True:置顶,False:取消置顶</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public ChatResponse SetChatTop(string groupName, bool isTop = true)
      => WxMainWindow.SetChatTop(groupName, isTop);
    /// <summary>
    /// 改变自有群群备注
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="newMemo">新备注</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public ChatResponse ChageOwerChatGroupMemo(string groupName, string newMemo)
      => WxMainWindow.ChageOwerChatGroupMemo(groupName, newMemo);
    /// <summary>
    /// 改变自有群群名
    /// </summary>
    /// <param name="oldGroupName">旧群名称</param>
    /// <param name="newGroupName">新群名称</param>
    /// <returns>微信响应结果</returns>
    public ChatResponse ChangeOwerChatGroupName(string oldGroupName, string newGroupName)
      => WxMainWindow.ChangeOwerChatGroupName(oldGroupName, newGroupName);
    /// <summary>
    /// 更新群聊公告
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="groupNotice">群聊公告</param>
    /// <returns>微信响应结果</returns>
    public async Task<ChatResponse> UpdateGroupNotice(string groupName, string groupNotice)
      => await WxMainWindow.UpdateGroupNotice(groupName, groupNotice);
    /// <summary>
    /// 创建群聊
    /// 如果存在，则打开它，否则创建一个新群聊
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public ChatResponse CreateOrUpdateOwnerChatGroup(string groupName, OneOf<string, string[]> memberName)
      => WxMainWindow.CreateOrUpdateOwnerChatGroup(groupName, memberName);
    /// <summary>
    /// 检查好友是否存在,好友可以为群聊与普通好友
    /// </summary>
    /// <param name="friendName">好友名称</param>
    /// <param name="doubleClick">是否双击,True:是,False:否</param>
    /// <returns>是否存在,True:是,False:否</returns>
    public bool CheckFriendExist(string friendName, bool doubleClick = false)
      => WxMainWindow.CheckFriendExist(friendName, doubleClick);
    /// <summary>
    /// 添加群聊成员，适用于自有群
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> AddOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
      => await WxMainWindow.AddOwnerChatGroupMember(groupName, memberName);
    /// <summary>
    /// 删除群聊，适用于自有群,与退出群聊不同，退出群聊是退出群聊，删除群聊会删除自有群的所有好友，然后退出群聊
    /// willdo: 这里有一个问题，如果删除群的用户很多，则需要滚屏才能全部选中。
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> DeleteOwnerChatGroup(string groupName)
      => await WxMainWindow.DeleteOwnerChatGroup(groupName);
    /// <summary>
    /// 移除群聊成员,适用于自有群
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> RemoveOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
      => await WxMainWindow.RemoveOwnerChatGroupMember(groupName, memberName);
    /// <summary>
    /// 邀请群聊成员,适用于他有群
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <param name="helloText">打招呼文本</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> InviteChatGroupMember(string groupName, OneOf<string, string[]> memberName, string helloText = "")
      => await WxMainWindow.InviteChatGroupMember(groupName, memberName, helloText);
    /// <summary>
    /// 添加群聊里面的好友为自己的好友,适用于从他有群中添加好友为自己的好友
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="memberName">成员名称</param>
    /// <param name="intervalSecond">间隔时间</param>
    /// <param name="helloText">打招呼文本</param>
    /// <param name="label">好友标签,方便归类管理</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> AddChatGroupMemberToFriends(string groupName, OneOf<string, string[]> memberName, int intervalSecond = 5, string helloText = "", string label = "")
      => await WxMainWindow.AddChatGroupMemberToFriends(groupName, memberName, intervalSecond, helloText, label);
    /// <summary>
    /// 添加群聊里面的所有好友为自己的好友,适用于从他有群中添加所有好友为自己的好友
    /// 风控提醒：
    /// 1、此方法容易触发微信风控机制，建议使用分页添加，并使用键鼠模拟器的方式增加好友。
    /// 1、微信对于加好友每天有数量的限制，实际测试一天只能加20多个，超出数量会返回[操作过于频繁，请稍后再试。]消息.
    /// 2、实际测试:使用键鼠模拟器的方式增加好友，只会受上述的增加好友数量限制，不会被风控退出。
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="exceptList">排除列表</param>
    /// <param name="intervalSecond">间隔时间</param>
    /// <param name="helloText">打招呼文本</param>
    /// <param name="label">好友标签,方便归类管理</param>
    /// <param name="pageNo">起始页码,从1开始,如果从0开始，表示不使用分页，全部添加好友，但容易触发微信风控机制，建议使用分页添加</param>
    /// <param name="pageSize">页数量</param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> AddAllChatGroupMemberToFriends(string groupName, List<string> exceptList = null, int intervalSecond = 3,
        string helloText = "", string label = "", int pageNo = 1, int pageSize = 15)
      => await WxMainWindow.AddAllChatGroupMemberToFriends(groupName, exceptList, intervalSecond, helloText, label, pageNo, pageSize);
    /// <summary>
    /// 添加群聊里面的所有好友为自己的好友,适用于从他有群中添加所有好友为自己的好友
    /// </summary>
    /// <param name="groupName">群聊名称</param>
    /// <param name="options">添加群聊成员为好友的选项<see cref="AddGroupMemberOptions"/></param>
    /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
    public async Task<ChatResponse> AddAllChatGroupMemberToFriends(string groupName, Action<AddGroupMemberOptions> options)
      => await WxMainWindow.AddAllChatGroupMemberToFriends(groupName, options);
    #endregion

    #region 通讯录操作
    /// <summary>
    /// 获取所有好友
    /// </summary>
    /// <returns>好友列表</returns>
    public List<string> GetAllFriends()
      => WxMainWindow.AddressBook.GetAllFriends();
    /// <summary>
    /// 定位好友
    /// </summary>
    /// <param name="friendName">好友名称</param>
    /// <returns>是否存在</returns>
    public bool LocateFriend(string friendName)
      => WxMainWindow.AddressBook.LocateFriend(friendName);
    /// <summary>
    /// 获取所有公众号
    /// </summary>
    /// <returns>公众号列表</returns>
    public List<string> GetAllOfficialAccount()
    => WxMainWindow.AddressBook.GetAllOfficialAccount();
    /// <summary>
    /// 获取所有待添加好友
    /// </summary>
    /// <param name="keyWord">关键字,如果设置关键字，则返回包含关键字的新好友，如果没有设置，则返回所有新好友</param>
    /// <returns>待添加好友昵称列表</returns>
    public List<string> GetAllWillAddFriends(string keyWord = null)
      => WxMainWindow.AddressBook.GetAllWillAddFriends(keyWord);
    /// <summary>
    /// 通过新好友
    /// </summary>
    /// <param name="keyWord">关键字,如果设置关键字，则通过包含关键字的新好友，如果没有设置，则通过所有新好友</param>
    /// <param name="suffix">后缀,如果设置后缀，则在此好友昵称后添加后缀</param>
    /// <param name="label">用户标签</param>
    /// <returns>通过的新好友昵称列表</returns>
    public List<string> PassedAllNewFriend(string keyWord = null, string suffix = null, string label = null)
      => WxMainWindow.AddressBook.PassedAllNewFriend(keyWord, suffix, label);
    /// <summary>
    /// 移除好友
    /// </summary>
    /// <param name="nickName">好友昵称</param>
    /// <returns>是否成功</returns>
    public bool RemoveFriend(string nickName)
      => WxMainWindow.AddressBook.RemoveFriend(nickName);
    /// <summary>
    /// 添加好友
    /// willdo:有必要吗？
    /// </summary>
    /// <param name="friendNames">微信号/手机号列表</param>
    /// <param name="label">用户标签</param>
    /// <returns>好友名称列表和是否成功</returns>
    public List<(string friendName, bool isSuccess, string errMessage)> AddFriends(List<string> friendNames, string label = "")
      => WxMainWindow.AddressBook.AddFriends(friendNames, label);
    /// <summary>
    /// 添加好友
    /// 注意：不能添加太频繁，否则可能会触发微信的风控机制，导致加好友失败
    /// willdo:有必要吗？
    /// </summary>
    /// <param name="friendName">微信号/手机号</param>
    /// <param name="label">用户标签</param>
    /// <returns>是否成功</returns>
    public bool AddFriend(string friendName, string label = "")
      => WxMainWindow.AddressBook.AddFriend(friendName, label);
    #endregion

    #region 所有监听操作，包括消息监听、朋友圈监听、新用户监听
    /// <summary>
    /// 添加消息监听，用户需要提供一个回调函数，当有消息时，会调用回调函数
    /// callBack回调函数参数：
    /// 1.新消息气泡<see cref="MessageBubble"/>
    /// 2.包含新消息气泡的列表<see cref="List{MessageBubble}"/>，适用于给LLM大模型提供上下文
    /// 3.发送者<see cref="Sender"/>，可以用此对象发送消息、发送文件、发送表情等
    /// 4.当前微信窗口对象<see cref="WeChatMainWindow"/>，适用于全部操作，如给指定好友发送消息、发送文件、发送表情等
    /// 5.服务提供者<see cref="IServiceProvider"/>，适用于使用者传入服务提供者，用于有户获取自己注入的服务
    /// </summary>
    /// <param name="nickName">好友名称</param>
    /// <param name="callBack">回调函数,由好友提供</param>
    public async Task AddMessageListener(string nickName, Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider> callBack)
      => await WxMainWindow.AddMessageListener(nickName, callBack);
    /// <summary>
    /// 移除监听消息
    /// </summary>
    /// <param name="nickName">好友名称</param>
    public void StopMessageListener(string nickName)
      => WxMainWindow.StopMessageListener(nickName);
    /// <summary>
    /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会调用回调函数
    /// 此方法需要自行处理好友是否通过，如果需要自动通过，请使用<see cref="AddNewFriendAutoPassedListener"/>
    /// </summary>
    /// <param name="callBack">回调函数</param>
    public void AddNewFriendCustomPassedListener(Action<List<string>> callBack)
      => WxMainWindow.AddNewFriendCustomPassedListener(callBack);
    /// <summary>
    /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会调用回调函数
    /// </summary>
    /// <param name="callBack">回调函数</param>
    /// <param name="keyWord">关键字</param>
    /// <param name="suffix">后缀</param>
    /// <param name="label">标签</param>
    public void AddNewFriendAutoPassedListener(Action<List<string>> callBack, string keyWord = null, string suffix = null, string label = null)
      => WxMainWindow.AddNewFriendAutoPassedListener(callBack, keyWord, suffix, label);
    /// <summary>
    /// 移除新用户监听
    /// </summary>
    public void StopNewUserListener()
      => WxMainWindow.StopNewUserListener();
    /// <summary>
    /// 添加新用户监听，用户需要提供一个回调函数，当有新用户时，会自动通过此用户，并且将此用户打开到子窗口，当有新消息时，会调用回调函数
    /// </summary>
    /// <param name="callBack">回调函数</param>
    /// <param name="keyWord">关键字</param>
    /// <param name="suffix">后缀</param>
    /// <param name="label">标签</param>
    public void AddNewFriendAutoPassedAndOpenSubWinListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatClientFactory, IServiceProvider> callBack, string keyWord = null, string suffix = null, string label = null)
      => WxMainWindow.AddNewFriendAutoPassedAndOpenSubWinListener(callBack, keyWord, suffix, label);
    /// <summary>
    /// 添加朋友圈监听,当监听到指定的好友发朋友圈时，可以自动点赞，或者执行其他操作，如：回复评论等
    /// </summary>
    /// <param name="nickNameOrNickNames">监听的好友名称或好友名称列表</param>
    /// <param name="autoLike">是否自动点赞</param>
    /// <param name="action">朋友圈对象<see cref="MomentsContext"/>,可以通过Monents对象调用回复评论等操作,服务提供者<see cref="IServiceProvider"/>，适用于使用者获取自己注入的服务</param>
    public void AddMomentsListener(OneOf<string, List<string>> nickNameOrNickNames, bool autoLike = true, Action<MomentsContext, IServiceProvider> action = null)
      => WxMainWindow.Moments.AddMomentsListener(nickNameOrNickNames, autoLike, action);
    /// <summary>
    /// 停止朋友圈监听
    /// </summary>
    public void StopMomentsListener()
      => WxMainWindow.Moments.StopMomentsListener();
    #endregion
    #region 风控退出监听
    /// <summary>
    /// 添加运行检查风控退出监听
    /// </summary>
    private void addAppRunningCheckListener()
    {
      _logger.Trace($"微信客户端是{NickName}添加运行检查监听");
      _RetryCount = 0;
      _CheckAppRunningTimer = new System.Threading.Timer(_ =>
      {
        try
        {
          if (_disposed)
            return;
          if (_CheckAppRunningCancellationTokenSource.Token.IsCancellationRequested)
            return;
          if (_CheckRunningFlag)
          {
            _logger.Trace($"微信客户端[{NickName}]运行检查风控退出监听线程正在运行，跳过本次检测");
            return;
          }
          _CheckRunningFlag = true;
          _addAppRunningCheckListenerCore();
        }
        catch (OperationCanceledException)
        {
          _logger.Info($"微信客户端是{NickName}运行检查监听线程已停止，正常取消,不做处理");
        }
        catch (WindowNotExsitException)
        {
          _CheckRunningFlag = false;
          _AppRunning = false;  // 标记应用已停止，让依赖此属性的代码知道
          _CheckAppRunningCancellationTokenSource?.Cancel();  // 取消后续检查，让 Timer 回调提前退出
          _logger.Error($"微信客户端是{NickName}运行检查监听失败:窗口不存在，已停止监听");
        }
        catch (Exception ex)
        {
          _CheckRunningFlag = false;
          _logger.Error($"微信客户端是{NickName}运行检查监听失败:{ex.Message}", ex);
        }
      }, null, 0, WeAutomation.Config.CheckAppRunningInterval * 1000);
    }
    /// <summary>
    /// 运行检查风控退出监听核心方法
    /// </summary>
    private void _addAppRunningCheckListenerCore()
    {
      try
      {
        _CheckAppRunningUIThreadInvoker.Run(automation =>
        {
          _CheckAppRunningCancellationTokenSource?.Token.ThrowIfCancellationRequested();
          var wxWindowResult = _GetWindowInfo(automation);
          if (wxWindowResult.Success && wxWindowResult.Result != null)
          {
            _RetryCount = 0;
            var window = wxWindowResult.Result;
            if (window.ClassName == "WeChatMainWndForPC")
            {
              this._AppRunning = true;
              _CheckRunningFlag = false;
              _RequireRetryLogin = false;
              _logger.Trace($"微信客户端是[{NickName}]运行检查监听成功，没有被风控退出");
            }
            else if (window.ClassName == "WeChatLoginWndForPC")
            {
              _CheckRunningFlag = false;
              this._AppRunning = false;
              if (_RequireRetryLogin)
                return;
              _RequireRetryLogin = true;
              _logger.Error($"微信客户端是[{NickName}]运行检查监听结果：被风控退出，正在尝试自动登录");
              RetryLogin(automation, window);
            }
            else
            {
              _logger.Error($"微信客户端是[{NickName}]运行检查监听结果：被风控退出，错误原因：{window.ClassName}窗口不存在");
              throw new WindowNotExsitException($"微信客户端是[{NickName}]运行检查监听结果：被风控退出，错误原因：{window.ClassName}窗口不存在");
            }
          }
          else
          {
            _logger.Error($"微信客户端是{NickName}运行检查监听失败，窗口不存在");
            throw new WindowNotExsitException($"微信客户端是{NickName}运行检查监听失败，错误原因：窗口不存在");
          }
          _CheckRunningFlag = false;
        }).GetAwaiter().GetResult();
      }
      catch (OperationCanceledException)
      {
        _logger.Info($"微信客户端是{NickName}运行检查监听线程已停止，正常取消,不做处理");
      }
      catch (WindowNotExsitException ex)
      {
        _RetryCount++;
        if (_RetryCount < 3)
        {
          _CheckRunningFlag = false;
          _logger.Error($"微信客户端是{NickName}运行检查监听失败:{ex.Message},重试{_RetryCount}次", ex);
          Thread.Sleep(300);
          return;
        }
        _CheckRunningFlag = true;
        _logger.Error($"重试{_RetryCount}次后，微信客户端是{NickName}运行检查监听失败:{ex.Message},严重：系统将不再监听微信的风控退出.", ex);
        throw;  //因为抛出的是窗口不存在，所以直接终止应用运行.
      }
      catch (Exception ex)
      {
        this._AppRunning = false;
        _logger.Error($"微信客户端是{NickName}运行检查监听失败:{ex.Message}", ex);
      }
    }
    private RetryResult<Window> _GetWindowInfo(UIA3Automation automation)
    {
      var desktop = automation.GetDesktop();
      var result = Retry.WhileNull<Window>(() =>
      {
        var window = desktop.FindFirstChild(cf => cf.ByClassName("WeChatMainWndForPC").And(cf.ByProcessId(WxMainWindow.ProcessId)))?.AsWindow();
        return window;
      }, timeout: TimeSpan.FromSeconds(5), interval: TimeSpan.FromMilliseconds(200));
      if (result.Success)
      {
        return result;
      }
      return Retry.WhileNull<Window>(() =>
      {
        var window = desktop.FindFirstChild(cf => cf.ByClassName("WeChatLoginWndForPC").And(cf.ByProcessId(WxMainWindow.ProcessId)))?.AsWindow();
        return window;
      }, timeout: TimeSpan.FromSeconds(5), interval: TimeSpan.FromMilliseconds(200));
    }
    /// <summary>
    /// 自动登录
    /// </summary>
    private void RetryLogin(UIA3Automation automation, Window window)
    {
      _ClickConfirmButton(window);
      _ClickRetryButton(automation, window);
    }

    /// <summary>
    /// 点击登录按钮
    /// </summary>
    /// <param name="automation">自动化</param>
    /// <param name="window">窗口</param>
    private void _ClickRetryButton(UIA3Automation automation, Window window)
    {
      var loginButtonResult = Retry.WhileNull(() =>
      {
        var cf = automation.ConditionFactory;
        var cond = cf.ByControlType(ControlType.Button)
                      .And(cf.ByName("登录"));
        var loginButton = window.FindFirst(TreeScope.Descendants, cond)?.AsButton();
        return loginButton;
      },
        timeout: TimeSpan.FromSeconds(3),
        interval: TimeSpan.FromMilliseconds(200));
      if (loginButtonResult.Success)
      {
        var button = loginButtonResult.Result;
        _logger.Trace($"按系统设定，等待{WeAutomation.Config.AppRetryWaitTime}秒后，自动点击登录按钮");
        Thread.Sleep(WeAutomation.Config.AppRetryWaitTime * 1000);
        window.Focus();
        button.DrawHighlightExt();
        button.WaitUntilClickable();
        button.Focus();
        if (!button.IsOffscreen && button.IsEnabled)
        {
          button.ClickEnhance(window);
          _logger.Trace("已自动点击登录按钮，等待人工通过微信验证");
        }
      }
      else
      {
        _logger.Info("没有找到登录按钮，可能用户正在人工通过微信验证");
      }
    }

    /// <summary>
    /// 点击确认按钮
    /// </summary>
    /// <param name="window">窗口</param>
    private void _ClickConfirmButton(Window window)
    {
      var confirmButtonResult = Retry.WhileNull(() => window.FindFirstByXPath("//Button[@Name='确认']")?.AsButton(),
        timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
      if (confirmButtonResult.Success)
      {
        var button = confirmButtonResult.Result;
        button.DrawHighlightExt();
        button.WaitUntilClickable();
        button.Focus();
        button.ClickEnhance(window);
        Thread.Sleep(300);
        _logger.Trace("已自动点击确认按钮，下一步自动点击登录按钮");
      }
    }
    #endregion
    #region 释放资源
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~WeChatClient()
    {
      Dispose(false);
    }
    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
        return;
      _disposed = true;
      if (disposing)
      {
        _CheckAppRunningCancellationTokenSource?.Cancel();
        _CheckAppRunningTimer?.Dispose();
        WxNotifyIcon?.Dispose();
        WxMainWindow?.Dispose();
        _CheckAppRunningCancellationTokenSource?.Dispose();
        _CheckAppRunningUIThreadInvoker?.Dispose();
      }
    }
    #endregion
  }
}
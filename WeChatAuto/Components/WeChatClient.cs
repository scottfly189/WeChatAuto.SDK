using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;
using System;
using WxAutoCommon.Models;
using OneOf;
using WeChatAuto.Utils;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using FlaUI.Core.Tools;
using System.Windows.Markup;
using System.Runtime.InteropServices;


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
    private readonly CancellationTokenSource _CheckAppRunningCancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// 微信客户端构造函数
    /// </summary>
    /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
    /// <param name="wxWindow">微信客户端窗口类</param>
    public WeChatClient(WeChatNotifyIcon wxNotifyIcon, WeChatMainWindow wxWindow, IServiceProvider serviceProvider, bool enableCheckAppRunning = true)
    {
      _logger = serviceProvider.GetRequiredService<AutoLogger<WeChatClient>>();
      WxNotifyIcon = wxNotifyIcon;
      WxMainWindow = wxWindow;
      _serviceProvider = serviceProvider;
      _CheckAppRunningUIThreadInvoker = new UIThreadInvoker();
      if (enableCheckAppRunning)
      {
        addAppRunningCheckListener();
      }
    }

    #region NotifyIcon操作
    public void ClickNotifyIcon()
    {
      WxNotifyIcon.Click();
    }
    #endregion


    #region 微信客户端工具操作
    /// <summary>
    /// 出错后捕获UI
    /// </summary>
    /// <param name="path">保存路径</param>
    public void CaptureUI(string path)
    {

    }
    /// <summary>
    /// 视频录制
    /// </summary>
    /// <param name="path">保存路径</param>
    public void VideoRecord(string path)
    {

    }
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
    public void StopMomentsListener() => WxMainWindow.Moments.StopMomentsListener();
    #endregion

    #region 消息操作
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
    #endregion

    #region 通讯录操作
    #endregion

    #region 监听操作，包括消息监听、朋友圈监听、新用户监听

    #endregion
    private void addAppRunningCheckListener()
    {
      try
      {
        _CheckAppRunningTimer = new System.Threading.Timer(_ =>
        {
          if (_CheckRunningFlag) return;
          _addAppRunningCheckListenerCore();
        }, null, 0, WeAutomation.Config.CheckAppRunningInterval * 1000);
      }
      catch (Exception ex)
      {
        _logger.Error($"微信客户端是{NickName}运行检查监听失败:{ex.Message}", ex);
      }
    }
    private void _addAppRunningCheckListenerCore()
    {
      try
      {
        _CheckAppRunningUIThreadInvoker.Run(automation =>
        {
          _CheckRunningFlag = true;
          _CheckAppRunningCancellationTokenSource.Token.ThrowIfCancellationRequested();
          var desktop = automation.GetDesktop();
          var wxWindowResult = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("WeChatMainWndForPC")
            .And(cf.ByControlType(ControlType.Window)
            .And(cf.ByProcessId(WxMainWindow.ProcessId)))),
            timeout: TimeSpan.FromSeconds(5),
            interval: TimeSpan.FromMilliseconds(200));
          if (wxWindowResult.Success && wxWindowResult.Result != null)
          {
            var window = wxWindowResult.Result.AsWindow();
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
            if (loginButtonResult.Success && loginButtonResult.Result != null)
            {
              if (this._AppRunning)
              {
                this._AppRunning = false;
                var button = loginButtonResult.Result;
                Thread.Sleep(WeAutomation.Config.AppRetryWaitTime * 1000);
                button.DrawHighlight();
                button.WaitUntilClickable();
                button.Focus();
                if (!button.IsOffscreen && button.IsEnabled)
                {
                  button.Click();   //等用户手动点击登录按钮,仅点击一次
                }
              }
            }
            else
            {
              this._AppRunning = true;
            }
          }
          else
          {
            _logger.Error($"微信客户端是{NickName}运行检查监听失败，窗口不存在");
            throw new Exception($"微信客户端[{NickName}]运行检查监听失败，窗口不存在,可能微信已退出，请重新打开微信客户端重试");
          }
          _CheckRunningFlag = false;
        }).Wait();
      }
      catch (OperationCanceledException)
      {
        _logger.Info($"微信客户端是{NickName}运行检查监听线程已停止，正常取消,不做处理");
      }
      catch (Exception ex)
      {
        _logger.Error($"微信客户端是{NickName}运行检查监听失败:{ex.Message}", ex);
      }
    }
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
      if (_disposed) return;
      if (disposing)
      {
        // 先取消操作，然后释放托管资源
        _CheckAppRunningCancellationTokenSource?.Cancel();
        _CheckAppRunningCancellationTokenSource?.Dispose();
      }
      WxMainWindow?.Dispose();
      _CheckAppRunningTimer?.Dispose();
      _CheckAppRunningUIThreadInvoker?.Dispose();
      _disposed = true;
    }
    #endregion
  }
}
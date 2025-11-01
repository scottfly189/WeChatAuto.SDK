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


namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端,一个微信客户端包含一个通知图标和一个窗口
    /// 适用于单个微信客户端的自动化操作
    /// </summary>
    public class WeChatClient
    {
        private IServiceProvider _serviceProvider;
        public WeChatNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
        public WeChatMainWindow WxMainWindow { get; private set; }  // 微信客户端主窗口
        public string NickName => WxMainWindow.NickName;   // 微信昵称

        public volatile bool _AppRunning = true;

        public bool AppRunning => _AppRunning;


        /// <summary>
        /// 微信客户端构造函数
        /// </summary>
        /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
        /// <param name="wxWindow">微信客户端窗口类</param>
        public WeChatClient(WeChatNotifyIcon wxNotifyIcon, WeChatMainWindow wxWindow, IServiceProvider serviceProvider)
        {
            WxNotifyIcon = wxNotifyIcon;
            WxMainWindow = wxWindow;
            _serviceProvider = serviceProvider;
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
        /// <param name="action">回调函数,参数：朋友圈内容列表<see cref="List{MonentItem}"/>,朋友圈对象<see cref="Moments"/>,可以通过Monents对象调用回复评论等操作,服务提供者<see cref="IServiceProvider"/>，适用于使用者获取自己注入的服务</param>
        public void AddMomentsListener(OneOf<string, List<string>> nickNameOrNickNames, bool autoLike = true, Action<List<MomentItem>, Moments, IServiceProvider> action = null)
          => WxMainWindow.Moments.AddMomentsListener(nickNameOrNickNames, autoLike, action);
        /// <summary>
        /// 停止朋友圈监听
        /// </summary>
        public void StopMomentsListener() => WxMainWindow.Moments.StopMomentsListener();
        #endregion

        #region 消息操作
        #endregion

        #region 群聊操作
        #endregion
    }
}
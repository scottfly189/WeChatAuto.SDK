using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;
using System;


namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端,一个微信客户端包含一个通知图标和一个窗口
    /// </summary>
    public class WeChatClient
    {
        private IServiceProvider _serviceProvider;
        public WeChatNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
        public WeChatMainWindow WxMainWindow { get; private set; }  // 微信客户端窗口

        public string NickName => WxMainWindow.NickName;   // 微信昵称


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

        public void ClickNotifyIcon()
        {
            WxNotifyIcon.Click();
        }

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
    }
}
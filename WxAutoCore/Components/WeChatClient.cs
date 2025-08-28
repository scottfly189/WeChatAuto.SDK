using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Utils;


namespace WxAutoCore.Components
{
    /// <summary>
    /// 微信客户端,一个微信客户端包含一个通知图标和一个窗口
    /// </summary>
    public class WeChatClient
    {
        public WeChatNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
        public WeChatMainWindow WxWindow { get; private set; }  // 微信客户端窗口

        public string NickName => WxWindow.NickName;   // 微信昵称


        /// <summary>
        /// 微信客户端构造函数
        /// </summary>
        /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
        /// <param name="wxWindow">微信客户端窗口类</param>
        public WeChatClient(WeChatNotifyIcon wxNotifyIcon, WeChatMainWindow wxWindow)
        {
            WxNotifyIcon = wxNotifyIcon;
            WxWindow = wxWindow;
        }

        public void ClickNotifyIcon()
        {
            WxNotifyIcon.Click();
        }
    }
}
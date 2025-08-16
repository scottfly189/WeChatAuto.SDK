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
    /// 微信客户端
    /// </summary>
    public class WxClient
    {
        public WxNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
        public WxWindow WxWindow { get; private set; }  // 微信客户端窗口


        /// <summary>
        /// 微信客户端构造函数
        /// </summary>
        /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
        /// <param name="wxWindow">微信客户端窗口类</param>
        public WxClient(WxNotifyIcon wxNotifyIcon, WxWindow wxWindow)
        {
            WxNotifyIcon = wxNotifyIcon;
            WxWindow = wxWindow;
        }
    }
}
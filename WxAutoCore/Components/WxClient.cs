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
        private int _ProcessId;
        private WxWindow _WxWindow;
        public WxNotifyIcon WxNotifyIcon { get; private set; }  // 微信客户端通知图标
        public int ProcessId => _ProcessId;  // 微信客户端进程ID
        public WxWindow WxWindow => _WxWindow;  // 微信客户端窗口



        /// <summary>
        /// 微信客户端构造函数
        /// </summary>
        /// <param name="window">微信客户端窗口类</param>
        /// <param name="processId">微信客户端进程ID</param>
        /// <param name="wxNotifyIcon">微信客户端通知图标类</param>
        /// <param name="notifyIconProcessId">微信客户端通知图标进程ID</param>
        public WxClient(Window window, int processId, Button wxNotifyIcon, int notifyIconProcessId)
        {
            _ProcessId = processId;
            WxNotifyIcon = new WxNotifyIcon(wxNotifyIcon, notifyIconProcessId);
        }


        /// <summary>
        /// 点击通知图标
        /// </summary>
        public void ClickNotifyIcon()
        {
            WxNotifyIcon.Click();
        }
    }
}
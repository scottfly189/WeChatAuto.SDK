using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow
    {
        private ChatContent _ChatContent;
        private WeChatMainWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window
        private int _ProcessId;
        private UIThreadInvoker _uiThreadInvoker;
        public Window SelfWindow { get => _SelfWindow; set => _SelfWindow = value; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        public ChatContent ChatContent => _ChatContent;

        public int ProcessId => _ProcessId;

        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口的微信窗口对象</param>
        public SubWin(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, string title)
        {
            _uiThreadInvoker = uiThreadInvoker;
            NickName = title;
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane", this, uiThreadInvoker,this._MainWxWindow);
            _ProcessId = _SelfWindow.Properties.ProcessId.Value;
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack"></param>
        public void AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork> callBack)
        {
            _ChatContent.ChatBody.AddListener(callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            _ChatContent.ChatBody.StopListener();
        }
        public void Close()
        {
            _SelfWindow.Close();
        }

        public void Minimize()
        {

        }
        public void WindowMin()
        {

        }

        public void Maximize()
        {

        }

        public void Restore()
        {

        }

        public void WindowTop()
        {

        }
    }
}
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
    public class SubWin : IWeChatWindow,IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private ChatContent _ChatContent;
        private WeChatMainWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window
        private int _ProcessId;
        private UIThreadInvoker _uiThreadInvoker;
        private SubWinList _SubWinList;
        private ChatBody _ChatBodyCache;
        public Window SelfWindow { get => _SelfWindow; set => _SelfWindow = value; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public ChatContent ChatContent => _ChatContent;
        public int ProcessId => _ProcessId;
        private volatile bool _disposed = false;
        

        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口的微信窗口对象</param>
        /// <param name="subWinList">子窗口列表</param>
        /// <param name="serviceProvider">服务提供者</param>
        public SubWin(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, string title, SubWinList subWinList, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiThreadInvoker = uiThreadInvoker;
            _SubWinList = subWinList;
            NickName = title;
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane", this, uiThreadInvoker, this._MainWxWindow, _serviceProvider);
            _ProcessId = _SelfWindow.Properties.ProcessId.Value;
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack"></param>
        public void AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork,IServiceProvider> callBack)
        {
            if (_disposed)
            {
                return;
            }
            if (_ChatBodyCache == null)
            {
                _ChatBodyCache = _ChatContent.ChatBody;
            }
            _ChatBodyCache.AddListener(callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            if (_disposed)
            {
                return;
            }

            _ChatBodyCache?.StopListener();
        }
        /// <summary>
        /// 关闭子窗口
        /// </summary>
        public void Close()
        {
            _SelfWindow.Close();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            if (_ChatBodyCache != null)
            {
                _ChatBodyCache.Dispose();
            }
        }
    }
}
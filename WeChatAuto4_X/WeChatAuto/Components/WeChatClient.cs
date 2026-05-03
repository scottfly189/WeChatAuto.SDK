using FlaUI.Core.Definitions;
using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using WeAutoCommon.Utils;
using System;
using WeAutoCommon.Models;
using OneOf;
using WeChatAuto.Utils;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using FlaUI.Core.Tools;
using WeAutoCommon.Exceptions;
using FlaUI.UIA3;
using WeAutoCommon.Simulator;
using System.Threading.Tasks;
using FlaUI.Core.Capturing;
using WeAutoCommon.Enums;
using WeChatAuto.Extentions;


namespace WeChatAuto.Components
{
    /// <summary>
    /// 微信客户端
    /// 适用于单个微信客户端的自动化操作
    /// </summary>
    public class WeChatClient : IDisposable
    {
        private readonly AutoLogger<WeChatClient> _logger;
        private IServiceProvider serviceProvider;
        private volatile bool _disposed = false;
        private UIThreadInvoker _MainThreadInvoker;
        #region 下面三个公开字段为比较稳定的字段，只要微信不关闭
        public readonly Window MainWindow;
        public readonly int ClientProcessId;
        public readonly WeChatClientFactory Factory;
        public UIThreadInvoker MainThreadInvoker => _MainThreadInvoker;
        #endregion

        public WeChatClient(int clientProcessId, IServiceProvider provider, WeChatClientFactory factory, Window window, UIThreadInvoker uIThreadInvoker)
        {
            this._MainThreadInvoker = uIThreadInvoker;
            this.MainWindow = window;
            this.serviceProvider = provider;
            this.Factory = factory;
            this.ClientProcessId = clientProcessId;
            _logger = provider.GetRequiredService<AutoLogger<WeChatClient>>();
        }

        #region 窗口管理
        #endregion

        #region Navigator管理
        #endregion

        #region 会话管理
        #endregion

        #region 消息管理
        #endregion

        #region  监听管理
        #endregion

        #region 好友/群聊管理
        #endregion

        #region 通讯录管理
        #endregion

        #region 朋友圈管理
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

            }
        }
        #endregion
    }
}
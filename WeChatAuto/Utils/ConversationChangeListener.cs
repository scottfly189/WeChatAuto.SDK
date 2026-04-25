using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Models;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 会话列表切换监听器
    /// </summary>
    public class ConversationChangeListener : IDisposable
    {
        private UIThreadInvoker uIThreadInvoker;
        private Window _Window;
        private IServiceProvider serviceProvider;
        private WeChatMainWindow _MainChat;

        public ConversationChangeListener(UIThreadInvoker uIThreadInvoker, Window window, WeChatMainWindow ownerMainChat, IServiceProvider serviceProvider)
        {
            this.uIThreadInvoker = uIThreadInvoker;
            this._Window = window;
            this._MainChat = ownerMainChat;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 开启会话列表监听器
        /// </summary>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public async Task Star(Action<ChatContext> callBack)
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// 暂停监听
        /// </summary>
        /// <returns></returns>
        public async Task Pause()
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Resume()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            Dispose();
            await Task.CompletedTask;
        }


        public void Dispose()
        {

        }
    }
}
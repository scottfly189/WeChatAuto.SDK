using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using OneOf;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow, IDisposable
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
        public void AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork, IServiceProvider> callBack)
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

        #region 群聊操作

        #region 群基础操作，适用于自有群与他有群
        /// <summary>
        /// 更新群聊选项
        /// </summary>
        /// <param name="action">更新群聊选项的Action</param>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse UpdateChatGroupOptions(Action<ChatGroupOptions> action, string groupName)
        {
            return new ChatResponse
            {
                IsSuccess = true,
                Message = "更新群聊选项成功"
            };
        }
        /// <summary>
        /// 获取群聊成员列表
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>群聊成员列表</returns>
        /// <returns></returns>
        public List<string> GetChatGroupMemberList(string groupName)
        {
            return new List<string>();
        }
        /// <summary>
        /// 搜索群聊成员
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>成员元素</returns>
        private AutomationElement SearchChatGroupMember(string groupName, string memberName)
        {
            return null;
        }
        private bool IsChatGroupMember(string groupName, string memberName)
        {
            return SearchChatGroupMember(groupName, memberName) != null;
        }
        #endregion

        #region 自有群操作
        /// <summary>
        /// 更新群聊名称
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="newGroupName"></param>
        /// <returns></returns>
        public ChatResponse UpdateOwnerChatGroupName(string groupName, string newGroupName)
        {
            return null;
        }
        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse CreateOwnerChatGroup(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }

        /// <summary>
        /// 删除群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse DeleteOwnerChatGroup(string groupName)
        {
            return null;
        }
        /// <summary>
        /// 发送群聊公告
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="notice">公告内容</param>
        /// <returns></returns>
        public ChatResponse SendOwnerChatGroupNotice(string groupName, string notice)
        {
            return null;
        }
        /// <summary>
        /// 添加群聊成员
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }

        public ChatResponse RemoveOwnerChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }

        #endregion
        #region 他有群特定操作
        /// <summary>
        /// 邀请群聊成员,适用于他有群
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse InviteChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }
        /// <summary>
        /// 添加群聊成员,适用于他有群
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddChatGroupMember(string groupName, OneOf<string, string[]> memberName)
        {
            return null;
        }
        #endregion
        #endregion

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
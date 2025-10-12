using System;
using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区标题区
    /// </summary>
    public class ChatHeader
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 聊天标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 聊天信息按钮
        /// </summary>
        public Button ChatInfoButton { get; set; }
        /// <summary>
        /// 聊天内容区标题区构造函数
        /// </summary>
        /// <param name="title">聊天标题</param>
        /// <param name="chatInfoButton">聊天信息按钮</param>
        public ChatHeader(string title, Button chatInfoButton, IServiceProvider serviceProvider)
        {
            Title = title;
            ChatInfoButton = chatInfoButton;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 点击聊天信息按钮
        /// </summary>
        public void ClickChatInfoButton()
        {
            ChatInfoButton?.Invoke();  //可能有问题，需要测试
        }
        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>聊天标题和聊天信息按钮名称</returns>
        public override string ToString()
        {
            return $"Title: {Title}";
        }
    }
}
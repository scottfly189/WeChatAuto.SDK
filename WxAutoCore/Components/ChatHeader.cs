using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区标题区
    /// </summary>
    public class ChatHeader
    {
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
        public ChatHeader(string title, Button chatInfoButton)
        {
            Title = title;
            ChatInfoButton = chatInfoButton;
        }
        /// <summary>
        /// 点击聊天信息按钮
        /// </summary>
        public void ClickChatInfoButton()
        {
            ChatInfoButton?.Invoke();  //可能有问题，需要测试
        }
    }
}
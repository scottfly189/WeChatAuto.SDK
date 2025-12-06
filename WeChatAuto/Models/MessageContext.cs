using System;
using System.Collections.Generic;
using WeChatAuto.Components;

namespace WeChatAuto.Models
{
    public class MessageContext
    {
        /// <summary>
        /// 新消息气泡列表
        /// 参考<see cref="MessageBubble"/>
        /// </summary>
        public List<MessageBubble> NewMessages { get; set; }
        /// <summary>
        /// 所有消息气泡列表
        /// 参考<see cref="MessageBubble"/>
        /// </summary>
        public List<MessageBubble> AllMessages { get; set; }
        /// <summary>
        /// 发送者,调用此类可以发送消息、发送文件、发送表情等
        /// 参考<see cref="Sender"/>
        /// </summary>
        public Sender Sender { get; set; }
        /// <summary>
        /// 当前微信客户端
        /// 参考<see cref="WeChatClient"/>
        /// </summary>
        public WeChatClient OwnerClient { get; set; }
        /// <summary>
        /// 系统微信客户端工厂,可以通过WeChatClientFactory获取其他微信客户端,发送消息、发送文件、发送表情等
        /// 参考<see cref="WeChatClientFactory"/>
        /// </summary>
        public WeChatClientFactory SystemClientFactory { get; set; }
        /// <summary>
        /// 服务提供者，使用者可以注入自己的服务，在此处获取
        /// 参考<see cref="IServiceProvider"/>
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
    }
}
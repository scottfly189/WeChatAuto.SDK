using System;
using System.Collections.Generic;
using System.Linq;
using WeChatAuto.Components;

namespace WeChatAuto.Models
{
    public sealed class MessageContext
    {
        public MessageContext(List<MessageBubble> newMessages, List<MessageBubble> allMessages, Sender sender, WeChatClient ownerClient, WeChatClientFactory systemClientFactory, IServiceProvider serviceProvider)
        {
            NewMessages = newMessages;
            AllMessages = allMessages;
            Sender = sender;
            OwnerClient = ownerClient;
            SystemClientFactory = systemClientFactory;
            ServiceProvider = serviceProvider;
        }
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

        #region 获取消息及消息列表
        /// <summary>
        /// 获取新消息
        /// </summary>
        /// <returns>新消息列表,参考<see cref="MessageBubble"/></returns>
        public List<MessageBubble> GetNewMessages()
        {
            return NewMessages;
        }
        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <returns>所有消息列表,参考<see cref="MessageBubble"/></returns>
        public List<MessageBubble> GetAllMessages()
        {
            return AllMessages;
        }
        /// <summary>
        /// 获取最后几条消息
        /// </summary>
        /// <param name="count">最后几条消息的数量</param>
        /// <returns>最后几条消息列表,参考<see cref="MessageBubble"/></returns>
        public List<MessageBubble> GetLastMessages(int count)
        {
            return AllMessages.Skip(AllMessages.Count - count).ToList();
        }
        /// <summary>
        /// 获取LLM上下文消息
        /// </summary>
        /// <returns>LLM上下文消息列表</returns>
        public List<string> GetLLMContextMessages()
        {
            return AllMessages.Select(item => $"{item.Who}: {item.MessageContent}").ToList();
        }
        /// <summary>
        /// 获取最后几条LLM上下文消息
        /// </summary>
        /// <param name="count">最后几条消息的数量</param>
        /// <returns>最后几条LLM上下文消息列表</returns>
        public List<string> GetLLMContextMessages(int count)
        {
            return AllMessages.Skip(AllMessages.Count - count).Select(item => $"{item.Who}: {item.MessageContent}").ToList();
        }
        /// <summary>
        /// 获取LLM上下文消息元组
        /// </summary>
        /// <returns>LLM上下文消息元组列表</returns>
        public List<(string who, string message)> GetLLMContextMessagesTuple()
        {
            return AllMessages.Select(item => (item.Who, item.MessageContent)).ToList();
        }

        /// <summary>
        /// 获取最后几条LLM上下文消息元组
        /// </summary>
        /// <param name="count">最后几条消息的数量</param>
        /// <returns>最后几条LLM上下文消息元组列表</returns>
        /// <returns></returns>
        public List<(string who, string message)> GetLLMContextMessagesTuple(int count)
        {
            return AllMessages.Skip(AllMessages.Count - count).Select(item => (item.Who, item.MessageContent)).ToList();
        }

        #endregion
        #region 内部聊天发送消息、发送文件、发送表情等
        #endregion
        #region 向聊天好友外的好友发送消息、发送文件、发送表情等
        #endregion
    }
}
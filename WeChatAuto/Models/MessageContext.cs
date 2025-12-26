using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;
using WeChatAuto.Components;
using WeAutoCommon.Enums;

namespace WeChatAuto.Models
{
    public sealed class MessageContext
    {
        public MessageContext(List<MessageBubble> newMessages, List<MessageBubble> allMessages, Sender sender, WeChatClient ownerClient, WeChatClientFactory systemClientFactory, IServiceProvider serviceProvider, string ownerNickName)
        {
            NewMessages = newMessages;
            AllMessages = allMessages;
            Sender = sender;
            OwnerClient = ownerClient;
            SystemClientFactory = systemClientFactory;
            ServiceProvider = serviceProvider;
            OwnerNickName = ownerNickName;
        }
        /// <summary>
        /// 当前我的微信昵称
        /// </summary>
        public string OwnerNickName { get; set; }
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

        /// <summary>
        /// 是否被人拍一拍
        /// </summary>
        /// <returns>是否被人拍一拍</returns>
        public bool IsBeTap()
        {
            return NewMessages.Any(item => item.MessageType == MessageType.拍一拍 && item.BeTap == OwnerNickName);
        }
        /// <summary>
        /// 是否被@
        /// </summary>
        /// <returns>是否被@</returns>
        public bool IsBeAt()
        {
            return MessageBubbleIsBeAt().Count > 0;
        }
        /// <summary>
        /// 获取被@的消息气泡列表
        /// </summary>
        /// <returns>被@的消息气泡列表</returns>
        public List<MessageBubble> MessageBubbleIsBeAt()
        {
            string prefix = $"@{OwnerNickName} ";
            return NewMessages.Where(item => item.MessageSource == MessageSourceType.好友消息 && item.MessageContent.Contains(prefix)).ToList();
        }
        /// <summary>
        /// 是否被引用
        /// </summary>
        /// <returns>是否被引用</returns>
        public bool IsBeReferenced()
        {
            return NewMessages.Any(item => item.MessageSource == MessageSourceType.好友消息 && item.BeReferencedPersion == OwnerNickName);
        }
        /// <summary>
        /// 获取新消息中我被引用的消息气泡列表
        /// </summary>
        /// <returns>新消息中我被引用的消息气泡列表</returns>
        public List<MessageBubble> MessageBubbleIsReferenced()
        {
            return NewMessages.Where(item => item.MessageSource == MessageSourceType.好友消息 && item.BeReferencedPersion == OwnerNickName).ToList();
        }
        /// <summary>
        /// 获取新消息中引用我的消息气泡列表中我发送的消息气泡列表
        /// </summary>
        /// <returns>新消息中引用我的消息气泡列表中我发送的消息气泡列表</returns>
        public List<MessageBubble> MessageBubbleIsReferencing()
        {
            List<MessageBubble> result = new List<MessageBubble>();
            var currentItem = NewMessages.Where(item => item.MessageSource == MessageSourceType.好友消息 && item.BeReferencedPersion == OwnerNickName).ToList();
            foreach (var item in currentItem)
            {
                var findItem = AllMessages.FirstOrDefault(u => u.MessageSource == MessageSourceType.自己发送消息 && u.Who == item.BeReferencedPersion && u.MessageContent == item.BeReferencedMessage);
                if (findItem != null)
                {
                    result.Add(findItem);
                }
            }
            return result;
        }

        #endregion
        #region 内部聊天发送消息、发送文件、发送表情等
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUserList">被@的用户列表</param>
        public void SendMessage(string message, List<string> atUserList = null)
        {
            Sender.SendMessage(message, atUserList);
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="files">文件路径列表</param>
        public void SendFile(string[] files)
        {
            Sender.SendFile(files);
        }
        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="emoji">表情名称或者描述或者索引</param>
        /// <param name="atUserList">被@的用户列表</param>
        public void SendEmoji(OneOf<int, string> emoji, List<string> atUserList = null)
        {
            Sender.SendEmoji(emoji, atUserList);
        }
        #endregion
        #region 向聊天好友外的好友发送消息、发送文件、发送表情等
        /// <summary>
        /// 向聊天好友外的好友发送消息
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，外部群不能@所有人</param>
        /// <returns></returns>
        public async Task SendMessageToFriend(string who, string message, OneOf<string, string[]> atUser = default)
        {
            await OwnerClient.SendWho(who, message, atUser);
        }
        /// <summary>
        /// 向聊天好友外的好友发送文件
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="files">文件路径列表</param>
        /// <returns></returns>
        public async Task SendFileToFriend(string who, string[] files)
        {
            await OwnerClient.SendFile(who, files);
        }
        /// <summary>
        /// 向聊天好友外的好友发送表情
        /// </summary>
        /// <param name="who">好友昵称</param>
        /// <param name="emoji">表情名称或者描述或者索引</param>
        /// <param name="atUser">被@的用户,最主要用于群聊中@人,可以是一个用户，也可以是多个用户，如果是自有群，可以@所有人，也可以@单个用户，外部群不能@所有人</param>
        /// <returns></returns>
        public async Task SendEmojiToFriend(string who, OneOf<int, string> emoji, OneOf<string, string[]> atUser = default)
        {
            await OwnerClient.SendEmoji(who, emoji, atUser);
        }
        #endregion
    }
}
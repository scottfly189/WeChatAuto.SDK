using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using Newtonsoft.Json;
using System;
using WeChatAuto.Models;
using System.Linq;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区气泡
    /// </summary>
    public class MessageBubble : IEquatable<MessageBubble>
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 消息来源类型
        /// </summary>
        public MessageSourceType MessageSource { get; set; }

        /// <summary>
        /// 发送者，好友或者群聊好友名称
        /// </summary>
        public string Who { get; set; }

        /// <summary>
        /// 群昵称
        /// 适用于群聊消息
        /// 后期做了简化：群聊中GroupNickName=who
        /// </summary>
        public string GroupNickName { get; set; } = "";

        /// <summary>
        /// 点击气泡后执行的操作按钮,有可能为空
        /// </summary>
        public Button ClickActionButton { get; set; } = null;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime? MessageTime { get; set; } = null;

        /// <summary>
        /// 被引用消息的人
        /// 适用于引用消息
        /// 注：目前只有文字消息支持引用
        /// </summary>
        public string BeReferencedPersion { get; set; } = "";
        /// <summary>
        /// 被引用消息的内容
        /// 适用于引用消息
        /// 注：目前只有文字消息支持引用
        /// </summary>
        public string BeReferencedMessage { get; set; } = "";

        /// <summary>
        /// 是否是新消息,读取列表的消息默认为旧消息
        /// </summary>
        public bool IsNew { get; set; } = false;

        /// <summary>
        /// 被拍一拍的人
        /// 适用于拍一拍消息
        /// </summary>
        public string BeTap { get; set; } = "";

        public int[] RuntimeId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is MessageBubble messageBubble)
            {
                if (messageBubble.RuntimeId == default(int[]))
                {
                    return false;
                }
                if (this.RuntimeId == default(int[]))
                {
                    return false;
                }
                return this.RuntimeId.SequenceEqual(messageBubble.RuntimeId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return string.Join("-", this.RuntimeId).GetHashCode();
        }

        public string BubbleHash
        {
            get
            {
                //使用RuntimeId作为哈希值
                return string.Join("-", this.RuntimeId);
                // 暂时不使用哈希值，因为计算速度问题
                // using (var sha256 = System.Security.Cryptography.SHA256.Create())
                // {
                //     var content = $"{this.MessageType.ToString()}|{this.MessageSource.ToString()}|{this.Who}|{this.MessageContent}";
                //     var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(content));
                //     return Convert.ToBase64String(hashBytes);
                // }
            }
        }

        public override string ToString()
        {
            var serializableObject = new
            {
                MessageType = this.MessageType.ToString(),
                MessageSource = this.MessageSource.ToString(),
                this.Who,
                this.GroupNickName,
                this.MessageContent,
                this.IsNew,
                this.BeTap,
                this.BeReferencedPersion,
                this.BeReferencedMessage,
                MessageTime = this.MessageTime.HasValue ? this.MessageTime.Value.ToString() : "",
                BubbleHash = this.BubbleHash,
            };
            return JsonConvert.SerializeObject(serializableObject, Formatting.Indented);
        }
        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <returns></returns>
        public string RrettyPrint()
        {
            return $"{this.Who}: {this.MessageContent}";
        }
        /// <summary>
        /// 转换为ChatSimpleMessage
        /// </summary>
        /// <returns>ChatSimpleMessage</returns>
        public ChatSimpleMessage ToChatSimpleMessage()
        {
            return new ChatSimpleMessage { Who = this.Who, Message = this.MessageContent };
        }
        /// <summary>
        /// 是否可点击
        /// </summary>
        /// <returns>是否可点击</returns>
        public bool IsInvokable()
        {
            return this.ClickActionButton != null;
        }

        public bool Equals(MessageBubble other)
        {
            if (other == null)
            {
                return false;
            }
            if (other.RuntimeId == default(int[]))
            {
                return false;
            }
            if (this.RuntimeId == default(int[]))
            {
                return false;
            }
            return this.RuntimeId.SequenceEqual(other.RuntimeId);
        }
    }
}
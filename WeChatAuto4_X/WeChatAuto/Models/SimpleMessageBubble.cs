using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using MessagePack;
using Newtonsoft.Json;
using WeAutoCommon.Enums;

namespace WeChatAuto.Models
{
    [MessagePackObject]
    public class SimpleMessageBubble : IEquatable<SimpleMessageBubble>
    {
        /// <summary>
        /// 微信名
        /// 如果是自己发送，则值为"我"
        /// 如果是系统发送，则值为"系统"
        /// </summary>
        [Key(1)]
        [JsonProperty("who")]
        public string Who { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [Key(2)]
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// 发送日期,仅精确到分钟
        /// 这个....新的方法应该不起作用，因为UI Tree不带时间，或者说带的时间不具备参考价值
        /// </summary>
        [Key(3)]
        [JsonProperty("send_date")]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Key(5)]
        [JsonProperty("message_type")]
        public MessageType MessageType { get; set; } = MessageType.None;

        /// <summary>
        /// 如果此消息是图片、文件、视频等，并且设置选项可以获取它们，则此字段存放的是他们的路径.
        /// 关于文件：请打开微信选项 “自动下载小于xxMB的文件”， 如果涉及的 文件 比较大，因为微信默认的是自动下载20M，可以设置大一些，如: 100MB
        /// </summary>
        [Key(6)]
        public string FilePath { get; set; }
        /// <summary>
        /// 如果设置选项需要获取图片、文件、视频等，并且此消息为图片、文件、视频类型，则此字段存放的是它们的base64字符串
        /// </summary>
        [IgnoreMember]
        [JsonProperty("image_base64_str")]
        public string Base64Str { get; set; } = "";
        /// <summary>
        /// UI Tree Item的class name
        /// 内部使用
        /// </summary>
        [Key(7)]
        [JsonProperty("ui-class-name")]
        public string UIClassName { get; set; }

        public override string ToString()
        {
            var date = SendDate;
            if (date == default)
            {
                return $"{this.MessageType.ToString()} - {this.Who}: {this.Message} {(string.IsNullOrWhiteSpace(FilePath) ? "" : FilePath)}";
            }
            else
            {
                return $"{this.MessageType.ToString()} - {date.ToString("yyyy-MM-dd HH:mm")} - {this.Who}: {this.Message} {(string.IsNullOrWhiteSpace(FilePath) ? "" : FilePath)}";
            }
        }
        /// <summary>
        /// 得到特征值
        /// </summary>
        /// <returns></returns>
        internal string GetFeature()
        {
            var date = SendDate;
            if (date == default)
            {
                return $"{Who}|{Message}|{MessageType.ToString()}|{UIClassName}";
            }
            else
            {
                return $"{Who}|{Message}|{date.ToString("yyyy-MM-dd HH:mm")}|{MessageType.ToString()}|{UIClassName}";
            }
        }

        public SimpleMessageBubble Clone()
        {
            return new SimpleMessageBubble
            {
                Who = this.Who,
                Message = this.Message,
                SendDate = this.SendDate,
                MessageType = this.MessageType,
                FilePath = this.FilePath,
            };
        }

        public override int GetHashCode()
        {
            return GetFeature().GetHashCode();
        }

        public bool Equals(SimpleMessageBubble other)
        {
            if (other == null)
                return false;
            return this.GetFeature().Equals(other.GetFeature());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SimpleMessageBubble);
        }

    }
}
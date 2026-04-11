using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WeChatAutoSDK_WebSupport.Models
{
    /// <summary>
    /// 发送图片、文件等的模型类
    /// </summary>
    public class AutomationFile
    {
        /// <summary>
        /// 通过谁（自己微信的昵称）发送
        /// </summary>
        [JsonPropertyName("from")]
        public required string From { get; set; }
        /// <summary>
        /// 发送给谁 - 微信好友/群的昵称
        /// </summary>
        [JsonPropertyName("to")]
        public required string To { get; set; }
        /// <summary>
        /// 图片、视频、文件转成的Base64内容
        /// </summary>
        [JsonPropertyName("file_content")]
        public required byte[] FileContent { get; set; }
        /// <summary>
        /// 建议客户端名称,包括后缀名，如: 三月计划.png，三月计划.xlsx
        /// </summary>
        [JsonPropertyName("client_suggestion_file_name")]
        public required string ClientSuggestionFileName { get; set; }
        /// <summary>
        /// message_id - 可选项，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
        /// </summary>
        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }
    }
}

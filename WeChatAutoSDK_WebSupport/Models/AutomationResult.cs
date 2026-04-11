using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WeChatAutoSDK_WebSupport.Models
{
    public class AutomationResult
    {
        /// <summary>
        /// message_id - 可选项，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
        /// </summary>
        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }
        /// <summary>
        /// 自动化成功还是失败
        /// </summary>
        [JsonPropertyName("Success")]
        public bool Success { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 如果失败的描述信息.
        /// </summary>
        [JsonPropertyName("description_if_error")]
        public string? DescriptionIfError { get; set; }
    }
}

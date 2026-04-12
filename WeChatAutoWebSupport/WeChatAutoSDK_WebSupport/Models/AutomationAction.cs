using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using WeChatAutoSDK_WebSupport.Enums;

namespace WeChatAutoSDK_WebSupport.Models
{
    public class AutomationAction
    {
        public ActionTypeEnum ActionType { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public object? Payload { get; set; }

        /// <summary>
        /// message_id - 可选项，如果用户要求返回发送结果时，message_id是必填项。message_id由调用方生成，要求唯一。自动化执行完成后，接口会返回这个message_id和发送结果，方便调用方进行消息发送结果的关联和处理。
        /// </summary>
        public string? MessageId { get; set; }

        public string? Method { get; set; }
        public string? Url { get; set; }
    }
}

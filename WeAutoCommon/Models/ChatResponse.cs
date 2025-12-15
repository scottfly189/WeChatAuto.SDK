using System.Collections.Generic;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// 微信响应结果
    /// </summary>
    public class ChatResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 扩展数据,如果返回有需要额外返回的数据，可以在这里返回
        /// </summary>
        public object ExtendData { get; set; } = null;
    }
}
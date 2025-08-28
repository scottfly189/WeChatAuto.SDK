namespace WxAutoCommon.Models
{
    /// <summary>
    /// 微信响应结果
    /// </summary>
    public class ChatResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
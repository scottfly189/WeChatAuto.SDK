using OneOf;
namespace WxAutoCommon.Models
{
    /// <summary>
    /// 微信消息
    /// </summary>
    public class WxMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 发送给谁,可以单人也可以群发
        /// </summary>
        public OneOf<string, string[]> ToUser { get; set; }
        /// <summary>
        /// @谁,可以单人也可以多人
        /// </summary>
        public OneOf<string, string[]> AtUser { get; set; }
        /// <summary>
        /// 是否打开新窗口发送
        /// </summary>
        public bool IsNewWindow { get; set; }

        /// <summary>
        /// 接口KEY
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
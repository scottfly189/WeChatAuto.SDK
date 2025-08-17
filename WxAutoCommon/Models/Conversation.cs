using WxAutoCommon.Enums;
namespace WxAutoCommon.Models
{
    /// <summary>
    /// 会话
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// 会话类型
        /// </summary>
        public ConversationType ConversationType { get; set; }
        /// <summary>
        /// 会话标题
        /// </summary>
        public string ConversationTitle { get; set; }
        /// <summary>
        /// 会话内容
        /// </summary>
        public string ConversationContent { get; set; }
        /// <summary>
        /// 会话图片
        /// </summary>
        public string ImageName { get; set; }
        public bool IsNotRead { get; set; } = false;
        /// <summary>
        /// 会话时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 是否免打扰
        /// </summary>
        public bool IsDoNotDisturb { get; set; } = false;  //是否免打扰
    }
}
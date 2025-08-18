using System.Text.Json;
using FlaUI.Core.AutomationElements;
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
        /// 是否是企业群
        /// </summary>
        public bool IsCompanyGroup { get; set; } = false;
        /// <summary>
        /// 会话头像按钮
        /// </summary>
        public Button ImageButton { get; set; }
        /// <summary>
        /// 是否有未读消息
        /// </summary>
        public bool HasNotRead { get; set; } = false;
        /// <summary>
        /// 会话时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 是否免打扰
        /// </summary>
        public bool IsDoNotDisturb { get; set; } = false;  //是否免打扰

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
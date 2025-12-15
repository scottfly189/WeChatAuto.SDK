using FlaUI.Core.AutomationElements;
using WeAutoCommon.Enums;
using Newtonsoft.Json;

namespace WeAutoCommon.Models
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
        
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; } = false;

        public override string ToString()
        {
            // 创建一个匿名对象来避免循环引用
            var serializableObject = new
            {
                ConversationType = this.ConversationType.ToString(),
                ConversationTitle = this.ConversationTitle,
                ConversationContent = this.ConversationContent,
                IsCompanyGroup = this.IsCompanyGroup,
                HasNotRead = this.HasNotRead,
                Time = this.Time,
                IsDoNotDisturb = this.IsDoNotDisturb,
                IsTop = this.IsTop,
            };

            return JsonConvert.SerializeObject(serializableObject, Formatting.Indented);
        }
    }
}
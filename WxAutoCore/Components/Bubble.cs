using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using Newtonsoft.Json;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区气泡
    /// </summary>
    public class Bubble
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 消息来源类型
        /// </summary>
        public MessageSourceType MessageSource { get; set; }

        /// <summary>
        /// 发送者，好友或者群聊好友名称
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 点击气泡后执行的操作按钮,有可能为空
        /// </summary>
        public Button ClickActionButton { get; set; } = null;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 引用消息
        /// </summary>
        public Bubble ReferencedBubble { get; set; } = null;

        public override string ToString()
        {
            var serializableObject = new
            {
                MessageType = this.MessageType.ToString(),
                MessageSource = this.MessageSource.ToString(),
                this.Sender,
                this.MessageContent,
            };
            return JsonConvert.SerializeObject(serializableObject, Formatting.Indented);
        }
        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <returns></returns>
        public string RrettyPrint()
        {
            return $"{this.Sender}: {this.MessageContent}";
        }
        /// <summary>
        /// 是否可点击
        /// </summary>
        /// <returns>是否可点击</returns>
        public bool IsInvokable()
        {
            return this.ClickActionButton != null;
        }
    }
}
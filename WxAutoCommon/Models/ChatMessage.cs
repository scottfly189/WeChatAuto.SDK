using System.Collections.Generic;

namespace WxAutoCommon.Models
{
    public class ChatMessage
    {
        /// <summary>
        /// 发送人
        /// </summary>
        public string FromUser {get;set;}
        /// <summary>
        /// 接收人的昵称或者群名
        /// </summary>
        public string ToUser {get;set;}
        /// <summary>
        /// 发送后是否清除消息
        /// </summary>
        public bool IsClear {get;set;} = true;
        /// <summary>
        /// @接收人列表
        /// </summary>
        public List<string> @Users {get;set;} = new List<string>();
        /// <summary>
        /// 是否精确匹配
        /// </summary>
        public bool IsExact {get;set;} = false;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message {get;set;}
    }
}
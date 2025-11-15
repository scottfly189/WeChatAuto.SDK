using System.Collections.Generic;
using System.Threading.Tasks;
using WxAutoCommon.Enums;

namespace WxAutoCommon.Models
{
    /// <summary>
    /// 聊天消息或者操作类型
    /// </summary>
    public class ChatActionMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public ActionType Type { get; set; }
        /// <summary>
        /// 发送给谁
        /// </summary>
        public string ToUser { get; set; }
        public List<string> AtUsers { get; set; } = new List<string>();
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息负载
        /// </summary>
        public object Payload { get; set; }
        /// <summary>
        /// 任务完成源
        /// </summary>
        public TaskCompletionSource<object> Tcs { get; set; }
        /// <summary>
        /// 是否打开聊天子窗口
        /// </summary>
        public bool IsOpenSubWin { get; set; } = true;
    }
}
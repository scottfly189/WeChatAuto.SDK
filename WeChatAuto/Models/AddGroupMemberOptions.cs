using System.Collections.Generic;

namespace WeChatAuto.Models
{
    public class AddGroupMemberOptions
    {
        /// <summary>
        /// 排除列表
        /// </summary>
        public List<string> ExceptList { get; set; } = null;
        /// <summary>
        /// 打招呼文本
        /// </summary>
        public string HelloText { get; set; } = "";
        /// <summary>
        /// 自动增加的好友的标签
        /// </summary>
        public string Label { get; set; } = "";
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int IntervalSecond { get; set; } = 3;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; } = 1;
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; } = 15;
    }
}
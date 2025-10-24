using System;
using System.Collections.Generic;

namespace WxAutoCommon.Models
{
    public class MonentItem
    {
        /// <summary>
        /// 好友名称
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        public List<HistoryCommentItem> HistoryItems { get; set; } = new List<HistoryCommentItem>();
    }

    public class HistoryCommentItem
    {
        /// <summary>
        /// 评论人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
    }
}
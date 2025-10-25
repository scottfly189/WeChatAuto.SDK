using System;
using System.Collections.Generic;
using System.Linq;

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
        /// 评论列表
        /// </summary>
        public List<HistoryCommentItem> HistoryItems { get; set; } = new List<HistoryCommentItem>();
        /// <summary>
        /// 点赞列表
        /// </summary>
        public List<string> Likers { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"From: {From}, Content: {Content}, Time: {Time}, HistoryItems: {string.Join(", ", HistoryItems.Select(h => h.ToString()))}, Likers: {string.Join(", ", Likers)}";
        }
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
        /// <summary>
        /// 回复给谁
        /// </summary>
        public string ReplyTo { get; set; } = "";
    }
}
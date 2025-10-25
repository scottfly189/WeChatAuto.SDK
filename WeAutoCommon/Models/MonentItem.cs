using System;
using System.Collections.Generic;
using System.Linq;

namespace WxAutoCommon.Models
{
    public class MonentItem
    {
        public MonentItem(string nickName)
        {
            _nickName = nickName;
        }
        private string _nickName;
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
        public List<ReplyItem> ReplyItems { get; set; } = new List<ReplyItem>();
        /// <summary>
        /// 点赞列表
        /// </summary>
        public List<string> Likers { get; set; } = new List<string>();

        /// <summary>
        /// 我是否点赞
        /// </summary>
        public bool IsMyLiked
        {
            get
            {
                return Likers.Contains(_nickName);
            }
        }

        public override string ToString()
        {
            return $"From: {From}, Content: {Content}, Time: {Time}, IsMyLiked: {IsMyLiked}, ReplyItems: {string.Join(", ", ReplyItems.Select(h => h.ToString()))}, Likers: {string.Join(", ", Likers)}";
        }
    }

    public class ReplyItem
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

        public override string ToString()
        {
            if (string.IsNullOrEmpty(ReplyTo))
            {
                return $"{From}: {Content}";
            }
            else
            {
                return $"{From} 回复 {ReplyTo}: {Content}";
            }
        }
    }
}
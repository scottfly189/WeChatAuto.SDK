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
        /// 列表项唯一标识
        /// </summary>
        public string ListItemKey { get; set; }
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
        /// <summary>
        /// 我是否是最后一个回复的人
        /// 判断条件：回复列表不为空，且最后一个回复的人是我
        /// </summary>
        public bool IsMyEndReply
        {
            get
            {
                var lastReplyItem = ReplyItems.LastOrDefault();
                if (lastReplyItem == null)
                {
                    return false;
                }
                return lastReplyItem.From == _nickName;
            }
        }

        /// <summary>
        /// 是否包含我的回复
        /// 判断条件：回复列表不为空，且包含我的回复
        /// </summary>
        public bool IsIncludeMyReply
        {
            get
            {
                return ReplyItems.Any(r => r.From == _nickName);
            }
        }

        public string UniqueKey
        {
            get
            {
                return $"From: {From}, Content: {Content}, Time: {Time}";
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
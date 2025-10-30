using System;
using System.Collections.Generic;
using System.Linq;

namespace WxAutoCommon.Models
{
    public class MonentItem : IEquatable<MonentItem>
    {
        public MonentItem(string myNickName)
        {
            _MyNickName = myNickName;
        }
        private string _MyNickName;
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
        /// 列表项名称
        /// </summary>
        public string ListItemName { get; set; }
        /// <summary>
        /// 列表项唯一标识
        /// 假设的标识，不一定准确，但用在自动化过程中足够了
        /// </summary>
        public string ListItemKey
        {
            get => GetListItemKey(ListItemName);
        }
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
                return Likers.Contains(_MyNickName);
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
                return lastReplyItem.From == _MyNickName;
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
                return ReplyItems.Any(r => r.From == _MyNickName);
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
            return $"From: {From}, Content: {Content}, Time: {Time}, IsMyEndReply: {IsMyEndReply}, IsIncludeMyReply: {IsIncludeMyReply}, IsMyLiked: {IsMyLiked}, ReplyItems: {string.Join(", ", ReplyItems.Select(h => h.ToString()))}, Likers: {string.Join(", ", Likers)}";
        }

        public bool Equals(MonentItem other)
        {
            if (other == null)
            {
                return false;
            }
            return this.ListItemKey == other.ListItemKey;
        }

        public override bool Equals(Object other)
        {
            return this.ListItemKey == (other as MonentItem).ListItemKey;
        }

        public override int GetHashCode()
        {
            return this.ListItemKey.GetHashCode();
        }
        /// <summary>
        /// 获取列表项唯一标识
        /// </summary>
        /// <param name="listItemName"></param>
        /// <returns></returns>
        public static string GetListItemKey(string listItemName)
        {
            //由于时间总是变化，所以需要从列表项名称中去掉时间，只保留除时间以外的其他内容，以作为唯一标识
            var arrayListItemName = listItemName.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Reverse();
            var time = "";
            foreach (var str in arrayListItemName)
            {
                if (str.Contains("秒"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("分钟"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("小时"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("天"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("月"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("日"))
                {
                    time = str;
                    break;
                }
                if (str.Contains("年"))
                {
                    time = str;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(time))
            {
                arrayListItemName = arrayListItemName.Where(item => !item.Equals(time));
            }
            arrayListItemName = arrayListItemName.Reverse();
            var key = string.Join("\n", arrayListItemName);
            return key;
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
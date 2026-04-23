using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using OneOf;
using WeAutoCommon.Enums;

namespace WeAutoCommon.Models
{
    /// <summary>
    /// 个人信息
    /// </summary>
    public class FriendInfo
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 备注名
        /// </summary>
        public string MemoName { get; set; }
        /// <summary>
        /// 地区,建议仅供参考
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Lable { get; set; }
        /// <summary>
        /// 共同群数量
        /// </summary>
        public string SameGroupNumber { get; set; } = "0个";
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string WxId { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        public string AvatarPath { get; set; }
        /// <summary>
        /// 头像Image
        /// <code>
        /// //调用示例
        /// var image = xxxx.AvatarImage;
        /// image.Save(xxxxx)
        /// </code>
        /// </summary>
        public Image AvatarImage { get; set; }
        /// <summary>
        /// 查询结果，三种查询结果：已是好友、未查询到或不支持手机号查询、能查询到，但不是好友.
        /// 具体结果请参见:<seealso cref="FriendSearchResultEnums"/>
        /// </summary>
        public FriendSearchResultEnums FriendSearchResult { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
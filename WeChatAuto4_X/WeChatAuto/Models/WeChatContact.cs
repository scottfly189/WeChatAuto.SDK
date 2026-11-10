using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 微信联系人,对应微信联系人表 Contact
    /// 支持多微信聚合。差异同步业务键：FromWechatWxId + UserName（对应原生 contact.username）。
    /// </summary>
    [SugarTable("wechat_contact")]
    [SugarIndex("index_from_webchat",nameof(FromWechat),OrderByType.Asc)]
    [SugarIndex("index_from_webchat_wxid",nameof(FromWechatWxId),OrderByType.Asc)]
    [SugarIndex("index_wxid",nameof(UserName),OrderByType.Asc)]
    [SugarIndex("index_nickname",nameof(NickName),OrderByType.Asc)]
    [SugarIndex("index_remark",nameof(Remark),OrderByType.Asc)]
    [SugarIndex("uk_contact",
        nameof(WeChatContact.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatContact.UserName), OrderByType.Asc,
        IsUnique = true)]
    public class WeChatContact
    {
        /// <summary>
        /// 自定义ID,自增，唯一
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 联系人所属微信昵称
        /// 表示这于哪个微信账号,应用于多微信号场景
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此联系人属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;
        /// <summary>
        /// 联系人所属微信的wxid
        /// 表示这于哪个微信账号的wxid,应用于多微信场景
        /// 由我们获取本机的wxid后自动填入
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此联系人属于哪个微信昵称")]
        public string FromWechatWxId { get; set; } = string.Empty;
        /// <summary>
        /// 对应微信数据Contact.db的 Contract 表的 id 字段
        /// </summary>
        public int WxContactId { get; set; }
        /// <summary>
        /// 对应微信数据库Contact.db的 Contact表的 local_type 字段
        /// </summary>
        public int LocalType { get; set; }
        /// <summary>
        /// 微信号人肉眼所见的名称
        /// 对应微信的: 显示优先级 remark > nick > username
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ShowName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Remark))
                {
                    return Remark;
                }
                if (!string.IsNullOrWhiteSpace(Alias))
                {
                    return Alias;
                }
                if (!string.IsNullOrWhiteSpace(NickName))
                {
                    return NickName;
                }
                return UserName;
            }
        }

        /// <summary>
        /// wxid,对应微信数据库Contact.db的username字段
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 昵称，对应微信数据库Contact.db的 nick_name 字段
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 微信号别名，对方设置的 微信号
        /// 对应微信数据库 Contact.db 的 alias 字段
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// 备注名,对应微信数据库contact.db的 remark 字段
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 图像大图，对应微信数据库 Contact.db 的 big_head_url
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string BigHeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 图像小图，对象微信数据库 Contact.db 的 small_head_url
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string SmallHeadUrl { get; set; } = string.Empty;

    }
}
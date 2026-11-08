using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 微信联系人,对应微信联系人表 Contact
    /// 支持多微信聚合
    /// </summary>
    [SugarTable("wechat_contact")]
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
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "此联系人属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;
        /// <summary>
        /// 联系人所属微信wxid
        /// 表示这于哪个微信账号的wxid,应用于多微信号场景
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "此联系人属于哪个微信昵称")]
        public string FromWechatWxId {get;set;} = string.Empty;
        /// <summary>
        /// 对应微信数据Contact.db的 Contract 表的 id 字段
        /// </summary>
        public int WxContactId { get; set; }
        /// <summary>
        /// 对应微信数据库Contact.db的 Contact表的 local_type 字段
        /// </summary>
        public int LocalType { get; set; }
        /// <summary>
        /// SDK 内部定义的客户端名称，例如 default、wx1、wx2
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// wxid,对应微信数据库Contact.db的username字段
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string WxId { get; set; } = string.Empty;
        /// <summary>
        /// 昵称，对应微信数据库Contact.db的 nick_name 字段
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 备注名,对应微信数据库contact.db的 remark 字段
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 图像大图，对应微信数据库 Contact.db 的 big_head_url
        /// </summary>
        [SugarColumn(Length = 1000, IsNullable = true)]
        public string BigHeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 图像小图，对象微信数据库 Contact.db 的 small_head_url
        /// </summary>
        [SugarColumn(Length = 1000, IsNullable = true)]
        public string SmallHeadUrl { get; set; } = string.Empty;

    }
}
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

        /// <summary>
        /// 国家 / 地区代码（ISO2，如 CN / HK / US）。
        /// 取自 contact.extra_buffer protobuf #5；未设置时为空串。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "国家/地区代码(ISO2)，来自 contact.extra_buffer #5")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 省 / 直辖市 / 州。取自 contact.extra_buffer #6，同步时经 <c>WxRegionNames</c> 译成中文；
        /// 未命中的境外州名保留英文原文。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "省/州，来自 contact.extra_buffer #6（同步时英译中）")]
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市 / 区。取自 contact.extra_buffer #7，同步时译成中文；未命中保留英文原文。
        /// 微信对直辖市会把区写在这里（如 Haidian → 海淀）。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "市/区，来自 contact.extra_buffer #7（同步时英译中）")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 微信标签名，逗号分隔（如 <c>声音,wechatauto22</c>）。
        /// 取自 contact.extra_buffer #30 的 label id，同步时用同轮读出的 <c>contact_label</c> 译成名字；
        /// 之后微信侧改名 / 删标签不会回改这里（同步不重扫标签表）。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "微信标签名，逗号分隔，来自 contact.extra_buffer #30 + contact_label")]
        public string Labels { get; set; } = string.Empty;

        /// <summary>
        /// 性别：0 未知 / 1 男 / 2 女。取自 contact.extra_buffer #2。
        /// </summary>
        [SugarColumn(ColumnDescription = "性别：0未知/1男/2女，来自 contact.extra_buffer #2")]
        public int Gender { get; set; }

        /// <summary>
        /// 来源场景码，取自 contact.extra_buffer #8（1 QQ / 3 微信号 / 6 手机号 / 10 名片 /
        /// 14 群聊 / 15 搜索手机号 / 17 名片分享 / 30 扫一扫）。0 = 原生库没写。
        /// </summary>
        [SugarColumn(ColumnDescription = "来源场景码，来自 contact.extra_buffer #8；0=未知")]
        public int SourceScene { get; set; }

        /// <summary>
        /// 来源中文文案（由 <see cref="SourceScene"/> 翻译，如「通过群聊添加」）；未知码写「场景码 N」。
        /// 同步时落库成列，方便直接 SQL 查 / 导出。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "来源文案，由 SourceScene 翻译")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// 添加（通过好友 / 关注公众号）时间，unix 秒。取自 contact.extra_buffer #41；
        /// 0 表示原生库没有写该字段（从 3.x 迁移过来的老联系人大多没有）。
        /// </summary>
        [SugarColumn(ColumnDescription = "添加时间，unix 秒，来自 contact.extra_buffer #41；0=未知")]
        public long AddTime { get; set; }

        /// <summary>
        /// 国家/地区中文名（<see cref="Country"/> 的 ISO2 码翻译，如 CN → 中国大陆）。不落库。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CountryName => WxCountryNames.ToChinese(Country);

        /// <summary>
        /// 展示用地区串：<c>国家·省·市</c>（如 <c>中国大陆·广东·深圳</c>），对齐上游口径。不落库。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Region => WxRegionNames.FormatRegion(Country, Province, City);
    }
}
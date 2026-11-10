using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 朋友圈点赞中间表。完整名单来自 SnsTimeLine.content XML 的 like_user_list（条目 type 一律为 1）。
    /// 同一条若在 SnsMessage_tmp3.serialized_comment_buf 有包，重叠字段
    ///（发起人、昵称、时间、comment_id、flag）以 protobuf 为准；名单本身不在包里，仍以 XML 列表为准。
    /// </summary>
    [SugarTable("wechat_sns_like")]
    [SugarIndex("uk_sns_like",
        nameof(WeChatSnsLike.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatSnsLike.Tid), OrderByType.Asc,
        nameof(WeChatSnsLike.UserName), OrderByType.Asc,
        IsUnique = true)]
    [SugarIndex("index_sns_like_from", nameof(WeChatSnsLike.FromWechatWxId), OrderByType.Asc)]
    [SugarIndex("index_sns_like_tid", nameof(WeChatSnsLike.Tid), OrderByType.Asc)]
    public class WeChatSnsLike
    {
        /// <summary>自定义主键，自增。</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>此记录属于哪个微信昵称（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此点赞属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>此记录属于哪个微信 wxid（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此点赞属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>所属动态 tid（有符号，与 <see cref="WeChatSnsPost.Tid"/> 相同）。</summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "所属动态 tid（有符号）")]
        public long Tid { get; set; }

        /// <summary>所属动态 tid 的无符号十进制，便于对照 XML。</summary>
        [SugarColumn(Length = 32, IsNullable = false, ColumnDescription = "所属动态 tid（无符号十进制）")]
        public string TidUnsigned { get; set; } = string.Empty;

        /// <summary>
        /// 点赞条目 id，对应 XML &lt;comment_id&gt; / protobuf field 10。
        /// 有包时以 protobuf 为准。
        /// </summary>
        [SugarColumn(ColumnDescription = "点赞 comment_id")]
        public long CommentId { get; set; }

        /// <summary>点赞人 username，对应 XML &lt;username&gt; / protobuf field 1；有包时以 protobuf 为准。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "点赞人 username")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 点赞人显示名。XML / protobuf 已有 nickname（protobuf field 3，有包时以包为准）；
        /// 写入中间表时按 <see cref="UserName"/> 查 Contact，Remark 优先，空则保留来源昵称。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "点赞人显示名")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>点赞时间，unix 秒。对应 XML &lt;create_time&gt; / protobuf field 7；有包时以 protobuf 为准。</summary>
        [SugarColumn(ColumnDescription = "点赞时间（unix 秒）")]
        public long CreateTime { get; set; }

        /// <summary>标志，对应 XML &lt;comment_flag&gt; / protobuf field 15；有包时以 protobuf 为准。位含义未逐项核实。</summary>
        [SugarColumn(ColumnDescription = "comment_flag")]
        public long CommentFlag { get; set; }
    }
}

using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 朋友圈评论中间表。完整名单来自 SnsTimeLine.content XML 的 comment_user_list（条目 type 一律为 2）。
    /// 同一条若在 SnsMessage_tmp3.serialized_comment_buf 有包，重叠字段
    ///（发起人、对象、类型、时间、正文、comment_id、flag）以 protobuf 为准；名单本身不在包里，仍以 XML 列表为准。
    /// 回复的父评：serialized_ref_buf 为完整包时，父评正文与发起人优先用包；否则用 XML 的 ref_comment_id / ref_username。
    /// </summary>
    [SugarTable("wechat_sns_comment")]
    [SugarIndex("uk_sns_comment",
        nameof(WeChatSnsComment.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatSnsComment.Tid), OrderByType.Asc,
        nameof(WeChatSnsComment.CommentId), OrderByType.Asc,
        IsUnique = true)]
    [SugarIndex("index_sns_comment_from", nameof(WeChatSnsComment.FromWechatWxId), OrderByType.Asc)]
    [SugarIndex("index_sns_comment_tid", nameof(WeChatSnsComment.Tid), OrderByType.Asc)]
    public class WeChatSnsComment
    {
        /// <summary>自定义主键，自增。</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>此记录属于哪个微信昵称（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此评论属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>此记录属于哪个微信 wxid（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此评论属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>所属动态 tid（有符号，与 <see cref="WeChatSnsPost.Tid"/> 相同）。</summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "所属动态 tid（有符号）")]
        public long Tid { get; set; }

        /// <summary>所属动态 tid 的无符号十进制，便于对照 XML。</summary>
        [SugarColumn(Length = 32, IsNullable = false, ColumnDescription = "所属动态 tid（无符号十进制）")]
        public string TidUnsigned { get; set; } = string.Empty;

        /// <summary>
        /// 评论 id，对应 XML &lt;comment_id&gt; / protobuf field 10。
        /// 有包时以 protobuf 为准。
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "评论 comment_id")]
        public long CommentId { get; set; }

        /// <summary>评论人 username，对应 XML &lt;username&gt; / protobuf field 1；有包时以 protobuf 为准。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "评论人 username")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 评论人显示名。XML / protobuf 已有 nickname（protobuf field 3，有包时以包为准）；
        /// 写入中间表时按 <see cref="UserName"/> 查 Contact，Remark 优先，空则保留来源昵称。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "评论人显示名")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>评论对象 username，对应 protobuf field 2；无包时可能为空（XML 条目不一定带此字段）。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "评论对象 username")]
        public string ToUserName { get; set; } = string.Empty;

        /// <summary>评论对象显示名，对应 protobuf field 4；无包时可能为空。写入时可再按 Contact 补。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "评论对象显示名")]
        public string ToNickName { get; set; } = string.Empty;

        /// <summary>评论正文，对应 XML &lt;content&gt; / protobuf field 8；有包时以 protobuf 为准。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "评论正文")]
        public string Content { get; set; } = string.Empty;

        /// <summary>条目类型。本表来自评论名单，应为 2。对应 XML &lt;type&gt; / protobuf field 5；有包时以 protobuf 为准。</summary>
        [SugarColumn(ColumnDescription = "类型（2=评论）")]
        public int Type { get; set; } = 2;

        /// <summary>评论时间，unix 秒。对应 XML &lt;create_time&gt; / protobuf field 7；有包时以 protobuf 为准。</summary>
        [SugarColumn(ColumnDescription = "评论时间（unix 秒）")]
        public long CreateTime { get; set; }

        /// <summary>父评 id，对应 XML &lt;ref_comment_id&gt;；0 表示不是回复。完整 ref 包若带 comment_id，以包为准。</summary>
        [SugarColumn(ColumnDescription = "父评 comment_id（回复时非 0）")]
        public long RefCommentId { get; set; }

        /// <summary>父评作者 username，对应 XML &lt;ref_username&gt;；完整 ref 包（field 1）优先。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "父评作者 username")]
        public string RefUserName { get; set; } = string.Empty;

        /// <summary>父评正文。仅当 serialized_ref_buf 为完整包（field 8）时能填；XML 名单本身不含父评正文。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "父评正文（完整 ref 包才有）")]
        public string RefContent { get; set; } = string.Empty;

        /// <summary>标志，对应 XML &lt;comment_flag&gt; / protobuf field 15；有包时以 protobuf 为准。位含义未逐项核实。</summary>
        [SugarColumn(ColumnDescription = "comment_flag")]
        public long CommentFlag { get; set; }

        /// <summary>是否已删，对应 XML &lt;b_deleted&gt;。</summary>
        [SugarColumn(ColumnDescription = "是否已删（b_deleted）")]
        public int IsDeleted { get; set; }
    }
}

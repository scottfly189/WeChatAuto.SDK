using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 朋友圈动态中间表，对应原生 SnsTimeLine（按 tid）。支持多微信聚合。
    /// 正文、媒体、类型、点赞标记、发布时间主要来自 content XML（SnsDataItem / TimelineObject）。
    /// pack_info_buf 是 protobuf，可以读，但本机绝大多数是空包，有值时只重复 tid 与时间，
    /// 不含正文或互动名单；CreateTime 以 XML &lt;createTime&gt; 为准，包内 field 2 有值时作校对。
    /// 完整点赞 / 评论不进本表，分别写入 <see cref="WeChatSnsLike"/> / <see cref="WeChatSnsComment"/>。
    /// </summary>
    [SugarTable("wechat_sns_post")]
    [SugarIndex("uk_sns_post",
        nameof(WeChatSnsPost.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatSnsPost.Tid), OrderByType.Asc,
        IsUnique = true)]
    [SugarIndex("index_sns_post_from", nameof(WeChatSnsPost.FromWechatWxId), OrderByType.Asc)]
    [SugarIndex("index_sns_post_user", nameof(WeChatSnsPost.UserName), OrderByType.Asc)]
    [SugarIndex("index_sns_post_time", nameof(WeChatSnsPost.CreateTime), OrderByType.Desc)]
    public class WeChatSnsPost
    {
        /// <summary>自定义主键，自增。</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>此记录属于哪个微信昵称（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此动态属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>此记录属于哪个微信 wxid（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此动态属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>
        /// 动态 id，对应 SnsTimeLine.tid（有符号 INTEGER，与原生库 JOIN 用这个值）。
        /// XML 里的 &lt;id&gt; / &lt;tid&gt; 是同一值的无符号十进制，见 <see cref="TidUnsigned"/>。
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "动态 tid（有符号，与原生库一致）")]
        public long Tid { get; set; }

        /// <summary>
        /// 动态 id 的无符号十进制字符串，便于对照 XML 与展示。
        /// 写入时按 unchecked((ulong)Tid).ToString() 计算。
        /// </summary>
        [SugarColumn(Length = 32, IsNullable = false, ColumnDescription = "动态 tid（无符号十进制）")]
        public string TidUnsigned { get; set; } = string.Empty;

        /// <summary>发布者 username，对应 SnsTimeLine.user_name / XML &lt;username&gt;。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "发布者 username")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 发布者显示名。XML LocalExtraInfo 已有 nickname；
        /// 写入中间表时按 <see cref="UserName"/> 查 Contact，Remark 优先，空则保留来源昵称。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "发布者显示名")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间，unix 秒。以 XML &lt;createTime&gt; 为准；
        /// 若 pack_info_buf protobuf field 2 有值，可作校对（本机与 XML 相同或早 1～2 秒）。
        /// </summary>
        [SugarColumn(ColumnDescription = "发布时间（unix 秒）")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 动态形态，对应 XML ContentObject/type。
        /// 已知：1 图片，2 纯文字，3 链接/分享，4 音乐（样本少），5 App/音乐分享（样本少），
        /// 7/15 视频（未按文件坐实），28 视频号动态，30 未核实，34 视频号直播，54 Live Photo。
        /// 不要用「某标签在不在」判断类型。
        /// </summary>
        [SugarColumn(ColumnDescription = "动态类型（ContentObject type）")]
        public int ContentType { get; set; }

        /// <summary>正文，对应 XML &lt;contentDesc&gt;；纯图片/视频可能为空。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "动态正文")]
        public string ContentDesc { get; set; } = string.Empty;

        /// <summary>我是否赞过，对应 XML &lt;like_flag&gt;（1 = 是）。</summary>
        [SugarColumn(ColumnDescription = "我是否赞过（like_flag）")]
        public int LikeFlag { get; set; }

        /// <summary>点赞人数，写入时按 like_user_list 条数填入，便于单表阅读。</summary>
        [SugarColumn(ColumnDescription = "点赞人数")]
        public int LikeCount { get; set; }

        /// <summary>评论条数，写入时按 comment_user_list 条数填入，便于单表阅读。</summary>
        [SugarColumn(ColumnDescription = "评论条数")]
        public int CommentCount { get; set; }

        /// <summary>
        /// 原生 content 的 XML 快照（含媒体列表等）。
        /// 点赞 / 评论已拆到另外两张中间表，查询互动不必再解析本字段。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "原生 content XML")]
        public string Content { get; set; } = string.Empty;
    }
}

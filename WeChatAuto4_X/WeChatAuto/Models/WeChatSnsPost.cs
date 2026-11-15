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
    [SugarIndex("index_sns_post_deleted", nameof(WeChatSnsPost.IsDeleted), OrderByType.Asc)]
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

        /// <summary>
        /// <see cref="Content"/> 的 SHA256（小写十六进制）。用于增量同步时判断旧帖是否被改写：
        /// SnsTimeLine 没有更新时间列，点赞/评论/编辑都会改写 content。
        /// 为空表示该行是早期版本写入、尚未计算，下次同步会重算。
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "content XML 的 SHA256")]
        public string ContentHash { get; set; } = string.Empty;

        /// <summary>
        /// 原生库里这条动态已经不存在（被作者删除 / 缓存清理）。软删除，保留历史与互动数据。
        /// 再次出现在 SnsTimeLine 时会自动改回 false。
        /// </summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "原生已不存在（软删除），NULL 视为 false（旧库升级）")]
        public bool? IsDeleted { get; set; }

        /// <summary>链接/分享卡片的标题，对应 XML ContentObject/title（图片/纯文字/视频通常为空）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "链接标题")]
        public string Title { get; set; } = string.Empty;

        /// <summary>链接/分享卡片的描述，对应 XML ContentObject/description。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "链接描述")]
        public string Description { get; set; } = string.Empty;

        /// <summary>链接/分享卡片的跳转地址，对应 XML ContentObject/contentUrl。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "链接地址")]
        public string ContentUrl { get; set; } = string.Empty;

        /// <summary>
        /// 位置的城市名，对应 XML location@city（原生多数行只有空的 location 占位节点）。
        /// 经纬度见 <see cref="LocationLatitude"/> / <see cref="LocationLongitude"/>。
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true, ColumnDescription = "位置城市")]
        public string PoiCity { get; set; } = string.Empty;

        /// <summary>
        /// 位置纬度，取 XML location@latitude 原值。<b>注意</b>：本机实测微信把经纬度放反了
        /// （北京记录为 latitude=116.5 / longitude=39.76），这里保留原值不做交换，避免二次解释出错。
        /// </summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "位置纬度（原生原值，可能被微信放反）")]
        public double LocationLatitude { get; set; }

        /// <summary>位置经度，取 XML location@longitude 原值（同上，可能被微信放反）。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "位置经度（原生原值，可能被微信放反）")]
        public double LocationLongitude { get; set; }

        /// <summary>转发来源 username，对应 XML TimelineObject/sourceUserName（本机多为空）。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "转发来源 username")]
        public string SourceUserName { get; set; } = string.Empty;

        /// <summary>转发来源显示名，对应 XML TimelineObject/sourceNickName。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "转发来源昵称")]
        public string SourceNickName { get; set; } = string.Empty;

        /// <summary>公众号相关：XML TimelineObject/publicUserName（本机 111 条有值）。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "公众号 username")]
        public string PublicUserName { get; set; } = string.Empty;

        /// <summary>公众号相关：XML TimelineObject/publicBrandContactType。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "公众号品牌类型")]
        public int PublicBrandContactType { get; set; }

        /// <summary>是否私密（仅自己可见），对应 XML TimelineObject/private ≠ 0。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "是否私密")]
        public bool? IsPrivate { get; set; }

        /// <summary>是否置顶，对应 XML TimelineObject/isTop ≠ 0。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "是否置顶")]
        public bool? IsTop { get; set; }

        /// <summary>是否被折叠，对应 XML TimelineObject/sightFolded ≠ 0。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "是否折叠")]
        public bool? SightFolded { get; set; }

        /// <summary>展示位标志，对应 XML TimelineObject/showFlag 原值。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "showFlag 原值")]
        public int ShowFlag { get; set; }

        /// <summary>
        /// 视频号形态：0 = 普通朋友圈动态，1 = 视频号动态（ContentObject/finderFeed），
        /// 2 = 视频号直播（ContentObject/finderLive）。
        /// </summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "视频号形态：0普通/1视频号/2直播")]
        public int FinderKind { get; set; }

        /// <summary>视频号的 objectId 或直播的 finderLiveID。</summary>
        [SugarColumn(Length = 120, IsNullable = true, ColumnDescription = "视频号/直播 id")]
        public string FinderId { get; set; } = string.Empty;

        /// <summary>视频号/直播的作者昵称。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "视频号作者昵称")]
        public string FinderNickName { get; set; } = string.Empty;

        /// <summary>视频号/直播的作者 username（@finder 后缀）。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "视频号作者 username")]
        public string FinderUsername { get; set; } = string.Empty;

        /// <summary>视频号/直播的文案描述。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "视频号文案")]
        public string FinderDesc { get; set; } = string.Empty;

        /// <summary>
        /// 视频号媒体文件链接：XML <c>ContentObject/finderFeed/mediaList/media/url</c>。
        /// 带 <c>token/encfilekey</c> 的 CDN 直链，会过期；实测脱离微信客户端直接请求返回 400，
        /// 仅作留档 / 后续研究，不保证可下载。直播（finderLive）没有播放直链，本字段为空。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "视频号媒体文件链接（CDN，可能过期）")]
        public string FinderUrl { get; set; } = string.Empty;

        /// <summary>
        /// 视频号/直播封面链接：<c>finderFeed/mediaList/media/thumbUrl</c> 或
        /// <c>finderLive/media/coverUrl</c>。实测为明文 JPEG，可直接下载。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "视频号/直播封面链接")]
        public string FinderCoverUrl { get; set; } = string.Empty;

        /// <summary>视频号 nonceId：<c>objectNonceId</c> / <c>finderNonceID</c>，留作以后拼接页面链接。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "视频号 nonceId")]
        public string FinderNonceId { get; set; } = string.Empty;

        /// <summary>本条动态的媒体数量（ContentObject/mediaList/media 个数），明细见 wechat_sns_media。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "媒体数量")]
        public int MediaCount { get; set; }
    }
}



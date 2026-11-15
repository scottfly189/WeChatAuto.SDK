using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 朋友圈媒体中间表：一条动态可以有多张图 / 多个视频（含 LivePhoto 的视频段）。
    /// 数据来自 <c>SnsTimeLine.content</c> 的 <c>ContentObject/mediaList/media</c>，按 <c>(Tid, Idx)</c> 唯一。
    /// <para>URL 是带 token 的 CDN 地址，会过期；<see cref="LocalPath"/> 预留给后续的媒体下载/缓存。</para>
    /// <para>视频号（finderFeed）的媒体列表结构与朋友圈 media 不同，暂不写入本表，见
    /// <see cref="WeChatSnsPost.FinderKind"/> / <see cref="WeChatSnsPost.FinderId"/>。</para>
    /// </summary>
    [SugarTable("wechat_sns_media")]
    [SugarIndex("uk_sns_media",
        nameof(WeChatSnsMedia.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatSnsMedia.Tid), OrderByType.Asc,
        nameof(WeChatSnsMedia.Idx), OrderByType.Asc,
        IsUnique = true)]
    [SugarIndex("index_sns_media_from", nameof(WeChatSnsMedia.FromWechatWxId), OrderByType.Asc)]
    [SugarIndex("index_sns_media_tid", nameof(WeChatSnsMedia.Tid), OrderByType.Asc)]
    public class WeChatSnsMedia
    {
        /// <summary>自定义主键，自增。</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>此记录属于哪个微信昵称（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此媒体属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>此记录属于哪个微信 wxid（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此媒体属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>所属动态 tid（有符号，与 <see cref="WeChatSnsPost.Tid"/> 相同）。</summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "所属动态 tid（有符号）")]
        public long Tid { get; set; }

        /// <summary>所属动态 tid 的无符号十进制，便于对照 XML。</summary>
        [SugarColumn(Length = 32, IsNullable = false, ColumnDescription = "所属动态 tid（无符号十进制）")]
        public string TidUnsigned { get; set; } = string.Empty;

        /// <summary>在 mediaList 中的顺序，从 0 开始。</summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "媒体在动态内的顺序，从 0 开始")]
        public int Idx { get; set; }

        /// <summary>媒体 id，对应 XML media/id。</summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "媒体 id")]
        public string MediaId { get; set; } = string.Empty;

        /// <summary>
        /// 媒体类型，对应 XML media/type（本机：2 图片、4/5/6 视频类、3 其它）。
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "媒体类型（media/type）")]
        public int MediaType { get; set; }

        /// <summary>媒体子类型，对应 XML media/sub_type。</summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "媒体子类型（media/sub_type）")]
        public int SubType { get; set; }

        /// <summary>媒体标题，对应 XML media/title（少数视频才有）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "媒体标题")]
        public string Title { get; set; } = string.Empty;

        /// <summary>媒体描述，对应 XML media/description（视频封面文案等）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "媒体描述")]
        public string Description { get; set; } = string.Empty;

        /// <summary>原图 / 原视频地址，对应 XML media/url 的值（带 token，会过期）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "媒体地址（带 token）")]
        public string Url { get; set; } = string.Empty;

        /// <summary>媒体文件 md5，对应 XML media/url@md5。</summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "媒体 md5")]
        public string UrlMd5 { get; set; } = string.Empty;

        /// <summary>视频文件 md5，对应 XML media/url@videomd5（仅视频）。</summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "视频 md5")]
        public string VideoMd5 { get; set; } = string.Empty;

        /// <summary>缩略图地址，对应 XML media/thumb 的值（带 token，会过期）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "缩略图地址")]
        public string ThumbUrl { get; set; } = string.Empty;

        /// <summary>低码率视频地址，对应 XML media/lowBandUrl（部分视频有）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "低码率视频地址")]
        public string LowBandUrl { get; set; } = string.Empty;

        /// <summary>超清视频地址，对应 XML media/uhd（部分视频有）。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "超清视频地址")]
        public string UhdUrl { get; set; } = string.Empty;

        /// <summary>宽，对应 XML media/size@width（像素）。</summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "宽（像素）")]
        public int Width { get; set; }

        /// <summary>高，对应 XML media/size@height（像素）。</summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "高（像素）")]
        public int Height { get; set; }

        /// <summary>文件大小，对应 XML media/size@totalSize（字节，可能为空）。</summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "文件大小（字节）")]
        public long FileSize { get; set; }

        /// <summary>视频时长（秒），对应 XML media/videoDuration，图片为 0。</summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "视频时长（秒）")]
        public double VideoDuration { get; set; }

        /// <summary>该媒体是否带 LivePhoto 段，对应 XML media/LivePhoto 节点。</summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否 LivePhoto")]
        public bool IsLivePhoto { get; set; }

        /// <summary>
        /// 预留：媒体下载到本机后的路径。URL 会过期，需要长期使用时由媒体同步写入这里。
        /// 只有开启 <c>WeChatConfig.DownloadSnsImages</c> 且下载成功时才写。
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "本地缓存路径（下载成功时写入）")]
        public string LocalPath { get; set; } = string.Empty;

        /// <summary>原图 / 原视频 URL 的 token 参数（XML <c>url@token</c>），拼接请求 URL 时必须带上。</summary>
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "URL token 参数")]
        public string UrlToken { get; set; } = string.Empty;

        /// <summary>
        /// 原图 / 原视频 URL 的 key（XML <c>url@key</c>），WxIsaac64 的种子。
        /// 响应头 <c>X-Enc</c> 非 0 时用它解密。
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "URL key（WxIsaac64 种子）")]
        public string UrlKey { get; set; } = string.Empty;

        /// <summary>缩略图 URL 的 token 参数（XML <c>thumb@token</c>）。</summary>
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "缩略图 token 参数")]
        public string ThumbToken { get; set; } = string.Empty;

        /// <summary>
        /// 视频解密 key：XML <c>media/enc@key</c>（本机样例形如 <c>&lt;enc key="1170354472"&gt;1&lt;/enc&gt;</c>），
        /// 也是 WxIsaac64 的种子。视频只加密前 128KB，用它生成 keystream 做前缀 XOR。
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true, ColumnDescription = "视频解密 key（enc@key）")]
        public string VideoKey { get; set; } = string.Empty;

        /// <summary>
        /// 下载状态：0 未处理 / 1 成功 / 2 永久失败（token 过期、404、内容不是图片）/ 3 跳过（非图片）。
        /// 永久失败不会重试；若动态内容后续变化，会按 tid 重建媒体行，新行重新从 0 开始。
        /// </summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "下载状态 0未处理/1成功/2永久失败/3跳过")]
        public int DownloadStatus { get; set; }

        /// <summary>已尝试下载次数（仅对可重试错误累加，到 3 次转永久失败）。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "下载尝试次数")]
        public int DownloadAttempts { get; set; }

        /// <summary>最近一次失败原因（截断到 200 字符），便于排查；成功时清空。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "下载失败原因")]
        public string DownloadError { get; set; } = string.Empty;

        /// <summary>下载成功时间（unix 秒）；未成功为 0。</summary>
        [SugarColumn(IsNullable = true, DefaultValue = "0", ColumnDescription = "下载成功时间（unix 秒）")]
        public long DownloadedAt { get; set; }
    }
}



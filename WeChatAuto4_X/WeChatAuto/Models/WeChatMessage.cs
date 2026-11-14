using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 微信消息中间表。UIA 监听与 Database Read 共用；支持多微信聚合。
    /// 对照原生 message_0/1/2.db 的 Msg_*（各表同构）：差异同步业务键为
    /// FromWechatWxId + ChatUsername + Shard + LocalId（ServerId 作辅助，不建外键）。
    /// 本机有 89 个会话跨 message_0/1/2，且各分片 local_id 都从 1 计，业务键必须带 Shard。
    /// 增量水位用 SortSeq：跨分片区间不重叠，按 FromWechatWxId + ChatUsername + Shard 取 MAX(SortSeq) 即可。
    /// server_id / sort_seq / local_type 等会超过 Int32，C# 用 long 承接；
    /// 列类型显式声明 INTEGER（SQLite 的 INTEGER 最多 8 字节，可存 64 位）。
    /// 不要改成 C# int，也不要让 SqlSugar 默认映射成 BIGINT。
    /// 自增主键仍必须是 int：SqlSugar 把 long 主键映成 BIGINT，SQLite 不允许 AUTOINCREMENT。
    /// UIA 写入时这些原生键可为空或 0，故先不加唯一约束，避免和旧行冲突。
    /// 发送者 username 在本分片 Name2Id.rowid = real_sender_id 解析后写入 SenderWxId，
    /// 不要跨分片使用 rowid。message_content 若被 WCDB 标成 zstd，写入前先解码；
    /// 群聊正文常见「sender_wxid:\n正文」，发送者进 SenderWxId，正文进 Content。
    /// packed_info_data / source / compress_content 不镜像进本表。
    /// </summary>
    [SugarTable("wechat_message")]
    [SugarIndex("index_message_fromwechat", nameof(WeChatMessage.FromWechat), OrderByType.Asc)]
    [SugarIndex("index_message_fromwechat_wxid", nameof(WeChatMessage.FromWechatWxId), OrderByType.Asc)]
    [SugarIndex("index_message_chat_username", nameof(WeChatMessage.ChatUsername), OrderByType.Asc)]
    [SugarIndex("index_message_sender_wxid", nameof(WeChatMessage.SenderWxId), OrderByType.Asc)]
    [SugarIndex("index_message_messagetype", nameof(WeChatMessage.MessageType), OrderByType.Asc)]
    [SugarIndex("index_message_local_type", nameof(WeChatMessage.LocalType), OrderByType.Asc)]
    [SugarIndex("index_message_messagetime", nameof(WeChatMessage.MessageTime), OrderByType.Asc)]
    [SugarIndex("index_message_createtime", nameof(WeChatMessage.CreateTime), OrderByType.Asc)]
    [SugarIndex("index_message_msg_create_time", nameof(WeChatMessage.MsgCreateTime), OrderByType.Asc)]
    [SugarIndex("index_message_sort_seq", nameof(WeChatMessage.SortSeq), OrderByType.Asc)]
    [SugarIndex("index_message_server_id", nameof(WeChatMessage.ServerId), OrderByType.Asc)]
    [SugarIndex("index_mutx_1",
        nameof(WeChatMessage.FromWechat), OrderByType.Asc,
        nameof(WeChatMessage.MessageType), OrderByType.Asc,
        nameof(WeChatMessage.MessageTime), OrderByType.Asc,
        nameof(WeChatMessage.CreateTime), OrderByType.Asc
        )]
    [SugarIndex("index_mutx_2",
        nameof(WeChatMessage.FromWechat), OrderByType.Asc,
        nameof(WeChatMessage.IsBotProcessed), OrderByType.Asc,
        nameof(WeChatMessage.MessageTime), OrderByType.Asc,
        nameof(WeChatMessage.CreateTime), OrderByType.Asc
        )]
    [SugarIndex("index_message_sync_key",
        nameof(WeChatMessage.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatMessage.ChatUsername), OrderByType.Asc,
        nameof(WeChatMessage.Shard), OrderByType.Asc,
        nameof(WeChatMessage.LocalId), OrderByType.Asc
        )]
    // 业务键的 (wxid, local_id) 投影：UpsertMessages 之前的 LoadFilePaths 只按 wxid + local_id 过滤，
    // 没有这棵索引时只能吃到 wxid 前缀（等于扫该账号全部消息）。实测 5 万行 500ms → 2ms。
    [SugarIndex("index_message_from_local_id",
        nameof(WeChatMessage.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatMessage.LocalId), OrderByType.Asc
        )]
    [SugarIndex("index_message_watermark",
        nameof(WeChatMessage.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatMessage.ChatUsername), OrderByType.Asc,
        nameof(WeChatMessage.Shard), OrderByType.Asc,
        nameof(WeChatMessage.SortSeq), OrderByType.Asc
        )]
    [SugarIndex("index_message_server_id_from",
        nameof(WeChatMessage.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatMessage.ServerId), OrderByType.Asc
        )]
    public class WeChatMessage
    {
        /// <summary>
        /// 数据库主键。
        /// 注意：SQLite 的自增主键只支持 int（long 会被 SqlSugar 映射为 bigint，
        /// 触发 “AUTOINCREMENT is only allowed on an INTEGER PRIMARY KEY” 错误），故此处使用 int。
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 所属本机微信账号，最主要目的是多微信聚合
        /// 表示这条消息属于哪个微信账号,应用于多微信号场景
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "哪个微信收到的此消息，应用于多微信消息聚合场合")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>
        /// 所属本机微信账号wxid,目的是为了多微信消息聚合
        /// 表示这条消息属于微信账号的wxid,应用于多微信场景
        /// 由我们获取本机的wxid后自动填入
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此消息属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>
        /// 会话 username（好友 wxid / 群 123@chatroom 等）。
        /// 对应原生 Msg_ 表名规则：Msg_ + MD5(UTF-8(username))；不是显示名。
        /// UIA 写入时可能为空。
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "会话 username（wxid / 群 id）")]
        public string ChatUsername { get; set; } = string.Empty;

        /// <summary>
        /// 所在消息分片，对应 message_0/1/2.db 的 0/1/2。
        /// local_id 只在同一分片的同一会话表内唯一；跨分片不要复用 rowid。
        /// UIA 写入时可为 0。
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "消息分片 0/1/2")]
        public int Shard { get; set; }

        /// <summary>
        /// 会话内本地 id，对应 Msg_*.local_id（该表主键）。
        /// 与 ChatUsername、Shard 一起才能定位原生行。UIA 写入时可为 0。
        /// </summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "原生 local_id")]
        public long LocalId { get; set; }

        /// <summary>
        /// 服务端消息 id，对应 Msg_*.server_id。可能为 0（未发出或尚未同步）。
        /// 值可能超过 Int32，C# 用 long，列类型为 INTEGER。
        /// </summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "原生 server_id")]
        public long ServerId { get; set; }

        /// <summary>
        /// 排序序号，对应 Msg_*.sort_seq。原生按它倒序即新到旧；可能超过 Int32，必须用 long。
        /// </summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "原生 sort_seq")]
        public long SortSeq { get; set; }

        /// <summary>
        /// 发送者 wxid。对应本分片 Name2Id 中 real_sender_id 那一行的 user_name。
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "发送者wxid")]
        public string SenderWxId { get; set; } = string.Empty;

        /// <summary>
        /// 原生消息类型，对应 Msg_*.local_type（存在 subtype&lt;&lt;32 的大整数，C# 用 long，列类型为 INTEGER）。
        /// 常见：1 文本，3 图片，34 语音，43 视频，47 动画表情，48 位置，10000 系统消息；
        /// 大值多为「低 32 位基础类型 + (subtype&lt;&lt;32)」，如 244813135921 为引用消息。
        /// UIA 写入时可为 0，此时看 <see cref="MessageType"/>。
        /// </summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "原生 local_type")]
        public long LocalType { get; set; }

        /// <summary>
        /// 消息类型（UIA 文本标签，或按 LocalType 翻译的名称）。
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "消息类型")]
        public string MessageType { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容。Database Read 写入前需按 WCDB_CT_message_content 解码；不存压缩原文。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "消息内容")]
        public string Content { get; set; } = "";

        /// <summary>
        /// 微信消息发生时间,仅精确到分钟 ,取自微信，可能为空！（UIA 路径）
        /// Database Read 请同时填 <see cref="MsgCreateTime"/>。
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "接收到消息的时间,取自微信，可能为空，如果不为空，仅精确到分钟")]
        public string MessageTime { get; set; }

        /// <summary>
        /// 原生消息时间，对应 Msg_*.create_time，unix 秒。UIA 写入时可为 0。
        /// 与 <see cref="CreateTime"/>（SDK 写入时间）不是同一字段。
        /// </summary>
        [SugarColumn(ColumnDataType = "INTEGER", IsNullable = true, ColumnDescription = "原生 create_time（unix 秒）")]
        public long MsgCreateTime { get; set; }

        /// <summary>
        /// 当消息为图片、文件、视频时，保存在磁盘位置.
        /// 原生消息表无此列，来自资源库或下载路径。
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "文件路径")]
        public string FilePath { get; set; }

        /// <summary>
        /// 原生发送状态，对应 Msg_*.status。本机常见 3，其次 4 / 2；位含义未逐项核实。UIA 写入时可为 0。
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "原生 status")]
        public int Status { get; set; }

        /// <summary>
        /// 消息是否由自己发送
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否为自己发送")]
        public bool IsSelf { get; set; } = false;

        /// <summary>
        /// 消息是否被Bot处理
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "是否被其他的wechat bot处理/做标识")]
        public bool IsBotProcessed { get; set; } = false;

        /// <summary>
        /// 消息被 SDK 写入时间.
        /// </summary>
        [SugarColumn(InsertServerTime = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}

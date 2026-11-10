using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 微信消息表,对应微信message.\d+.db的消息表
    /// 多微信聚合
    /// </summary>
    [SugarTable("wechat_message")]
    [SugarIndex("index_message_fromwechat", nameof(WeChatMessage.FromWechat), OrderByType.Asc)]
    [SugarIndex("index_message_who", nameof(WeChatMessage.Who), OrderByType.Asc)]
    [SugarIndex("index_message_sender", nameof(WeChatMessage.Sender), OrderByType.Asc)]
    [SugarIndex("index_message_messagetype", nameof(WeChatMessage.MessageType), OrderByType.Asc)]
    [SugarIndex("index_message_messagetime", nameof(WeChatMessage.MessageTime), OrderByType.Asc)]
    [SugarIndex("index_message_createtime", nameof(WeChatMessage.CreateTime), OrderByType.Asc)]
    [SugarIndex("index_mutx_1",
        nameof(WeChatMessage.FromWechat), OrderByType.Asc,
        nameof(WeChatMessage.Who), OrderByType.Asc,
        nameof(WeChatMessage.Sender), OrderByType.Asc,
        nameof(WeChatMessage.MessageType), OrderByType.Asc,
        nameof(WeChatMessage.MessageTime), OrderByType.Asc,
        nameof(WeChatMessage.CreateTime), OrderByType.Asc
        )]
    [SugarIndex("index_mutx_2",
        nameof(WeChatMessage.FromWechat), OrderByType.Asc,
        nameof(WeChatMessage.Who), OrderByType.Asc,
        nameof(WeChatMessage.IsBotProcessed), OrderByType.Asc,
        nameof(WeChatMessage.MessageTime), OrderByType.Asc,
        nameof(WeChatMessage.CreateTime), OrderByType.Asc
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
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此联系人属于哪个微信昵称")]
        public string FromWechatWxId { get; set; } = string.Empty;
        /// <summary>
        /// 被监听的微信好友/群昵称,如： 人工智能自动化技术讨论群
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "被监听的微信好友、群昵称，如:人工智能自动化技术讨论群")]
        public string Who { get; set; } = string.Empty;

        /// <summary>
        /// 发送者昵称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "发送者昵称")]
        public string Sender { get; set; } = string.Empty;

        /// <summary>
        /// 发送者wxid
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "发送者wxid")]
        public string SenderWxId { get; set; } = string.Empty;

        /// <summary>
        /// 消息类型
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "消息类型")]
        public string MessageType { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "消息内容")]
        public string Content { get; set; } = "";

        /// <summary>
        /// 微信消息发生时间,仅精确到分钟 ,取自微信，可能为空！
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true, ColumnDescription = "接收到消息的时间,取自微信，可能为空，如果不为空，仅精确到分钟")]
        public string MessageTime { get; set; }
        /// <summary>
        /// 当消息为图片、文件、视频时，保存在磁盘位置.
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true, ColumnDescription = "文件路径")]
        public string FilePath { get; set; }


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
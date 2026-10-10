using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 微信消息表类    
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
        /// 微信账号
        /// 表示这条消息属于哪个微信账号,应用于多微信号场景
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string FromWechat { get; set; } = string.Empty;
        /// <summary>
        /// 被监听的微信好友/群昵称,如： 人工智能自动化技术讨论群
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string Who { get; set; } = string.Empty;

        /// <summary>
        /// 发送者昵称
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Sender { get; set; } = string.Empty;

        /// <summary>
        /// 消息类型
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string MessageType { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string Content { get; set; } = "";

        /// <summary>
        /// 微信消息发生时间,仅精确到分钟 ,取自微信，可能为空！
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string MessageTime { get; set; }
        /// <summary>
        /// 当消息为图片时，图片保存在磁盘位置.
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string ImageFilePath { get; set; }

        /// <summary>
        /// 消息是否由自己发送
        /// </summary>
        public bool IsSelf { get; set; } = false;

        /// <summary>
        /// 消息是否被Bot处理
        /// </summary>
        public bool IsBotProcessed { get; set; } = false;

        /// <summary>
        /// 消息被 SDK 写入时间.
        /// </summary>
        [SugarColumn(InsertServerTime = true, IsOnlyIgnoreUpdate = true)]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
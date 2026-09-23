using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OneOf;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Models;
using FlaUI.Core.AutomationElements;
using Newtonsoft.Json;
using SqlSugar;

namespace WeChatAuto.Options
{
    /// <summary>
    /// 消息监听器选项.
    /// </summary>
    public class PullMessagesOptions
    {
        /// <summary>
        /// 使用历史缓存列表补足 - 为了加快历史消息的获取速度，默认为True,系统检查到新抓取的数据根据LCS/Diff Block算法与历史消息序列一致，则停止向上滚动获取消息，改由从缓存历史消息中获取数据，也可以设置为False,则每次都会向上滚动抓取固定（用户设置）数量的消息
        /// </summary>
        [JsonProperty("using_message_cache_fill")]
        public bool usingMessageCacheFill = true;

        /// <summary>
        /// 如果此好友在缓存中不存在，是否获取此好友的用户信息(包括wxid),并更新缓存，对于基于wxid的企业级开发很有用
        /// </summary>
        [JsonProperty("fetch_friend_info")]
        public bool FetchFriendInfo { get; set; } = false;

        /// <summary>
        /// 如果聊天记录中有图片，是否获取图片
        /// </summary>
        [JsonProperty("fetch_image")]
        public bool FetchImage { get; set; } = false;

        /// <summary>
        /// 如果聊天记录中有附件，是否获取附件
        /// 附件在微信消息的定义为： 文件,由于微信默认下载20M的文件，可以在微信->账号与存储->自动下载少于xxMB的文件那里，将数量设置设置大一些，如: 100或者200M，能加快性能
        /// </summary>
        [JsonProperty("fetch_attachment")]
        public bool FetchAttachment { get; set; } = false;

        /// <summary>
        /// 如果聊天记录中有视频，是否获取视频
        /// </summary>
        [JsonProperty("fetch_video")]
        public bool FetchVideo { get; set; } = false;

        /// <summary>
        /// 如果聊天记录中有红包、转账，是否点击
        /// </summary>
        [JsonProperty("click_red_envelope")]
        public bool ClickRedEnvelope { get; set; } = false;

        /// <summary>
        /// 使用LCS/Diff Block算法时的比较数量，默认为3
        /// </summary>
        [JsonProperty("diff_count")]
        public int DiffCount { get; set; } = 3;

        /// <summary>
        /// 到底部时重试次数，默认三次
        /// </summary>
        [JsonProperty("bottom_retry_number")]
        public int BottomRetryNumber { get; set; } = 3;
        /// <summary>
        /// OCR的padding设置，默认是50
        /// </summary>
        [JsonProperty("ocr_padding")]
        public int OcrPadding { get; set; } = 50;
        /// <summary>
        /// 设置OCR时是否测试，如果值为True,则会显示每张OCR的画红框图,方便调试,默认为 false.
        /// </summary>
        [JsonProperty("is_ocr_debug")]
        public bool IsOCRDebug { get; set; } = false;
        /// <summary>
        /// 是否将源图截取到temp目录，方便ocr调试,默认为False
        /// </summary>
        [JsonProperty("is_capture_ori_image")]
        public bool IsCaptureOriImage { get; set; } = false;
        /// <summary>
        /// 一般不用设置
        /// 由于可能OCR的局限设置的兜底方案，如:
        /// 类似 AI.Net 被识别成Al.Net,并且又不容易解决,在这里可以写上对应关系，如 Al.Net  AI.Net
        /// 格式如下： 左边是识别错误的字符串，右边是正确的字符串.
        /// </summary>
        [JsonProperty("fallback_list")]
        public Dictionary<string, string> FallbackList = new Dictionary<string, string>();

        /// <summary>
        /// 数据库类型，几乎支持所有数据库类型，默认是sqlite数据库,可以改成其他的数据库
        /// </summary>
        [JsonProperty("db_type")]
        public DbType DbTppe { get; set; } = DbType.Sqlite;
        /// <summary>
        /// 数据库连接字符串
        /// "ConnectionString": "PORT=5432;DATABASE=xxx;HOST=localhost;PASSWORD=xxx;USER ID=xxx", // PostgreSQL（Kdbndp、OpenGauss通用）
        /// "ConnectionString": "Server=localhost;Database=xxx;Uid=xxx;Pwd=xxx;SslMode=None;AllowLoadLocalInfile=true;AllowUserVariables=true;", // MySql,
        /// "ConnectionString": "User Id=xxx; Password=xxx; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))", // Oracle
        /// "ConnectionString": "Server=localhost;Database=xxx;User Id=xxx;Password=xxx;Encrypt=True;TrustServerCertificate=True;", // SqlServer
        /// "ConnectionString": "host=222.71.212.3;Port=27017;Database=testDB;Username= root;Password=123456;authSource=admin;replicaSet=", // MongoDB
        /// </summary>
        [JsonProperty("connection_string")]
        public string ConnectionString { get; set; } = "Data Source=wechat.db";

        /// <summary>
        /// 默认获取的最新消息写入上面设置的数据库，但是考虑到一些不想直接写数据库，如：调用webapi写服务器数据库，可以在此设置回调函数，在回调函数中调用webapi,
        /// 注意：如果不想写数据库，需要将DbType设置为DbType.Custom,否则此方法不起效果.
        /// 传入的参数: SDK传入最新获取的消息列表,详情请参考<seealso cref="SimpleMessageBubble"/>
        /// 返回： 写入为真还是假.
        /// </summary>
        public Func<List<SimpleMessageBubble>, bool> WriteDataBaseCallback { get; set; } = null;
    }
}
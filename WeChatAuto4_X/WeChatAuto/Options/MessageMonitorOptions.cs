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
    /// 消息监听器选项,适用于非弹窗监听/开放式监听与弹窗式监听
    /// </summary>
    public class MessageMonitorOptions
    {
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
        /// 到底部时重试次数，默认三次，如果是长文本比较多，建议改成6次.
        /// </summary>
        [JsonProperty("bottom_retry_number")]
        public int BottomRetryNumber { get; set; } = 3;

        /// <summary>
        /// 上下文抓取的数量
        /// </summary>
        [JsonProperty("context_fetch_number")]
        public int ContextFetchNumber { get; set; } = 40;

        /// <summary>
        /// 对于每一条原始消息的处理回调函数,供使用者精准控制每一条消息的处理.
        /// </summary>
        public Func<AutomationElement, Task> RawSingleMessageCallBack;

        /// <summary>
        /// 是否预防风控,如果待监控的群不多，建议设置为False,如果监测的群/好友很多，并且聊天很频繁，建议将设置为True.
        /// 因为人不可能一天24小时进行操作的,否则极易被微信风控退出。
        /// </summary>
        [JsonProperty("is_risk_prevention")]
        public bool IsRiskPrevention { get; set; } = false;

        /// <summary>
        /// 预防风控方法简单默认实现，使用者可以替换此方法来实现更精准的随机行为.
        /// 如果上面IsRiskPrevention设置为True,则预防风控方法生效，预设预防风控行为是等候一段时间，你也可以覆盖此方法，加入更多不可预测行为.
        /// 如：你可以加入随机与某人聊一句，或者运行其他的方法，甚至晚上一段时间停止等
        /// 触发时间：运4行6-10分钟之内的某个随机时间触发
        /// 预防风控方法运行时，消息监听会暂停，预防风控方法运行结束，消息监听继续.
        /// </summary>
        public Func<WeChatClient, Task> RiskPreventionAction { get; set; } = async client =>
        {
            await RandomWait.WaitAsync(60 * 1_000, 3 * 60 * 1_000);  //随机等候1..3分钟.
        };
        /// <summary>
        /// OCR的padding设置，默认是20
        /// </summary>
        public int OcrPadding { get; set; } = 50;
        /// <summary>
        /// 设置OCR时是否测试，如果值为True,则会显示每张OCR的画红框图,方便调试,默认为 false.
        /// </summary>
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
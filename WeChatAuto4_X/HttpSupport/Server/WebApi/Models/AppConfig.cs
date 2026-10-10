using Server.WebApi.Options;

namespace Server.WebApi.Models;

/// <summary>
/// App.json 配置文件的结构。
/// </summary>
public sealed class AppConfig
{
    /// <summary>
    /// HTTP 服务配置。
    /// </summary>
    public HttpServerOptions Http { get; set; } = new();

    /// <summary>
    /// 每个微信号的配置集合。
    /// </summary>
    public List<ClientConfig> Config { get; set; } = new();
}

/// <summary>
/// 单个微信号的配置。
/// </summary>
public sealed class ClientConfig
{
    /// <summary>
    /// 微信昵称（JSON 键为 "WechatNickName"，注意大小写）。
    /// </summary>
    public string WechatNickName { get; set; } = "";

    /// <summary>
    /// 数据库配置。
    /// </summary>
    public List<DatabaseConfig> Database { get; set; } = new();

    /// <summary>
    /// 消息监听配置。
    /// </summary>
    public List<MessageMonitorConfig> MessageMonitor { get; set; } = new();

    /// <summary>
    /// 新朋友请求监听配置。
    /// </summary>
    public List<NewFriendRequestMonitorConfig> NewFriendRequestMonitor { get; set; } = new();

    /// <summary>
    /// 文本转语音（TTS）配置。
    /// </summary>
    public TtsConfig TTS { get; set; } = new();

    /// <summary>
    /// 消息发送计划配置。
    /// </summary>
    public MessageSendPlanConfig MessageSendPlan { get; set; } = new();
}

/// <summary>
/// 数据库配置。
/// </summary>
public sealed class DatabaseConfig
{
    /// <summary>
    /// 数据库类型，如 sqlite。
    /// </summary>
    public string Type { get; set; } = "sqlite";

    /// <summary>
    /// 连接字符串。
    /// </summary>
    public string ConnectionString { get; set; } = "Data Source=wechat.db";
}

/// <summary>
/// 消息监听配置。
/// </summary>
public sealed class MessageMonitorConfig
{
    /// <summary>
    /// 监听类型，如 Open。
    /// </summary>
    public string Type { get; set; } = "Open";

    /// <summary>
    /// 需要监听的好友昵称列表。
    /// </summary>
    public List<string> MonitorFriends { get; set; } = new();

    /// <summary>
    /// 是否使用 UI Tree 回退方案。
    /// </summary>
    public bool UseUITreeRollbackPlan { get; set; } = true;

    /// <summary>
    /// 轮询超时时间（秒）。
    /// </summary>
    public int PollingTimeout { get; set; } = 10;
}

/// <summary>
/// 新朋友请求监听配置。
/// </summary>
public sealed class NewFriendRequestMonitorConfig
{
    /// <summary>
    /// 是否启用。
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 通过后是否删除。
    /// </summary>
    public bool PassedDelete { get; set; } = true;

    /// <summary>
    /// 关键词配置。
    /// </summary>
    public List<KeywordConfig> Keywords { get; set; } = new();
}

/// <summary>
/// 新朋友请求关键词配置。
/// </summary>
public sealed class KeywordConfig
{
    /// <summary>
    /// 关键词。
    /// </summary>
    public string Keyword { get; set; } = "";

    /// <summary>
    /// 后缀。
    /// </summary>
    public string Suffix { get; set; } = "";

    /// <summary>
    /// 标签。
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// 首条消息。
    /// </summary>
    public string FirstMessage { get; set; } = "";
}

/// <summary>
/// 文本转语音（TTS）配置。
/// </summary>
public sealed class TtsConfig
{
    /// <summary>
    /// 通义千问 API Key。
    /// </summary>
    public string QwenApiKey { get; set; } = "";

    /// <summary>
    /// 音色。
    /// </summary>
    public string Voice { get; set; } = "Cherry";

    /// <summary>
    /// 语言类型。
    /// </summary>
    public string LanguageType { get; set; } = "Auto";

    /// <summary>
    /// 指令。
    /// </summary>
    public string Instructions { get; set; } = "";

    /// <summary>
    /// 是否个性化优化（JSON 键为 "IsPersionOptimize"，沿用原文件拼写）。
    /// </summary>
    public bool IsPersionOptimize { get; set; } = false;
}

/// <summary>
/// 消息发送计划配置。
/// </summary>
public sealed class MessageSendPlanConfig
{
    /// <summary>
    /// 计划内容。
    /// </summary>
    public string Plan { get; set; } = "";
}

using System.Text.Json.Serialization;
using WeChatAuto.Models;
using WeChatAuto.Options;

namespace Server.WebApi.Contracts.Requests;

/// <summary>
/// 发送文本消息。who 为空时发送给当前焦点聊天窗口。
/// </summary>
public sealed class SendMessageRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    /// <summary>
    /// 被 @ 的好友列表。
    /// </summary>
    [JsonPropertyName("at_users")]
    public List<string>? AtUsers { get; set; }

    /// <summary>
    /// 引用的对话内容。
    /// </summary>
    [JsonPropertyName("refer")]
    public ChatRefer? Refer { get; set; }
}

/// <summary>
/// 发送表情。emoji 与 index 二选一（优先使用 index）。
/// </summary>
public sealed class SendEmojiRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>
    /// 表情名称或描述。
    /// </summary>
    [JsonPropertyName("emoji")]
    public string? Emoji { get; set; }

    /// <summary>
    /// 表情索引。
    /// </summary>
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("at_users")]
    public List<string>? AtUsers { get; set; }
}

/// <summary>
/// 发送文件。who 为空时发送给当前焦点聊天窗口。
/// </summary>
public sealed class SendFileRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("files")]
    public string[] Files { get; set; } = Array.Empty<string>();
}

/// <summary>
/// 发送语音消息。who 为空时发送给当前焦点聊天窗口。
/// </summary>
public sealed class SendVoiceMessageRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("file_path")]
    public string FilePath { get; set; } = "";
}

/// <summary>
/// 文字转语音发送。
/// </summary>
public sealed class SendVoiceMessageTtsRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; } = "";

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("options")]
    public VoiceOptions? Options { get; set; }

    [JsonPropertyName("optimize_with_llm")]
    public bool OptimizeWithLlm { get; set; }
}

/// <summary>
/// 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
/// </summary>
public sealed class SendVoiceChatsRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("partner")]
    public string[] Partner { get; set; } = Array.Empty<string>();
}

/// <summary>
/// 仅携带 who（可为空，作用于当前焦点窗口）的请求。
/// </summary>
public sealed class WhoRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }
}

/// <summary>
/// 拍一拍。
/// </summary>
public sealed class TapRequest
{
    [JsonPropertyName("who")]
    public string Who { get; set; } = "";

    [JsonPropertyName("prev_scroll_number")]
    public int PrevScrollNumber { get; set; } = 30;
}

/// <summary>
/// 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
/// </summary>
public sealed class ReferenceRequest
{
    [JsonPropertyName("message")]
    public ChatSimpleMessage? Message { get; set; }

    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("message_content")]
    public string? MessageContent { get; set; }

    [JsonPropertyName("prev_scroll_number")]
    public int PrevScrollNumber { get; set; } = 30;
}

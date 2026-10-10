using System.Text.Json.Serialization;

namespace Server.WebApi.Contracts.Requests;

/// <summary>
/// 搜索会话。
/// </summary>
public sealed class SearchConversationRequest
{
    [JsonPropertyName("who")]
    public string Who { get; set; } = "";
}

/// <summary>
/// 定位会话。
/// </summary>
public sealed class LocateConversationRequest
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}

/// <summary>
/// 设置会话免打扰。who 为空时作用于当前窗口。
/// </summary>
public sealed class SetDoNotDisturbRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("setting")]
    public bool Setting { get; set; } = true;
}

/// <summary>
/// 设置会话置顶。who 为空时作用于当前窗口。
/// </summary>
public sealed class SetTopMostRequest
{
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    [JsonPropertyName("setting")]
    public bool Setting { get; set; } = true;
}

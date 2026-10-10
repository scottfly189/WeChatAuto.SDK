using System.Text.Json.Serialization;
using WeChatAuto.Options;

namespace Server.WebApi.Contracts.Requests;

/// <summary>
/// 通过手机号码或微信号添加好友。
/// </summary>
public sealed class AddFriendsRequest
{
    [JsonPropertyName("friends")]
    public string[] Friends { get; set; } = Array.Empty<string>();

    [JsonPropertyName("options")]
    public AddFriendsOptions? Options { get; set; }
}

/// <summary>
/// 从群聊中添加好友。
/// </summary>
public sealed class AddGroupMemberToFriendsRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("members")]
    public List<string> Members { get; set; } = new();

    [JsonPropertyName("options")]
    public AddFriendsOptions? Options { get; set; }
}

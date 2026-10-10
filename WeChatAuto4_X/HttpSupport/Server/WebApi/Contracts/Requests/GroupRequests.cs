using System.Text.Json.Serialization;

namespace Server.WebApi.Contracts.Requests;

/// <summary>
/// 创建群聊。
/// </summary>
public sealed class CreateGroupRequest
{
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = "";

    [JsonPropertyName("first_who")]
    public string? FirstWho { get; set; }

    [JsonPropertyName("members")]
    public string[] Members { get; set; } = Array.Empty<string>();
}

/// <summary>
/// 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
/// </summary>
public sealed class GroupMemberRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("members")]
    public string[] Members { get; set; } = Array.Empty<string>();
}

/// <summary>
/// 修改群名。
/// </summary>
public sealed class RenameGroupRequest
{
    [JsonPropertyName("old_group_name")]
    public string OldGroupName { get; set; } = "";

    [JsonPropertyName("new_group_name")]
    public string NewGroupName { get; set; } = "";
}

/// <summary>
/// 修改自己在群中的昵称。
/// </summary>
public sealed class ChangeGroupNickNameRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("nick_name")]
    public string NickName { get; set; } = "";
}

/// <summary>
/// 修改群备注。
/// </summary>
public sealed class ChangeGroupMemoRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("new_memo")]
    public string NewMemo { get; set; } = "";
}

/// <summary>
/// 更新群公告。
/// </summary>
public sealed class UpdateGroupNoticeRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("group_notice")]
    public string GroupNotice { get; set; } = "";
}

/// <summary>
/// 邀请群成员（外部群）。
/// </summary>
public sealed class InviteGroupMemberRequest
{
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    [JsonPropertyName("members")]
    public List<string> Members { get; set; } = new();

    [JsonPropertyName("invite_reason")]
    public string InviteReason { get; set; } = "";
}

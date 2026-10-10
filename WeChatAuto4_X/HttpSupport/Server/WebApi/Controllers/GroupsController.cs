using Microsoft.AspNetCore.Mvc;
using OneOf;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Services;

namespace Server.WebApi.Controllers;

/// <summary>
/// 群聊管理：自有群与外部群操作。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/groups")]
public sealed class GroupsController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public GroupsController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("{groupName}/is-owner")]
    public async Task<IActionResult> IsOwner(string clientId, string groupName)
        => Ok(await _provider.Resolve(clientId).IsOwnerChatGroup(groupName));

    [HttpGet("{groupName}/owner")]
    public async Task<IActionResult> GetOwner(string clientId, string groupName)
        => Ok(await _provider.Resolve(clientId).GetGroupOwner(groupName));

    [HttpGet("members")]
    public async Task<IActionResult> GetMembers(string clientId, [FromQuery] string? groupName)
        => Ok(await _provider.Resolve(clientId).GetChatGroupMemberList(groupName ?? ""));

    [HttpPost("members")]
    public async Task<IActionResult> AddMembers(string clientId, [FromBody] GroupMemberRequest request)
    {
        OneOf<string, string[]> memberName = request.Members;
        await _provider.Resolve(clientId).AddOwnerChatGroupMember(request.GroupName ?? "", memberName);
        return NoContent();
    }

    [HttpDelete("members")]
    public async Task<IActionResult> RemoveMembers(string clientId, [FromBody] GroupMemberRequest request)
    {
        OneOf<string, string[]> memberName = request.Members;
        var result = await _provider.Resolve(clientId).RemoveOwnerChatGroupMember(request.GroupName ?? "", memberName);
        return Ok(result);
    }

    [HttpDelete("{groupName}")]
    public async Task<IActionResult> Quit(string clientId, string groupName, [FromQuery] bool clearHistory = true)
    {
        await _provider.Resolve(clientId).QuitChatGroup(groupName, clearHistory);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string clientId, [FromBody] CreateGroupRequest request)
        => Ok(await _provider.Resolve(clientId).CreateOwnerChatGroup(request.GroupName, request.FirstWho ?? "", request.Members));

    [HttpPut("name")]
    public async Task<IActionResult> ChangeName(string clientId, [FromBody] RenameGroupRequest request)
        => Ok(await _provider.Resolve(clientId).ChangeOwnerChatGroupName(request.OldGroupName, request.NewGroupName));

    [HttpPut("nickname")]
    public async Task<IActionResult> ChangeNickName(string clientId, [FromBody] ChangeGroupNickNameRequest request)
        => Ok(await _provider.Resolve(clientId).ChangeChatGroupNickName(request.GroupName ?? "", request.NickName));

    [HttpPut("memo")]
    public async Task<IActionResult> ChangeMemo(string clientId, [FromBody] ChangeGroupMemoRequest request)
        => Ok(await _provider.Resolve(clientId).ChangeChatGroupMemo(request.GroupName ?? "", request.NewMemo));

    [HttpPut("notice")]
    public async Task<IActionResult> UpdateNotice(string clientId, [FromBody] UpdateGroupNoticeRequest request)
        => Ok(await _provider.Resolve(clientId).UpdateGroupNotice(request.GroupName ?? "", request.GroupNotice));

    [HttpPost("invite")]
    public async Task<IActionResult> Invite(string clientId, [FromBody] InviteGroupMemberRequest request)
        => Ok(await _provider.Resolve(clientId).InviteChatGroupMember(request.GroupName ?? "", request.Members, request.InviteReason));

    [HttpPost("add-members-to-friends")]
    public async Task<IActionResult> AddMembersToFriends(string clientId, [FromBody] AddGroupMemberToFriendsRequest request)
        => Ok(await _provider.Resolve(clientId).AddChatGroupMemberToFriends(request.GroupName ?? "", request.Members, request.Options));
}

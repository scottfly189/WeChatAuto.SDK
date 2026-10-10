using Microsoft.AspNetCore.Mvc;
using OneOf;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Contracts.Responses;
using Server.WebApi.Services;
using WeAutoCommon.Models;

namespace Server.WebApi.Controllers;

/// <summary>
/// 好友与通讯录：加好友、好友列表、缓存好友管理。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/friends")]
public sealed class FriendsController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public FriendsController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("add-window/open")]
    public async Task<IActionResult> OpenAddWindow(string clientId)
    {
        var window = await _provider.Resolve(clientId).OpenAddFriensWin();
        return Ok(WindowHandleResponse.From(window));
    }

    [HttpPost("add-window/close")]
    public async Task<IActionResult> CloseAddWindow(string clientId)
    {
        await _provider.Resolve(clientId).CloseAddFriendWin();
        return NoContent();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(string clientId, [FromBody] AddFriendsRequest request)
    {
        OneOf<string, string[]> friends = request.Friends;
        var result = await _provider.Resolve(clientId).AddFriends(friends, request.Options, HttpContext.RequestAborted);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFriends(string clientId, [FromQuery] bool fromCache = true)
        => Ok(await _provider.Resolve(clientId).GetAllFriends(fromCache));

    [HttpGet("names")]
    public async Task<IActionResult> GetAllFriendNames(string clientId)
        => Ok(await _provider.Resolve(clientId).GetAllFriendNames());

    [HttpDelete("{nickName}")]
    public async Task<IActionResult> RemoveFriend(string clientId, string nickName)
        => Ok(await _provider.Resolve(clientId).RemoveFriend(nickName));

    [HttpGet("cache")]
    public async Task<IActionResult> GetFriendListFromCache(string clientId)
        => Ok(await _provider.Resolve(clientId).GetFriendListFromCacheAsync());

    [HttpGet("cache/by-name/{who}")]
    public IActionResult GetFriendsFromCache(string clientId, string who)
        => Ok(_provider.Resolve(clientId).GetFriendsFromCache(who));

    [HttpGet("cache/by-wxid/{wxid}")]
    public async Task<IActionResult> GetFriendByWxidFromCache(string clientId, string wxid)
        => Ok(await _provider.Resolve(clientId).GetFriendWithWxIDFromCacheAsync(wxid));

    [HttpGet("cache/{who}")]
    public async Task<IActionResult> GetFriendFromCache(string clientId, string who)
        => Ok(await _provider.Resolve(clientId).GetFriendFromCacheAsync(who));

    [HttpPost("cache")]
    public async Task<IActionResult> AddOrUpdateFriend(string clientId, [FromBody] FriendInfo friend)
    {
        await _provider.Resolve(clientId).AddOrUpdateFriendFromCacheAsync(friend);
        return NoContent();
    }

    [HttpDelete("cache/{who}")]
    public async Task<IActionResult> RemoveFriendFromCache(string clientId, string who)
    {
        await _provider.Resolve(clientId).RemoveFriendFromCacheAsync(who);
        return NoContent();
    }

    [HttpDelete("cache/by-wxid/{wxid}")]
    public async Task<IActionResult> RemoveFriendByWxidFromCache(string clientId, string wxid)
    {
        await _provider.Resolve(clientId).RemoveFriendWithWxIDFromCacheAsync(wxid);
        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Services;

namespace Server.WebApi.Controllers;

/// <summary>
/// 会话管理：会话列表、搜索、定位、免打扰、置顶。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/conversations")]
public sealed class ConversationsController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public ConversationsController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllConversations(string clientId)
        => Ok(await _provider.Resolve(clientId).GetAllConversations());

    [HttpGet("visible-titles")]
    public async Task<IActionResult> GetVisibleTitles(string clientId)
        => Ok(await _provider.Resolve(clientId).GetVisibleConversationTitles());

    [HttpGet("visible")]
    public async Task<IActionResult> GetVisibleConversations(string clientId)
        => Ok(await _provider.Resolve(clientId).GetVisibleConversations());

    [HttpPost("search")]
    public async Task<IActionResult> Search(string clientId, [FromBody] SearchConversationRequest request)
        => Ok(await _provider.Resolve(clientId).SearchFriend(request.Who));

    [HttpPost("locate")]
    public async Task<IActionResult> Locate(string clientId, [FromBody] LocateConversationRequest request)
        => Ok(await _provider.Resolve(clientId).LocateConversation(request.Title));

    [HttpPost("set-do-not-disturb")]
    public async Task<IActionResult> SetDoNotDisturb(string clientId, [FromBody] SetDoNotDisturbRequest request)
        => Ok(await _provider.Resolve(clientId).SetDoNotDisturb(request.Who, request.Setting));

    [HttpPost("set-top-most")]
    public async Task<IActionResult> SetTopMost(string clientId, [FromBody] SetTopMostRequest request)
        => Ok(await _provider.Resolve(clientId).SetTopMost(request.Who, request.Setting));
}

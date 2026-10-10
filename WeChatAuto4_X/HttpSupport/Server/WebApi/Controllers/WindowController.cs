using Microsoft.AspNetCore.Mvc;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Contracts.Responses;
using Server.WebApi.Services;

namespace Server.WebApi.Controllers;

/// <summary>
/// 窗口管理：最大化/还原/置顶/焦点、子窗口、搜索窗口、句柄与进程。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/window")]
public sealed class WindowController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public WindowController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("maximize")]
    public async Task<IActionResult> Maximize(string clientId)
    {
        await _provider.Resolve(clientId).Max();
        return NoContent();
    }

    [HttpPost("restore")]
    public async Task<IActionResult> Restore(string clientId)
    {
        await _provider.Resolve(clientId).Restore();
        return NoContent();
    }

    [HttpPost("pin")]
    public async Task<IActionResult> Pin(string clientId)
    {
        await _provider.Resolve(clientId).Pinned();
        return NoContent();
    }

    [HttpPost("unpin")]
    public async Task<IActionResult> Unpin(string clientId)
    {
        await _provider.Resolve(clientId).UnPinned();
        return NoContent();
    }

    [HttpPost("focus")]
    public async Task<IActionResult> Focus(string clientId)
    {
        await _provider.Resolve(clientId).Focus();
        return NoContent();
    }

    [HttpPost("close-search-window")]
    public async Task<IActionResult> CloseSearchWindow(string clientId, [FromBody] SubWindowRequest request)
    {
        await _provider.Resolve(clientId).CloseSearchWindow(request.Who);
        return NoContent();
    }

    [HttpPost("open-sub-window")]
    public async Task<IActionResult> OpenSubWindow(string clientId, [FromBody] SubWindowRequest request)
    {
        var window = await _provider.Resolve(clientId).OpenSubWin(request.Who);
        return Ok(WindowHandleResponse.From(window));
    }

    [HttpPost("close-sub-window")]
    public async Task<IActionResult> CloseSubWindow(string clientId, [FromBody] SubWindowRequest request)
    {
        await _provider.Resolve(clientId).CloseSubWin(request.Who);
        return NoContent();
    }

    [HttpGet("handler")]
    public IActionResult GetHandler(string clientId)
        => Ok(new { handler = _provider.Resolve(clientId).GetHandler().ToInt64() });

    [HttpGet("process-id")]
    public IActionResult GetProcessId(string clientId)
        => Ok(new { process_id = (int)_provider.Resolve(clientId).GetProcessId() });
}

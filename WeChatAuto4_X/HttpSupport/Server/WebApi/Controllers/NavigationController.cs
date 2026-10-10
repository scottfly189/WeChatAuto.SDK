using Microsoft.AspNetCore.Mvc;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Services;

namespace Server.WebApi.Controllers;

/// <summary>
/// 导航栏管理：切换导航、关闭导航窗口。
/// </summary>
[ApiController]
[Route("api/v1/clients/{clientId}/navigation")]
public sealed class NavigationController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public NavigationController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("switch")]
    public async Task<IActionResult> Switch(string clientId, [FromBody] SwitchNavigationRequest request)
    {
        await _provider.Resolve(clientId).SwitchNavigation(request.NavigationType);
        return NoContent();
    }

    [HttpPost("close")]
    public async Task<IActionResult> Close(string clientId, [FromBody] SwitchNavigationRequest request)
    {
        await _provider.Resolve(clientId).CloseNavWin(request.NavigationType);
        return NoContent();
    }
}

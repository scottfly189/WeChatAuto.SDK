using Microsoft.AspNetCore.Mvc;
using Server.WebApi.Contracts.Requests;
using Server.WebApi.Contracts.Responses;
using Server.WebApi.Services;

namespace Server.WebApi.Controllers;

/// <summary>
/// 微信客户端：列表、个人信息、头像、任务栏图标。
/// </summary>
[ApiController]
[Route("api/v1/clients")]
public sealed class ClientsController : ControllerBase
{
    private readonly IWeChatClientProvider _provider;

    public ClientsController(IWeChatClientProvider provider)
    {
        _provider = provider;
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_provider.GetAll().Select(ClientInfoResponse.From).ToList());

    [HttpGet("{clientId}")]
    public IActionResult GetInfo(string clientId)
        => Ok(ClientInfoResponse.From(_provider.Resolve(clientId)));

    [HttpGet("{clientId}/owner-info")]
    public IActionResult GetOwnerInfo(string clientId)
        => Ok(_provider.Resolve(clientId).GetOwerInfo());

    [HttpPost("{clientId}/avatar")]
    public async Task<IActionResult> SaveAvatar(string clientId, [FromBody] SaveAvatarRequest request)
    {
        await _provider.Resolve(clientId).SaveOwnerAvator(request.Path);
        return NoContent();
    }

    [HttpPost("{clientId}/notify-icon/click")]
    public async Task<IActionResult> ClickNotifyIcon(string clientId, [FromBody] NotifyIconClickRequest request)
    {
        var client = _provider.Resolve(clientId);
        if (request.Index is int index)
        {
            await client.ClickNotifyIcon(index);
        }
        else if (!string.IsNullOrWhiteSpace(request.WechatName))
        {
            await client.ClickNotifyIcon(request.WechatName);
        }
        else
        {
            return BadRequest(new ProblemDetails
            {
                Title = "参数错误",
                Detail = "index 与 wechat_name 至少提供一个。",
            });
        }

        return NoContent();
    }
}

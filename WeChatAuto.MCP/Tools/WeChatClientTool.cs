using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;
using WeChatAuto.Components;

[McpServerToolType]
public sealed class WeChatClientTool
{
    private readonly WeChatClientService _weChatClientService;
    public WeChatClientTool(WeChatClientService weChatClientService)
    {
        _weChatClientService = weChatClientService;
    }
    [McpServerTool, Description("获取本机打开的所有微信客户端名称")]
    public async Task<string> GetAllWeChatClientNames()
    {
        var clientNames = _weChatClientService.GetAllWeChatClientNames();
        return await Task.FromResult(JsonSerializer.Serialize(clientNames));
    }
    
    [McpServerTool, Description("获取当前微信客户端名称,系统默认使用获得的第一个微信客户端名称作为当前微信客户端名称")]
    public async Task<string> GetCurrentWeChatClientName()
    {
        var clientName = _weChatClientService.GetCurrentWeChatClientName();
        return await Task.FromResult(clientName ?? string.Empty);
    }
}
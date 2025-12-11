using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;
using WeChatAuto.Components;

[McpServerToolType]
public sealed class WeChatClientTool
{
    private readonly WeChatClientFactory _weChatClientFactory;
    public WeChatClientTool(WeChatClientFactory weChatClientFactory)
    {
        _weChatClientFactory = weChatClientFactory;
    }
    [McpServerTool,Description("获取本机打开的所有微信客户端名称")]
    public Task<string> GetAllWeChatClientNames()
    {
        var clientNames = _weChatClientFactory.GetWxClientNames();
        return Task.FromResult(JsonSerializer.Serialize(clientNames));
    }
}
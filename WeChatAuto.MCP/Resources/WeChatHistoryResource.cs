using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

[McpServerResourceType]
public sealed class WeChatHistoryResource
{
    private readonly WeChatClientService _weChatClientService;
    public WeChatHistoryResource(WeChatClientService weChatClientService)
    {
        _weChatClientService = weChatClientService;
    }
    [McpServerResource(UriTemplate="wechatmcp://history/{who}?pageCount={pageCount}",Name ="get_wechat_history",MimeType ="application/json")]
    [Description("获取指定好友或者群聊的所有聊天记录，默认获取10页聊天记录，如果指定页数，则获取指定页数的聊天记录")]
    public Task<string> GetWeChatHistoryResource(
        [Description("好友或者群聊昵称")] string who,
        [Description("获取的聊天记录数量，默认是10页,可以指定获取的页数,如果指定页数，则获取指定页数的聊天记录")] int pageCount = 10)
    {
        var result = _weChatClientService.GetChatAllHistory(who, pageCount);
        return Task.FromResult(JsonSerializer.Serialize(result));
    }
}
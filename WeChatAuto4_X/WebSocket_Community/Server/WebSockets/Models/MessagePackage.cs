using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// 微信消息包
/// </summary>
public class MessagePackage
{
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }
    [JsonPropertyName("func_Name")]
    public string? FuncName { get; set; }
    [JsonPropertyName("options")]
    public string? Options { get; set; }
    [JsonPropertyName("from_wechat")]
    public required string FromWechat {get;set;}
}
/// <summary>
/// 微信消息 - 包装类
/// </summary>
public class MessagePackageWrapper : MessagePackage
{
    public WebSocketHandler? handler { get; set; }

    public static MessagePackageWrapper Create(MessagePackage package, WebSocketHandler handler)
    {
        return new MessagePackageWrapper
        {
            handler = handler,
            RequestId = package.RequestId,
            FuncName = package.FuncName,
            Options = package.Options,
            FromWechat = package.FromWechat,
        };
    }
}
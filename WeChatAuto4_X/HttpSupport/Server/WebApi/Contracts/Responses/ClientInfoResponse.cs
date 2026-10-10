using System.Text.Json.Serialization;
using WeChatAuto.Components;

namespace Server.WebApi.Contracts.Responses;

/// <summary>
/// 微信客户端概要信息。
/// </summary>
public sealed class ClientInfoResponse
{
    [JsonPropertyName("nick_name")]
    public string NickName { get; set; } = "";

    [JsonPropertyName("wx_id")]
    public string WxId { get; set; } = "";

    [JsonPropertyName("avator_path")]
    public string AvatorPath { get; set; } = "";

    [JsonPropertyName("process_id")]
    public int ProcessId { get; set; }

    [JsonPropertyName("handler")]
    public long Handler { get; set; }

    [JsonPropertyName("wechat_index")]
    public int WechatIndex { get; set; }

    public static ClientInfoResponse From(WeChatClient client) => new()
    {
        NickName = client.NickName,
        WxId = client.WxId,
        AvatorPath = client.AvatorPath,
        ProcessId = client.ClientProcessId,
        Handler = client.GetHandler().ToInt64(),
        WechatIndex = client.WechatIndex,
    };
}

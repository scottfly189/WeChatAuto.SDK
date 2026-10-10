namespace Server.WebApi.Exceptions;

/// <summary>
/// 指定的微信客户端（昵称或 wxid）不存在。
/// </summary>
public sealed class WeChatClientNotFoundException : Exception
{
    public WeChatClientNotFoundException(string clientId)
        : base($"微信客户端[{clientId}]不存在，请检查微信是否打开。")
    {
        ClientId = clientId;
    }

    /// <summary>
    /// 请求方传入的客户端标识（昵称或 wxid）。
    /// </summary>
    public string ClientId { get; }
}

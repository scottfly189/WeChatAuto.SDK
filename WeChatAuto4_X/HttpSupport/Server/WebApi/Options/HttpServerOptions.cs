namespace Server.WebApi.Options;

/// <summary>
/// HTTP 服务选项，绑定 App.json 的 Http 节。
/// </summary>
public sealed class HttpServerOptions
{
    public const string SectionName = "Http";

    /// <summary>
    /// Kestrel 监听端口，默认 5000。
    /// </summary>
    public int Port { get; set; } = 5000;
}

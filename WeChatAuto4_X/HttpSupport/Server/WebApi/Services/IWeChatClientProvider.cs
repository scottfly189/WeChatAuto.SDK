using WeChatAuto.Components;

namespace Server.WebApi.Services;

/// <summary>
/// 微信客户端解析器：按昵称或 wxid 定位客户端。
/// </summary>
public interface IWeChatClientProvider
{
    /// <summary>
    /// 按昵称或 wxid 解析客户端，未命中抛 <see cref="Exceptions.WeChatClientNotFoundException"/>。
    /// </summary>
    WeChatClient Resolve(string clientId);

    /// <summary>
    /// 获取全部客户端。
    /// </summary>
    IReadOnlyList<WeChatClient> GetAll();
}

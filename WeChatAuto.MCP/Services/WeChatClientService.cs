using WeChatAuto.Components;

/// <summary>
/// 微信客户端服务,提供微信客户端的获取、设置等操作
/// </summary>
public class WeChatClientService
{
    private readonly WeChatClientFactory _weChatClientFactory;
    /// <summary>
    /// 微信客户端服务构造函数
    /// </summary>
    /// <param name="weChatClientFactory">微信客户端工厂,参考<see cref="WeChatClientFactory"/></param>
    public WeChatClientService(WeChatClientFactory weChatClientFactory)
    {
        _weChatClientFactory = weChatClientFactory;
    }
    /// <summary>
    /// 获取所有微信客户端名称
    /// </summary>
    /// <returns>微信客户端名称列表</returns>
    public List<string> GetAllWeChatClientNames()
    {
        return _weChatClientFactory.GetWxClientNames();
    }

    /// <summary>
    /// 获取当前微信客户端名称
    /// </summary>
    /// <returns>当前微信客户端名称</returns>
    public string GetCurrentWeChatClientName()
    {
        var clientNames = GetAllWeChatClientNames();
        var clientName = clientNames.FirstOrDefault();
        return clientName ?? string.Empty;
    }
}
using Server.WebApi.Exceptions;
using WeChatAuto.Components;

namespace Server.WebApi.Services;

public sealed class WeChatClientProvider : IWeChatClientProvider
{
    private readonly WeChatClientFactory _factory;

    public WeChatClientProvider(WeChatClientFactory factory)
    {
        _factory = factory;
    }

    public WeChatClient Resolve(string clientId)
    {
        if (string.IsNullOrWhiteSpace(clientId))
            throw new WeChatClientNotFoundException(clientId ?? string.Empty);

        foreach (var client in _factory.GetWeChatClientList().Values)
        {
            if (string.Equals(client.NickName, clientId, StringComparison.Ordinal)
                || string.Equals(client.WxId, clientId, StringComparison.Ordinal))
            {
                return client;
            }
        }

        throw new WeChatClientNotFoundException(clientId);
    }

    public IReadOnlyList<WeChatClient> GetAll()
        => _factory.GetWeChatClientList().Values.ToList();
}

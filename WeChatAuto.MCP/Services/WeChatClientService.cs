using System.Text.Json;
using ModelContextProtocol.Protocol;
using WeChatAuto.Components;
using WeChatAuto.Models;
using WeAutoCommon.Models;

/// <summary>
/// 微信客户端服务,提供微信客户端的获取、设置等操作
/// </summary>
public class WeChatClientService : IDisposable
{
    private volatile bool _disposed = false;
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

    /// <summary>
    /// 查找并打开好友或者群聊昵称,如果找到，则打开好友或者群聊窗口
    /// </summary>
    /// <param name="who">好友或者群聊昵称</param>
    /// <returns>是否找到并打开会话</returns>
    public WeAutoCommon.Models.Result FindAndOpenFriendOrGroup(string who)
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        return client.FindAndOpenFriendOrGroup(who);
    }

    /// <summary>
    /// 获取所有气泡标题列表，默认获取10页气泡标题列表
    /// <see cref="ChatSimpleMessage"/>
    /// </summary>
    /// <param name="who">好友名称，可以是好友，也可以是群聊名称</param>
    /// <param name="pageCount">获取的气泡数量，默认是10页,可以指定获取的页数，如果指定为-1，则获取所有气泡</param>
    /// <returns>所有气泡标题列表</returns>
    public List<ChatSimpleMessage> GetChatAllHistory(string who, int pageCount = 10)
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        return client.GetChatAllHistory(who, pageCount);
    }
    /// <summary>
    /// 发送消息给指定好友
    /// </summary>
    /// <param name="who">好友名称</param>
    /// <param name="message">消息内容</param>
    /// <returns>是否发送成功</returns>
    public async Task<string> SendMessage(string who, string message)
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        await client.SendWho(who, message);
        return JsonSerializer.Serialize(new { success = true });
    }
    /// <summary>
    /// 发送消息给指定好友(或者群聊昵称)，并@指定用户,仅适用于群聊
    /// </summary>
    /// <param name="who">好友或者群聊昵称</param>
    /// <param name="message">消息内容</param>
    /// <param name="atUser">被@的用户,仅适用于群聊</param>
    public async Task SendMessage(string who, string message, string atUser = "")
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        await client.SendWho(who, message, atUser);
    }
    /// <summary>
    /// 发送消息给指定好友(或者群聊昵称)，并@指定用户,仅适用于群聊
    /// </summary>
    /// <param name="who">好友或者群聊昵称</param>
    /// <param name="message">消息内容</param>
    /// <param name="atUsers">被@的用户,仅适用于群聊</param>
    /// <returns>操作结果</returns>
    public async Task<WeAutoCommon.Models.Result> SendMessage(string who, string message, string[] atUsers)
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        await client.SendWho(who, message, atUsers);
        return WeAutoCommon.Models.Result.Ok();
    }

    /// <summary>
    /// 批量发送消息给指定多个好友(或者群聊昵称)
    /// </summary>
    /// <param name="whos">好友或者群聊昵称列表</param>
    /// <param name="message">消息内容</param>
    /// <returns>操作结果</returns>
    public async Task<WeAutoCommon.Models.Result> SendWhos(string[] whos, string message)
    {
        var clientName = GetCurrentWeChatClientName();
        var client = _weChatClientFactory.GetWeChatClient(clientName);
        await client.SendWhos(whos, message);
        return WeAutoCommon.Models.Result.Ok();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;
        if (disposing)
        {
            _weChatClientFactory?.Dispose();
        }
    }
    ~WeChatClientService()
    {
        Dispose(false);
    }

}
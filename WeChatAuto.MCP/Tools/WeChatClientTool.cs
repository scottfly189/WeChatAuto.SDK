using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;
using WeChatAuto.Components;

[McpServerToolType]
public sealed class WeChatClientTool
{
    private readonly WeChatClientService _weChatClientService;
    public WeChatClientTool(WeChatClientService weChatClientService)
    {
        _weChatClientService = weChatClientService;
    }
    [McpServerTool, Description("获取本机打开的所有微信客户端名称")]
    public async Task<string> GetAllWeChatClientNames()
    {
        var clientNames = _weChatClientService.GetAllWeChatClientNames();
        return await Task.FromResult(JsonSerializer.Serialize(clientNames));
    }

    [McpServerTool, Description("获取当前使用的微信客户端名称,系统默认使用获得的第一个微信客户端名称作为当前微信客户端名称")]
    public async Task<string> GetCurrentWeChatClientName()
    {
        var clientName = _weChatClientService.GetCurrentWeChatClientName();
        return await Task.FromResult(clientName ?? string.Empty);
    }

    [McpServerTool, Description("查找并打开好友或者群聊昵称,如果找到，则打开好友或者群聊窗口")]
    public async Task<string> FindAndOpenFriendOrGroup([Description("好友或者群聊昵称")] string who)
    {
        var result = _weChatClientService.FindAndOpenFriendOrGroup(who);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("获取指定好友或者群聊的所有聊天记录，默认获取10页聊天记录，如果指定页数，则获取指定页数的聊天记录")]
    public async Task<string> GetChatAllHistory([Description("好友或者群聊昵称")] string who, [Description("获取的聊天记录数量，默认是10页,可以指定获取的页数,如果指定页数，则获取指定页数的聊天记录")] int pageCount = 10)
    {
        var result = _weChatClientService.GetChatAllHistory(who, pageCount);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发送消息给指定好友(或者群聊昵称)")]
    public async Task<string> SendMessage(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message)
    {
        await _weChatClientService.SendMessage(who, message);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }

    [McpServerTool, Description("发送消息给指定好友(或者群聊昵称)，并@指定一个好友,仅适用于群聊")]
    public async Task<string> SendMessageWithAtUser(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message,
        [Description("被@的用户,仅适用于群聊")] string atUser)
    {
        await _weChatClientService.SendMessage(who, message, atUser);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }
    [McpServerTool, Description("发送消息给指定好友(或者群聊昵称)，并@指定多个好友,仅适用于群聊")]
    public async Task<string> SendMessageWithAtUsers(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message,
        [Description("被@的用户列表,仅适用于群聊")] string[] atUsers)
    {
        var result = await _weChatClientService.SendMessage(who, message, atUsers);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发送消息给指定多个好友(或者群聊昵称)")]
    public async Task<string> SendMessagesWithAtUsers(
        [Description("好友或者群聊昵称列表")] string[] whos,
        [Description("消息内容")] string message)
    {
        var result = await _weChatClientService.SendWhos(whos, message);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发起语音聊天,适用于群聊中发起语音聊天")]
    public async Task<string> SendVoiceChats(
        [Description("群聊名称")] string groupName,
        [Description("好友名称列表,可以指定多个好友名称")] string[] whos)
    {
        _weChatClientService.SendVoiceChats(groupName, whos);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }
    
    [McpServerTool, Description("发起视频聊天,适用于单个好友")]
    public async Task<string> SendVideoChat(
        [Description("好友名称,微信好友昵称")] string who)
    {
        _weChatClientService.SendVideoChat(who);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }
}
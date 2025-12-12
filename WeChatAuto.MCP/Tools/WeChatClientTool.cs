using System.ComponentModel;
using System.Diagnostics.Contracts;
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


    [McpServerTool, Description("获取指定好友或者群聊的所有聊天记录，默认获取10页聊天记录，如果指定页数，则获取指定页数的聊天记录")]
    public async Task<string> GetChatAllHistory([Description("好友或者群聊昵称")] string who, [Description("获取的聊天记录数量，默认是10页,可以指定获取的页数,如果指定页数，则获取指定页数的聊天记录")] int pageCount = 10)
    {
        var result = _weChatClientService.GetChatAllHistory(who, pageCount);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发送文字消息给指定好友(或者群聊昵称)。这是发送普通文字消息的标准方法，需要提供消息内容。如果只是发送文字消息，请使用此方法而不是其他方法。")]
    public async Task<string> SendMessage(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message)
    {
        await _weChatClientService.SendMessage(who, message);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }

    [McpServerTool, Description("发送文字消息给指定好友(或者群聊昵称)，并@指定一个好友,仅适用于群聊")]
    public async Task<string> SendMessageWithAtUser(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message,
        [Description("被@的用户,仅适用于群聊")] string atUser)
    {
        await _weChatClientService.SendMessage(who, message, atUser);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }
    [McpServerTool, Description("发送文字消息给指定好友(或者群聊昵称)，并@指定多个好友,仅适用于群聊")]
    public async Task<string> SendMessageWithAtUsers(
        [Description("好友或者群聊昵称")] string who,
        [Description("消息内容")] string message,
        [Description("被@的用户列表,仅适用于群聊")] string[] atUsers)
    {
        var result = await _weChatClientService.SendMessage(who, message, atUsers);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发送文字消息给指定多个好友(或者群聊昵称)")]
    public async Task<string> SendMessages(
        [Description("好友或者群聊昵称列表")] string[] whos,
        [Description("消息内容")] string message)
    {
        var result = await _weChatClientService.SendWhos(whos, message);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("发起语音通话(语音聊天)给指定好友或者群聊。注意：这不是发送消息的方法，而是发起实时语音通话。如果需要发送文字消息，请使用SendMessage等方法。")]
    public async Task<string> StartVoiceChats(
        [Description("群聊名称")] string groupName,
        [Description("好友名称列表,可以指定多个好友名称")] string[] whos)
    {
        _weChatClientService.SendVoiceChats(groupName, whos);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }

    [McpServerTool, Description("发起视频通话(视频聊天)给指定好友。注意：这不是发送消息的方法，而是发起实时视频通话。如果需要发送文字消息，请使用SendMessage等方法。")]
    public async Task<string> StartVideoChat(
        [Description("好友名称,微信好友昵称")] string who)
    {
        _weChatClientService.SendVideoChat(who);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }
    [McpServerTool, Description("发布群聊公告")]
    public async Task<string> PublishGroupChatNotice(
        [Description("微信群聊名称")] string groupName,
        [Description("公告内容")] string notice)
    {
        var result = await _weChatClientService.PublishGroupChatNotice(groupName, notice);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }

    [McpServerTool, Description("删除聊天记录(或者说聊天内容)")]
    public async Task<string> ClearChatGroupHistory(
        [Description("微信好友或者群聊名称")] string groupName)
    {
        await _weChatClientService.ClearChatGroupHistory(groupName);
        return await Task.FromResult(JsonSerializer.Serialize(new { success = true }));
    }

    [McpServerTool, Description("转发消息,默认转发5行消息，可以指定转发行数")]
    public async Task<string> ForwardMessage(
        [Description("转发消息的来源,可以是好友名称，也可以是群聊名称")] string fromWho,
        [Description("转发消息的接收者,可以是好友名称，也可以是群聊名称")] string toWho,
        [Description("转发消息的行数,默认是5行,可以指定转发行数")] int rowCount = 5)
    {
        var result = await _weChatClientService.ForwardMessage(fromWho, toWho, rowCount);
        return await Task.FromResult(JsonSerializer.Serialize(result));
    }
}
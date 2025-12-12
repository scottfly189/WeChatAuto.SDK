using System.ComponentModel;
using ModelContextProtocol.Server;

[McpServerPromptType]
public sealed class WeChatPrompt
{
    [McpServerPrompt, Description("获取本机打开的所有微信客户端名称")]
    public static string GetAllWeChatClientNamesPrompt() => """
    我本机打开了哪些微信客户端？
    """;
    [McpServerPrompt, Description("获取当前使用的微信客户端名称")]
    public static string GetCurrentWeChatClientNamePrompt() => """
    我当前使用的是哪个微信客户端？
    """;
    [McpServerPrompt, Description("获取指定好友或者群聊的聊天记录")]
    public static string GetChatHistoryPrompt([Description("好友或者群聊昵称")] string who) => """
    获取指定好友或者群聊的所有聊天记录，请根据好友或者群聊昵称: {who} 获取所有聊天记录
    """;
    [McpServerPrompt, Description("发送消息给指定好友或者群聊")]
    public static string SendMessagePrompt([Description("好友或者群聊昵称")] string who, [Description("消息内容")] string message) => $"""
    发送消息给指定好友或者群聊，请根据好友或者群聊昵称: {who} 发送消息: {message}
    """;

    [McpServerPrompt, Description("发送消息给指定好友或者群聊，并@指定用户")]
    public static string SendMessageWithAtUserPrompt([Description("好友或者群聊昵称")] string who, [Description("消息内容")] string message, [Description("被@的用户")] string atUser) => $"""
    发送消息给指定好友或者群聊，请根据好友或者群聊昵称: {who} 发送消息: {message}，并@指定用户: {atUser}
    """;
    [McpServerPrompt, Description("批量发送消息给指定多个好友或者群聊")]
    public static string SendWhosPrompt([Description("好友或者群聊昵称列表")] string[] whos, [Description("消息内容")] string message) => $"""
    批量发送消息给指定多个好友或者群聊，请根据好友或者群聊昵称列表: {whos} 发送消息: {message}
    """;
    [McpServerPrompt, Description("发起语音群聊,适用于群聊中发起语音聊天，可以指定多个好友名称")]
    public static string SendVoiceChatsPrompt([Description("群聊名称")] string groupName, [Description("好友名称列表,可以指定多个好友名称")] string[] whos) => $"""
    发起群聊中语音聊天，请根据群聊名称: {groupName} 和好友名称列表: {whos} 发起语音聊天
    """;
    [McpServerPrompt, Description("发起视频聊天,适用于单个好友")]
    public static string SendVideoChatPrompt([Description("好友名称,微信好友昵称")] string who) => $"""
    发起视频聊天,请根据好友名称: {who} 发起视频聊天
    """;
}
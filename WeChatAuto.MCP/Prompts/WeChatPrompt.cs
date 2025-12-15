using System.ComponentModel;
using ModelContextProtocol.Server;

namespace WeChatAuto.MCP.Prompts;


[McpServerPromptType]
public sealed class WeChatPrompt
{
    /// <summary>
    /// 获取本机打开的所有微信客户端名称的提示词
    /// </summary>
    [McpServerPrompt, Description("获取本机打开的所有微信客户端名称")]
    public static string GetAllWeChatClientNamesPrompt() => """
    我本机打开了哪些微信客户端？
    """;
    /// <summary>
    /// 获取当前使用的微信客户端名称的提示词
    /// </summary>
    [McpServerPrompt, Description("获取当前使用的微信客户端名称")]
    public static string GetCurrentWeChatClientNamePrompt() => """
    我当前使用的是哪个微信客户端？
    """;
    /// <summary>
    /// 获取指定好友或者群聊的聊天记录的提示词
    /// </summary>
    [McpServerPrompt, Description("获取指定好友或者群聊的聊天记录")]
    public static string GetChatHistoryPrompt([Description("好友或者群聊昵称")] string who) => $"""
    获取指定好友或者群聊的所有聊天记录，请根据好友或者群聊昵称: {who} 获取所有聊天记录
    """;
    /// <summary>
    /// 获取指定好友或者群聊的聊天记录并进行摘要的提示词
    /// </summary>
    [McpServerPrompt, Description("获取指定好友或者群聊的聊天记录并进行摘要")]
    public static string GetChatHistoryAndSummaryPrompt([Description("好友或者群聊昵称")] string who) => $"""
    获取指定好友或者群聊的所有聊天记录并进行摘要，请获取好友: {who} 的聊天记录，并根据聊天记录进行摘要，并返回摘要结果
    """;
    /// <summary>
    /// 发送消息给指定好友或者群聊的提示词
    /// </summary>
    [McpServerPrompt, Description("发送消息给指定好友或者群聊")]
    public static string SendMessagePrompt([Description("好友或者群聊昵称")] string who, [Description("消息内容")] string message) => $"""
    发送消息给指定好友或者群聊，请根据好友或者群聊昵称: {who} 发送消息: {message}
    """;
    /// <summary>
    /// 发送消息给指定好友或者群聊，并@指定用户
    /// </summary>
    [McpServerPrompt, Description("发送消息给指定好友或者群聊，并@指定用户")]
    public static string SendMessageWithAtUserPrompt([Description("好友或者群聊昵称")] string who, [Description("消息内容")] string message, [Description("被@的用户")] string atUser) => $"""
    发送消息给指定好友或者群聊，请根据好友或者群聊昵称: {who} 发送消息: {message}，并@指定用户: {atUser}
    """;
    /// <summary>
    /// 批量发送消息给指定多个好友或者群聊的提示词
    /// </summary>
    [McpServerPrompt, Description("批量发送消息给指定多个好友或者群聊")]
    public static string SendWhosPrompt([Description("好友或者群聊昵称列表")] string[] whos, [Description("消息内容")] string message) => $"""
    批量发送消息给指定多个好友或者群聊，请根据好友或者群聊昵称列表: {whos} 发送消息: {message}
    """;
    /// <summary>
    /// 发起语音聊天,适用于群聊中发起语音聊天，可以指定多个好友名称
    /// </summary>
    [McpServerPrompt, Description("发起语音聊天,适用于群聊中发起语音聊天，可以指定多个好友名称")]
    public static string SendVoiceChatsPrompt([Description("群聊名称")] string groupName, [Description("好友名称列表,可以指定多个好友名称")] string[] whos) => $"""
    发起语音聊天，请根据群聊名称: {groupName} 和好友名称列表: {whos} 发起语音聊天
    """;
    /// <summary>
    /// 发起视频聊天,适用于单个好友
    /// </summary>
    [McpServerPrompt, Description("发起视频聊天,适用于单个好友")]
    public static string SendVideoChatPrompt([Description("好友名称,微信好友昵称")] string who) => $"""
    发起视频聊天,请根据好友名称: {who} 发起视频聊天
    """;
    /// <summary>
    /// 获取指定好友或者群聊的聊天记录并进行摘要，并发送给指定好友或者群聊的提示词
    /// </summary>
    [McpServerPrompt, Description("获取指定好友或者群聊的聊天记录并进行摘要，并发送给指定好友或者群聊")]
    public static string SummaryChatHistoryAndSendMessagePrompt([Description("获取的聊天记录的好友或者群聊昵称")] string getWho, [Description("发送给的好友或者群聊昵称")] string sendToWho) =>
    $"""
    获取指定好友或者群聊的所有聊天记录并进行摘要，请获取好友: {getWho} 的聊天记录，并根据聊天记录进行摘要，交将摘要结果发给好友或者群聊: {sendToWho}
    """;
    /// <summary>
    /// 转发消息,默认转发5行消息，可以指定转发行数
    /// </summary>
    [McpServerPrompt, Description("转发消息,默认转发5行消息，可以指定转发行数")]
    public static string ForwardMessagePrompt([Description("转发消息的来源,可以是好友名称，也可以是群聊名称")] string fromWho, [Description("转发消息的接收者,可以是好友名称，也可以是群聊名称")] string toWho, [Description("转发消息的行数,默认是5行,可以指定转发行数")] int rowCount = 5) => $"""
    请帮我从 {fromWho} 转发消息给 {toWho} ，转发消息的行数: {rowCount}
    """;
    /// <summary>
    /// 发布群聊公告
    /// </summary>
    [McpServerPrompt, Description("发布群聊公告")]  
    public static string PublishGroupChatNoticePrompt([Description("群聊名称")] string groupName, [Description("公告内容")] string notice) => $"""
    请帮我发布群聊公告，群聊名称: {groupName} ，公告内容: {notice}
    """;
    /// <summary>
    /// 删除聊天记录
    /// </summary>
    [McpServerPrompt, Description("删除聊天记录")]
    public static string ClearChatGroupHistoryPrompt([Description("微信好友或者群聊名称")] string groupName) => $"""
    请帮我删除 {groupName} 的聊天记录
    """;
}
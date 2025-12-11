using System.ComponentModel;
using ModelContextProtocol.Server;

[McpServerPromptType]
public sealed class WeChatPrompt
{
    [McpServerPrompt,Description("获取本机打开的所有微信客户端名称")]
    public static string GetAllWeChatClientNamesPrompt() => """
    我本机打开了哪些微信客户端？
    """;
}
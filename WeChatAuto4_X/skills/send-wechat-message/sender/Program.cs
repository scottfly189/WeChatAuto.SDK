/*
 * send-wechat：通过 WeChatAuto.SDK 发送微信文本消息的命令行小工具。
 *
 * 用法（由 SKILL.md 指导调用）：
 *   send-wechat <微信账号昵称> <好友/群聊名> <消息内容>    给指定好友/群聊发文本
 *   send-wechat <微信账号昵称> - <消息内容>               给当前聊天窗口发文本
 *   send-wechat --list                                   列出当前打开的微信账号
 *   send-wechat --help                                   显示本帮助
 *
 * 前置条件：
 *   1. Windows 上已登录并打开微信 4.x 客户端（窗口不能最小化/隐藏）；
 *   2. 若微信未公开 UI Tree，需开启 IsForceOpenUITree（默认已开启）。
 * 说明：发送纯文本消息走 UIA 搜索 + 文本输入，不依赖 OCR，无需准备模型文件。
 */

using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

return await RunAsync(args);

static async Task<int> RunAsync(string[] args)
{
    if (args.Length == 0 || args[0] is "--help" or "-h")
    {
        PrintUsage();
        return 0;
    }

    // 初始化 SDK（内部 DI）。
    var serviceProvider = WeAutomation.Initialize(options =>
    {
        options.DebugMode = false;          // 生产环境建议关闭
        options.EnableOCR = false;          // 纯文本发送不需要 OCR，关闭以避免依赖模型文件
        options.IsForceOpenUITree = true;   // 若微信未公开 UI Tree，则强制打开
    });

    using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();

    if (args[0] == "--list")
    {
        var names = clientFactory.GetWeChatClientNames();
        Console.WriteLine($"检测到 {names.Count} 个微信客户端：{(names.Count == 0 ? "（无）" : string.Join("、", names))}");
        return names.Count == 0 ? 1 : 0;
    }

    if (args.Length < 3)
    {
        Console.Error.WriteLine("错误：参数不足。");
        PrintUsage();
        return 2;
    }

    var account = args[0];
    var who = args[1] == "-" ? null : args[1];
    var message = string.Join(" ", args.Skip(2)).Trim();

    if (string.IsNullOrEmpty(message))
    {
        Console.Error.WriteLine("错误：消息内容不能为空。");
        return 2;
    }

    try
    {
        var client = clientFactory.GetWeChatClient(account);

        if (string.IsNullOrEmpty(who))
        {
            await client.SendMessage(message);          // 发给当前聊天窗口
        }
        else
        {
            await client.SendMessage(who, message);     // 发给指定好友/群聊
        }

        Console.WriteLine($"成功：已通过微信 [{account}] 向 [{(who ?? "当前窗口")}] 发送：{message}");
        return 0;
    }
    catch (Exception ex)
    {
        var names = clientFactory.GetWeChatClientNames();
        Console.Error.WriteLine($"失败：{ex.Message}");
        Console.Error.WriteLine($"当前可用的微信账号：{(names.Count == 0 ? "（无）" : string.Join("、", names))}");
        return 1;
    }
}

static void PrintUsage()
{
    Console.WriteLine("""
        用法：
          send-wechat <微信账号昵称> <好友/群聊名> <消息内容>    给指定好友/群聊发文本
          send-wechat <微信账号昵称> - <消息内容>               给当前聊天窗口发文本
          send-wechat --list                                   列出当前打开的微信账号
          send-wechat --help                                   显示本帮助

        说明：
          <微信账号昵称>：本机微信账号本人的昵称（用 --list 查看），不是好友名。
          <好友/群聊名>：会话列表里肉眼可见的标题（好友备注/昵称，或群名）。
        """);
}

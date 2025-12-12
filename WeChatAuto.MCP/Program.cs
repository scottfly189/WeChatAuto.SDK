using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using ModelContextProtocol;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddLogging(logging  =>
{
    logging.ClearProviders();
    logging.AddConsole(options =>
    {
        options.LogToStandardErrorThreshold = LogLevel.Trace;
    });
});
//注册MCP服务，使用标准输入输出传输，从程序集加载工具、提示和资源
WeAutomation.Initialize(builder.Services, options =>
{
    options.DebugMode = true;
    options.EnableMouseKeyboardSimulator = false;
    options.EnableRecordVideo = true;
}).AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly()
    .WithResources<WeChatHistoryResource>();

builder.Services.AddSingleton<WeChatClientService>();

Alert.Show($"当前进程ID: {Process.GetCurrentProcess().Id}");

var host = builder.Build();

//运行MCP服务
await host.RunAsync();
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using ModelContextProtocol;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using WeChatAuto.Components;

var builder = Host.CreateApplicationBuilder(args);
//注册MCP服务，使用标准输入输出传输，从程序集加载工具、提示和资源
WeAutomation.Initialize(builder.Services, options =>
{
    options.DebugMode = true;
    options.EnableMouseKeyboardSimulator = false;
}).AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly()
    .WithResourcesFromAssembly();

var host = builder.Build();

//运行MCP服务
await host.RunAsync();
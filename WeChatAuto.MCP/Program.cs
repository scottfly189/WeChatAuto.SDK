using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeChatAuto.MCP.Utils;
using WeChatAuto.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole(options =>
    {
        options.LogToStandardErrorThreshold = LogLevel.Trace;
    });
});
WeAutomation.Initialize(builder.Services, options =>
{
    options.DebugMode = false;  //测试环境可以开启DebugMode
    options.EnableMouseKeyboardSimulator = false;
    options.EnableRecordVideo = false;
}).AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly();

builder.Services.AddSingleton<WeChatClientService>();

Alert.Show($"当前进程ID: {Process.GetCurrentProcess().Id}");

var host = builder.Build();

try
{
    //运行MCP服务
    await host.RunAsync();
}
catch (Exception ex)
{
    // 记录异常（如果需要）
    Alert.Show($"MCP服务运行异常: {ex.Message}");
    throw;
}
finally
{
    Alert.Show("MCP服务已停止");
}
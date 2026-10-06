/********************************************************************/
/*          WeChatAuto.SDK的websocket server 社区版                 */
/*                                                                 */
/* author: alex                                                    */
/* date: 2026-02-20                                                */
/* 注意:社区版不包含微信消息获取功能，需要完整版本请加入vip            */
/******************************************************************/

using Microsoft.Extensions.DependencyInjection;
using WeChatAuto;
using WeChatAuto.Services;
using WeChatAuto.Components;


var builder = WebApplication.CreateBuilder(args);

//初始化WeChatAuto.SdK
WeAutomation.Initialize(builder.Services, options =>
{
    options.DebugMode = false;
    //options.InitAdressBook = false;  //是否初始化通讯录，如果通讯录比较大，可能会比较慢.
    options.EnableOCR = true;
    //options.EnableMouseKeyboardSimulator = false;   //是否允许键鼠模拟器，如果允许，下面是键鼠模拟器的配置
    //options.KMDeviceVID = 0x1701;
    //options.KMDevicePID = 0x2612;
    //options.KMVerifyUserData = "4F6A21981BE675822DEE7B9BC39F3791";
});
builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

builder.Services.AddSingleton<ConnectionManager>();
builder.Services.AddScoped<WebSocketHandler>();
builder.Services.AddScoped<SocketSessionChannel>();
builder.Services.AddTransient<MessageHandler>();

var app = builder.Build();

var loggerFactory  = app.Services.GetService<ILoggerFactory>();
var logger = loggerFactory?.CreateLogger(nameof(Program));
var factory = app.Services.GetRequiredService<WeChatClientFactory>();
var clients = factory.GetWeChatClientNames();
logger?.LogInformation($"总共打开微信 {clients.Count()} 个：{string.Join(",", clients)}");

app.UseWebSockets();


app.Map("/ws", async (HttpContext context, WebSocketHandler handler, CancellationToken userToken) =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        return;
    }

    var ws = await context.WebSockets.AcceptWebSocketAsync();
    await handler.HandleAsync(ws, userToken);
});

app.Run();
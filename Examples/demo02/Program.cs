using Microsoft.Extensions.Hosting;
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

WeAutomation.Initialize(builder.Services, options =>
{
    //开启调试模式，调试模式会在获得焦点时边框高亮，生产环境建议关闭
    options.DebugMode = true;
    //开启录制视频功能，录制的视频会保存在项目的运行目录下的Videos文件夹中
    //options.EnableRecordVideo = true;  
});

//这里注入自已的服务（或者对象），如LLM服务等
builder.Services.AddSingleton<LLMService>();

var serviceProvider = builder.Services.BuildServiceProvider();
var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// 得到名称为"AI.net"的微信客户端实例，测试时请将AI.net替换为你自己的微信昵称
var client = clientFactory.GetWeChatClient("AI.net");


var app = builder.Build();
await app.RunAsync();

public class LLMService
{
    public string LLMDoSomething() => "Hello World!";
}

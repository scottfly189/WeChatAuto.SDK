using Microsoft.Extensions.Hosting;
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
// 得到名称为"Alex"的微信客户端实例，测试时请将AI.net替换为你自己的微信昵称
var client = clientFactory.GetWeChatClient("Alex");
await client.AddMessageListener("测试11", (messageContext) =>
{
    var index = 0;
    //打印收到最新消息
    foreach (var message in messageContext.NewMessages)
    {
        index++;
        Console.WriteLine($"收到消息：{index}：{message.ToString()}");
        Console.WriteLine($"收到消息：{index}：{message.Who}：{message.MessageContent}");
    }
    //打印收到所有消息的后十条
    var allMessages = messageContext.AllMessages.Skip(messageContext.AllMessages.Count - 10).ToList();
    index = 0;
    foreach (var message in allMessages)
    {
        index++;
        Console.WriteLine($"...收到所有消息的前10条之第{index}条：{message.Who}：{message.MessageContent}");
        Console.WriteLine($".................详细之第{index}条：{message.ToString()}");
    }
    //是否有人@我
    if (messageContext.IsBeAt())
    {
        var messageBubble = messageContext.MessageBubbleIsBeAt().FirstOrDefault();
        if (messageBubble != null)
        {
            messageContext.SendMessage("我被@了！！！！我马上就回复你！！！！", new List<string> { messageBubble.Who });
        }
        else
        {
            messageContext.SendMessage("我被@了！！！！我马上就回复你！！！！");
        }
    }
    //是否有人引用了我的消息
    if (messageContext.IsBeReferenced())
    {
        messageContext.SendMessage("我被引用了！！！！");
    }
    //是否有人拍了拍我
    if (messageContext.IsBeTap())
    {
        messageContext.SendMessage("我被拍一拍了[微笑]！！！！");
    }
    if (!messageContext.IsBeAt() && !messageContext.IsBeReferenced() && !messageContext.IsBeTap())
    {
        //回复消息，这里可以引入大模型自动回复
        messageContext.SendMessage($"我收到了{messageContext.NewMessages.FirstOrDefault()?.Who}的消息：{messageContext.NewMessages.FirstOrDefault()?.MessageContent}");
    }
    //可以通过注入的服务容器获取你注入的服务实例，然后调用你的业务逻辑,一般都是LLM的自动回复逻辑
    var llmService = messageContext.ServiceProvider.GetRequiredService<LLMService>();
    llmService.DoSomething();
},
//下面的firstMessageAction可选，适用于添加消息监听时，需要我首先发送一些消息给好友的场景
sender =>
{
    //发送文本消息
    sender.SendMessage("你好啊！我是AI.Net,很高兴认识你！");
    //发送表情
    //sender.SendEmoji(1);
    //发送文件,改成你的文件路径
    //sender.SendFile(new string[] { @"C:\Users\Administrator\Desktop\me\avatar.png" });
});


var app = builder.Build();
await app.RunAsync();

/// <summary>
/// 一个包含LLM服务的Service类，用于注入到MessageContext中
/// </summary>
public class LLMService
{
    private ILogger<LLMService> _logger;
    public LLMService(ILogger<LLMService> logger)
    {
        _logger = logger;
    }
    public void DoSomething()
    {
        _logger.LogInformation("这里是你注入的服务实例，可以在这里编写你的业务逻辑  ");
    }
}

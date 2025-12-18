using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;

// 初始化WeAutomation服务
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;   //开启调试模式，调试模式会在获得焦点时边框高亮，生产环境建议关闭
    //options.EnableRecordVideo = true;  //开启录制视频功能，录制的视频会保存在项目的运行目录下的Videos文件夹中
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
Console.WriteLine($"当前客户端打开的微信客户端为：{string.Join(",", clientFactory.GetWeChatClientNames())}，共计{clientFactory.GetWeChatClientNames().Count}个微信客户端。");
//获取当前打开的微信客户端名称列表
var clentNames = clientFactory.GetWeChatClientNames();    
//获取第一个微信客户端
var wxClient = clientFactory.GetWeChatClient(clentNames.First());  
 //通过第一个微信客户端发送消息给AI.Net好友昵称，请修改成自己的好友昵称
wxClient?.SendWho("AI.Net","你好，欢迎使用WeChatAuto.SDK微信自动化框架！"); 


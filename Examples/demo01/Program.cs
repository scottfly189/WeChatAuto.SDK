using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;


var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
Console.WriteLine($"当前客户端打开的微信客户端为：{string.Join(",", clientFactory.GetWxClientNames())}，共计{clientFactory.GetWxClientNames().Count}个微信客户端。");
var wxClientNames = clientFactory.GetWxClientNames();
var wxClient = clientFactory.GetWeChatClient(wxClientNames[0]);
wxClient?.SendWho("AI.Net","你好，欢迎使用AI.Net微信自动化框架！");


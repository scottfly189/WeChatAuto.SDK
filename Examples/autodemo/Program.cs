using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// 请修改为你的微信昵称
var client = clientFactory.GetWeChatClient("Alex");

client.AddFriendRequestAutoAcceptAndOpenChatListener((context) =>
{
    // Console.WriteLine($"收到好友请求：{messageContext.Sender.NickName}");
    // //自动接受好友请求
    // client.AcceptFriendRequest(messageContext.Sender.UserName);
    //return WeAutoCommon.Models.Result.Ok();
    Console.WriteLine($"收到好友请求：");
}, async (sender) =>
{
    sender.SendMessage("你好，我是wechatauto.sdk测试功能导航机器人，很高兴认识你！现在让我带你体验一下wechatauto.sdk的部分功能....咱们开始咯....");
    await Task.Delay(1000);
},"wechatauto","test","wechatauto");

await Task.Delay(-1);
client.StopNewUserListener();

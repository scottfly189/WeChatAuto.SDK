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
    context.SendMessage($"我收到您发的消息\"{context.GetAllMessages().LastOrDefault()?.MessageContent}\",不过我还没有接入LLM大模型，无法回答您的问题，还是让我带您体验一下wechatauto.sdk的一些基本功能吧,如果有什么问题，可以事后联系作者！");
}, async (sender) =>
{
    sender.SendMessage("亲，终于盼到你了，我是wechatauto.sdk测试功能导航机器人，很高兴认识你！现在让我带你体验一下wechatauto.sdk的部分功能....咱们开始咯....我准备发送图片消息:");
    await Task.Delay(1000);
    sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Images/1.png" });
    await Task.Delay(1000);
    sender.SendMessage("我准备发送表情消息:");
    sender.SendEmoji(1);
    await Task.Delay(1000);
    sender.SendMessage("我准备发送视频文件:");
    sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Videos/1.mp4" });
},"wechatauto","test","wechatauto");

await Task.Delay(-1);
client.StopNewUserListener();

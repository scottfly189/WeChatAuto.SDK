using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WeAutoCommon.Utils;

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// 请修改为你的微信昵称
var client = clientFactory.GetWeChatClient("Alex");

var index = 0;

client.AddFriendRequestAutoAcceptAndOpenChatListener((context) =>
{
    context.SendMessage($"我收到您发的消息\"{context.GetAllMessages().LastOrDefault()?.MessageContent}\",不过我还没有接入LLM大模型，无法回答您的问题，还是让我带您体验一下wechatauto.sdk的一些基本功能吧,如果有什么问题，可以事后联系作者！");
}, async (sender) =>
{
    index++;
    sender.SendMessage("亲，终于盼到你了，我是wechatauto.sdk测试功能导航机器人，很高兴认识你！现在让我带你体验一下wechatauto.sdk的部分功能....咱们开始咯....我准备发送图片消息:");
    await RandomWait.WaitAsync(1000, 3000);
    sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Images/1.png" });
    await RandomWait.WaitAsync(1000, 3000);
    sender.SendMessage("我准备发送表情消息:");
    sender.SendEmoji(1);
    await RandomWait.WaitAsync(1500, 3000);
    sender.SendMessage("我准备发送视频文件:");
    sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Videos/1.mp4" });
    await Task.Delay(10000);
    sender.SendMessage("现在...我准备拉你到一个测试群里，请稍候...");
    await RandomWait.WaitAsync(1000, 5000);
    var groupName = "wechatauto测试群" + index;
    client.CreateOrUpdateOwnerChatGroup(groupName, new string[] { "khcgb", sender.FullTitle });
    await RandomWait.WaitAsync(1000, 5000);
    await client.SendWho(groupName, "欢迎来到群聊!大家可以一起开心聊天了...", "所有人");
    await RandomWait.WaitAsync(1000, 5000);
    await client.SendWho(groupName, "群里面的Alex是作者，有什么问题可以联系他...", sender.FullTitle);
    await RandomWait.WaitAsync(2000, 5000);
    await client.SendWho(groupName, "群聊里可以进行一些经典操作：如发送消息，修改群备注，自动增加好友，删除好友等等...这些功能等您探索哦.", sender.FullTitle);
    await RandomWait.WaitAsync(2000, 5000);
    await client.SendWho(groupName, "现在进行群删除操作....我会把群所有人都清空，然后把群也删除掉...", sender.FullTitle);
    await RandomWait.WaitAsync(3000, 10000);
    await client.DeleteOwnerChatGroup(groupName);
    sender.SendMessage("怎么样?....是不是很智能，呵呵,WeChatAuto天生为人工智能而生，接入LLM更智能哦!");
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("为了不占用资源，我把您删除哦！如果你以后有需要请重新加我....温馨提示：请备注: wechatauto ,否则可能不会通过");
    client.RemoveFriend(sender.FullTitle);
}, "test", "test", "wechatauto", false);

await Task.Delay(-1);
client.StopNewUserListener();

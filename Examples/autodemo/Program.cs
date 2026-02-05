using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WeAutoCommon.Utils;

/*************************************************************
此demo为一个完整的自动化脚本，包括：
1. 自动接受好友请求
2. 自动打开聊天窗口
3. 自动发送消息
4. 自动发送文件
5. 自动发送表情
6. 自动发送视频
7. 自动创建群聊
8. 自动@所有人
9. 自动修改群备注
10. 自动增加群好友
11. 自动发送群聊消息
12. 自动清空群成员
13. 自动删除群聊
14. 自动解散群聊
15. 自动删除好友
等部分全流程功能,请换成你自己的微信号和昵称，以及文件和图片路径
注意：由于微信风控的限制，一天之内不能太多的删除好友，删除群聊，所以使用时请谨慎.
*/

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
    options.EnableMouseKeyboardSimulator = true;
    //下面的内容可选，如果需要使用键鼠模拟器，请填写设备VID和PID，并启用键鼠模拟器，如果有校验数据，请填写校验数据
    options.KMDevicePID = 0x1701;
    options.KMDeviceVID = 0x2612;
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// 请修改为你的微信昵称
var client = clientFactory.GetWeChatClient("Alex");

var suffix = $"_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

client.AddFriendRequestAutoAcceptAndOpenChatListener((context) =>
{
    context.SendMessage($"我收到您发的消息\"{context.GetAllMessages().LastOrDefault()?.MessageContent}\",不过我还没有接入LLM大模型，无法回答您的问题，还是让我带您体验一下wechatauto.sdk的一些基本功能吧,如果有什么问题，可以事后联系作者！");
}, async (sender) =>
{
    sender.SendMessage("亲，终于盼到你了，我是wechatauto.sdk测试导航机器人，很高兴认识你！现在让我带你体验一下wechatauto.sdk的部分功能..大概1分钟时间..咱们开始咯....我准备发送图片消息:");
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
    var groupName = "wechatauto测试群" + suffix;
    client.CreateOrUpdateOwnerChatGroup(groupName, new string[] { "khcgb", sender.FullTitle });
    await RandomWait.WaitAsync(1000, 5000);
    await client.SendWho(groupName, "欢迎来到群聊!大家可以一起开心聊天了...🎉🎉", "所有人");
    await RandomWait.WaitAsync(1000, 5000);
    await client.SendWho(groupName, "群里面的Alex是作者，有什么问题可以联系他...", sender.FullTitle);
    await RandomWait.WaitAsync(2000, 5000);
    await client.SendFile(groupName, new string[] { $"{AppContext.BaseDirectory}/Images/1.png" });
    await RandomWait.WaitAsync(2000, 5000);
    await client.SendWho(groupName, "群聊里可以进行一些经典操作：如发送消息，监听群消息，修改群备注，自动增加群好友，删除好友等等...这些功能等您探索哦😊.");
    await RandomWait.WaitAsync(2000, 5000);
    await client.SendWho(groupName, "接下来将清空群成员，并删除该群聊，请稍候...", sender.FullTitle);
    await RandomWait.WaitAsync(3000, 10000);
    await client.DeleteOwnerChatGroup(groupName);
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("怎么样?....是不是很Cool?呵呵😊,WeChatAuto天生为人工智能而生，我的源码在github的VIP库里，您可以下载源码进行深度学习，另外，当您接入LLM大模型后将更智能哦🎉🎉🚀🚀");
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("另外：在Telegram上可以讨论WeChatAuto.SDK,请用Telegram加入下面的群:");
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("Telegram技术交流群: https://t.me/+1yUjlnKiXQtiZWU1");
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("更新通知Channel: https://t.me/+7gLw2qWmFiRmNzVl");
    await RandomWait.WaitAsync(2000, 5000);
    sender.SendMessage("感谢您的体验，为了节省资源，我将暂时把您移除好友。如果之后还有需要，欢迎随时加我哦！温馨提示：添加时请备注“wechatauto”，这样更容易通过好友申请。祝您生活愉快，我们下次再见！");
    await RandomWait.WaitAsync(2000, 5000);
    client.RemoveFriend(sender.FullTitle);
}, "test", "test", "wechatauto");

await Task.Delay(-1);
client.StopNewUserListener();

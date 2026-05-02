using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WeAutoCommon.Utils;

/*************************************************************
此demo为一个完整的自动化脚本，与autodemo2的区别点在于：
- 不删除好友
- 不解散群聊
- 不删除群好友

所以。。。。风控压力少很多
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

var semaphore = new SemaphoreSlim(1, 1); // 初始计数为1，保证同一时间只有一个任务执行

client.AddFriendRequestAutoAcceptAndOpenChatListener((context) =>
{
    context.SendMessage($"我收到您发的消息\"{context.GetAllMessages().LastOrDefault()?.MessageContent}\",不过我还没有接入LLM大模型，无法回答您的问题，还是让我带您体验一下wechatauto.sdk的一些基本功能吧,如果有什么问题，可以事后联系作者！");
}, async (sender) =>
{
    await semaphore.WaitAsync(); // 等待获取锁，如果已被占用则等待
    try
    {
        sender.SendMessage("亲，终于盼到你了，我是wechatauto.sdk测试导航机器人，很高兴认识你！现在让我带你体验一下wechatauto.sdk的部分功能..大概1分钟时间..咱们开始咯....我准备发送图片消息:");
        await RandomWait.WaitAsync(1000, 5000);
        sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Images/1.png" });
        await RandomWait.WaitAsync(1000, 5000);
        sender.SendMessage("我准备发送表情消息:");
        sender.SendEmoji(1);
        await RandomWait.WaitAsync(1500, 5000);
        sender.SendMessage("我准备发送视频文件:");
        sender.SendFile(new string[] { $"{AppContext.BaseDirectory}/Videos/1.mp4" });
        await RandomWait.WaitAsync(5000, 10000);
        sender.SendMessage("现在...我准备拉你到一个人工智能自动化技术讨论群（非VIP群），请稍候...");
        await RandomWait.WaitAsync(1000, 5000);
        var groupName = "人工智能自动化技术讨论群";
        client.CreateOrUpdateOwnerChatGroup(groupName, new string[] { sender.FullTitle });
        await RandomWait.WaitAsync(1000, 5000);
        await client.SendWho(groupName, $"欢迎🎉🎉{sender.FullTitle}🎉🎉来到本群!大家可以一起开心讨论人工智能自动化技术🎉🎉", "所有人");
        await RandomWait.WaitAsync(1000, 5000);
        await client.SendWho(groupName, "另外:群里面的Alex是作者，有什么问题可以联系他...", sender.FullTitle);
        await RandomWait.WaitAsync(2000, 5000);
        await client.SendFile(groupName, new string[] { $"{AppContext.BaseDirectory}/Images/1.png" });
        await RandomWait.WaitAsync(2000, 5000);
        await client.SendWho(groupName,
"""
群规（请务必阅读）

- 本群为严谨的技术讨论群，核心主题为：
人工智能在自动化领域中的应用、实践与原理。
- 禁止讨论任何政治或涉政敏感话题。
该类内容与群定位无关，且无法产生建设性讨论。
- 禁止发布关于公司、人事、职场抱怨、情绪宣泄等内容。
本群不提供情绪价值，仅聚焦技术本身。
- 禁止分享个人生活相关内容，包括但不限于：
旅游、美食、日常琐事、个人动态等。

请将公共讨论资源留给技术话题，踩红线必T

欢迎内容：
 技术问题与实践经验
 架构设计、实现思路、踩坑总结
 对 AI + 自动化 的独立思考与专业见解

🎉 理性讨论，观点自由；聚焦技术，拒绝灌水。祝您在本群玩得开心😊
""", sender.FullTitle);
        sender.SendMessage("怎么样?....是不是很Cool?呵呵😊,WeChatAuto天生为人工智能而生，我的源码在github的VIP库里，您可以下载源码进行深度学习，另外，当您接入LLM大模型后将更智能哦🎉🎉🚀🚀");
        await RandomWait.WaitAsync(2000, 5000);
        sender.SendMessage("另外：在Telegram上也可以讨论WeChatAuto.SDK,请用Telegram加入下面的群:");
        await RandomWait.WaitAsync(2000, 5000);
        sender.SendMessage("Telegram技术交流群: https://t.me/+1yUjlnKiXQtiZWU1");
        await RandomWait.WaitAsync(2000, 5000);
        sender.SendMessage("更新通知Channel: https://t.me/+7gLw2qWmFiRmNzVl");
        await RandomWait.WaitAsync(2000, 5000);
        sender.SendMessage("感谢您的体验，如果您有任何问题，可以随时联系作者，或者加入VIP群进行更深入的学习交流，祝您生活愉快,我做为测试导航机器人将暂时陪您到这，下次回复的会是人类😊");
        await RandomWait.WaitAsync(2000, 5000);
    }
    finally
    {
        semaphore.Release(); // 释放锁，让下一个等待的任务继续执行
    }
}, "test", "test", "wechatauto");

await Task.Delay(-1);
client.StopNewUserListener();

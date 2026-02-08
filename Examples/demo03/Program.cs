
/***
 * 示例三 - 消息管理演示 - 群中发送消息，并@好友
 */

using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = true;
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
var client = clientFactory.GetWeChatClient("Alex");
//发送消息给AI.Net好友昵称，请修改成自己的好友昵称
await client.SendWho("AI.Net", "你好，世界！");
//发送消息给群聊"测试11"，并好友:@123321
await client.SendWho("测试11", "你好，世界！", "123321");
//发送消息给群聊"测试11"，并好友:@123321,@Alex
await client.SendWho("测试11","你好，世界！",new string[]{"123321","Alex"});


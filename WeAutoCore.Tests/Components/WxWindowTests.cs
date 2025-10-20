using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCore.Utils;
using Xunit.Abstractions;
using WxAutoCommon.Configs;
using WeAutoCommon.Classes;
using System.Diagnostics;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace WeAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WxWindowTests
    {
        private readonly string _wxClientName = WeAutomation.Config.TestClientName;
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public WxWindowTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试窗口操作")]
        public async Task TestWindowAction()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.WindowMax();
            DrawHightlightHelper.DrawHightlight(window.Window, window.UiThreadInvoker);
            window.WindowRestore();
            DrawHightlightHelper.DrawHightlight(window.Window, window.UiThreadInvoker);
            window.WindowMin();
            await WeAutomation.Wait(2);
            window.WinMinRestore();
            window.WindowTop(true);
            await WeAutomation.Wait(2);
            window.WindowTop(false);
            await WeAutomation.Wait(2);
            Assert.True(true);
        }

        [Fact(DisplayName = "测试获取昵称")]
        public void TestNickName()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var nickName = window.NickName;
            _output.WriteLine($"昵称: {nickName}");
            Assert.Equal(_wxClientName, nickName);
        }

        [Fact(DisplayName = "测试获取当前聊天窗口的标题")]
        public void Test_GetCurrentChatTitle()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var title = window.GetCurrentChatTitle();
            _output.WriteLine($"当前聊天窗口的标题: {title}");
            Assert.True(title != null);
        }

        [Fact(DisplayName = "测试发送当前窗口消息-确保当前窗口是聊天窗口")]
        public async Task Test_SendMessage()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendCurrentMessage("你好，世界！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        //要先打开测试人的聊天窗口
        [Fact(DisplayName = "测试发送消息-已打开聊天窗口")]
        public async Task Test_SendWho_AlreadyOpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeAutomation.Config.TestFriendNickName, "你好，世界111！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-当前聊天窗口-确保打开是测试人的聊天窗口")]
        public async Task Test_SendWho_CurrentChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeAutomation.Config.TestFriendNickName, "你好，世界222！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeAutomation.Config.TestFriendNickName, "你好，世界333！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中,并打开聊天窗口")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList_OpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat(WeAutomation.Config.TestFriendNickName, "你好，世界333222！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,不在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_NOT_InConversationList()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeAutomation.Config.TestFriendNickName, "你好，世界444！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-不存在的人")]
        public async Task Test_SendWho_Not_Exist_Person()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("不存在的人", "你好，世界555！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息")]
        public async Task Test_SendWhoAndOpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat(WeAutomation.Config.TestFriendNickName, "你好，世界666！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量")]
        public async Task Test_SendWhos()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendWhos([WeAutomation.Config.TestFriendNickName, WeAutomation.Config.TestGroupNickName], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量,并打开聊天窗口")]
        public async Task Test_SendWhosAndOpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendWhosAndOpenChat([WeAutomation.Config.TestFriendNickName, WeAutomation.Config.TestGroupNickName], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送索引给指定好友")]
        public async Task Test_SendEmoji_Index()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeAutomation.Config.TestFriendNickName, EmojiListHelper.Items[0].Index, false);
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送名称给指定好友")]
        public async Task Test_SendEmoji_Name()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeAutomation.Config.TestFriendNickName, "微笑", false);
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送值给指定好友")]
        public async Task Test_SendEmoji_value()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeAutomation.Config.TestFriendNickName, "[微笑]", false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送表情-发送索引给指定好友-打开子窗口")]
        public async Task Test_SendEmoji_open_subwin()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeAutomation.Config.TestFriendNickName, EmojiListHelper.Items[0].Index, true);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送表情-发送索引给指定好友-发送给多个好友-打开多个子窗口")]
        public async Task Test_SendEmoji_multi_open_subwin()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmojis([WeAutomation.Config.TestFriendNickName, WeAutomation.Config.TestGroupNickName], EmojiListHelper.Items[0].Index, true);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送图片")]
        public async Task Test_File_image()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeAutomation.Config.TestFriendNickName, @"C:\Users\Administrator\Desktop\ssss\logo.png", false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送视频")]
        public async Task Test_File_vedio()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeAutomation.Config.TestFriendNickName, @"C:\Users\Administrator\Desktop\ssss\4.mp4", false);
            Assert.True(true);
            await Task.Delay(60000);
        }


        [Fact(DisplayName = "测试发送文件-发送多个文件")]
        public async Task Test_File_Multi_File()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeAutomation.Config.TestGroupNickName, new string[] { @"C:\Users\Administrator\Desktop\ssss\4.mp4", @"C:\Users\Administrator\Desktop\ssss\logo.png", @"C:\Users\Administrator\Desktop\ssss\3.pdf" }, false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试添加新好友监听-自定义通过")]
        public async Task Test_AddNewFriendCustomPassedListener()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.AddNewFriendCustomPassedListener(list =>
            {
                _output.WriteLine($"添加好友: {list.Count}");
            });
            Assert.True(true);
            await Task.Delay(600000000);
        }

        //实际测试好象长时间放置线程有问题.
        [Fact(DisplayName = "测试添加新好友监听-自动通过")]
        public async Task Test_AddNewFriendAutoPassedListener()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.AddNewFriendAutoPassedListener(list =>
            {
                _output.WriteLine($"添加好友: {list.Count}");
            }, null, "test", "测试");
            Assert.True(true);
            await Task.Delay(600000000);
        }

        [Fact(DisplayName = "测试添加消息监听")]
        public async Task Test_AddMessageListener()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.AddMessageListener(WeAutomation.Config.TestFriendNickName, (newBubbles, bubblesList, sender, mainWindow, framework, serviceProvider) =>
            {
                Trace.WriteLine($"消息: 收到新消息数量:{newBubbles.Count},当前可见消息数量:{bubblesList.Count}");
            });
            Assert.True(true);
            await Task.Delay(6000000);
        }
        [Fact(DisplayName = "测试添加消息监听,并返回新消息")]
        public async Task Test_AddMessageListener_Reback()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.AddMessageListener("歪脖子的模版交流群", (newBubbles, bubblesList, sender, mainWindow, framework, serviceProvider) =>
            {
                Trace.WriteLine($"消息: 收到新消息数量:{newBubbles.Count},当前可见消息数量:{bubblesList.Count}");
                foreach (var bubble in newBubbles)
                {
                    Trace.WriteLine($"消息: 新消息内容:{bubble.MessageContent}");
                    sender.SendMessage($"收到消息:{bubble.MessageContent}");

                }
            });
            Assert.True(true);
            await Task.Delay(6000000);
        }

        [Fact(DisplayName = "测试更新群聊选项")]
        public async Task Test_UpdateChatGroupOptions()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.UpdateChatGroupOptions("AI.Net、秋歌", action =>
            {
                action.ShowGroupNickName = true;
            });
        }

        [Theory(DisplayName = "测试检查好友是否存在")]
        [InlineData(".NET-AI实时快讯3群", false)]
        [InlineData("AI.Net", false)]
        [InlineData("AI.Net", true)]
        [InlineData(".NET-AI实时快讯3群", true)]
        [InlineData("不存在的人", false)]
        [InlineData("不存在的人", true)]
        public void Test_CheckFriendExist(string groupName, bool doubleClick = false)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var flag = window.CheckFriendExist(groupName, doubleClick);
            _output.WriteLine($"检查好友是否存在: {groupName}, 双击: {doubleClick}, 结果: {flag} ");
            if (groupName != "不存在的人")
            {
                Assert.True(flag);
            }
            else
            {
                Assert.False(flag);
            }
        }

        [Fact(DisplayName = "测试创建群聊")]
        public void Test_CreateOwnerChatGroup()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.CreateOwnerChatGroup("测试07", new string[] { "AI.Net", "秋歌" });
            _output.WriteLine($"创建群聊结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "测试更新群聊备注")]
        public void Test_UpdateGroupMemo()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.ChageOwerChatGroupMemo("测试07", "测试07新的备注");
            _output.WriteLine($"更新群聊备注结果: {result.Message}");
            Assert.True(result.Success);
        }
        
        [Fact(DisplayName = "测试更新群聊名称")]
        public void Test_UpdateGroupName()  
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.ChangeOwerChatGroupName("测试07新名称", "测试07");
            _output.WriteLine($"更新群聊名称结果: {result.Message}");
            Assert.True(result.Success);
        }
    }
}
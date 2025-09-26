using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCore.Utils;
using Xunit.Abstractions;
using WxAutoCommon.Configs;
using WeAutoCommon.Classes;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WxWindowTests
    {
        private readonly string _wxClientName = WeChatConfig.TestClientName;
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
            DrawHightlightHelper.DrawHightlight(window.Window, framework.UiThreadInvoker);
            window.WindowRestore();
            DrawHightlightHelper.DrawHightlight(window.Window, framework.UiThreadInvoker);
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
            await window.SendWho(WeChatConfig.TestFriendNickName, "你好，世界111！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-当前聊天窗口-确保打开是测试人的聊天窗口")]
        public async Task Test_SendWho_CurrentChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeChatConfig.TestFriendNickName, "你好，世界222！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeChatConfig.TestFriendNickName, "你好，世界333！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中,并打开聊天窗口")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList_OpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat(WeChatConfig.TestFriendNickName, "你好，世界333222！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,不在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_NOT_InConversationList()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho(WeChatConfig.TestFriendNickName, "你好，世界444！");
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
            await window.SendWhoAndOpenChat(WeChatConfig.TestFriendNickName, "你好，世界666！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量")]
        public async Task Test_SendWhos()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendWhos([WeChatConfig.TestFriendNickName, WeChatConfig.TestGroupNickName], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量,并打开聊天窗口")]
        public async Task Test_SendWhosAndOpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendWhosAndOpenChat([WeChatConfig.TestFriendNickName, WeChatConfig.TestGroupNickName], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送索引给指定好友")]
        public async Task Test_SendEmoji_Index()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeChatConfig.TestFriendNickName, EmojiListHelper.Items[0].Index, false);
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送名称给指定好友")]
        public async Task Test_SendEmoji_Name()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeChatConfig.TestFriendNickName, "微笑", false);
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送表情-发送值给指定好友")]
        public async Task Test_SendEmoji_value()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeChatConfig.TestFriendNickName, "[微笑]", false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送表情-发送索引给指定好友-打开子窗口")]
        public async Task Test_SendEmoji_open_subwin()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmoji(WeChatConfig.TestFriendNickName, EmojiListHelper.Items[0].Index, true);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送表情-发送索引给指定好友-发送给多个好友-打开多个子窗口")]
        public async Task Test_SendEmoji_multi_open_subwin()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendEmojis([WeChatConfig.TestFriendNickName, WeChatConfig.TestGroupNickName], EmojiListHelper.Items[0].Index, true);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送图片")]
        public async Task Test_File_image()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeChatConfig.TestFriendNickName, @"C:\Users\Administrator\Desktop\ssss\logo.png", false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送视频")]
        public async Task Test_File_vedio()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeChatConfig.TestFriendNickName, @"C:\Users\Administrator\Desktop\ssss\4.mp4", false);
            Assert.True(true);
            await Task.Delay(60000);
        }


        [Fact(DisplayName = "测试发送文件-发送多个文件")]
        public async Task Test_File_Multi_File()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendFile(WeChatConfig.TestFriendNickName, new string[] { @"C:\Users\Administrator\Desktop\ssss\4.mp4", @"C:\Users\Administrator\Desktop\ssss\logo.png", @"C:\Users\Administrator\Desktop\ssss\3.pdf" }, false);
            Assert.True(true);
            await Task.Delay(60000);
        }
    }
}
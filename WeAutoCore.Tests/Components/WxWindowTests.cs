using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCore.Utils;
using Xunit.Abstractions;
using WxAutoCommon.Configs;

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
            DrawHightlightHelper.DrawHightlight(window.Window,framework.UiThreadInvoker);
            window.WindowRestore();
            DrawHightlightHelper.DrawHightlight(window.Window,framework.UiThreadInvoker);
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

        [Fact(DisplayName = "测试发送消息")]
        public void Test_SendMessage()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendMessage("你好，世界！");
            Assert.True(true);
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

        [Fact(DisplayName = "测试发送消息-当前聊天窗口")]
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
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-不存在的人")]
        public async Task Test_SendWho_Not_Exist_Person()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("不存在的人", "你好，世界555！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息")]
        public async Task Test_SendWhoAndOpenChat()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat(WeChatConfig.TestFriendNickName, "你好，世界666！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-批量")]
        public async Task Test_SendWhos()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.SendWhos([WeChatConfig.TestFriendNickName, WeChatConfig.TestGroupNickName], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(60000);
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
    }
}
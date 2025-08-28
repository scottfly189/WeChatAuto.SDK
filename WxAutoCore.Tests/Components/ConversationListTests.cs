using FlaUI.Core.AutomationElements;
using WxAutoCore.Components;
using Xunit;
using Xunit.Abstractions;
using WxAutoCommon.Models;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class ConversationListTests
    {
        private readonly string _wxClientName = WeChatConfig.TestClientName;
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public ConversationListTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试获取会话列表")]
        public void TestGetVisibleConversations()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var conversations = window.Conversations;
            var list = conversations.GetVisibleConversations();
            foreach (var conversation in list)
            {
                _output.WriteLine(conversation.ToString());
            }
            Assert.True(list.Count > 0);
        }

        [Fact(DisplayName = "测试点击会话")]
        public void TestClickConversation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var conversations = window.Conversations;
            conversations.ClickConversation(WeChatConfig.TestGroupNickName);
        }

        [Fact(DisplayName = "测试双击会话")]
        public void TestDoubleClickConversation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var conversations = window.Conversations;
            conversations.DoubleClickConversation(WeChatConfig.TestGroupNickName);
        }

        [Fact(DisplayName = "测试获取会话列表所有会话标题")]
        public void TestGetConversationTitles()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var conversations = window.Conversations;
            var titles = conversations.GetConversationTitles();
            foreach (var title in titles)
            {
                _output.WriteLine(title);
            }
            Assert.True(titles.Count > 0);
        }
    }
}
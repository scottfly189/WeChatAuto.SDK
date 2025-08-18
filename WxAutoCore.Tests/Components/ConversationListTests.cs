using FlaUI.Core.AutomationElements;
using WxAutoCore.Components;
using Xunit;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class ConversationListTests
    {
        private readonly string _wxClientName = "Alex Zhao";
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
    }
}
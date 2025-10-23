using FlaUI.Core.AutomationElements;
using WxAutoCore.Components;
using Xunit;
using Xunit.Abstractions;
using WxAutoCommon.Models;
using WxAutoCommon.Configs;
using WxAutoCore.Services;

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
            var window = client.WxMainWindow;
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
            var window = client.WxMainWindow;
            var conversations = window.Conversations;
            conversations.ClickConversation(".NET-AI实时快讯3群");
        }

        [Fact(DisplayName = "测试双击会话")]
        public void TestDoubleClickConversation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var conversations = window.Conversations;
            conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        }

        [Fact(DisplayName = "获取会话列表可见会话标题")]
        public void TestGetConversationTitles()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var conversations = window.Conversations;
            var titles = conversations.GetVisibleConversationTitles();
            foreach (var title in titles)
            {
                _output.WriteLine(title);
            }
            Assert.True(titles.Count > 0);
        }

        [Fact(DisplayName = "获取会话列表所有会话")]
        public void TestGetAllConversations()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var conversations = window.Conversations;
            var list = conversations.GetAllConversations();
            foreach (var conversation in list)
            {
                _output.WriteLine(conversation.ToString());
            }
            Assert.True(list.Count > 0);
        }

        [Fact(DisplayName = "测试定位会话")]
        public void TestLocateConversation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var conversations = window.Conversations;
            var result = conversations.LocateConversation(".NET-AI实时快讯3群");
            _output.WriteLine($"定位会话结果: {result}");
            result = conversations.LocateConversation("哈哈");
            _output.WriteLine($"定位会话结果: {result}");
            Assert.True(result);    
        }
    }
}
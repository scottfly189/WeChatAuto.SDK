using WxAutoCommon.Models;
using Xunit.Abstractions;
using WxAutoCommon.Configs;
using WeChatAuto.Services;

namespace WeChatAuto.Tests.Components
{
    [Collection("UiTestCollection")]
    public class SearchTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public SearchTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试搜索聊天")]
        public void TestSearchChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var search = window.Search;
            search.SearchChat("AI.Net");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索联系人")]
        public void TestSearchContact()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var search = window.Search;
            search.SearchContact("AI.Net");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索收藏")]
        public void TestSearchCollection()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var search = window.Search;
            search.SearchCollection("AI.Net");
            Assert.True(true);
        }
    }
}
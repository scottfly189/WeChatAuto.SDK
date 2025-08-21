using WxAutoCommon.Models;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class SearchTests
    {
        private readonly string _wxClientName = WxConfig.TestClientName;
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
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchChat(WxConfig.TestFriendNickName);
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索联系人")]
        public void TestSearchContact()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchContact(WxConfig.TestFriendNickName);
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索收藏")]
        public void TestSearchCollection()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchCollection(WxConfig.TestFriendNickName);
            Assert.True(true);
        }
    }
}
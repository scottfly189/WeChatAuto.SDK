using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components
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
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchChat("秋歌");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索联系人")]
        public void TestSearchContact()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchContact("老妈");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试搜索收藏")]
        public void TestSearchCollection()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var search = window.Search;
            search.SearchCollection("秋歌");
            Assert.True(true);
        }
    }
}
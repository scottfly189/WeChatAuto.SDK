using WxAutoCore.Utils;
using Xunit.Abstractions;


namespace WxAutoCore.Tests.Components
{
    [Collection("GlobalCollection")]
    public class NavigationTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private readonly ITestOutputHelper _output;
        private GlobalFixture _globalFixture;
        public NavigationTests(ITestOutputHelper output, GlobalFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试导航栏操作")]
        public async Task TestNavigationAction()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var navigation = window.Navigation;
        }
    }
}
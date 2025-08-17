using WxAutoCommon.Enums;
using WxAutoCore.Services;
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

        [Fact(DisplayName = "测试切换导航栏")]
        public async Task TestSwitchNavigation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            var navigation = window.Navigation;
            navigation.SwitchNavigation(NavigationType.通讯录);
            navigation.SwitchNavigation(NavigationType.聊天);
            navigation.SwitchNavigation(NavigationType.收藏);

            Assert.True(true);
        }
    }
}
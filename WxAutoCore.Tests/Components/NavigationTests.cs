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
            await WxAutomation.Wait(2);
            navigation.SwitchNavigation(NavigationType.聊天);
            await WxAutomation.Wait(2);
            navigation.SwitchNavigation(NavigationType.收藏);
            await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.聊天文件);
            // await WxAutomation.Wait(2);
            navigation.SwitchNavigation(NavigationType.朋友圈);
            await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.搜一搜);
            // await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.视频号);
            // await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.看一看);
            // await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.小程序面板);
            // await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.手机);
            // await WxAutomation.Wait(2);
            // navigation.SwitchNavigation(NavigationType.设置及其他);
            // await WxAutomation.Wait(2);
            Assert.True(true);
        }
    }
}
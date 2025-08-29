using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCore.Utils;
using Xunit.Abstractions;
using WxAutoCommon.Configs;


namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class NavigationTests
    {
        private readonly string _wxClientName = WeChatConfig.TestClientName;
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public NavigationTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试切换导航栏")]
        public async Task TestSwitchNavigation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var navigation = window.Navigation;
            navigation.SwitchNavigation(NavigationType.通讯录);
            navigation.SwitchNavigation(NavigationType.聊天);
            navigation.SwitchNavigation(NavigationType.收藏);
            var caches = navigation._wxLocationCaches;
            var element = caches.GetElement(NavigationType.聊天文件.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.朋友圈.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.视频号.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.看一看.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.搜一搜.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.小程序面板.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.手机.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            element = caches.GetElement(NavigationType.设置及其他.ToString());
            DrawHightlightHelper.DrawHightlight(element,framework.UiThreadInvoker);
            await Task.CompletedTask;
            Assert.True(true);
        }

        [Fact(DisplayName = "测试当前导航栏")]
        public async Task TestCurrentNavigationElement()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var navigation = window.Navigation;
            navigation.SwitchNavigation(NavigationType.通讯录);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.通讯录);
            Assert.Equal(NavigationType.通讯录.ToString(), navigation.CurrentNavigationElement.Name);
        }

        // [Fact(DisplayName = "测试关闭导航栏")]
        [Fact(Skip = "测试关闭导航栏")]
        public async Task TestCloseNavigation()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var navigation = window.Navigation;
            navigation.SwitchNavigation(NavigationType.聊天文件);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.聊天文件);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.朋友圈);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.朋友圈);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.视频号);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.视频号);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.看一看);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.看一看);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.搜一搜);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.搜一搜);
            await WeAutomation.Wait(4);
            navigation.SwitchNavigation(NavigationType.小程序面板);
            await WeAutomation.Wait(4);
            navigation.CloseNavigation(NavigationType.小程序面板);
            await WeAutomation.Wait(4);

            Assert.True(true);
        }
    }
}
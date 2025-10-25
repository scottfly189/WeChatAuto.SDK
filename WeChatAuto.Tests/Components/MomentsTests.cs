using FlaUI.Core.AutomationElements;
using WxAutoCommon.Enums;
using WxAutoCommon.Models;
using WeChatAuto.Services;
using WeChatAuto.Utils;
using Xunit.Abstractions;
using WxAutoCommon.Configs;


namespace WeChatAuto.Tests.Components
{
    [Collection("UiTestCollection")]
    public class MomentsTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public MomentsTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试打开朋友圈")]
        public void TestOpenMoments()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var moments = window.Moments;
            moments.OpenMoments();
            Assert.True(moments.IsMomentsOpen());
        }
        [Fact(DisplayName = "测试获取朋友圈内容列表")]  
        public void TestGetMomentsList()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var moments = window.Moments;
            var momentsList = moments.GetMomentsList();
            Assert.True(true);
        }

    }
}
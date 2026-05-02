using WeAutoCommon.Models;
using WeChatAuto.Components;
using Xunit.Abstractions;
using WeAutoCommon.Configs;
using WeChatAuto.Services;

namespace WeChatAuto.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WeChatClientFactoryTests
    {
        private readonly string _wxClientName = "Alex";
        private ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public WeChatClientFactoryTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试获取微信客户端名称")]
        public void TestGetWxClientName()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            _output.WriteLine($"微信客户端名称: {client.WxMainWindow.NickName}");
            Assert.True(client.WxMainWindow.NickName == _wxClientName);
        }
    }
}
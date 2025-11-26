using WxAutoCommon.Models;
using WeChatAuto.Components;
using Xunit.Abstractions;
using WxAutoCommon.Configs;
using WeChatAuto.Services;

namespace WeChatAuto.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WeChatClientFactoryTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public WeChatClientFactoryTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试刷新微信客户端")]
        public void TestRefreshWxWindows()
        {
            var framework = _globalFixture.clientFactory;
            framework._FetchAllWxWindows();
            _output.WriteLine($"微信客户端数量: {framework.WxClientList.Count}");
            foreach (var client in framework.WxClientList)
            {
                _output.WriteLine($"微信客户端: {client.Key}");
            }
            Assert.True(framework.WxClientList.Count > 0);
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
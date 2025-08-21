using WxAutoCommon.Models;
using WxAutoCore.Components;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WxFramworkTests
    {
        private readonly string _wxClientName = WxConfig.TestClientName;
        private ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public WxFramworkTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试刷新微信客户端")]
        public void TestRefreshWxWindows()
        {
            var framework = _globalFixture.wxFramwork;
            framework.RefreshWxWindows();
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
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            _output.WriteLine($"微信客户端名称: {client.WxWindow.NickName}");
            Assert.True(client.WxWindow.NickName == _wxClientName);
        }
    }
}
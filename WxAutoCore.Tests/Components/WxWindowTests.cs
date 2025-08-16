using WxAutoCore.Services;
using WxAutoCore.Utils;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components
{
    [Collection("GlobalCollection")]
    public class WxWindowTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private readonly ITestOutputHelper _output;
        private GlobalFixture _globalFixture;
        public WxWindowTests(ITestOutputHelper output, GlobalFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试窗口操作")]
        public async Task TestWindowAction()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxWindow;
            window.WindowMax();
            DrawHightlightHelper.DrawHightlight(window.Window);
            window.WindowRestore();
            DrawHightlightHelper.DrawHightlight(window.Window);
            window.WindowMin();
            await WxAutomation.Wait(2);
            window.WinMinRestore();
            window.WindowTop(true);
            await WxAutomation.Wait(2);
            window.WindowTop(false);
            await WxAutomation.Wait(2);
            Assert.True(true);
        }
    }
}
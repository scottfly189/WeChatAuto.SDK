using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;

[Collection("UiTestCollection")]
public class WeChatClientTests
{
    private readonly string _wxClientName = "Alex Zhao";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public WeChatClientTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    [Fact(DisplayName = "测试微信客户端运行检查监听")]
    public async Task TestCheckAppRunning()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        Assert.True(client.AppRunning);
        await Task.Delay(-1);  //阻塞测试，直到微信客户端退出
    }
}
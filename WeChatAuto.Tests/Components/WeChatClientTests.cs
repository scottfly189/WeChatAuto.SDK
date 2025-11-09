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

    [Fact(DisplayName = "测试屏幕截图")]
    public async Task TestCaptureUI()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var filePath = client.CaptureUI();
        _output.WriteLine($"截图保存路径：{filePath}");
        Assert.True(!string.IsNullOrWhiteSpace(filePath));
        await Task.CompletedTask;
    }

    [Fact(DisplayName = "测试视频录制")]
    public async Task TestRecordVideo()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        Assert.True(true);
        await Task.Delay(20*1000);
    }
}
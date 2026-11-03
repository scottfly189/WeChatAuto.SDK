using System.Diagnostics;
using OneOf;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;


[Collection("UiTestCollection")]
public class ToolBarTests
{
    private readonly string _wxClientName = "Alex";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public ToolBarTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试窗口操作")]
    public async Task Test_Max_Restore_Pined_UnPined()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        await client.Max();
        await client.Restore();
        await client.Pinned();
        await client.UnPinned();
    }

    [Fact(DisplayName = "隐藏窗口/打开窗口")]
    public async Task TestHideWindow()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        await client.CloseWindowAsync();
        await Task.Delay(5000);
        await client.OpenWindowAsync();
        await Task.Delay(5000);
    }

    [Fact(DisplayName = "打开窗口")]
    public async Task TestOpenWindow()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        await client.OpenWindowAsync();
    }
}
using Xunit;
using Xunit.Abstractions;
using WxAutoCore.Components;
using WxAutoCommon.Models;

namespace WxAutoCore.Tests.Components;

[Collection("UiTestCollection")]
public class SenderTests
{
    private readonly string _wxClientName = WxConfig.TestClientName;
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public SenderTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    [Fact(DisplayName = "测试发送文本消息")]
    public void Test_Inline_SendTextMessage()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var window = client.WxWindow;
        var sender = window.ChatContent.ChatBody.Sender;
        sender.SendMessage("你好，世界！");
        Assert.True(true);
    }
}
using Xunit;
using Xunit.Abstractions;
using WxAutoCore.Components;
using WxAutoCommon.Models;

namespace WxAutoCore.Tests.Components;

[Collection("UiTestCollection")]
public class ChatHeaderTests
{
    private readonly string _wxClientName = WxConfig.TestClientName;
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public ChatHeaderTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    [Fact(DisplayName = "测试获取聊天标题")]
    public void Test_Inline_ChatHeaderTitle()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var window = client.WxWindow;
        var chatHeader = window.ChatContent.ChatHeader;
        _output.WriteLine(chatHeader.Title);
        Assert.True(chatHeader.Title != null);
    }
}
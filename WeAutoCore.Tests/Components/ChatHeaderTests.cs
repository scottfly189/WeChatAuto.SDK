using Xunit;
using Xunit.Abstractions;
using WxAutoCore.Components;
using WxAutoCommon.Models;
using WxAutoCommon.Configs;
using WxAutoCore.Services;

namespace WxAutoCore.Tests.Components;

[Collection("UiTestCollection")]
public class ChatHeaderTests
{
    private readonly string _wxClientName = "AlexZhao";
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
        var window = client.WxMainWindow;
        var chatHeader = window.ChatContent.ChatHeader;
        _output.WriteLine(chatHeader.Title);
        Assert.True(chatHeader.Title != null);
    }

    [Fact(DisplayName = "测试获取子窗口标题")]
    public void Test_SubWin_ChatHeaderTitle()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        var subWin = mainWindow.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        var chatHeader = subWin.ChatContent.ChatHeader;
        _output.WriteLine(chatHeader.Title);
        Assert.Equal(".NET-AI实时快讯3群", chatHeader.Title);
        mainWindow.SubWinList.CloseAllSubWins();
    }
}
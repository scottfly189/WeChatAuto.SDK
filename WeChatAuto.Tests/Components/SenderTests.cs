using Xunit;
using Xunit.Abstractions;
using WeChatAuto.Components;
using WeAutoCommon.Models;
using WeChatAuto.Services;
using WeAutoCommon.Configs;

namespace WeChatAuto.Tests.Components;

[Collection("UiTestCollection")]
public class SenderTests
{
    private readonly string _wxClientName = "Alex";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public SenderTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }



    [Fact(DisplayName = "测试弹出窗口发送文本消息-被@的用户")]
    public async Task Test_SubWin_SendTextMessage_atUser()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        var subWin = mainWindow.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendMessage("你好，世界！", "秋收");
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送表情")]
    public async Task Test_SubWin_SendEmoji()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        var subWin = mainWindow.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendEmoji(11);
        sender.SendEmoji("微笑");
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送文件-单文件")]
    public async Task Test_SubWin_SendFile_Single()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        var subWin = mainWindow.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendFile(new string[] { @"C:\Users\Administrator\Desktop\ssss\logo.png" });
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送文件-多文件")]
    public async Task Test_SubWin_SendFile_Multi()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(".NET-AI实时快讯3群");
        var subWin = mainWindow.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendFile(new string[] { @"C:\Users\Administrator\Desktop\ssss\logo.png", @"C:\Users\Administrator\Desktop\ssss\4.mp4", @"C:\Users\Administrator\Desktop\ssss\3.pdf" });
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }
}
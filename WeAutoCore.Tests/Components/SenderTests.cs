using Xunit;
using Xunit.Abstractions;
using WxAutoCore.Components;
using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCommon.Configs;

namespace WxAutoCore.Tests.Components;

[Collection("UiTestCollection")]
public class SenderTests
{
    private readonly string _wxClientName = WeAutomation.Config.TestClientName;
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public SenderTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    //注意：测试时需要打开一个特定的好友
    [Fact(DisplayName = "测试发送文本消息")]
    public void Test_Inline_SendTextMessage()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var window = client.WxMainWindow;
        var sender = window.ChatContent.ChatBody.Sender;
        sender.SendMessage("你好，世界！");
        Assert.True(true);
    }

    //注意：测试时需要待发送的好友在会话列表中
    [Fact(DisplayName = "测试弹出窗口发送文本消息")]
    public async Task Test_SubWin_SendTextMessage()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(WeAutomation.Config.TestGroupNickName);
        var subWin = mainWindow.SubWinList.GetSubWin(WeAutomation.Config.TestGroupNickName);
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendMessage("你好，世界！");
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送文本消息-被@的用户")]
    public async Task Test_SubWin_SendTextMessage_atUser()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(WeAutomation.Config.TestGroupNickName);
        var subWin = mainWindow.SubWinList.GetSubWin(WeAutomation.Config.TestGroupNickName);
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendMessage("你好，世界！", "秋歌");
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送表情")]
    public async Task Test_SubWin_SendEmoji()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(WeAutomation.Config.TestGroupNickName);
        var subWin = mainWindow.SubWinList.GetSubWin(WeAutomation.Config.TestGroupNickName);
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
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(WeAutomation.Config.TestGroupNickName);
        var subWin = mainWindow.SubWinList.GetSubWin(WeAutomation.Config.TestGroupNickName);
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendFile(new string[] { @"C:\Users\Administrator\Desktop\ssss\logo.png" });
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }

    [Fact(DisplayName = "测试弹出窗口发送文件-多文件")]
    public async Task Test_SubWin_SendFile_Multi()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var mainWindow = client.WxMainWindow;
        mainWindow.Conversations.DoubleClickConversation(WeAutomation.Config.TestGroupNickName);
        var subWin = mainWindow.SubWinList.GetSubWin(WeAutomation.Config.TestGroupNickName);
        var sender = subWin.ChatContent.ChatBody.Sender;
        sender.SendFile(new string[] { @"C:\Users\Administrator\Desktop\ssss\logo.png", @"C:\Users\Administrator\Desktop\ssss\4.mp4", @"C:\Users\Administrator\Desktop\ssss\3.pdf" });
        Assert.True(true);
        await Task.Delay(5000);
        mainWindow.SubWinList.CloseAllSubWins();
    }
}
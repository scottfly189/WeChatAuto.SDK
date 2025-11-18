using WeChatAuto.Services;
using WxAutoCommon.Models;
using Xunit.Abstractions;
using WxAutoCommon.Enums;
using WeChatAuto.Utils;
using WxAutoCommon.Configs;

namespace WeChatAuto.Tests.Components;

[Collection("UiTestCollection")]
public class MessageBubbleListTests
{
    private readonly string _wxClientName = "Alex Zhao";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public MessageBubbleListTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试获取可见气泡列表")]
    public void Test_Get_Main_Bubble_List()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var bubbleList = window.MainChatContent.ChatBody.BubbleListObject;
        var bubbles = bubbleList.Bubbles;
        foreach (var bubble in bubbles)
        {
            _output.WriteLine(bubble.ToString());
            if (bubble.ClickActionButton != null)
            {
                _output.WriteLine($"有点击按钮，可点击！");
            }
        }
        Assert.True(bubbles.Count >= 0);
    }

    [Fact(DisplayName = "测试获取子窗口好友气泡列表")]
    public void Test_Get_Sub_Bubble_Friend_List()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var subWin = window.SubWinList.GetSubWin("AI.Net");
        if (subWin == null)
        {
            _output.WriteLine("子窗口不存在");
            Assert.True(false);
            return;
        }
        var subBubbleList = subWin.ChatContent.ChatBody.BubbleListObject;
        var subBubbles = subBubbleList.Bubbles;
        foreach (var bubble in subBubbles)
        {
            _output.WriteLine(bubble.ToString());
            if (bubble.ClickActionButton != null)
            {
                _output.WriteLine($"有点击按钮，可点击！");
            }
        }
        Assert.True(subBubbles.Count >= 0);
    }

    [Fact(DisplayName = "测试获取子窗口群聊气泡列表")]
    public void Test_Get_Sub_Bubble_Group_List()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var subWin = window.SubWinList.GetSubWin(".NET-AI实时快讯3群");
        if (subWin == null)
        {
            _output.WriteLine("子窗口不存在");
            Assert.True(false);
            return;
        }
        var subBubbleList = subWin.ChatContent.ChatBody.BubbleListObject;
        var subBubbles = subBubbleList.Bubbles;
        foreach (var bubble in subBubbles)
        {
            _output.WriteLine(bubble.ToString());
            if (bubble.ClickActionButton != null)
            {
                _output.WriteLine($"有点击按钮，可点击！");
            }
        }
        Assert.True(subBubbles.Count >= 0);
    }

    [Fact(DisplayName = "测试获取聊天类型")]
    public async Task Test_Get_Chat_Type()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        await window.SendWho("AI.Net", "hello world!");
        _output.WriteLine(window.MainChatContent.ChatBody.BubbleListObject.GetChatType().ToString());
        Assert.Equal(ChatType.好友, window.MainChatContent.ChatBody.BubbleListObject.GetChatType());
        await window.SendWho(".NET-AI实时快讯3群", "hello world!");
        _output.WriteLine(window.MainChatContent.ChatBody.BubbleListObject.GetChatType().ToString());
        Assert.Equal(ChatType.群聊, window.MainChatContent.ChatBody.BubbleListObject.GetChatType());
    }
}
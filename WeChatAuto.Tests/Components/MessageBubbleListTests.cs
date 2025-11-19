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

    [Fact(DisplayName = "测试获取主窗口可见气泡标题列表")]
    public void Test_Get_Main_Bubble_List_Simple()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var bubbleList = window.MainChatContent.ChatBody.BubbleListObject;
        var chatSimpleMessages = bubbleList.ChatSimpleMessages;
        _output.WriteLine($"获取到的气泡标题列表数量：{chatSimpleMessages.Count}");
        foreach (var chatSimpleMessage in chatSimpleMessages)
        {
            _output.WriteLine(chatSimpleMessage.ToString());
        }
        Assert.True(chatSimpleMessages.Count >= 0);
    }

    [Fact(DisplayName = "测试获取可见气泡列表")]
    public void Test_Get_Main_Bubble_List()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var bubbleList = window.MainChatContent.ChatBody.BubbleListObject;
        var bubbles = bubbleList.Bubbles;
        _output.WriteLine($"获取到的气泡列表数量：{bubbles.Count}");
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

    [Theory(DisplayName = "测试转发单条消息-主窗口")]
    [InlineData("AI.Net", "@Alex Zhao 发些有意思的", "测试11")]
    [InlineData("秋歌", "她跳绳可以的", "测试11")]
    [InlineData("秋歌", "[视频]", "测试11")]
    [InlineData("gggccc", "但是我现在有工作", "测试11")]
    [InlineData("歪燕子", "不会英文啊", "测试11")]
    [InlineData(".NET-AI实时快讯3群", "hello world!", "测试11")]
    public async Task Test_Forward_Single_Message_main_window(string who, string message, string to)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var bubbleList = window.MainChatContent.ChatBody.BubbleListObject;
        bubbleList.ForwardSingleMessage(who: who, message: message, to: to);
        Assert.True(true);
        await Task.CompletedTask;
    }

    [Theory(DisplayName = "测试转发单条消息-子窗口")]
    [InlineData("测试11", "AI.Net", "@Alex Zhao 发些有意思的", "测试11")]
    [InlineData("测试11", "秋歌", "她跳绳可以的", "测试11")]
    [InlineData("测试11", "秋歌", "[视频]", "测试11")]
    [InlineData("歪脖子的模版交流群", "gggccc", "但是我现在有工作", "测试11")]
    [InlineData("歪脖子的模版交流群", "gggccc", "但是我现在有工作2", "测试11")]
    [InlineData(".NET-AI实时快讯3群", ".NET-AI实时快讯3群", "hello world!", "测试11")]
    public async Task Test_Forward_Single_Message_sub_window(string subWinName, string who, string message, string to)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var window = client.WxMainWindow;
        var subWin = window.SubWinList.GetSubWin(subWinName);
        if (subWin == null)
        {
            _output.WriteLine("子窗口不存在");
            Assert.True(false);
            return;
        }
        var subBubbleList = subWin.ChatContent.ChatBody.BubbleListObject;
        subBubbleList.ForwardSingleMessage(who: who, message: message, to: to);
        Assert.True(true);
        await Task.CompletedTask;
    }

}
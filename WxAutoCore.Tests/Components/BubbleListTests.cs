using WxAutoCore.Services;
using WxAutoCommon.Models;
using Xunit.Abstractions;
using WxAutoCommon.Enums;
using WxAutoCore.Utils;

namespace WxAutoCore.Tests.Components;

[Collection("UiTestCollection")]
public class BubbleListTests
{
    private readonly string _wxClientName = WxConfig.TestClientName;
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public BubbleListTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试获取气泡列表")]
    public void Test_Get_Bubble_List()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var window = client.WxWindow;
        var bubbleList = window.ChatContent.ChatBody.BubbleList;
        var bubbles = bubbleList.Bubbles;
        foreach (var bubble in bubbles)
        {
            _output.WriteLine(bubble.ToString());
            if (bubble.ClickActionButton != null)
            {
                _output.WriteLine($"有点击按钮，可点击！");
            }
        }
        Assert.True(bubbles.Count > 0);
    }

    [Fact(DisplayName = "测试获取聊天类型")]
    public async Task Test_Get_Chat_Type()
    {
        var framework = _globalFixture.wxFramwork;
        var client = framework.GetWxClient(_wxClientName);
        var window = client.WxWindow;
        await window.SendWho(WxConfig.TestFriendNickName, "hello world!");
        Assert.Equal(ChatType.好友, window.ChatContent.ChatBody.BubbleList.GetChatType());
        await window.SendWho(WxConfig.TestGroupNickName, "hello world!");
        Assert.Equal(ChatType.群聊, window.ChatContent.ChatBody.BubbleList.GetChatType());
    }
}
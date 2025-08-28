using WxAutoCommon.Models;
using WxAutoCore.Services;
using Xunit;
using WxAutoCommon.Configs;

namespace WxAutoCore.Tests.Services;

[Collection("WeChatPCCoreTests")]
public class WeChatPCCoreTests
{

    [Fact(DisplayName = "发送一个好友，多个好友消息,以内联的方式")]
    public void SendMessage_Should_Send_Message_To_User_Inline()
    {
        var core = new WeChatPCCore(WeChatConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WeChatConfig.TestFriendNickName);
        response = core.SendMessage("Hello, World!", WeChatConfig.TestAlternativeFriendNickName);
        response = core.SendMessage("Hello, World!", new string[] { WeChatConfig.TestFriendNickName, WeChatConfig.TestAlternativeFriendNickName });
        Assert.True(response.IsSuccess);
    }

    [Fact(DisplayName = "发送一个好友，多个好友消息，并打开独立窗口")]
    public void SendMessage_Should_Send_Message_To_User_Open()
    {
        var core = new WeChatPCCore(WeChatConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WeChatConfig.TestFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", WeChatConfig.TestAlternativeFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", new string[] { WeChatConfig.TestFriendNickName, WeChatConfig.TestAlternativeFriendNickName }, default, true);
        Assert.True(response.IsSuccess);
        core.CloseAllSubWindow();
    }
}
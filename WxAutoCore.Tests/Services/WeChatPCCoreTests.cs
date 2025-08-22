using WxAutoCommon.Models;
using WxAutoCore.Services;
using Xunit;

namespace WxAutoCore.Tests.Services;

[Collection("WeChatPCCoreTests")]
public class WeChatPCCoreTests
{

    [Fact(DisplayName = "发送一个好友，多个好友消息,以内联的方式")]
    public void SendMessage_Should_Send_Message_To_User_Inline()
    {
        var core = new WeChatPCCore(WxConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WxConfig.TestFriendNickName);
        response = core.SendMessage("Hello, World!", WxConfig.TestAlternativeFriendNickName);
        response = core.SendMessage("Hello, World!", new string[] { WxConfig.TestFriendNickName, WxConfig.TestAlternativeFriendNickName });
        Assert.True(response.IsSuccess);
    }

    [Fact(DisplayName = "发送一个好友，多个好友消息，并打开独立窗口")]
    public void SendMessage_Should_Send_Message_To_User_Open()
    {
        var core = new WeChatPCCore(WxConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WxConfig.TestFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", WxConfig.TestAlternativeFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", new string[] { WxConfig.TestFriendNickName, WxConfig.TestAlternativeFriendNickName }, default, true);
        Assert.True(response.IsSuccess);
        core.CloseAllSubWindow();
    }
}
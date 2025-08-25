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
        var core = new WeChatPCCore(WxAutoConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WxAutoConfig.TestFriendNickName);
        response = core.SendMessage("Hello, World!", WxAutoConfig.TestAlternativeFriendNickName);
        response = core.SendMessage("Hello, World!", new string[] { WxAutoConfig.TestFriendNickName, WxAutoConfig.TestAlternativeFriendNickName });
        Assert.True(response.IsSuccess);
    }

    [Fact(DisplayName = "发送一个好友，多个好友消息，并打开独立窗口")]
    public void SendMessage_Should_Send_Message_To_User_Open()
    {
        var core = new WeChatPCCore(WxAutoConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WxAutoConfig.TestFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", WxAutoConfig.TestAlternativeFriendNickName, default, true);
        response = core.SendMessage("Hello, World!", new string[] { WxAutoConfig.TestFriendNickName, WxAutoConfig.TestAlternativeFriendNickName }, default, true);
        Assert.True(response.IsSuccess);
        core.CloseAllSubWindow();
    }
}
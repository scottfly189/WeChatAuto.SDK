using WxAutoCommon.Models;
using WxAutoCore.Services;
using Xunit;

namespace WxAutoCore.Tests.Services;

[Collection("WeChatPCCoreTests")]
public class WeChatPCCoreTests
{

    [Fact]
    public void SendMessage_Should_Send_Message_To_User()
    {
        var core = new WeChatPCCore(WxConfig.TestClientName);
        var response = core.SendMessage("Hello, World!", WxConfig.TestFriendNickName);
        Assert.True(response.IsSuccess);
    }
}
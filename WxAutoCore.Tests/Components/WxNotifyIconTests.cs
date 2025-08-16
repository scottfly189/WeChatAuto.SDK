using WxAutoCore.Components;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Components;

/// <summary>
/// 微信通知图标测试
/// </summary>
[Collection("GlobalCollection")]
public class WxNotifyIconTests
{
    private readonly string _sutClientName = "Alex Zhao";
    private readonly GlobalFixture _fixture;
    private readonly ITestOutputHelper _output;
    public WxNotifyIconTests(GlobalFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;;
    }

    [Fact(DisplayName = "测试点击通知图标是否能显示微信主窗口")]
    public void Test_WxNotifyIcon_Click_Cound_Show_Wechat_Main_Window()
    {
        var wxClient = _fixture.wxFramwork.GetWxClient(_sutClientName);
        var wxNotifyIcon = wxClient.WxNotifyIcon;
        wxNotifyIcon.Click();
        Assert.True(wxNotifyIcon.NotifyIcon.Properties.Name == "微信");
    }
}
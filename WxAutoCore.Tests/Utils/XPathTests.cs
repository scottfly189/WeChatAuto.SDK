using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;
using WxAutoCore.Services;
using Xunit;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Utils;

[Collection("UiTestCollection")]
public class XPathTests
{
    private readonly string _wxClientName = "Alex Zhao";
    private ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public XPathTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试XPath")]
    public async Task Test_Navigation_XPath()
    {
        var framework = _globalFixture.wxFramwork;
        var sutClient = framework.GetWxClient(_wxClientName);
        var sutWindow = sutClient.WxWindow;
        sutWindow.Navigation.SwitchNavigation(NavigationType.聊天文件);
        var window = Retry.WhileNull(checkMethod: () => sutWindow.Window.Automation.GetDesktop()
            .FindFirstByXPath($"/Window[@Name='聊天文件'][@ClassName='FileListMgrWnd'][@ProcessId={sutWindow.ProcessId}]"),
            timeout: TimeSpan.FromSeconds(10),
            interval: TimeSpan.FromMilliseconds(200)
        );
        await WxAutomation.Wait(4);
        sutWindow.Navigation.CloseNavigation(NavigationType.聊天文件);
    }
}

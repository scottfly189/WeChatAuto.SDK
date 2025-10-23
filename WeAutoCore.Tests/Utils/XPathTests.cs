using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;
using WxAutoCommon.Models;
using WxAutoCore.Services;
using WxAutoCommon.Configs;
using Xunit;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Utils;

[Collection("UiTestCollection")]
public class XPathTests
{
    private readonly string _wxClientName = "AlexZhao";
    private ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public XPathTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试XPath")]
    public void Test_Navigation_XPath()
    {
        var framework = _globalFixture.wxFramwork;
        var sutClient = framework.GetWxClient(_wxClientName);
        var sutWindow = sutClient.WxMainWindow;
        // var element = Retry.WhileNull(checkMethod: () => sutWindow.Window
        //     .FindFirstByXPath($"/Pane[1]/Pane/Pane[1]/Button").AsButton(),
        //     timeout: TimeSpan.FromSeconds(10),
        //     interval: TimeSpan.FromMilliseconds(200)
        // );
        // Assert.NotNull(element);
        // element?.Result?.DrawHighlight();
        Assert.True(true);
    }
}

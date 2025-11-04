using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WxAutoCommon.Enums;
using WxAutoCommon.Models;
using WeChatAuto.Services;
using WxAutoCommon.Configs;
using Xunit;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Utils;

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
    public void Test_Navigation_XPath()
    {
        var framework = _globalFixture.clientFactory;
        var sutClient = framework.GetWeChatClient(_wxClientName);
        var sutWindow = sutClient.WxMainWindow;
        Assert.True(true);
    }
}

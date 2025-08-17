using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using Xunit;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Utils;

[Collection("GlobalCollection")]
public class XPathTests
{
    private readonly string _wxClientName = "Alex Zhao";
    private ITestOutputHelper _output;
    private GlobalFixture _globalFixture;
    public XPathTests(ITestOutputHelper output, GlobalFixture globalFixture)
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
        string xPath = $"//ToolBar[@Name='导航'][@IsEnabled='true'][@ProcessId='{sutWindow.ProcessId}']";
        _output.WriteLine(sutWindow.ProcessId.ToString());
        var navigationRoot = Retry.WhileNull(() => sutWindow.Window.FindFirstByXPath(xPath), timeout: TimeSpan.FromSeconds(2));
        navigationRoot?.Result?.DrawHighlight();
        await Task.CompletedTask;
        var addressButton = Retry.WhileNull(() => navigationRoot?.Result?.FindFirstByXPath("//Button[@Name='通讯录'][@IsEnabled='true']"),
        timeout: TimeSpan.FromSeconds(5));
        addressButton?.Result?.DrawHighlight();
    }
}

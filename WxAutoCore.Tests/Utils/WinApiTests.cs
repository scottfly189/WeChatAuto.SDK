using WxAutoCore.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WxAutoCore.Tests.Utils;

public class WinApiTests
{
    private ITestOutputHelper _output;
    public WinApiTests(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact(DisplayName = "测试获取在顶部的窗口的进程ID")]
    public void TestGetTopWindowProcessIdByClassName()
    {
        var topWindowProcessId = WinApi.GetTopWindowProcessIdByClassName("WeChatMainWndForPC");
        Assert.True(topWindowProcessId > 0);
        _output.WriteLine($"在顶部的窗口的进程ID: {topWindowProcessId}");
    }
}
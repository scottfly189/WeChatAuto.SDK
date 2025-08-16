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
    
    [Fact(Skip = "已测试成功，跳过")]
    // [Fact(DisplayName = "测试获取在顶部的窗口的进程ID")]
    public void TestGetTopWindowProcessIdByClassName()
    {
        var topWindowProcessId = WinApi.GetTopWindowProcessIdByClassName("WindowsForms10.Window.8.app.0.141b42a_r8_ad1");
        Assert.True(topWindowProcessId > 0);
        _output.WriteLine($"在顶部的窗口的进程ID: {topWindowProcessId}");
    }
}
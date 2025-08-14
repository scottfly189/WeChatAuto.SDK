using Xunit;
using WxAutoCore.Components;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace WxAutoCore.Tests.Components;

[Collection("GlobalCollection")]
public class WxFramworkTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WxFramwork _framework;
    private readonly ITestOutputHelper _output;
    public WxFramworkTests(GlobalFixture fixture, ITestOutputHelper output)
    {
        _serviceProvider = fixture.ServiceProvider;
        _framework = fixture.wxFramwork;
        _output = output;
    }
    [Fact(DisplayName = "测试GlobalFixture是否注入成功,应该注入成功")]
    public void Test_ServiceProvider_NotNull()
    {
        Assert.NotNull(_serviceProvider);
    }

    [Fact(DisplayName = "测试WxFramwork是否为空,应该不为空")]
    public void Test_WxFramwork_IsNotNull()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        Assert.NotNull(framework);
        _output.WriteLine("WxFramwork: " + framework.ToString());
    }

    [Fact(DisplayName = "测试WxFramwork是否初始化成功")]
    public void InitTest()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        Assert.NotNull(framework);
    }

    [Fact(DisplayName = "测试WxClient的NickName是否正确")]
    public void Test_WxClient_NickName()
    {
        var wxClientName = "Alex Zhao";
        var nickName = _framework.GetWxClient(wxClientName)?.NickName;
        Assert.Equal(wxClientName, nickName);
        wxClientName = "一个错误的名字";
        nickName = _framework.GetWxClient(wxClientName)?.NickName;
        Assert.Null(nickName);
    }
}
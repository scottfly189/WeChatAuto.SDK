using Xunit;
using WxAutoCore.Components;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Services;
public class GlobalFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WxFramwork _framework;
    private readonly string _sutClientName = "Alex Zhao";  //测试的
    private readonly WxClient _sutWxClient;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WxFramwork wxFramwork => _framework;
    public string SutClientName => _sutClientName;
    public WxClient SutWxClient => _sutWxClient;
    public GlobalFixture()
    {
        var services = new ServiceCollection();
        services.AddWxAutomation();
        _serviceProvider = services.BuildServiceProvider();
        _framework = _serviceProvider.GetRequiredService<WxFramwork>();
        _sutWxClient = _framework.GetWxClient(_sutClientName);
    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        framework.Dispose();
    }
}
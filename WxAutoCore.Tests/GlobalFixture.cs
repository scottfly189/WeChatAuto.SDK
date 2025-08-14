using Xunit;
using WxAutoCore.Components;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Services;
public class GlobalFixture : IDisposable
{
    private  IServiceProvider _serviceProvider;
    private  WxFramwork _framework;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WxFramwork wxFramwork => _framework;
    public GlobalFixture()
    {
        var services = new ServiceCollection();
        services.AddWxAutomation();
        _serviceProvider = services.BuildServiceProvider();
        _framework = _serviceProvider.GetRequiredService<WxFramwork>();
        _framework.Init();
    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        framework.Dispose();
    }
}
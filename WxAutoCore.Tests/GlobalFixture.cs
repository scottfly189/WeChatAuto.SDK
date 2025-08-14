using Xunit;
using WxAutoCore.Components;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Services;
public class GlobalFixture : IDisposable
{
    private  IServiceProvider _serviceProvider;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public GlobalFixture()
    {
        var services = new ServiceCollection();
        services.AddWxAutomation();
        _serviceProvider = services.BuildServiceProvider();
        //_serviceProvider.GetRequiredService<WxFramwork>().Init();
    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        framework.Dispose();
    }
}
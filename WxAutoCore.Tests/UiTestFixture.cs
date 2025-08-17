using Xunit;
using WxAutoCore.Components;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Services;
public class UiTestFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WxFramwork _framework;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WxFramwork wxFramwork => _framework;
    public UiTestFixture()
    {
        var services = new ServiceCollection();
        services.AddWxAutomation();
        _serviceProvider = services.BuildServiceProvider();
        _framework = _serviceProvider.GetRequiredService<WxFramwork>();

    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WxFramwork>();
        if (framework != null)
        {
            framework.Dispose();
        }
    }
}
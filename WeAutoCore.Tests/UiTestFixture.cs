using Xunit;
using WxAutoCore.Components;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Services;
public class UiTestFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WeChatFramwork _framework;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WeChatFramwork wxFramwork => _framework;
    public UiTestFixture()
    {
        var services = new ServiceCollection();
        services.AddWxAutomation();
        _serviceProvider = services.BuildServiceProvider();
        _framework = _serviceProvider.GetRequiredService<WeChatFramwork>();

    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WeChatFramwork>();
        if (framework != null)
        {
            framework.Dispose();
        }
    }
}
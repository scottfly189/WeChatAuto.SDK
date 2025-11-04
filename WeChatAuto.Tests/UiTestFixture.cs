using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;
public class UiTestFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WeChatClientFactory _framework;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WeChatClientFactory wxFramwork => _framework;
    public UiTestFixture()
    {
        _serviceProvider = WeAutomation.GetServiceProvider(options =>
        {
            options.DebugMode = true;
            options.EnableMouseKeyboardSimulator = true;
            options.KMDeiviceVID = 0x2612;
            options.KMDeivicePID = 0x1701;
        });
        _framework = _serviceProvider.GetRequiredService<WeChatClientFactory>();

    }
    public void Dispose()
    {
        var framework = _serviceProvider.GetRequiredService<WeChatClientFactory>();
        if (framework != null)
        {
            framework.Dispose();
        }
    }
}
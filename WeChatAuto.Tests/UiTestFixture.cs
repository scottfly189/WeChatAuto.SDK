using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Components;
using WeChatAuto.Services;
public class UiTestFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WeChatClientFactory _Factory;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WeChatClientFactory clientFactory => _Factory;
    public UiTestFixture()
    {
        _serviceProvider = WeAutomation.Initialize(options =>
        {
            options.DebugMode = true;
            options.EnableMouseKeyboardSimulator = true;
            options.KMDeiviceVID = 0x30FA;
            options.KMDeivicePID = 0x0300;
            options.KMVerifyUserData = "7AFC3F101F98F5E50939A84AD36F9357";
            options.KMMouseMoveMode = 8;
            options.EnableRecordVideo = true;
        });
        _Factory = _serviceProvider.GetRequiredService<WeChatClientFactory>();

    }
    public void Dispose()
    {
        _Factory.Dispose();
    }
}
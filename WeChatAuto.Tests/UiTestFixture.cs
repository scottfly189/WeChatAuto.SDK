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
            options.KMDeiviceVID = 0x2612;
            options.KMDeivicePID = 0x1701;
            options.KMVerifyUserData = "4F6A21981BE675822DEE7B9BC39F3791";
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
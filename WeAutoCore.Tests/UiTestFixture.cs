using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCore.Components;
using WxAutoCore.Services;
public class UiTestFixture : IDisposable
{
    private IServiceProvider _serviceProvider;
    private WeChatFramwork _framework;
    public IServiceProvider ServiceProvider => _serviceProvider;
    public WeChatFramwork wxFramwork => _framework;
    public UiTestFixture()
    {
        _serviceProvider = WeAutomation.GetServiceProvider(options =>
        {
            options.TestClientName = "Alex Zhao";
            options.TestFriendNickName = "AI.Net";
            options.TestGroupNickName = ".NET-AI实时快讯3群";
            options.TestAlternativeFriendNickName = "文件传输助手";
            options.TestAlternativeGroupNickName = "备选测试群";
            options.DebugMode = true;
        });
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
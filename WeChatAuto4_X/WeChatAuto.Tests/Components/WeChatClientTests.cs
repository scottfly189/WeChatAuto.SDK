using System.Diagnostics;
using OneOf;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;


[Collection("UiTestCollection")]
public class WeChatClientTests
{
    private readonly string _wxClientName = "Alex";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public WeChatClientTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "测试初始化")]
    public void Test_Initialize()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        Assert.NotEmpty(client.NickName);
    }
    [Fact(DisplayName = "测试保存自己的头像")]
    public async Task Test_Save_Avator()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var path = Path.Combine(AppContext.BaseDirectory, "temp");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png");
        await client.Navigation.SaveOwnerAvator(path);
        Assert.True(File.Exists(path));
    }
}
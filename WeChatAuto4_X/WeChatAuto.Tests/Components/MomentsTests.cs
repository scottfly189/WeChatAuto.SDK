using System.Diagnostics;
using OneOf;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;


[Collection("UiTestCollection")]
public class MomentsTests
{
    private readonly string _wxClientName = "Alex";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public MomentsTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    [Fact(DisplayName = "刷新朋友圈")]
    public async Task TestRefresh_sns_list()
    {
        // Given
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        // When
        var result = await client.RefeshSnsList();
        // Then
        Assert.True(result);
    }
    // [Fact(DisplayName = "打开朋友圈-关闭朋友圈-vip功能")]
    // public async Task Test_open_close_moments()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     await client.OpenMoments();
    //     await Task.Delay(5000);
    //     await client.CloseMoments();
    //     Assert.True(true);
    // }

    // [Fact(DisplayName = "发朋友圈-vip功能")]
    // public async Task Test_add_moments()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.AddMoments(new List<string> {Path.Combine(AppContext.BaseDirectory, "Assets", "1.png"),
    //     Path.Combine(AppContext.BaseDirectory,"Assets","pzllm.png") }, "应该可以的", new Options.MomentsOptions
    //     {
    //         AtUsrs = new List<string> { "软件作家涛哥_vip" },
    //         Labels = new List<string>() { "aaa" },
    //     });
    //     Assert.True(result);
    // }

    // [Fact(DisplayName = "发朋友圈-多标签-vip功能")]
    // public async Task Test_add_moments_mult_label()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.AddMoments(new List<string> {Path.Combine(AppContext.BaseDirectory, "Assets", "1.png"),
    //     Path.Combine(AppContext.BaseDirectory,"Assets","pzllm.png") }, "使用键鼠测试发朋友圈一", new Options.MomentsOptions
    //     {
    //         AtUsrs = new List<string> { "软件作家涛哥_vip" },
    //         Labels = new List<string> { "aaa", "666" },
    //     });
    //     Assert.True(result);
    // }

    // [Fact(DisplayName = "发朋友圈-测试错误情况 - vip 功能")]
    // public async Task Test_add_moments_err()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.AddMoments(new List<string> {Path.Combine(AppContext.BaseDirectory, "Assets", "1.png"),
    //     Path.Combine(AppContext.BaseDirectory,"Assets","pzllm.png") }, "使用键鼠测试发朋友圈一", new Options.MomentsOptions
    //     {
    //         AtUsrs = new List<string> { "软件作家涛哥_vip222" },
    //         Labels = new List<string> { "888" },
    //     });
    //     Assert.False(result);
    // }

    // [Fact(DisplayName = "删除朋友圈 - vip功能")]
    // public async Task Test_remove_monents()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.RemoveMoments("使用键鼠测试发朋友圈一");
    //     Assert.True(result);
    // }

    // [Fact(DisplayName = "删除朋友圈-故意设置错误 - vip 功能")]
    // public async Task Test_remove_monents_error()
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.RemoveMoments("使用键鼠测试发朋友圈一");
    //     Assert.False(result);
    // }
}
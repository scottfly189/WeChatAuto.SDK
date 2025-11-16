using OneOf;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;

[Collection("UiTestCollection")]
public class WeChatClientTests
{
    private readonly string _wxClientName = "Alex Zhao";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public WeChatClientTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }
    [Fact(DisplayName = "测试微信客户端运行检查监听")]
    public async Task TestCheckAppRunning()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        Assert.True(client.AppRunning);
        await Task.Delay(-1);  //阻塞测试，直到微信客户端退出
    }

    [Fact(DisplayName = "测试屏幕截图")]
    public async Task TestCaptureUI()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var filePath = client.CaptureUI();
        _output.WriteLine($"截图保存路径：{filePath}");
        Assert.True(!string.IsNullOrWhiteSpace(filePath));
        await Task.CompletedTask;
    }

    [Fact(DisplayName = "测试视频录制")]
    public async Task TestRecordVideo()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        Assert.True(true);
        await Task.Delay(20 * 1_000);
    }
    #region NotifyIcon操作

    [Fact(DisplayName = "测试点击通知图标")]
    public async Task TestClickNotifyIcon()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.ClickNotifyIcon();
        await Task.Delay(20 * 1_000);
        Assert.True(true);
    }
    #endregion
    #region 主窗口操作
    [Theory(DisplayName = "测试窗口置顶")]
    [InlineData(true)]
    [InlineData(false)]
    public async Task TestWindowTop(bool isTop = true)
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.WindowTop(isTop);
        await Task.Delay(10 * 1_000);
        Assert.True(true);
    }
    [Fact(DisplayName = "测试窗口最小化")]
    public async Task TestWindowMin()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        await Task.Delay(5 * 1_000);
        client.WindowMin();
        await Task.Delay(10 * 1_000);
        Assert.True(true);
    }
    [Fact(DisplayName = "测试窗口最大化")]
    public async Task TestWindowMax()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        await Task.Delay(5 * 1_000);
        client.WindowMax();
        await Task.Delay(10 * 1_000);
        Assert.True(true);
    }
    [Fact(DisplayName = "测试窗口还原")]
    public async Task TestWindowRestore()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        await Task.Delay(5 * 1_000);
        client.WindowRestore();
        Assert.True(true);
    }
    #endregion
    #region 搜索
    [Theory(DisplayName = "测试搜索")]
    [InlineData("AI.Net", false)]
    [InlineData("AI.Net", true)]
    public async Task TestSearchSomething(string text, bool isClear = false)
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.SearchSomething(text, isClear);
        Assert.True(true);
        await Task.CompletedTask;
    }
    [Fact(DisplayName = "测试清空搜索框")]
    public async Task TestClearText()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        await Task.Delay(5 * 1_000);
        client.ClearText();
        Assert.True(true);
        await Task.CompletedTask;
    }
    [Fact(DisplayName = "测试搜索聊天")]
    public async Task TestSearchChat()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.SearchChat("AI.Net");
        Assert.True(true);
        await Task.CompletedTask;
    }
    [Fact(DisplayName = "测试搜索联系人")]
    public async Task TestSearchContact()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.SearchContact("AI.Net");
        Assert.True(true);
        await Task.CompletedTask;
    }

    [Fact(DisplayName = "测试搜索收藏")]
    public async Task TestSearchCollection()
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        client.SearchCollection("AI.Net");
        Assert.True(true);
        await Task.CompletedTask;
    }
    #endregion
    #region 消息操作
    //注意重点测试：
    // 1.接收人不在会话中;2.接收人在会话中;3.接收人在会话中，但是在一些特殊一点的位置;4、接收人在子窗口中.
    //加强@测试,完成@谁与@全部人,群聊中
    [Theory(DisplayName = "测试发送消息给单个好友")]
    [InlineData("AI.Net", "你好，世界1！", "", false, true, 1)]
    [InlineData("测试11", "你好，世界2！", "", false, true, 2)]
    [InlineData("AI.Net", "你好，世界3！", "", true, true, 3)]
    [InlineData("测试11", "你好，世界4!", "", true, true, 4)]
    [InlineData("测试11", "你好，世界5！", new string[] { "AI.Net", "秋歌" }, false, true, 5)]
    [InlineData("超级猩球2", "你好，世界6！", new string[] { "杨奇峰", "V姐", "土豆核", "土豆核2" }, false, true, 6)]
    [InlineData("歪脖子的模版交流群", "好晚，大家睡着没有？", new string[] { "直脖子", "使不得先生", "常" }, false, true, 7)]
    [InlineData("测试11", "你好，世界5！", new string[] { "AI.Net", "秋歌" }, true, true, 8)]
    [InlineData("歪脖子的模版交流群", "好晚，大家睡着没有？", new string[] { "直脖子", "使不得先生", "常" }, true, true, 9)]
    public async Task TestSendWho(string who, string message, object atUser = default,
        bool isOpenChat = true, bool result = true, int flag = 0)
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        var atUserOneOf = atUser is string ? OneOf<string, string[]>.FromT0((string)atUser) : OneOf<string, string[]>.FromT1((string[])atUser);
        await client.SendWho(who, message, atUserOneOf, isOpenChat);
        _output.WriteLine($"测试标识：{flag}");
        Assert.True(result);
        await Task.CompletedTask;
    }

    [Theory(DisplayName = "测试发送消息给多个好友")]
    [InlineData(new string[] { "AI.Net","测试11",".NET-AI实时快讯3群" }, "你好，世界1！", "", false, true, 1)]
    [InlineData(new string[] { "AI.Net","测试11",".NET-AI实时快讯3群" }, "你好，世界3！", "", true, true, 2)]
    [InlineData(new string[] { "测试11" }, "你好，世界4!", "", true, true, 3)]
    [InlineData(new string[] { "测试01","测试11",".NET-AI实时快讯3群" }, "你好，世界5！", new string[] { "AI.Net", "秋歌" }, false, true,4)]
    [InlineData(new string[] { "测试01","测试11",".NET-AI实时快讯3群","歪脖子的模版交流群" }, "今日大家都没有休息？", new string[] { "直脖子", "使不得先生", "常" }, true, true, 5)]
    public async Task TestSendWhos(string[] whos, string message, object atUser = default,
        bool isOpenChat = true, bool result = true, int flag = 0)
    {
        var clientFactory = _globalFixture.clientFactory;
        var client = clientFactory.GetWeChatClient(_wxClientName);
        var atUserOneOf = atUser is string ? OneOf<string, string[]>.FromT0((string)atUser) : OneOf<string, string[]>.FromT1((string[])atUser);
        await client.SendWhos(whos, message, atUserOneOf, isOpenChat);
        _output.WriteLine($"测试标识：{flag}");
        Assert.True(true);
        await Task.CompletedTask;
    }
    #endregion
}
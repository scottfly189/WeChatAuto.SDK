using System.Diagnostics;
using OneOf;
using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components;


[Collection("UiTestCollection")]
public class MessageBubbleListTests
{
    private readonly string _wxClientName = "Alex";
    private readonly ITestOutputHelper _output;
    private UiTestFixture _globalFixture;
    public MessageBubbleListTests(ITestOutputHelper output, UiTestFixture globalFixture)
    {
        _output = output;
        _globalFixture = globalFixture;
    }

    [Fact(DisplayName = "获取当前窗口的聊天记录")]
    public async Task Test_Get_Current_ChatHistory()
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var list = await client.GetChatHistory(DateTime.Parse("2026-09-03"));
        Assert.True(list.Count != 0);
        list.ForEach(item =>
        {
            _output.WriteLine(item.ToString());
        });
        _output.WriteLine($"总共有:{list.Count}条消息");
    }

    [Theory(DisplayName = "测试按日期获取历史消息")]
    [InlineData("WeChatAuto.SDK官方技术支持")]
    // [InlineData("前端攻城狮")]
    [InlineData("苏智明_vip")]
    [InlineData("软件作家涛哥_vip")]
    [InlineData("[9]Senparc微信视频课程学员群")]
    public async Task Test_Get_ChatHistory(string who)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var list = await client.GetChatHistory(who, DateTime.Parse("2026-05-27"));
        Assert.True(list.Count != 0);
        list.ForEach(item =>
        {
            _output.WriteLine(item.ToString());
        });
        _output.WriteLine($"总共有:{list.Count}条消息");
    }

    [Theory(DisplayName = "测试按日期-开始时间-结束时间获取历史消息")]
    [InlineData("郭老总_vip")]
    [InlineData("软件作家涛哥_vip")]
    [InlineData("苏智明_vip")]
    public async Task Test_GetChatHistory_startdate_enddate(string who)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var list = await client.GetChatHistory(who, DateTime.Parse("2026-06-13 19:20"), DateTime.Parse("2026-06-13 19:30"));
        Assert.True(list.Count != 0);
        list.ForEach(item =>
        {
            _output.WriteLine(item.ToString());
        });
        _output.WriteLine($"总共有:{list.Count}条消息");
    }

    [Theory(DisplayName = "测试拍一拍-群聊")]
    [InlineData("智影工坊_test")]
    [InlineData("AI.Net")]
    public async Task Test_Tap_who_group(string who)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var result = await client.TapWho(who);
        Assert.True(result);
    }
    [Theory(DisplayName = "测试拍一拍-好友")]
    [InlineData("秋歌")]
    public async Task Test_Tap_who_single(string who)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var result = await client.TapWho(who);
        Assert.True(result);
    }

    // [Theory(DisplayName = "测试转发单条消息")]
    // [InlineData("AI.Net", "我来一条：这个是测试一")]
    // [InlineData("Alex", "谁敢相信？困扰无数人的养生真谛，竟然全被这几句大白话讲透了！")]
    // [InlineData("秋歌", "昨天数学课堂作业没写完，今天语文没写完[擦汗]")]
    // [InlineData("Alex", "还不算啊，老婆")]
    // public async Task Test_Forward_Sinble_message(string who, string message)
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.ForwardSingleMessage(who, message, new string[] { "AI.Net", "文件传输助手" }, 40);
    //     Assert.True(result);
    // }

    // [Theory(DisplayName = "测试转发多条消息-本窗口")]
    // [InlineData(10)]
    // public async Task Test_Forward_multix_message(int rowNo)
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.ForwardMultipleMessage("",new string[] { "AI.Net", "文件传输助手" }, rowCount: rowNo);
    //     Assert.True(result);
    // }

    // [Theory(DisplayName = "测试转发多条消息-查找who")]
    // [InlineData(10)]
    // public async Task Test_Forward_multix_message_who(int rowNo)
    // {
    //     var framework = _globalFixture.clientFactory;
    //     var client = framework.GetWeChatClient(_wxClientName);
    //     var result = await client.ForwardMultipleMessage("秋歌", new string[] { "AI.Net", "文件传输助手" }, rowCount: rowNo);
    //     Assert.True(result);
    // }

}
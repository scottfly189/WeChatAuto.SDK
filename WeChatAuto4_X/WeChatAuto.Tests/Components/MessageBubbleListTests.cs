using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using OneOf;
using WeAutoCommon.Extentions;
using WeChatAuto.Components;
using WeChatAuto.Models;
using WeChatAuto.Utils;
using Xunit.Abstractions;
using System.Linq;
using Dm.util;

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

    [Theory(DisplayName = "测试转发单条消息")]
    [InlineData("AI.Net", "我来一条：这个是测试一")]
    [InlineData("Alex", "谁敢相信？困扰无数人的养生真谛，竟然全被这几句大白话讲透了！")]
    [InlineData("秋歌", "昨天数学课堂作业没写完，今天语文没写完[擦汗]")]
    [InlineData("Alex", "还不算啊，老婆")]
    public async Task Test_Forward_Sinble_message(string who, string message)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var result = await client.ForwardSingleMessage(who, message, new string[] { "AI.Net", "文件传输助手" }, 40);
        Assert.True(result);
    }

    [Theory(DisplayName = "测试转发多条消息-本窗口")]
    [InlineData(10)]
    public async Task Test_Forward_multix_message(int rowNo)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var result = await client.ForwardMultipleMessage("", new string[] { "AI.Net", "文件传输助手" }, rowCount: rowNo);
        Assert.True(result);
    }

    [Theory(DisplayName = "测试转发多条消息-查找who")]
    [InlineData(10)]
    public async Task Test_Forward_multix_message_who(int rowNo)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        var result = await client.ForwardMultipleMessage("秋歌", new string[] { "AI.Net", "文件传输助手" }, rowCount: rowNo);
        Assert.True(result);
    }

    [Theory(DisplayName = "给定一个AutomationElement对象 - 获取图片信息.")]
    [InlineData("DroidMirror官方技术支持")]
    public async Task Test_FetchImageAsync(string groupName)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        //首先找到一个图片元素
        await client.SearchFriend(groupName);
        var root = client.MainWindow.FindFirstByXPath(UITreeGlobal.MessageRootPath).AsListBox();
        var bubble = await _GetImageElementAsync(client, root);
        FetchedMedia fetchedMedia = await client.ChatContent.FetchImageAsync(bubble);
        _output.WriteLine(fetchedMedia.toString());
        Assert.NotNull(fetchedMedia);
    }

    [Theory(DisplayName = "给定一个AutomationElement对象 - 获取文件信息.")]
    [InlineData("DroidMirror官方技术支持")]
    public async Task Test_FetchFileAsync(string groupName)
    {
        var framework = _globalFixture.clientFactory;
        var client = framework.GetWeChatClient(_wxClientName);
        //首先找到一个图片元素
        await client.SearchFriend(groupName);
        var root = client.MainWindow.FindFirstByXPath(UITreeGlobal.MessageRootPath).AsListBox();
        var bubble = await _GetFileElementAsync(client, root);
        FetchedMedia fetchedMedia = await client.ChatContent.FetchFileAsync(bubble);
        _output.WriteLine(fetchedMedia.toString());
        Assert.NotNull(fetchedMedia);
    }

    private async Task<AutomationElement> _GetFileElementAsync(WeChatClient client, ListBox? root)
    {
        var point = root!.BoundingRectangle.SafeRandomPoint();
        AutomationElement? bubble = null;

        var index = 0;
        var oldSnap = new List<string>();
        while (index < 3) //先下到底部
        {
            var newSnap = root.Items.Select(u => u.Properties.RuntimeId.ToUniqueString()).ToList();
            var exceptList = newSnap.Except(oldSnap).ToList();
            if (exceptList.Count > 0)
            {
                index = 0;
                oldSnap = newSnap;
            }
            else
            {
                if (root.Items[root.Items.Count() - 1].BoundingRectangle.Y +
                    root.Items[root.Items.Count() - 1].BoundingRectangle.Height >
                    root.BoundingRectangle.Y +
                    root.BoundingRectangle.Height + 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            MouseScrollHelper.DownStep(point.Confusion(3, 6), 3);
        }
        //再往上滚动，方便发现一张文件消息
        index = 0;
        oldSnap = new List<string>();
        var ratio = DpiHelper.GetScaleForWindow(client.MainWindow.Properties.NativeWindowHandle);
        while (index < 3)
        {
            var newSnap = new List<string>();
            __GetNewSnapshot__(root, newSnap, ratio);
            var exceptList = newSnap.Except(oldSnap).ToList();
            if (exceptList.Count > 0)
            {
                index = 0;
                oldSnap = newSnap;
                var willProcessList = root.AsListBox().Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
                var item = willProcessList.Where(u => u.Properties.ClassName.Equals("mmui::ChatBubbleItemView") && u.Name.startsWith("文件")).FirstOrDefault();
                if (item != null)
                {
                    bubble = item;
                    break;
                }
            }
            else
            {
                if (root.Items[0].BoundingRectangle.Y < root.BoundingRectangle.Y)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            MouseScrollHelper.UpStep(point.Confusion(3, 6), 3);
        }

        return bubble;
    }

    private async Task<AutomationElement?> _GetImageElementAsync(WeChatClient client, ListBox? root)
    {
        var point = root!.BoundingRectangle.SafeRandomPoint();
        AutomationElement? bubble = null;

        var index = 0;
        var oldSnap = new List<string>();
        while (index < 3) //先下到底部
        {
            var newSnap = root.Items.Select(u => u.Properties.RuntimeId.ToUniqueString()).ToList();
            var exceptList = newSnap.Except(oldSnap).ToList();
            if (exceptList.Count > 0)
            {
                index = 0;
                oldSnap = newSnap;
            }
            else
            {
                if (root.Items[root.Items.Count() - 1].BoundingRectangle.Y +
                    root.Items[root.Items.Count() - 1].BoundingRectangle.Height >
                    root.BoundingRectangle.Y +
                    root.BoundingRectangle.Height + 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            MouseScrollHelper.DownStep(point.Confusion(3, 6), 3);
        }
        //再往上滚动，方便发现一张图片消息
        index = 0;
        oldSnap = new List<string>();
        var ratio = DpiHelper.GetScaleForWindow(client.MainWindow.Properties.NativeWindowHandle);
        while (index < 3)
        {
            var newSnap = new List<string>();
            __GetNewSnapshot__(root, newSnap, ratio);
            var exceptList = newSnap.Except(oldSnap).ToList();
            if (exceptList.Count > 0)
            {
                index = 0;
                oldSnap = newSnap;
                var willProcessList = root.AsListBox().Items.Where(u => exceptList.Contains(u.Properties.RuntimeId.ToUniqueString())).ToList();
                var item = willProcessList.Where(u => u.Properties.ClassName.Equals("mmui::ChatBubbleReferItemView") && u.Name.startsWith("图片")).FirstOrDefault();
                if (item != null)
                {
                    bubble = item;
                    break;
                }
            }
            else
            {
                if (root.Items[0].BoundingRectangle.Y < root.BoundingRectangle.Y)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            MouseScrollHelper.UpStep(point.Confusion(3, 6), 3);
        }

        return bubble;
    }

    private List<string> businessMessageClassNames = new List<string>()
        {
            "mmui::ChatBubbleReferItemView","mmui::ChatBubbleItemView","mmui::ChatVoiceItemView","mmui::ChatPersonalCardItemView","mmui::ChatNoteCardItemView","mmui::ChatTextItemView","mmui::ChatImageGroupItemView"
        };

    private void __GetNewSnapshot__(AutomationElement root, List<string> newSnapshot, decimal ratio)
    {
        var height = (int)(73 * ratio);        //考虑dpi影响,为群消息的最小高度
        var height1 = (int)(56 * ratio);       //考虑右边是自己的情况
        var items = root.AsListBox().Items;
        foreach (var item in items)
        {
            if (item.ClassName.equals("mmui::ChatItemView") || item.ClassName.equals("mmui::ChatSystemInfoItemView") || item.ClassName.equals("mmui::ChatFolderItemView"))
            {
                //系统消息,直接加入
                newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
            }
            else
            {
                if (businessMessageClassNames.Contains(item.Properties.ClassName))
                {
                    var realHeight = height;
                    if (item.BoundingRectangle.Height <= height)
                    {
                        realHeight = item.BoundingRectangle.Height;
                    }
                    if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + realHeight <= root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                    {
                        newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
                    }
                    else
                    {
                        realHeight = height1;
                        if (item.BoundingRectangle.Height <= height1)
                        {
                            realHeight = item.BoundingRectangle.Height;
                        }
                        if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + realHeight <= root.BoundingRectangle.Y + root.BoundingRectangle.Height + 1)
                        {
                            newSnapshot.add(item.Properties.RuntimeId.ToUniqueString());
                        }
                    }
                }
                else
                {
                    throw new Exception($"错误：终于发现一个以前不存在的消息ClassName={item.Properties.ClassName},请告之作者！");
                }
            }
        }
    }
}
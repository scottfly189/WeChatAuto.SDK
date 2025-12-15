using WeAutoCommon.Models;
using WeChatAuto.Services;
using WeChatAuto.Utils;
using Xunit.Abstractions;
using System.Diagnostics;
using Xunit.Sdk;
using WeChatAuto.Models;
using WeAutoCommon.Utils;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace WeChatAuto.Tests.Components
{
    [Collection("UiTestCollection")]
    public class WxWindowTests
    {
        private readonly string _wxClientName = "Alex";
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public WxWindowTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试窗口操作")]
        public async Task TestWindowAction()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            window.WindowMax();
            DrawHightlightHelper.DrawHightlight(window.Window, window.UiMainThreadInvoker);
            window.WindowRestore();
            DrawHightlightHelper.DrawHightlight(window.Window, window.UiMainThreadInvoker);
            window.WindowMin();
            await Task.Delay(2000);
            window.WindowTop(true);
            await Task.Delay(2000);
            window.WindowTop(false);
            await Task.Delay(2000);
            Assert.True(true);
        }

        [Fact(DisplayName = "测试获取昵称")]
        public void TestNickName()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var nickName = window.NickName;
            _output.WriteLine($"昵称: {nickName}");
            Assert.Equal(_wxClientName, nickName);
        }

        [Fact(DisplayName = "测试获取当前聊天窗口的标题")]
        public void Test_GetCurrentChatTitle()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var title = window.GetCurrentChatTitle();
            _output.WriteLine($"当前聊天窗口的标题: {title}");
            Assert.True(title != null);
        }
        //要先打开测试人的聊天窗口
        [Fact(DisplayName = "测试发送消息-已打开聊天窗口")]
        public async Task Test_SendWho_AlreadyOpenChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("AI.Net", "你好，世界111！");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试发送消息-当前聊天窗口-确保打开是测试人的聊天窗口")]
        public async Task Test_SendWho_CurrentChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("AI.Net", "你好，世界222！");
            Assert.True(true);
            await Task.Delay(60000);
        }
        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("AI.Net", "你好，世界333！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,但是在会话列表中,并打开聊天窗口")]
        public async Task Test_SendWho_NotCurrentChat_InConversationList_OpenChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat("AI.Net", "你好，世界333222！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送消息-非当前聊天窗口,不在会话列表中")]
        public async Task Test_SendWho_NotCurrentChat_NOT_InConversationList()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("AI.Net", "你好，世界444！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-不存在的人")]
        public async Task Test_SendWho_Not_Exist_Person()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWho("不存在的人", "你好，世界555！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息")]
        public async Task Test_SendWhoAndOpenChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhoAndOpenChat("AI.Net", "你好，世界666！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量")]
        public async Task Test_SendWhos()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhos(["AI.Net", ".NET-AI实时快讯3群"], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(30000);
        }

        [Fact(DisplayName = "测试发送消息-批量,并打开聊天窗口")]
        public async Task Test_SendWhosAndOpenChat()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendWhosAndOpenChat(["AI.Net", ".NET-AI实时快讯3群"], "你好，世界777！");
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送图片")]
        public async Task Test_File_image()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendFile("AI.Net", @"C:\Users\Administrator\Desktop\ssss\logo.png", false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试发送文件-发送视频")]
        public async Task Test_File_vedio()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendFile("AI.Net", @"C:\Users\Administrator\Desktop\ssss\4.mp4", false);
            Assert.True(true);
            await Task.Delay(60000);
        }


        [Fact(DisplayName = "测试发送文件-发送多个文件")]
        public async Task Test_File_Multi_File()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.SendFile(".NET-AI实时快讯3群", new string[] { @"C:\Users\Administrator\Desktop\ssss\4.mp4", @"C:\Users\Administrator\Desktop\ssss\logo.png", @"C:\Users\Administrator\Desktop\ssss\3.pdf" }, false);
            Assert.True(true);
            await Task.Delay(60000);
        }

        [Fact(DisplayName = "测试添加新好友监听-自定义通过")]
        public async Task Test_AddNewFriendCustomPassedListener()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            window.AddNewFriendCustomPassedListener(list =>
            {
                _output.WriteLine($"添加好友: {list.Count}");
            });
            Assert.True(true);
            await Task.Delay(600000000);
        }

        //实际测试好象长时间放置线程有问题.
        [Fact(DisplayName = "测试添加新好友监听-自动通过")]
        public async Task Test_AddNewFriendAutoPassedListener()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            window.AddNewFriendAutoPassedListener(list =>
            {
                _output.WriteLine($"添加好友: {list.Count}");
            }, null, "test", "测试");
            Assert.True(true);
            await Task.Delay(600000000);
        }

        [Fact(DisplayName = "测试添加消息监听")]
        public async Task Test_AddMessageListener()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.AddMessageListener("AI.Net", (messageContext) =>
            {
                //Trace.WriteLine($"消息: 收到新消息数量:{newBubbles.Count},当前可见消息数量:{bubblesList.Count}");
            });
            Assert.True(true);
            await Task.Delay(6000000);
        }
        [Fact(DisplayName = "测试添加消息监听,并返回新消息")]
        public async Task Test_AddMessageListener_Reback()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.AddMessageListener("歪脖子的模版交流群", (messageContext) =>
            {
                // Trace.WriteLine($"消息: 收到新消息数量:{newBubbles.Count},当前可见消息数量:{bubblesList.Count}");
                // foreach (var bubble in newBubbles)
                // {
                //     Trace.WriteLine($"消息: 新消息内容:{bubble.MessageContent}");
                //     //sender.SendMessage($"收到消息:{bubble.MessageContent}");

                // }
            });
            Assert.True(true);
            await Task.Delay(6000000);
        }

        #region 群聊操作

        [Theory(DisplayName = "测试检查群聊是否存在")]
        [InlineData(".NET-AI实时快讯3群", false)]
        [InlineData("AI.Net", false)]
        [InlineData("AI.Net", true)]
        [InlineData(".NET-AI实时快讯3群", true)]
        [InlineData("不存在的人", false)]
        [InlineData("不存在的人", true)]
        public void Test_CheckFriendExist(string groupName, bool doubleClick = false)
        {
            var clientFacotry = _globalFixture.clientFactory;
            var client = clientFacotry.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var flag = window.CheckFriendExist(groupName, doubleClick);
            _output.WriteLine($"检查好友是否存在: {groupName}, 双击: {doubleClick}, 结果: {flag} ");
            if (groupName != "不存在的人")
            {
                Assert.True(flag);
            }
            else
            {
                Assert.False(flag);
            }
            Thread.Sleep(20 * 1_000);
        }

        [Fact(DisplayName = "测试创建群聊,群聊不存在的情况下")]
        public void Test_CreateOwnerChatGroup_ChatGroup_NotExist()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.CreateOrUpdateOwnerChatGroup("测试11", new string[] { "AI.Net", "秋歌" });
            _output.WriteLine($"创建群聊结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "测试创建群聊,群聊存在的情况下")]
        public void Test_CreateOwnerChatGroup_ChatGroup_Exist()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.CreateOrUpdateOwnerChatGroup("测试11", new string[] { "阿恩-frankie", "阿恩" });
            _output.WriteLine($"创建群聊结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "测试移除群聊成员")]
        public async Task Test_RemoveOwnerChatGroupMember()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.RemoveOwnerChatGroupMember("测试11", new string[] { "阿恩-frankie", "阿恩" });
            _output.WriteLine($"移除群聊成员结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "测试删除群聊")]
        public async Task Test_DeleteOwnerChatGroup()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.DeleteOwnerChatGroup("测试09-01");
            Assert.True(true);
        }


        [Fact(DisplayName = "测试更新群聊备注")]
        public async Task Test_UpdateGroupMemo()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.ChageOwerChatGroupMemo("测试09", "测试09新的备注6");
            _output.WriteLine($"更新群聊备注结果: {result.Message}");
            Assert.True(result.Success);
            await Task.CompletedTask;
        }

        [Fact(DisplayName = "测试更新群聊名称")]
        public async Task Test_UpdateGroupName()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.ChangeOwerChatGroupName("测试09", "测试09-01");
            _output.WriteLine($"更新群聊名称结果: {result.Message}");
            Assert.True(result.Success);
            await Task.Delay(40 * 1_000);
        }
        [Theory(DisplayName = "测试更新群聊公告")]
        [InlineData("测试04")]
        [InlineData("实时AI快讯 5群")]
        [InlineData("测试01")]
        public async Task Test_UpdateGroupNotice(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.UpdateGroupNotice(groupName, "测试04新的公告999");
            _output.WriteLine($"更新群聊公告结果: {result.Message}");
            if (groupName == "测试04" || groupName == "测试01")
            {
                Assert.True(result.Success);
            }
            else
            {
                Assert.False(result.Success);
            }
        }

        [Theory(DisplayName = "测试设置消息免打扰")]
        [InlineData("他有群01", true, true)]
        [InlineData("他有群01", false, true)]
        public void Test_SetMessageWithoutInterruption(string friendName, bool isMessageWithoutInterruption, bool resultFlag)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.SetMessageWithoutInterruption(friendName, isMessageWithoutInterruption);
            _output.WriteLine($"设置消息免打扰结果: {result.Message}");
            Assert.True(result.Success == resultFlag);
        }

        [Theory(DisplayName = "测试设置保存到通讯录")]
        [InlineData("他有群01", true, true)]
        [InlineData("他有群01", false, true)]
        public void Test_SetSaveToAddress(string friendName, bool isSaveToAddress, bool resultFlag)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.SetSaveToAddress(friendName, isSaveToAddress);
            _output.WriteLine($"设置保存到通讯录结果: {result.Message}");
            Assert.True(result.Success == resultFlag);
        }

        [Theory(DisplayName = "测试设置聊天置顶")]
        [InlineData("他有群02", true)]
        [InlineData("他有群02", false)]
        public void Test_SetChatTop(string friendName, bool isChatTop)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = window.SetChatTop(friendName, isChatTop);
            _output.WriteLine($"设置聊天置顶结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "测试清空群聊历史")]
        public async Task Test_ClearChatGroupHistory()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.ClearChatGroupHistory("测试03");
            Assert.True(true);
        }

        [Fact(DisplayName = "测试退出群聊")]
        public async Task Test_QuitChatGroup()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            await window.QuitChatGroup("测试03");
            Assert.True(true);
        }

        [Theory(DisplayName = "测试获取群主")]
        [InlineData(".NET-AI实时快讯3群")]
        [InlineData(".NET AI和sk的爱情故事会")]
        [InlineData("猫哥 VIP 学习二群")]
        public async Task Test_GetGroupOwner(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var owner = await window.GetGroupOwner(groupName);
            _output.WriteLine($"群主: {owner}");
            Assert.True(true);
        }

        [Theory(DisplayName = "测试获取群聊成员列表")]
        [InlineData(".NET-AI实时快讯3群")]
        [InlineData(".NET AI和sk的爱情故事会")]
        [InlineData("猫哥 VIP 学习二群")]
        public async Task Test_GetChatGroupMemberList(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var list = await window.GetChatGroupMemberList(groupName);
            foreach (var item in list)
            {
                _output.WriteLine(item);
            }
            Assert.True(true);
        }

        [Theory(DisplayName = "测试邀请群聊成员,适用于他有群")]
        [InlineData("他有群01")]
        [InlineData("他有群02")]
        public async Task Test_InviteChatGroupMember(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.InviteChatGroupMember(groupName, new string[] { "AI.Net" }, "你好啊");
            _output.WriteLine($"邀请群聊成员结果: {result.Message}");
            Assert.True(result.Success);
        }

        [Theory(DisplayName = "测试添加群聊成员为好友,适用于他有群")]
        [InlineData("他有群01")]
        [InlineData("他有群02")]
        public async Task Test_AddChatGroupMemberToFriends(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.AddChatGroupMemberToFriends(groupName, new string[] { "AI.Net", "秋歌" }, 5, "你好呀", "测试标签");
            _output.WriteLine($"添加群聊成员为好友结果: {result.Message}");
            Assert.True(result.Success);
            await Task.Delay(-1);
        }


        [Theory(DisplayName = "测试添加群聊成员为好友,适用于他有群")]
        [InlineData("他有群01")]
        [InlineData("他有群02")]
        [InlineData("歪脖子的模版交流群")]
        public async Task Test_AddAllChatGroupMemberToFriends(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.AddAllChatGroupMemberToFriends(groupName, null, 5, "兄弟不用管，测试自动化", "测试标签");
            _output.WriteLine($"添加群聊成员为好友结果: {result.Message}");
            Assert.True(result.Success);
            await Task.Delay(-1);
        }

        [Theory(DisplayName = "测试添加群聊成员为好友,适用于他有群,分页添加")]
        [InlineData("他有群01")]
        [InlineData("他有群02")]
        [InlineData("歪脖子的模版交流群")]
        public async Task Test_AddAllChatGroupMemberToFriends_Page(string groupName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var result = await window.AddAllChatGroupMemberToFriends(groupName, (options) =>
            {
                options.PageNo = 8;
                options.IntervalSecond = 5;
                options.HelloText = "兄弟不用管，测试自动化";
                options.Label = "测试标签";
                options.PageSize = 15;
            });
            _output.WriteLine($"添加群聊成员为好友结果: {result.Message}");
            Assert.True(result.Success);
            await Task.Delay(-1);
        }
        #endregion
    }
}
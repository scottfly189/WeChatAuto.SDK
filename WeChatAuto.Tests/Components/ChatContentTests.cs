using Xunit.Abstractions;

namespace WeChatAuto.Tests.Components
{
    /// <summary>
    /// 测试注意点：
    /// 1、主窗口的ChatContent
    /// 2、子窗口的ChatContent
    /// </summary>
    [Collection("UiTestCollection")]
    public class ChatContentTests
    {
        private readonly string _wxClientName = "Alex";
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public ChatContentTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }
        [Fact(DisplayName = "测试获取聊天标题_主窗口")]
        public void Test_FullTitle_MainWindow()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.MainChatContent;
            var fullTitle = chatContent.FullTitle;
            _output.WriteLine(fullTitle);
            Assert.True(!string.IsNullOrWhiteSpace(fullTitle));
        }
        [Theory(DisplayName = "测试获取聊天标题_子窗口")]
        [InlineData(".NET-AI实时快讯3群")]
        [InlineData("AI.Net")]
        [InlineData("微信支付")]
        public void Test_FullTitle_SubWindow(string subWinName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.SubWinList.GetSubWin(subWinName).ChatContent;
            var fullTitle = chatContent.FullTitle;
            _output.WriteLine(fullTitle);
            Assert.True(!string.IsNullOrWhiteSpace(fullTitle));
        }
        [Fact(DisplayName = "测试获取聊天人数_主窗口")]
        public void Test_GetChatMemberCount_MainWindow()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.MainChatContent;
            var chatMemberCount = chatContent.ChatMemberCount;
            _output.WriteLine(chatMemberCount.ToString());
            Assert.True(chatMemberCount > 0);
        }

        [Theory(DisplayName = "测试获取聊天人数_子窗口")]
        [InlineData(".NET-AI实时快讯3群")]
        [InlineData("AI.Net")]
        [InlineData("文件传输助手")]
        [InlineData("歪脖子的模版交流群")]
        public void Test_GetChatMemberCount_SubWindow(string subWinName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.SubWinList.GetSubWin(subWinName).ChatContent;
            var chatMemberCount = chatContent.ChatMemberCount;
            _output.WriteLine(chatMemberCount.ToString());
            Assert.True(chatMemberCount > 0);
        }

        [Fact(DisplayName = "测试获取聊天类型_主窗口")]
        public void Test_ChatType_MainWindow()
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.MainChatContent;
            var chatType = chatContent.ChatType;
            _output.WriteLine(chatType.ToString());
            Assert.True(true);
        }

        [Theory(DisplayName = "测试获取聊天类型_子窗口")]
        [InlineData(".NET-AI实时快讯3群")]
        [InlineData("AI.Net")]
        [InlineData("文件传输助手")]
        [InlineData("歪脖子的模版交流群")]
        [InlineData("51CTO博客")]
        public void Test_ChatType_SubWindow(string subWinName)
        {
            var framework = _globalFixture.clientFactory;
            var client = framework.GetWeChatClient(_wxClientName);
            var window = client.WxMainWindow;
            var chatContent = window.SubWinList.GetSubWin(subWinName).ChatContent;
            var chatType = chatContent.ChatType;
            _output.WriteLine(chatType.ToString());
            Assert.True(true);
        }
    }
}
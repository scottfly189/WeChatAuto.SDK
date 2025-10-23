using Xunit;
using FlaUI.Core.AutomationElements;
using FlaUI.Core;
using WxAutoCore.Components;
using Xunit.Abstractions;
using WxAutoCore.Services;
using WxAutoCommon.Models;
using WxAutoCommon.Configs;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class SubWinListTests
    {
        private readonly string _wxClientName = "Alex Zhao";
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public SubWinListTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
            _output.WriteLine("开始测试子窗口的各项功能 =================================================");
        }
        [Fact(DisplayName = "测试获取所有子窗口名称")]
        public async Task TestGetAllSubWinNames()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.Conversations.DoubleClickConversation("文件传输助手");
            var subWinList = window.SubWinList;
            var subWinNames = subWinList.GetAllSubWinNames();
            foreach (var subWinName in subWinNames)
            {
                _output.WriteLine($"子窗口名称: {subWinName}");
            }
            Assert.True(subWinNames.Count > 0);
            await Task.Delay(5000);
            subWinList.CloseAllSubWins();
        }
        [Theory(DisplayName = "测试获取子窗口")]
        [InlineData("文件传输助手")]
        public async Task TestGetSubWin(string subWinName)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.Conversations.DoubleClickConversation(subWinName);
            var subWinList = window.SubWinList;
            var subWin = subWinList.GetSubWin(subWinName);
            Assert.NotNull(subWin);
            await Task.Delay(5000);
            subWinList.CloseAllSubWins();
        }
        [Theory(DisplayName = "测试判断子窗口是否打开")]
        [InlineData("文件传输助手")]
        public async Task TestGetSubWinIsOpen(string subWinName)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            window.Conversations.DoubleClickConversation(subWinName);
            var subWinList = window.SubWinList;
            var isOpen = subWinList.CheckSubWinIsOpen(subWinName);
            Assert.True(isOpen);
            await Task.Delay(5000);
            subWinList.CloseAllSubWins();
            isOpen = subWinList.CheckSubWinIsOpen(subWinName);
            Assert.False(isOpen);
        }

        [Theory(DisplayName = "测试判断子窗口是否存在，如果不存在，则打开")]
        [InlineData(".NET-AI实时快讯3群")]
        public async Task TestCheckSubWinExistAndOpen(string subWinName)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var subWinList = window.SubWinList;
            await subWinList.CheckSubWinExistAndOpen(subWinName);
            _output.WriteLine($"子窗口{subWinName}已打开");

            Assert.True(true);
        }


        [Theory(DisplayName = "测试监听子窗口")]
        [InlineData(".NET-AI实时快讯3群")]
        public async Task TestCheckSubWinMonitor(string subWinName)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var subWinList = window.SubWinList;
            subWinList.RegisterMonitorSubWin(subWinName);

            Assert.True(true);
            await Task.Delay(50000);
        }
    }
}
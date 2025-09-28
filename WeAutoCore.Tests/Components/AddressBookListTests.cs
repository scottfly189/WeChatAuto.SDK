using FlaUI.Core.AutomationElements;
using WxAutoCore.Components;
using Xunit;
using Xunit.Abstractions;
using WxAutoCommon.Models;
using WxAutoCommon.Configs;

namespace WxAutoCore.Tests.Components
{
    [Collection("UiTestCollection")]
    public class AddressBookListTests
    {
        private readonly string _wxClientName = WeChatConfig.TestClientName;
        private readonly ITestOutputHelper _output;
        private UiTestFixture _globalFixture;
        public AddressBookListTests(ITestOutputHelper output, UiTestFixture globalFixture)
        {
            _output = output;
            _globalFixture = globalFixture;
        }

        [Fact(DisplayName = "测试获取所有好友")]
        public void TestGetAllFriends()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var addressBookList = window.AddressBook;
            var friends = addressBookList.GetAllFriends();
            foreach (var friend in friends)
            {
                _output.WriteLine(friend);
            }
            Assert.True(friends.Count > 0);
        }
        [Fact(DisplayName = "测试定位好友")]
        public void TestLocateFriend()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var addressBookList = window.AddressBook;
            var result = addressBookList.LocateFriend("陈建华");
            Assert.True(result);
        }

        [Fact(DisplayName = "测试获取所有公众号")]
        public void TestGetAllOfficialAccount()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var addressBookList = window.AddressBook;
            var officialAccounts = addressBookList.GetAllOfficialAccount();
            foreach (var officialAccount in officialAccounts)
            {
                _output.WriteLine(officialAccount);
            }
            Assert.True(officialAccounts.Count > 0);
        }

        [Fact(DisplayName = "测试获取所有待添加好友，不加关键字")]
        public void TestGetAllWillAddFriends()
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var addressBookList = window.AddressBook;
            var willAddFriends = addressBookList.GetAllWillAddFriends();
            foreach (var willAddFriend in willAddFriends)
            {
                _output.WriteLine(willAddFriend);
            }
            Assert.True(true);
        }
        
        [Theory(DisplayName = "测试获取所有待添加好友，加关键字")]
        [InlineData("test")]
        public void TestGetAllWillAddFriendsWithKeyWord(string keyWord)
        {
            var framework = _globalFixture.wxFramwork;
            var client = framework.GetWxClient(_wxClientName);
            var window = client.WxMainWindow;
            var addressBookList = window.AddressBook;
            var willAddFriends = addressBookList.GetAllWillAddFriends(keyWord);
            foreach (var willAddFriend in willAddFriends)
            {
                _output.WriteLine(willAddFriend);
            }
            Assert.True(true);
        }
    }
}
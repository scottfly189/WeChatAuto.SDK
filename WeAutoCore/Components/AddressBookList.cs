using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 通讯录列表
    /// </summary>
    public class AddressBookList
    {
        private UIThreadInvoker _uiThreadInvoker;
        private Window _Window;
        private WeChatMainWindow _MainWin;
        public AddressBookList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
            _Window = window;
            _MainWin = wxWindow;
        }
        /// <summary>
        /// 获取所有好友
        /// </summary>
        /// <returns>好友列表</returns>
        public List<string> GetAllFriends()
        {
            _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.通讯录);
            try
            {
                var result = _GetAllFriendsCore();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取通讯录发生错误:" + ex.ToString());
                throw;
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.聊天);
            }
        }
        public bool LocateFriend(string friendName)
        {
            _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.通讯录);
            bool result = _uiThreadInvoker.Run(automation =>
            {
                bool existTag = false;
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var scrollPattern = root.Patterns.Scroll.Pattern;
                if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                {
                    for (double p = 0; p <= 1; p += scrollPattern.VerticalViewSize)
                    {
                        scrollPattern.SetScrollPercent(0, p);
                        Thread.Sleep(600);
                        var fList = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()
                        .Where(item => !item.Name.Contains("公众号") && !(item.Name == "新的朋友"))
                        .Where(item => !string.IsNullOrWhiteSpace(item.Name.Trim()))
                        .Select(item => item.Name.Trim())
                        .ToList();
                        if (fList.Contains(friendName))
                        {
                            existTag = true;
                            break;
                        }
                    }
                }
                else
                {
                    var fList = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()
                    .Where(item => !item.Name.Contains("公众号") && !(item.Name == "新的朋友"))
                    .Where(item => !string.IsNullOrWhiteSpace(item.Name.Trim()))
                    .Select(item => item.Name.Trim())
                    .ToList();
                    var list = fList.GroupBy(item => item).Select(item => item.Key).ToList();
                    if (list.Contains(friendName))
                    {
                        existTag = true;
                    }
                }

                return existTag;
            }).Result;
            return result;
        }
        private List<string> _GetAllFriendsCore()
        {
            List<string> result = _uiThreadInvoker.Run(automation =>
            {
                var list = new List<string>();
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var scrollPattern = root.Patterns.Scroll.Pattern;
                if (scrollPattern != null && scrollPattern.VerticallyScrollable)
                {
                    for (double p = 0; p <= 1; p += scrollPattern.VerticalViewSize)
                    {
                        scrollPattern.SetScrollPercent(0, p);
                        Thread.Sleep(600);
                        var fList = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()
                        .Where(item => !item.Name.Contains("公众号") && !(item.Name == "新的朋友"))
                        .Where(item => !string.IsNullOrWhiteSpace(item.Name.Trim()))
                        .Select(item => item.Name.Trim())
                        .ToList();
                        list.AddRange(fList.Except(list));
                    }
                }
                else
                {
                    var fList = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList()
                      .Where(item => !item.Name.Contains("公众号") && !(item.Name == "新的朋友"))
                      .Where(item => !string.IsNullOrWhiteSpace(item.Name.Trim()))
                      .Select(item => item.Name.Trim())
                      .ToList();
                    list = fList.GroupBy(item => item).Select(item => item.Key).ToList();
                }

                return list;
            }).Result;
            return result;
        }
        /// <summary>
        /// 获取所有公众号
        /// </summary>
        /// <returns>公众号列表</returns>
        public List<string> GetAllOfficialAccount()
        {
            return new List<string>();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;
using WxAutoCore.Extentions;
using WxAutoCore.Utils;

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
        /// <summary>
        /// 定位好友
        /// </summary>
        /// <param name="friendName">好友名称</param>
        /// <returns>是否存在</returns>
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
                        .Where(item => !(item.Name == "新的朋友"))
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
                    .Where(item => !(item.Name == "新的朋友"))
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
        /// <summary>
        /// 获取所有公众号
        /// </summary>
        /// <returns>公众号列表</returns>
        public List<string> GetAllOfficialAccount()
        {
            var result = this.LocateFriend("公众号");
            List<string> list = null;
            try
            {
                if (result)
                {
                    list = _GetAllOfficialAccountCore();
                }
                else
                {
                    throw new Exception("公众号不存在");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取公众号发生错误:" + ex.ToString());
                throw new Exception("获取公众号发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.通讯录);
            }
            return list;
        }
        /// <summary>
        /// 获取所有待添加好友
        /// </summary>
        /// <param name="keyWord">关键字,如果设置关键字，则返回包含关键字的新好友，如果没有设置，则返回所有新好友</param>
        /// <returns>待添加好友昵称列表</returns>
        public List<string> GetAllWillAddFriends(string keyWord = null)
        {
            _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.通讯录);
            try
            {
                List<string> list = _GetAllWillAddFriendsCore(keyWord);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取待添加好友发生错误:" + ex.ToString());
                throw new Exception("获取待添加好友发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.聊天);
            }
        }

        /// <summary>
        /// 通过新好友
        /// </summary>
        /// <param name="keyWord">关键字,如果设置关键字，则通过包含关键字的新好友，如果没有设置，则通过所有新好友</param>
        /// <param name="suffix">后缀,如果设置后缀，则在此好友昵称后添加后缀</param>
        /// <param name="label">用户标签</param>
        /// <returns>通过的新好友昵称列表</returns>
        public List<string> PassedAllNewFriend(string keyWord = null, string suffix = null, string label = null)
        {
            _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.通讯录);
            try
            {
                List<string> list = _PassedAllNewFriendCore(keyWord, suffix, label);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("添加好友发生错误:" + ex.ToString());
                throw new Exception("添加好友发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(WxAutoCommon.Enums.NavigationType.聊天);
            }
        }
        /// <summary>
        /// 通过所有新好友的核心方法
        /// </summary>
        /// <param name="keyWord">关键字,如果设置关键字，则返回包含关键字的新好友，如果没有设置，则返回所有新好友</param>
        /// <param name="suffix">后缀,如果设置后缀，则在此好友昵称后添加后缀</param>
        /// <param name="label">用户标签</param>
        /// <returns></returns>
        private List<string> _PassedAllNewFriendCore(string keyWord = null, string suffix = null, string label = null)
        {
            List<string> list = _uiThreadInvoker.Run(automation =>
            {
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var scrollPattern = root.Patterns.Scroll.Pattern;
                scrollPattern.SetScrollPercent(0, 0);
                var rList = new List<string>();
                var newFriendItem = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("新的朋友"))).AsListBoxItem();
                var button = newFriendItem.FindFirstByXPath("//Button[@Name='ContactListItem'][@IsOffscreen='false']").AsButton();
                button.WaitUntilClickable(TimeSpan.FromSeconds(5));
                button.Click();
                Thread.Sleep(600);
                var panelRoot = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='新的朋友'][@IsOffscreen='false']")?.AsListBox();
                var subList = panelRoot?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                foreach (var item in subList)
                {
                    button = item.FindFirstByXPath("//Button[@Name='接受'][@IsOffscreen='false']").AsButton();

                    if (button != null)
                    {
                        if (string.IsNullOrEmpty(keyWord))
                        {
                            var buttonName = this._ConfirmFriend(button, suffix, label);
                            this._SwitchFriend(root);
                            rList.Add(buttonName);
                        }
                        else
                        {
                            var buttonName = item.FindFirstByXPath("(//Button)[1]").AsButton().Name.Trim();
                            var labels = item.FindAllDescendants(cf => cf.ByControlType(ControlType.Text)).ToList();
                            var query = labels.FirstOrDefault(u => u.Name.Contains(keyWord));
                            if (query != null)
                            {
                                buttonName = this._ConfirmFriend(button, suffix, label);
                                this._SwitchFriend(root);
                                rList.Add(buttonName);
                            }
                        }
                    }
                }
                return rList;
            }).Result;
            return list;
        }
        private void _SwitchFriend(AutomationElement root)
        {
            var topItems = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
            var topItem = topItems.FirstOrDefault(u => !(u.Name == "新的朋友") && !(u.Name == ""));
            var button = topItem.FindFirstByXPath("//Button[1]").AsButton();
            button.WaitUntilClickable(TimeSpan.FromSeconds(5));
            button.Click();
            Thread.Sleep(600);
            var newFriendItem = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("新的朋友"))).AsListBoxItem();
            button = newFriendItem.FindFirstByXPath("//Button[@Name='ContactListItem'][@IsOffscreen='false']").AsButton();
            button.WaitUntilClickable(TimeSpan.FromSeconds(5));
            button.Click();
            Thread.Sleep(600);
        }
        /// <summary>
        /// 确认好友
        /// </summary>
        /// <param name="button">按钮</param>
        /// <param name="suffix">后缀</param>
        /// <param name="label">用户标签</param>
        private string _ConfirmFriend(AutomationElement button, string suffix = null, string label = null)
        {
            var buttonName = "";
            button.WaitUntilClickable(TimeSpan.FromSeconds(5));
            button.Click();
            Thread.Sleep(600);
            var confirmWindow = Retry.WhileNull(() => _Window.FindFirstByXPath("/Window[@Name='通过朋友验证'][@IsOffscreen='false']")?.AsWindow(),
                timeout: TimeSpan.FromSeconds(5)
            ).Result;
            if (confirmWindow != null)
            {
                var memoNameEdit = confirmWindow.FindFirstByXPath("/Pane[2]/Pane[1]/Pane/Pane/Pane[1]/Pane/Edit").AsTextBox();
                memoNameEdit.Focus();
                memoNameEdit.Click();
                buttonName = memoNameEdit.Name.Trim();
                //处理后缀
                if (!string.IsNullOrEmpty(suffix))
                {
                    var value = memoNameEdit.Name + "_" + suffix;
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(value);
                    buttonName = value;
                }
                //处理标签
                if (!string.IsNullOrEmpty(label))
                {
                    var labelEdit = confirmWindow.FindFirstByXPath("/Pane[2]/Pane[1]/Pane/Pane/Pane[2]/Pane[1]/Pane/Edit").AsTextBox();
                    labelEdit.Focus();
                    labelEdit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(label);
                    labelEdit.Click();
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                }

                var confirmButton = confirmWindow.FindFirstByXPath("//Button[@Name='确定'][@IsOffscreen='false']").AsButton();
                confirmButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                confirmButton.Click();
                Thread.Sleep(600);
            }
            return buttonName;
        }

        /// <summary>
        /// 获取所有待添加好友的核心方法
        /// </summary>
        /// <returns>待添加好友昵称列表</returns>
        /// <param name="keyWord">关键字,如果设置关键字，则返回包含关键字的新好友，如果没有设置，则返回所有新好友</param>
        private List<string> _GetAllWillAddFriendsCore(string keyWord = null)
        {
            List<string> list = _uiThreadInvoker.Run(automation =>
            {
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var scrollPattern = root.Patterns.Scroll.Pattern;
                scrollPattern.SetScrollPercent(0, 0);
                var rList = new List<string>();
                var newFriendItem = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("新的朋友"))).AsListBoxItem();
                var button = newFriendItem.FindFirstByXPath("//Button[@Name='ContactListItem'][@IsOffscreen='false']").AsButton();
                button.WaitUntilClickable(TimeSpan.FromSeconds(5));
                button.Click();
                Thread.Sleep(600);
                var panelRoot = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='新的朋友'][@IsOffscreen='false']")?.AsListBox();
                var subList = panelRoot?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                foreach (var item in subList)
                {
                    button = item.FindFirstByXPath("//Button[@Name='接受'][@IsOffscreen='false']").AsButton();
                    if (button != null)
                    {
                        if (string.IsNullOrEmpty(keyWord))
                        {
                            button = item.FindFirstByXPath("(//Button)[1]").AsButton();
                            rList.Add(button.Name.Trim());
                        }
                        else
                        {
                            var labels = item.FindAllDescendants(cf => cf.ByControlType(ControlType.Text)).ToList();
                            var query = labels.FirstOrDefault(u => u.Name.Contains(keyWord));
                            if (query != null)
                            {
                                button = item.FindFirstByXPath("(//Button)[1]").AsButton();
                                rList.Add(button.Name.Trim());
                            }
                        }
                    }
                }

                return rList;
            }).Result;
            return list;
        }

        /// <summary>
        /// 移除单个好友
        /// </summary>
        /// <param name="friendName">好友名称</param>
        /// <returns>是否成功</returns>
        public bool RemoveFriend(string friendName)
        {
            return false;
        }
        /// <summary>
        /// 移除特定后缀好友
        /// </summary>
        /// <param name="suffix">后缀</param>
        /// <returns>是否成功</returns>
        public bool RemoveSuffixFriend(string suffix)
        {
            return false;
        }
        /// <summary>
        /// 获取所有好友
        /// </summary>
        /// <returns>好友列表</returns>
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
        /// 获取所有公众号的核心方法    
        /// </summary>
        /// <returns></returns>
        private List<string> _GetAllOfficialAccountCore()
        {
            List<string> result = _uiThreadInvoker.Run(automation =>
            {
                var list = new List<string>();
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var item = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("公众号")));
                if (item != null)
                {
                    var button = item.FindFirstByXPath("//Button[@Name='ContactListItem'][@IsOffscreen='false']").AsButton();
                    button?.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    button?.Click();
                    Thread.Sleep(600);
                    var panelRoot = _Window.FindFirstByXPath("/Pane[2]/Pane/Pane[2]/Pane/Pane/Pane/Pane[2]");
                    list = panelRoot?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem))
                        .Where(u => !u.Name.Equals("该账号已冻结")).ToList().Select(u => u.Name.Trim()).ToList();
                }

                return list;
            }).Result;
            return result;
        }
    }

}
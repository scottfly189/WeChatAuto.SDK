using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using WeAutoCommon.Enums;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 通讯录列表
    /// </summary>
    public class AddressBookList
    {
        private readonly IServiceProvider _serviceProvider;
        private UIThreadInvoker _uiMainThreadInvoker;
        private Window _Window;
        private AutoLogger<AddressBookList> _logger;
        private WeChatMainWindow _MainWin;
        public AddressBookList(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<AddressBookList>>();
            _uiMainThreadInvoker = uiThreadInvoker;
            _Window = window;
            _MainWin = wxWindow;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获取所有好友
        /// </summary>
        /// <returns>好友列表</returns>
        public List<string> GetAllFriends()
        {
            _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
            try
            {
                var result = _GetAllFriendsCore();
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("获取通讯录发生错误:" + ex.ToString());
                throw;
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(WeAutoCommon.Enums.NavigationType.聊天);
            }
        }
        /// <summary>
        /// 定位好友
        /// </summary>
        /// <param name="friendName">好友昵称</param>
        /// <returns>是否存在</returns>
        public bool LocateFriend(string friendName)
        {
            _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
            bool result = _uiMainThreadInvoker.Run(automation =>
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
            }).GetAwaiter().GetResult();
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
                Trace.WriteLine("获取公众号发生错误:" + ex.ToString());
                _logger.Error("获取公众号发生错误:" + ex.ToString());
                _logger.Error(ex.StackTrace);
                throw new Exception("获取公众号发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
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
            _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
            try
            {
                List<string> list = _GetAllWillAddFriendsCore(keyWord);
                return list;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("获取待添加好友发生错误:" + ex.ToString());
                _logger.Error("获取待添加好友发生错误:" + ex.ToString());
                _logger.Error(ex.StackTrace);
                throw new Exception("获取待添加好友发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(NavigationType.聊天);
            }
        }

        /// <summary>
        /// 通过新好友
        /// </summary>
        /// <param name="keyWord">关键字,如果设置关键字，则通过包含关键字的新好友，如果没有设置，则通过所有新好友</param>
        /// <param name="suffix">后缀,如果设置后缀，则在此好友昵称后添加后缀</param>
        /// <param name="label">好友标签</param>
        /// <param name="isDelet">添加好友成功后是否删除好友申请按钮，默认删除</param>
        /// <returns>通过的新好友昵称列表</returns>
        public List<string> PassedAllNewFriend(string keyWord = null, string suffix = null, string label = null, bool isDelet = true)
        {
            _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
            try
            {
                List<string> list = _PassedAllNewFriendCore(keyWord, suffix, label, isDelet);
                _RetrySwitchNewFriend();
                return list;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("添加好友发生错误:" + ex.ToString());
                _logger.Error("添加好友发生错误:" + ex.ToString());
                _logger.Error(ex.StackTrace);
                throw new Exception("添加好友发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(NavigationType.聊天);
            }
        }
        /// <summary>
        /// 重试切换到新的朋友页面,目的是去除通知图标
        /// </summary>
        private void _RetrySwitchNewFriend()
        {
            RandomWait.Wait(1000, 3000);
            _uiMainThreadInvoker.Run(automation =>
            {
                var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
                var scrollPattern = root.Patterns.Scroll.Pattern;
                scrollPattern.SetScrollPercent(0, 0);
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName("新的朋友").Not())).ToList();
                var firstFriendItem = items.First(u => u.Name != "" && u.Name != null && u.Name != "新的朋友");
                if (firstFriendItem != null)
                {
                    firstFriendItem.DrawHighlightExt();
                    firstFriendItem.Click();
                }
                RandomWait.Wait(500, 3000);
                var newFriendItem = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("新的朋友"))).AsListBoxItem();
                if (newFriendItem != null)
                {
                    newFriendItem.DrawHighlightExt();
                    newFriendItem.Click();
                }
                RandomWait.Wait(500, 3000);
            }).GetAwaiter().GetResult();
        }
        /// <summary>
        /// 移除好友
        /// 注意： 如果删除好友，从通讯录删除好友后，同步的，应该将监听删除
        /// </summary>
        /// <param name="nickName">好友昵称</param>
        /// <returns>是否成功</returns>
        public bool RemoveFriend(string nickName)
        {
            try
            {
                var result = this.LocateFriend(nickName);
                if (result)
                {
                    _MainWin.StopMessageListener(nickName);    //停止消息监听
                    result = _uiMainThreadInvoker.Run(automation =>
                    {
                        var resultTag = _RemoveFriendCore(nickName);
                        //点击新的朋友按钮 - 复原 - 以利于下次操作
                        _SwitchToNewFriend();
                        return resultTag;
                    }).GetAwaiter().GetResult();
                }
                else
                {
                    return false;
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("删除好友发生错误:" + ex.ToString());
                _logger.Error("删除好友发生错误:" + ex.ToString());
                _logger.Error(ex.StackTrace);
                return false;
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(NavigationType.聊天);
            }
        }
        /// <summary>
        /// 切换到 新的朋友 页面
        /// </summary>
        private void _SwitchToNewFriend()
        {
            var root = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
            var scrollPattern = root.Patterns.Scroll.Pattern;
            scrollPattern.SetScrollPercent(0, 0);
            var rList = new List<string>();
            var newFriendItem = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByText("新的朋友"))).AsListBoxItem();
            var button = newFriendItem.FindFirstByXPath("//Button[@Name='ContactListItem'][@IsOffscreen='false']").AsButton();
            button.WaitUntilClickable(TimeSpan.FromSeconds(5));
            button.Click();
        }

        /// <summary>
        /// 删除好友核心方法
        /// </summary>
        /// <param name="nickName">好友昵称</param>
        /// <returns>是否成功</returns>
        private bool _RemoveFriendCore(string nickName)
        {
            var listBox = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/List[@Name='联系人'][@IsOffscreen='false']")?.AsListBox();
            var listItems = listBox.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName(nickName))).AsListBoxItem();
            if (listItems != null)
            {
                listItems.Focus();
                listItems.Click();
                Thread.Sleep(600);
                var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='更多']";
                var moreButton = _Window.FindFirstByXPath(xPath)?.AsButton();
                if (moreButton != null)
                {
                    moreButton.Focus();
                    moreButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    moreButton.Click();
                    Thread.Sleep(600);
                    var menu = _Window.FindFirstChild(cf => cf.Menu()).AsMenu();
                    if (menu != null)
                    {
                        var deleItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("删除联系人"))).AsListBoxItem();
                        if (deleItem != null)
                        {
                            deleItem.Focus();
                            deleItem.WaitUntilClickable(TimeSpan.FromSeconds(5));
                            deleItem.Click();
                            Thread.Sleep(600);
                            var conformButton = _Window.FindFirstByXPath("/Pane[1]/Pane/Pane/Button[@Name='删除']")?.AsButton();
                            conformButton.Focus();
                            conformButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                            conformButton.Click();

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="friendNames">微信号/手机号列表</param>
        /// <param name="label">好友标签</param>
        /// <returns>好友昵称列表和是否成功</returns>
        public List<(string friendName, bool isSuccess, string errMessage)> AddFriends(List<string> friendNames, string label = "")
        {
            List<(string friendName, bool isSuccess, string errMessage)> resultList = new List<(string friendName, bool isSuccess, string errMessage)>();
            _MainWin.Navigation.SwitchNavigation(NavigationType.通讯录);
            try
            {
                resultList = _uiMainThreadInvoker.Run(automation =>
                {
                    var rList = new List<(string friendName, bool isSuccess, string errMessage)>();
                    var sButton = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Button[@Name='添加朋友'][@IsOffscreen='false']")?.AsButton();
                    if (sButton == null)
                    {
                        return rList;
                    }
                    sButton.Focus();
                    sButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                    sButton.Click();
                    Thread.Sleep(600);
                    var xPath = "";
                    var cancelButton = Retry.WhileNull(() =>
                    {
                        xPath = "/Pane/Pane/Pane/Pane/Button[@Name='取消'][@IsOffscreen='false']";
                        return _Window.FindFirstByXPath(xPath)?.AsButton();
                    }, TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200));
                    var searchEdit = Retry.WhileNull(() =>
                    {
                        xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='微信号/手机号']";
                        return _Window.FindFirstByXPath(xPath)?.AsTextBox();
                    }, TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200)).Result;

                    if (cancelButton == null || searchEdit == null)
                    {
                        return rList;
                    }

                    foreach (var item in friendNames)
                    {
                        searchEdit.Focus();
                        searchEdit.Click();
                        Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                        Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                        Keyboard.Type(item);
                        Keyboard.Press(VirtualKeyShort.RETURN);
                        Thread.Sleep(300);
                        xPath = $"/Pane/Pane/Pane/Pane/Pane/List/ListItem[@Name='搜索：{item}']";
                        var listItem = _Window.FindFirstByXPath(xPath)?.AsListBoxItem();
                        if (listItem != null)
                        {
                            listItem.Focus();
                            listItem.Click();
                            var deskTop = automation.GetDesktop();
                            var paneResult = Retry.WhileNull(() =>
                            {
                                return deskTop.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByProcessId(_MainWin.ProcessId)).
                                    And(cf.ByClassName("ContactProfileWnd")));
                            }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                            if (paneResult.Success)
                            {
                                //打开弹窗
                                var pane = paneResult.Result;
                                var nickName = pane.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text))?.AsLabel()?.Name?.Trim();
                                xPath = "//Button[@Name='添加到通讯录'][1]";
                                var addToAddressBookButton = pane.FindFirstByXPath(xPath)?.AsButton();
                                if (addToAddressBookButton != null)
                                {
                                    addToAddressBookButton.Focus();
                                    addToAddressBookButton.Click();
                                    Thread.Sleep(600);
                                    var dialog = Retry.WhileNull(() =>
                                    {
                                        return _Window.FindFirstByXPath("/Window[@Name='添加朋友请求'][@IsOffscreen='false']")?.AsWindow();
                                    }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                                    if (dialog.Success)
                                    {
                                        if (!String.IsNullOrWhiteSpace(label))
                                        {
                                            xPath = "/Pane[2]/Pane[1]/Pane/Pane/Pane[3]/Pane[1]/Pane/Edit";
                                            var labelEdit = dialog.Result.FindFirstByXPath(xPath)?.AsTextBox();
                                            if (labelEdit != null)
                                            {
                                                labelEdit.Focus();
                                                labelEdit.Click();
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                                                Keyboard.Type(label);
                                                labelEdit.Click();
                                                Keyboard.Press(VirtualKeyShort.RETURN);
                                            }
                                        }
                                        xPath = "//Button[@Name='确定']";
                                        var confirmButton = dialog.Result.FindFirstByXPath(xPath)?.AsButton();
                                        if (confirmButton != null)
                                        {
                                            confirmButton.Focus();
                                            confirmButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                                            confirmButton.Click();
                                            Thread.Sleep(600);
                                            rList.Add((item, true, "添加成功，待对方验证"));
                                        }
                                        else
                                        {
                                            rList.Add((item, true, "可能以前有添加过"));
                                        }
                                    }
                                    else
                                    {
                                        rList.Add((item, true, "此好友可能以前添加过，所以直接通过"));
                                    }
                                }
                                else
                                {
                                    rList.Add((item, false, "此好友可能已经添加过"));
                                }
                            }
                            else
                            {
                                //增加好友可能腾迅返回问题,如：手机号或者微信号无效、不存在等问题
                                xPath = "/Pane[2]/Pane/Pane[1]/Pane[2]/Pane/List";
                                var listBox = _Window.FindFirstByXPath(xPath)?.AsListBox();
                                var firstChild = listBox.FindFirstChild()?.AsListBoxItem();
                                if (firstChild != null)
                                {
                                    if (!firstChild.Name.StartsWith("搜索"))
                                    {
                                        var err = firstChild.Name.Trim();
                                        rList.Add((item, false, err));
                                    }
                                    else
                                    {
                                        rList.Add((item, false, "发生未知错误"));
                                    }
                                }
                                else
                                {
                                    rList.Add((item, false, "发生未知错误"));
                                }
                            }
                        }
                    }

                    return rList;
                }).GetAwaiter().GetResult();

                return resultList;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("添加好友发生错误:" + ex.ToString());
                _logger.Error("添加好友发生错误:" + ex.ToString());
                _logger.Error(ex.StackTrace);
                throw new Exception("添加好友发生错误:" + ex.ToString());
            }
            finally
            {
                _MainWin.Navigation.SwitchNavigation(NavigationType.聊天);
            }
        }
        /// <summary>
        /// 添加好友
        /// 注意：不能添加太频繁，否则可能会触发微信的风控机制，导致加好友失败
        /// </summary>
        /// <param name="friendName">微信号/手机号</param>
        /// <param name="label">好友标签</param>
        /// <returns>是否成功</returns>
        public bool AddFriend(string friendName, string label = "")
        {
            return this.AddFriends(new List<string> { friendName }, label).FirstOrDefault(u => u.friendName == friendName).isSuccess;
        }

        /// <summary>
        /// 通过所有新好友的核心方法
        /// </summary>
        /// <param name="keyWord">关键字,如果设置关键字，则返回包含关键字的新好友，如果没有设置，则返回所有新好友</param>
        /// <param name="suffix">后缀,如果设置后缀，则在此好友昵称后添加后缀</param>
        /// <param name="label">好友标签</param>
        /// <param name="isDelet">添加好友成功后是否删除好友申请按钮，默认删除</param>
        /// <returns></returns>
        private List<string> _PassedAllNewFriendCore(string keyWord = null, string suffix = null, string label = null, bool isDelet = true)
        {
            List<string> list = _uiMainThreadInvoker.Run(automation =>
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
                _PreProcessUserDetailPage();
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
                            //这里加一个判断，如果isDelet为true，则删除好友申请按钮
                            this._DeleteFriendRequestButton(isDelet, item.Name);
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
                                //这里加一个判断，如果isDelet为true，则删除好友申请按钮
                                this._DeleteFriendRequestButton(isDelet, item.Name);
                                this._SwitchFriend(root);
                                rList.Add(buttonName);
                            }
                        }
                    }
                }
                return rList;
            }).GetAwaiter().GetResult();
            return list;
        }
        private void _DeleteFriendRequestButton(bool isDelet, string buttonName)
        {
            if (!isDelet)
                return;
            RandomWait.Wait(3000, 8000);
            var panelRoot = _Window.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='新的朋友'][@IsOffscreen='false']")?.AsListBox();
            var subList = panelRoot?.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
            var items = subList?.Where(u => u.Name == buttonName).ToList();
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    var button = item.FindFirstByXPath("//Button[@Name='接受'][@IsOffscreen='false']")?.AsButton();
                    if (button == null)
                    {
                        item.RightClick();
                        RandomWait.Wait(500, 2000);
                        var menuRetry = Retry.WhileNull(() => _Window.FindFirstChild(cf => cf.Menu()).AsMenu(), TimeSpan.FromSeconds(3), TimeSpan.FromMilliseconds(200));
                        if (menuRetry.Success)
                        {
                            var menu = menuRetry.Result;
                            var menuItem = menu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("删除"))).AsListBoxItem();
                            if (menuItem != null)
                            {
                                menuItem.Click();
                                RandomWait.Wait(500, 2000);
                            }
                        }
                    }

                }
            }
            else
            {
                _logger.Debug($"找不到好友申请按钮: {buttonName}");
            }
        }
        /// <summary>
        /// 预处理用户详情页面
        /// 有时候可能用户点击了详情页，导致监听新用户申请失效
        /// </summary>
        private void _PreProcessUserDetailPage()
        {
            var xPath = "/Pane[2]/Pane/Pane[2]/Pane/Pane[1]/Button | /Pane[2]/Pane/Pane[2]/Pane/Pane/Pane[1]/Button";
            var button = _Window.FindFirstByXPath(xPath)?.AsButton();
            if (button != null)
            {
                button.Click();
            }
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
        /// <param name="label">好友标签</param>
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
                    RandomWait.Wait(800, 3000);
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
                    RandomWait.Wait(800, 3000);
                }

                var confirmButton = confirmWindow.FindFirstByXPath("//Button[@Name='确定'][@IsOffscreen='false']").AsButton();
                confirmButton.WaitUntilClickable(TimeSpan.FromSeconds(5));
                confirmButton.Click();
                RandomWait.Wait(800, 3000);
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
            List<string> list = _uiMainThreadInvoker.Run(automation =>
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
            }).GetAwaiter().GetResult();
            return list;
        }


        /// <summary>
        /// 获取所有好友
        /// </summary>
        /// <returns>好友列表</returns>
        private List<string> _GetAllFriendsCore()
        {
            List<string> result = _uiMainThreadInvoker.Run(automation =>
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
            }).GetAwaiter().GetResult();
            return result;
        }
        /// <summary>
        /// 获取所有公众号的核心方法    
        /// </summary>
        /// <returns></returns>
        private List<string> _GetAllOfficialAccountCore()
        {
            List<string> result = _uiMainThreadInvoker.Run(automation =>
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
            }).GetAwaiter().GetResult();
            return result;
        }
    }

}
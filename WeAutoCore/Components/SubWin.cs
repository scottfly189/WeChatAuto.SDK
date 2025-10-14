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
using OneOf;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;
using WxAutoCore.Extentions;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private ChatContent _ChatContent;
        private WeChatMainWindow _MainWxWindow;    //主窗口对象
        private Window _SelfWindow;        //子窗口FlaUI的window
        private int _ProcessId;
        private UIThreadInvoker _uiThreadInvoker;
        private SubWinList _SubWinList;
        private ChatBody _ChatBodyCache;
        public Window SelfWindow { get => _SelfWindow; set => _SelfWindow = value; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public ChatContent ChatContent => _ChatContent;
        public int ProcessId => _ProcessId;
        private volatile bool _disposed = false;


        /// <summary>
        /// 子窗口构造函数
        /// </summary>
        /// <param name="window">子窗口FlaUI的window</param>
        /// <param name="wxWindow">主窗口的微信窗口对象</param>
        /// <param name="subWinList">子窗口列表</param>
        /// <param name="serviceProvider">服务提供者</param>
        public SubWin(Window window, WeChatMainWindow wxWindow, UIThreadInvoker uiThreadInvoker, string title, SubWinList subWinList, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiThreadInvoker = uiThreadInvoker;
            _SubWinList = subWinList;
            NickName = title;
            _SelfWindow = window;
            _MainWxWindow = wxWindow;
            _ChatContent = new ChatContent(_SelfWindow, ChatContentType.SubWindow, "/Pane[2]/Pane/Pane[2]/Pane/Pane", this, uiThreadInvoker, this._MainWxWindow, _serviceProvider);
            _ProcessId = _SelfWindow.Properties.ProcessId.Value;
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="callBack"></param>
        public void AddMessageListener(Action<List<MessageBubble>, List<MessageBubble>, Sender, WeChatMainWindow, WeChatFramwork, IServiceProvider> callBack)
        {
            if (_disposed)
            {
                return;
            }
            if (_ChatBodyCache == null)
            {
                _ChatBodyCache = _ChatContent.ChatBody;
            }
            _ChatBodyCache.AddListener(callBack);
        }
        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopListener()
        {
            if (_disposed)
            {
                return;
            }

            _ChatBodyCache?.StopListener();
        }

        #region 群聊操作

        #region 群基础操作，适用于自有群与他有群
        /// <summary>
        /// 更新群聊选项
        /// </summary>
        /// <param name="action">更新群聊选项的Action</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse UpdateChatGroupOptions(Action<ChatGroupOptions> action)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                if (!_IsSidebarOpen())
                {
                    _OpenSidebar();
                }
                Thread.Sleep(500);
                _FocuseSearchText();
                ChatGroupOptions options = _InitChatGroupOptions();
                options.Reset();
                action(options);
                _UpdateChatGroupOptions(options);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
            finally
            {
                //关闭侧边栏，只需要点击一下sender.
                _SenderFocus();
            }
        }
        /// <summary>
        /// 发送编辑框聚焦，目的是关闭侧边栏
        /// </summary>
        private void _SenderFocus()
        {
            var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='输入']";
            var edit = _SelfWindow.FindFirstByXPath(xPath)?.AsTextBox();
            if (edit != null)
            {
                edit.Focus();
                edit.WaitUntilClickable();
                _SelfWindow.SilenceClickExt(edit);
            }
        }
        /// <summary>
        /// 初始化群聊选项
        /// </summary>
        /// <returns></returns>
        private ChatGroupOptions _InitChatGroupOptions()
        {
            ChatGroupOptions options = new ChatGroupOptions();
            _uiThreadInvoker.Run(automation =>
            {
                var rootXPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane";
                var root = _SelfWindow.FindFirstByXPath(rootXPath);   //根节点
                var button = root.FindFirstByXPath("/Pane/Pane/Pane/Button[@Name='群聊名称']")?.AsButton();
                _FetchGroupName(options, button);
                var element = button.GetParent().GetParent().GetSibling(1);
                _FetchGroupNotice(options, element);
                element = element.GetSibling(1); //获取群聊备注
                _FetchGroupMemo(options, element);
                element = element.GetSibling(1); //群昵称
                _FetchMyGroupNickName(options, element);
                element = element.GetSibling(2); //是否显示群昵称
                _FetchShowGroupNickName(options, element);
                element = element.GetSibling(1); //是否免打扰
                _FetchNoDisturb(options, element);
                element = element.GetSibling(1); //是否置顶
                _FetchTop(options, element);
                element = element.GetSibling(1); //是否保存至通讯录
                _FetchSaveToAddressBook(options, element);

            }).Wait();
            return options;
        }

        //获取是否保存至通讯录
        private void _FetchSaveToAddressBook(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='保存到通讯录']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.SaveToAddressBook = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否置顶
        private void _FetchTop(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='置顶聊天']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.Top = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否免打扰
        private void _FetchNoDisturb(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='消息免打扰']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.NoDisturb = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取是否显示群昵称
        private void _FetchShowGroupNickName(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//CheckBox[@Name='显示群成员昵称']";
            var checkBox = element.FindFirstByXPath(xPath)?.AsCheckBox();
            options.ShowGroupNickName = checkBox.ToggleState == ToggleState.On ? true : false;
        }
        //获取群聊昵称
        private void _FetchMyGroupNickName(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "//Button[@Name='我在本群的昵称']";
            var button = element.FindFirstByXPath(xPath)?.AsButton();
            if (button.Patterns.Value.IsSupported)
            {
                var pattern = button.Patterns.Value.Pattern;
                options.MyGroupNickName = pattern.Value.Value;
            }
        }
        //获取群聊备注
        private void _FetchGroupMemo(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "/Pane/Button[@Name='备注']";
            var button = element.FindFirstByXPath(xPath)?.AsButton();
            if (button.Patterns.Value.IsSupported)
            {
                var pattern = button.Patterns.Value.Pattern;
                options.GroupMemo = pattern.Value.Value;
                if (!string.IsNullOrWhiteSpace(options.GroupMemo))
                {
                    options.GroupMemo = options.GroupMemo == "群聊的备注仅自己可见" ? "" : options.GroupMemo;
                }
            }
        }
        //获取群聊公告
        private void _FetchGroupNotice(ChatGroupOptions options, AutomationElement element)
        {
            if (element == null)
                return;
            var xPath = "/Pane/Text[@Name='群公告']";
            var text = element.FindFirstByXPath(xPath)?.AsTextBox();
            if (text.Patterns.Value.IsSupported)
            {
                var pattern = text.Patterns.Value.Pattern;
                options.GroupNotice = pattern.Value.Value;
            }
        }

        //获取群聊名称
        private void _FetchGroupName(ChatGroupOptions options, Button button)
        {
            if (button != null)
            {
                if (button.Patterns.Value.IsSupported)
                {
                    var pattern = button.Patterns.Value.Pattern;
                    options.GroupName = pattern.Value.Value;
                }
                else
                {
                    var element = button.GetSibling(-1);
                    if (element != null && element.ControlType == ControlType.Text)
                    {
                        options.GroupName = element.AsLabel().Name;
                    }
                }
            }
        }
        //更新群聊选项
        private void _UpdateChatGroupOptions(ChatGroupOptions options)
        {
            _uiThreadInvoker.Run(automation =>
            {
                if (options.GroupNameChanged)
                {
                    _UpdateGroupName(GetNewElement(), options.GroupName);
                }
                if (options.ShowGroupNickNameChanged)
                {
                    _UpdateShowGroupNickName(GetNewElement(), options.ShowGroupNickName);
                }
                if (options.NoDisturbChanged)
                {
                    _UpdateNoDisturb(GetNewElement(), options.NoDisturb);
                }
                if (options.TopChanged)
                {
                    _UpdateTop(GetNewElement(), options.Top);
                }
                if (options.SaveToAddressBookChanged)
                {
                    _UpdateSaveToAddressBook(GetNewElement(), options.SaveToAddressBook);
                }
                if (options.GroupNoticeChanged)
                {
                    _UpdateGroupNotice(GetNewElement(), options.GroupNotice);
                }
                if (options.MyGroupNickNameChanged)
                {
                    _UpdateMyGroupNickName(GetNewElement(), options.MyGroupNickName);
                }
                if (options.GroupMemoChanged)
                {
                    _UpdateGroupMemo(GetNewElement(), options.GroupMemo);
                }
            });
        }
        private AutomationElement GetNewElement()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            var chatGroupNamePath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='群聊名称']";
            var button = _SelfWindow.FindFirstByXPath(chatGroupNamePath)?.AsButton();
            var element = button.GetParent().GetParent();
            return element;
        }
        //更新群聊名称
        private void _UpdateGroupName(AutomationElement element, string groupName)
        {
            var xPath = "//Button[@Name='群聊名称']";
            var button = element.FindFirstByXPath(xPath)?.AsButton();
            if (button.Patterns.Value.IsSupported)
            {
                //可以修改
                button.Focus();
                button.WaitUntilClickable();
                button.Click();
                Thread.Sleep(300);
                var edit = Retry.WhileNull(() => element.FindFirstByXPath("//Edit")?.AsTextBox(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (edit != null)
                {
                    edit.Focus();
                    edit.WaitUntilClickable();
                    edit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(groupName);
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                }
            }
            else
            {
                //不可以修改
            }
        }
        //更新是否显示群昵称
        private void _UpdateShowGroupNickName(AutomationElement element, bool showGroupNickName)
        {
            var rootElement = element.GetSibling(5);
            var el = rootElement.FindFirstByXPath("//CheckBox[@Name='显示群成员昵称']")?.AsCheckBox();
            if (el != null)
            {
                el.Focus();
                var result = el.ToggleState == ToggleState.On ? true : false;
                if (result != showGroupNickName)
                {
                    el.ToggleState = showGroupNickName ? ToggleState.On : ToggleState.Off;
                }
            }
        }
        //更新是否免打扰
        private void _UpdateNoDisturb(AutomationElement element, bool noDisturb)
        {
            var rootElement = element.GetSibling(6);
            var el = rootElement.FindFirstByXPath("//CheckBox[@Name='消息免打扰']")?.AsCheckBox();
            if (el != null)
            {
                el.Focus();
                var result = el.ToggleState == ToggleState.On ? true : false;
                if (result != noDisturb)
                {
                    el.ToggleState = noDisturb ? ToggleState.On : ToggleState.Off;
                }
            }
        }
        //更新是否置顶
        private void _UpdateTop(AutomationElement element, bool top)
        {
            var rootElement = element.GetSibling(7);
            var el = rootElement.FindFirstByXPath("//CheckBox[@Name='置顶聊天']")?.AsCheckBox();
            if (el != null)
            {
                el.Focus();
                var result = el.ToggleState == ToggleState.On ? true : false;
                if (result != top)
                {
                    el.ToggleState = top ? ToggleState.On : ToggleState.Off;
                }
            }
        }
        //更新是否保存至通讯录
        private void _UpdateSaveToAddressBook(AutomationElement element, bool saveToAddressBook)
        {
            var rootElement = element.GetSibling(8);
            var el = rootElement.FindFirstByXPath("//CheckBox[@Name='保存到通讯录']")?.AsCheckBox();
            if (el != null)
            {
                el.Focus();
                var result = el.ToggleState == ToggleState.On ? true : false;
                if (result != saveToAddressBook)
                {
                    el.ToggleState = saveToAddressBook ? ToggleState.On : ToggleState.Off;
                }
            }
        }
        //更新群聊公告
        private void _UpdateGroupNotice(AutomationElement element, string groupNotice)
        {
            var rootElement = element.GetSibling(1);
            var el = rootElement.FindFirstByXPath("//Button[@Name='点击编辑群公告']")?.AsButton();
            if (el != null)
            {
                el.Focus();
                el.WaitUntilClickable();
                el.Click();
                Thread.Sleep(300);
                var popWin = Retry.WhileNull(() =>
                {
                    var desktop = el.Automation.GetDesktop();
                    var pWin = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("群公告")).And(cf.ByProcessId(_MainWxWindow.ProcessId)).And(cf.ByClassName("ChatRoomAnnouncementWnd")))?.AsWindow();
                    return pWin;
                }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                if (popWin != null)
                {
                    var editButton = popWin.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Button[@Name='编辑']")?.AsButton();
                    if (editButton != null)
                    {
                        editButton.Focus();
                        editButton.WaitUntilClickable();
                        editButton.Click();

                        var edit = popWin.FindFirstByXPath("//Edit")?.AsTextBox();
                        edit.Focus();
                        edit.WaitUntilClickable();
                        edit.Click();
                        Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                        Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                        Keyboard.Type(groupNotice);
                        Wait.UntilInputIsProcessed();
                        var finishButton = popWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                        finishButton.Focus();
                        finishButton.WaitUntilClickable();
                        finishButton.Click();

                        var sendButton = Retry.WhileNull(() => popWin.FindFirstByXPath("//Button[@Name='发布']")?.AsButton(), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                        if (sendButton != null)
                        {
                            sendButton.Focus();
                            sendButton.WaitUntilClickable();
                            sendButton.Click();
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        //无权限编辑
                        popWin.Close();
                    }
                }

            }

        }
        //更新我自己在群聊中的昵称
        private void _UpdateMyGroupNickName(AutomationElement element, string myGroupNickName)
        {
            var rootElement = element.GetSibling(3);
            var el = rootElement.FindFirstByXPath("//Button[@Name='我在本群的昵称']")?.AsButton();
            if (el != null)
            {
                el.Focus();
                el.WaitUntilClickable();
                el.Click();
                Thread.Sleep(300);
                var edit = Retry.WhileNull(() => element.FindFirstByXPath("//Edit")?.AsTextBox(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (edit != null)
                {
                    edit.Focus();
                    edit.WaitUntilClickable();
                    edit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(myGroupNickName);
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                }
            }
        }
        //更新群聊备注
        private void _UpdateGroupMemo(AutomationElement element, string groupMemo)
        {
            var rootElement = element.GetSibling(2);
            var el = rootElement.FindFirstByXPath("//Button[@Name='备注']")?.AsButton();
            if (el != null)
            {
                el.Focus();
                el.WaitUntilClickable();
                el.Click();
                Thread.Sleep(300);
                var edit = Retry.WhileNull(() => element.FindFirstByXPath("//Edit")?.AsTextBox(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (edit != null)
                {
                    edit.Focus();
                    edit.WaitUntilClickable();
                    edit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(groupMemo);
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                }
            }
        }
        private void _FocuseSearchText()
        {
            var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='搜索群成员']";
            _uiThreadInvoker.Run(automation =>
            {
                var edit = _SelfWindow.FindFirstByXPath(xPath)?.AsTextBox();
                if (edit != null)
                {
                    edit.Focus();
                    _SelfWindow.SilenceClickExt(edit);
                }
            }).Wait();
        }
        /// <summary>
        /// 是否打开侧边栏
        /// </summary>
        /// <returns>是否打开侧边栏</returns>
        private bool _IsSidebarOpen()
        {
            bool result = _uiThreadInvoker.Run(automation =>
            {
                var pane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd"))
                    .And(cf.ByName("SessionChatRoomDetailWnd")));
                return pane != null;
            }).Result;
            return result;
        }
        private void _OpenSidebar()
        {
            var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='聊天信息']";
            _uiThreadInvoker.Run(automation =>
            {
                var button = _SelfWindow.FindFirstByXPath(xPath)?.AsButton();
                if (button != null)
                {
                    button.WaitUntilClickable();
                    button.Focus();
                    button.Click();
                }
            }).Wait();

        }
        /// <summary>
        /// 获取群聊成员列表
        /// </summary>
        /// <returns>群聊成员列表</returns>
        public List<string> GetChatGroupMemberList()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            Thread.Sleep(500);
            List<string> list = _uiThreadInvoker.Run(automation =>
            {
                var memberList = new List<string>();
                var xPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='聊天成员']";
                var listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var rootPane = listBox.GetParent();
                while (true)
                {
                    //反复点击“查看更多”按钮
                    var moreButton = Retry.WhileNull(() =>
                    {
                        return rootPane.FindFirstByXPath("//Button[@Name='查看更多']")?.AsButton();
                    }, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                    if (moreButton.Success)
                    {
                        moreButton.Result.WaitUntilClickable();
                        moreButton.Result.Click();
                        Thread.Sleep(600);
                    }
                    else
                    {
                        break;
                    }
                }
                listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                foreach (var item in items)
                {
                    if (item.Name != "添加" && item.Name != "移除")
                    {
                        memberList.Add(item.Name);
                    }
                }
                return memberList;
            }).Result;
            return list;
        }
        /// <summary>
        /// 搜索群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>成员元素</returns>
        private AutomationElement SearchChatGroupMember(string memberName)
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            var element = _uiThreadInvoker.Run(automation =>
            {
                var edit = _SelfWindow.FindFirstByXPath("/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='搜索群成员']")?.AsTextBox();
                if (edit != null)
                {
                    edit.Focus();
                    edit.WaitUntilClickable();
                    edit.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Keyboard.Type(memberName);
                    Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                    Thread.Sleep(600);
                    var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/ListItem";
                    var list = _SelfWindow.FindAllByXPath(xPath)?.ToList();
                    if (list != null && list.Count > 0)
                    {
                        var item = list.FirstOrDefault(s => s.Name == memberName);
                        return item;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }).Result;

            return element;
        }
        /// <summary>
        /// 是否是群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>是否是群聊成员</returns>
        public bool IsChatGroupMember(string memberName)
        {
            return SearchChatGroupMember(memberName) != null;
        }
        /// <summary>
        /// 是否是自有群
        /// </summary>
        /// <returns>是否是自有群</returns>
        public bool IsOwnerChatGroup()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            Thread.Sleep(500);
            bool result = _uiThreadInvoker.Run(automation =>
            {
                var memberList = new List<string>();
                var xPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='聊天成员']";
                var listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                items = items.Where(item => item.Name != "添加" && item.Name != "移除").ToList();
                var firstItem = items.First();   //群主

                return firstItem.Name == _MainWxWindow.NickName ? true : false;
            }).Result;
            return result;
        }
        /// <summary>
        /// 清空群聊历史
        /// </summary>
        public void ClearChatGroupHistory()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            _uiThreadInvoker.Run(automation =>
            {
                var clearButton = _SelfWindow.FindFirstByXPath("//Button[@Name='清空聊天记录']")?.AsButton();
                if (clearButton != null)
                {
                    clearButton.WaitUntilClickable();
                    clearButton.Focus();
                    clearButton.Click();
                    var confirmButton = Retry.WhileNull(() =>
                    {
                        return _SelfWindow.FindFirstByXPath("/Pane[1]/Pane/Pane/Button[@Name='清空']")?.AsButton();
                    }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                    if (confirmButton != null)
                    {
                        confirmButton.WaitUntilClickable();
                        confirmButton.Focus();
                        confirmButton.Click();
                    }
                }
            }).Wait();
        }
        /// <summary>
        /// 退出群聊
        /// </summary>
        public void QuitChatGroup()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            _uiThreadInvoker.Run(automation =>
            {
                var clearButton = _SelfWindow.FindFirstByXPath("//Button[@Name='退出群聊']")?.AsButton();
                if (clearButton != null)
                {
                    clearButton.WaitUntilClickable();
                    clearButton.Focus();
                    clearButton.Click();
                    var confirmButton = Retry.WhileNull(() =>
                    {
                        return _SelfWindow.FindFirstByXPath("/Pane[1]/Pane/Pane/Button[@Name='退出']")?.AsButton();
                    }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                    if (confirmButton != null)
                    {
                        confirmButton.WaitUntilClickable();
                        confirmButton.Focus();
                        confirmButton.Click();
                    }
                }
            }).Wait();
        }
        #endregion

        #region 自有群操作


        /// <summary>
        /// 删除群聊,与退出群聊不同，退出群聊是退出群聊，删除群聊会删除自有群的所有好友，然后退出群聊
        /// </summary>
        /// <param name="groupName">群聊名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse DeleteOwnerChatGroup()
        {
            ChatResponse result = new ChatResponse();
            try
            {
                this._RemoveAllChatGroupMember();
                this.QuitChatGroup();
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }
        private void _RemoveAllChatGroupMember()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            _uiThreadInvoker.Run(automation =>
            {
                var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='聊天成员']";
                var listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var revListItem = listBox?.FindFirstByXPath("//ListItem[@Name='移出']")?.AsButton();
                if (revListItem != null)
                {
                    var revButton = revListItem.FindFirstByXPath("/Pane/Pane/Button")?.AsButton();
                    if (revButton != null)
                    {
                        revButton.WaitUntilClickable();
                        revButton.Focus();
                        revButton.Click();
                        var deleteMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("DeleteMemberWnd")).And(cf.ByClassName("DeleteMemberWnd")))?.AsWindow(),
                        TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                        if (deleteMemberWin != null)
                        {
                            var listBoxRoot = deleteMemberWin.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                            var revList = listBoxRoot.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).ToList();
                            foreach (var item in revList)
                            {
                                item.AsCheckBox().ToggleState = ToggleState.On;
                                Thread.Sleep(500);
                            }
                            var confirmButton = deleteMemberWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                            if (confirmButton != null)
                            {
                                confirmButton.WaitUntilClickable();
                                confirmButton.Focus();
                                confirmButton.Click();
                            }
                        }
                    }
                }
            }).Wait();
            Thread.Sleep(100);
        }
        /// <summary>
        /// 发送群聊公告
        /// </summary>
        /// <param name="notice">公告内容</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse PublishOwnerChatGroupNotice(string notice)
        {
            return this.UpdateChatGroupOptions(options =>
            {
                options.GroupNotice = notice;
            });
        }
        /// <summary>
        /// 添加群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddOwnerChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        /// <summary>
        /// 移除群聊成员
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果<see cref="ChatResponse"/></returns>
        public ChatResponse RemoveOwnerChatGroupMember(OneOf<string, string[]> memberName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                this._RemoveChatGroupMemberCore(memberName);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        private void _RemoveChatGroupMemberCore(OneOf<string, string[]> memberName)
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchText();
            _uiThreadInvoker.Run(automation =>
            {
                var pane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd"))
                    .And(cf.ByName("SessionChatRoomDetailWnd")));
                var revListItem = pane.FindFirstByXPath("//ListItem[@Name='移出']")?.AsListBoxItem();
                var revButton = revListItem.FindFirstByXPath("/Pane/Pane/Button")?.AsButton();
                if (revButton != null)
                {
                    revButton.WaitUntilClickable();
                    revButton.Focus();
                    revButton.Click();
                    var deleteMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("DeleteMemberWnd")).And(cf.ByClassName("DeleteMemberWnd")))?.AsWindow(),
                    TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                    if (deleteMemberWin != null)
                    {
                        var listBoxRoot = deleteMemberWin.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                        // if (listBoxRoot)
                        // var revList = listBoxRoot.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).ToList();
                    }

                }
            }).Wait();
            Thread.Sleep(100);
        }

        #endregion
        #region 他有群特定操作
        /// <summary>
        /// 邀请群聊成员,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse InviteChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        /// <summary>
        /// 添加群聊成员,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddChatGroupMember(OneOf<string, string[]> memberName)
        {
            return null;
        }
        #endregion
        #endregion

        /// <summary>
        /// 关闭子窗口
        /// </summary>
        public void Close()
        {
            _SelfWindow.Close();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            if (_ChatBodyCache != null)
            {
                _ChatBodyCache.Dispose();
            }
        }
    }
}
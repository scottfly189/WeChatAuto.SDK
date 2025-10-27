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
using OneOf;
using WindowsInput;
using WindowsInput.Native;
using WxAutoCommon.Enums;
using WxAutoCommon.Interface;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using WxAutoCommon.Simulator;
using WeChatAuto.Services;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 子窗口基类
    /// </summary>
    public class SubWin : IWeChatWindow, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<SubWin> _logger;
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
            _logger = serviceProvider.GetRequiredService<AutoLogger<SubWin>>();
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
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
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
            if (!_IsSidebarOpen(false))
            {
                _OpenSidebar(false);
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
                // Keyboard.TypeSimultaneously(VirtualKeyShort.TAB);
                // Keyboard.TypeSimultaneously(VirtualKeyShort.TAB);
                // Keyboard.TypeSimultaneously(VirtualKeyShort.SPACE); // 空格触发点击
                InputSimulator input = new InputSimulator();
                // input.Mouse.MoveMouseTo(button.GetClickablePoint().X, button.GetClickablePoint().Y);
                // input.Mouse.LeftButtonClick();
                button.Focus();
                button.WaitUntilClickable();
                // Mouse.LeftClick(button.GetClickablePoint());
                var position = button.GetClickablePoint();
                input.Mouse.MoveMouseTo(position.X, position.Y);


                var edit = Retry.WhileNull(() => element.FindFirstByXPath("//Edit")?.AsTextBox(), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (edit != null)
                {
                    edit.Focus();
                    //Keyboard.TypeSimultaneously(VirtualKeyShort.RETURN);
                    //input.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A).KeyPress(VirtualKeyCode.BACK);
                    input.Keyboard.TextEntry(groupName);

                }
                else
                {
                    Trace.WriteLine("没有找到Edit控件，无法修改群聊名称");
                }
            }
            else
            {
                //不可以修改
                Trace.WriteLine("不支持此Pattern");
            }
        }
        //更新是否显示群昵称
        private void _UpdateShowGroupNickName(AutomationElement element, bool showGroupNickName)
        {
            var rootElement = element.GetSibling(5);
            var el = rootElement.FindFirstByXPath("//CheckBox[@Name='显示群成员昵称']")?.AsCheckBox();
            if (el != null)
            {
                // el.Focus();
                // el.DrawHighlightExt();
                var result = el.ToggleState == ToggleState.On ? true : false;
                if (result != showGroupNickName)
                {
                    // el.ToggleState = showGroupNickName ? ToggleState.On : ToggleState.Off;
                    // if (el.Patterns.Toggle.IsSupported)
                    // {
                    //     var pattern = el.Patterns.Toggle.Pattern;
                    //     pattern.Toggle();   
                    // }
                    Mouse.MoveTo(el.GetClickablePoint());
                    el.Click();
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
        /// <summary>
        /// 更新群聊公告
        /// </summary>
        /// <param name="groupNotice">群聊公告</param>
        public ChatResponse UpdateGroupNotice(string groupNotice)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                _uiThreadInvoker.Run(automation =>
                {
                    if (!_IsSidebarOpen(false))
                    {
                        _OpenSidebar(false);
                    }
                    _FocuseSearchTextExt(false);
                    Thread.Sleep(500);
                    var rootElement = this.GetNewElement();
                    _UpdateGroupNotice(rootElement, groupNotice);
                }).Wait();
                result.Success = true;
                result.Message = "更新群聊公告成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
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
                Thread.Sleep(300);
                el.DrawHighlightExt();
                el.Click();
                Thread.Sleep(300);
                var popWin = Retry.WhileNull(() =>
                {
                    var desktop = el.Automation.GetDesktop();
                    var pWin = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_MainWxWindow.ProcessId)).And(cf.ByClassName("ChatRoomAnnouncementWnd")))?.AsWindow();
                    return pWin;
                }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                if (popWin != null)
                {
                    popWin.DrawHighlightExt();
                    var editButton = Retry.WhileNull(() => popWin.FindFirstByXPath("//Button[@Name='编辑'] | //Button[@Name='完成']")?.AsButton(),
                    TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                    if (editButton != null)
                    {
                        if (editButton.Name == "编辑")
                        {
                            editButton.DrawHighlightExt();
                            editButton.WaitUntilClickable();
                            editButton.Click();
                            Thread.Sleep(300);
                        }

                        var edit = popWin.FindFirstByXPath("//Edit")?.AsTextBox();
                        edit.DrawHighlightExt();
                        edit.Focus();
                        edit.WaitUntilClickable();
                        edit.Click();
                        Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                        Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                        Keyboard.Type(groupNotice);
                        Wait.UntilInputIsProcessed();
                        Thread.Sleep(1000);
                        var finishButton = popWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                        finishButton?.DrawHighlightExt();
                        finishButton?.Focus();
                        finishButton?.WaitUntilClickable();
                        finishButton?.Click();

                        var sendButton = Retry.WhileNull(() => popWin.FindFirstByXPath("//Button[@Name='发布']")?.AsButton(), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                        if (sendButton != null)
                        {
                            sendButton.DrawHighlightExt();
                            sendButton.Focus();
                            sendButton.WaitUntilClickable();
                            sendButton.Click();
                        }
                        else
                        {
                            popWin.Close();
                            throw new Exception("可能相同公告内容，未找到发布按钮");
                        }
                    }
                    else
                    {
                        //无权限编辑
                        popWin.Close();
                        throw new Exception("无编辑权限");
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
                    edit.DrawHighlightExt();
                    edit.Focus();
                    edit.Click();
                }
            }).Wait();
        }

        private void _FocuseSearchTextExt(bool autoThread = true)
        {
            Action action = () =>
            {
                Thread.Sleep(500);
                var poupPane = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByClassName("SessionChatRoomDetailWnd")
                    .And(cf.ByControlType(ControlType.Pane))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                var edit = poupPane.FindFirstDescendant(cf => cf.ByName("搜索群成员"))?.AsTextBox();
                if (edit != null)
                {
                    edit.DrawHighlightExt();
                    edit.Focus();
                    edit.Click();
                }
            };
            if (autoThread)
            {
                _uiThreadInvoker.Run(automation => action()).Wait();
            }
            else
            {
                action();
            }
        }


        /// <summary>
        /// 是否打开侧边栏
        /// </summary>
        /// <param name="autoThread">是否使用线程执行</param>
        /// <returns>是否打开侧边栏</returns>
        private bool _IsSidebarOpen(bool autoThread = true)
        {
            Func<bool> func = () =>
            {
                var pane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd")));
                return pane != null;
            };
            if (autoThread)
            {
                bool result = _uiThreadInvoker.Run(automation => func()).Result;
                return result;
            }
            else
            {
                return func();
            }
        }
        private void _OpenSidebar(bool autoThread = true)
        {
            Action action = () =>
            {
                var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Button[@Name='聊天信息']";
                var button = _SelfWindow.FindFirstByXPath(xPath)?.AsButton();
                if (button != null)
                {
                    button.DrawHighlightExt();
                    button.WaitUntilClickable();
                    button.Focus();
                    button.Click();
                }
            };
            if (autoThread)
            {
                _uiThreadInvoker.Run(automation => action()).Wait();
            }
            else
            {
                action();
            }

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
            _FocuseSearchTextExt();
            Thread.Sleep(500);
            List<string> list = _uiThreadInvoker.Run(automation =>
            {
                var memberList = new List<string>();
                var xPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='聊天成员']";
                var listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var rootPane = listBox.GetParent();
                rootPane?.DrawHighlightExt();
                while (true)
                {
                    //反复点击“查看更多”按钮
                    var moreButton = Retry.WhileNull(() =>
                    {
                        return rootPane.FindFirstByXPath("//Button[@Name='查看更多']")?.AsButton();
                    }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                    if (moreButton.Success)
                    {
                        moreButton.Result?.DrawHighlightExt();
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
                    if (item.Name != "添加" && item.Name != "移出")
                    {
                        memberList.Add(item.Name.Trim());
                    }
                }
                _logger.Info("获取群聊成员列表成功，成员数量：" + memberList.Count);
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
        /// <returns>是否是群聊成员,True:是,False:否</returns>
        public bool IsChatGroupMember(string memberName)
        {
            return SearchChatGroupMember(memberName) != null;
        }
        /// <summary>
        /// 是否是自有群
        /// </summary>
        /// <returns>是否是自有群,True:是,False:否</returns>
        public bool IsOwnerChatGroup()
        {
            return GetGroupOwner() == _MainWxWindow.NickName;
        }
        /// <summary>
        /// 获取群主
        /// </summary>
        /// <returns>群主昵称</returns>
        public string GetGroupOwner()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchTextExt();
            Thread.Sleep(500);
            string result = _uiThreadInvoker.Run(automation =>
            {
                var memberList = new List<string>();
                var xPath = "/Pane[1]/Pane/Pane/Pane/Pane/Pane/Pane/List[@Name='聊天成员']";
                var listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                listBox = _SelfWindow.FindFirstByXPath(xPath)?.AsListBox();
                var items = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                items = items.Where(item => item.Name != "添加" && item.Name != "移出").ToList();
                var firstItem = items.First();   //群主
                _logger.Info("获取群主成功，群主昵称：" + firstItem?.Name);

                return firstItem?.Name ?? "";
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
            _FocuseSearchTextExt();
            Thread.Sleep(300);
            _uiThreadInvoker.Run(automation =>
            {
                var poupPane = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByClassName("SessionChatRoomDetailWnd")
                .And(cf.ByControlType(ControlType.Pane))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                var list = poupPane.FindFirstByXPath("//List[@Name='聊天成员']");
                var scrollPane = list.GetParent();
                if (scrollPane.Patterns.Scroll.IsSupported)
                {
                    var pattern = scrollPane.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 1);
                }

                var clearButton = Retry.WhileNull(() => _SelfWindow.FindFirstByXPath("//Button[@Name='清空聊天记录']")?.AsButton(),
                TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                if (clearButton != null)
                {
                    clearButton.DrawHighlightExt();
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
            _FocuseSearchTextExt();
            _uiThreadInvoker.Run(automation =>
            {
                var poupPane = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByClassName("SessionChatRoomDetailWnd")
                    .And(cf.ByControlType(ControlType.Pane))), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                var list = poupPane.FindFirstByXPath("//List[@Name='聊天成员']");
                var scrollPane = list.GetParent();
                if (scrollPane.Patterns.Scroll.IsSupported)
                {
                    var pattern = scrollPane.Patterns.Scroll.Pattern;
                    pattern.SetScrollPercent(0, 1);
                }
                var exitButton = _SelfWindow.FindFirstByXPath("//Button[@Name='退出群聊']")?.AsButton();
                if (exitButton != null)
                {
                    exitButton.DrawHighlightExt();
                    exitButton.WaitUntilClickable();
                    exitButton.Focus();
                    exitButton.Click();
                    var confirmButton = Retry.WhileNull(() =>
                    {
                        return _SelfWindow.FindFirstByXPath("/Pane[1]/Pane/Pane/Button[@Name='退出']")?.AsButton();
                    }, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
                    if (confirmButton != null)
                    {
                        confirmButton.WaitUntilClickable();
                        confirmButton.Focus();
                        confirmButton.Click();
                        _logger.Info("退出群聊成功");
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
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        private void _RemoveAllChatGroupMember()
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchTextExt();
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
                            listBoxRoot.DrawHighlightExt();
                            var revList = listBoxRoot.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox)).ToList();
                            foreach (var item in revList)
                            {
                                var checkBox = item.AsCheckBox();
                                if (checkBox.ToggleState != ToggleState.On)
                                {
                                    xPath = "//Button";
                                    var checkButton = checkBox.FindFirstByXPath(xPath)?.AsButton();
                                    if (checkButton != null)
                                    {
                                        checkButton.WaitUntilClickable();
                                        checkButton.Focus();
                                        checkButton.DrawHighlightExt();
                                        checkButton.Click();
                                    }
                                }
                                Thread.Sleep(500);
                            }
                            var finishButton = deleteMemberWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                            finishButton?.DrawHighlightExt();
                            if (finishButton != null)
                            {
                                finishButton.WaitUntilClickable();
                                finishButton.Focus();
                                finishButton.Click();
                                Thread.Sleep(300);
                                var confirmWin = Retry.WhileNull(() => deleteMemberWin.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("ConfirmDialog"))),
                                TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                                confirmWin.DrawHighlightExt();
                                if (confirmWin != null)
                                {
                                    var deleButton = Retry.WhileNull(() => confirmWin.FindFirstDescendant(cf => cf.ByName("删除").And(cf.ByControlType(ControlType.Button)))?.AsButton(),
                                    TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                                    deleButton?.DrawHighlightExt();
                                    if (deleButton != null)
                                    {
                                        deleButton.WaitUntilClickable();
                                        deleButton.Focus();
                                        Keyboard.Press(VirtualKeyShort.RETURN);
                                    }
                                    Thread.Sleep(1000);
                                }
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
        /// 添加群聊成员,适用于自有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddOwnerChatGroupMember(OneOf<string, string[]> memberName)
        {
            ChatResponse result = new ChatResponse();
            try
            {
                this._AddChatGroupMemberCore(memberName);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        private void _AddChatGroupMemberCore(OneOf<string, string[]> memberName)
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchTextExt();
            List<string> addList = new List<string>();
            if (memberName.IsT0)
            {
                addList.Add(memberName.AsT0);
            }
            else
            {
                addList.AddRange(memberName.AsT1);
            }
            List<string> chatGroupFriends = this.GetChatGroupMemberList();
            addList = addList.Except(chatGroupFriends).ToList();
            if (addList.Count == 0)
            {
                _logger.Warn("警告：实际待邀请的人数为零，请检查你输入的待邀请人群是否都在群聊中。");
                return;
            }
            _uiThreadInvoker.Run(automation =>
            {
                var rootPane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd")));
                var addListItem = rootPane.FindFirstByXPath("//ListItem[@Name='添加']")?.AsListBoxItem();
                var addButton = addListItem.FindFirstByXPath("/Pane/Pane/Button")?.AsButton();
                if (addButton != null)
                {
                    addButton.DrawHighlightExt();
                    addButton.WaitUntilClickable();
                    addButton.Focus();
                    addButton.Click();
                    _SelectWillAddPersion(addList);
                    //点击完成按钮
                    _ClickAddConfirmButton();
                }
            }).Wait();
            Thread.Sleep(100);
        }
        /// <summary>
        /// 点击添加确认按钮
        /// </summary>
        private void _ClickAddConfirmButton()
        {
            var addMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("AddMemberWnd")).And(cf.ByClassName("AddMemberWnd")))?.AsWindow(),
             TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
            if (addMemberWin != null)
            {
                var finishButton = addMemberWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                if (finishButton != null)
                {
                    finishButton.WaitUntilClickable();
                    finishButton.Focus();
                    finishButton.Click();
                    Thread.Sleep(300);
                }
            }
        }
        /// <summary>
        /// 选择需要添加的人员
        /// </summary>
        /// <param name="addList">待添加的人员列表</param>
        private void _SelectWillAddPersion(List<string> addList)
        {
            var addMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("AddMemberWnd")).And(cf.ByClassName("AddMemberWnd")))?.AsWindow(),
             TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
            if (addMemberWin != null)
            {
                foreach (var item in addList)
                {
                    //查询选人
                    var searchTextBox = addMemberWin.FindFirstByXPath("//Edit[@Name='搜索']")?.AsTextBox();
                    searchTextBox.Focus();
                    searchTextBox.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Thread.Sleep(1000);
                    Keyboard.Type(item);
                    // Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                    Thread.Sleep(300);
                    var listBoxRoot = addMemberWin.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                    var listItems = listBoxRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                    var listItem = listItems.FirstOrDefault(cf => cf.Name.Trim() == item).AsListBoxItem();
                    if (listItem != null)
                    {
                        var button = listItem.FindFirstByXPath("//Button")?.AsButton();
                        button?.Click();
                    }
                    Thread.Sleep(300);
                }
            }
        }
        /// <summary>
        /// 移除群聊成员,适用于自有群
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
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }

        private void _RemoveChatGroupMemberCore(OneOf<string, string[]> memberName)
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchTextExt();
            List<string> revList = new List<string>();
            if (memberName.IsT0)
            {
                revList.Add(memberName.AsT0);
            }
            else
            {
                revList.AddRange(memberName.AsT1);
            }
            _uiThreadInvoker.Run(automation =>
            {
                var pane = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd")));
                var revListItem = pane.FindFirstByXPath("//ListItem[@Name='移出']")?.AsListBoxItem();
                var revButton = revListItem.FindFirstByXPath("/Pane/Pane/Button")?.AsButton();
                if (revButton != null)
                {
                    revButton.WaitUntilClickable();
                    revButton.DrawHighlightExt();
                    revButton.Focus();
                    revButton.Click();
                    _SelectWillDeletePersion(revList);
                    //点击完成按钮
                    _ClickDeleteConfirmButton();
                }
            }).Wait();
            Thread.Sleep(100);
        }
        /// <summary>
        /// 点击删除确认按钮
        /// </summary>
        private void _ClickDeleteConfirmButton()
        {
            var deleteMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("DeleteMemberWnd")).And(cf.ByClassName("DeleteMemberWnd")))?.AsWindow(),
            TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
            if (deleteMemberWin != null)
            {
                var finishButton = deleteMemberWin.FindFirstByXPath("//Button[@Name='完成']")?.AsButton();
                if (finishButton != null)
                {
                    finishButton.DrawHighlightExt();
                    finishButton.WaitUntilClickable();
                    finishButton.Focus();
                    finishButton.Click();
                    Thread.Sleep(300);
                    //点击“确认”按钮
                    var ConfirmDialog = Retry.WhileNull(() => deleteMemberWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByClassName("ConfirmDialog"))),
                        TimeSpan.FromSeconds(3), TimeSpan.FromMilliseconds(200))?.Result;
                    if (ConfirmDialog != null)
                    {
                        ConfirmDialog.DrawHighlightExt();
                        var confirmButton = ConfirmDialog.FindFirstByXPath("//Button[@Name='删除']")?.AsButton();
                        if (confirmButton != null)
                        {
                            confirmButton.DrawHighlightExt();
                            Thread.Sleep(300);
                            confirmButton.WaitUntilClickable();
                            confirmButton.Focus();
                            Keyboard.Press(VirtualKeyShort.ENTER);
                            // confirmButton.Click();
                            Thread.Sleep(300);
                        }
                    }
                    else
                    {
                        var cancelButton = deleteMemberWin.FindFirstByXPath("//Button[@Name='取消']")?.AsButton();
                        cancelButton.DrawHighlightExt();
                        cancelButton?.Click();
                    }
                }
            }
        }
        /// <summary>
        /// 选择需要删除的人员
        /// </summary>
        /// <param name="revList">待删除的人员列表</param>
        private void _SelectWillDeletePersion(List<string> revList)
        {
            var deleteMemberWin = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("DeleteMemberWnd")).And(cf.ByClassName("DeleteMemberWnd")))?.AsWindow(),
            TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
            if (deleteMemberWin != null)
            {
                deleteMemberWin.DrawHighlightExt();
                foreach (var item in revList)
                {
                    //查询选人
                    var searchTextBox = deleteMemberWin.FindFirstByXPath("//Edit[@Name='搜索']")?.AsTextBox();
                    searchTextBox.Focus();
                    searchTextBox.DrawHighlightExt();
                    searchTextBox.Click();
                    Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                    Thread.Sleep(1000);
                    Keyboard.Type(item);
                    // Keyboard.Press(VirtualKeyShort.RETURN);
                    Wait.UntilInputIsProcessed();
                    Thread.Sleep(300);
                    var listBoxRoot = deleteMemberWin.FindFirstByXPath("//List[@Name='请勾选需要添加的联系人']")?.AsListBox();
                    var listItems = listBoxRoot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).ToList();
                    if (listItems != null && listItems.Count > 0)
                    {
                        foreach (var subItem in listItems)
                        {
                            var button = subItem.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button))?.AsButton();
                            if (button?.Name?.Trim() == item)
                            {
                                button?.Click();
                            }
                            Thread.Sleep(300);
                        }
                    }
                    Thread.Sleep(300);
                }

            }
        }

        #endregion
        #region 他有群特定操作
        /// <summary>
        /// 邀请群聊成员,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse InviteChatGroupMember(OneOf<string, string[]> memberName, string helloText = "")
        {
            ChatResponse result = new ChatResponse();
            try
            {
                this._AddChatGroupMemberCore(memberName);
                this._ConfirmInviteChatGroupMember(helloText);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        private void _ConfirmInviteChatGroupMember(string helloText = "")
        {
            var confirmPane = Retry.WhileNull(() => _SelfWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("WeUIDialog"))),
             TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200))?.Result;
            if (confirmPane != null)
            {
                if (!string.IsNullOrWhiteSpace(helloText))
                {
                    var edit = confirmPane.FindFirstByXPath("//Edit")?.AsTextBox();
                    if (edit != null)
                    {
                        edit.Focus();
                        edit.Click();
                        Keyboard.Type(helloText);
                        Wait.UntilInputIsProcessed();
                        Thread.Sleep(300);
                    }
                }
                var confirmButton = confirmPane.FindFirstByXPath("//Button[@Name='发送']")?.AsButton();
                if (confirmButton != null)
                {
                    confirmButton.WaitUntilClickable();
                    confirmButton.Focus();
                    confirmButton.Click();
                    Thread.Sleep(300);
                }
            }
            else
            {
                _logger.Info("由于群主没有设置认证，所以没有弹出确认窗口，成功邀请！");
            }
        }
        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于他有群
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果</returns>
        [Obsolete("由于微信对于自动化的限制，暂时放弃此方法，修改成硬件模拟的方式")]
        public ChatResponse AddChatGroupMemberToFriends(OneOf<string, string[]> memberName, int intervalSecond = 3, string helloText = "")
        {
            return this.AddChatGroupMemberToFriends(memberName, intervalSecond, helloText, "");
        }
        /// <summary>
        /// 添加群聊里面的好友为自己的好友,适用于从他有群中添加好友为自己的好友
        /// 注：使用此方法必须要打开硬件模拟器，否则无法正常添加好友
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <param name="label">好友标签,方便归类管理</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse AddChatGroupMemberToFriends(OneOf<string, string[]> memberName, int intervalSecond = 3, string helloText = "", string label = "")
        {
            ChatResponse result = new ChatResponse();
            try
            {
                _logger.Info($"开始添加群聊成员为好友，待添加列表: {string.Join(",", memberName.IsT0 ? new string[] { memberName.AsT0 } : memberName.AsT1)}");
                this._AddChatGroupFriendsCore(memberName, intervalSecond, helloText, label);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        /// <summary>
        /// 添加群聊里面的好友为自己的好友核心方法
        /// </summary>
        /// <param name="memberName">成员名称</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <param name="label">好友标签,方便归类管理</param>
        private void _AddChatGroupFriendsCore(OneOf<string, string[]> memberName, int intervalSecond = 3, string helloText = "", string label = "")
        {
            if (!_IsSidebarOpen())
            {
                _OpenSidebar();
            }
            _FocuseSearchTextExt();
            List<string> addList = new List<string>();
            if (memberName.IsT0)
            {
                addList.Add(memberName.AsT0);
            }
            else
            {
                addList.AddRange(memberName.AsT1);
            }
            var willAddList = _GetWillAddListFromContacts(addList);
            _logger.Info($"获取到待添加列表: {string.Join(",", willAddList)}");
            if (willAddList.Count == 0)
            {
                _logger.Warn("警告：实际待添加的人数为零，请检查你输入的待添加人群是否都在通讯录中。");
                return;
            }
            _uiThreadInvoker.Run(automation =>
            {
                foreach (var item in willAddList)
                {
                    var paneRoot = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd")));
                    if (paneRoot != null)
                    {
                        _logger.Info($"开始搜索群成员: {item}");
                        var xPath = "/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Pane/Edit[@Name='搜索群成员']";
                        var edit = _SelfWindow.FindFirstByXPath(xPath)?.AsTextBox();
                        if (edit != null)
                        {
                            edit.Focus();
                            edit.Click();
                            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                            Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                            Keyboard.Type(item);
                            Keyboard.Press(VirtualKeyShort.RETURN);
                            Wait.UntilInputIsProcessed();
                            Thread.Sleep(600);
                            var listItem = paneRoot.FindFirstByXPath("//ListItem")?.AsListBoxItem();
                            if (listItem != null)
                            {
                                listItem.DrawHighlightExt();
                                var button = listItem.FindFirstByXPath("/Pane/Pane/Button").AsButton();
                                if (button != null)
                                {
                                    button.DrawHighlightExt();
                                    button.WaitUntilClickable();

                                    listItem.GetParent().Focus();
                                    var point = button.BoundingRectangle.Center();
                                    // KMSimulatorService.LeftClick(point);
                                    // KMSimulatorService.LeftClick();
                                    Mouse.MoveTo(point);
                                    Mouse.LeftClick();
                                    Mouse.LeftClick();

                                    return;
                                    var addPane = Retry.WhileNull(() => paneRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("添加好友")).And(cf.ByClassName("WeUIDialog"))),
                                        TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200))?.Result;
                                    if (addPane != null)
                                    {
                                        //点击添加到通讯录按钮
                                        var addButton = addPane.FindFirstByXPath("//Button[@Name='添加到通讯录']")?.AsButton();
                                        addButton.WaitUntilClickable();
                                        addButton.Click();
                                        var addConfirmWinResult = Retry.WhileNull(() => _MainWxWindow.Window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window).And(cf.ByName("添加朋友请求")).And(cf.ByClassName("WeUIDialog")))?.AsWindow(),
                                            TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                                        if (addConfirmWinResult.Success && addConfirmWinResult.Result != null)
                                        {
                                            if (!string.IsNullOrWhiteSpace(helloText))
                                            {
                                                var helloTextEdit = addConfirmWinResult.Result.FindFirstByXPath("/Pane[2]/Pane[1]/Pane/Pane/Pane[1]/Pane/Edit").AsTextBox();
                                                helloTextEdit.Focus();
                                                helloTextEdit.Click();
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                                                Keyboard.Type(helloText);
                                                helloTextEdit.Click();
                                                Keyboard.Press(VirtualKeyShort.RETURN);
                                                Wait.UntilInputIsProcessed();
                                                Thread.Sleep(600);
                                            }
                                            if (!string.IsNullOrWhiteSpace(label))
                                            {
                                                var labelEdit = addConfirmWinResult.Result.FindFirstByXPath("/Pane[2]/Pane[1]/Pane/Pane/Pane[3]/Pane[1]/Pane/Edit").AsTextBox();
                                                labelEdit.Focus();
                                                labelEdit.Click();
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                                                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                                                Keyboard.Type(label);
                                                labelEdit.Click();
                                                Keyboard.Press(VirtualKeyShort.RETURN);
                                                Wait.UntilInputIsProcessed();
                                                Thread.Sleep(600);
                                            }
                                            button = addConfirmWinResult.Result.FindFirstByXPath("//Button[@Name='确定']")?.AsButton();
                                            button.WaitUntilClickable();
                                            button.Click();
                                            Thread.Sleep(600);
                                        }
                                        else
                                        {
                                            edit.Focus();
                                            edit.Click();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(intervalSecond * 1000);  //停顿间隔时间
            }).Wait();
        }
        /// <summary>
        /// 从通讯录中获取待添加列表
        /// </summary>
        /// <param name="addList">待添加列表</param>
        /// <returns>待添加列表</returns>
        private List<string> _GetWillAddListFromContacts(List<string> addList)
        {
            var list = _uiThreadInvoker.Run(automation =>
            {
                var paneRoot = _SelfWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByClassName("SessionChatRoomDetailWnd")));
                _ExpndListBox(paneRoot);
                var xPath = "//List[@Name='聊天成员']";
                var listBox = paneRoot.FindFirstByXPath(xPath)?.AsListBox();
                var listItem = listBox.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem)).Select(item => item.AsListBoxItem()).ToList();
                var listItemValues = listItem.Select(item => item.Name).ToList();
                listItemValues = listItemValues.Intersect(addList).ToList();
                return listItemValues;
            }).Result;
            return list;
        }


        /// <summary>
        /// 展开列表框
        /// </summary>
        /// <param name="pane">列表框</param>
        private void _ExpndListBox(AutomationElement pane)
        {
            var xPath = "//Button[@Name='查看更多']";
            var button = pane.FindFirstByXPath(xPath)?.AsButton();
            if (button != null)
            {
                button.WaitUntilClickable();
                button.Click();
                Thread.Sleep(600);
            }
        }
        /// <summary>
        /// 添加群聊里面的所有好友为自己的好友,适用于从他有群中添加所有好友为自己的好友
        /// </summary>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="exceptList">排除列表</param>
        /// <param name="helloText">打招呼文本</param>
        /// <returns>微信响应结果</returns>
        [Obsolete("由于微信对于自动化的限制，暂时放弃此方法,修改成硬件模拟的方式")]
        public ChatResponse AddAllChatGroupMemberToFriends(List<string> exceptList = null, int intervalSecond = 3, string helloText = "")
        {
            return this.AddAllChatGroupMemberToFriends(exceptList, intervalSecond, helloText, "");
        }
        /// <summary>
        /// 添加群聊里面的所有好友为自己的好友,适用于从他有群中添加所有好友为自己的好友
        /// </summary>
        /// <param name="exceptList">排除列表</param>
        /// <param name="intervalSecond">间隔时间</param>
        /// <param name="helloText">打招呼文本</param>
        /// <param name="label">好友标签,方便归类管理</param>
        /// <returns>微信响应结果</returns>
        [Obsolete("由于微信对于自动化的限制，暂时放弃此方法,修改成硬件模拟的方式")]
        public ChatResponse AddAllChatGroupMemberToFriends(List<string> exceptList = null, int intervalSecond = 3, string helloText = "", string label = "")
        {
            ChatResponse result = new ChatResponse();
            try
            {
                var memberList = this.GetChatGroupMemberList();
                var myNickName = _MainWxWindow.NickName;
                memberList.Remove(myNickName);
                memberList = memberList.Except(exceptList).ToList();
                this.AddChatGroupMemberToFriends(memberList.ToArray(), intervalSecond, helloText, label);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return result;
            }
        }
        #endregion
        #endregion

        #region 子窗口操作
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
        #endregion
    }
}
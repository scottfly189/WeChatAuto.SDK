using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;
using WeAutoCommon.Enums;
using WeAutoCommon.Utils;
using WeChatAuto.Utils;
using FlaUI.UIA3.Converters;
using FlaUI.Core.WindowsAPI;
using System;
using WeChatAuto.Extentions;
using WeAutoCommon.Interface;
using System.Text;
using OneOf;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Windows.Controls.Primitives;
using FlaUI.Core.Patterns;
using System.Drawing;
using System.Threading.Tasks;
using WeAutoCommon.Extentions;
using FlaUI.UIA3.Patterns;
using FlaUI.UIA3;
using WeChatAuto.Models;
using WeAutoCommon.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Media.Imaging;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using WeChatAuto.Options;
using System.IO;
using Emgu.CV.Structure;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区发送者
    /// </summary>
    public class Search
    {
        private readonly AutoLogger<Search> _logger;
        private UIThreadInvoker _uiThreadInvoker;
        private readonly IServiceProvider _serviceProvider;
        private WeChatClient _Client;
        /// <summary>
        /// 聊天内容区发送者构造函数
        /// </summary>
        internal Search(WeChatClient client, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _uiThreadInvoker = uiThreadInvoker;
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<AutoLogger<Search>>();
            this._Client = client;
        }

        /// <summary>
        /// 打开新增朋友窗口
        /// </summary>
        /// <returns></returns>
        public async Task<Window> OpenAddFriensWin() => await WeChatInvoker.Call(OpenAddFriensWinCore);


        internal Window OpenAddFriensWinCore(UIA3Automation automation)
        {
            var desktop = automation.GetDesktop();
            var winRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByName("添加朋友").And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)).And(cf.ByControlType(ControlType.Window)).And(cf.ByAutomationId("AddFriendWindow"))), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (winRetry.Success)
            {
                winRetry.Result.AsWindow().Focus();
                RandomWait.Wait(100, 300);
                this._Client.MoveWinToMainCenter(winRetry.Result.AsWindow());
                return winRetry.Result.AsWindow();
            }
            //打开
            var conversationRoot = this._Client.Conversations.ConversationRoot;
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Group/Group/Group/Group/Button[@Name='快捷操作']";
            var buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (buttonRetry.Success)
            {
                var button = buttonRetry.Result.AsButton();
                var point = button.BoundingRectangle.Center();
                var point2 = point.Confusion(10, 10);
                Mouse.Position = point2;
                RandomWait.Wait(100, 300);
                point2 = point.Confusion(10, 10);
                SupperMouseKey.MoveTo(point2);
                RandomWait.Wait(100, 300);
                point2 = point.Confusion(10, 10);
                SupperMouseKey.MoveTo(point2);
                RandomWait.Wait(300, 900);
                SupperMouseKey.LeftClick();
                RandomWait.Wait(300, 900);
                var menuRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath("/Window/Group/List[@Name='快捷操作'][@AutomationId='chat_more_entry']"), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (menuRetry.Success)
                {
                    var menuList = menuRetry.Result;
                    var menuItem = menuList.FindFirstChild(cf => cf.ByName("添加朋友").And(cf.ByControlType(ControlType.ListItem)));
                    if (menuItem != null)
                    {
                        point = menuItem.BoundingRectangle.Center().Confusion(10, 5);
                        SupperMouseKey.MoveTo(point);
                        RandomWait.Wait(100, 300);
                        point = menuItem.BoundingRectangle.Center().Confusion(10, 5);
                        SupperMouseKey.MoveTo(point);
                        RandomWait.Wait(300, 900);
                        SupperMouseKey.LeftClick();
                        RandomWait.Wait(300, 1200);

                        winRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByName("添加朋友").And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)).And(cf.ByControlType(ControlType.Window)).And(cf.ByAutomationId("AddFriendWindow"))), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                        if (winRetry.Success)
                        {
                            this._Client.MoveWinToMainCenter(winRetry.Result.AsWindow());
                            return winRetry.Result.AsWindow();
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 关闭新增朋友窗口
        /// </summary>
        /// <returns></returns>
        public async Task CloseAddFriendWin() => await WeChatInvoker.Call(CloseAddFriendWinCore);


        internal void CloseAddFriendWinCore(UIA3Automation automation)
        {
            var desktop = automation.GetDesktop();
            var winRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByName("添加朋友").And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)).And(cf.ByControlType(ControlType.Window)).And(cf.ByAutomationId("AddFriendWindow"))), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (winRetry.Success)
            {
                winRetry.Result.AsWindow().Focus();
                RandomWait.Wait(300, 900);
                winRetry.Result.AsWindow().Close();
            }
        }

        /// <summary>
        /// 通过手机号码、微信号查找并添加好友
        /// </summary>
        /// <param name="friends">手机号码或者微信号列表</param>
        /// <param name="options">增加朋友选项，具体请参考<see cref="AddFriendsOptions"/></param>
        /// <param name="token">取消令牌</param>
        /// <returns>添加好友结果列表，详情请参见<see cref="FriendAddResult"/></returns>
        public async Task<IDictionary<string, FriendAddResult>> AddFriends(OneOf<string, string[]> friends, AddFriendsOptions options = null, CancellationToken token = default)
        => await WeChatInvoker.Call(AddFriendsCore, friends, options, token);

        internal IDictionary<string, FriendAddResult> AddFriendsCore(UIA3Automation automation, OneOf<string, string[]> friends, AddFriendsOptions options, CancellationToken token)
        {
            var result = new Dictionary<string, FriendAddResult>();
            try
            {
                var win = OpenAddFriensWinCore(automation);
                if (win == null)
                    return result;
                token.ThrowIfCancellationRequested();
                var friendList = friends.IsT0 ? new List<string>() { friends.AsT0 } : friends.AsT1.ToList();
                foreach (var f in friendList)
                {
                    var path = "/Group/Group/Group/Button[@Name='清空']";
                    var clearRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
                    if (clearRetry.Success)
                    {
                        var cPoint = clearRetry.Result.BoundingRectangle.Center().Confusion(5, 5);
                        SupperMouseKey.MoveTo(cPoint);
                        RandomWait.Wait(100, 300);
                        SupperMouseKey.MoveTo(clearRetry.Result.BoundingRectangle.Center().Confusion(5, 5));
                        RandomWait.Wait(300, 900);
                        SupperMouseKey.LeftClick();
                        RandomWait.Wait(300, 900);
                    }
                    token.ThrowIfCancellationRequested();
                    path = "/Group/Group/Group/Edit[@Name='搜索'][@ClassName='mmui::XValidatorTextEdit']";
                    var edit = win.FindFirstByXPath(path).AsTextBox();
                    if (edit == null)
                        continue;
                    var point = edit.BoundingRectangle.Center().Confusion(20, 4);
                    SupperMouseKey.MoveTo(point);
                    RandomWait.Wait(100, 300);
                    SupperMouseKey.MoveTo(edit.BoundingRectangle.Center().Confusion(20, 4));
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.LeftClick();
                    RandomWait.Wait(300, 900);
                    //SupperMouseKey.Type(f);
                    // ClipboardHelper.SetText(f);
                    edit.Text = f;
                    RandomWait.Wait(300, 900);
                    path = "/Group/Group/Button[@Name='搜索']";
                    var button = win.FindFirstByXPath(path).AsButton();
                    token.ThrowIfCancellationRequested();
                    if (button != null)
                    {
                        point = button.BoundingRectangle.Center().Confusion(20, 4);
                        SupperMouseKey.MoveTo(point);
                        RandomWait.Wait(100, 300);
                        SupperMouseKey.MoveTo(button.BoundingRectangle.Center().Confusion(20, 4));
                        RandomWait.Wait(300, 900);
                        SupperMouseKey.LeftClick();
                        RandomWait.Wait(300, 900);
                        __ProcessSearResult(win, result, f, options, token);
                    }
                    if (options != null)
                    {
                        RandomWait.Wait(System.Math.Max(options.IntervalTime - 1, 0) * 1000, Math.Max(options.IntervalTime + 1, 1) * 1000);
                        continue;
                    }
                    RandomWait.Wait(2000, 5000);
                    token.ThrowIfCancellationRequested();
                }

                if (options != null && options.IsCloseWin)
                {
                    CloseAddFriendWinCore(automation);
                }
                else
                {
                    CloseAddFriendWinCore(automation);
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                //用户取消

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            return result;
        }

        private void __ProcessSearResult(Window win, Dictionary<string, FriendAddResult> result, string friend, AddFriendsOptions options, CancellationToken token)
        {
            //第一种情况： 找不到用户
            var path = "/Group/Group/Group/Text[@Name='无法找到该用户，请检查你填写的账号是否正确。']";
            var errTextRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (errTextRetry.Success)
            {
                result.Add(friend, FriendAddResult.No_Find);
                return;
            }
            token.ThrowIfCancellationRequested();
            //第二种情况...添加
            path = "/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='添加到通讯录'][@AutomationId='content_v_view.ProfileActionUi.add_friend_button']";
            var buttonRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (buttonRetry.Success)
            {
                var button = buttonRetry.Result.AsButton();
                var point = button.BoundingRectangle.SafeRandomPoint();
                SupperMouseKey.MoveTo(point);
                RandomWait.Wait(100, 300);
                SupperMouseKey.MoveTo(point.Confusion(10, 5));
                RandomWait.Wait(300, 900);
                SupperMouseKey.LeftClick();
                RandomWait.Wait(300, 900);
                __ConfigAddInfomation(win, result, friend, options, token);
                //可能是待验证状态，或者是直接通过状态
                path = "/Group/Group/Group/Group/Group/Group/Group/Group/Button[@AutomationId='content_v_view.ProfileActionUi.add_friend_button'][@Name='等待验证'][@ClassName='mmui::XOutlineButton']";
                RandomWait.Wait(2000, 4000);
                var validButtonRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (validButtonRetry.Success)
                {
                    result.Add(friend, FriendAddResult.Adding);
                }
                else
                {
                    //可能是已添加状态
                    path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Text[@Name='微信号：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
                    var labelRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                    if (labelRetry.Success)
                    {
                        __ProcessAlreadFriendInfomation(win, friend, labelRetry.Result, token);
                        result.Add(friend, FriendAddResult.Added);
                    }
                }
                return;
            }
            token.ThrowIfCancellationRequested();
            //第三种情况...已是好友,更新cache.
            path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Text[@Name='微信号：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            var wxLabelRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (wxLabelRetry.Success)
            {
                __ProcessAlreadFriendInfomation(win, friend, wxLabelRetry.Result, token);
                result.Add(friend, FriendAddResult.Friend);
            }
        }
        private void __ConfigAddInfomation(Window win, Dictionary<string, FriendAddResult> result, string friend, AddFriendsOptions options, CancellationToken token)
        {
            var desktop = win.Automation.GetDesktop();
            var addWinRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByName("申请添加朋友").And(cf.ByClassName("mmui::VerifyFriendWindow").And(cf.ByControlType(ControlType.Window)).And(cf.ByProcessId(this._Client.MainWindow.Properties.ProcessId)))), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            if (addWinRetry.Success)
            {
                var addWin = addWinRetry.Result.AsWindow();
                this._Client.MoveWinToMainCenter(addWin);
                if (options != null && !string.IsNullOrWhiteSpace(options.SayHi))
                {
                    var path = "/Group/Group/Group/Group/Group/Group/Group/Edit[@Name='发送添加朋友申请'][@ClassName='mmui::XValidatorTextEdit']";
                    var edit = addWin.FindFirstByXPath(path);
                    var point = edit.BoundingRectangle.SafeRandomPoint();
                    SupperMouseKey.MoveTo(point);
                    RandomWait.Wait(100, 300);
                    SupperMouseKey.MoveTo(point.Confusion(10, 5));
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.LeftClick();
                    RandomWait.Wait(300, 900);
                    ClipboardHelper.SetText(options.SayHi);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.BACK);
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
                    RandomWait.Wait(600, 2000);
                    token.ThrowIfCancellationRequested();
                }
                if (options != null && !string.IsNullOrWhiteSpace(options.Suffix))
                {
                    var path = "/Group/Group/Group/Group/Group/Group/Text/Edit[@Name='修改备注'][@ClassName='mmui::XLineEdit'] | /Group/Group/Group/Group/Group/Group/Group/Group/Text/Edit[@Name='修改备注'][@ClassName='mmui::XLineEdit']";
                    var edit = addWin.FindFirstByXPath(path);
                    var memoName = edit.GetParent().Name;  //得到名字，但是名字可能是空格等异常情况.
                    if (string.IsNullOrWhiteSpace(memoName))
                    {
                        memoName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());  //如果为空，则得到一个随机名称
                    }
                    if (!memoName.EndsWith($"_{options.Suffix}"))
                    {
                        memoName = $"{memoName}_{options.Suffix}";
                    }
                    var point = edit.BoundingRectangle.SafeRandomPoint();
                    SupperMouseKey.MoveTo(point);
                    RandomWait.Wait(100, 300);
                    SupperMouseKey.MoveTo(point.Confusion(10, 5));
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.LeftClick();
                    RandomWait.Wait(300, 900);
                    ClipboardHelper.SetText(memoName);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.BACK);
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
                    RandomWait.Wait(600, 2000);
                    token.ThrowIfCancellationRequested();

                }
                if (options != null && !string.IsNullOrWhiteSpace(options.Label))
                {
                    var lableItem = addWin.FindFirstByXPath("/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='修改标签'][@AutomationId='button'] | /Group/Group/Group/Group/Group/Group/Button[@Name='修改标签'][@AutomationId='button']");
                    var point = lableItem.BoundingRectangle.SafeRandomPoint();
                    Mouse.MoveTo(point);
                    RandomWait.Wait(100, 600);
                    Mouse.LeftClick();
                    RandomWait.Wait(1000, 2500);
                    //标签名可能已经存在，或者不存在，需要新建.
                    var list = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByClassName("mmui::XTableView")).And(cf.ByName("标签"))).AsListBox();
                    var items = list.FindAllChildren(cf => cf.ByControlType(ControlType.CheckBox));
                    if (items.Select(x => x.Name).Where(x => x.Equals(options.Label)).Count() > 0)
                    {
                        //已经有标签
                        var selectItem = items.FirstOrDefault(x => x.Name.Equals(options.Label));
                        if (selectItem != null)
                        {
                            var point2 = selectItem.BoundingRectangle.SafeRandomPoint();
                            Mouse.MoveTo(point2);
                            Mouse.LeftClick();
                            RandomWait.Wait(300, 900);
                        }
                    }
                    else
                    {
                        //无标签，需要新建
                        var searchEdit = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("搜索")).And(cf.ByClassName("mmui::XValidatorTextEdit")));
                        if (searchEdit != null)
                        {
                            searchEdit.Focus();
                            var point2 = searchEdit.BoundingRectangle.SafeRandomPoint();
                            Mouse.Click(point2);
                            ClipboardHelper.SetText(options.Label);
                            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_V);
                            RandomWait.Wait(300, 900);
                            list = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByClassName("mmui::XTableView")).And(cf.ByName("标签"))).AsListBox();
                            var createItemRetry = Retry.WhileNull(() => list.Items.Where(u => u.Name.Contains("创建新标签") && u.ControlType == ControlType.ListItem).FirstOrDefault(),
                            timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                            if (createItemRetry != null)
                            {
                                var createItem = createItemRetry.Result;
                                point2 = createItem.BoundingRectangle.SafeRandomPoint();
                                Mouse.Click(point2);
                                RandomWait.Wait(300, 900);
                            }
                        }
                    }
                    token.ThrowIfCancellationRequested();
                    var randomRetry = Random.Shared.Next(1, 10);
                    if (randomRetry <= 5)
                    {
                        //点击备注栏
                        var clkItem = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("修改备注").And(cf.ByClassName("mmui::XLineEdit"))));
                        clkItem.Click();
                    }
                    else
                    {
                        //点击“确定上面一点”
                        var button = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定")).And(cf.ByClassName("mmui::XOutlineButton")));
                        if (button != null)
                        {
                            var buttonRect = button.BoundingRectangle;
                            var point2 = new Rectangle(buttonRect.X - 100, buttonRect.Y, 90, buttonRect.Height).SafeRandomPoint();
                            Mouse.MoveTo(point2);
                            Mouse.Click();
                        }
                    }
                }
                token.ThrowIfCancellationRequested();
                //点击确定
                RandomWait.Wait(1000, 3000);
                var confirmButton = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("确定")).And(cf.ByClassName("mmui::XOutlineButton")));
                if (confirmButton != null)
                {
                    SupperMouseKey.MoveTo(confirmButton.BoundingRectangle.Center().Confusion(10, 5));
                    RandomWait.Wait(100, 300);
                    SupperMouseKey.MoveTo(confirmButton.BoundingRectangle.Center().Confusion(10, 5));
                    RandomWait.Wait(300, 900);
                    SupperMouseKey.LeftClick();
                    RandomWait.Wait(1000, 1500);
                }
            }
        }


        private void __ProcessAlreadFriendInfomation(Window win, string friend, AutomationElement wxLabel, CancellationToken token)
        {
            //更新缓存数据.
            FriendInfo friendInfo = new FriendInfo();
            var wxid = wxLabel.GetSibling(1);
            if (wxid != null)
                friendInfo.WxId = wxid.Name;
            var oldFriendInfo = this._Client.GetFriendWithWxIDFromCache(friendInfo.WxId);
            var path = "/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Text[@AutomationId='right_v_view.nickname_button_view.display_name_text'][@ClassName='mmui::XTextView']";
            var labelRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (labelRetry.Success)
            {
                var label = labelRetry.Result.AsLabel();
                friendInfo.NickName = label.Name;
                friendInfo.MemoName = label.Name;
            }

            //获取pane的root
            path = "/Group/Group/Group/Group/Group/Group/Group[@AutomationId='content_v_view'][@ClassName='mmui::ProfileResizeVBoxView']";
            var rootRetry = Retry.WhileNull(() => win.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (!rootRetry.Success)
            {
                return;
            }
            var root = rootRetry.Result;
            //地区
            path = "//Text[@Name='地区：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            var item = root.FindFirstByXPath(path);
            if (item != null)
            {
                item = item.GetSibling(1);
                friendInfo.Area = item?.Name;
            }
            //昵称
            path = "//Text[@Name='昵称：'][@AutomationId='right_v_view.user_info_center_view.basic_line_view.basic_line.key_text']";
            item = root.FindFirstByXPath(path);
            if (item != null)
            {
                item = item.GetSibling(1);
                friendInfo.NickName = item?.Name;
            }
            //头像
            path = "//Button[@AutomationId='head_image_v_view.head_view_']";
            item = root.FindFirstByXPath(path);
            if (item != null)
            {
                path = Path.Combine(AppContext.BaseDirectory, "Avator", $"{friendInfo.WxId}.png");
                item.CaptureToFile(path);
                friendInfo.AvatarPath = path;
            }
            //备注
            path = "//Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.remark_line.value_remark_view.content_view.ProfileTextView'][@ClassName='mmui::ProfileTextView']";
            item = root.FindFirstByXPath(path);
            if (item != null)
            {
                friendInfo.MemoName = item.Name;
            }
            //共同群聊
            path = "//Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.more_line_v_view.chatroom_intersection.value_normal_view.content_view.value_reader_view.value_reader_'][@ClassName='mmui::ProfileTextView']";
            item = root.FindFirstByXPath(path);
            if (item != null)
            {
                friendInfo.SameGroupNumber = item?.Name;
            }
            else
            {
                friendInfo.SameGroupNumber = oldFriendInfo?.SameGroupNumber;
            }
            //标签
            path = "//Text[@AutomationId='content_v_view.ProfileResizeVBoxView.detail_scroll_view.gradient_mask_stacked_view.default_scroll_area.qt_scrollarea_viewport.detail_content_host.detail_center_v_view.detail_derived_content_view.section_shell.main_line_v_view.tag_line.value_normal_view.content_view.value_reader_view.value_reader_'][@ClassName='mmui::ProfileTextView']";
            item = root.FindFirstByXPath(path);
            if (item != null)
            {
                if (!string.IsNullOrWhiteSpace(item.Name))
                {
                    var labelList = item.Name.Split(',').ToList();
                    friendInfo.Lable = labelList;
                }
            }

            friendInfo.Signature = oldFriendInfo?.Signature;
            friendInfo.Source = oldFriendInfo?.Source;
            friendInfo.AddDateTime = oldFriendInfo?.AddDateTime;

            this._Client.AddOrUpdateFriendFromCache(friendInfo);
        }
    }
}

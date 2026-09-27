using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using System.Linq;
using WeAutoCommon.Utils;
using FlaUI.Core.Definitions;
using WeAutoCommon.Models;
using WeAutoCommon.Enums;
using System;
using FlaUI.Core.Tools;
using FlaUI.Core.Conditions;
using System.Text.RegularExpressions;
using WeChatAuto.Utils;
using WeChatAuto.Extentions;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;
using WeAutoCommon.Simulator;
using FlaUI.UIA3;
using System.Threading.Tasks;
using WeAutoCommon.Extentions;
using FlaUI.Core.Input;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.WindowsAPI;
using System.Configuration;



namespace WeChatAuto.Components
{
    /// <summary>
    /// 会话列表
    /// </summary>
    public class ConversationList
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AutoLogger<ConversationList> _logger;
        private UIThreadInvoker _uiThreadInvoker;
        private WeChatClient _Client;
        internal ListBox ConversationRoot => _GetConversationRoot();   //会话列表根结点的查找方法

        internal ConversationList(WeChatClient client, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<AutoLogger<ConversationList>>();
            _uiThreadInvoker = uiThreadInvoker;
            _Client = client;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 设置会话消息免打扰
        /// </summary>
        /// <param name="setting">如果为:true,则设置会话消息免打扰，如果为:false,则：允许消息通知</param>
        /// <param name="who">要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口设置免打扰</param>
        /// <returns>免打扰的状态</returns>
        public async Task<bool> SetDoNotDisturb(string who, bool setting = true)
        {
            if (!string.IsNullOrWhiteSpace(who))
            {
                var result = await WeChatInvoker.Call(SearchWhoCore, who);
                if (!result)
                    return false;
            }
            return await WeChatInvoker.Call(SetDoNotDisturbCore, setting);
        }

        private bool SetDoNotDisturbCore(UIA3Automation automation, bool setting)
        {
            var root = this.ConversationRoot;
            var item = root.Items.FirstOrDefault(x => x.IsSelected);
            if (item == null)
                return false;
            //可能位置不正确,先调整位置
            __ChangeItemPosition(root, item);
            //弹菜单
            item.RightClick();
            RandomWait.Wait(600, 1200);
            var path = "/Window[@Name='Weixin'][@ClassName='mmui::XMenu']";
            var popupWinRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (popupWinRetry.Success)
            {
                var popupMenu = popupWinRetry.Result;
                var menuItem = popupMenu.FindFirstChild(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("允许消息通知").Or(cf.ByName("消息免打扰"))));
                if (setting)  //设置消息免打扰
                {
                    if (menuItem.Name.Equals("允许消息通知"))
                    {
                        SupperMouseKey.TypeSimultaneously(VirtualKeyShort.ESC);
                        return true;
                    }
                    if (menuItem.Name.Equals("消息免打扰"))
                    {
                        menuItem.Click();
                        RandomWait.Wait(300, 900);
                        return true;
                    }
                }
                else
                {
                    //取消消息免打扰
                    if (menuItem.Name.Equals("消息免打扰"))
                    {
                        SupperMouseKey.TypeSimultaneously(VirtualKeyShort.ESC);
                        return true;
                    }
                    if (menuItem.Name.Equals("允许消息通知"))
                    {
                        menuItem.Click();
                        RandomWait.Wait(300, 900);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 设置会话置顶
        /// </summary>
        /// <param name="setting">true:聊天置顶;false:取消聊天置顶</param>
        /// <param name="who">要设置的 好友/群聊 名称,可以为空,如果为空，则为当前窗口置顶</param>
        /// <returns>是否是置顶状态</returns>
        public async Task<bool> SetTopMost(string who, bool setting = true)
        {
            if (!string.IsNullOrWhiteSpace(who))
            {
                var result = await WeChatInvoker.Call(SearchWhoCore, who);
                if (!result)
                    return false;
            }
            return await WeChatInvoker.Call(SetTopMostCore, setting);
        }

        private bool SetTopMostCore(UIA3Automation automation, bool setting)
        {
            var root = this.ConversationRoot;
            var item = root.Items.FirstOrDefault(x => x.IsSelected);
            if (item == null)
                return false;
            //可能位置不正确,先调整位置
            __ChangeItemPosition(root, item);
            //弹菜单
            item.RightClick();
            RandomWait.Wait(600, 1200);
            var path = "/Window[@Name='Weixin'][@ClassName='mmui::XMenu']";
            var popupWinRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (popupWinRetry.Success)
            {
                var popupMenu = popupWinRetry.Result;
                var menuItem = popupMenu.FindFirstChild(cf => cf.ByControlType(ControlType.MenuItem));
                if (setting)  //设置置顶
                {
                    if (menuItem.Name.Equals("取消置顶"))
                    {
                        SupperMouseKey.TypeSimultaneously(VirtualKeyShort.ESC);
                        return true;
                    }
                    if (menuItem.Name.Equals("置顶"))
                    {
                        menuItem.Click();
                        RandomWait.Wait(300, 900);
                        return true;
                    }
                }
                else
                {
                    if (menuItem.Name.Equals("置顶"))
                    {
                        SupperMouseKey.TypeSimultaneously(VirtualKeyShort.ESC);
                        return true;
                    }
                    if (menuItem.Name.Equals("取消置顶"))
                    {
                        menuItem.Click();
                        RandomWait.Wait(300, 900);
                        return true;
                    }
                }
            }

            return false;
        }

        private static void __ChangeItemPosition(ListBox root, ListBoxItem item)
        {
            var point = root.BoundingRectangle.SafeRandomPoint();
            var index = 0;
            while (index < 3)
            {
                if (item.BoundingRectangle.Y < root.BoundingRectangle.Y)
                {
                    MouseScrollHelper.UpStep(point, 3);
                }
                else
                {
                    if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + item.BoundingRectangle.Height <= root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                        break;
                }
                index++;
            }
            index = 0;
            while (index < 3)
            {
                if (item.BoundingRectangle.Y + item.BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                {
                    MouseScrollHelper.DownStep(point, 3);
                }
                else
                {
                    if (item.BoundingRectangle.Y >= root.BoundingRectangle.Y && item.BoundingRectangle.Y + item.BoundingRectangle.Height <= root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                        break;
                }
                index++;
            }
        }

        /// <summary>
        /// 搜索好友/群聊
        /// </summary>
        /// <param name="who">待搜索的好友/群聊昵称,who - 微信会话列表肉眼可见的名称,如果群有备注，则这个who即为备注名</param>
        /// <returns></returns>
        public async Task<bool> Search(string who)
        {
            return await WeChatInvoker.Call(SearchWhoCore, who);
        }

        internal bool SearchWhoCore(UIA3Automation automation, string who)
        {
            var root = ConversationRoot;

            bool result = _SearchLiveConversations(who, root);
            if (!result)
            {
                result = _SearchFromEdit(who, root);
            }

            return result;
        }
        /// <summary>
        /// 如果子窗口存在，关闭子窗口，如果不存在，则不做动作。
        /// </summary>
        /// <param name="who">好友、群的昵称</param>
        /// <returns></returns>
        public async Task CloseSubWin(string who)
        {
            await WeChatInvoker.Call(CloseSubWinCore, who);
        }

        private void CloseSubWinCore(UIA3Automation automation, string who)
        {
            var desktop = automation.GetDesktop();
            var subWinRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("mmui::ChatSingleWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByName(who)))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (subWinRetry.Success)
            {
                var subWin = subWinRetry.Result.AsWindow();
                subWin.Close();
                RandomWait.Wait(100, 800);
            }
        }

        /// <summary>
        /// 打开who指定的子窗口
        /// </summary>
        /// <param name="who">好友、群的昵称</param>
        /// <returns></returns>
        public async Task<Window> OpenSubWin(string who)
        {
            return await WeChatInvoker.Call(OpenSubWinCore, who);
        }

        internal Window OpenSubWinCore(UIA3Automation automation, string who)
        {
            var desktop = automation.GetDesktop();
            var subWinRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("mmui::ChatSingleWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByName(who)))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
            if (subWinRetry.Success)
            {
                var subWin = subWinRetry.Result.AsWindow();
                RandomWait.Wait(300, 900);
                return subWin;
            }
            if (!SearchWhoCore(automation, who))
                return null;
            //打开子窗口
            var croot = ConversationRoot;
            var subList = croot.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByClassName("mmui::ChatSessionCell"))).ToList().Select(u => u.AsListBoxItem()).ToList();
            var clickItem = subList.FirstOrDefault(x => x.IsSelected);

            if (clickItem != null)
            {
                this._Client.MainWindow.Focus();
                clickItem.DoubleClick();

                subWinRetry = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("mmui::ChatSingleWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByName(who)))), TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(200));
                if (subWinRetry.Success)
                {
                    var subWin = subWinRetry.Result.AsWindow();
                    this._Client.MoveWinToMainCenter(subWin);
                    RandomWait.Wait(300, 900);
                    if (!CheckAnchorExist())
                        clickItem.Click();

                    return subWin;
                }
            }

            return null;
        }

        internal bool CheckAnchorExist()
        {
            var path = "/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Group/Button[@Name='聊天记录'][@ClassName='mmui::XButton']";
            var buttonRetry = Retry.WhileNull(() => this._Client.MainWindow.FindFirstByXPath(path), TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(200));
            return buttonRetry.Success;
        }

        //如果会话列表存在who,则直接点击.
        private bool _SearchLiveConversations(string who, ListBox root)
        {
            if (_CheckCurrentSelect(who, root))
                return true;

            AutomationElement FindTarget()
            {
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                foreach (var item in items)
                {
                    var names = item.Name?.Trim().Split('\n');
                    if (names?[0].Trim() == who)
                        return item;
                }
                return null;
            }

            var target = FindTarget();
            if (target == null)
                return false;

            // 二次确认
            var freshTarget = FindTarget();
            if (freshTarget == null)
                return false;

            var rect = freshTarget.BoundingRectangle;

            if (rect.Width <= 0 || rect.Height <= 0)
                return false;
            if (freshTarget.IsOffscreen)
                return false;
            System.Drawing.Point point = default;
            if (freshTarget.BoundingRectangle.Y < root.BoundingRectangle.Y)
            {
                _ScrollUpStep();
                freshTarget = FindTarget();
                rect = freshTarget.BoundingRectangle;
            }
            else
            {
                if (freshTarget.BoundingRectangle.Y + freshTarget.BoundingRectangle.Height > root.BoundingRectangle.Y + root.BoundingRectangle.Height)
                {
                    DownCore();
                    freshTarget = FindTarget();
                    rect = freshTarget.BoundingRectangle;
                }
            }

            point = rect.SafeRandomPoint();
            _Client.MainWindow.Focus();
            Mouse.Click(point);

            return true;
        }

        private bool _CheckCurrentSelect(string who, ListBox root)
        {
            if (root.IsPatternSupported(root.Automation.PatternLibrary.SelectionPattern))
            {
                var s = root.Patterns.Selection.Pattern;
                var v = s.Selection.ValueOrDefault;
                if (v != null && v.Length > 0)
                {
                    var i = v[0];
                    var names = i.Name?.Trim().Split('\n');
                    if (names?[0].Trim() == who)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查是否是选中状态.
        /// </summary>
        /// <returns></returns>
        internal bool CheckSelectState()
        {
            var root = ConversationRoot;
            if (root.IsPatternSupported(root.Automation.PatternLibrary.SelectionPattern))
            {
                var s = root.Patterns.Selection.Pattern;
                var v = s.Selection.ValueOrDefault;
                if (v != null && v.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //如果会话列表不存在who,再查询
        private bool _SearchFromEdit(string who, ListBox root)
        {
            //通过搜索框搜索]
            var path = UITreeGlobal.Search_Bar_Edit;
            var edit = _Client.MainWindow.FindFirstByXPath(path);
            edit.Focus();
            edit.DrawHighlightExt();
            edit.AsTextBox().Text = who;
            RandomWait.Wait(300, 1200);
            //等候浮动菜单出来
            var popWinResult = Retry.WhileNull(() =>
            {
                path = UITreeGlobal.Search_Bar_PopupMenu;
                return _Client.MainWindow.FindFirstByXPath(path).AsWindow();
            }, timeout: TimeSpan.FromSeconds(1), interval: TimeSpan.FromMilliseconds(200));

            if (popWinResult.Success)
            {
                var popupList = popWinResult.Result;
                popupList.DrawHighlightExt();
                var firstItem = popupList.FindFirstChild();
                var fistFlag = true;
                if (firstItem.Name.Equals("群聊") ||
                    firstItem.Name.Equals("联系人") ||
                    firstItem.Name.Equals("最常使用"))
                {
                    AutomationElement item = null;
                    var index = 0;
                    while (item == null || !item.Name.Trim().Equals(who) || index < 30)
                    {
                        if (fistFlag)
                        {
                            item = firstItem.GetSibling(1);
                            fistFlag = false;
                        }
                        else
                        {
                            item = item.GetSibling(1);
                        }
                        if (item == null)
                            break;
                        if (item.Name.Trim().Equals(who))
                        {
                            System.Drawing.Point point = item.BoundingRectangle.SafeRandomPoint();
                            Mouse.Position = point;
                            Mouse.Click(point);
                            RandomWait.Wait(600, 1500);
                            return true;
                        }
                        index++;
                        System.Diagnostics.Debug.WriteLine($"name={item.Name}");
                        if (item.Name.Equals("查看全部") || item.Name.Equals("聊天记录") || item.Name.Equals("收藏") || item.Name.Equals("搜索网络结果"))
                            break;
                    }
                }
                else
                {
                    firstItem = popupList.FindFirstChild(cf => cf.ByName("群聊"));
                    if (firstItem == null)
                    {
                        Keyboard.Type(VirtualKeyShort.ESC);
                        return false;
                    }
                    AutomationElement item = null;
                    var index = 0;
                    while (item == null || !item.Name.Trim().Equals(who) || index < 30)
                    {
                        if (fistFlag)
                        {
                            item = firstItem.GetSibling(1);
                            fistFlag = false;
                        }
                        else
                        {
                            item = item.GetSibling(1);
                        }
                        if (item == null)
                            break;
                        if (item.Name.Trim().Equals(who))
                        {
                            System.Drawing.Point point = item.BoundingRectangle.SafeRandomPoint();
                            Mouse.Position = point;
                            Mouse.Click(point);
                            RandomWait.Wait(600, 1500);
                            return true;
                        }
                        index++;
                        if (item.Name.Equals("查看全部") || item.Name.Equals("聊天记录") || item.Name.Equals("收藏") || item.Name.Equals("搜索网络结果"))
                            break;
                    }
                }
            }
            Keyboard.Type(VirtualKeyShort.ESC);
            return false;
        }

        /// <summary>
        /// 获取会话列表可见会话
        /// 会话信息包含：会话名称、会话未读消息数、会话头像等具体信息，请参考<see cref="SimpleConversation"/>
        /// </summary>
        /// <returns>返回<see cref="Conversation"/>列表</returns>
        public async Task<List<SimpleConversation>> GetVisibleConversations()
        {
            return await WeChatInvoker.Call(GetVisibleConversationsCore);
        }

        internal List<SimpleConversation> GetVisibleConversationsCore(UIA3Automation automation)
        {
            var list = new List<SimpleConversation>();
            var items = GetVisibleConversationElements(automation);
            foreach (var item in items)
            {
                var cItem = GetConversationItemFromName(item.Name);
                list.Add(cItem);
            }
            return list;
        }
        internal AutomationElement[] GetVisibleConversationElements(UIA3Automation automation)
        {
            var root = ConversationRoot;
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            return items;
        }
        internal SimpleConversation GetConversationItemFromName(string name)
        {
            var conversation = new SimpleConversation();
            string[] aryName = name.Split('\n');
            conversation.ConversationTitle = aryName[0].Trim();
            conversation.IsDoNotDisturb = aryName.ToList().Where(u => u.Trim().Equals("消息免打扰")).FirstOrDefault() != null ? true : false;
            conversation.IsTop = aryName.ToList().Where(u => u.Trim().Equals("已置顶")).FirstOrDefault() != null ? true : false;
            var match = Regex.Match(name, @"\[([\d]*)条\]");
            if (match.Success)
            {
                conversation.NotReadNumbr = int.TryParse(match.Groups[1].Value, out int value) ? value : 0;
            }

            return conversation;
        }

        /// <summary>
        /// 获取会话列表所有会话的标题
        /// 考虑到效率，只返回名称列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllConversations()
        {
            return await WeChatInvoker.Call(GetAllConversationsCore);
        }

        internal List<string> GetAllConversationsCore(UIA3Automation automation)
        {
            HashSet<string> list = new HashSet<string>();
            //先最到顶端，然后再往上收集所有标题
            UpCore(automation, (items, rect) => true);
            DownCore(automation, (items, rect) =>
            {
                foreach (var item in items)
                {
                    var aryItems = item.Name.Trim().Split('\n');
                    var title = aryItems[0].Trim();
                    list.Add(title);
                }
                return true;
            });

            return list.ToList();
        }

        /// <summary>
        /// 定位会话
        /// 定位会话的用途：可以将会话列表滚动到指定会话的位置，使指定会话可见
        /// </summary>
        /// <param name="title">会话标题</param>
        /// <returns>如果找到会话，则返回true，否则返回false</returns>
        public async Task<bool> LocateConversation(string title)
        {
            return await WeChatInvoker.Call(LocateConversationCore, title);
        }
        /// <summary>
        /// 会话列表向上滚动，并且执行需要的业务逻辑
        /// </summary>
        /// <param name="callBack">
        /// <para>滚动过程中的回调，建议：如果处理业务结束，返回false,意味着不向上滚动，如果业务没有处理到，则返回true,继续滚动</para>
        /// <para>参数中的Rectangle为会话列表容器的<see cref="Rectangle"/>,如果超出容器Rectangle范围，应该返回true继续滚动，直到可以点击为止</para>
        /// </param>
        /// <returns></returns>
        public async Task Up(Func<AutomationElement[], Rectangle, bool> callBack)
        {
            await WeChatInvoker.Call(UpCore, callBack);
        }


        /// <summary>
        /// 会话列表滚动向下滚动，并且执行需要的业务逻辑
        /// <param name="callBack">
        /// <para>滚动过程中的回调，建议：如果处理业务结束，返回false,意味着不向上滚动，如果业务没有处理到，则返回true,继续滚动</para>
        /// <para>参数中的Rectangle为会话列表容器的<see cref="Rectangle"/>,如果超出容器Rectangle范围，应该返回true继续滚动，直到可以点击为止</para>
        /// </param>
        /// </summary>
        /// <returns></returns>
        public async Task Down(Func<AutomationElement[], Rectangle, bool> callBack)
        {
            await WeChatInvoker.Call(DownCore, callBack);
        }

        internal bool LocateConversationCore(UIA3Automation automation, string title)
        {
            var searchFlag = false;
            _ScrollListBox((items, rect) =>
            {
                foreach (var item in items)
                {
                    string[] splitNames = item.Name.Split('\n');
                    if (splitNames[0].Trim().Equals(title))
                    {
                        if (item.BoundingRectangle.Y < rect.Y || item.BoundingRectangle.Y + item.BoundingRectangle.Height > rect.Y + rect.Height)
                        {

                            if (item.BoundingRectangle.Y + item.BoundingRectangle.Height > rect.Y + rect.Height)
                            {
                                _Client.MainWindow.Focus();
                                var point = rect.SafeRandomPoint();
                                Mouse.Position = point;
                                Mouse.Scroll(-1 * WeAutomation.Config.ConversationInterval);
                                searchFlag = true;
                                return false;
                            }
                            return true;
                        }

                        searchFlag = true;
                        return false;
                    }
                }
                return true;
            });
            return searchFlag;
        }

        /// <summary>
        /// 获取会话列表可见会话标题
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetVisibleConversationTitles()
        {
            return await WeChatInvoker.Call(GetVisibleConversationTitlesCore);
        }

        private List<string> GetVisibleConversationTitlesCore(UIA3Automation automation)
        {
            HashSet<string> list = new HashSet<string>();
            var root = ConversationRoot;
            var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            foreach (var item in items)
            {
                var names = item.Name.Split('\n');
                list.Add(names[0].Trim());
            }
            return list.ToList();
        }

        /// <summary>
        /// 获取会话列表根节点
        /// </summary>
        /// <returns></returns>
        internal ListBox _GetConversationRoot()
        {
            var find = false;
            ListBox root = null;
            while (!find)
            {
                var path = UITreeGlobal.ConversationRootPath;
                root = _Client.MainWindow.FindFirstByXPath(path).AsListBox();
                find = root != null;
                if (!find)
                {
                    _Client.Navigation.SwitchNavigationCore(null, NavigationType.微信);
                    RandomWait.Wait(300, 1000);
                }
            }
            root?.DrawHighlightExt();
            return root;
        }
        //往上滚动
        internal void _ScrollUpStep()
        {
            _Client.MainWindow.Focus();
            var root = this.ConversationRoot;
            var point = root.BoundingRectangle.SafeRandomPoint();

            Mouse.Position = point;
            Mouse.Scroll(WeAutomation.Config.ConversationInterval);
        }
        //往下滚动
        internal void DownCore()
        {
            _Client.MainWindow.Focus();
            var root = this.ConversationRoot;
            var point = root.BoundingRectangle.SafeRandomPoint();

            Mouse.Position = point;
            Mouse.Scroll(WeAutomation.Config.ConversationInterval * -1);
        }
        internal void _ScrollListBox(Func<AutomationElement[], Rectangle, bool> func)
        {
            var root = this.ConversationRoot;

            _Client.MainWindow.Focus();
            var children = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            if (children.Length == 0)
                return;
            if (!func(children, root.BoundingRectangle))
                return;
            //先回到顶端，从顶端开始.
            var point = root.BoundingRectangle.SafeRandomPoint();
            var retryCount = 0;
            var continueFlag = false;
            while (retryCount <= 1)
            {
                _Client.MainWindow.Focus();
                Mouse.Position = point;
                Mouse.Scroll(WeAutomation.Config.ConversationInterval);
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                continueFlag = func(items, root.BoundingRectangle);

                if (!continueFlag)
                    break;
                var item = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem));
                if (item.BoundingRectangle.Y == root.BoundingRectangle.Y)
                {
                    retryCount++;
                }
                else
                {
                    retryCount = 0;
                }
                RandomWait.Wait(200, 1000);
            }
            if (!continueFlag)
                return;
            //从头往下.
            retryCount = 0;
            RandomWait.Wait(100, 500);
            var oldTitle = "";
            while (retryCount <= 2)
            {
                _Client.MainWindow.Focus();
                Mouse.Position = point;
                Mouse.Scroll(-1 * WeAutomation.Config.ConversationInterval);
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));

                continueFlag = func(items, root.BoundingRectangle);

                if (!continueFlag)
                    break;
                RandomWait.Wait(100, 500);
                if (items == null || items.Length == 0)
                    break;
                var lastItem = items[items.Length - 1];
                if (lastItem.Name.Trim() != oldTitle)
                {
                    oldTitle = lastItem.Name.Trim();
                    retryCount = 0;
                }
                else
                {
                    retryCount++;
                }
            }
        }

        internal void UpCore(UIA3Automation automation, Func<AutomationElement[], Rectangle, bool> callBack)
        {
            var root = this.ConversationRoot;
            _Client.MainWindow.Focus();
            var children = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            if (children.Length == 0)
                return;
            //先回到顶端，从顶端开始.
            if (!callBack(children, root.BoundingRectangle))
                return;
            var point = root.BoundingRectangle.SafeRandomPoint();
            var retryCount = 0;
            _Client.MainWindow.Focus();
            while (retryCount <= 2)
            {
                Mouse.Position = point;
                RandomWait.Wait(20, 100);
                Mouse.Scroll(WeAutomation.Config.ConversationInterval);
                RandomWait.Wait(50, 250);
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                if (!callBack(items, root.BoundingRectangle))
                    break;
                var item = root.FindFirstChild(cf => cf.ByControlType(ControlType.ListItem));
                if (item.BoundingRectangle.Y == root.BoundingRectangle.Y)
                {
                    retryCount++;
                }
                else
                {
                    retryCount = 0;
                }
                RandomWait.Wait(50, 250);
            }
        }

        internal void DownCore(UIA3Automation automation, Func<AutomationElement[], Rectangle, bool> callBack)
        {
            var root = this.ConversationRoot;

            _Client.MainWindow.Focus();
            var children = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
            if (children.Length == 0)
                return;
            if (!callBack(children, root.BoundingRectangle))
                return;

            //从当前位置往下.
            var point = root.BoundingRectangle.SafeRandomPoint();
            var retryCount = 0;
            RandomWait.Wait(100, 200);
            var oldTitle = "";
            _Client.MainWindow.Focus();
            while (retryCount <= 2)
            {
                Mouse.Position = point;
                RandomWait.Wait(50, 100);
                Mouse.Scroll(-1 * WeAutomation.Config.ConversationInterval);
                RandomWait.Wait(50, 250);
                var items = root.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                if (items == null || items.Length == 0)
                    break;
                if (!callBack(items, root.BoundingRectangle))
                    break;
                var lastItem = items[items.Length - 1];
                if (lastItem.Name.Trim() != oldTitle)
                {
                    oldTitle = lastItem.Name.Trim();
                    retryCount = 0;
                }
                else
                {
                    retryCount++;
                }
                RandomWait.Wait(50, 300);
            }
        }
    }
}
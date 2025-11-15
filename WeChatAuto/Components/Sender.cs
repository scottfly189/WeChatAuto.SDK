using FlaUI.Core;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;
using WxAutoCommon.Enums;
using WxAutoCommon.Utils;
using WeChatAuto.Utils;
using FlaUI.UIA3.Converters;
using FlaUI.Core.WindowsAPI;
using System;
using WeChatAuto.Extentions;
using WxAutoCommon.Interface;
using System.Text;
using OneOf;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Windows.Controls.Primitives;
using FlaUI.Core.Patterns;

namespace WeChatAuto.Components
{
    /// <summary>
    /// 聊天内容区发送者
    /// </summary>
    public class Sender
    {
        private readonly AutoLogger<Sender> _logger;
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private AutomationElement _SenderRoot;
        private UIThreadInvoker _uiThreadInvoker;
        public TextBox ContentArea => GetContentArea();
        private readonly IServiceProvider _serviceProvider;
        public List<(ChatBoxToolBarType type, Button button)> ToolBarButtons => GetToolBarButtons();
        public Button SendButton => GetSendButton();
        /// <summary>
        /// 聊天内容区发送者构造函数
        /// <param name="window">窗口<see cref="Window"/></param>
        /// <param name="senderRoot">发送者根元素<see cref="AutomationElement"/></param>
        /// <param name="wxWindow">微信窗口封装<see cref="WeChatMainWindow"/></param>
        /// </summary>
        public Sender(Window window, AutomationElement senderRoot, IWeChatWindow wxWindow, string title, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            _Window = window;
            _WxWindow = wxWindow;
            _SenderRoot = senderRoot;
            _uiThreadInvoker = uiThreadInvoker;
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<AutoLogger<Sender>>();
            _logger.Info($"Sender对象使用线程：{uiThreadInvoker.ThreadName}");
        }
        /// <summary>
        /// 获取工具栏按钮
        /// </summary>
        /// <param name="type">工具栏按钮类型</param>
        /// <returns>工具栏按钮</returns>
        public Button GetToolBarButton(ChatBoxToolBarType type)
        {
            var toolBarButtons = GetToolBarButtons();
            return toolBarButtons.FirstOrDefault(btn => btn.type == type).button;
        }
        /// <summary>
        /// 发起语音聊天
        /// </summary>
        public void SendVoiceChat()
        {
            var voiceChatButton = GetToolBarButton(ChatBoxToolBarType.语音聊天);
            voiceChatButton.Invoke();
        }
        /// <summary>
        /// 发起视频聊天
        /// </summary>
        public void SendVideoChat()
        {
            var videoChatButton = GetToolBarButton(ChatBoxToolBarType.视频聊天);
            videoChatButton.Invoke();
        }
        /// <summary>
        /// 获取工具栏按钮
        /// </summary>
        /// <returns>工具栏按钮</returns>
        public List<(ChatBoxToolBarType type, Button button)> GetToolBarButtons()
        {
            var toolBarRoot = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar))).GetAwaiter().GetResult();
            DrawHightlightHelper.DrawHightlight(toolBarRoot, _uiThreadInvoker);
            var buttons = _uiThreadInvoker.Run(automation => toolBarRoot.FindAllChildren(cf => cf.ByControlType(ControlType.Button))).GetAwaiter().GetResult();
            List<Button> buttonList = buttons.Select(btn => btn.AsButton()).ToList();
            List<(ChatBoxToolBarType type, Button button)> toolBarButtons = new List<(ChatBoxToolBarType type, Button button)>
            {
                (ChatBoxToolBarType.表情, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_EMOTION))),
                (ChatBoxToolBarType.发送文件, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_SEND_FILE))),
                (ChatBoxToolBarType.截图, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_SCREENSHOT))),
                (ChatBoxToolBarType.聊天记录, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_CHAT_RECORD))),
                (ChatBoxToolBarType.直播, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_LIVE))),
                (ChatBoxToolBarType.语音聊天, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_VOICE_CHAT))),
                (ChatBoxToolBarType.视频聊天, buttonList.FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_VIDEO_CHAT)))
            };
            return toolBarButtons;
        }
        /// <summary>
        /// 获取输入框
        /// </summary>
        /// <returns>输入框</returns>
        public TextBox GetContentArea()
        {
            var contentArea = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit))).GetAwaiter().GetResult().AsTextBox();
            return contentArea;
        }
        /// <summary>
        /// 获取发送按钮
        /// </summary>
        /// <returns>发送按钮</returns>
        public Button GetSendButton()
        {
            var sendButton = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByText(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_SEND)))).GetAwaiter().GetResult().AsButton();
            DrawHightlightHelper.DrawHightlight(sendButton, _uiThreadInvoker);
            return sendButton;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void SendMessage(string message, List<string> atUserList = null)
        {
            if (atUserList == null || atUserList.Count == 0)
            {
                _WxWindow.SilenceEnterText(ContentArea, message);
                Thread.Sleep(500);
                var button = SendButton;
                _WxWindow.SilenceClickExt(button);
            }
            else
            {
                this._AtUserActionCore(atUserList);
                // Thread.Sleep(500);
                // _WxWindow.SilenceEnterText(ContentArea, message);
                // Thread.Sleep(500);
                // var button = SendButton;
                // _WxWindow.SilenceClickExt(button);
            }
        }
        private void _AtUserActionCore(List<string> atUserList)
        {
            _Window.Focus();
            Thread.Sleep(500);
            ContentArea.Click();
            _uiThreadInvoker.Run(automation =>
            {
                Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
                Keyboard.TypeSimultaneously(VirtualKeyShort.BACK);
                Thread.Sleep(300);
                foreach (var atUser in atUserList)
                {
                    Keyboard.Type("@");
                    Thread.Sleep(300);
                    var listResult = Retry.WhileNull(() => _Window.FindFirstByXPath("//Pane[@Name='ChatContactMenu'][@ClassName='ChatContactMenu']/Pane[2]/List"),
                    timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                    if (listResult.Success)
                    {
                        var list = listResult.Result.AsListBox();
                        list.DrawHighlightExt(_uiThreadInvoker);
                        IScrollPattern pattern = null;
                        if (list.Patterns.Scroll.IsSupported)
                        {
                            pattern = list.Patterns.Scroll.Pattern;
                            pattern.SetScrollPercent(0, 0);
                        }
                        var listItemResult = Retry.WhileNull(() => list.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName(atUser))),
                            timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200));
                        if (listItemResult.Success && listItemResult.Result.Length > 0)
                        {
                            var listItems = listItemResult.Result.ToList();
                            var listItem = listItems.FirstOrDefault(item => item.Name == atUser);
                            if (listItem != null)
                            {
                                if (listItem.IsOffscreen)
                                {
                                    //滚动列表使@用户可见
                                    while (listItem.IsOffscreen)
                                    {
                                        double currentPercent = pattern.VerticalScrollPercent;
                                        double newPercent = Math.Min(currentPercent + pattern.VerticalViewSize, 1);
                                        pattern.SetScrollPercent(0, newPercent);
                                        Thread.Sleep(600);
                                        listItems = Retry.WhileNull(() => list.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName(atUser))),
                                          timeout: TimeSpan.FromSeconds(3), interval: TimeSpan.FromMilliseconds(200)).Result.ToList();
                                        listItem = listItems.FirstOrDefault(item => item.Name == atUser);
                                        if (!listItem.IsOffscreen)
                                        {
                                            var button = listItem.FindFirstByXPath("//Button[@IsOffscreen='false']").AsButton();
                                            button?.WaitUntilClickable();
                                            button?.DrawHighlightExt();
                                            button?.Click();
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    var button = listItem.FindFirstByXPath("//Button").AsButton();
                                    button?.WaitUntilClickable();
                                    button?.DrawHighlightExt();
                                    button?.Click();
                                }
                            }
                            else
                            {
                                _logger.Info($"未找到@用户{atUser},可能未显示在列表中,将采用键盘输入方式输入@用户");
                                this._CustomTypeAtUser(atUser);
                            }
                        }
                        else
                        {
                            _logger.Info($"未找到@用户{atUser},将采用键盘输入方式输入@用户");
                            this._CustomTypeAtUser(atUser);
                        }
                    }
                    else
                    {
                        _logger.Warn($"未找到@用户菜单窗口");
                    }
                }
            }).GetAwaiter().GetResult();
        }
        private void _CustomTypeAtUser(string atUser)
        {

        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public void SendMessage(string message, OneOf<string, string[]> atUser = default)
        {
            var atUserList = new List<string>();
            if (atUser.Value != default)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        atUserList.Add(user);
                    },
                    (string[] atUsers) =>
                    {
                        atUserList.AddRange(atUsers);
                    }
                );
            }
            this.SendMessage(message, atUserList);
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="files">文件路径列表</param>
        public void SendFile(string[] files)
        {
            _WxWindow.SilencePasteSimple(files, ContentArea);

            var button = SendButton;
            _WxWindow.SilenceClickExt(button);
        }
        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="emoji">表情名称或者描述或者索引</param>
        public void SendEmoji(OneOf<int, string> emoji, List<string> atUserList = null)
        {
            var message = "";
            emoji.Switch(
                (int emojiId) =>
                {
                    message = EmojiListHelper.Items.FirstOrDefault(item => item.Index == emojiId)?.Value ?? EmojiListHelper.Items[0].Value;
                },
                (string emojiName) =>
                {
                    message = emojiName;
                    if (!(message.StartsWith("[") && message.EndsWith("]")))
                    {
                        message = EmojiListHelper.Items.FirstOrDefault(item => item.Description == emojiName)?.Value ?? message;
                    }
                }
            );
            this.SendMessage(message, atUserList);
        }
    }
}
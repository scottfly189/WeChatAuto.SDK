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
using WxAutoCore.Utils;
using FlaUI.UIA3.Converters;
using FlaUI.Core.WindowsAPI;
using System;
using WxAutoCore.Extentions;
using WxAutoCommon.Interface;
using System.Text;
using OneOf;
using WeAutoCommon.Classes;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区发送者
    /// </summary>
    public class Sender
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private AutomationElement _SenderRoot;
        private UIThreadInvoker _uiThreadInvoker;
        public TextBox ContentArea => GetContentArea();
        public List<(ChatBoxToolBarType type, Button button)> ToolBarButtons => GetToolBarButtons();
        public Button SendButton => GetSendButton();
        /// <summary>
        /// 聊天内容区发送者构造函数
        /// <param name="window">窗口<see cref="Window"/></param>
        /// <param name="senderRoot">发送者根元素<see cref="AutomationElement"/></param>
        /// <param name="wxWindow">微信窗口封装<see cref="WeChatMainWindow"/></param>
        /// </summary>
        public Sender(Window window, AutomationElement senderRoot, IWeChatWindow wxWindow, string title, UIThreadInvoker uiThreadInvoker)
        {
            _Window = window;
            _WxWindow = wxWindow;
            _SenderRoot = senderRoot;
            _uiThreadInvoker = uiThreadInvoker;
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
            var toolBarRoot = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar))).Result;
            DrawHightlightHelper.DrawHightlight(toolBarRoot, _uiThreadInvoker);
            var buttons = _uiThreadInvoker.Run(automation => toolBarRoot.FindAllChildren(cf => cf.ByControlType(ControlType.Button))).Result;
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
            var contentArea = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit))).Result.AsTextBox();
            return contentArea;
        }
        /// <summary>
        /// 获取发送按钮
        /// </summary>
        /// <returns>发送按钮</returns>
        public Button GetSendButton()
        {
            var sendButton = _uiThreadInvoker.Run(automation => _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByText(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_SEND)))).Result.AsButton();
            DrawHightlightHelper.DrawHightlight(sendButton, _uiThreadInvoker);
            return sendButton;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void SendMessage(string message)
        {
            _WxWindow.SilenceEnterText(ContentArea, message);
            var button = SendButton;
            _WxWindow.SilenceClickExt(button);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的用户</param>
        public void SendMessage(string message, OneOf<string, string[]> atUser = default)
        {
            if (atUser.Value != default)
            {
                atUser.Switch(
                    (string user) =>
                    {
                        message = $"@{user} {message}";
                    },
                    (string[] atUsers) =>
                    {
                        var atUserList = atUsers.ToList();
                        var atUserString = "";
                        atUserList.ForEach(user =>
                        {
                            atUserString += $"@{user} ";
                        });
                        message = $"{atUserString} {message}";
                    }
                );
            }
            this.SendMessage(message);
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
        public void SendEmoji(OneOf<int, string> emoji)
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
            this.SendMessage(message);
        }
    }
}
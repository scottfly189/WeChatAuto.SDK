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

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区发送者
    /// </summary>
    public class Sender
    {
        private Window _Window;
        private IWeChatWindow _WxWindow;
        private string _Title;
        private AutomationElement _SenderRoot;
        public TextBox ContentArea => GetContentArea();
        public List<(ChatBoxToolBarType type, Button button)> ToolBarButtons => GetToolBarButtons();
        public Button SendButton => GetSendButton();
        /// <summary>
        /// 聊天内容区发送者构造函数
        /// <param name="window">窗口<see cref="Window"/></param>
        /// <param name="senderRoot">发送者根元素<see cref="AutomationElement"/></param>
        /// <param name="wxWindow">微信窗口封装<see cref="WeChatMainWindow"/></param>
        /// </summary>
        public Sender(Window window, AutomationElement senderRoot,IWeChatWindow wxWindow,string title)
        {
            _Window = window;
            _WxWindow = wxWindow;
            _SenderRoot = senderRoot;
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
            var toolBarRoot = _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar));
            DrawHightlightHelper.DrawHightlight(toolBarRoot);
            var buttons = toolBarRoot.FindAllChildren(cf => cf.ByControlType(ControlType.Button));
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
            ///Pane[2]/Pane/Pane[1]/Edit
            // var xPath = "/Pane[2]/Pane/Pane[1]/Edit";
            // var contentAreaRoot = _SenderRoot.FindFirstByXPath(xPath);
            // var contentArea = contentAreaRoot.AsTextBox();
            var contentArea = _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit)).AsTextBox();
            return contentArea;
        }
        /// <summary>
        /// 获取发送按钮
        /// </summary>
        /// <returns>发送按钮</returns>
        public Button GetSendButton()
        {
            // var xPath = "/Pane/Pane/Pane/Pane/Pane/Button";
            // var senderBotton = _SenderRoot.FindAllByXPath(xPath);
            // var sendButton = senderBotton.ToList().Select(btn => btn.AsButton()).FirstOrDefault(btn => btn.Name.Contains(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_SEND));
            var sendButton = _SenderRoot.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByText(WeChatConstant.WECHAT_CHAT_BOX_CONTENT_SEND))).AsButton();
            DrawHightlightHelper.DrawHightlight(sendButton);
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
    }
}
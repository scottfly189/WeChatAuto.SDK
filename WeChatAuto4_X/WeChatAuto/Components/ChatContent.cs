using System.Linq;
using System.Text.RegularExpressions;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WeAutoCommon.Enums;
using WeAutoCommon.Interface;
using WeChatAuto.Utils;
using WeAutoCommon.Utils;
using System;
using Microsoft.Extensions.DependencyInjection;
using FlaUI.Core.Tools;
using System.Drawing;
using FlaUI.Core.Capturing;
using WeChatAuto.Extentions;
using OneOf;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatAuto.Models;
using FlaUI.UIA3;
using FlaUI.Core.WindowsAPI;
using WeChatAuto.Options;

namespace WeChatAuto.Components
{
    public class ChatContent
    {
        private readonly AutoLogger<ChatContent> _logger;
        private UIThreadInvoker _uiMainThreadInvoker;
        private readonly IServiceProvider _serviceProvider;
        private WeChatClient _Client;
        private ChatHeader _Header;
        private MessageBubbleList _MessageList;
        private Sender _Sender;

        internal Sender Sender => _Sender;
        internal MessageBubbleList MessageBubbleList => _MessageList;
        internal ChatHeader ChatHeader => _Header;

        internal AutomationElement Root
        {
            get
            {
                var path = @"/Group/Custom/Group/Group/Group/Custom/Custom/Custom/Group/Custom/Custom/Group[@AutomationId='chat_message_page']";
                var itemResult = Retry.WhileNull(() => _Client.MainWindow.FindFirstByXPath(path), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
                return itemResult.Success ? itemResult.Result : null;
            }
        }

        public ChatContent(WeChatClient client, UIThreadInvoker uiThreadInvoker, IServiceProvider serviceProvider)
        {
            this._Client = client;
            _logger = serviceProvider.GetRequiredService<AutoLogger<ChatContent>>();
            _uiMainThreadInvoker = uiThreadInvoker;
            _serviceProvider = serviceProvider;
            _Sender = new Sender(this._Client, _uiMainThreadInvoker, serviceProvider, this);
            _Header = new ChatHeader(this._Client, serviceProvider, _uiMainThreadInvoker, this);
            _MessageList = new MessageBubbleList(this._Client, _uiMainThreadInvoker, this, serviceProvider);
        }

        /// <summary>
        /// 当前窗口的Sender输入区域点击，以获得焦点，也可以取消系统的消息提醒或者关闭右侧Pane等作用
        /// </summary>
        /// <returns></returns>
        public async Task FocuseSenderInput() => await this.Sender.FocuseSenderInput();

        /// <summary>
        /// 关闭查询窗口,如果查询窗口打开则关闭，如果查询窗口没有打开，则不作动作
        /// </summary>
        /// <param name="who">关闭谁的查询窗口</param>
        /// <returns></returns>
        public async Task CloseSearchWindow(string who)
        {
            await WeChatInvoker.Call(CloseSearchWindowCore, who);
        }

        internal void CloseSearchWindowCore(UIA3Automation automation, string who)
        {
            var desktop = automation.GetDesktop();
            Window subWin = null;
            var winResult = Retry.WhileNull(() => desktop.FindAllChildren(cf => cf.ByClassName("mmui::SearchMsgUniqueChatWindow").And(cf.ByControlType(ControlType.Window).And(cf.ByProcessId(_Client.MainWindow.Properties.ProcessId)))), timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            if (winResult.Success && winResult.Result.Length > 0)
            {
                //发现窗口，但不确定是否是本窗口
                var subWins = winResult.Result;
                subWin = subWins.FirstOrDefault(u =>
                {
                    var name = u.Name.Replace("“", "").Replace("”", "");
                    if (name.Contains($"{who}"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }).AsWindow();
                if (subWin != null)
                {
                    subWin.Close();
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="who">好友或者群聊的名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友</param>
        /// <param name="refer">引用的对话内容,请参考<see cref="ChatRefer"/></param>
        public async Task SendMessage(string who, string message, OneOf<string, string[], List<string>> atUser = default, ChatRefer refer = null)
          => await Sender.SendMessage(who, message, atUser, refer);
        /// <summary>
        /// 发送消息,给当前窗口发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">被@的好友</param>
        /// <param name="refer">引用的对话内容,请参考<see cref="ChatRefer"/></param>
        public async Task SendMessage(string message, OneOf<string, string[], List<string>> atUser = default, ChatRefer refer = null)
            => await Sender.SendMessage(message, atUser, refer);

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="who">好友/群聊，可以为空,如果为空，则发送到当前聊天窗口</param>
        /// <param name="files">文件路径列表</param>
        public async Task SendFile(string who, string[] files) => await Sender.SendFile(who, files);

        /// <summary>
        /// 发送表情
        /// </summary>
        /// <param name="who">好友或者群聊的名称</param>
        /// <param name="emoji">表情名称或者描述或者索引</param>
        /// <param name="atUserList">被@的好友列表</param>
        public async Task SendEmoji(string who, OneOf<int, string> emoji, List<string> atUserList = null) => await Sender.SendEmoji(who, emoji, atUserList);

        /// <summary>
        /// 发起单人语音聊天
        /// </summary>
        /// <param name="who">好友昵称,可以为空，如果为空，则发送到当前聊天窗口</param>
        public async Task SendVoiceChat(string who) => await Sender.SendVoiceChat(who);

        /// <summary>
        /// 发起单人视频聊天
        /// </summary>
        /// <param name="who">好友昵称,可以为空，如果为空，则发送到当前聊天窗口</param>
        public async Task SendVedioChat(string who) => await Sender.SendVedioChat(who);

        /// <summary>
        /// 发起多人语音聊天，适用于群聊发起语音聊天
        /// </summary>
        /// <param name="who">群聊名称,可以为空，如果为空，则发送到当前聊天窗口</param>
        /// <param name="partner">参与者，好友昵称列表,必须是群聊成员</param>
        public async Task SendVoiceChats(string who, string[] partner) => await Sender.SendVoiceChats(who, partner);

        /// <summary>
        /// 发送语音消息,此功能依赖虚拟声卡：Cable input/Cable output
        /// 请在声音-->设置-->将输入设备改成: Cable output
        /// 如果没有安装虚拟声卡，请在:https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip下载
        /// </summary>
        /// <param name="who">好友昵称或群聊名称</param>
        /// <param name="filePath">语音文件路径</param>
        public async Task SendVoiceMessage(string who, string filePath) => await Sender.SendVoiceMessage(who, filePath);


        /// <summary>
        /// 文字转语音发送
        /// 工作原理： 通过音频大模型从文字转成语音后，再通过微信发送指定的好友/群聊
        /// 注：系统默认支持: 阿里千问 Qwen3-TTS系列 模型
        /// 为什么选择阿里千问 Qwen3-TTS系列 模型？
        /// 1. 阿里千问 Qwen3-TTS系列 在国际上的语音合成领域也是第一T队;
        /// 2. 完美支持：声音克隆、声音设计、可以通过指令方便控制语速、情感和语言风格、聊天自然，可以停顿、笑等、为未来的AI 电话/语音 聊天做准备
        /// </summary>
        /// <param name="apiKey">千问的api key</param>
        /// <param name="who">好友或者群聊，可以为空，如果为空，则为当前焦点聊天窗口</param>
        /// <param name="message">文本消息</param>
        /// <param name="options">声音选项，用于指定模型、音色等</param>
        /// <param name="optimizeWithLlm">待发送消息是否需要LLM优化</param>
        /// <param name="customProcess">如果系统提供的大模型不满足使用，可以自定义文字转语音方法</param>
        /// <returns></returns>
        public async Task SendVoiceMessageWithTTS(string who, string apiKey, string message, VoiceOptions options, bool optimizeWithLlm = false, Func<string, string> customProcess = null) => await Sender.SendVoiceMessageWithTTS(who, apiKey, message, options,optimizeWithLlm, customProcess);

        /// <summary>
        /// 给本聊天窗口发送语音消息，请确保本聊天窗口可用.
        /// 请在声音-->设置-->将输入设备改成: Cable output
        /// 如果没有安装虚拟声卡，请在:https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip下载
        /// </summary>
        /// <param name="filePath">语音文件路径</param>
        /// <returns></returns>
        public async Task SendVoiceMessage(string filePath) => await Sender.SendVoiceMessage(filePath);

        /// <summary>
        /// 根据日期获取当前聊天窗口的聊天历史
        /// </summary>
        /// <param name="date">查询日期,如果为空则为当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(DateTime date = default) => await _MessageList.GetChatHistory(date);

        /// <summary>
        /// 根据日期获取聊天历史
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称</param>
        /// <param name="date">查询日期,如果不传，则是当天日期</param>
        /// <returns>返回<see cref="ChatSimpleMessage"/>列表</returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime date = default) => await _MessageList.GetChatHistory(who, date);
        /// <summary>
        /// 获取一段时间的(开始时间与结束时间)聊天历史记录
        /// </summary>
        /// <param name="who">微信名称，可以是好友/群聊的微信名称</param>
        /// <param name="startDate">开始日期,支持时、分、秒</param>
        /// <param name="endDate">结束日期，支持时、分、秒</param>
        /// <returns></returns>
        public async Task<List<ChatSimpleMessage>> GetChatHistory(string who, DateTime startDate, DateTime endDate) => await _MessageList.GetChatHistory(who, startDate, endDate);
    }
}
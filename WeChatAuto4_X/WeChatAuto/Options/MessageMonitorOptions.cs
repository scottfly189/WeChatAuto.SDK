using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OneOf;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Models;
using FlaUI.Core.AutomationElements;
using Newtonsoft.Json;

namespace WeChatAuto.Options
{
    /// <summary>
    /// 消息监听器选项.
    /// </summary>
    public class MessageMonitorOptions
    {
        /// <summary>
        /// 如果此好友在缓存中不存在，是否获取此好友的用户信息(包括wxid),并更新缓存，对于基于wxid的企业级开发很有用
        /// </summary>
        [JsonProperty("fetch_friend_info")]
        public bool FetchFriendInfo { get; set; } = false;

        /// <summary>
        /// 如果聊天记录中有图片，是否获取图片
        /// </summary>
        [JsonProperty("fetch_image")]
        public bool FetchImage { get; set; } = false;

        /// <summary>
        /// 如果聊天记录有微信语音，则取出微信语音的内容，这个依赖微信的设置: 设置 --> 通用 --> 打开"聊天中的语音消息自动转成文字"
        /// </summary>
        [JsonProperty("fetch_voice_chat")]
        public bool FetchVoiceChat { get; set; } = false;
        /// <summary>
        /// 如果聊天记录中有红包、转账，是否点击
        /// </summary>
        [JsonProperty("click_red_envelope")]
        public bool ClickRedEnvelope { get; set; } = false;

        /// <summary>
        /// 手动处理消息，SDK只默认处理了文字消息、微信语音、图片消息、红包/转账消息，其他的消息可以自行处理，如：自行处理打开链接抓取链接内容等.
        /// </summary>
        public Action<AutomationElement, SimpleMessageBubble> CustomProcessMessageAction = null;
    }
}
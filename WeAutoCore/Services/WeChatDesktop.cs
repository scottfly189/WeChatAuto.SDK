using System;
using System.Collections.Generic;
using System.Linq;
using OneOf;
using WeAutoCore.Models;
using WxAutoCommon.Configs;
using WxAutoCommon.Models;
using WxAutoCore.Components;

namespace WxAutoCore.Services
{
    /// <summary>
    /// 微信PC端自动化核心服务
    /// </summary>
    public class WeChatDesktop
    {
        private readonly WeChatFramwork _wxFramwork;
        private Dictionary<string, WeChatClient> _wxClientList = new Dictionary<string, WeChatClient>();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WeChatDesktop(WeChatFramwork weChatFramwork)
        {
            _wxFramwork = weChatFramwork;
            _wxClientList = _wxFramwork.GetWxClientList();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUser">发送给谁,可单人，也可以为一数组人群</param>
        /// <param name="atUser">@谁,可单人，也可以为一数组人群</param>
        /// <param name="clientName">微信客户端名称,如果只有一个客户端，则可以不传</param>
        /// <param name="apiKey">接口KEY,非Pro接口不需要传</param>
        /// <param name="isOPenWindow">是否打开窗口,默认打开</param>
        /// <returns>微信响应结果</returns>
        public ChatResponse SendMessage(string message,
                                      OneOf<string, string[]> toUser,
                                      OneOf<string, string[]> @user = default,
                                      bool isOPenWindow = true,
                                      string clientName = "",
                                      string apiKey = "")
        {
            return this.SendMessage(new ChatMessageInner()
            {
                Message = message,
                ToUser = toUser,
                AtUser = @user,
                IsNewWindow = isOPenWindow,
                ClientName = clientName,
                ApiKey = apiKey
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息体,<see cref="ChatMessageInner"/></param>
        /// <returns>微信响应结果</returns>
        private ChatResponse SendMessage(ChatMessageInner message)
        {
            var wxClient = GetWxClient(message.ClientName);
            try
            {
                var wxMainWindow = wxClient.WxMainWindow;
                if (!string.IsNullOrEmpty(message.ApiKey))
                {
                    WeAutomation.Config.ApiKey = message.ApiKey;
                }

                if (message.IsNewWindow)
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await wxMainWindow.SendWhoAndOpenChat(toUser, message.Message, message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            wxMainWindow.SendWhosAndOpenChat(toUsers, message.Message, message.AtUser);
                        }
                    );
                }
                else
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await wxMainWindow.SendWho(toUser, message.Message, message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            wxMainWindow.SendWhos(toUsers, message.Message, message.AtUser);
                        }
                    );
                }
            }
            catch (System.Exception ex)
            {
                return new ChatResponse()
                {
                    IsSuccess = false,
                    Message = $"发送失败:{ex.Message}"
                };
            }

            return new ChatResponse()
            {
                IsSuccess = true,
                Message = "发送成功"
            };
        }

        /// <summary>
        /// 获取微信客户端
        /// </summary>
        /// <param name="clientName">微信客户端名称</param>
        /// <returns>微信客户端</returns>
        /// <exception cref="Exception"></exception>
        private WeChatClient GetWxClient(string clientName)
        {
            if (_wxClientList.Count() == 0)
            {
                throw new Exception("微信客户端不存在，请检查微信是否打开");
            }
            if (string.IsNullOrEmpty(clientName))
            {
                return _wxClientList.First().Value;
            }
            if (_wxClientList.ContainsKey(clientName))
            {
                return _wxClientList[clientName];
            }
            throw new Exception($"微信客户端[{clientName}]不存在，请检查微信是否打开");
        }
    }
}
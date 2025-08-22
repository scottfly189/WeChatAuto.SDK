using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OneOf;
using WxAutoCommon.Models;
using WxAutoCore.Components;

namespace WxAutoCore.Services
{
    /// <summary>
    /// 微信PC端自动化核心服务
    /// </summary>
    public class WeChatPCCore
    {
        private WxFramwork _wxFramwork;
        private string _nickName;
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WeChatPCCore()
        {
            _wxFramwork = new WxFramwork();
        }
        /// <summary>
        /// 带昵称参数的构造函数
        /// </summary>
        /// <param name="nickName"></param>
        public WeChatPCCore(string nickName) : this()
        {
            _nickName = nickName;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toUser">发送给谁,可单人，也可以为一数组人群</param>
        /// <param name="User">发送给谁</param>
        /// <param name="isOPenWindow">是否打开窗口</param>
        /// <returns>微信响应结果</returns>
        public WxResponse SendMessage(string message,
                                      OneOf<string, string[]> toUser,
                                      OneOf<string, string[]> @user = default,
                                      bool isOPenWindow = false,
                                      string apiKey = "")
        {
            return this.SendMessage(new WxMessage()
            {
                Message = message,
                ToUser = toUser,
                AtUser = @user,
                IsNewWindow = isOPenWindow,
                ApiKey = apiKey
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息体,<see cref="WxMessage"/></param>
        /// <returns>微信响应结果</returns>
        public WxResponse SendMessage(WxMessage message)
        {
            CheckNickName();
            try
            {
                if (!string.IsNullOrEmpty(message.ApiKey))
                {
                    WxConfig.ApiKey = message.ApiKey;
                }
                var client = _wxFramwork.GetWxClient(_nickName);
                if (client == null)
                {
                    throw new Exception($"未找到微信窗口,可能微信客户端昵称:{_nickName}不正确");
                }
                var window = client.WxWindow;
                if (window == null)
                {
                    throw new Exception($"未找到微信窗口,可能微信客户端昵称:{_nickName}不正确");
                }
                if (message.IsNewWindow)
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await client.WxWindow.SendWhoAndOpenChat(toUser, message.Message,message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            client.WxWindow.SendWhosAndOpenChat(toUsers, message.Message,message.AtUser);
                        }
                    );
                }
                else
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await client.WxWindow.SendWho(toUser, message.Message,message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            client.WxWindow.SendWhos(toUsers, message.Message,message.AtUser);
                        }
                    );
                }
            }
            catch (System.Exception ex)
            {
                return new WxResponse()
                {
                    IsSuccess = false,
                    Message = $"发送失败:{ex.Message}"
                };
            }

            return new WxResponse()
            {
                IsSuccess = true,
                Message = "发送成功"
            };
        }
        /// <summary>
        /// 检查昵称是否为空
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckNickName()
        {
            if (string.IsNullOrEmpty(_nickName))
            {
                throw new Exception("昵称不能为空");
            }
        }
    }
}
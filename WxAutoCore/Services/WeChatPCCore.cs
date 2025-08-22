using System;
using System.Collections.Concurrent;
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
        private WxClient _wxClient;
        private WxMainWindow _wxMainWindow;
        /// <summary>
        /// 微信主窗口
        /// <see cref="WxMainWindow"/>
        /// </summary>
        public WxMainWindow WxMainWindow => _wxMainWindow;
        private string _ClientName;
        public string ClientName => _ClientName;
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
        /// <param name="clientName">微信客户端名称</param>
        public WeChatPCCore(string clientName) : this()
        {
            _ClientName = clientName;
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
                _InitWxClient();
                if (message.IsNewWindow)
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await _wxMainWindow.SendWhoAndOpenChat(toUser, message.Message, message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            _wxMainWindow.SendWhosAndOpenChat(toUsers, message.Message, message.AtUser);
                        }
                    );
                }
                else
                {
                    message.ToUser.Switch(
                        async (string toUser) =>
                        {
                            await _wxMainWindow.SendWho(toUser, message.Message, message.AtUser);
                        },
                        (string[] toUsers) =>
                        {
                            _wxMainWindow.SendWhos(toUsers, message.Message, message.AtUser);
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
        /// 关闭所有子窗口
        /// </summary>
        public void CloseAllSubWindow()
        {
            _wxClient.WxWindow.SubWinList.CloseAllSubWins();
        }

        /// <summary>
        /// 关闭指定子窗口
        /// </summary>
        /// <param name="nickName">好友昵称，或者窗口名称</param>
        public void CloseSubWindow(string nickName)
        {
            _wxClient.WxWindow.SubWinList.CloseSubWin(nickName);
        }
        /// <summary>
        /// 初始化微信客户端
        /// </summary>
        private void _InitWxClient()
        {
            if (_wxClient == null)
            {
                _wxClient = _wxFramwork.GetWxClient(_ClientName);
                if (_wxClient == null)
                {
                    throw new Exception($"未找到微信窗口,可能微信客户端昵称:{_ClientName}不正确");
                }
                _wxMainWindow = _wxClient.WxWindow;
                if (_wxMainWindow == null)
                {
                    throw new Exception($"未找到微信窗口,可能微信客户端昵称:{_ClientName}不正确");
                }
            }
        }
        /// <summary>
        /// 检查昵称是否为空
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckNickName()
        {
            if (string.IsNullOrEmpty(_ClientName))
            {
                throw new Exception("昵称不能为空");
            }
        }
    }
}
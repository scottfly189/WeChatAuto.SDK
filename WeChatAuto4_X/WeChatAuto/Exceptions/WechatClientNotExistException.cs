using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeChatAuto.Utils;
using System;

namespace WeChatAuto.Exceptions
{
    /// <summary>
    /// 微信客户端不存在异常
    /// </summary>
    public class WechatClientNotExistException : System.Exception
    {
        public WechatClientNotExistException()
        {
        }

        public WechatClientNotExistException(string message)
            : base(message)
        {
        }

        public WechatClientNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
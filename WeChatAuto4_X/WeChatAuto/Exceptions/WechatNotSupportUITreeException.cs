using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeChatAuto.Utils;
using System;

namespace WeChatAuto.Exceptions
{
    /// <summary>
    /// 微信 没有公开UI Tree
    /// </summary>
    public class WechatNotSupportUITreeException : System.Exception
    {
        public WechatNotSupportUITreeException()
        {
        }

        public WechatNotSupportUITreeException(string message)
            : base(message)
        {
        }

        public WechatNotSupportUITreeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
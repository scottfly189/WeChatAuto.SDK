using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeChatAuto.Utils;
using System;

namespace WeChatAuto.Exceptions
{
    /// <summary>
    /// 微信没有打开异常
    /// </summary>
    public class WechatNotOpenedException : System.Exception
    {
        public WechatNotOpenedException()
        {
        }

        public WechatNotOpenedException(string message)
            : base(message)
        {
        }

        public WechatNotOpenedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
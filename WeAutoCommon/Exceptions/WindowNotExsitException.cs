using System;

namespace WxAutoCommon.Exceptions
{
    /// <summary>
    /// 窗口不存在异常
    /// </summary>
    public class WindowNotExsitException : Exception
    {
        public WindowNotExsitException(string message) : base(message)
        {
        }
    }
}
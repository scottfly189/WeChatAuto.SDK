using System.Runtime.InteropServices;

namespace WeChatAuto.Utils
{
    public static class DpiAwareness
    {
        /// <summary>
        /// 设置进程DPI感知(旧方法)
        /// 注意：此方法必须在任何窗口创建之前调用，如果使用库的应用已经设置DPI感知，调用此方法无效
        /// </summary>
        /// <returns>是否成功</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();
        /// <summary>
        /// 设置进程DPI感知(新方法)
        /// 注意：此方法必须在任何窗口创建之前调用
        /// </summary>
        /// <param name="value">DPI感知值</param>
        /// <returns>是否成功</returns>
        [DllImport("Shcore.dll")]
        public static extern int SetProcessDpiAwareness(int value);
    }
}
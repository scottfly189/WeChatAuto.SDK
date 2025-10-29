using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;

namespace WxAutoCommon.Utils
{
    public static class DpiHelper
    {
        // ---- DPI 感知级别 ----
        public enum ProcessDpiAwareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        // ---- DPI_AWARENESS_CONTEXT ----
        private enum DpiAwarenessContext
        {
            Unaware = 16,
            SystemAware = 17,
            PerMonitorAware = 18,
            PerMonitorAwareV2 = 34
        }

        // ---- API 声明 ----
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool GetProcessDpiAwareness(IntPtr hProcess, out ProcessDpiAwareness awareness);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDpiAwarenessContextForWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool AreDpiAwarenessContextsEqual(IntPtr dpiContextA, IntPtr dpiContextB);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("user32.dll")]
        private static extern bool IsProcessDPIAware();

        [DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hwnd);

        // ---- 判断是否 Per-Monitor V2 感知 ----
        public static bool IsPerMonitorV2Aware(IntPtr hwnd)
        {
            IntPtr ctx = GetDpiAwarenessContextForWindow(hwnd);
            return AreDpiAwarenessContextsEqual(ctx, (IntPtr)DpiAwarenessContext.PerMonitorAwareV2);
        }



        // ---- 获取进程级 DPI 感知 ----
        public static ProcessDpiAwareness GetProcessAwareness(IntPtr process)
        {
            GetProcessDpiAwareness(process, out var awareness);
            return awareness;
        }

        // ---- 获取窗口 DPI ----
        public static uint GetWindowDpi(IntPtr hwnd)
        {
            try
            {
                return GetDpiForWindow(hwnd);
            }
            catch
            {
                return 96; // 默认逻辑DPI
            }
        }

        /// <summary>
        /// 获取DPI感知的坐标
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="point">坐标</param>
        /// <returns>DPI坐标</returns>
        public static Point GetDpiAwarePoint(this Window window, Point point)
        {
            var dpi = GetWindowDpi(window.Properties.NativeWindowHandle.Value);
            return new Point((int)(point.X * dpi / 96), (int)(point.Y * dpi / 96));
        }
        /// <summary>
        /// 获取DPI感知的坐标
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>DPI坐标</returns>
        public static Point GetDpiAwarePoint(this Window window, int x, int y)
          => window.GetDpiAwarePoint(new Point(x, y));
        /// <summary>
        /// 获取DPI感知的元素中心点坐标
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        /// <returns>DPI坐标</returns>
        public static Point GetDpiAwarePoint(this Window window, AutomationElement element)
        {
            var dpi = GetWindowDpi(window.Properties.NativeWindowHandle.Value);
            var point = element.BoundingRectangle.Center();
            return GetDpiAwarePoint(window, point);
        }

        /// <summary>
        /// 获取窗口DPI
        /// </summary>
        /// <param name="window">窗口</param>
        /// <returns>DPI</returns>
        public static int GetWindowDpi(this Window window)
        {
            return (int)GetWindowDpi(window.Properties.NativeWindowHandle.Value);
        }
    }
}

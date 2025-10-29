using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;

namespace WeAutoCommon.Utils
{
    public static class DpiHelper
    {
        private const string USER32 = "user32.dll";
        private const string SHCORE = "shcore.dll";

        public enum ProcessDpiAwareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport(USER32)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport(USER32)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport(USER32)]
        private static extern bool IsProcessDPIAware();

        [DllImport(SHCORE)]
        private static extern int GetProcessDpiAwareness(IntPtr hProcess, out ProcessDpiAwareness awareness);

        [DllImport(USER32)]
        private static extern uint GetDpiForWindow(IntPtr hwnd);

        private const uint PROCESS_QUERY_INFORMATION = 0x0400;

        // 动态加载函数
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private delegate IntPtr GetDpiAwarenessContextForWindowDelegate(IntPtr hwnd);
        private delegate bool AreDpiAwarenessContextsEqualDelegate(IntPtr a, IntPtr b);

        private static GetDpiAwarenessContextForWindowDelegate _getContext;
        private static AreDpiAwarenessContextsEqualDelegate _cmpContext;

        static DpiHelper()
        {
            var user32 = GetModuleHandle(USER32);
            if (user32 != IntPtr.Zero)
            {
                var getCtxPtr = GetProcAddress(user32, "GetDpiAwarenessContextForWindow");
                var cmpPtr = GetProcAddress(user32, "AreDpiAwarenessContextsEqual");

                if (getCtxPtr != IntPtr.Zero)
                    _getContext = (GetDpiAwarenessContextForWindowDelegate)Marshal.GetDelegateForFunctionPointer(getCtxPtr, typeof(GetDpiAwarenessContextForWindowDelegate));

                if (cmpPtr != IntPtr.Zero)
                    _cmpContext = (AreDpiAwarenessContextsEqualDelegate)Marshal.GetDelegateForFunctionPointer(cmpPtr, typeof(AreDpiAwarenessContextsEqualDelegate));
            }
        }

        private enum DpiAwarenessContext
        {
            Unaware = 16,
            SystemAware = 17,
            PerMonitorAware = 18,
            PerMonitorAwareV2 = 34
        }

        public static bool IsPerMonitorV2Aware(IntPtr hwnd)
        {
            if (_getContext == null || _cmpContext == null) return false;
            var ctx = _getContext(hwnd);
            return _cmpContext(ctx, (IntPtr)DpiAwarenessContext.PerMonitorAwareV2);
        }

        public static bool IsPerMonitorAware(IntPtr hwnd)
        {
            if (_getContext == null || _cmpContext == null) return false;
            var ctx = _getContext(hwnd);
            return _cmpContext(ctx, (IntPtr)DpiAwarenessContext.PerMonitorAware);
        }

        /// <summary>
        /// 获取进程的DPI感知级别（进程级别，无法检测Per-Monitor V2）
        /// </summary>
        /// <param name="processHandle">进程句柄，如果为IntPtr.Zero则返回Unknown</param>
        /// <returns>进程DPI感知级别</returns>
        public static ProcessDpiAwareness GetProcessAwareness(IntPtr processHandle)
        {
            if (processHandle == IntPtr.Zero)
                return ProcessDpiAwareness.Process_DPI_Unaware;

            try
            {
                int hr = GetProcessDpiAwareness(processHandle, out var awareness);
                // HRESULT S_OK = 0
                if (hr == 0)
                    return awareness;
                else
                    return ProcessDpiAwareness.Process_DPI_Unaware;
            }
            catch
            {
                return ProcessDpiAwareness.Process_DPI_Unaware;
            }
        }

        /// <summary>
        /// 从窗口句柄获取进程的DPI感知级别
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>进程DPI感知级别</returns>
        public static ProcessDpiAwareness GetProcessAwarenessFromWindow(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
                return ProcessDpiAwareness.Process_DPI_Unaware;

            try
            {
                GetWindowThreadProcessId(hwnd, out uint processId);
                if (processId == 0)
                    return ProcessDpiAwareness.Process_DPI_Unaware;

                IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION, false, processId);
                if (processHandle == IntPtr.Zero)
                    return ProcessDpiAwareness.Process_DPI_Unaware;

                try
                {
                    return GetProcessAwareness(processHandle);
                }
                finally
                {
                    CloseHandle(processHandle);
                }
            }
            catch
            {
                return ProcessDpiAwareness.Process_DPI_Unaware;
            }
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

        public static void DumpDpiInfo(IntPtr hwnd = default, IntPtr process = default)
        {
            if (hwnd == IntPtr.Zero)
                hwnd = GetForegroundWindow();

            // 如果未提供进程句柄，从窗口句柄获取
            ProcessDpiAwareness processAwareness;
            if (process == IntPtr.Zero)
            {
                processAwareness = GetProcessAwarenessFromWindow(hwnd);
            }
            else
            {
                processAwareness = GetProcessAwareness(process);
            }

            Trace.WriteLine("========== DPI 状态 ==========");
            Trace.WriteLine($"窗口句柄: 0x{hwnd.ToInt64():X}");
            Trace.WriteLine($"窗口 DPI: {GetWindowDpi(hwnd)}");
            Trace.WriteLine($"进程感知级别: {processAwareness}");
            Trace.WriteLine($"Per-Monitor 感知: {IsPerMonitorAware(hwnd)}");
            Trace.WriteLine($"Per-Monitor V2 感知: {IsPerMonitorV2Aware(hwnd)}");
            Trace.WriteLine($"IsProcessDPIAware (Win7兼容): {IsProcessDPIAware()}");
            Trace.WriteLine("==============================");
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
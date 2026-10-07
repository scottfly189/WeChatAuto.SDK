using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WeChatAuto.Utils
{

    /// <summary>
    /// Win32 窗口操作工具类。
    /// 支持 Windows 10 / Windows 11。
    /// </summary>
    public static class Win32WindowHelper
    {
        private const string User32 = "user32.dll";

        #region ShowWindow

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(
            IntPtr hWnd,
            int nCmdShow);

        #endregion

        #region SetWindowPos

        private static readonly IntPtr HWND_TOP = IntPtr.Zero;

        private static readonly IntPtr HWND_TOPMOST = new(-1);

        private static readonly IntPtr HWND_NOTOPMOST = new(-2);

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_ASYNCWINDOWPOS = 0x4000;

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        #endregion

        #region GetWindowRect

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(
            IntPtr hWnd,
            out RECT lpRect);

        #endregion

        #region IsWindow

        [DllImport(User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        #endregion

        #region GetWindowThreadProcessId

        [DllImport(User32)]
        private static extern uint GetWindowThreadProcessId(
            IntPtr hWnd,
            out uint lpdwProcessId);

        #endregion

        #region GetForegroundWindow / IsZoomed

        [DllImport(User32)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport(User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsZoomed(IntPtr hWnd);

        #endregion

        #region Public API

        /// <summary>
        /// 判断 HWND 是否是有效的 Windows 窗口。
        /// </summary>
        public static bool IsValidWindow(IntPtr hwnd)
        {
            return hwnd != IntPtr.Zero && IsWindow(hwnd);
        }

        /// <summary>
        /// 获取窗口所属的 ProcessId。
        /// </summary>
        public static int GetProcessId(IntPtr hwnd)
        {
            if (!IsValidWindow(hwnd))
            {
                return 0;
            }

            GetWindowThreadProcessId(hwnd, out uint processId);

            return unchecked((int)processId);
        }

        /// <summary>
        /// 获取当前前台窗口所属进程的 ProcessId；无前台窗口时返回 0。
        /// </summary>
        public static int GetForegroundProcessId()
        {
            return GetProcessId(GetForegroundWindow());
        }

        /// <summary>
        /// 判断窗口是否处于最大化状态。
        /// </summary>
        public static bool IsWindowZoomed(IntPtr hwnd)
        {
            return IsValidWindow(hwnd) && IsZoomed(hwnd);
        }

        /// <summary>
        /// 判断指定 HWND 是否属于指定进程。
        /// </summary>
        public static bool IsWindowBelongsToProcess(
            IntPtr hwnd,
            int processId)
        {
            return IsValidWindow(hwnd)
                   && GetProcessId(hwnd) == processId;
        }

        /// <summary>
        /// 获取窗口的位置和大小。
        /// 坐标为屏幕坐标。
        /// </summary>
        public static bool TryGetWindowRect(
            IntPtr hwnd,
            out int x,
            out int y,
            out int width,
            out int height)
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;

            if (!IsValidWindow(hwnd))
            {
                return false;
            }

            if (!GetWindowRect(hwnd, out RECT rect))
            {
                return false;
            }

            x = rect.Left;
            y = rect.Top;

            width = rect.Right - rect.Left;
            height = rect.Bottom - rect.Top;

            return true;
        }

        /// <summary>
        /// 获取窗口的位置和大小。
        /// </summary>
        public static WindowRect GetWindowRect(IntPtr hwnd)
        {
            if (!TryGetWindowRect(
                    hwnd,
                    out int x,
                    out int y,
                    out int width,
                    out int height))
            {
                throw new Win32Exception(
                    Marshal.GetLastWin32Error(),
                    "无法获取窗口位置和大小。");
            }

            return new WindowRect(x, y, width, height);
        }

        /// <summary>
        /// 移动窗口。
        /// </summary>
        /// <param name="hwnd">窗口 HWND。</param>
        /// <param name="x">屏幕 X 坐标。</param>
        /// <param name="y">屏幕 Y 坐标。</param>
        public static void MoveWindow(
            IntPtr hwnd,
            int x,
            int y)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            bool result = SetWindowPos(
                hwnd,
                IntPtr.Zero,
                x,
                y,
                0,
                0,
                SWP_NOSIZE |
                SWP_NOZORDER |
                SWP_NOACTIVATE |
                SWP_ASYNCWINDOWPOS);

            if (!result)
            {
                ThrowLastWin32Error("移动窗口失败。");
            }
        }

        /// <summary>
        /// 调整窗口大小。
        /// 保持当前位置不变。
        /// </summary>
        public static void ResizeWindow(
            IntPtr hwnd,
            int width,
            int height)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            bool result = SetWindowPos(
                hwnd,
                IntPtr.Zero,
                0,
                0,
                width,
                height,
                SWP_NOMOVE |
                SWP_NOZORDER |
                SWP_NOACTIVATE |
                SWP_ASYNCWINDOWPOS);

            if (!result)
            {
                ThrowLastWin32Error("调整窗口大小失败。");
            }
        }

        /// <summary>
        /// 同时设置窗口位置和大小。
        /// </summary>
        public static void SetWindowBounds(
            IntPtr hwnd,
            int x,
            int y,
            int width,
            int height)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    "窗口宽度必须大于 0。");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    "窗口高度必须大于 0。");
            }

            bool result = SetWindowPos(
                hwnd,
                IntPtr.Zero,
                x,
                y,
                width,
                height,
                SWP_NOZORDER |
                SWP_NOACTIVATE |
                SWP_ASYNCWINDOWPOS);

            if (!result)
            {
                ThrowLastWin32Error("设置窗口位置和大小失败。");
            }
        }

        /// <summary>
        /// 设置窗口位置和大小，并激活窗口。
        /// </summary>
        public static void SetWindowBoundsAndActivate(
            IntPtr hwnd,
            int x,
            int y,
            int width,
            int height)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            RestoreWindow(hwnd);

            bool result = SetWindowPos(
                hwnd,
                HWND_TOP,
                x,
                y,
                width,
                height,
                SWP_SHOWWINDOW |
                SWP_ASYNCWINDOWPOS);

            if (!result)
            {
                ThrowLastWin32Error(
                    "设置窗口位置、大小并激活窗口失败。");
            }
        }

        /// <summary>
        /// 恢复窗口。
        /// 如果窗口处于最小化或最大化状态，会恢复到正常状态。
        /// </summary>
        public static void RestoreWindow(IntPtr hwnd)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            ShowWindow(hwnd, SW_RESTORE);
        }

        /// <summary>
        /// 最大化窗口。
        /// </summary>
        public static void MaximizeWindow(IntPtr hwnd)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            ShowWindow(hwnd, SW_MAXIMIZE);
        }

        /// <summary>
        /// 最小化窗口。
        /// </summary>
        public static void MinimizeWindow(IntPtr hwnd)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            ShowWindow(hwnd, SW_MINIMIZE);
        }

        /// <summary>
        /// 显示窗口。
        /// </summary>
        public static void ShowWindow(IntPtr hwnd)
        {
            if (!IsValidWindow(hwnd))
            {
                throw new ArgumentException(
                    "指定的 HWND 无效。",
                    nameof(hwnd));
            }

            ShowWindow(hwnd, SW_SHOW);
        }

        #endregion

        private static void ThrowLastWin32Error(string message)
        {
            int error = Marshal.GetLastWin32Error();

            throw new Win32Exception(
                error,
                $"{message} Win32Error={error}");
        }
    }

    /// <summary>
    /// Windows 窗口的位置和大小。
    /// </summary>
    public readonly record struct WindowRect(
        int X,
        int Y,
        int Width,
        int Height);


}
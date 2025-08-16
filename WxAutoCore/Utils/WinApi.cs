using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
namespace WxAutoCore.Utils
{


    public class WinApi
    {
        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        const uint GW_HWNDPREV = 3;
        const uint GW_HWNDNEXT = 2;
        const uint GW_HWNDFIRST = 0;
        const uint GW_HWNDLAST = 1;

        /// <summary>
        /// 查找所有包含指定窗口名称的窗口句柄
        /// </summary>
        /// <param name="windowName">要查找的窗口名称</param>
        /// <returns>所有匹配的窗口句柄列表</returns>
        private static List<IntPtr> GetAllWindows(string windowName)
        {
            List<IntPtr> windowHandles = new List<IntPtr>();

            EnumWindows((hWnd, lParam) =>
            {
                if (!IsWindowVisible(hWnd)) return true;

                StringBuilder title = new StringBuilder(256);
                GetWindowText(hWnd, title, title.Capacity);

                if (title.ToString().Contains(windowName))
                {
                    windowHandles.Add(hWnd);
                }
                return true;
            }, IntPtr.Zero);

            return windowHandles;
        }

        /// <summary>
        /// 查找最顶层的指定名称窗口
        /// </summary>
        /// <param name="windowName">要查找的窗口名称</param>
        /// <returns>最顶层的窗口句柄</returns>
        private static IntPtr GetTopMostWindow(string windowName)
        {
            var windowHandles = GetAllWindows(windowName);

            if (windowHandles.Count == 0)
            {
                return IntPtr.Zero;
            }

            if (windowHandles.Count == 1)
            {
                return windowHandles[0];
            }

            // 如果有多个窗口，找到Z-order最高的那个
            IntPtr topMostWindow = IntPtr.Zero;
            IntPtr highestZOrderWindow = IntPtr.Zero;

            // 从桌面最顶层开始，向下遍历所有窗口
            IntPtr currentWindow = GetTopWindow(IntPtr.Zero);

            while (currentWindow != IntPtr.Zero)
            {
                // 检查当前窗口是否是我们要找的微信窗口
                StringBuilder title = new StringBuilder(256);
                GetWindowText(currentWindow, title, title.Capacity);

                if (title.ToString().Contains(windowName))
                {
                    // 找到第一个匹配的窗口，它就是Z-order最高的
                    topMostWindow = currentWindow;
                    break;
                }

                // 移动到下一个窗口（Z-order较低的）
                currentWindow = GetWindow(currentWindow, GW_HWNDNEXT);
            }

            return topMostWindow;
        }

        /// <summary>
        /// 查找所有包含指定窗口名称的窗口的进程ID
        /// </summary>
        /// <param name="windowName">要查找的窗口名称</param>
        /// <returns>所有匹配窗口的进程ID列表</returns>
        private static List<uint> GetAllWindowProcessIds(string windowName)
        {
            List<uint> processIds = new List<uint>();
            var windowHandles = GetAllWindows(windowName);

            foreach (var hwnd in windowHandles)
            {
                GetWindowThreadProcessId(hwnd, out uint pid);
                processIds.Add(pid);
            }

            return processIds;
        }

        /// <summary>
        /// 获取指定窗口名称的顶层窗口的进程ID
        /// </summary>
        /// <param name="windowName">要查找的窗口名称</param>
        /// <returns>最顶层窗口的进程ID</returns>
        public static uint GetTopWindow(string windowName)
        {
            var topMostWindow = GetTopMostWindow(windowName);

            if (topMostWindow != IntPtr.Zero)
            {
                GetWindowThreadProcessId(topMostWindow, out uint pid);
                return pid;
            }
            else
            {
                throw new Exception($"没找到 {windowName} 窗口");
            }
        }
    }

}
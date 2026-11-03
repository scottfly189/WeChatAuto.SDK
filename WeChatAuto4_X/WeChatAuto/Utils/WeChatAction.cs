using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace WeChatAuto.Utils
{

    /// <summary>
    /// 按 PID 找微信主窗、唤回（含关窗仅留托盘）、最小化、恢复等。
    /// 主窗是 Qt51514QWindowIcon + 标题「微信」。
    /// </summary>
    public static class WeChatAction
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;

        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONUP = 0x0202;
        private const uint WM_RBUTTONDOWN = 0x0204;
        private const uint WM_RBUTTONUP = 0x0205;
        private const uint WM_NULL = 0x0000;
        private const uint WM_SYSCOMMAND = 0x0112;
        private const int SC_RESTORE = 0xF120;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_CLOSE = 0xF060;

        private const uint TB_BUTTONCOUNT = 0x0418;
        private const uint TB_GETBUTTON = 0x0417;
        private const uint TB_GETBUTTONINFOW = 0x043F;
        private const uint TB_GETITEMRECT = 0x041D;

        // 跨进程读任务栏工具栏：TBBUTTON 里的 dwData 指向 explorer 进程内的 TRAYDATA，其首字段为图标所属 HWND。
        // TBBUTTON = iBitmap(4) + idCommand(4) + fsState(1) + fsStyle(1) + bReserved[6|2] + dwData(ptr) + iString(ptr)
        private const int TB_BUTTON_SIZE_32 = 20;
        private const int TB_BUTTON_SIZE_64 = 32;
        private const int TB_BUTTON_DWDATA_OFFSET_32 = 12;
        private const int TB_BUTTON_DWDATA_OFFSET_64 = 16;

        private const uint PROCESS_VM_OPERATION = 0x0008;
        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;
        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_RELEASE = 0x8000;
        private const uint PAGE_READWRITE = 0x0004;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_firstTop = new IntPtr(-2);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_NOZORDER = 0x0004;

        // 临时把当前线程切到 per-monitor 感知，用于按物理像素换算托盘图标坐标
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = new IntPtr(-3);

        [StructLayout(LayoutKind.Sequential)]
        public struct WinInfo
        {
            public IntPtr Hwnd;
            public bool Visible;
            public string Class;
            public string Title;
        }

        #region P/Invoke

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll")]
        private static extern IntPtr SetThreadDpiAwarenessContext(IntPtr dpiContext);

        [DllImport("user32.dll")]
        private static extern bool SetPhysicalCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetPhysicalCursorPos(out POINT lpPoint);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool IsWow64Process(IntPtr hProcess, out bool Wow64Process);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left, Top, Right, Bottom; }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int X, Y; }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion U;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx, dy;
            public uint mouseData, dwFlags, time;
            public IntPtr dwExtraInfo;
        }

        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_MOVE = 0x0001;

        #endregion

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeProcessHandle OpenProcess(uint access, bool inherit, int pid);

        const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        public static Process[] Refresh(string name) =>
            Process.GetProcessesByName(name);

        public static List<Process> FindMains(Process[] list)
        {
            var ids = list.Select(p => p.Id).ToHashSet();
            var roots = new List<Process>();
            foreach (var p in list)
            {
                int ppid = GetParentId(p.Id);
                if (!ids.Contains(ppid)) roots.Add(p);
            }
            return roots;
        }

        static int GetParentId(int pid)
        {
            using var q = new ManagementObjectSearcher(
                $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId={pid}");
            foreach (ManagementObject mo in q.Get())
            {
                using (mo)
                    return Convert.ToInt32(mo["ParentProcessId"]);
            }
            return 0;
        }

        /// <summary>枚举某 PID 下所有顶层窗（含不可见）。</summary>
        public static List<WinInfo> ListWindows(int pid)
        {
            var list = new List<WinInfo>();
            EnumWindows((h, _) =>
            {
                GetWindowThreadProcessId(h, out uint wpid);
                if ((int)wpid != pid) return true;

                var cls = new StringBuilder(256);
                GetClassName(h, cls, cls.Capacity);
                var title = new StringBuilder(512);
                GetWindowText(h, title, title.Capacity);

                list.Add(new WinInfo
                {
                    Hwnd = h,
                    Visible = IsWindowVisible(h),
                    Class = cls.ToString(),
                    Title = title.ToString()
                });
                return true;
            }, IntPtr.Zero);
            return list;
        }

        /// <summary>
        /// 找真正主窗：Qt51514QWindowIcon + 标题「微信」。
        /// 关窗后 Vis=False 仍在；绝不要选 WxTrayIconMessageWindow。
        /// </summary>
        public static IntPtr FindMainHwnd(int pid)
        {
            var wins = ListWindows(pid);

            var main = wins.FirstOrDefault(w =>
                w.Class.IndexOf("QWindowIcon", StringComparison.OrdinalIgnoreCase) >= 0
                && w.Title == "微信"
                && w.Class.IndexOf("WxTrayIcon", StringComparison.OrdinalIgnoreCase) < 0);

            if (main.Hwnd != IntPtr.Zero) return main.Hwnd;

            main = wins.FirstOrDefault(w =>
                w.Class.IndexOf("QWindowIcon", StringComparison.OrdinalIgnoreCase) >= 0
                && w.Class.IndexOf("WxTrayIcon", StringComparison.OrdinalIgnoreCase) < 0);

            return main.Hwnd;
        }

        /// <summary>找托盘消息窗（调试用；不要 Close 它）。</summary>
        public static IntPtr FindTrayMsgHwnd(int pid)
        {
            return ListWindows(pid)
                .FirstOrDefault(w =>
                    w.Class.IndexOf("WxTrayIconMessageWindow", StringComparison.OrdinalIgnoreCase) >= 0)
                .Hwnd;
        }

        /// <summary>
        /// 唤回主窗。关窗仅留托盘时，纯 ShowWindow 会出「红关钮假窗」——
        /// 先尝试托盘左键等价；失败再 ShowWindow + 前台。
        /// </summary>
        public static bool BringUp(int pid)
        {
            var hwnd = FindMainHwnd(pid);
            if (hwnd == IntPtr.Zero || !IsWindow(hwnd)) return false;

            // 已可见且非最小化：直接抢前台
            if (IsWindowVisible(hwnd) && !IsIconic(hwnd))
            {
                ForceForeground(hwnd);
                return true;
            }

            // 关窗后 Vis=False：优先托盘单击（与用户点 notifyicon 等价）
            if (!IsWindowVisible(hwnd))
            {
                if (TryClickNotifyIcon(pid))
                {
                    Thread.Sleep(200);
                    hwnd = FindMainHwnd(pid);
                    if (hwnd != IntPtr.Zero && IsWindowVisible(hwnd))
                    {
                        ForceForeground(hwnd);
                        return true;
                    }
                }

                // 托盘点不到时：用 SYSCOMMAND 恢复，比裸 ShowWindow 更接近系统路径
                SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
                ShowWindow(hwnd, SW_RESTORE);
                ShowWindow(hwnd, SW_SHOW);
                ForceForeground(hwnd);

                // // 仍假窗时再补一次托盘
                // Thread.Sleep(100);
                // if (!IsWindowVisible(hwnd) || LooksGhost(hwnd))
                //     TryClickNotifyIcon(pid);

                return IsWindow(hwnd);
            }

            // 最小化
            if (IsIconic(hwnd))
            {
                SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
                ShowWindow(hwnd, SW_RESTORE);
                ForceForeground(hwnd);
                return true;
            }

            ShowWindow(hwnd, SW_SHOW);
            ForceForeground(hwnd);
            return true;
        }

        /// <summary>最小化主窗（到任务栏/托盘行为随微信设置）。</summary>
        public static bool Minimize(int pid)
        {
            var hwnd = FindMainHwnd(pid);
            if (hwnd == IntPtr.Zero || !IsWindow(hwnd)) return false;
            if (!IsWindowVisible(hwnd)) return true; // 已关窗态

            SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_MINIMIZE, IntPtr.Zero);
            ShowWindow(hwnd, SW_MINIMIZE);
            return IsIconic(hwnd) || !IsWindowVisible(hwnd);
        }

        /// <summary>从最小化恢复（已关窗仅托盘时请用 BringUp）。</summary>
        public static bool Restore(int pid)
        {
            var hwnd = FindMainHwnd(pid);
            if (hwnd == IntPtr.Zero || !IsWindow(hwnd)) return false;

            if (!IsWindowVisible(hwnd))
                return BringUp(pid); // 关窗态走完整唤回

            if (IsIconic(hwnd))
            {
                SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
                ShowWindow(hwnd, SW_RESTORE);
            }

            ForceForeground(hwnd);
            return IsWindowVisible(hwnd) && !IsIconic(hwnd);
        }

        /// <summary>
        /// 关闭主窗（等同点标题栏 X）：主窗隐藏，进程与 notifyicon 仍在。
        /// </summary>
        public static bool Hide(int pid)
        {
            var hwnd = FindMainHwnd(pid);
            if (hwnd == IntPtr.Zero || !IsWindow(hwnd)) return false;
            if (!IsWindowVisible(hwnd) && !IsIconic(hwnd)) return true; // 已是关窗态
                                                                        // 走系统关闭命令，让微信自己进「关窗留托盘」逻辑
            SendMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_CLOSE, IntPtr.Zero);
            // 少数版本若仍可见，再补一次（仍只打主窗）
            Thread.Sleep(100);
            hwnd = FindMainHwnd(pid);
            if (hwnd != IntPtr.Zero && IsWindow(hwnd) && IsWindowVisible(hwnd))
                PostMessage(hwnd, 0x0010 /* WM_CLOSE */, IntPtr.Zero, IntPtr.Zero);
            Thread.Sleep(100);
            hwnd = FindMainHwnd(pid);
            return hwnd == IntPtr.Zero || !IsWindowVisible(hwnd);
        }


        /// <summary>调试：打印该 PID 所有窗。</summary>
        public static void Dump(int pid)
        {
            foreach (var w in ListWindows(pid))
                Console.WriteLine($"PID={pid} HWND=0x{w.Hwnd.ToInt64():X} Vis={w.Visible} Class={w.Class} Title={w.Title}");
            var main = FindMainHwnd(pid);
            Console.WriteLine($"选用主窗 HWND=0x{main.ToInt64():X}");
        }

        #region 内部

        private static bool LooksGhost(IntPtr hwnd)
        {
            // 假窗常见：已 Visible 但随后点托盘才正常；此处仅作弱提示，真正修复靠托盘点击
            return IsWindowVisible(hwnd);
        }

        private static void ForceForeground(IntPtr hwnd)
        {
            var fg = GetForegroundWindow();
            uint fgTid = 0, curTid = GetCurrentThreadId();
            if (fg != IntPtr.Zero)
                GetWindowThreadProcessId(fg, out fgTid);
            uint targetTid;
            GetWindowThreadProcessId(hwnd, out targetTid);

            if (fgTid != 0 && fgTid != curTid)
                AttachThreadInput(curTid, fgTid, true);
            if (targetTid != 0 && targetTid != curTid)
                AttachThreadInput(curTid, targetTid, true);

            ShowWindow(hwnd, SW_RESTORE);
            SetForegroundWindow(hwnd);
            SwitchToThisWindow(hwnd, true);
            SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
            SetWindowPos(hwnd, new IntPtr(-2), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW); // HWND_NOTOPMOST

            if (fgTid != 0 && fgTid != curTid)
                AttachThreadInput(curTid, fgTid, false);
            if (targetTid != 0 && targetTid != curTid)
                AttachThreadInput(curTid, targetTid, false);
        }

        /// <summary>
        /// 在任务栏通知区找到属于该 PID 的图标并模拟左键单击（只点它，不碰其它图标）。
        /// Win10/11 托盘结构：Shell_TrayWnd → TrayNotifyWnd → SysPager → ToolbarWindow32（及溢出区）。
        /// </summary>
        private static bool TryClickNotifyIcon(int pid)
        {
            // 1) 主通知区
            if (ClickTrayIconByPid(pid))
                return true;

            // 2) 溢出区（“显示隐藏的图标”已展开时）
            return ClickOverflowTrayIconByPid(pid);
        }

        private static bool ClickTrayIconByPid(int pid)
        {
            foreach (var toolbar in FindTrayToolbars(FindWindow("Shell_TrayWnd", null)))
            {
                if (ClickToolbarIconForPid(toolbar, pid))
                    return true;
            }
            return false;
        }

        private static bool ClickOverflowTrayIconByPid(int pid)
        {
            var overflow = FindWindow("NotifyIconOverflowWindow", null);
            if (overflow == IntPtr.Zero || !IsWindowVisible(overflow)) return false;

            foreach (var toolbar in FindTrayToolbars(overflow))
            {
                if (ClickToolbarIconForPid(toolbar, pid))
                    return true;
            }
            return false;
        }

        /// <summary>枚举托盘窗口下所有 ToolbarWindow32（主区/溢出区都可能不止一个工具栏）。</summary>
        private static List<IntPtr> FindTrayToolbars(IntPtr root)
        {
            var toolbars = new List<IntPtr>();
            if (root == IntPtr.Zero) return toolbars;

            EnumChildWindows(root, (h, _) =>
            {
                if (GetWindowClassName(h) == "ToolbarWindow32")
                    toolbars.Add(h);
                return true;
            }, IntPtr.Zero);
            return toolbars;
        }

        /// <summary>
        /// 只点击属于该 PID 的托盘图标。
        /// 定位依据：TBBUTTON.dwData 指向 explorer 进程内的 TRAYDATA，其首字段是图标所属窗口 HWND
        /// （微信是 WxTrayIconMessageWindow），拿它反查 PID 即可精确匹配，两个微信也不会混。
        /// 注意不要退回“按按钮数均分坐标挨个点”的老做法：那会把任务栏上所有图标都点一遍。
        /// </summary>
        private static bool ClickToolbarIconForPid(IntPtr toolbar, int pid)
        {
            // 隐藏的工具栏（如未展开的溢出区）不能点：它的按钮坐标不可见，点了会落到别的窗口上
            if (!IsWindowVisible(toolbar)) return false;

            if (!TryGetTrayIconRect(toolbar, pid, out RECT rect)) return false;

            var center = new POINT
            {
                X = rect.Left + (rect.Right - rect.Left) / 2,
                Y = rect.Top + (rect.Bottom - rect.Top) / 2
            };

            // TB_GETITEMRECT 返回的是物理像素的客户区坐标；而本进程若被 DPI 虚拟化，
            // ClientToScreen 会把它当逻辑坐标再放大一次，结果偏到隔壁图标上（点出别的应用）。
            // 这里临时把当前线程切到 per-monitor 感知：坐标换算与鼠标定位都走物理像素。
            var previousContext = SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            if (previousContext == IntPtr.Zero)
                previousContext = SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE);
            if (previousContext == IntPtr.Zero) return false;

            try
            {
                if (!ClientToScreen(toolbar, ref center)) return false;

                // 该点必须真的落在托盘工具栏上；否则说明图标被裁掉/被遮挡，宁可不点也不能乱点
                if (WindowFromPoint(center) != toolbar) return false;

                ClickScreen(center.X, center.Y);
                return true;
            }
            finally
            {
                SetThreadDpiAwarenessContext(previousContext);
            }
        }

        /// <summary>
        /// 跨进程读工具栏按钮，找出属于 pid 的图标按钮在工具栏客户区内的矩形。
        /// 链路：OpenProcess(explorer) → VirtualAllocEx → TB_GETBUTTON → ReadProcessMemory(TBBUTTON)
        ///       → dwData → ReadProcessMemory(TRAYDATA.hWnd) → GetWindowThreadProcessId == pid。
        /// </summary>
        private static bool TryGetTrayIconRect(IntPtr toolbar, int pid, out RECT rect)
        {
            rect = default;

            int count = (int)SendMessage(toolbar, TB_BUTTONCOUNT, IntPtr.Zero, IntPtr.Zero);
            if (count <= 0) return false;

            GetWindowThreadProcessId(toolbar, out uint shellPid);
            IntPtr shell = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, shellPid);
            if (shell == IntPtr.Zero) return false;

            bool is64 = IsProcess64Bit(shell);
            int buttonSize = is64 ? TB_BUTTON_SIZE_64 : TB_BUTTON_SIZE_32;
            int dwDataOffset = is64 ? TB_BUTTON_DWDATA_OFFSET_64 : TB_BUTTON_DWDATA_OFFSET_32;
            const int remoteBufferSize = 128;

            IntPtr remote = IntPtr.Zero;
            try
            {
                remote = VirtualAllocEx(shell, IntPtr.Zero, new UIntPtr(remoteBufferSize), MEM_COMMIT, PAGE_READWRITE);
                if (remote == IntPtr.Zero) return false;

                var buffer = new byte[remoteBufferSize];
                for (int i = 0; i < count; i++)
                {
                    if ((long)SendMessage(toolbar, TB_GETBUTTON, (IntPtr)i, remote) == 0) continue;
                    if (!ReadProcessMemory(shell, remote, buffer, new UIntPtr((uint)buttonSize), out _)) continue;

                    IntPtr trayData = is64
                        ? new IntPtr(BitConverter.ToInt64(buffer, dwDataOffset))
                        : new IntPtr(BitConverter.ToInt32(buffer, dwDataOffset));
                    if (trayData == IntPtr.Zero) continue;

                    if (!ReadRemotePointer(shell, trayData, is64, out IntPtr owner)) continue;
                    if (owner == IntPtr.Zero || !IsWindow(owner)) continue;

                    GetWindowThreadProcessId(owner, out uint ownerPid);
                    if ((int)ownerPid != pid) continue;

                    if ((long)SendMessage(toolbar, TB_GETITEMRECT, (IntPtr)i, remote) == 0) continue;
                    if (!ReadProcessMemory(shell, remote, buffer, new UIntPtr(16), out _)) continue;

                    rect = new RECT
                    {
                        Left = BitConverter.ToInt32(buffer, 0),
                        Top = BitConverter.ToInt32(buffer, 4),
                        Right = BitConverter.ToInt32(buffer, 8),
                        Bottom = BitConverter.ToInt32(buffer, 12)
                    };
                    return rect.Right > rect.Left && rect.Bottom > rect.Top;
                }
                return false;
            }
            finally
            {
                if (remote != IntPtr.Zero)
                    VirtualFreeEx(shell, remote, UIntPtr.Zero, MEM_RELEASE);
                CloseHandle(shell);
            }
        }

        private static bool ReadRemotePointer(IntPtr process, IntPtr address, bool is64, out IntPtr value)
        {
            value = IntPtr.Zero;
            var raw = new byte[8];
            if (!ReadProcessMemory(process, address, raw, new UIntPtr(is64 ? 8u : 4u), out _)) return false;

            long v = is64 ? BitConverter.ToInt64(raw, 0) : BitConverter.ToInt32(raw, 0);
            value = new IntPtr(v);
            return true;
        }

        /// <summary>目标进程是否 64 位（决定 TBBUTTON 里指针字段的宽度）。</summary>
        private static bool IsProcess64Bit(IntPtr process)
        {
            if (!Environment.Is64BitOperatingSystem) return false;
            if (!IsWow64Process(process, out bool wow64)) return IntPtr.Size == 8;
            return !wow64;
        }

        private static string GetWindowClassName(IntPtr hwnd)
        {
            var sb = new StringBuilder(256);
            GetClassName(hwnd, sb, sb.Capacity);
            return sb.ToString();
        }

        private static void ClickScreen(int x, int y)
        {
            bool hasOld = GetPhysicalCursorPos(out POINT old);

            SetPhysicalCursorPos(x, y);
            var inputs = new INPUT[2];
            inputs[0] = new INPUT { type = INPUT_MOUSE, U = new InputUnion { mi = new MOUSEINPUT { dwFlags = MOUSEEVENTF_LEFTDOWN } } };
            inputs[1] = new INPUT { type = INPUT_MOUSE, U = new InputUnion { mi = new MOUSEINPUT { dwFlags = MOUSEEVENTF_LEFTUP } } };
            SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));

            Thread.Sleep(100);                                  // 先让托盘处理完这次点击
            if (hasOld) SetPhysicalCursorPos(old.X, old.Y);     // 鼠标归位，尽量不打扰用户
        }

        #endregion
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WxAutoCommon.Utils
{
    /// <summary>
    /// Win11专用IME控制工具类
    /// </summary>
    public class IMEHelper
    {
        // Windows API声明
        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmAssociateContext(IntPtr hwnd, IntPtr hImc);
        [DllImport("imm32.dll")]
        public static extern bool ImmDisableIME(uint idThread);

        const uint KLF_ACTIVATE = 1;

        // 常量定义
        private const uint KLF_SETFORPROCESS = 0x00000100;
        private const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private const int INPUTLANGCHANGE_SYSCHARSET = 0x0001;


        /// <summary>
        /// 禁用指定进程的IME输入法（适用于自动化第三方应用程序）
        /// </summary>
        /// <param name="processId">目标进程ID</param>
        /// <returns>是否成功禁用IME</returns>
        public static bool DisableImeForProcess(int processId)
        {
            try
            {
                // 步骤1：强制切换到英文键盘布局
                var hkl = LoadKeyboardLayout("00000409", KLF_ACTIVATE);
                if (hkl != IntPtr.Zero)
                {
                    ActivateKeyboardLayout(hkl, 0);
                }

                // 步骤2：获取目标进程
                var process = Process.GetProcessById(processId);
                
                // 步骤3：尝试禁用所有UI线程的IME
                // 对于第三方应用程序，可能需要禁用多个线程的IME
                bool anySuccess = false;
                var uiThreads = process.Threads
                    .Cast<ProcessThread>()
                    .Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)
                    .OrderBy(t => t.Id);

                foreach (var thread in uiThreads)
                {
                    try
                    {
                        var result = ImmDisableIME((uint)thread.Id);
                        if (result)
                        {
                            anySuccess = true;
                            Console.WriteLine($"成功禁用线程 {thread.Id} 的IME");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"禁用线程 {thread.Id} 的IME失败: {ex.Message}");
                    }
                }
                
                return anySuccess;
            }
            catch (ArgumentException)
            {
                // 进程不存在
                Console.WriteLine($"进程ID {processId} 不存在");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"禁用进程 {processId} 的IME时出错: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 通过进程名称禁用IME（适用于自动化第三方应用程序）
        /// </summary>
        /// <param name="processName">进程名称（不包含.exe）</param>
        /// <returns>是否成功禁用IME</returns>
        public static bool DisableImeForProcessByName(string processName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    Console.WriteLine($"未找到进程: {processName}");
                    return false;
                }

                bool anySuccess = false;
                foreach (var process in processes)
                {
                    Console.WriteLine($"处理进程: {process.ProcessName} (PID: {process.Id})");
                    if (DisableImeForProcess(process.Id))
                    {
                        anySuccess = true;
                    }
                }

                return anySuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"通过进程名称禁用IME时出错: {ex.Message}");
                return false;
            }
        }


        // 禁用某窗口的 IME（解除 hwnd 与 输入上下文的关联）
        public static void DisableImeForWindow(IntPtr hwnd)
        {
            // 将该窗口关联上下文设为 0，表示没有 IME 输入
            ImmAssociateContext(hwnd, IntPtr.Zero);
        }

        /// <summary>
        /// Win11专用IME禁用方法
        /// 该方法通过多种方式组合来禁用IME，适用于Windows 11系统
        /// </summary>
        public static void DisableImeForWin11()
        {
            try
            {
                Console.WriteLine("执行Win11专用IME禁用方法...");

                // 步骤1：终止TextInputHost进程
                TerminateTextInputHostProcess();

                // 步骤2：强制设置英文键盘布局
                ForceEnglishKeyboardLayout();

                // 步骤3：发送系统消息禁用IME
                SendSystemMessageToDisableIME();

                Console.WriteLine("IME禁用操作完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IME禁用过程中出现错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 终止TextInputHost进程
        /// </summary>
        private static void TerminateTextInputHostProcess()
        {
            try
            {
                var processes = System.Diagnostics.Process.GetProcessesByName("TextInputHost");
                if (processes.Length > 0)
                {
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"发现TextInputHost进程 (PID: {process.Id})");
                        process.Kill();
                        Console.WriteLine("已终止TextInputHost进程");
                    }
                }
                else
                {
                    Console.WriteLine("未发现TextInputHost进程");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"终止TextInputHost进程失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 强制切换到英文键盘布局
        /// </summary>
        private static void ForceEnglishKeyboardLayout()
        {
            try
            {
                // 加载美式英文键盘布局 (00000409)
                var hkl = LoadKeyboardLayout("00000409", KLF_SETFORPROCESS);
                if (hkl != IntPtr.Zero)
                {
                    // 激活键盘布局
                    ActivateKeyboardLayout(hkl, 0);
                    Console.WriteLine("已强制切换到英文键盘布局");
                }
                else
                {
                    Console.WriteLine("加载英文键盘布局失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"切换键盘布局时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 发送系统消息禁用IME
        /// </summary>
        private static void SendSystemMessageToDisableIME()
        {
            try
            {
                IntPtr consoleHwnd = GetConsoleWindow();
                if (consoleHwnd != IntPtr.Zero)
                {
                    // 发送WM_INPUTLANGCHANGEREQUEST消息
                    SendMessage(consoleHwnd, WM_INPUTLANGCHANGEREQUEST,
                        INPUTLANGCHANGE_SYSCHARSET, GetKeyboardLayout(0));

                    Console.WriteLine("已发送系统消息禁用IME");
                }
                else
                {
                    Console.WriteLine("无法获取控制台窗口句柄");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送系统消息失败: {ex.Message}");
            }
        }
    }
}
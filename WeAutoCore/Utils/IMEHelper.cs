using System;
using System.Runtime.InteropServices;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace WxAutoCore.Utils
{

    public class IMEHelper
    {
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmReleaseContext(IntPtr hwnd, IntPtr hImc);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmAssociateContext(IntPtr hwnd, IntPtr hImc);

        [DllImport("imm32.dll")]
        public static extern bool ImmDisableIME(uint idThread);

        // 禁用当前线程的 IME
        public static void DisableImeForCurrentThread()
        {
            uint threadId = (uint)System.Threading.Thread.CurrentThread.ManagedThreadId;
            // 注意：ImmDisableIME 的参数是线程 / 进程 id，具体可能跟平台有关
            ImmDisableIME(threadId);
        }

        // 禁用某窗口的 IME（解除 hwnd 与 输入上下文的关联）
        public static void DisableImeForWindow(IntPtr hwnd)
        {
            // 将该窗口关联上下文设为 0，表示没有 IME 输入
            ImmAssociateContext(hwnd, IntPtr.Zero);
        }
    }
}

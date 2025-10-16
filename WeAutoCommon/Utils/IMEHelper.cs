using System;
using System.Runtime.InteropServices;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace WxAutoCommon.Utils
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
            var hkl = LoadKeyboardLayout("00000409", KLF_ACTIVATE);
            ActivateKeyboardLayout(hkl, 0);
            // uint threadId = (uint)System.Threading.Thread.CurrentThread.ManagedThreadId;
            // // 注意：ImmDisableIME 的参数是线程 / 进程 id，具体可能跟平台有关
            // ImmDisableIME(threadId);
        }

        // 禁用某窗口的 IME（解除 hwnd 与 输入上下文的关联）
        public static void DisableImeForWindow(IntPtr hwnd)
        {
            // 将该窗口关联上下文设为 0，表示没有 IME 输入
            ImmAssociateContext(hwnd, IntPtr.Zero);
        }
        [DllImport("user32.dll")]
        static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint Flags);

        const uint KLF_ACTIVATE = 1;
    }

    public static class InputMethodController
    {
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        private static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);

        [DllImport("imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint Flags);

        private const int IME_CMODE_ALPHANUMERIC = 0x0000; // 英文输入模式
        private const uint KLF_ACTIVATE = 0x00000001;      // 激活新布局
        private const uint KLF_SETFORPROCESS = 0x00000100;

        /// <summary>
        /// 将指定窗口的输入法切换为英文模式（如果是中文输入法，会转为英文状态）。
        /// </summary>
        public static void SetEnglishMode(IntPtr hwnd)
        {
            var hIMC = ImmGetContext(hwnd);
            if (hIMC != IntPtr.Zero)
            {
                ImmSetConversionStatus(hIMC, IME_CMODE_ALPHANUMERIC, 0);
                ImmReleaseContext(hwnd, hIMC);
            }
        }

        /// <summary>
        /// 全局切换键盘布局为美式英文（00000409）。
        /// </summary>
        public static void SwitchToUSKeyboard()
        {
            var hkl = LoadKeyboardLayout("00000409", KLF_SETFORPROCESS);
            ActivateKeyboardLayout(hkl, 0);
        }

        /// <summary>
        /// 对 FlaUI 自动化元素应用输入法切换。
        /// </summary>
        public static void ForceEnglishForElement(AutomationElement element)
        {
            if (element == null) return;
            // 激活美式键盘
            SwitchToUSKeyboard();

            var rect = element.BoundingRectangle;
            int x = (int)(rect.Left + rect.Right) / 2;
            int y = (int)(rect.Top + rect.Bottom) / 2;

            var pt = new POINT { X = x, Y = y };
            IntPtr hwnd = WindowFromPoint(pt);
            if (hwnd != IntPtr.Zero)
            {
                // 将目标窗口输入法切英文模式
                SetEnglishMode(hwnd);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        private static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, POINT pt, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
    }

}

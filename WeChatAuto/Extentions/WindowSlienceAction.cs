using System;
using System.Runtime.InteropServices;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Interface;
using WxAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Utils;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Tools;
using WeAutoCommon.Utils;
using System.Drawing;

namespace WeChatAuto.Extentions
{

    /// <summary>
    /// 静默操作的扩展
    /// </summary>
    public static class WindowSlienceAction
    {
        /// <summary>
        /// 静默点击
        /// 最好保证是最近刷新的元素，这样支持窗口移动.
        /// 此方法dpi无感
        /// </summary>
        /// <param name="window">窗口Window</param>
        /// <param name="element">要点击的元素<see cref="AutomationElement"/>最好保证是最近刷新的元素</param>
        public static void SilenceClickExt(this Window window, AutomationElement element)
        {
            Wait.UntilInputIsProcessed();
            var windowHandle = window.Properties.NativeWindowHandle.Value;
            var elementRectangle = element.BoundingRectangle;

            // 计算按钮中心点相对于窗口的坐标
            int x = elementRectangle.X + (elementRectangle.Width / 2);
            int y = elementRectangle.Y + (elementRectangle.Height / 2);

            // 转换为窗口客户区坐标
            var windowRectangle = window.BoundingRectangle;
            int clientX = x - windowRectangle.X;
            int clientY = y - windowRectangle.Y;

            // 构建 lParam (y << 16) | (x & 0xFFFF)
            int lParamValue = (clientY << 16) | (clientX & 0xFFFF);
            IntPtr lParam = (IntPtr)lParamValue;

            // 鼠标按下
            User32.SendMessage(windowHandle, WindowsMessages.WM_LBUTTONDOWN, IntPtr.Zero, lParam);
            // 鼠标释放
            User32.SendMessage(windowHandle, WindowsMessages.WM_LBUTTONUP, IntPtr.Zero, lParam);
            Wait.UntilInputIsProcessed();
        }
        /// <summary>
        /// 静默输入文本
        /// </summary>
        /// <param name="element">输入框<see cref="TextBox"/></param>
        /// <param name="text">文本</param>
        public static void SilenceEnterText(this Window wxWindow, TextBox edit, string text)
        {
            wxWindow.SilenceClickExt(edit);

            var hwnd = wxWindow.Properties.NativeWindowHandle.Value;
            var rect = edit.BoundingRectangle;
            int x = (int)((rect.Left + rect.Right) / 2 - wxWindow.BoundingRectangle.Left);
            int y = (int)((rect.Top + rect.Bottom) / 2 - wxWindow.BoundingRectangle.Top);
            IntPtr lParam = (IntPtr)((y << 16) | (x & 0xFFFF));
            foreach (char c in text)
            {
                User32.SendMessage(hwnd, WindowsMessages.WM_CHAR, (IntPtr)c, IntPtr.Zero);
            }

            Wait.UntilInputIsProcessed();
        }
        /// <summary>
        /// 静默回车
        /// </summary>
        /// <param name="wxWindow">微信窗口封装<see cref="IWeChatWindow"/></param>
        /// <param name="edit">输入框<see cref="TextBox"/></param>
        public static void SilenceReturn(this Window wxWindow, TextBox edit)
        {
            wxWindow.SilenceClickExt(edit);
            Wait.UntilInputIsProcessed();

            var hwnd = wxWindow.Properties.NativeWindowHandle.Value;
            var rect = edit.BoundingRectangle;
            int x = (int)((rect.Left + rect.Right) / 2 - wxWindow.BoundingRectangle.Left);
            int y = (int)((rect.Top + rect.Bottom) / 2 - wxWindow.BoundingRectangle.Top);
            IntPtr lParam = (IntPtr)((y << 16) | (x & 0xFFFF));
            User32.SendMessage(hwnd, WindowsMessages.WM_KEYDOWN, (IntPtr)VirtualKeyShort.RETURN, lParam);
        }



        /// <summary>
        /// 使用SendKeys的简单方法
        /// </summary>
        public static void SilencePasteSimple(this Window wxWindow, string[] filePath, TextBox edit)
        {
            // 1. 先点击输入框获取焦点
            wxWindow.SilenceClickExt(edit);
            Wait.UntilInputIsProcessed();
            System.Threading.Thread.Sleep(200);

            // 2. 复制文件到剪贴板
            var result = ClipboardApi.CopyFilesToClipboard(filePath);
            if (!result)
            {
                throw new Exception($"复制文件到剪贴板失败: {string.Join(", ", filePath)}");
            }

            // 3. 等待剪贴板操作完成
            Wait.UntilInputIsProcessed();
            System.Threading.Thread.Sleep(600);

            // 4. 使用SendKeys发送Ctrl+V
            try
            {
                edit.Focus();
                System.Threading.Thread.Sleep(500);
                // System.Windows.Forms.SendKeys.SendWait("^v"); // ^v 表示 Ctrl+V
                Keyboard.Press(VirtualKeyShort.CONTROL);
                Keyboard.Press(VirtualKeyShort.KEY_V);
                Keyboard.Release(VirtualKeyShort.KEY_V);
                Keyboard.Release(VirtualKeyShort.CONTROL);
            }
            catch
            {
                // 如果SendKeys失败，回退到Windows消息方式
                var hwnd = wxWindow.Properties.NativeWindowHandle.Value;
                User32.SendMessage(hwnd, WindowsMessages.WM_PASTE, IntPtr.Zero, IntPtr.Zero);
            }

            Wait.UntilInputIsProcessed();
            System.Threading.Thread.Sleep(500);
        }
        /// <summary>
        /// 显示点击
        /// 此方法dpi无感
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void ShowClick(this Window window, AutomationElement element)
        {
            ClickHighlighter.ShowClick(element.BoundingRectangle.Center());
        }
        /// <summary>
        /// 显示点击
        /// 此方法dpi感知
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="element">元素</param>
        public static void ShowAwareClick(this Window window, AutomationElement element)
        {
            var point = window.GetDpiAwarePoint(element);
            ClickHighlighter.ShowClick(point);
        }
        /// <summary>
        /// 显示点击
        /// 此方法dpi无感知
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="point">坐标</param>
        public static void ShowClick(this Window window, Point point)
        {
            ClickHighlighter.ShowClick(point);
        }
        /// <summary>
        /// 显示点击
        /// 此方法dpi感知
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="point">坐标</param>
        public static void ShowAwareClick(this Window window, Point point)
        {
            var dpiPoint = window.GetDpiAwarePoint(point);
            ClickHighlighter.ShowClick(dpiPoint);
        }
    }
}
using System;
using System.Windows;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Interface;
using WxAutoCore.Components;
using WxAutoCore.Utils;

namespace WxAutoCore.Extentions
{

    /// <summary>
    /// 静默操作的扩展
    /// </summary>
    public static class SilenceActionTools
    {
        /// <summary>
        /// 静默点击
        /// 最好保证是最近刷新的元素，这样支持窗口移动.
        /// </summary>
        /// <param name="wxWindow">微信窗口封装<see cref="IWeChatWindow"/></param>
        /// <param name="element">要点击的元素<see cref="AutomationElement"/>最好保证是最近刷新的元素</param>
        public static void SilenceClickExt(this IWeChatWindow wxWindow, AutomationElement element)
        {
            Wait.UntilInputIsProcessed();
            var windowHandle = wxWindow.SelfWindow.Properties.NativeWindowHandle.Value;
            var elementRectangle = element.BoundingRectangle;

            // 计算按钮中心点相对于窗口的坐标
            int x = elementRectangle.X + (elementRectangle.Width / 2);
            int y = elementRectangle.Y + (elementRectangle.Height / 2);

            // 转换为窗口客户区坐标
            var windowRectangle = wxWindow.SelfWindow.BoundingRectangle;
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
        public static void SilenceEnterText(this IWeChatWindow wxWindow, TextBox edit, string text)
        {
            wxWindow.SilenceClickExt(edit);

            var hwnd = wxWindow.SelfWindow.Properties.NativeWindowHandle.Value;
            var rect = edit.BoundingRectangle;
            int x = (int)((rect.Left + rect.Right) / 2 - wxWindow.SelfWindow.BoundingRectangle.Left);
            int y = (int)((rect.Top + rect.Bottom) / 2 - wxWindow.SelfWindow.BoundingRectangle.Top);
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
        public static void SilenceReturn(this IWeChatWindow wxWindow, TextBox edit)
        {
            wxWindow.SilenceClickExt(edit);
            Wait.UntilInputIsProcessed();

            var hwnd = wxWindow.SelfWindow.Properties.NativeWindowHandle.Value;
            var rect = edit.BoundingRectangle;
            int x = (int)((rect.Left + rect.Right) / 2 - wxWindow.SelfWindow.BoundingRectangle.Left);
            int y = (int)((rect.Top + rect.Bottom) / 2 - wxWindow.SelfWindow.BoundingRectangle.Top);
            IntPtr lParam = (IntPtr)((y << 16) | (x & 0xFFFF));
            User32.SendMessage(hwnd, WindowsMessages.WM_KEYDOWN, (IntPtr)VirtualKeyShort.RETURN, lParam);
        }

        /// <summary>
        /// 静默粘贴
        /// </summary>
        /// <param name="wxWindow">微信窗口封装<see cref="IWeChatWindow"/></param>
        /// <param name="filePath">文件路径数组</param>
        /// <param name="edit"></param>
        public static void SilencePaste(this IWeChatWindow wxWindow, string[] filePath, TextBox edit)
        {
            // 1. 先点击输入框获取焦点
            wxWindow.SilenceClickExt(edit);
            Wait.UntilInputIsProcessed();

            // 2. 复制文件到剪贴板
            var result = ClipboardApi.CopyFilesToClipboard(filePath);
            if (!result)
            {
                throw new Exception($"复制文件到剪贴板失败: {string.Join(", ", filePath)}");
            }
            
            // 3. 等待剪贴板操作完成
            Wait.UntilInputIsProcessed();
            System.Threading.Thread.Sleep(100); // 额外等待确保剪贴板数据就绪
            
            // 4. 发送粘贴消息
            var hwnd = wxWindow.SelfWindow.Properties.NativeWindowHandle.Value;
            var rect = edit.BoundingRectangle;
            int x = (int)((rect.Left + rect.Right) / 2 - wxWindow.SelfWindow.BoundingRectangle.Left);
            int y = (int)((rect.Top + rect.Bottom) / 2 - wxWindow.SelfWindow.BoundingRectangle.Top);
            IntPtr lParam = (IntPtr)((y << 16) | (x & 0xFFFF));
            
            // 5. 发送粘贴消息
            var pasteResult = User32.SendMessage(hwnd, WindowsMessages.WM_PASTE, IntPtr.Zero, lParam);
            
            // 6. 等待粘贴操作完成
            Wait.UntilInputIsProcessed();
        }
    }
}
using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using WxAutoCommon.Interface;
using WxAutoCore.Components;

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
        public static void ClickExt(this IWeChatWindow wxWindow, AutomationElement element)
        {
            Wait.UntilInputIsProcessed();
            var lastWindow = wxWindow.SelfWindow.Automation.GetDesktop().FindFirstChild(cf => cf.ByControlType(ControlType.Window)
                .And(cf.ByProcessId(wxWindow.ProcessId)));
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
    }
}
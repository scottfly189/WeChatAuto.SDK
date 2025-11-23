using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using WeChatAuto.Extentions;
using WeChatAuto.Services;
using WxAutoCommon.Simulator;
using WxAutoCommon.Utils;

namespace WeChatAuto.Utils
{
    public static class ClickEnhanceExtentions
    {
        /// <summary>
        /// 点击增强
        /// 实现键鼠模拟器硬件点击方式与自动化点击方式的兼容
        /// 注意：此方法需要运行在UIThreadInvoker线程中
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="window">窗口</param>
        public static void ClickEnhance(this AutomationElement element, Window window)
        {
            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
            {
                KMSimulatorService.LeftClick(window, element);
            }
            else
            {
                element.WaitUntilClickable();
                element.Click();
            }
        }
        /// <summary>
        /// 双击增强
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="window">窗口</param>
        public static void DblClickEnhance(this AutomationElement element, Window window)
        {
            if (WeAutomation.Config.EnableMouseKeyboardSimulator)
            {
                KMSimulatorService.LeftDblClickWithDpiAware(window, element);
            }
            else
            {
                var point = element.BoundingRectangle.Center();
                Mouse.MoveTo(point);
                Mouse.LeftClick();
                Mouse.LeftClick();
            }
        }   
    }
}
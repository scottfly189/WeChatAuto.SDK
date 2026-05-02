using FlaUI.Core.AutomationElements;
using WeChatAuto.Services;

namespace WeChatAuto.Extentions
{
    public static class TypeNewStringExtentions
    {
        /// <summary>
        /// 清空输入框并输入字符串
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="str">字符串</param>
        /// <param name="window">窗口</param>
        public static void TypeNewString(this AutomationElement element, string str, Window window)
        {
            KMSimulatorService.ClearAndTypeString(str, window, element);
        }
        /// <summary>
        /// 清空输入框并输入字符串并按下回车键
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="str">字符串</param>
        /// <param name="window">窗口</param>
        public static void TypeNewStringAndEnter(this AutomationElement element, string str, Window window)
        {
            KMSimulatorService.ClearAndTypeString(str, window, element);
            KMSimulatorService.Enter(window, element);
        }
        /// <summary>
        /// 按下回车键
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="window">窗口</param>
        public static void Enter(this AutomationElement element, Window window)
        {
            KMSimulatorService.Enter(window, element);
        }
        /// <summary>
        /// 只输入字符串,不按下回车键,不清空输入框
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="str">字符串</param>
        /// <param name="window">窗口</param>
        public static void OnlyTypeString(this AutomationElement element, string str, Window window)
        {
            KMSimulatorService.KeyPressString(str);
        }
    }
}
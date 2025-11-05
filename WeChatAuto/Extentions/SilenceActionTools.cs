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

namespace WeChatAuto.Extentions
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
          => wxWindow.SelfWindow.SilenceClickExt(element);

        /// <summary>
        /// 静默输入文本
        /// </summary>
        /// <param name="element">输入框<see cref="TextBox"/></param>
        /// <param name="text">文本</param>
        public static void SilenceEnterText(this IWeChatWindow wxWindow, TextBox edit, string text)
          => wxWindow.SelfWindow.SilenceEnterText(edit, text);
        /// <summary>
        /// 静默回车
        /// </summary>
        /// <param name="wxWindow">微信窗口封装<see cref="IWeChatWindow"/></param>
        /// <param name="edit">输入框<see cref="TextBox"/></param>
        public static void SilenceReturn(this IWeChatWindow wxWindow, TextBox edit)
          => wxWindow.SelfWindow.SilenceReturn(edit);



        /// <summary>
        /// 使用SendKeys的简单方法
        /// </summary>
        public static void SilencePasteSimple(this IWeChatWindow wxWindow, string[] filePath, TextBox edit)
          => wxWindow.SelfWindow.SilencePasteSimple(filePath, edit);
    }
}
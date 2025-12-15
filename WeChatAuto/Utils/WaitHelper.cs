using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using WeAutoCommon.Utils;

namespace WeChatAuto.Utils
{
    public static class WaitHelper
    {
        /// <summary>
        /// 等待控件可输入
        /// 如果控件是文本框，则等待文本框可输入
        /// </summary>
        public static bool WaitTextBoxReady(AutomationElement element, TimeSpan timeout, UIThreadInvoker uiThreadInvoker)
        {
            return uiThreadInvoker.Run(automation =>
            {
                return Retry.WhileFalse(() =>
                {
                    if (element == null) return false;
                    var textBox = element.AsTextBox();
                    if (textBox == null) return false;
                    return !textBox.IsOffscreen
                           && textBox.IsEnabled
                           && !textBox.IsReadOnly;
                },
                timeout: timeout,
                interval: TimeSpan.FromMilliseconds(100));
            }).GetAwaiter().GetResult().Success;
        }

        public static bool WaitWindowReady(AutomationElement window, TimeSpan timeout, UIThreadInvoker uiThreadInvoker)
        {
            return uiThreadInvoker.Run(automation =>
            {
                return Wait.UntilResponsive(window, timeout);
            }).GetAwaiter().GetResult();
        }
    }
}
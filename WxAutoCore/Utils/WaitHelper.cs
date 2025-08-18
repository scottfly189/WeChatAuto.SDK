using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

namespace WxAutoCore.Utils
{
    public static class WaitHelper
    {
        /// <summary>
        /// 等待控件可输入
        /// 如果控件是文本框，则等待文本框可输入
        /// </summary>
        public static bool WaitTextBoxReady(AutomationElement element, TimeSpan timeout)
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
            interval: TimeSpan.FromMilliseconds(100)).Success;
        }

        public static bool WaitWindowReady(AutomationElement window, TimeSpan timeout) => Wait.UntilResponsive(window, timeout);
    }
}
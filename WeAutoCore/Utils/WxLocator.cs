using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

namespace WxAutoCore.Utils
{
    /// <summary>
    /// 微信元素定位器
    /// 谨慎使用,在保证此元素与定位条件一致的情况下使用
    /// </summary>
    public static class WxLocator
    {
        /// <summary>
        /// 定位元素
        /// 具体WxLocation使用请看<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="element">父元素</param>
        /// <param name="location">定位信息</param>
        /// <returns>定位到的元素</returns>
        /// <exception cref="Exception">定位失败</exception>
        public static AutomationElement LocateElement(this AutomationElement element, WxLocation location)
        {
            if (!string.IsNullOrEmpty(location.AutomationId))
            {
                return Retry.WhileNull(() => element.FindFirstDescendant(cf => cf.ByAutomationId(location.AutomationId)), timeout: TimeSpan.FromSeconds(5)).Result;
            }
            else if (location.Condition != null)
            {
                return Retry.WhileNull(() => element.FindFirst(TreeScope.Descendants, location.Condition), timeout: TimeSpan.FromSeconds(5)).Result;
            }
            else if (!string.IsNullOrEmpty(location.XPath))
            {
                return Retry.WhileNull(() => element.FindFirstByXPath(location.XPath), timeout: TimeSpan.FromSeconds(5)).Result;
            }
            throw new Exception("locate automation element failed");
        }

        /// <summary>
        /// 定位元素
        /// </summary>
        /// <param name="parent">父元素</param>
        /// <param name="location">定位信息</param>
        /// <returns>定位到的元素</returns>
        public static AutomationElement Locate(AutomationElement parent, WxLocation location)
        {
            return parent.LocateElement(location);
        }
    }
}
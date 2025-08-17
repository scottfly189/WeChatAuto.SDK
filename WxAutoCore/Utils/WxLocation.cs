using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace WxAutoCore.Utils
{
    /// <summary>
    /// 微信元素定位信息
    /// </summary>
    public class WxLocation
    {
        public AutomationElement ParentElement { get; set; }  // 元素
        public string AutomationId { get; set; }  // 自动化ID
        public ConditionBase Condition { get; set; }   // 条件工厂生成
        public string XPath { get; set; }              // XPath
        /// <summary>
        /// 构造函数
        /// 具体被使用请看<see cref="WxLocator"/>类
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="automationId">自动化ID</param>
        /// <param name="xPath">XPath</param>
        public WxLocation(AutomationElement parentElement, string automationId = "", string xPath = "")
        {
            ParentElement = parentElement;
            AutomationId = automationId;
            XPath = xPath;
        }
        /// <summary>
        /// 构造函数
        /// 具体被使用请看<see cref="WxLocator"/>类
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="condition">条件</param>
        public WxLocation(AutomationElement parentElement, ConditionBase condition)
        {
            ParentElement = parentElement;
            Condition = condition;
        }
        /// <summary>
        /// 构造函数
        /// 具体被使用请看<see cref="WxLocator"/>类
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="automationId">自动化ID</param>
        /// <param name="condition">条件</param>
        /// <param name="xPath">XPath</param>
        public WxLocation(AutomationElement parentElement, string automationId = "", ConditionBase condition = null, string xPath = "") : this(parentElement, automationId, xPath)
        {
            Condition = condition;
        }

        /// <summary>
        /// 刷新元素
        /// </summary>
        /// <returns>刷新后的元素</returns>
        /// <exception cref="Exception">刷新失败</exception>
        public AutomationElement RefreshElement()
        {
            if (ParentElement != null)
            {
                return ParentElement.LocateElement(this);
            }
            throw new Exception("refresh automation element failed");
        }
    }
}
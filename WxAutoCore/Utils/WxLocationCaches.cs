using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace WxAutoCore.Utils
{
    public class WxLocationCaches
    {
        private readonly Dictionary<string, WxLocation> _locations = new Dictionary<string, WxLocation>();
        /// <summary>
        /// 获取所有定位器名称
        /// </summary>
        public List<string> LocationNames => _locations.Keys.ToList<string>();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WxLocationCaches()
        {

        }
        /// <summary>
        /// 获取元素
        /// 
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <returns>元素</returns>
        public AutomationElement GetElement(string locatorName)
        {
            return RefreshElement(locatorName);
        }
        /// <summary>
        /// 获取定位信息
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <returns>定位信息</returns>
        public WxLocation GetLocation(string locatorName) => _locations.TryGetValue(locatorName, out var locator) ? locator : null;

        /// <summary>
        /// 刷新元素
        /// 定位信息请参考<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <returns>刷新后的元素</returns>
        /// <exception cref="Exception">定位器不存在</exception>
        public AutomationElement RefreshElement(string locatorName)
        {
            WxLocation location = GetLocation(locatorName);
            if (location == null)
            {
                throw new Exception("location not found");
            }
            return location.RefreshElement();
        }
        /// <summary>
        /// 添加定位信息,详情请看<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <param name="locator">定位信息</param>
        public void AddLocation(string locatorName, WxLocation locator)
        {
            _locations[locatorName] = locator;
        }
        /// <summary>
        /// 添加定位器,详情请看<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <param name="parentElement">父元素</param>
        /// <param name="condition">条件</param>
        public void AddConditionLocation(string locatorName, AutomationElement parentElement, ConditionBase condition)
        {
            _locations[locatorName] = new WxLocation(parentElement, condition: condition);
        }
        /// <summary>
        /// 添加定位器,详情请看<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <param name="parentElement">父元素</param>
        /// <param name="xPath">XPath</param>
        public void AddXPathLocation(string locatorName, AutomationElement parentElement, string xPath)
        {
            _locations[locatorName] = new WxLocation(parentElement, "", xPath: xPath);
        }
        /// <summary>
        /// 添加定位器,详情请看<see cref="WxLocation"/>类
        /// </summary>
        /// <param name="locatorName">定位器名称</param>
        /// <param name="parentElement">父元素</param>
        /// <param name="automationId">自动化ID</param>
        public void AddAutomationIdLocation(string locatorName, AutomationElement parentElement, string automationId)
        {
            _locations[locatorName] = new WxLocation(parentElement, automationId: automationId, xPath: "");
        }
        /// <summary>
        /// 清空定位器
        /// </summary>
        public void Clear()
        {
            _locations.Clear();
        }
    }
}
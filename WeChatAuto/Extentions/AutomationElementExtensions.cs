using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace WeChatAuto.Extentions
{
    public static class AutomationElementExtensions
    {
        /// <summary>
        /// 使用 TreeWalker 获取指定偏移量的同级元素。
        /// offset = +1 表示下一个同级；-1 表示上一个同级。
        /// </summary>
        public static AutomationElement GetSibling(this AutomationElement element, int offset)
        {
            if (element == null) return null;

            var automation = element.Automation;
            var walker = automation.TreeWalkerFactory.GetControlViewWalker();
            // 也可换成 GetRawViewWalker() / GetContentViewWalker()

            if (offset == 0)
                return element;

            var sibling = element;

            if (offset > 0)
            {
                for (int i = 0; i < offset; i++)
                {
                    sibling = walker.GetNextSibling(sibling);
                    if (sibling == null) return null;
                }
            }
            else
            {
                for (int i = 0; i < -offset; i++)
                {
                    sibling = walker.GetPreviousSibling(sibling);
                    if (sibling == null) return null;
                }
            }

            return sibling;
        }

        /// <summary>
        /// 获取父元素。
        /// </summary>
        public static AutomationElement GetParent(this AutomationElement element)
        {
            if (element == null) return null;
            var walker = element.Automation.TreeWalkerFactory.GetControlViewWalker();
            return walker.GetParent(element);
        }
    }
}

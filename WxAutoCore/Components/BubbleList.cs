using FlaUI.Core.AutomationElements;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 聊天内容区气泡列表
    /// </summary>
    public class BubbleList
    {
        private Window _SelfWindow;
        private AutomationElement _BubbleListRoot;
        public Bubble[] Bubbles { get; set; }
        public BubbleList(Window selfWindow, AutomationElement bubbleListRoot)
        {
            _SelfWindow = selfWindow;
            _BubbleListRoot = bubbleListRoot;
        }

        /// <summary>
        /// 获取气泡列表
        /// </summary>
        public void GetBubbles()
        {

        }

        /// <summary>
        /// 查看更多
        /// </summary>
        public void LookMore()
        {

        }
    }
}
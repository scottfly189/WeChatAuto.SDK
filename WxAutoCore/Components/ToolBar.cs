using FlaUI.Core.AutomationElements;
using WxAutoCommon.Utils;

namespace WxAutoCore.Components
{
    public class ToolBar
    {
        private AutomationElement _ToolBar;
        private Window _Window;
        private Button _TopButton;
        private Button _MinButton;
        private Button _MaxButton;   //最大化或者还原
        private Button _CloseButton;

        public AutomationElement ToolBarInfo => _ToolBar;
        public ToolBar(Window window)
        {
            _Window = window;
            _ToolBar = _Window.FindFirstByXPath("/Pane[2]/Pane/Pane[2]/ToolBar");
            var childen = _TopButton.FindAllChildren();
            _TopButton = childen[0].AsButton();
            _MinButton = childen[1].AsButton();
            _MaxButton = childen[2].AsButton();
            _CloseButton = childen[3].AsButton();
        }

        /// <summary>
        /// 置顶
        /// </summary>
        public void Top(bool isTop = true)
        {
            if (isTop)
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_TOP_BUTTON))
                {
                    _TopButton.Invoke();
                }
            }
            else
            {
                if (_TopButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_UNTOP_BUTTON))
                {
                    _TopButton.Invoke();
                }
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        public void Min()
        {
            _MinButton.Invoke();
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public void Max()
        {
            if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_MAX_BUTTON))
            {
                _MaxButton.Invoke();
            }
        }

        /// <summary>
        /// 还原
        /// </summary>
        public void Restore()
        {
            if (_MaxButton.Name.Equals(WeChatConstant.WECHAT_SYSTEM_RESTORE_BUTTON))
            {
                _MaxButton.Invoke();
            }
        }
    }
}
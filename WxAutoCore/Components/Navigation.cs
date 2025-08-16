using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WxAutoCommon.Enums;
using WxAutoCore.Utils;

namespace WxAutoCore.Components
{
    public class Navigation
    {
        private Window _Window;
        private List<Button> _NavigationButtons = new List<Button>();
        public List<Button> NavigationButtons => _NavigationButtons;
        public Navigation(Window window)
        {
            _Window = window;
            _RefreshNavigation();
        }
        private void _RefreshNavigation()
        {
            _NavigationButtons.Clear();
            var navigationRoot = _Window.FindFirstByXPath("/Pane/Pane/ToolBar");
            var buttons = navigationRoot.FindAllChildren(cf => cf.ByControlType(ControlType.Button));
            foreach (var button in buttons)
            {
                _NavigationButtons.Add(button.AsButton());
            }
            var rootPane = _Window.FindFirstByXPath("/Pane/Pane/ToolBar/Pane[1]");
            buttons = rootPane.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            foreach (var button in buttons)
            {
                //视频号、看一看、搜一搜
                _NavigationButtons.Add(button.AsButton());
            }
            var buttonItem = _Window.FindFirstByXPath("/Pane/Pane/ToolBar/Pane[2]").FindFirstDescendant(cf => cf.ByControlType(ControlType.Button)).AsButton();
            _NavigationButtons.Add(buttonItem);
            buttonItem = _Window.FindFirstByXPath("/Pane/Pane/ToolBar/Pane[3]").FindFirstDescendant(cf => cf.ByControlType(ControlType.Button)).AsButton();
            _NavigationButtons.Add(buttonItem);
            buttonItem = _Window.FindFirstByXPath("/Pane/Pane/ToolBar/Pane[4]").FindFirstDescendant(cf => cf.ByControlType(ControlType.Button)).AsButton();
            _NavigationButtons.Add(buttonItem);
        }
        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="navigationType">导航栏类型</param>
        public void SwitchNavigation(NavigationType navigationType)
        {
            _RefreshNavigation();
            var name = navigationType.ToString();
            var button = _NavigationButtons.FirstOrDefault(b => b.Name.Equals(name));
            if (button != null)
            {
                DrawHightlightHelper.DrawHightlight(button);
                button.Click();
            }
        }


    }
}
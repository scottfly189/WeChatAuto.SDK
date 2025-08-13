using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 弹出窗口列表
    /// </summary>
    public class PopWinList
    {
        private List<PopWin> _PopWins = new List<PopWin>();

        public List<PopWin> PopWins => _PopWins;

        public PopWinList(Window window)
        {
            RefreshPopWins(window);
        }

        /// <summary>
        /// 刷新弹出窗口列表
        /// </summary>
        /// <param name="window">窗口</param>
        public void RefreshPopWins(Window window)
        {
            _PopWins.Clear();
            var desktop = window.Automation.GetDesktop();
            var popWins = desktop.FindAllChildren(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(window.Properties.ProcessId))));
            foreach (var popWin in popWins)
            {
                _PopWins.Add(new PopWin(popWin.AsWindow(), popWin.Name));
            }
        }
    }
}
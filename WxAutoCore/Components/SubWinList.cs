using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core;
using System.Linq;
using FlaUI.Core.Tools;
using System;

namespace WxAutoCore.Components
{
    /// <summary>
    /// 子窗口列表
    /// </summary>
    public class SubWinList
    {
        private WxWindow _MainWxWindow;
        private Window _MainFlaUIWindow;

        public SubWinList(Window window, WxWindow wxWindow)
        {
            _MainWxWindow = wxWindow;
            _MainFlaUIWindow = window;
        }
        /// <summary>
        /// 得到所有子窗口名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllSubWinNames()
        {
            var desktop = _MainFlaUIWindow.Automation.GetDesktop();
            var subWinRetry = Retry.WhileNull(()=>desktop.FindAllChildren(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId)))),
                        timeout:TimeSpan.FromSeconds(10),
                        interval:TimeSpan.FromMilliseconds(200));
            if (subWinRetry.Success)
            {
                return subWinRetry.Result.ToList().Select(subWin => subWin.Name).ToList();
            }
            return new List<string>();
        }
        /// <summary>
        /// 判断子窗口是否打开
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns></returns>
        public bool GetSubWinIsOpen(string name)
        {
            var subWin = GetSubWin(name);
            return subWin != null;
        }

        /// <summary>
        /// 根据名称获取子窗口
        /// </summary>
        /// <param name="name">子窗口名称</param>
        /// <returns>子窗口对象<see cref="WxSubWindow"/></returns>
        public WxSubWindow GetSubWin(string name)
        {
            var desktop = _MainFlaUIWindow.Automation.GetDesktop();
            var subWin = Retry.WhileNull(() => desktop.FindFirstChild(cf => cf.ByClassName("ChatWnd")
                        .And(cf.ByControlType(ControlType.Window)
                        .And(cf.ByProcessId(_MainFlaUIWindow.Properties.ProcessId))
                        .And(cf.ByName(name)))),
                        timeout: TimeSpan.FromSeconds(10),
                        interval: TimeSpan.FromMilliseconds(200));
            if (subWin.Success)
            {
                return new WxSubWindow(subWin.Result.AsWindow(), _MainWxWindow);
            }
            return null;
        }
    }
}
using System.Diagnostics;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;
using FlaUI.Core;
using FlaUI.UIA3;
using WeChatAuto.Components;
using WeAutoCommon.Models;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WeAutoCommon.Utils;
using FlaUI.Core.Definitions;

namespace WeChatAuto.Utils
{
    public class ShellNotifyHelper
    {
        public static Maybe<AutomationElement[]> GetNotifyIcons(AutomationElement taskBar)
        {
            //第一种情况：有“用户提示通知区域"
            (AutomationElement[] el, bool success) result = _GetNotifyIconButton_1(taskBar);
            if (result.success)
                return result.el.ToMaybe();
            result = _GetNotifyIconButton_2(taskBar);
            if (result.success)
                return result.el.ToMaybe();

            throw new Exception("错误：你没有打开微信或者你系统的微信UI Tree不在Wechatauto.sdk的支持范围，请联系作者予以支持!");
        }

        private static (AutomationElement[] el, bool success) _GetNotifyIconButton_1(AutomationElement taskBar)
        {
            var root = taskBar.FindFirstDescendant(cf => cf.ByName("用户提示通知区域").And(cf.ByControlType(ControlType.ToolBar)));
            if (root != null)
            {
                var elements = root.FindAllChildren(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("微信")));
                if (elements.Length > 0)
                    return (elements, true);
            }
            else
            {
                WeChatConstant.WECHAT_SYSTEM_NAME = "WeChat";
                var elements = taskBar.FindAllChildren(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("WeChat")));
                if (elements.Length > 0)
                    return (elements, true);
            }
            return (new AutomationElement[0], false);
        }

        private static (AutomationElement[] el, bool success) _GetNotifyIconButton_2(AutomationElement taskBar)
        {
            var xPath = "//Button[@ClassName='SystemTray.NormalButton'][@Name='微信'] | //Button[@ClassName='SystemTray.NormalButton'][@Name=' 微信'] | //Button[@ClassName='SystemTray.NormalButton'][@Name='WeChat']";
            var result = Retry.WhileNull(() => taskBar.FindAllByXPath(xPath),
                      timeout: TimeSpan.FromSeconds(2), interval: TimeSpan.FromMilliseconds(200));
            if (result.Success)
            {
                return (result.Result, true);
            }
            return (new AutomationElement[0], false);
        }
    }
}

using FlaUI.Core.AutomationElements;
using WxAutoCommon.Configs;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;
using WxAutoCore.Services;

namespace WxAutoCore.Utils
{
    public static class DrawHightlightHelper
    {
        public static void DrawHightlight(AutomationElement element, UIThreadInvoker uiThreadInvoker)
        {
            if (WeAutomation.Config.DebugMode && element != null)
            {
                uiThreadInvoker.Run(automation => element.DrawHighlight());
            }
        }
    }
}
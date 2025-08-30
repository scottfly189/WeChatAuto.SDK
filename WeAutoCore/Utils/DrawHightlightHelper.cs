
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Configs;
using WxAutoCommon.Models;
using WxAutoCommon.Utils;

namespace WxAutoCore.Utils
{
    public static class DrawHightlightHelper
    {
        public static void DrawHightlight(AutomationElement element, UIThreadInvoker uiThreadInvoker)
        {
            if (WeChatConfig.DebugMode && element != null)
            {
                uiThreadInvoker.Run(automation => element.DrawHighlight());
            }
        }
    }
}
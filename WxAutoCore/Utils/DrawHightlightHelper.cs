
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Models;

namespace WxAutoCore.Utils
{
    public static class DrawHightlightHelper
    {
        public static void DrawHightlight(AutomationElement element)
        {
            if (WxAutoConfig.DebugMode && element != null)
            {
                element.DrawHighlight();
            }
        }
    }
}
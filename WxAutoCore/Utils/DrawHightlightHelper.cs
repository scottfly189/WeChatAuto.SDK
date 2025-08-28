
using FlaUI.Core.AutomationElements;
using WxAutoCommon.Models;

namespace WxAutoCore.Utils
{
    public static class DrawHightlightHelper
    {
        public static void DrawHightlight(AutomationElement element)
        {
            if (WeChatConfig.DebugMode && element != null)
            {
                element.DrawHighlight();
            }
        }
    }
}
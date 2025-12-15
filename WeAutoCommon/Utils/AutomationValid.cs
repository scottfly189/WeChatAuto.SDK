using FlaUI.Core.AutomationElements;

namespace WeAutoCommon.Utils
{
    public static class AutomationValid
    {
        public static bool IsValid(AutomationElement el)
        {
            try
            {
                if (el == null) return false;
                var name = el?.Name;  // 若失效，这里会抛异常
                return el != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
using WxAutoCommon.Enums;

namespace WxAutoCommon.Models
{
    /// <summary>
    /// 自动化动作
    /// </summary>
    public class AutoAction
    {
        public ActionType ActionType { get; set; }
        public string ToUser { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
    }
}
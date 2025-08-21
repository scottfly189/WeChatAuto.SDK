using FlaUI.Core.AutomationElements;

namespace WxAutoCommon.Interface
{
    /// <summary>
    /// 微信窗口接口
    /// </summary>
    public interface IWeChatWindow
    {
        Window SelfWindow { get; set; }
        int ProcessId { get; }
    }
}
using FlaUI.Core.AutomationElements;

namespace WxAutoCommon.Interface
{
    /// <summary>
    /// 聊天内容接口
    /// </summary>
    public interface IWeChatWindow
    {
        Window SelfWindow { get; set; }
        int ProcessId { get; }
    }
}
using System.Text.Json.Serialization;
using WeAutoCommon.Enums;

namespace Server.WebApi.Contracts.Requests;

/// <summary>
/// 保存头像到指定路径。
/// </summary>
public sealed class SaveAvatarRequest
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";
}

/// <summary>
/// 点击任务栏微信图标。index 与 wechat_name 二选一。
/// </summary>
public sealed class NotifyIconClickRequest
{
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("wechat_name")]
    public string? WechatName { get; set; }
}

/// <summary>
/// 打开/关闭指定好友或群的子窗口。
/// </summary>
public sealed class SubWindowRequest
{
    [JsonPropertyName("who")]
    public string Who { get; set; } = "";
}

/// <summary>
/// 切换/关闭导航栏窗口。
/// </summary>
public sealed class SwitchNavigationRequest
{
    [JsonPropertyName("navigation_type")]
    public NavigationType NavigationType { get; set; }
}

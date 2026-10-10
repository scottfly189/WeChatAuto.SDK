using System.Text.Json.Serialization;
using FlaUI.Core.AutomationElements;

namespace Server.WebApi.Contracts.Responses;

/// <summary>
/// 窗口句柄响应，用于把 FlaUI <see cref="Window"/> 映射为可序列化的句柄。
/// </summary>
public sealed class WindowHandleResponse
{
    [JsonPropertyName("handler")]
    public long Handler { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    public static WindowHandleResponse From(Window? window)
    {
        if (window is null)
            return new WindowHandleResponse();

        window.Properties.Name.TryGetValue(out var title);
        return new WindowHandleResponse
        {
            Handler = window.Properties.NativeWindowHandle.ValueOrDefault.ToInt64(),
            Title = title,
        };
    }
}

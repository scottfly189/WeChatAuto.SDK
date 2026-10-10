using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.WebApi.Converters;

/// <summary>
/// 将 <see cref="Image"/> 序列化为 null，避免把其庞大的内部属性写入 JSON。
/// 主要用于 <see cref="WeAutoCommon.Models.FriendInfo.AvatarImage"/> 这类不可序列化成员。
/// </summary>
public sealed class ImageJsonConverter : JsonConverter<Image>
{
    public override Image? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => null;

    public override void Write(Utf8JsonWriter writer, Image? value, JsonSerializerOptions options)
        => writer.WriteNullValue();
}

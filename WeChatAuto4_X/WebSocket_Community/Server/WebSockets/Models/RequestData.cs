using System.Text.Json;
using System.Text.Json.Serialization;


/// <summary>
/// 请求数据，用于网络传输
/// </summary>
public class RequestData
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }      //ping /pong / command
    [JsonPropertyName("data")]
    public string? Data { get; set; }
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }
}
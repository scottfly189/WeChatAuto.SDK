using System.Text.Json;
using Server.WebApi.Models;

namespace Server.WebApi.Services;

/// <summary>
/// App.json 配置文件的读写服务。
/// </summary>
public sealed class AppConfigStore : IAppConfigStore
{
    /// <summary>
    /// 配置文件路径：程序运行目录下的 App.json。
    /// 源码目录中的 App.json 仅作为模板，构建时由 csproj（CopyToOutputDirectory=PreserveNewest）
    /// 拷贝到运行目录；实际读写都以运行目录这份为准。
    /// </summary>
    public string DefaultPath { get; } = Path.Combine(AppContext.BaseDirectory, "App.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
    };

    public AppConfig Load()
    {
        if (!File.Exists(DefaultPath))
            return new AppConfig();

        var json = File.ReadAllText(DefaultPath);
        if (string.IsNullOrWhiteSpace(json))
            return new AppConfig();

        return JsonSerializer.Deserialize<AppConfig>(json, JsonOptions) ?? new AppConfig();
    }

    public void Save(AppConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var json = JsonSerializer.Serialize(config, JsonOptions);
        File.WriteAllText(DefaultPath, json);
    }
}

using System.Text.Json;
using Server.WebApi.Models;

namespace Server.WebApi.Services;

/// <summary>
/// App.json 配置文件的读写服务。
/// </summary>
public sealed class AppConfigStore : IAppConfigStore
{
    /// <summary>
    /// App.json 文件路径。
    /// 开发环境下优先读写项目源码目录中的 App.json（否则运行期写入的是 bin 输出目录里的拷贝，
    /// 源码目录的 App.json 会一直保持为空）；发布环境下退回程序运行目录下的 App.json。
    /// </summary>
    public string DefaultPath { get; } = ResolveDefaultPath();

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
        var directory = Path.GetDirectoryName(DefaultPath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
        File.WriteAllText(DefaultPath, json);
    }

    /// <summary>
    /// 解析 App.json 路径：从程序运行目录向上查找，第一个同时包含 App.json 与 .csproj 的目录
    /// 即项目源码目录；找不到时（发布环境）退回运行目录下的 App.json。
    /// </summary>
    private static string ResolveDefaultPath()
    {
        var exeDir = AppContext.BaseDirectory;
        var exeCandidate = Path.Combine(exeDir, "App.json");

        try
        {
            var dir = new DirectoryInfo(exeDir);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "App.json");
                if (File.Exists(candidate) && dir.EnumerateFiles("*.csproj").Any())
                    return candidate;

                dir = dir.Parent;
            }
        }
        catch
        {
            // 目录不可访问等异常时，退回程序运行目录
        }

        return exeCandidate;
    }
}

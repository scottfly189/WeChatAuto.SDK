using Server.WebApi.Models;

namespace Server.WebApi.Services;

/// <summary>
/// App.json 配置的读取与保存。
/// </summary>
public interface IAppConfigStore
{
    /// <summary>
    /// 读取配置；文件不存在或为空时返回带默认值的实例。
    /// </summary>
    AppConfig Load();

    /// <summary>
    /// 保存配置到 App.json。
    /// </summary>
    void Save(AppConfig config);
}

using SqlSugar;
using WeChatAuto.Models;

namespace Server.WebApi.Services;

/// <summary>
/// 基于 SqlSugar 的微信消息数据库帮助工具：数据库类型映射、建库建表、连接测试与数据导入。
/// </summary>
public static class SqlSugarDatabaseHelper
{
    /// <summary>
    /// 界面展示名 → SqlSugar DbType 的映射。MariaDB 走 MySql 驱动；MongoDB/Redis 为 NoSQL，SqlSugarCore 暂不支持。
    /// </summary>
    private static readonly Dictionary<string, DbType> DisplayNameToDbType = new()
    {
        ["SQLite"] = DbType.Sqlite,
        ["MySQL"] = DbType.MySql,
        ["SQL Server"] = DbType.SqlServer,
        ["PostgreSQL"] = DbType.PostgreSQL,
        ["Oracle"] = DbType.Oracle,
        ["MariaDB"] = DbType.MySql,
        ["ClickHouse"] = DbType.ClickHouse,
        ["DB2"] = DbType.DB2,
    };

    /// <summary>
    /// 按界面展示名解析数据库类型；不支持的类型返回 false。
    /// </summary>
    public static bool TryGetDbType(string? displayName, out DbType dbType)
    {
        if (!string.IsNullOrWhiteSpace(displayName) && DisplayNameToDbType.TryGetValue(displayName, out dbType))
            return true;
        dbType = DbType.Custom;
        return false;
    }

    /// <summary>
    /// 按 App.json 中 Database.Type 字符串解析数据库类型（大小写不敏感）。
    /// </summary>
    public static bool TryParseDbType(string? type, out DbType dbType)
    {
        if (!string.IsNullOrWhiteSpace(type) && Enum.TryParse(type, true, out dbType) && dbType != DbType.Custom)
            return true;
        dbType = DbType.Custom;
        return false;
    }

    /// <summary>
    /// 反向取展示名（用于把已保存的类型回填到界面下拉框）。
    /// </summary>
    public static string? GetDisplayName(DbType dbType)
    {
        foreach (var kv in DisplayNameToDbType)
        {
            if (kv.Value == dbType)
                return kv.Key;
        }
        return null;
    }

    /// <summary>
    /// 创建 SqlSugar 客户端。
    /// </summary>
    public static SqlSugarClient CreateClient(DbType dbType, string connectionString)
        => new(new ConnectionConfig
        {
            ConnectionString = connectionString,
            DbType = dbType,
            IsAutoCloseConnection = true,
        });

    /// <summary>
    /// 如果数据库不存在则创建，并初始化 <see cref="WeChatMessage"/> 表（幂等）。
    /// </summary>
    public static void EnsureDatabase(SqlSugarClient db)
    {
        db.DbMaintenance.CreateDatabase();
        db.CodeFirst.InitTables<WeChatMessage>();
    }

    /// <summary>
    /// 测试连接：尝试建库建表，成功返回 true，失败返回 false 并带错误信息。
    /// </summary>
    public static bool TestConnection(DbType dbType, string connectionString, out string? error)
    {
        error = null;
        try
        {
            using var db = CreateClient(dbType, connectionString);
            EnsureDatabase(db);
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 把源库中的 <see cref="WeChatMessage"/> 数据导入目标库，返回导入条数；源库无表或无数据返回 0。
    /// </summary>
    public static int ImportMessages(DbType fromType, string fromConnectionString, DbType toType, string toConnectionString)
    {
        using var from = CreateClient(fromType, fromConnectionString);
        using var to = CreateClient(toType, toConnectionString);
        return ImportMessages(from, to);
    }

    /// <summary>
    /// 把源库中的 <see cref="WeChatMessage"/> 数据导入目标库，返回导入条数；源库无表或无数据返回 0。
    /// </summary>
    public static int ImportMessages(SqlSugarClient from, SqlSugarClient to)
    {
        List<WeChatMessage> rows;
        try
        {
            rows = from.Queryable<WeChatMessage>().ToList();
        }
        catch
        {
            // 源库无表或不可读，视为无可导入数据
            return 0;
        }

        if (rows.Count == 0)
            return 0;

        to.CodeFirst.InitTables<WeChatMessage>();
        to.Insertable(rows).ExecuteCommand();
        return rows.Count;
    }
}

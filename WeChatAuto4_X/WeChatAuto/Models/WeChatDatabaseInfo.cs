using MessagePack;

namespace WeChatAuto.Models;

/// <summary>
/// 本机微信昵称 - wxid的键值对
/// 适用于多微信的本地存储
/// </summary>
[MessagePackObject]
public class WeChatDatabaseInfo
{
    /// <summary>
    /// 本微信的昵称,在本机多微信的各个微信要求昵称不同.
    /// </summary>
    [Key(0)]
    public string NickName { get; set; }
    /// <summary>
    /// 本微信数据库的解密Key
    /// 不需要每次都解密
    /// </summary>
    [Key(1)]
    public string Key { get; set; }
    /// <summary>
    /// 微信wxid
    /// </summary>
    [Key(2)]
    public string OwnerWxId { get; set; }
    /// <summary>
    /// 本微信db_storage目录所在的路径
    /// </summary>
    [Key(3)]
    public string DbStorage { get; set; }
    /// <summary>
    /// 微信存储位置，取法: 微信主窗口--> 设置 --> 账号与存储 --> 存储位置（形如 <c>D:\xwechat_files</c>）
    /// <para>
    /// 作为缓存使用：首轮由 SDK 通过设置页自动化读取后写入，之后启动直接复用，不再操作微信窗口。
    /// 同一台机器的多个微信账号共用同一个存储位置，所以每条记录里存的都是同一个值。
    /// 读取逻辑见 <c>WeChatAuto.Utils.WeChatStorageRootStore</c>。
    /// </para>
    /// </summary>
    [Key(4)]
    public string WeChatStorageRoot { get; set; }
}

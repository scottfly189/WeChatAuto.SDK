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
    public string NickName {get;set;}
    /// <summary>
    /// 本微信数据库的解密Key
    /// 不需要每次都解密
    /// </summary>
    [Key(1)]
    public string Key {get;set;}
    [Key(2)]
    public string OwnerWxId {get;set;}
    [Key(3)]
    public string DbStorage {get;set;}
}

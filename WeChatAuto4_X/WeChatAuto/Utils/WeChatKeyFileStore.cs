#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MessagePack;
using WeChatAuto.Models;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// <c>wechatkey.dat</c> 的唯一读写入口：MessagePack 负载 + DPAPI 加密。
    /// <para>
    /// 文件格式：<c>dpapi1:</c> + Base64( DPAPI( MessagePack( List&lt;WeChatDatabaseInfo&gt; ) ) )。
    /// 旧版本写的是**无前缀的明文 MessagePack**，这里仍然能读（兼容），下次写入自动升级成密文。
    /// </para>
    /// <para>
    /// 为什么必须集中到一处：这个文件同时被「密钥缓存」（<c>WeChatClient</c>）和「微信存储位置缓存」
    /// （<c>WeChatStorageRootStore</c>）读写，任何一处漏掉 DPAPI 都会把整份文件写回明文。
    /// </para>
    /// </summary>
    internal static class WeChatKeyFileStore
    {
        /// <summary>加密负载的文本前缀；出现它才按 DPAPI 解，否则按旧明文解析。</summary>
        const string DpapiPrefix = "dpapi1:";

        static readonly byte[] DpapiPrefixBytes = Encoding.ASCII.GetBytes(DpapiPrefix);

        /// <summary>文件当前是否是加密格式（不存在 / 空文件都返回 <c>false</c>）。</summary>
        public static bool IsEncrypted(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return false;

                using var stream = File.OpenRead(path);
                if (stream.Length < DpapiPrefixBytes.Length)
                    return false;

                var head = new byte[DpapiPrefixBytes.Length];
                var read = stream.Read(head, 0, head.Length);
                if (read != head.Length)
                    return false;

                for (var i = 0; i < head.Length; i++)
                    if (head[i] != DpapiPrefixBytes[i])
                        return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取账号列表。文件不存在 / 空 / 损坏 / 解不开都返回空列表（不抛），
        /// 其中「解不开」会通过 <paramref name="warn"/> 提示，绝不会退回把密文当明文解析。
        /// </summary>
        public static List<WeChatDatabaseInfo> Load(string path, Action<string>? warn = null)
        {
            var empty = new List<WeChatDatabaseInfo>();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return empty;

            byte[] raw;
            try
            {
                raw = File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                warn?.Invoke($"读取 wechatkey.dat 失败，按无缓存处理：{ex.Message}");
                return empty;
            }

            if (raw.Length == 0)
                return empty;

            byte[] payload;
            if (HasDpapiPrefix(raw))
            {
                var plain = Dpapi.Unprotect(DecodeBase64(raw, warn));
                if (plain is null)
                {
                    warn?.Invoke("wechatkey.dat 无法用当前 Windows 用户解密（换用户 / 换机器 / 文件损坏），本次按无缓存处理，将重新获取密钥");
                    return empty;
                }
                payload = plain;
            }
            else
            {
                // 旧格式：明文 MessagePack。可读，但下次写回会升级成 DPAPI。
                payload = raw;
            }

            try
            {
                return MessagePackSerializer.Deserialize<List<WeChatDatabaseInfo>>(payload)
                       ?? empty;
            }
            catch (Exception ex)
            {
                warn?.Invoke($"wechatkey.dat 解析失败，按无缓存处理：{ex.Message}");
                return empty;
            }
        }

        /// <summary>
        /// 写入账号列表（DPAPI 加密，临时文件 + 原子替换）。
        /// DPAPI 不可用时退回明文并 <paramref name="warn"/>，保证功能不因此中断（与旧行为一致）。
        /// </summary>
        public static void Save(string path, List<WeChatDatabaseInfo> entries, Action<string>? warn = null)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path is empty", nameof(path));
            entries ??= new List<WeChatDatabaseInfo>();

            var payload = MessagePackSerializer.Serialize(entries);
            var protectedBytes = Dpapi.Protect(payload);

            byte[] fileBytes;
            if (protectedBytes is null || protectedBytes.Length == 0)
            {
                warn?.Invoke("DPAPI 加密不可用，wechatkey.dat 将以明文保存（与旧版本行为一致）");
                fileBytes = payload;
            }
            else
            {
                var base64 = Convert.ToBase64String(protectedBytes);
                var prefix = Encoding.ASCII.GetBytes(DpapiPrefix);
                fileBytes = new byte[prefix.Length + base64.Length];
                Buffer.BlockCopy(prefix, 0, fileBytes, 0, prefix.Length);
                Encoding.ASCII.GetBytes(base64, 0, base64.Length, fileBytes, prefix.Length);
            }

            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            var tmp = path + ".tmp";
            try
            {
                File.WriteAllBytes(tmp, fileBytes);
                File.Move(tmp, path, overwrite: true);
            }
            catch
            {
                // 写失败/被占用：别把临时文件（含密钥材料）留在盘上。
                try { if (File.Exists(tmp)) File.Delete(tmp); } catch (Exception) { }
                throw;
            }
        }

        static bool HasDpapiPrefix(byte[] raw)
        {
            if (raw.Length < DpapiPrefixBytes.Length)
                return false;
            for (var i = 0; i < DpapiPrefixBytes.Length; i++)
                if (raw[i] != DpapiPrefixBytes[i])
                    return false;
            return true;
        }

        static byte[]? DecodeBase64(byte[] raw, Action<string>? warn)
        {
            try
            {
                var text = Encoding.ASCII.GetString(raw, DpapiPrefixBytes.Length, raw.Length - DpapiPrefixBytes.Length);
                return Convert.FromBase64String(text);
            }
            catch (Exception ex)
            {
                warn?.Invoke($"wechatkey.dat 的加密负载 Base64 损坏，按无缓存处理：{ex.Message}");
                return null;
            }
        }
    }
}


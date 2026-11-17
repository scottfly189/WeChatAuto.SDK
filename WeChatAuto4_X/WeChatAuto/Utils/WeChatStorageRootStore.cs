#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;
using WeAutoCommon.Configs;
using WeChatAuto.Models;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 微信存储位置（<see cref="WeChatConfig.WeChatStorageRoot"/>，形如 <c>D:\xwechat_files</c>）的
    /// 进程内缓存 + <c>wechatkey.dat</c> 持久化。
    /// <para>
    /// 取值优先级：显式配置 &gt; 进程内已解析（多微信共用）&gt; <c>wechatkey.dat</c> 缓存 &gt; 设置页自动化读取。
    /// </para>
    /// <para>
    /// 为什么需要它：自动化读「设置 → 账号与存储 → 存储位置」要开设置窗口，慢且会打断用户操作；
    /// 而同一台机器上的多个微信账号共用同一个存储位置（微信自身也是这么设计的），
    /// 所以整个进程只解析一次，成功后落到 <c>wechatkey.dat</c>，下次启动直接复用。
    /// </para>
    /// <para>
    /// 这里只负责「字符串的规范化 / 缓存 / 读写文件」，不触发 UI 自动化——避免 Utils 反向依赖
    /// <c>WeChatClient</c>；真正的自动化读取由 <c>WeChatClient</c> 在首轮同步前调用。
    /// </para>
    /// </summary>
    internal static class WeChatStorageRootStore
    {
        /// <summary>保护 <c>wechatkey.dat</c> 的读-改-写。</summary>
        static readonly object Gate = new();

        /// <summary>进程内有效值；空串表示尚未解析。</summary>
        public static string Current { get; private set; } = string.Empty;

        /// <summary>
        /// 写入进程内值。会自动规范化；空值 / 非法路径被忽略（不会把已有值清空）。
        /// </summary>
        public static void Set(string? root)
        {
            var normalized = Normalize(root);
            if (normalized.Length > 0)
                Current = normalized;
        }

        /// <summary>
        /// 规范化存储位置：去首尾空白（含误加的双引号）、展开 <c>%ENV%</c> 环境变量、转绝对路径、去掉末尾分隔符。
        /// 空值、明显不是 Windows 绝对路径（如 <c>::bad::</c>）或带非法字符时返回空串，**不抛异常**。
        /// 注意：本方法只做字符串层面校验，**不检查目录是否存在**；需要可用性判断的调用方请再用
        /// <see cref="Directory.Exists(string)"/> 判一次（本类的读取方法已内置该判断）。
        /// </summary>
        public static string Normalize(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return string.Empty;
            try
            {
                var expanded = Environment.ExpandEnvironmentVariables(raw.Trim().Trim('"'));
                if (expanded.Length == 0)
                    return string.Empty;

                // Windows 上不合法的路径字符直接拒绝（GetFullPath 对部分字符不报错，会产出怪路径）
                if (expanded.IndexOfAny(InvalidPathChars) >= 0)
                    return string.Empty;

                var full = Path.GetFullPath(expanded);
                if (!IsWindowsAbsolutePath(full))
                    return string.Empty;

                // 去掉末尾的 \ 或 /（"C:\" 这种根路径保留）
                if (full.Length > 3)
                    full = full.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                return full;
            }
            catch
            {
                return string.Empty;
            }
        }

        static readonly char[] InvalidPathChars = { '"', '<', '>', '|', '*', '?', '\0' };

        /// <summary>
        /// 是否是 Windows 风格绝对路径：<c>X:\...</c> 或 UNC <c>\\server\share</c>。
        /// 用来挡掉 <c>::bad::path::</c> 这类被 <see cref="Path.GetFullPath(string)"/> 放行的怪值。
        /// </summary>
        static bool IsWindowsAbsolutePath(string path)
        {
            if (path.Length >= 3
                && char.IsLetter(path[0])
                && path[1] == ':'
                && (path[2] == '\\' || path[2] == '/'))
                return true;
            return path.StartsWith(@"\\", StringComparison.Ordinal);
        }

        /// <summary>
        /// 从 <c>wechatkey.dat</c> 的账号条目里找一条「有值且目录仍然存在」的存储位置。
        /// 文件不存在 / 解析失败都返回 <c>false</c>。
        /// </summary>
        /// <param name="root">命中的存储位置（已规范化）。</param>
        /// <param name="keyFilePath">缓存文件路径；默认 <see cref="WeChatLocalPaths.WeChatKeyFile"/>，仅为便于测试保留。</param>
        public static bool TryReadFromKeyFile(out string root, string? keyFilePath = null)
        {
            root = string.Empty;
            try
            {
                var entries = ReadKeyFile(keyFilePath);
                foreach (var entry in entries)
                {
                    var candidate = Normalize(entry?.WeChatStorageRoot);
                    if (candidate.Length > 0 && Directory.Exists(candidate))
                    {
                        root = candidate;
                        return true;
                    }
                }
            }
            catch
            {
                // 缓存文件损坏时当作没有缓存，交给自动化重新取。
            }
            return false;
        }

        /// <summary>
        /// 把存储位置写回 <c>wechatkey.dat</c> 的**所有**账号条目（多微信共用同一个位置）。
        /// 文件还不存在时静默跳过：等首次取 key 时由 <c>_ReDecrypt</c> 建文件并写入。
        /// 写失败只回调 <paramref name="warn"/>，不影响同步流程。
        /// </summary>
        /// <param name="root">要写入的存储位置。</param>
        /// <param name="warn">写失败时的提示回调，可为 null。</param>
        /// <param name="keyFilePath">缓存文件路径；默认 <see cref="WeChatLocalPaths.WeChatKeyFile"/>，仅为便于测试保留。</param>
        public static void PersistToKeyFile(string? root, Action<string>? warn = null, string? keyFilePath = null)
        {
            var normalized = Normalize(root);
            if (normalized.Length == 0)
                return;

            lock (Gate)
            {
                try
                {
                    var file = keyFilePath ?? WeChatLocalPaths.WeChatKeyFile;
                    if (!File.Exists(file))
                        return;

                    var entries = ReadKeyFile(keyFilePath);
                    if (entries.Count == 0)
                        return;

                    var changed = false;
                    foreach (var entry in entries)
                    {
                        if (entry == null)
                            continue;
                        if (string.Equals(entry.WeChatStorageRoot, normalized, StringComparison.OrdinalIgnoreCase))
                            continue;
                        entry.WeChatStorageRoot = normalized;
                        changed = true;
                    }
                    if (!changed)
                        return;

                    Directory.CreateDirectory(Path.GetDirectoryName(file)!);
                    File.WriteAllBytes(file, MessagePackSerializer.Serialize(entries));
                }
                catch (Exception ex)
                {
                    warn?.Invoke($"写入 wechatkey.dat 的微信存储位置失败（下次启动会重新读取）：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 初始化阶段（<c>WeAutomation.Initialize</c>）用缓存回填配置：不触发 UI 自动化。
        /// 已经显式配置了值就只做规范化并同步进缓存。
        /// </summary>
        /// <param name="config">要回填的配置对象。</param>
        /// <param name="warn">写缓存失败时的提示回调，可为 null。</param>
        /// <param name="keyFilePath">缓存文件路径；默认走 <see cref="WeChatLocalPaths.WeChatKeyFile"/>，仅为便于测试保留。</param>
        public static void EnsureConfig(WeChatConfig? config, Action<string>? warn = null, string? keyFilePath = null)
        {
            if (config == null)
                return;

            var configured = Normalize(config.WeChatStorageRoot);
            if (configured.Length > 0)
            {
                Current = configured;
                config.WeChatStorageRoot = configured;
                PersistToKeyFile(configured, warn, keyFilePath);
                return;
            }

            if (Current.Length > 0)
            {
                config.WeChatStorageRoot = Current;
                return;
            }

            if (TryReadFromKeyFile(out var cached, keyFilePath))
            {
                Current = cached;
                config.WeChatStorageRoot = cached;
            }
        }

        /// <summary>读取并反序列化 <c>wechatkey.dat</c>；文件不存在返回空列表。</summary>
        static List<WeChatDatabaseInfo> ReadKeyFile(string? keyFilePath = null)
        {
            var file = keyFilePath ?? WeChatLocalPaths.WeChatKeyFile;
            if (!File.Exists(file))
                return new List<WeChatDatabaseInfo>();

            var bytes = File.ReadAllBytes(file);
            if (bytes.Length == 0)
                return new List<WeChatDatabaseInfo>();

            return MessagePackSerializer.Deserialize<List<WeChatDatabaseInfo>>(bytes)
                   ?? new List<WeChatDatabaseInfo>();
        }
    }
}

#nullable enable
using System;
using System.IO;
using WeChatAuto.Services;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 本机微信自动化的数据目录（默认 <c>%LOCALAPPDATA%\WeChatAuto</c>）。
    /// <para>需要跨升级、跨安装目录保留的文件都放这里：数据库密钥缓存 <c>wechatkey.dat</c>、
    /// hot-sync 暂存 <c>hotsync-staging</c>、媒体缓存 <c>media-cache</c>。</para>
    /// <para>暂存 / 媒体缓存 / 朋友圈下载目录都可被配置覆盖（见 <see cref="WeAutomation.Config"/>），
    /// 方便 C 盘空间紧张的用户改到大容量磁盘；为空时仍用下面的默认路径。</para>
    /// <para>不要把这类数据写进程序目录：安装目录可能只读（如 Program Files），
    /// 换目录 / 覆盖升级时也会丢。默认路径一律由 <see cref="Environment.SpecialFolder.LocalApplicationData"/> 推导，
    /// 不写死盘符、用户名或绝对路径。</para>
    /// </summary>
    public static class WeChatLocalPaths
    {
        /// <summary>根目录：<c>%LOCALAPPDATA%\WeChatAuto</c>。</summary>
        public static string Root => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WeChatAuto");

        /// <summary>数据库密钥缓存文件：<c>%LOCALAPPDATA%\WeChatAuto\wechatkey.dat</c>。</summary>
        public static string WeChatKeyFile => Path.Combine(Root, "wechatkey.dat");

        /// <summary>
        /// hot-sync 状态根目录（只存水位指纹/联系人签名/缺口标记，不再存 DB/WAL 副本）。
        /// 默认 <c>%LOCALAPPDATA%\WeChatAuto\hotsync-state</c>；升级时若旧
        /// <c>hotsync-staging</c> 状态目录存在则继续复用，避免水位/指纹重置。
        /// </summary>
        public static string HotSyncStateRoot
        {
            get
            {
                var state = Path.Combine(Root, "hotsync-state");
                var legacy = Path.Combine(Root, "hotsync-staging");
                return !Directory.Exists(state) && Directory.Exists(legacy) ? legacy : state;
            }
        }

        /// <summary>
        /// 消息媒体缓存根目录。默认 <c>%LOCALAPPDATA%\WeChatAuto\media-cache</c>，
        /// 可用 <c>WeChatConfig.MediaCacheDir</c> 覆盖。
        /// </summary>
        public static string MediaCacheRoot => Resolve(WeAutomation.Config.MediaCacheDir, "media-cache");

        /// <summary>
        /// 朋友圈下载媒体根目录。默认 <c>%LOCALAPPDATA%\WeChatAuto\sns-media</c>，
        /// 可用 <c>WeChatConfig.SnsMediaDir</c> 覆盖。只有开启 <c>DownloadSnsImages</c> 时才写入。
        /// </summary>
        public static string SnsMediaRoot => Resolve(WeAutomation.Config.SnsMediaDir, "sns-media");

        /// <summary>
        /// hot-sync 拷贝诊断日志：<c>%LOCALAPPDATA%\WeChatAuto\logs\hotsync-copy.log</c>。
        /// 仅当 <c>WeChatConfig.HotSyncCopyLog=true</c> 时写入（见 <c>WxDecrypt.CopyLogFile</c>）。
        /// </summary>
        public static string HotSyncCopyLogFile => Path.Combine(Root, "logs", "hotsync-copy.log");

        /// <summary>配置为空时回落到默认目录；配置支持 <c>%ENV%</c> 环境变量与相对路径。</summary>
        static string Resolve(string? configured, string defaultName)
        {
            if (!string.IsNullOrWhiteSpace(configured))
            {
                var expanded = Environment.ExpandEnvironmentVariables(configured.Trim());
                return Path.GetFullPath(expanded);
            }
            return Path.Combine(Root, defaultName);
        }

        /// <summary>
        /// 旧版本把 <c>wechatkey.dat</c> 放在程序目录（<see cref="AppContext.BaseDirectory"/>）。
        /// 新位置还没有该文件时，把旧文件迁移过来（只会发生一次），避免升级后重新获取一遍密钥。
        /// </summary>
        /// <param name="legacyDirectory">旧文件所在目录，默认程序目录；留参数只为便于测试。</param>
        /// <returns>发生了迁移返回 <c>true</c>；新旧位置都没有、或新位置已存在则返回 <c>false</c>。</returns>
        public static bool AdoptLegacyWeChatKeyFile(string? legacyDirectory = null)
        {
            var target = WeChatKeyFile;
            if (File.Exists(target))
                return false;

            var legacy = Path.Combine(
                string.IsNullOrWhiteSpace(legacyDirectory) ? AppContext.BaseDirectory : legacyDirectory,
                "wechatkey.dat");
            if (!File.Exists(legacy))
                return false;

            Directory.CreateDirectory(Root);
            try
            {
                File.Move(legacy, target);
            }
            catch (IOException)
            {
                // 跨盘或被占用时退化成“复制 + 尽力删除旧文件”，删不掉也不影响功能。
                File.Copy(legacy, target);
                try
                {
                    File.Delete(legacy);
                }
                catch
                {
                    // 旧文件删不掉（只读/占用）可以忽略：新位置已经可用。
                }
            }

            return true;
        }
    }
}


using System;
using System.IO;

namespace WxAutoCore.Models
{
    /// <summary>
    /// 微信自动化参数配置类
    /// </summary>
    public static class WxParam
    {
        /// <summary>
        /// 语言设置
        /// </summary>
        public static class Language
        {
            public const string Chinese = "Cn";
            public const string ChineseTraditional = "CnT";
            public const string English = "En";
        }

        /// <summary>
        /// 当前语言设置
        /// </summary>
        public static string CurrentLanguage { get; set; } = Language.Chinese;

        /// <summary>
        /// 是否启用日志文件
        /// </summary>
        public static bool EnableFileLogger { get; set; } = true;

        /// <summary>
        /// 下载文件/图片默认保存路径
        /// </summary>
        public static string DefaultSavePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wxauto文件下载");

        /// <summary>
        /// 是否启用消息哈希值用于辅助判断消息，开启后会稍微影响性能
        /// </summary>
        public static bool MessageHash { get; set; } = false;

        /// <summary>
        /// 头像到消息X偏移量，用于消息定位，点击消息等操作
        /// </summary>
        public static int DefaultMessageXbias { get; set; } = 51;

        /// <summary>
        /// 是否强制重新自动获取X偏移量，如果设置为True，则每次启动都会重新获取
        /// </summary>
        public static bool ForceMessageXbias { get; set; } = true;

        /// <summary>
        /// 监听消息时间间隔，单位秒
        /// </summary>
        public static int ListenInterval { get; set; } = 1;

        /// <summary>
        /// 搜索聊天对象超时时间
        /// </summary>
        public static int SearchChatTimeout { get; set; } = 5;
    }
} 
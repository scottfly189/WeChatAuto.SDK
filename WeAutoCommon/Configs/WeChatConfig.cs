using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace WxAutoCommon.Configs
{
    /// <summary>
    /// 微信自动化参数配置类
    /// </summary>
    public class WeChatConfig
    {
        /// <summary>
        /// 接口KEY
        /// </summary>
        public string ApiKey { get; set; } = "";

        /// <summary>
        /// 下载文件/图片默认保存路径
        /// </summary>
        public string DefaultSavePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wxauto_download");

        /// <summary>
        /// 是否启用消息哈希值用于辅助判断消息，开启后会稍微影响性能
        /// </summary>
        public bool MessageHash { get; set; } = false;


        /// <summary>
        /// 监听消息时间间隔，单位秒
        /// </summary>
        public int ListenInterval { get; set; } = 3;
        /// <summary>
        /// 朋友圈监听时间间隔，单位秒
        /// </summary>
        public int MomentsListenInterval { get; set; } = 10;

        /// <summary>
        /// 新用户监听时间间隔，单位秒
        /// </summary>
        public int NewUserListenerInterval { get; set; } = 5;

        /// <summary>
        /// 监听子窗口时间间隔，单位秒
        /// </summary>
        public int MonitorSubWinInterval { get; set; } = 5;


        /// <summary>
        /// 是否启用调试模式
        /// </summary>
        public bool DebugMode { get; set; } = false;
        /// <summary>
        /// 是否启用鼠标键盘模拟器
        /// 启用后，一些普通automation操作不了功能，启用硬件模拟器后，可以操作。
        /// 注意：需要购买键鼠模拟器，并在此处启用。
        /// </summary>
        public bool EnableMouseKeyboardSimulator { get; set; } = false;
        /// <summary>
        /// 键鼠模拟器设备VID
        /// </summary>
        public int KMDeiviceVID { get; set; } = 0x2612;
        /// <summary>
        /// 键鼠模拟器设备PID
        /// </summary>
        public int KMDeivicePID { get; set; } = 0x1701;
    }

    public static class Language
    {
        public static string CurrentLanguage { get; set; } = Language.Chinese;
        public const string Chinese = "Cn";
        public const string ChineseTraditional = "CnT";
        public const string English = "En";
    }
}
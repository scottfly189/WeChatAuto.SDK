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
        /// 检查微信客户端是否运行的时间间隔，单位秒
        /// 用于风控退出时的重试机制
        /// </summary>
        public int CheckAppRunningInterval { get; set; } = 3;
        /// <summary>
        /// 微信客户端退出时的重试等待时间，单位秒,默认等候10秒
        /// </summary>
        public int AppRetryWaitTime { get; set; } = 10;
        /// <summary>
        /// 是否启用检查微信客户端是否运行
        /// 默认启用
        /// </summary>
        public bool EnableCheckAppRunning { get; set; } = true;

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
        public string KMVerifyUserData { get; set; } = "";
        /// <summary>
        /// 点击偏移量,单位像素
        /// 为了避免每次点击都点击到同一个位置，可以设置一个偏移量，实际点击位置为点击位置减去偏移量的一个随机值
        /// </summary>
        public int OffsetOfClick { get; set; } = 5;
        /// <summary>
        /// 输出字符串编码类型,默认使用剪贴板粘贴输出字符串。优点是输出字符多时速度更快且不受输入法影响
        /// </summary>
        public int OutputStringType { get; set; } = 4;
        /// <summary>
        /// 鼠标移动模式
        /// </summary>
        public int MouseMoveMode { get; set; } = 0;
    }

    public static class Language
    {
        public static string CurrentLanguage { get; set; } = Language.Chinese;
        public const string Chinese = "Cn";
        public const string ChineseTraditional = "CnT";
        public const string English = "En";
    }
}
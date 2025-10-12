using System;
using System.IO;

namespace WxAutoCommon.Configs
{
    /// <summary>
    /// 微信自动化参数配置类
    /// </summary>
    public class WeChatConfig
    {

        /// <summary>
        /// 是否启用日志文件
        /// </summary>
        public bool EnableFileLogger { get; set; } = true;

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
        public bool DebugMode { get; set; } = true;

        /// <summary>
        /// 测试好友,要改成自己的才能正确运行单元测试
        /// </summary>
        public string TestFriendNickName { get; set; } = "AI.Net";

        /// <summary>
        /// 备选测试好友,要改成自己的才能正确运行单元测试
        /// </summary>
        public string TestAlternativeFriendNickName { get; set; } = "文件传输助手";

        /// <summary>
        /// 测试客户端,要改成自己的才能正确运行单元测试
        /// </summary>
        public string TestClientName { get; set; } = "Alex Zhao";

        /// <summary>
        /// 测试群组,要改成自己的才能正确运行单元测试
        /// </summary>
        public string TestGroupNickName { get; set; } = ".NET-AI实时快讯3群";

        /// <summary>
        /// 备选测试群组,要改成自己的才能正确运行单元测试
        /// </summary>
        public string TestAlternativeGroupNickName { get; set; } = "备选测试群";

        /// <summary>
        /// 语言设置
        /// </summary>

    }

    public static class Language
    {
        public static string CurrentLanguage { get; set; } = Language.Chinese;
        public const string Chinese = "Cn";
        public const string ChineseTraditional = "CnT";
        public const string English = "En";
    }
}
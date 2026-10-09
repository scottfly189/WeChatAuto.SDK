using System;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Configs;
using WeChatAuto.Components;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using WeAutoCommon.Utils;
using Microsoft.Win32;
using System.Diagnostics;

namespace WeChatAuto.Services
{
    public static class WeAutomation
    {
        private static IServiceCollection _internalServices = null;
        private static IServiceProvider _internalServiceProvider = null;
        private static WeChatConfig _config = new WeChatConfig();
        public static WeChatConfig Config => _config;
        private static InitializationMode _initializationMode = InitializationMode.None;

        public static SynchronizationContext currentContext;

        private enum InitializationMode
        {
            None,
            ExternalDI,      // 使用外部依赖注入框架
            InternalDI       // 使用内部依赖注入框架
        }

        /// <summary>
        /// 统一初始化入口 - 使用外部依赖注入框架
        /// 当宿主应用已有依赖注入框架时使用此重载
        /// </summary>
        /// <param name="services">依赖注入服务集合</param>
        /// <param name="options">配置选项</param>
        /// <returns>IServiceCollection，用于链式调用</returns>
        public static IServiceCollection Initialize(IServiceCollection services, Action<WeChatConfig> options = default)
        {
            currentContext = SynchronizationContext.Current;
            _ = _initializationMode == InitializationMode.InternalDI ?
                throw new InvalidOperationException("不能同时使用外部和内部依赖注入框架。如果已经使用无参数的Initialize方法初始化，请使用GetServiceProvider()获取IServiceProvider。")
                : "";
            _initializationMode = InitializationMode.ExternalDI;
            return AddWxAutomationCore(services, options);
        }

        /// <summary>
        /// 统一初始化入口 - 使用内部依赖注入框架
        /// 当宿主应用没有依赖注入框架时使用此重载
        /// </summary>
        /// <param name="options">配置选项</param>
        /// <returns>IServiceProvider，用于获取服务实例</returns>
        public static IServiceProvider Initialize(Action<WeChatConfig> options = default) => GetServiceProvider(options);

        private static IServiceCollection AddWxAutomationCore(IServiceCollection services, Action<WeChatConfig> options)
        {
            options?.Invoke(_config);
            DpiAwareness.SetProcessDpiAwareness();

            UpdateNarrator(); //打开讲述人模式.

            // 阻止显示器关闭/屏保
            SetThreadExecutionState(
                ES_CONTINUOUS |
                ES_SYSTEM_REQUIRED |
                ES_DISPLAY_REQUIRED);

            // 远程桌面(RDP)场景适配：允许最小化远程桌面窗口而不中断UI自动化
            SuppressRemoteDesktopMinimize();

            RegisterServices(services);

            return services;
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<WeChatClientFactory>();
            services.AddSingleton<ForceOpenUITree>();
            services.AddAutoLogger();
            if (_config.EnableMouseKeyboardSimulator)
            {
                services.AddKMSimulator(_config.KMDeviceVID,
                                        _config.KMDevicePID,
                                        _config.KMVerifyUserData,
                                        _config.KMOutputStringType);
            }
            services.AddSingleton<WeChatCaptureImage>(_ =>
            {
                return new WeChatCaptureImage(_config.CaptureUIPath);
            });
            services.AddSingleton<WeChatRecordVideo>(_ =>
            {
                return new WeChatRecordVideo(_config.TargetVideoPath);
            });
            services.AddSingleton<OCRService>();
            services.AddHttpClient<QwenClientService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("User-Agent", "WeChatAuto.SDK");
                client.BaseAddress = new Uri("https://dashscope.aliyuncs.com/api/v1");
            });
        }

        /// <summary>
        /// 如果用户端没有依赖注入框架，则用此方法初始化
        /// 注意：此方法与AddWxAutomation()方法不能同时使用
        /// </summary>
        /// <param name="options">配置选项</param>
        /// <returns>IServiceProvider，用于获取服务实例</returns>
        private static IServiceProvider GetServiceProvider(Action<WeChatConfig> options = default)
        {
            currentContext = SynchronizationContext.Current;
            _ = _initializationMode == InitializationMode.ExternalDI ?
                throw new InvalidOperationException("不能同时使用AddWxAutomation和GetServiceProvider方法。如果已经使用AddWxAutomation初始化，请使用外部依赖注入容器。")
                : "";
            _initializationMode = InitializationMode.InternalDI;
            if (_internalServiceProvider != null)
            {
                return _internalServiceProvider;
            }

            _internalServices = new ServiceCollection();
            _internalServiceProvider = AddWxAutomationCore(_internalServices, options).BuildServiceProvider();
            return _internalServiceProvider;
        }
        /// <summary>
        /// 打开讲述人模式.
        /// </summary>
        private static void UpdateNarrator()
        {
            try
            {
                var status = RegistryHelper.GetDword(RegistryHive.CurrentUser, @"Software\Microsoft\Narrator\NoRoam", "RunningState") ?? 0;
                if (status == 0)
                {
                    RegistryHelper.SetDword(RegistryHive.CurrentUser, @"Software\Microsoft\Narrator\NoRoam", "RunningState", 1);
                }
            }
            catch (Exception)
            {
                Trace.WriteLine("wechatauto.sdk想通过修改注册表来打开'讲述人'模式，但失败了，请手动打开'讲述人'模式....");
            }
        }

        /// <summary>
        /// 远程桌面(RDP)最小化适配。
        /// RDP 客户端(mstsc.exe)在窗口最小化时会挂起远端显示缓冲，导致 UI 自动化
        /// 无法获取画面/投递输入。通过写入 RemoteDesktop_SuppressWhenMinimized=2 禁用该优化。
        /// 注意：该注册表需写在"运行 mstsc 的客户端机器"上（UiPath 官方方案），
        /// 而非被控的机器人/服务器机器上；此处一并写入以覆盖 SDK 同时部署在客户端机器上的场景。
        /// </summary>
        private static void SuppressRemoteDesktopMinimize()
        {
            const string subKey = @"Software\Microsoft\Terminal Server Client";
            const string valueName = "RemoteDesktop_SuppressWhenMinimized";
            const int enabled = 2;

            // mstsc 可能是 32 位或 64 位进程，两个注册表视图都要写；
            // 同时覆盖 HKCU(当前用户) 与 HKLM(本机所有用户)。
            var hives = new[] { RegistryHive.CurrentUser, RegistryHive.LocalMachine };

            foreach (var hive in hives)
            {
                foreach (var view in new[] { RegistryView.Registry32, RegistryView.Registry64 })
                {
                    try
                    {
                        using (var baseKey = RegistryKey.OpenBaseKey(hive, view))
                        using (var key = baseKey.CreateSubKey(subKey, true))
                        {
                            key?.SetValue(valueName, enabled, RegistryValueKind.DWord);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"wechatauto.sdk 想写入注册表 {hive}\\{subKey}\\{valueName} 以允许最小化远程桌面，但失败了：{ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 关闭远程桌面后保持图形界面（用于"允许客户直接关掉远程桌面"的场景）。
        /// 原理：把当前 RDP 会话通过 tscon 转移回控制台(console)会话，之后即使关闭远程桌面，
        /// 会话仍保持解锁、桌面持续渲染，UI 自动化可继续运行。
        /// 需管理员权限；调用后当前远程桌面会立即断开。
        /// 客户应在准备关闭远程桌面前调用一次（例如挂在服务端 UI 的"关闭"按钮上）。
        /// </summary>
        /// <param name="width">远程桌面会话使用的分辨率宽度（像素），转移后恢复用；可不传</param>
        /// <param name="height">远程桌面会话使用的分辨率高度（像素），转移后恢复用；可不传</param>
        public static void RedirectToConsoleSession(int? width = null, int? height = null)
        {
            try
            {
                int sessionId = Process.GetCurrentProcess().SessionId;
                uint consoleSession = WTSGetActiveConsoleSessionId();

                // 已在控制台会话中，无需转移
                if (consoleSession != 0xFFFFFFFF && sessionId == (int)consoleSession)
                {
                    Trace.WriteLine("wechatauto.sdk 当前已运行在控制台会话中，无需执行 tscon 转移。");
                    return;
                }

                var psi = new ProcessStartInfo
                {
                    FileName = System.IO.Path.Combine(Environment.SystemDirectory, "tscon.exe"),
                    Arguments = $"{sessionId} /dest:console",
                    UseShellExecute = true, // runas 动词需要 ShellExecute
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas"          // 非管理员时触发 UAC 提权
                };

                Process.Start(psi);

                // tscon 转移是异步的；当前进程会随会话一起被转移到控制台会话后继续运行。
                // 转移后分辨率可能回落（常见 1024x768），影响图像识别，这里按客户远程桌面
                // 时的分辨率恢复，保证自动化看到和之前一致的画面。
                if (width.HasValue && height.HasValue && width.Value > 0 && height.Value > 0)
                {
                    RestoreDisplayResolution(width.Value, height.Value);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"wechatauto.sdk 执行 tscon 将会话转移回控制台会话失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 等 tscon 会话转移完成后，把控制台会话分辨率恢复为远程桌面时的分辨率，
        /// 避免回落成 1024x768 之类导致图像识别/坐标定位失败。
        /// </summary>
        private static void RestoreDisplayResolution(int width, int height)
        {
            // 等待 tscon 完成会话转移（转移期间显示上下文会重建）
            Thread.Sleep(3000);

            for (int attempt = 0; attempt < 3; attempt++)
            {
                var dm = new DEVMODE
                {
                    dmDeviceName = new string(new char[32]),
                    dmFormName = new string(new char[32]),
                    dmSize = (short)Marshal.SizeOf(typeof(DEVMODE)),
                    dmPelsWidth = width,
                    dmPelsHeight = height,
                    dmFields = DM_PELSWIDTH | DM_PELSHEIGHT,
                };

                int result = ChangeDisplaySettings(ref dm, CDS_UPDATEREGISTRY);
                if (result == DISP_CHANGE_SUCCESSFUL)
                {
                    Trace.WriteLine($"wechatauto.sdk 已将控制台会话分辨率恢复为 {width}x{height}。");
                    return;
                }

                Trace.WriteLine($"wechatauto.sdk 恢复分辨率 {width}x{height} 失败（错误码 {result}），第 {attempt + 1} 次尝试。");
                Thread.Sleep(1000);
            }
        }

        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);

        [DllImport("kernel32.dll")]
        static extern uint WTSGetActiveConsoleSessionId();

        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;
        const uint ES_DISPLAY_REQUIRED = 0x00000002;

        [DllImport("user32.dll")]
        static extern int ChangeDisplaySettings(ref DEVMODE lpDevMode, uint dwFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        const int DM_PELSWIDTH = 0x00080000;
        const int DM_PELSHEIGHT = 0x00100000;
        const uint CDS_UPDATEREGISTRY = 0x00000001;
        const int DISP_CHANGE_SUCCESSFUL = 0;

    }
}
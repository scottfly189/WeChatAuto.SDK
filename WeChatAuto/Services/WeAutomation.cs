using System;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Configs;
using WeChatAuto.Components;
using WeChatAuto.Extentions;
using WeChatAuto.Utils;
using System.Runtime.InteropServices;

namespace WeChatAuto.Services
{
    public static class WeAutomation
    {
        private static IServiceCollection _internalServices = null;
        private static WeChatConfig _config = new WeChatConfig();
        public static WeChatConfig Config => _config;

        /// <summary>
        /// 如果用户端已经有依赖注入框架，则直接使用此方法注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWxAutomation(this IServiceCollection services, Action<WeChatConfig> options = default)
        {
            SetProcessDpiAwareness();
            services.AddSingleton<WeChatClientFactory>();
            services.AddAutoLogger();
            options?.Invoke(_config);
            if (_config.EnableMouseKeyboardSimulator)
            {
                services.AddKMSimulator(_config.KMDeiviceVID,
                                        _config.KMDeivicePID,
                                        _config.KMVerifyUserData,
                                        _config.OutputStringType);
            }
            services.AddSingleton<WeChatCaptureImage>(_ =>
            {
                return new WeChatCaptureImage(_config.CaptureUIPath);
            });
            services.AddSingleton<WeChatRecordVideo>(_ =>
            {
                return new WeChatRecordVideo(_config.TargetVideoPath);
            });

            return services;
        }
        /// <summary>
        /// 如果用户端没有依赖注入框架，则用此方法初始化
        /// 注意：此方法与AddWxAutomation()方法不能同时使用
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(Action<WeChatConfig> options = default)
        {
            if (_internalServices == null)
            {
                _internalServices = new ServiceCollection();
            }
            return _internalServices.AddWxAutomation(options).BuildServiceProvider();
        }
        /// <summary>
        /// 设置进程DPI感知,如果使用库的应用已经设置DPI感知，此方法无效。
        /// 此方法必须在任何窗口创建之前调用
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void SetProcessDpiAwareness()
        {
            switch (WeAutomation.Config.ProcessDpiAwareness)
            {
                case 0:
                    return;
                case 1:
                    DpiAwareness.SetProcessDPIAware();
                    break;
                case 2:
                    DpiAwareness.SetProcessDpiAwareness(2);
                    break;
                default:
                    throw new Exception("无效的DPI感知值");
            }
        }
    }
}
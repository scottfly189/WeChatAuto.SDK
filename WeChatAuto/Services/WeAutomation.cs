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
        private static IServiceProvider _internalServiceProvider = null;
        private static WeChatConfig _config = new WeChatConfig();
        public static WeChatConfig Config => _config;
        private static InitializationMode _initializationMode = InitializationMode.None;

        private enum InitializationMode
        {
            None,
            ExternalDI,      // 使用外部依赖注入框架
            InternalDI       // 使用内部依赖注入框架
        }

        /// <summary>
        /// 如果用户端已经有依赖注入框架，则直接使用此方法注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWxAutomation(this IServiceCollection services, Action<WeChatConfig> options = default)
        {
            if (_initializationMode == InitializationMode.InternalDI)
            {
                throw new InvalidOperationException("不能同时使用AddWxAutomation和GetServiceProvider方法。如果已经使用GetServiceProvider初始化，请使用GetServiceProvider返回的IServiceProvider。");
            }
            _initializationMode = InitializationMode.ExternalDI;
            return AddWxAutomationCore(services, options);
        }

        private static IServiceCollection AddWxAutomationCore(IServiceCollection services, Action<WeChatConfig> options)
        {
            options?.Invoke(_config);
            SetProcessDpiAwareness();

            RegisterServices(services);

            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<WeChatClientFactory>();
            services.AddAutoLogger();
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
        }

        /// <summary>
        /// 如果用户端没有依赖注入框架，则用此方法初始化
        /// 注意：此方法与AddWxAutomation()方法不能同时使用
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(Action<WeChatConfig> options = default)
        {
            if (_initializationMode == InitializationMode.ExternalDI)
            {
                throw new InvalidOperationException("不能同时使用AddWxAutomation和GetServiceProvider方法。如果已经使用AddWxAutomation初始化，请使用外部依赖注入容器。");
            }
            _initializationMode = InitializationMode.InternalDI;
            if (_internalServiceProvider != null)
            {
                return _internalServiceProvider;
            }

            if (_internalServices == null)
            {
                _internalServices = new ServiceCollection();
            }
            _internalServiceProvider = AddWxAutomationCore(_internalServices, options).BuildServiceProvider();
            return _internalServiceProvider;
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
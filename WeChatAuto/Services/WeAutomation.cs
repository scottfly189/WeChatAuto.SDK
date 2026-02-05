using System;
using Microsoft.Extensions.DependencyInjection;
using WeAutoCommon.Configs;
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
        /// 统一初始化入口 - 使用外部依赖注入框架
        /// 当宿主应用已有依赖注入框架时使用此重载
        /// </summary>
        /// <param name="services">依赖注入服务集合</param>
        /// <param name="options">配置选项</param>
        /// <returns>IServiceCollection，用于链式调用</returns>
        public static IServiceCollection Initialize(IServiceCollection services, Action<WeChatConfig> options = default)
        {
            if (_initializationMode == InitializationMode.InternalDI)
            {
                throw new InvalidOperationException("不能同时使用外部和内部依赖注入框架。如果已经使用无参数的Initialize方法初始化，请使用GetServiceProvider()获取IServiceProvider。");
            }
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

            RegisterServices(services);

            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<WeChatClientFactory>();
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
        }

        /// <summary>
        /// 如果用户端没有依赖注入框架，则用此方法初始化
        /// 注意：此方法与AddWxAutomation()方法不能同时使用
        /// </summary>
        /// <param name="options">配置选项</param>
        /// <returns>IServiceProvider，用于获取服务实例</returns>
        private static IServiceProvider GetServiceProvider(Action<WeChatConfig> options = default)
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

            _internalServices = new ServiceCollection();
            _internalServiceProvider = AddWxAutomationCore(_internalServices, options).BuildServiceProvider();
            return _internalServiceProvider;
        }

    }
}
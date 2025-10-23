using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Configs;
using WeChatAuto.Components;
using WeChatAuto.Extentions;

namespace WeChatAuto.Services
{
    public static class WeAutomation
    {
        private static IServiceProvider _internalProvider = null;
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
            services.AddSingleton<WeChatFramwork>();
            services.AddAutoLogger();
            options?.Invoke(_config);

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
            if (_internalProvider == null)
                _internalProvider = _internalServices.AddWxAutomation(options).BuildServiceProvider();
            return _internalProvider;
        }
    }
}
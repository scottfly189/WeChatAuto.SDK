using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Models;
using WxAutoCore.Components;
using WxAutoCore.Interface;
using WxAutoCore.Services.WxAutomationSubscription;

namespace WxAutoCore.Services
{
    public static class WxAutomation
    {
        private static IServiceProvider _internalProvider = null;

        /// <summary>
        /// 如果用户端已经有依赖注入框架，则直接注入
        /// 注意：此方法与Init方法不能同时使用
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWxAutomation(this IServiceCollection services)
        {
            //这里增加服务.
            services.AddSingleton<WxAutoSubscriptionService>();
            services.AddSingleton<WxFramwork>();
            services.AddSingleton<WxClient>();
            return services;
        }
        /// <summary>
        /// 如果用户端没有依赖注入框架，则初始化
        /// 注意：此方法与AddWxAutomation方法不能同时使用
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider()
        {
            if (_internalProvider == null)
                _internalProvider = new ServiceCollection().AddWxAutomation().BuildServiceProvider();
            return _internalProvider;
        }
        /// <summary>
        /// 如果客户端使用了依赖注入框架，则需要调用此方法初始化
        /// </summary>
        public static void Init()
        {
            _internalProvider.GetRequiredService<WxFramwork>().Init();
        }


        /// <summary>
        /// 等待seconds秒
        /// </summary>
        /// <param name="seconds"></param>
        public static void Wait(int seconds = 2)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
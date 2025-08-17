using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            services.AddSingleton<WxFramwork>();
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
        /// 等待seconds秒
        /// </summary>
        /// <param name="seconds"></param>
        public static async Task Wait(int seconds = 2)
        {
            await Task.Run(() => Thread.Sleep(seconds * 1000)).ConfigureAwait(false);
        }

    }
}
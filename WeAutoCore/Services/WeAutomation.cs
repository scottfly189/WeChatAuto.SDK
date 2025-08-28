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
    public static class WeAutomation
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
            services.AddSingleton<WeChatFramwork>();
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
        /// <param name="cancellationToken">取消令牌</param>
        public static async Task Wait(int seconds = 2, CancellationToken cancellationToken = default)
        {
            try
            {
                // 使用Task.Delay替代Thread.Sleep，避免阻塞线程
                await Task.Delay(seconds * 1000, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // 处理取消操作
                throw;
            }
            catch (Exception ex)
            {
                // 记录其他异常
                throw new InvalidOperationException($"等待操作失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 等待指定时间
        /// </summary>
        /// <param name="timeout">等待时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        public static async Task Wait(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            try
            {
                await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"等待操作失败: {ex.Message}", ex);
            }
        }
    }
}
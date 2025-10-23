using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeChatAuto.Utils;

namespace WeChatAuto.Extentions
{
    public static class AutoLoggerExtensions
    {
        /// <summary>
        /// 添加自动化日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoLogger(this IServiceCollection services)
        {
            services.AddTransient(typeof(AutoLogger<>));
            var hasLoggerFactory = services.Any(s => s.ServiceType == typeof(ILoggerFactory));
            if (!hasLoggerFactory)
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                    builder.SetMinimumLevel(LogLevel.Trace);
                });
            }

            return services;
        }
    }
}
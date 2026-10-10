using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Server.WebApi.Converters;
using Server.WebApi.Filters;
using Server.WebApi.Services;
using WeChatAuto.Components;

namespace Server.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注册 WeChat 自动化 WebAPI 所需的服务：控制器、JSON 选项、Swagger、CORS。
    /// SDK 已通过内部 DI 初始化（不可二次初始化），因此直接注册已创建好的工厂单例。
    /// </summary>
    public static IServiceCollection AddWeChatApi(this IServiceCollection services, WeChatClientFactory factory)
    {
        services.AddSingleton(factory);
        services.AddSingleton<IWeChatClientProvider, WeChatClientProvider>();
        services.AddSingleton<IAppConfigStore, AppConfigStore>();

        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new ImageJsonConverter());
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        return services;
    }
}

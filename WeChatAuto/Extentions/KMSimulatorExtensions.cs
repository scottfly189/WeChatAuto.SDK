using Microsoft.Extensions.DependencyInjection;
using WeChatAuto.Services;

namespace WeChatAuto.Extentions
{
    public static class KMSimulatorExtensions
    {
        public static IServiceCollection AddKMSimulator(this IServiceCollection services, int deviceVID, int devicePID, string verifyUserData,int outputStringType=4)
        {
            KMSimulatorService.Init(deviceVID, devicePID, verifyUserData,outputStringType);
            return services;
        }
    }
}
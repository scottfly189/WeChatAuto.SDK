using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Simulator;

namespace WeChatAuto.Extentions
{
    public static class KMSimulatorExtensions
    {
        public static IServiceCollection AddKMSimulator(this IServiceCollection services, int deviceVID, int devicePID, string verifyUserData)
        {
            KMSimulatorService.Init(deviceVID, devicePID, verifyUserData);
            return services;
        }
    }
}
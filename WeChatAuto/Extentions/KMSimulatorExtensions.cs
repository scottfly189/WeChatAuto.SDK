using Microsoft.Extensions.DependencyInjection;
using WxAutoCommon.Simulator;

namespace WeChatAuto.Extentions
{
    public static class KMSimulatorExtensions
    {
        public static IServiceCollection AddKMSimulator(this IServiceCollection services, int deviceVID, int devicePID)
        {
            KMSimulatorService.Init(deviceVID, devicePID);
            return services;
        }
    }
}
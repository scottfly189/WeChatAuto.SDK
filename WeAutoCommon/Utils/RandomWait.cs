using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeAutoCommon.Utils
{
    public static class RandomWait
    {
        /// <summary>
        /// 等待随机时间
        /// </summary>
        /// <param name="minTime">最小时间,单位:毫秒</param>
        /// <param name="maxTime">最大时间,单位:毫秒</param>
        public static void Wait(int minTime, int maxTime)
        {
            Random rng = new Random();
            Thread.Sleep(rng.Next(minTime, maxTime + 1));
        }
        /// <summary>
        /// 等待随机时间
        /// </summary>
        /// <param name="minTime">最小时间,单位:毫秒</param>
        /// <param name="maxTime">最大时间,单位:毫秒</param>
        public static async Task WaitAsync(int minTime, int maxTime)
        {
            await Task.Delay(new Random().Next(minTime, maxTime + 1));
        }
    }
}
using System;
using System.Threading;

namespace WxAutoCommon.Utils
{
    public static class RandomWait
    {
        /// <summary>
        /// 等待随机时间
        /// </summary>
        /// <param name="minTime">最小时间</param>
        /// <param name="maxTime">最大时间</param>
        public static void Wait(int minTime, int maxTime)
        {
            Random rng = new Random();
            Thread.Sleep(rng.Next(minTime, maxTime + 1));
        }
    }
}
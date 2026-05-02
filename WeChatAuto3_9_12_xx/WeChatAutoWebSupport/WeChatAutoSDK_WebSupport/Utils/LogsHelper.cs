using AntdUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace WeChatAutoSDK_WebSupport.Utils
{
    public static class LogsHelper
    {
        private static BlockingCollection<string> _LogsList = new BlockingCollection<string>();
        private static Task? task;

        private static void _LogCore(string level, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            _LogsList.Add($"{DateTime.Now.ToString("HH:mm:ss fff")} - {level}: {message}");
        }
        public static void LogInfo(string message) => _LogCore("INFO", message);

        public static void LogDebug(string message) => _LogCore("DEBUG", message);

        public static void LogWarning(string message) => _LogCore("WARN", message);

        public static void LogError(string message) => _LogCore("ERROR", message);
        public static void LogError(Exception ex) => _LogCore("ERROR", ex.ToString());

        public static void LogFatal(string message) => _LogCore("FATAL", message);
        /// <summary>
        /// 注册日志消息方法
        /// 注意：要求action方法UI线程安全
        /// </summary>
        /// <param name="action">注册的消费方法</param>
        public static void RegisterConsume(Action<string> action)
        {
            if (task != null)
                return;
            task = Task.Run(() =>
            {
                foreach (var log in _LogsList.GetConsumingEnumerable())
                {
                    action(log);
                }
            });
        }

        public static void Dispose()
        {
            _LogsList.CompleteAdding();
        }
    }
}

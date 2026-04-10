using AntdUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeChatAutoSDK_WebSupport.Utils
{
    public static class LogsHelper
    {
        private static List<string> _LogsList = new List<string>();

        private const int MAX_SIZE = 2000;

        private static readonly Object lockObject = new object();
        private static void _LogCore(string level,string message)
        {
            lock(lockObject)
            {
                if (string.IsNullOrWhiteSpace(message))
                    return;
                if (_LogsList.Count > MAX_SIZE) { _LogsList.RemoveAt(0); }
                _LogsList.Add($"{DateTime.Now.ToString("HH:mm:ss fff")} - {level}: {message}");
            }
        }
        public static void LogInfo(string message) => _LogCore("INFO",message);

        public static void LogDebug(string message) => _LogCore("DEBUG", message);

        public static void LogWarning(string message) => _LogCore("WARN",message);

        public static void LogError(string message) => _LogCore("ERROR", message);
        public static void LogError(Exception ex) => _LogCore("ERROR", ex.ToString());

        public static void LogFatal(string message) => _LogCore("FATAL", message);

        public static List<string> GetLogs() => _LogsList;

    }
}

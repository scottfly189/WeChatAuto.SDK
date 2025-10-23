using System;
using Microsoft.Extensions.Logging;

namespace WeChatAuto.Utils
{
    /// <summary>
    /// 自动化日志类
    /// </summary>
    public class AutoLogger<T>
    {
        private readonly ILogger<T> _logger;
        public AutoLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void Log(LogLevel level, string message)
        {
            _logger.Log(level, message);
        }
        public void Log(LogLevel level, string message, Exception exception)
        {
            _logger.Log(level, exception, message);
        }
        public void Trace(string message)
        {
            _logger.LogTrace(message);
        }
        public void Trace(string message, Exception exception)
        {
            _logger.LogTrace(exception, message);
        }
        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }
        public void Debug(string message, Exception exception)
        {
            _logger.LogDebug(exception, message);
        }
        public void Info(string message)
        {
            _logger.LogInformation(message);
        }
        public void Info(string message, Exception exception)
        {
            _logger.LogInformation(exception, message);
        }
        public void Warn(string message)
        {
            _logger.LogWarning(message);
        }
        public void Warn(string message, Exception exception)
        {
            _logger.LogWarning(exception, message);
        }
        public void Error(string message)
        {
            _logger.LogError(message);
        }
        public void Error(string message, Exception exception)
        {
            _logger.LogError(exception, message);
        }
        public void Fatal(string message)
        {
            _logger.LogCritical(message);
        }
        public void Fatal(string message, Exception exception)
        {
            _logger.LogCritical(exception, message);
        }
    }
}
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;

namespace Fanex.Affiliate.DataService.Job.Log
{
    public class LogWrapper : ILogWrapper
    {
        private LogLevel _logLevel;

        public LogWrapper(LogLevel logLevel = LogLevel.Error)
        {
            _logLevel = logLevel;
        }

        public static ILogWrapper Logger => new LogWrapper();

        public void Error(string message)
        {
            if (_logLevel <= LogLevel.Error)
            {
                LogHelper.Error(message);
            }
        }

        public void Error(string message, string category)
        {
            if (_logLevel <= LogLevel.Error)
            {
                LogHelper.Error(message, category);
            }
        }

        public void Error(string message, Exception exception)
        {
            if (_logLevel <= LogLevel.Error)
            {
                LogHelper.Error(message, exception);
            }
        }

        public void Error(string message, string category, Exception exception)
        {
            if (_logLevel <= LogLevel.Error)
            {
                LogHelper.Error(message, category, exception);
            }
        }

        public void Info(string message)
        {
            if (_logLevel <= LogLevel.Info)
            {
                LogHelper.Info(message);
            }
        }

        public void Info(string message, string category)
        {
            if (_logLevel <= LogLevel.Info)
            {
                LogHelper.Info(message,category);
            }
        }

        public void Info(string format, params object[] args)
        {
            if (_logLevel <= LogLevel.Info)
            {
                LogHelper.Info(format, args);
            }
        }

        public void Info(string format, string category, params object[] args)
        {
            if (_logLevel <= LogLevel.Info)
            {
                LogHelper.Info(format, category, args);
            }
        }

        public void SetLogLevel(LogLevel level)
        {
            _logLevel = level;
        }
    }
}

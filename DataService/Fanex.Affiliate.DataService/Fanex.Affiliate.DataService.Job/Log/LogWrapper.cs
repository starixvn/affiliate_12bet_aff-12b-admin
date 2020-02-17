using Fanex.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job.Log
{
    public class LogWrapper : ILogWrapper
    {
        protected ILogger _logger;
        protected LogLevel _logLevel;

        public LogWrapper(ILogger logger = null, LogLevel logLevel = LogLevel.Error)
        {
            _logger = logger ?? Fanex.Logging.Logger.Log;
            _logLevel = logLevel;
        }

        private LogLevel LogLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }

        public static ILogWrapper Logger => new LogWrapper();

        public void Error(string message)
        {
            if (LogLevel <= LogLevel.Error)
            {
                _logger.Error(message);
            }
        }

        public void Error(string message, string category)
        {
            if (LogLevel <= LogLevel.Error)
            {
                _logger.Error(message,category);
            }
        }

        public void Error(string message, Exception exception)
        {
            if (LogLevel <= LogLevel.Error)
            {
                _logger.Error(message, exception);
            }
        }

        public void Error(string message, string category, Exception exception)
        {
            if (LogLevel <= LogLevel.Error)
            {
                _logger.Error(message, category, exception);
            }
        }

        public void Info(string message)
        {
            if (LogLevel <= LogLevel.Info)
            {
                _logger.Info(message);
            }
        }

        public void Info(string message, string category)
        {
            if (LogLevel <= LogLevel.Info)
            {
                _logger.Info(message,category);
            }
        }

        public void Info(string format, params object[] args)
        {
            if (LogLevel <= LogLevel.Info)
            {
                _logger.Info(format, args);
            }
        }

        public void Info(string format, string category, params object[] args)
        {
            if (LogLevel <= LogLevel.Info)
            {
                _logger.Info(format, category, args);
            }
        }

        public void SetLogLevel(LogLevel level)
        {
            LogLevel = level;
        }
    }
}

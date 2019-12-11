using Quartz.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job
{
    public class LogProvider : ILogProvider
    {
        public Logging.ILogger Logger { get; set; } = Logging.Logger.Log;

        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (func == null)
                    return false;

                var msg = func();
                switch (level)
                {
                    case LogLevel.Debug:
                        Logger.Debug(msg, parameters);
                        break;
                    case LogLevel.Error:
                        Logger.Error(msg, exception);
                        break;
                    case LogLevel.Fatal:
                        Logger.Critical(msg, parameters);
                        Logger.Error(msg, exception);
                        break;
                    case LogLevel.Info:
                        Logger.Info(msg, parameters);
                        break;
                    case LogLevel.Warn:
                        Logger.Warn(msg, parameters);
                        break;
                    default:
                        Logger.Debug(msg, parameters);
                        break;
                }
                return true;
            };
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}

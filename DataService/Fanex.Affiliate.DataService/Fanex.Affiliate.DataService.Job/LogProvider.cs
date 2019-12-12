using Fanex.Logging;
using Quartz.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job
{
    public class LogProvider : ILogProvider
    {
        public Quartz.Logging.Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (func == null)
                    return false;

                var msg = func();
                switch (level)
                {
                    case LogLevel.Debug:
                        LogService.Debug(msg, parameters);
                        break;
                    case LogLevel.Error:
                        LogService.Error(msg, exception);
                        break;
                    case LogLevel.Fatal:
                        LogService.Critical(msg, parameters);
                        LogService.Error(msg, exception);
                        break;
                    case LogLevel.Info:
                        LogService.Info(msg, parameters);
                        break;
                    case LogLevel.Warn:
                        LogService.Warn(msg, parameters);
                        break;
                    default:
                        LogService.Debug(msg, parameters);
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

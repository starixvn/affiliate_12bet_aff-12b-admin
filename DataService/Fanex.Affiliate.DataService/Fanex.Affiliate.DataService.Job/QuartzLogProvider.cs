using Fanex.Logging;
using Quartz.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job
{
    public class QuartzLogProvider : ILogProvider
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
                    case Quartz.Logging.LogLevel.Debug:
                        LogService.Debug(msg, parameters);
                        break;
                    case Quartz.Logging.LogLevel.Error:
                        LogService.Error(msg, exception);
                        break;
                    case Quartz.Logging.LogLevel.Fatal:
                        LogService.Critical(msg, parameters);
                        LogService.Error(msg, exception);
                        break;
                    case Quartz.Logging.LogLevel.Info:
                        LogService.Info(msg, parameters);
                        break;
                    case Quartz.Logging.LogLevel.Warn:
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

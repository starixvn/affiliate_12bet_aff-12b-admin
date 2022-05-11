using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Quartz.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job
{
    public class QuartzLogProvider : ILogProvider
    {
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
                        LogHelper.Debug(msg, parameters);
                        break;
                    case LogLevel.Error:
                        LogHelper.WriteError(exception);
                        break;
                    case LogLevel.Fatal:
                        LogHelper.Critical(msg, parameters);
                        LogHelper.WriteError(exception);
                        break;
                    case LogLevel.Info:
                        LogHelper.Info(msg, parameters);
                        break;
                    case LogLevel.Warn:
                        LogHelper.Warn(msg, parameters);
                        break;
                    default:
                        LogHelper.Debug(msg, parameters);
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

using System;
using Quartz.Logging;
using Fanex.Logging;
using System.Diagnostics;

namespace Fanex.Affiliate.DataService.Job
{
    public class ConsoleLogger : ILogger
    {
        private readonly string defaultLogCategory = "default";

        public ConsoleLogger(string catogery = null)
        {
            if (!string.IsNullOrWhiteSpace(catogery))
                defaultLogCategory = catogery;
        }
        #region Debug functions
        public void Debug(string message, string category)
                => WriteLog(message, category, TraceEventType.Verbose);

        public void Debug(string message)
            => Debug(message, defaultLogCategory);

        public void Debug(string format, params object[] args)
            => Debug(string.Format(format, args));

        public void Debug(string format, string category, params object[] args)
            => Debug(string.Format(format, args), category);

        #endregion Debug functions

        #region Info functions

        public void Info(string message, string category)
            => WriteLog(message, category, TraceEventType.Information);

        public void Info(string message)
            => Info(message, defaultLogCategory);

        public void Info(string format, params object[] args)
            => Info(string.Format(format, args));

        public void Info(string format, string category, params object[] args)
            => Info(string.Format(format, args), category);

        #endregion Info functions

        #region Warning functions

        public void Warn(string message, string category)
            => WriteLog(message, category, TraceEventType.Warning);

        public void Warn(string message)
            => Warn(message, defaultLogCategory);

        public void Warn(string format, params object[] args)
            => Warn(string.Format(format, args));

        public void Warn(string format, string category, params object[] args)
            => Warn(string.Format(format, args), category);

        #endregion Warning functions

        #region Error functions

        public void Error(string message, string category)
            => WriteLog(message, category, TraceEventType.Error);

        public void Error(string message)
            => Error(message, defaultLogCategory);

        public void Error(string message, Exception exception)
            => Error(message, defaultLogCategory, exception);

        public void Error(string message, string category, Exception exception)
            => Error(string.Format("{0}\n{1}", message, exception), category);

        #endregion Error functions

        #region Critical

        public void Critical(string message, string category)
            => WriteLog(message, category, TraceEventType.Critical);

        public void Critical(string message)
            => Critical(message, defaultLogCategory);

        public void Critical(string format, params object[] args)
            => Critical(string.Format(format, args));

        public void Critical(string format, string category, params object[] args)
            => Critical(string.Format(format, args), category);
        #endregion

        private void WriteLog(string message, string category, TraceEventType eventType)
        {
            Console.WriteLine($"[" + DateTime.Now.ToLongTimeString() + "] [" + eventType + "] " + message);
        }
    }
}

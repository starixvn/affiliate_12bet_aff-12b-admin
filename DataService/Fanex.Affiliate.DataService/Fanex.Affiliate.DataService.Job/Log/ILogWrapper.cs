using System;

namespace Fanex.Affiliate.DataService.Job.Log
{
    public interface ILogWrapper
    {
        void Info(string message);
        void Info(string format, params object[] args);
        void Info(string message, string category);
        void Info(string format, string category, params object[] args);

        void Error(string message);
        void Error(string message, Exception exception);
        void Error(string message, string category);
        void Error(string message, string category, Exception exception);

        void SetLogLevel(LogLevel level);
    }
}

using System;
using Quartz.Logging;
using Fanex.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job
{
    public class ConsoleLoggerEngine : ILogEngine
    {
        private readonly string defaultLogCategory = "console log";

        public void Write(string message, TraceEventType severity, string category)
        {
            Console.WriteLine($"[" + DateTime.Now.ToLongTimeString() + "] [" + severity + "] " + message);
        }

        public Task WriteAsync(string message, TraceEventType severity, string category)
        {
            Console.WriteLine($"[" + DateTime.Now.ToLongTimeString() + "] [" + severity + "] " + message);
            return Task.CompletedTask;
        }

        public string RenderJavascriptLogger()
        {
            return "";
        }
    }
}

using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Job.Log
{
    public class LogWrapperManager
    {
        private readonly Dictionary<string, LogLevel> _logTracker = new Dictionary<string, LogLevel>();
        internal static LogWrapperManager _currentLogManager;

        public LogLevel DefaultLogLevel { get; set; } = LogLevel.Info;

        public static LogWrapperManager StartNewLogTracker()
        {
            _currentLogManager = new LogWrapperManager();
            return _currentLogManager;
        }

        public static LogWrapperManager GetCurrentLogManager()
        {
            return _currentLogManager;
        }

        public void AddLogConfig(string category, LogLevel level)
        {
            _logTracker.Add(category, level);
        }

        internal LogLevel GetCategoryConfig(string category)
        {
            try
            {
                var level = _logTracker[category];
                return level;
            }
            catch (System.Exception)
            {
                return DefaultLogLevel;
            }
        }
    }
}

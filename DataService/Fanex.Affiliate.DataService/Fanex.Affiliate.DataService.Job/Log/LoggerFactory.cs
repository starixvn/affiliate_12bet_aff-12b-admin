
namespace Fanex.Affiliate.DataService.Job.Log
{
    public static class LoggerFactory
    {
        public static ILogWrapper CreateLogger<T>() 
            where T: class
        {
            var type = typeof(T);
            var currentLogManager = LogWrapperManager.GetCurrentLogManager();
            if (currentLogManager == null)
            {
                currentLogManager = LogWrapperManager.StartNewLogTracker();
            }
            var categoryName = type.FullName;
            var logLevel = currentLogManager.GetCategoryConfig(categoryName);

            return new LogWrapper(logLevel: logLevel);
        }
        
        public static ILogWrapper CreateLogger(string name)
        {
            var currentLogManager = LogWrapperManager.GetCurrentLogManager();
            if (currentLogManager == null)
            {
                currentLogManager = LogWrapperManager.StartNewLogTracker();
            }
            var logLevel = currentLogManager.GetCategoryConfig(name);

            return new LogWrapper(logLevel: logLevel);
        } 
    }
}

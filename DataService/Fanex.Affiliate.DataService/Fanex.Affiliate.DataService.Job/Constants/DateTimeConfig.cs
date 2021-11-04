namespace Fanex.Affiliate.DataService.Job.Constants
{
    public static class DateTimeConfig
    {
        public const int ThreeDaysAgo = -3;
        public const int TwoDaysAgo = -2;
        public const string CronScheduleDailyAtOneAM = "0 0 1 * * ?"; //Run job daily at 01:00:00 AM
    }
}

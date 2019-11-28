
namespace Fanex.Affiliate.DataService.Importer.IMOne
{
    public static class Constants
    {
        public static string IMOneDateTimeFormat => "yyyy-MM-dd HH.00.00";
    }

    public enum IMoneResponseStatus
    {
        SUCCESS = 0,
        INVALID_CURRENCY = 507,
        INVALID_CODEWALLET = 508,
        INVALID_TIME_RANGE = 525,
        INVALID_START_TIME = 526,
        INVALID_DATETIME_FORMAT = 528
    }
}

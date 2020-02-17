
namespace Fanex.Affiliate.DataService.Importer.IMOne
{
    public static class Constants
    {
        public static string IMOneDateTimeFormat => "yyyy-MM-dd HH.mm.ss";
    }

    public enum IMoneResponseStatus
    {
        SUCCESS = 0,
        PLAYER_NOT_EXIST = 504,
        INVALID_PLAYER_ID = 506,
        INVALID_CURRENCY = 507,
        INVALID_CODEWALLET = 508,
        INVALID_TIME_RANGE = 525,
        INVALID_START_TIME = 526,
        BET_DETAIL_IN_PROCESS = 527,
        INVALID_DATETIME_FORMAT = 528,
        INVALID_PAGE_SIZE = 529,
        NO_DATA_FOUND = 558,
    }
}

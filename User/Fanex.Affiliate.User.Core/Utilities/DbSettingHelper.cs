namespace Fanex.Affiliate.User.Core.Utilities
{
    using Constants;

    public static class DbSettingHelper
    {
        public static string BuildSpKey(bool isInternal, string spKey)
        {
            return spKey + (isInternal ? DBMode.View : DBMode.Main);
        }
    }
}

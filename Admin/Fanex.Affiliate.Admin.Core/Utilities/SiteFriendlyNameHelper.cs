using Fanex.Affiliate.Admin.Core.Configurations;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class SiteFriendlyNameHelper
    {
        public static string Replace12Bet(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var siteFriendlyName = AppSetting.Instance.SiteFriendlyName;

            return text.Replace("12BET", siteFriendlyName);
        }
    }
}
using System.Configuration;
using System.Linq;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class Helper
    {
        private const string ASIAN_AFF_TYPE = "AsianAffType";

        public static bool IsAsianAffType(string affType)
        {
            var asianAffTypeString = ConfigurationManager.AppSettings[ASIAN_AFF_TYPE];
            var asianAffType = asianAffTypeString?.Split(',');
            return asianAffType.Any(t => t == affType);
        }
    }
}

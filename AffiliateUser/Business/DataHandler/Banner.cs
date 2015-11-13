using System.Data;
using AffiliateUser.Business.DataAccess;

namespace DataHandler
{
    public class Banner
    {
        public static DataTable GetBannerTypesOld()
        {
            var result = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET);
            return result.ResultSet.Tables.Count > 0 ? result.ResultSet.Tables[0] : null;
        }

        public static DataTable GetBannerTypes()
        {
            var result = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET);
            return result.ResultSet.Tables.Count > 0 ? result.ResultSet.Tables[0] : null;
        }
    }
}
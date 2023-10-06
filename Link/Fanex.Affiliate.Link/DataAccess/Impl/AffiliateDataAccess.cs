using Fanex.Affiliate.Link.DataAccess.Interface;
using Fanex.Data.SqlServer;
using System;

namespace Fanex.Affiliate.Link.DataAccess.Impl
{
    public class AffiliateDataAccess : IAffiliateDataAccess
    {
        public string GetCommGroupByAffId(int affId)
        {
            const string uSpKey = "Aff_AffiliateUser_ExtraUserInfoSel";

            using (var objectDb = new SqlObjectDb(uSpKey))
            {
                objectDb.SetParameterValue("AffId", affId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToString(reader["GroupName"]);
                    }

                    return string.Empty;
                }
            }
        }
    }
}
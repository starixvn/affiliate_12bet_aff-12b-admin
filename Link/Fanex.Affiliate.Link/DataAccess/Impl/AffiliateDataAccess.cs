using Fanex.Affiliate.Link.DataAccess.Interface;
using Fanex.Affiliate.Link.DTO;
using Fanex.Data.SqlServer;
using System;
using System.Collections.Generic;

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

        public IEnumerable<Language> GetSystemLanguages()
        {
            const string uSpKey = "Aff_Language_Sel";

            using (var objectDb = new SqlObjectDb(uSpKey))
            {
                return objectDb.Query<Language>();
            }
        }
    }
}
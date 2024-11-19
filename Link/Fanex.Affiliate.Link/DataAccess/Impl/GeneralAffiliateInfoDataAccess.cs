namespace Fanex.Affiliate.Link.DataAccess.Impl
{
    using Fanex.Affiliate.Link.DataAccess.Interface;
    using Fanex.Data.SqlServer;
    using System;

    /// <summary>
    /// Tracking statistics.
    /// </summary>
    public class GeneralAffiliateInfoDataAccess : IGeneralAffiliateInfoDataAccess
    {
        public (int affId, string affCode) GetGeneralAffiliateInfo(int? affId = null, string affCode = null)
        {
            const string uSpKey = "Aff_Get_GeneralAffInfo";

            using (var objectDb = new SqlObjectDb(uSpKey))
            {
                objectDb.SetParameterValue("AffId", affId);
                objectDb.SetParameterValue("AffCode", affCode);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (Convert.ToInt32(reader["AffId"]), Convert.ToString(reader["AffCode"]));
                    }

                    return (0, string.Empty);
                }
            }
        }

        public string GetCommGroupByAffCode(string affCode)
        {
            const string uSpKey = "Aff_AffiliateUser_ExtraUserInfoSel";

            using (var objectDb = new SqlObjectDb(uSpKey))
            {
                objectDb.SetParameterValue("AffCode", affCode);

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

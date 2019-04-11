using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Customer;

namespace Fanex.Affiliate.Admin.Core.Repository.Customer
{
    public class CustomerLeftPanelRepository : BaseRepository, ICustomerLeftPanelRepository
    {
        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName)
        {
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
            AffiliateInfoOnLeftPanel result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, affUserName);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AffiliateInfoOnLeftPanel
                        {
                            AffId = Convert.ToInt32(reader["AffId"]),
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                            AffName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                            AffType = Convert.ToByte(reader["AffType"]),
                            BalanceBf = Convert.ToDecimal(reader["BalBroughtForward"]),
                            CommGroupName = Convert.ToString(reader["GroupName"]),
                            ContactApps = Convert.ToString(reader["ContactApps"]),
                            Gender = Convert.ToBoolean(reader["Gender"]),
                            IsClosed = Convert.ToBoolean(reader["Closed"]),
                            LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginDate"]),
                            LastLoginIp = Convert.ToString(reader["LastLoginIp"]),
                            MoreSiteUrl = Convert.ToString(reader["MoreSiteUrls"]),
                            SiteUrl = Convert.ToString(reader["SiteUrl"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            Status = Convert.ToString(reader["Status"]),
                            Phone = Convert.ToString(reader["Phone"])
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            const string SpKey = "Aff_Admin_LeftPanelOnlineList_Sel";
            var result = new List<OnlineList>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, affUserName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new OnlineList
                        {
                            Ip = Convert.ToString(reader["Ip"]),
                            ReloadTime = Convert.ToDateTime(reader["ReloadTime"])
                        });
                    }
                }
            }

            return result;
        }
    }
}

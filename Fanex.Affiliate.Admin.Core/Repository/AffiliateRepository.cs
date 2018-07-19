using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AffiliateRepository : BaseRepository, IAffiliateRepository
    {
        public AffLeftPanelInfo GetInfo(string username)
        {
            var result = new AffLeftPanelInfo();
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader(new { username }))
                {
                    if (reader.Read())
                    {
                        result.GeneralInfo = GetGeneralInfo(reader);
                        result.OnlineInfoList = GetOnlineInfoList(reader);
                    }
                }
            }

            return result;
        }

        private static AffInfo GetGeneralInfo(IDataReader reader)
        {
            return new AffInfo
            {
                AffId = Convert.ToInt32(reader["AffId"]),
                Status = Convert.ToString(reader["Status"]),
                Closed = Convert.ToBoolean(reader["Closed"]),
                IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                LastLoginDate = Convert.ToDateTime(reader["LastLoginDate"]),
                LastLoginIP = Convert.ToString(reader["LastLoginIP"]),
                UserName = Convert.ToString(reader["UserName"]),
                UserName2 = Convert.ToString(reader["UserName2"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Gender = Convert.ToBoolean(reader["Gender"]),
                Email = Convert.ToString(reader["Email"]),
                Phone = Convert.ToString(reader["Phone"]),
                ContactApps = Convert.ToString(reader["ContactApps"]),
                Address = Convert.ToString(reader["Address"]),
                City = Convert.ToString(reader["City"]),
                State = Convert.ToString(reader["State"]),
                Postal = Convert.ToString(reader["Postal"]),
                Country = Convert.ToString(reader["Country"]),
                Question = Convert.ToString(reader["Question"]),
                Answer = Convert.ToString(reader["Answer"]),
                MailOption = Convert.ToBoolean(reader["MailOption"]),
                SiteUrl = Convert.ToString(reader["SiteURL"]),
                MoreSiteUrls = Convert.ToString(reader["MoreSiteURLs"]),
                NoSiteDesc = Convert.ToString(reader["NoSiteDesc"]),
                GroupId = Convert.ToInt32(reader["GroupId"]),
                GroupName = Convert.ToString(reader["GroupName"]),
                BalBroughtForward = Convert.ToDecimal(reader["BalBroughtForward"]),
                PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                PreferredPayment = Convert.ToString(reader["PreferredPayment"]),
                PlayerUsername = Convert.ToString(reader["PlayerUsername"]),
                NetellerAccountId = Convert.ToString(reader["NetellerAccountId"]),
                MoneybookersEmail = Convert.ToString(reader["MoneybookersEmail"]),
                WireBankName = Convert.ToString(reader["BankName"]),
                WireBankAddress = Convert.ToString(reader["BankAddress"]),
                WireSwiftCode = Convert.ToString(reader["BankSwiftCode"]),
                WireAccountName = Convert.ToString(reader["BankAccountName"]),
                WireAccountNo = Convert.ToString(reader["BankAccountNo"]),
                AffType = 2/*Convert.ToByte(reader["AffType"])*/
            };
        }

        private static IEnumerable<OnlineInfo> GetOnlineInfoList(IDataReader reader)
        {
            var result = new List<OnlineInfo>();
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    result.Add(new OnlineInfo
                    {
                        Ip = Convert.ToString(reader["Ip"]),
                        ReloadTime = Convert.ToDateTime(reader["ReloadTime"])
                    });
                }
            }

            return result;
        }
    }
}
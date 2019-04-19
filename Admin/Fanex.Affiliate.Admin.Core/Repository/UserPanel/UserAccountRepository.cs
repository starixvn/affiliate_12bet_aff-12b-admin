using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class UserAccountRepository : BaseRepository, IUserAccountRepository
    {
        public UserAccount GetUserAccountInfo(int affId)
        {
            const string SpKey = "Aff_AffiliateSelEditInfo";
            UserAccount result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, affId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new UserAccount
                        {
                            AffId = Convert.ToInt32(reader["AffId"]),
                            AffName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            Gender = Convert.ToBoolean(reader["Gender"]),
                            Status = Convert.ToString(reader["Status"]),
                            Email = Convert.ToString(reader["Email"]),
                            Phone = Convert.ToString(reader["Phone"]),
                            ContactApps = Convert.ToString(reader["ContactApps"]),
                            Address = Convert.ToString(reader["Address"]),
                            City = Convert.ToString(reader["City"]),
                            State = Convert.ToString(reader["State"]),
                            Postal = Convert.ToString(reader["Postal"]),
                            Country = Convert.ToString(reader["Country"]),
                            MoreSiteUrl = Convert.ToString(reader["MoreSiteUrls"]),
                            SiteUrl = Convert.ToString(reader["SiteUrl"]),
                            PreferredPayment = Convert.ToString(reader["PreferredPayment"]),
                            PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                            PlayerUsername = Convert.ToString(reader["Player_Username"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            NetellerAccountID = Convert.ToString(reader["Neteller_AccountID"]),
                            MoneybookersEmail = Convert.ToString(reader["Moneybookers_Email"]),
                            BankName = Convert.ToString(reader["Wire_BankName"]),
                            BankAddress = Convert.ToString(reader["Wire_BankAddress"]),
                            BankSwiftCode = Convert.ToString(reader["Wire_SwiftCode"]),
                            BankAccountName = Convert.ToString(reader["Wire_AccountName"]),
                            BankAccountNo = Convert.ToString(reader["Wire_AccountNo"]),
                            Question = Convert.ToString(reader["Question"]),
                            Answer = Convert.ToString(reader["Answer"]),
                            MailOption = Convert.ToBoolean(reader["MailOption"])
                        };
                    }
                }
            }

            return result;
        }
    }
}

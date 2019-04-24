using System;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

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
                objectDb.AddInParameter("AffId", DbType.Int32, affId);

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
                            Gender = reader["Gender"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["Gender"]),
                            Email = Convert.ToString(reader["Email"]),
                            Phone = Convert.ToString(reader["Phone"]),
                            ContactApps = Convert.ToString(reader["ContactApps"]),
                            Address = Convert.ToString(reader["Address"]),
                            City = Convert.ToString(reader["City"]),
                            State = Convert.ToString(reader["State"]),
                            Postcode = Convert.ToString(reader["Postal"]),
                            Country = Convert.ToString(reader["Country"]),
                            MoreSiteUrls = Convert.ToString(reader["MoreSiteUrls"]),
                            SiteUrl = Convert.ToString(reader["SiteUrl"]),
                            PreferredPayment = Convert.ToString(reader["PreferredPayment"]),
                            PlayerUsername = Convert.ToString(reader["Player_Username"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            NetellerAccountId = Convert.ToString(reader["Neteller_AccountID"]),
                            MoneybookersEmail = Convert.ToString(reader["Moneybookers_Email"]),
                            BankName = Convert.ToString(reader["Wire_BankName"]),
                            BankAddress = Convert.ToString(reader["Wire_BankAddress"]),
                            BankCode = Convert.ToString(reader["Wire_SwiftCode"]),
                            BankAccountName = Convert.ToString(reader["Wire_AccountName"]),
                            BankAccountNumber = Convert.ToString(reader["Wire_AccountNo"]),
                            Question = Convert.ToString(reader["Question"]),
                            Answer = Convert.ToString(reader["Answer"]),
                            MailOption = reader["MailOption"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["MailOption"])
                        };
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateUserAccount(UserAccountParam param)
        {
            const string SpKey = "Aff_AffiliateUpdInfo";
            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("FirstName", DbType.String, param.FirstName);
                objectDb.AddInParameter("LastName", DbType.String, param.LastName);
                objectDb.AddInParameter("Gender", DbType.Boolean, param.Gender);
                objectDb.AddInParameter("Email", DbType.String, param.Email);
                objectDb.AddInParameter("Phone", DbType.String, param.Phone);

                objectDb.AddInParameter("ContactApps", DbType.String, param.ContactApps);
                objectDb.AddInParameter("Address", DbType.String, param.Address);
                objectDb.AddInParameter("City", DbType.String, param.City);
                objectDb.AddInParameter("State", DbType.String, param.State);
                objectDb.AddInParameter("Postal", DbType.String, param.Postcode);
                objectDb.AddInParameter("Country", DbType.String, param.Country);

                objectDb.AddInParameter("EmailFormat", DbType.Boolean, param.MailOption);

                objectDb.AddInParameter("SiteURL", DbType.String, param.SiteUrl);
                objectDb.AddInParameter("MoreSiteURLs", DbType.String, param.MoreSiteUrls);
                objectDb.AddInParameter("NoSiteDesc", DbType.String, param.NoSiteDescription);

                objectDb.AddInParameter("PreferredPayment", DbType.String, param.PreferredPayment);
                objectDb.AddInParameter("Player_Username", DbType.String, param.PlayerUsername);
                objectDb.AddInParameter("Neteller_AccountId", DbType.String, param.NetellerAccountId);
                objectDb.AddInParameter("Moneybookers_Email", DbType.String, param.MoneybookersEmail);
                objectDb.AddInParameter("Wire_BankName", DbType.String, param.BankName);
                objectDb.AddInParameter("Wire_BankAddress", DbType.String, param.BankAddress);
                objectDb.AddInParameter("Wire_SwiftCode", DbType.String, param.BankCode);
                objectDb.AddInParameter("Wire_AccountName", DbType.String, param.BankAccountName);
                objectDb.AddInParameter("Wire_AccountNo", DbType.String, param.BankAccountNumber);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}

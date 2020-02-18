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
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
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
                            PlayerUsername = Convert.ToString(reader["PlayerUsername"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            NetellerAccountId = Convert.ToString(reader["NetellerAccountID"]),
                            MoneybookersEmail = Convert.ToString(reader["MoneybookersEmail"]),
                            BankName = Convert.ToString(reader["BankName"]),
                            BankAddress = Convert.ToString(reader["BankAddress"]),
                            BankCode = Convert.ToString(reader["BankSwiftCode"]),
                            BankAccountName = Convert.ToString(reader["BankAccountName"]),
                            BankAccountNumber = Convert.ToString(reader["BankAccountNo"]),
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
            const string SpKey = "Aff_Admin_AffiliateInfo_Upd";
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

                objectDb.AddInParameter("MailOption", DbType.Boolean, param.MailOption);

                objectDb.AddInParameter("SiteURL", DbType.String, param.SiteUrl);
                objectDb.AddInParameter("MoreSiteURLs", DbType.String, param.MoreSiteUrls);
                objectDb.AddInParameter("NoSiteDesc", DbType.String, param.NoSiteDescription);

                objectDb.AddInParameter("PreferredPayment", DbType.String, param.PreferredPayment);
                objectDb.AddInParameter("PlayerUsername", DbType.String, param.PlayerUsername);
                objectDb.AddInParameter("NetellerAccountId", DbType.String, param.NetellerAccountId);
                objectDb.AddInParameter("MoneybookersEmail", DbType.String, param.MoneybookersEmail);
                objectDb.AddInParameter("BankName", DbType.String, param.BankName);
                objectDb.AddInParameter("BankAddress", DbType.String, param.BankAddress);
                objectDb.AddInParameter("BankSwiftCode", DbType.String, param.BankCode);
                objectDb.AddInParameter("BankAccountName", DbType.String, param.BankAccountName);
                objectDb.AddInParameter("BankAccountNo", DbType.String, param.BankAccountNumber);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}

using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Common.Extensions;
using System;
using System.Collections.Generic;

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
                objectDb.SetParameterValue("AffId", affId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new UserAccount
                        {
                            AffId = Convert.ToInt32(reader["AffId"]),
                            AffName = Convert.ToString(reader["UserName"]),
                            Status = Convert.ToString(reader["Status"]),
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
                            QuestionId = reader["QuestionId"] == DBNull.Value ? (int?)null : reader.ToInt("QuestionId"),
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
                objectDb.SetParameterValue("AffId", param.AffId);
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("FirstName", param.FirstName);
                objectDb.SetParameterValue("LastName", param.LastName);
                objectDb.SetParameterValue("Gender", param.Gender);
                objectDb.SetParameterValue("Email", param.Email);
                objectDb.SetParameterValue("Phone", param.Phone);

                objectDb.SetParameterValue("ContactApps", param.ContactApps);
                objectDb.SetParameterValue("Address", param.Address);
                objectDb.SetParameterValue("City", param.City);
                objectDb.SetParameterValue("State", param.State);
                objectDb.SetParameterValue("Postal", param.Postcode);
                objectDb.SetParameterValue("Country", param.Country);

                objectDb.SetParameterValue("MailOption", param.MailOption);

                objectDb.SetParameterValue("SiteURL", param.SiteUrl);
                objectDb.SetParameterValue("MoreSiteURLs", param.MoreSiteUrls);
                objectDb.SetParameterValue("NoSiteDesc", param.NoSiteDescription);

                objectDb.SetParameterValue("PreferredPayment", param.PreferredPayment);
                objectDb.SetParameterValue("PlayerUsername", param.PlayerUsername);
                objectDb.SetParameterValue("NetellerAccountId", param.NetellerAccountId);
                objectDb.SetParameterValue("MoneybookersEmail", param.MoneybookersEmail);
                objectDb.SetParameterValue("BankName", param.BankName);
                objectDb.SetParameterValue("BankAddress", param.BankAddress);
                objectDb.SetParameterValue("BankSwiftCode", param.BankCode);
                objectDb.SetParameterValue("BankAccountName", param.BankAccountName);
                objectDb.SetParameterValue("BankAccountNo", param.BankAccountNumber);
                objectDb.SetParameterValue("IP", param.Ip);
                objectDb.SetParameterValue("QuestionId", param.QuestionId);
                objectDb.SetParameterValue("Answer", param.Answer);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
        {
            const string spKey = "Aff_User_SecurityQuestions_Sel";

            var securityQuestionList = new List<SecurityQuestion>();

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.SetParameterValue("IsIncludedOldData", isIncludeOldData);
                objectDb.SetParameterValue("Language", language);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        securityQuestionList.Add(new SecurityQuestion
                        {
                            QuestionId = Convert.ToByte(reader["QuestionID"]),
                            Question = Convert.ToString(reader["Question"])
                        });
                    }
                }
            }

            return securityQuestionList;
        }
    }
}

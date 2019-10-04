using System;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AffiliateRepository : BaseRepository, IAffiliateRepository
    {
        public AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null)
        {
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
            AffiliateBasicInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, affId);
                objectDb.AddInParameter("Username", DbType.String, affName);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildAffiliateBasicInfo(reader);
                    }
                }
            }

            return result;
        }

        public ResultInfo ApproveAffiliate(ApproveAffiliateParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Approved";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("Password", DbType.String, param.Password);
                objectDb.AddInParameter("Status", DbType.String, param.Status);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                objectDb.AddOutParameter("ErrCode", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("ErrMsg", DbType.String, Size.SizeOfString);

                objectDb.ExecuteNonQuery();

                return new ResultInfo
                {
                    Code = Convert.ToInt32(objectDb.GetParameterValue("ErrCode")),
                    Message = objectDb.GetParameterValue("ErrMsg").ToString()
                };
            }
        }

        public DepositAccountInfo GetDepositPlayerAccount(string playerUserName)
        {
            const string uSpKey = "Deposit_GetCustDataForCreateIBCACByUserName";

            using (var objectDb = GetObjectDb(uSpKey))
            {
                return objectDb.QueryEntity<DepositAccountInfo>(new { deposit_username = playerUserName });
            }
        }

        private static AffiliateBasicInfo BuildAffiliateBasicInfo (IDataReader reader)
        {
            return new AffiliateBasicInfo
            {
                AffId = Convert.ToInt32(reader["AffId"]),
                GroupId = Convert.ToInt32(reader["GroupId"]),
                UserName = Convert.ToString(reader["UserName"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                Email = Convert.ToString(reader["Email"]),
                MailOption = Convert.ToBoolean(reader["MailOption"]),
                PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                PlayerUsername = Convert.ToString(reader["PlayerUsername"]),
                AffType = Convert.ToByte(reader["AffType"])
            };
        }
    }
}
using System;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AffiliateRepository : BaseRepository, IAffiliateRepository
    {
        public AffiliateBasicInfo GetAffiliateBasicInfo(int affId)
        {
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
            AffiliateBasicInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, affId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AffiliateBasicInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            Email = Convert.ToString(reader["Email"]),
                            MailOption = Convert.ToBoolean(reader["MailOption"]),
                            PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                            PlayerUsername = Convert.ToString(reader["PlayerUsername"])
                        };
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
                objectDb.AddInParameter("Password", DbType.Int32, param.Password);
                objectDb.AddInParameter("Status", DbType.Int32, param.Status);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
                objectDb.AddOutParameter("ErrMsg", DbType.String, 2000);

                objectDb.ExecuteNonQuery();

                return new ResultInfo
                {
                    Code = Convert.ToInt32(objectDb.GetParameterValue("ErrCode")),
                    Message = objectDb.GetParameterValue("ErrMsg").ToString()
                };
            }
        }
    }
}
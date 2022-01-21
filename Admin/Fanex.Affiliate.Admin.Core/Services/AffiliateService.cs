using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AffiliateService : BaseRepository, IAffiliateService
    {
        public AffiliateBasicInfo GetAffiliateBasicInfo(int? affId, string affName = null)
        {
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
            AffiliateBasicInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("AffId", affId);
                objectDb.SetParameterValue("Username", affName);

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
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("AffId", param.AffId);
                objectDb.SetParameterValue("Password", param.Password);
                objectDb.SetParameterValue("Status", param.Status);
                objectDb.SetParameterValue("Ip", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static AffiliateBasicInfo BuildAffiliateBasicInfo(IDataReader reader)
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

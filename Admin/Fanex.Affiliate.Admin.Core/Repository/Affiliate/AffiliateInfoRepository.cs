using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public class AffiliateInfoRepository : BaseRepository, IAffiliateInfoRepository
    {
        public EntityResult<AffiliateInfo> GetAffiliateList(AffiliateInfoParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Sel";
            var result = new EntityResult<AffiliateInfo>();
            var dataList = new List<AffiliateInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignAffiliateInfoParam(objectDb, param);
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildAffiliateInfoItem(reader));
                    }
                }
                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }
            return result;
        }

        public ResultInfo ToggleFavorite(FavoriteParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Favorite_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignFavoriteParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo ChangePassword(PasswordParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Password_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignPasswordParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignAffiliateInfoParam(IObjectDb objectDb, AffiliateInfoParam param)
        {
            objectDb.SetParameterValue(Field.AffId, param.AffIdNumber);
            objectDb.SetParameterValue("UserId", param.UserId);
            objectDb.SetParameterValue("GroupID", param.GroupId);
            objectDb.SetParameterValue("TargetCountryCodes", param.TargetCountryCodes);
            objectDb.SetParameterValue("PreferredLanguages", param.PreferenceLanguages);
            objectDb.SetParameterValue("Status", param.Status);
            objectDb.SetParameterValue("ProcessingStatus", param.ProcessingStatus);
            objectDb.SetParameterValue("UserName", param.UserName);
            objectDb.SetParameterValue("Email", param.Email);
            objectDb.SetParameterValue("SortType", param.SortBy);
            objectDb.SetParameterValue("SortDirectionType", param.SortDirectionType);
            objectDb.SetParameterValue("PageIndex", param.PageIndex);
            objectDb.SetParameterValue("PageSize", param.PageSize);
            objectDb.SetParameterValue("IsExportExcel", param.IsExportExcel);

            objectDb.SetParameterValue("NRows", DefaultValue.Int);
        }

        private static AffiliateInfo BuildAffiliateInfoItem(IDataReader reader)
        {
            return new AffiliateInfo
            {
                AffId = Convert.ToInt32(reader[Field.AffId]),
                UserName = Convert.ToString(reader["Username"]),
                Status = Convert.ToString(reader["Status"]),
                FirstName = HttpUtility.HtmlDecode(Convert.ToString(reader["FirstName"])),
                LastName = HttpUtility.HtmlDecode(Convert.ToString(reader["LastName"])),
                TargetCountryCode = Convert.ToString(reader["TargetCountryCode"]),
                Language = Convert.ToString(reader["PreferredLanguage"]),
                Email = Convert.ToString(reader["Email"]),
                RegistrationDate = Convert.ToDateTime(reader["RegisteredDate"]),
                ApprovalDate = reader["ApprovedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ApprovedDate"]),
                CasinoDate = reader["CasinoJoinedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CasinoJoinedDate"]),
                CommissionGroup = Convert.ToString(reader["GroupName"]),
                BalanceBf = reader["BFF"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["BFF"]),
                Member = reader["NoOfMember"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["NoOfMember"]),
                IsFavorite = Convert.ToBoolean(reader["IsFavorite"]),
                IsClosed = Convert.ToBoolean(reader["IsClosed"]),
                Currency = Convert.ToString(reader["Currency"]),
                PlayerCurrency = Convert.ToString(reader["PlayerCurrency"]),
                Phone = HttpUtility.HtmlDecode(Convert.ToString(reader["Phone"])),
                PlayerUserName = Convert.ToString(reader["Player_Username"]),
                ProcessingStatus = Convert.ToString(reader["ProcessingStatus"])
            };
        }

        private static void AssignFavoriteParam(IObjectDb objectDb, FavoriteParam param)
        {
            objectDb.SetParameterValue("UserId", param.UserId);
            objectDb.SetParameterValue(Field.AffId, param.AffId);
            objectDb.SetParameterValue("IsFavorite", param.IsFavorite);
            objectDb.SetParameterValue("IP", param.Ip);
        }

        private static void AssignPasswordParam(IObjectDb objectDb, PasswordParam param)
        {
            objectDb.SetParameterValue("UserId", param.UserId);
            objectDb.SetParameterValue(Field.AffId, param.AffId);
            objectDb.SetParameterValue("Password", param.Password);
            objectDb.SetParameterValue("IP", param.Ip);
        }
    }
}

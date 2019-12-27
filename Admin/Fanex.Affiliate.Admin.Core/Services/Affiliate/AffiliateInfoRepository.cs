using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

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
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
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
            objectDb.AddInParameter(Field.AffId, DbType.Int32, param.AffIdNumber);
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter("GroupID", DbType.Int32, param.GroupId);
            objectDb.AddInParameter("PreferredLanguage", DbType.String, param.Language);
            objectDb.AddInParameter("Status", DbType.String, param.Status);
            objectDb.AddInParameter("UserName", DbType.String, param.UserName);
            objectDb.AddInParameter("Email", DbType.String, param.Email);
            objectDb.AddInParameter("SortType", DbType.Byte, param.SortBy);
            objectDb.AddInParameter("SortDirectionType", DbType.Boolean, param.SortDirectionType);
            objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
            objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);
            objectDb.AddInParameter("IsExportExcel", DbType.Boolean, param.IsExportExcel);

            objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
        }

        private static AffiliateInfo BuildAffiliateInfoItem(IDataReader reader)
        {
            return new AffiliateInfo
            {
                AffId = Convert.ToInt32(reader[Field.AffId]),
                UserName = Convert.ToString(reader["Username"]),
                Status = Convert.ToString(reader["Status"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
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
                Phone = Convert.ToString(reader["Phone"]),
                PlayerUserName = Convert.ToString(reader["Player_Username"])
            };
        }

        private static void AssignFavoriteParam(IObjectDb objectDb, FavoriteParam param)
        {
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter(Field.AffId, DbType.Int32, param.AffId);
            objectDb.AddInParameter("IsFavorite", DbType.Boolean, param.IsFavorite);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }

        private static void AssignPasswordParam(IObjectDb objectDb, PasswordParam param)
        {
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter(Field.AffId, DbType.Int32, param.AffId);
            objectDb.AddInParameter("Password", DbType.String, param.Password);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }
    }
}

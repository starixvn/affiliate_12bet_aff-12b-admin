using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CommissionGroupRepository : BaseRepository, ICommissionGroupRepository
    {
        public CommissionGroupResultEntity GetCommissionGroupList(CommissionGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Sel";

            var result = new CommissionGroupResultEntity();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader(param))
                {
                    result.CommGroupList = GetCommGroupList(reader);
                    result.TierSettingList = GetTierSettingList(reader);
                    result.DeductionSettingList = GetDeductionSettingList(reader);
                }

                param.Nrows = Convert.ToInt32(objectDb.GetParameterValue("Nrows"));
            }

            return result;
        }

        public ResultInfo InsertCommissionGroup(InsertCommissionGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("GroupName", DbType.String, param.GroupName);
                objectDb.AddInParameter("Description", DbType.String, param.Description);
                objectDb.AddInParameter("DeductionList", DbType.Xml, param.DeductionList);
                objectDb.AddInParameter("TierList", DbType.Xml, param.TierList);
                objectDb.AddInParameter("QMTurnover", DbType.Decimal, param.QmTurnover);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
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

        private static IEnumerable<CommGroup> GetCommGroupList(IDataReader reader)
        {
            var commGroupList = new List<CommGroup>();
            while (reader.Read())
            {
                commGroupList.Add(new CommGroup
                {
                    RowIdx = Convert.ToInt32(reader["RowIDX"]),
                    GroupId = Convert.ToInt32(reader["GroupId"]),
                    GroupName = reader["GroupName"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    Creator = reader["Creator"].ToString(),
                    Description = reader["Description"].ToString(),
                    QmTurnover = Convert.ToDecimal(reader["QMTurnover"]),
                    IsAdjustment = Convert.ToBoolean(reader["IsAdjustment"]),
                    NoAffs = Convert.ToInt32(reader["NoAffs"])
                });
            }

            return commGroupList;
        }

        private static IEnumerable<TierSetting> GetTierSettingList(IDataReader reader)
        {
            var tierSettingList = new List<TierSetting>();
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    tierSettingList.Add(new TierSetting
                    {
                        GroupId = Convert.ToInt32(reader["GroupId"]),
                        TierId = Convert.ToInt32(reader["TierId"]),
                        MinAmount = Convert.ToDecimal(reader["MinAmount"]),
                        MaxAmount = Convert.ToDecimal(reader["MaxAmount"]),
                        MinQm = Convert.ToInt32(reader["MinQm"]),
                        MaxQm = Convert.ToInt32(reader["MaxQm"]),
                        Commission = Convert.ToDecimal(reader["Commission"])
                    });
                }
            }

            return tierSettingList;
        }

        private static IEnumerable<DeductionSetting> GetDeductionSettingList(IDataReader reader)
        {
            var deductionSettingList = new List<DeductionSetting>();
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    deductionSettingList.Add(new DeductionSetting
                    {
                        GroupId = Convert.ToInt32(reader["GroupId"]),
                        DeductionId = Convert.ToInt32(reader["DeductionId"]),
                        DeductionName = reader["DeductionName"].ToString(),
                        ProductIds = reader["ProductIds"].ToString(),
                        DeductedPercentage = reader["DeductedPercentage"] == DBNull.Value ? default(decimal) : Convert.ToDecimal(reader["DeductedPercentage"]),
                        NextDeductedPercentage = reader["NextDeductedPercentage"] == DBNull.Value ? default(decimal) : Convert.ToDecimal(reader["NextDeductedPercentage"])
                    });
                }
            }

            return deductionSettingList;
        }
    }
}
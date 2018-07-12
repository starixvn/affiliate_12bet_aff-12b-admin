using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Commission;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CommissionRepository : BaseRepository, ICommissionRepository
    {
        public CommissionResultEntity GetCommissionSettingList(CommissionSettingParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Sel";

            var result = new CommissionResultEntity();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader(param))
                {
                    result.CommGroupList = GetCommGroupList(reader);
                    result.TierSettingList = GetTierSettingList(reader);
                    result.DeductionSettingList = GetDeductionSettingList(reader);
                }
            }

            return result;
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
                    IsAdjustment = Convert.ToBoolean(reader["IsAdjustment"])
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
                        DeductedPercentage = Convert.ToDecimal(reader["DeductedPercentage"]),
                        NextDeductedPercentage = Convert.ToDecimal(reader["NextDeductedPercentage"])
                    });
                }
            }

            return deductionSettingList;
        }
    }
}
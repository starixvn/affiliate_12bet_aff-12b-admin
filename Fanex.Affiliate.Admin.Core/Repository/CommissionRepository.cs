using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Commission;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CommissionRepository : BaseRepository, ICommissionRepository
    {
        public CommissionResultEntity GetCommissionSettingList(CommissionSettingParam param)
        {
            var result = new CommissionResultEntity();
            var commGroupList = new List<CommGroup>();
            const string SpKey = "Aff_Admin_CommGroup_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader(param))
                {
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
                }
            }

            result.CommGroupList = commGroupList;

            return result;
        }
    }
}

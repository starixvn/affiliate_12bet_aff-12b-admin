using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CommissionGroupRepository : BaseRepository, ICommissionGroupRepository
    {
        public CommissionGroupResultEntity GetCommissionGroupList(GetCommissionGroupParam param)
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

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCreateUpdateCommissionGroupParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue(Field.ErrCode);

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue(Field.ErrMsg).ToString()
                };
            }
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCreateUpdateCommissionGroupParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue(Field.ErrCode);

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue(Field.ErrMsg).ToString()
                };
            }
        }

        public ResultInfo DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.UserId, DbType.Int32, param.UserId);
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue(Field.ErrCode);

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue(Field.ErrMsg).ToString()
                };
            }
        }

        public IEnumerable<Deduction> GetDeductionList()
        {
            const string SpKey = "Aff_Admin_CommGroup_Deductions_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<Deduction>();
            }
        }

        public IEnumerable<SelectItem> GetNormalCommGroupList(GetGroupListParam param)
        {
            const string SpKey = "Aff_Admin_CommGroupNormal_Sel";
            var result = new List<SelectItem>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddOutParameter(Field.GroupId, DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SelectItem
                        {
                            Id = reader[Field.GroupId].ToString(),
                            Name = reader[Field.GroupName].ToString()
                        });
                    }
                }
                var groupId = objectDb.GetParameterValue(Field.GroupId);
                param.GroupId = groupId == DBNull.Value ? 0 : Convert.ToInt32(groupId);
            }

            return result;
        }

        public ResultInfo AssignGroup(AssignGroupParam param)
        {
            const string SpKey = "Aff_Admin_CommGroup_Assign";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.UserId, DbType.Int32, param.UserId);
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue(Field.ErrCode);

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue(Field.ErrMsg).ToString()
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
                    GroupId = Convert.ToInt32(reader[Field.GroupId]),
                    GroupName = reader[Field.GroupName].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    Creator = reader["Creator"].ToString(),
                    Description = reader["Description"].ToString(),
                    QmTurnover = Convert.ToDecimal(reader["QMTurnover"]),
                    NextQmTurnover = Convert.ToDecimal(reader["NextQMTurnover"]),
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
                        GroupId = Convert.ToInt32(reader[Field.GroupId]),
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
                        GroupId = Convert.ToInt32(reader[Field.GroupId]),
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

        private static void AssignCreateUpdateCommissionGroupParam(IObjectDb objectDb, CreateUpdateCommissionGroupParam param)
        {
            objectDb.AddInParameter(Field.UserId, DbType.Int32, param.UserId);
            objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);
            objectDb.AddInParameter(Field.GroupName, DbType.String, param.GroupName);
            objectDb.AddInParameter("Description", DbType.String, param.Description);
            objectDb.AddInParameter("DeductionList", DbType.Xml, param.DeductionList);
            objectDb.AddInParameter("TierList", DbType.Xml, param.TierList);
            objectDb.AddInParameter("QMTurnover", DbType.Decimal, param.QmTurnover);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
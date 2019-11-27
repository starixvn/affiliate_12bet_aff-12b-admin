using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using System.Data;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class DepartmentService : BaseRepository, IDepartmentService
    {
        public IEnumerable<DepartmentInfo> GetDepartmentList()
        {
            const string SpKey = "Aff_Con_GroupInfoSel";

            var result = new List<DepartmentInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildDepartmentInfoItem(reader));
                    }
                }
            }

            return result;
        }

        public DepartmentInfo GetDepartmentDetail(int selectedDepartmentId)
        {
            const string SpKey = "Aff_Con_GroupInfoSel";

            DepartmentInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, selectedDepartmentId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildDepartmentInfoItem(reader);
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertDepartment(InsertUpdateDepartmentParam param)
        {
            const string SpKey = "Aff_Con_GroupInfoIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter(Field.GroupId, DbType.Int32, Size.SizeOfInt);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDepartment(InsertUpdateDepartmentParam param)
        {
            const string SpKey = "Aff_Con_GroupInfoUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.DepartmentId);

                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDepartment(DeleteDepartmentParam param)
        {
            const string SpKey = "Aff_Con_GroupInfoDel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.DepartmentId);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<PolicyInfo> GetRolePolicy(int departmentId)
        {
            const string SpKey = "Aff_Con_GroupPolicySel";

            var result = new List<PolicyInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, departmentId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PolicyInfo
                        {
                            ParentId = reader.GetVal<int>("ParentId"),
                            Policy = Convert.ToInt32(reader["Policy"]),
                            FunctionId = reader.GetVal<int>("FuncId"),
                            FunctionDescription = Convert.ToString(reader["FuncDesc"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateRolePolicy(UpdatePolicyParam param)
        {
            const string SpKey = "Aff_Con_GroupPolicyUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("grouppolicys", DbType.String, param.GroupPolicies);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static DepartmentInfo BuildDepartmentInfoItem(IDataReader reader)
        {
            return new DepartmentInfo
            {
                DepartmentId = Convert.ToInt32(reader["groupid"]),
                DepartmentName = Convert.ToString(reader["groupname"]),
                Description = Convert.ToString(reader["groupdesc"]),
                NoOfUser = reader.GetVal<int>("nouser")
            };
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateDepartmentParam param)
        {
            objectDb.AddInParameter("GroupName", DbType.String, param.DepartmentName);
            objectDb.AddInParameter("GroupDesc", DbType.String, param.Description);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}

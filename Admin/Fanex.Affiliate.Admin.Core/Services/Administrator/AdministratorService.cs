using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class AdministratorService : BaseRepository, IAdministratorService
    {
        public IEnumerable<AdministratorInfo> GetUserList(int groupId, string userName, string firstLetter)
        {
            const string SpKey = "Aff_Con_AdminSelByGroupID";

            var result = new List<AdministratorInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                if (groupId != 0)
                {
                    objectDb.AddInParameter(Field.GroupId, DbType.Int32, groupId);
                }
                objectDb.AddInParameter(Field.UserName, DbType.String, "%" + userName + "%");
                objectDb.AddInParameter("firstchar", DbType.String, firstLetter == "0-9" ? "[" + firstLetter + "]%" : firstLetter + "%");

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dataItem = BuildAdministratorInfo(reader);
                        dataItem.Creator = Convert.ToString(reader["Creator"]);
                        dataItem.IsOpen = !reader.GetVal<bool>("closed");
                        dataItem.GroupName = Convert.ToString(reader["GroupName"]);
                        dataItem.CreatedDate = reader["CreateDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreateDate"]);

                        result.Add(dataItem);
                    }
                }
            }

            return result;
        }

        public AdministratorInfo GetUserDetail(int userId)
        {
            const string SpKey = "Aff_Con_AdminSelByCustID";

            AdministratorInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.CustId, DbType.Int32, userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildAdministratorInfo(reader);
                    }
                }
            }

            return result;
        }

        public IEnumerable<PolicyInfo> GetAccountPolicyList(int groupId)
        {
            const string SpKey = "Aff_Con_AdminPolicySelByCustID";

            var result = new List<PolicyInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.CustId, DbType.Int32, groupId);
                objectDb.AddOutParameter(Field.GroupId, DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildPolicyInfo(reader));
                    }
                }
            }

            return result;
        }

        public IEnumerable<PolicyInfo> GetPolicyListByGroupIds(string groupIds)
        {
            const string SpKey = "Aff_Con_Batch_AdminPolicySel";

            var result = new List<PolicyInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("custids", DbType.String, groupIds);
                objectDb.AddOutParameter(Field.GroupId, DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildPolicyInfo(reader));
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertUser(InsertUserParam param)
        {
            const string SpKey = "Aff_Con_AdminIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter(Field.CustId, DbType.Int32, Size.SizeOfInt);
                objectDb.AddInParameter(Field.UserName, DbType.String, "AA" + param.UserName);
                objectDb.AddInParameter("userpwd", DbType.String, param.Password);
                objectDb.AddInParameter(Field.FirstName, DbType.String, param.FirstName);
                objectDb.AddInParameter(Field.LastName, DbType.String, param.LastName);
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);

                AssignUserInfoParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo InsertExistedUser(InsertExistedUserParam param)
        {
            const string SpKey = "Aff_Con_AdminInsFromExisted";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.UserName, DbType.String, param.UserName);
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);
                objectDb.AddInParameter("creator", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateUser(UpdateUserParam param)
        {
            const string SpKey = "Aff_Con_AdminUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.CustId, DbType.Int32, param.AccountId);
                objectDb.AddInParameter(Field.FirstName, DbType.String, param.FirstName);
                objectDb.AddInParameter(Field.LastName, DbType.String, param.LastName);
                objectDb.AddInParameter(Field.GroupId, DbType.Int32, param.GroupId);

                AssignUserInfoParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo ChangeStatus(ChangeStatusParam param)
        {
            const string SpKey = "Aff_Con_AdminUpdClosed";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.CustId, DbType.Int32, param.SelectedUserId);
                objectDb.AddInParameter("closed", DbType.Boolean, !param.IsOpen);

                AssignUserInfoParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateAccountPolicy(UpdatePolicyParam param)
        {
            const string SpKey = "Aff_Con_AdminPolicyUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("mempolicys", DbType.String, param.GroupPolicies);

                AssignUserInfoParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo ChangePassword(UpdatePasswordParam param)
        {
            const string SpKey = "Aff_Admin_ChangePassword";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.CustId, DbType.Int32, param.AccountId);
                objectDb.AddInParameter("NewPwd", DbType.String, param.Password);

                AssignUserInfoParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private AdministratorInfo BuildAdministratorInfo(IDataReader reader)
        {
            return new AdministratorInfo
            {
                UserId = Convert.ToInt32(reader[Field.CustId]),
                UserName = Convert.ToString(reader[Field.UserName]),
                FirstName = Convert.ToString(reader[Field.FirstName]),
                LastName = Convert.ToString(reader[Field.LastName]),
                GroupId = reader.GetVal<int>(Field.GroupId)
            };
        }

        private PolicyInfo BuildPolicyInfo(IDataReader reader)
        {
            return new PolicyInfo
            {
                ParentId = reader.GetVal<int>("ParentId"),
                Policy = Convert.ToInt32(reader["Policy"]),
                FunctionId = Convert.ToInt32(reader["FuncId"]),
                FunctionDescription = Convert.ToString(reader["FuncDesc"]),
                GroupPolicy = reader.GetVal<byte>("GPolicy")
            };
        }

        private static void AssignUserInfoParam(IObjectDb objectDb, UserInfoParam param)
        {
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }
    }
}

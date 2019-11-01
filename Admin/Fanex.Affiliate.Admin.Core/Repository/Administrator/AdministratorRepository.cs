using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.Administrator
{
    public class AdministratorRepository : BaseRepository, IAdministratorRepository
    {
        public IEnumerable<AdministratorInfo> GetUserList(int groupId, string userName, string firstLetter)
        {
            const string SpKey = "Aff_Con_AdminSelByGroupID";

            var result = new List<AdministratorInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                if(groupId != 0)
                {
                    objectDb.AddInParameter("GroupID", DbType.Int32, groupId);
                }
                objectDb.AddInParameter("UserName", DbType.String, "%" + userName + "%");
                objectDb.AddInParameter("firstchar", DbType.String, firstLetter + "%");

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new AdministratorInfo
                        {
                            UserId = Convert.ToInt32(reader["custid"]),
                            UserName = Convert.ToString(reader["username"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            Creator = Convert.ToString(reader["Creator"]),
                            IsOpen = !Convert.ToBoolean(reader["closed"]),
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                            GroupName = Convert.ToString(reader["GroupName"]),
                            CreatedDate = Convert.ToDateTime(reader["CreateDate"])
                        });
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
                objectDb.AddInParameter("custid", DbType.Int32, userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new AdministratorInfo
                        {
                            UserId = Convert.ToInt32(reader["custid"]),
                            UserName = Convert.ToString(reader["username"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<PolicyInfo> GetPolicyList(int groupId)
        {
            const string SpKey = "Aff_Con_GroupPolicySel";

            var result = new List<PolicyInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("GroupID", DbType.Int32, groupId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PolicyInfo
                        {
                            ParentId = Convert.ToInt32(reader["ParentId"]),
                            Policy = Convert.ToInt32(reader["Policy"]),
                            FunctionId = Convert.ToInt32(reader["FunctionId"]),
                            FunctionDescription = Convert.ToInt32(reader["FunctionDescription"]),
                            GroupName = Convert.ToString(reader["GroupName"]),
                            SubFunctionCount = Convert.ToInt32(reader["SubFunctionCount"])
                        });
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
                objectDb.AddOutParameter("Custid", DbType.Int32, Size.SizeOfInt);
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("userpwd", DbType.String, param.Password);
                objectDb.AddInParameter("firstname", DbType.String, param.FirstName);
                objectDb.AddInParameter("lastname", DbType.String, param.LastName);
                objectDb.AddInParameter("groupid", DbType.Int32, param.GroupId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo InsertUserExist(InsertUserExistParam param)
        {
            const string SpKey = "Aff_Con_AdminInsFromExisted";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("groupid", DbType.Int32, param.GroupId);
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
                objectDb.AddInParameter("custid", DbType.Int32, param.SelectedUserId);
                objectDb.AddInParameter("firstname", DbType.String, param.FirstName);
                objectDb.AddInParameter("lastname", DbType.String, param.LastName);
                objectDb.AddInParameter("groupid", DbType.Int32, param.GroupId);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

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
                objectDb.AddInParameter("custid", DbType.Int32, param.SelectedUserId);
                objectDb.AddInParameter("closed", DbType.Boolean, !param.IsOpen);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}

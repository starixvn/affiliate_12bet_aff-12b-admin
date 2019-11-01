using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Administrator
{
    public class AdministratorRepository : BaseRepository, IAdministratorRepository
    {
        public IEnumerable<AdministratorInfo> GetAdminList(int groupId, string userName, string firstLetter)
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
    }
}

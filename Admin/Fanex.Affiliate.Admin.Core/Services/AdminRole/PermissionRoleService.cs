using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class PermissionRoleService : BaseRepository, IPermissionRoleService
    {
        public IEnumerable<PermissionRoleInfo> GetPermissionRoleList(string userName, bool isGeneral = true)
        {
            string SpKey = isGeneral ? "" : string.Empty;
            var result = new List<PermissionRoleInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, userName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildItem(reader));
                    }
                }
            }

            return result;
        }

        private PermissionRoleInfo BuildItem(IDataReader reader)
        {
            return new PermissionRoleInfo
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                IsView = Convert.ToBoolean(reader["IsView"]),
                IsEdit = Convert.ToBoolean(reader["IsEdit"])
            };
        }
    }
}

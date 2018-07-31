using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public IList<UserPermission> GetPermissionList(int userId)
        {
            var spKey = "Aff_Admin_PolicySel";
            var result = new List<UserPermission>();

            using (var objectDb = GetObjectDb(spKey))
            {
                using (var reader = objectDb.ExecuteReader(new { userId }))
                {
                    while (reader.Read())
                    {
                        result.Add(new UserPermission
                        {
                            FunctionName = reader["FuncName"].ToString(),
                            IsHost = Convert.ToBoolean(reader["Host"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
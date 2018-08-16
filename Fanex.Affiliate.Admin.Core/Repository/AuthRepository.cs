using System;
using System.Collections.Generic;
using System.Data;
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

        public bool ReCheckin(int onUserId)
        {
            var spKey = "Aff_Recheckin";

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.AddInParameter("on_userid", DbType.Int32, onUserId);
                objectDb.AddOutParameter("iskickedout", DbType.Int32, 4);
                return Convert.ToBoolean(objectDb.GetParameterValue("iskickedout"));
                
            }
        }
    }
}
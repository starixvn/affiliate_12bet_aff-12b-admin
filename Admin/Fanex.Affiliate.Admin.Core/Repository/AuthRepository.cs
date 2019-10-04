using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public IList<UserPermission> GetPermissionList(int userId)
        {
            const string SpKey = "Aff_Admin_PolicySel";
            var result = new List<UserPermission>();

            using (var objectDb = GetObjectDb(SpKey))
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
            const string SpKey = "Aff_Recheckin";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("on_userid", DbType.Int32, onUserId);
                objectDb.AddOutParameter("iskickedout", DbType.Int32, Size.SizeOfInt);
                objectDb.ExecuteNonQuery();
                var isKickedOut = objectDb.GetParameterValue("iskickedout");

                return (isKickedOut != DBNull.Value) && Convert.ToBoolean(isKickedOut);
            }
        }
    }
}
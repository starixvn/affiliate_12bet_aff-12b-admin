using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class PermissionRoleService : BaseRepository, IPermissionRoleService
    {
        public IEnumerable<PermissionRoleInfo> GetPermissionRoleList(int userId, bool isGeneral = true)
        {
            string SpKey = isGeneral ? "Aff_Admin_AdminUser_FunctionPermission_Sel" : "Aff_Admin_AdminUser_Market_Sel";
            var result = new List<PermissionRoleInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.Int32, userId);

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
                FuncId = reader.ToInt("FuncId"),
                FuncName = reader.ToString("FuncName")
            };
        }
    }
}

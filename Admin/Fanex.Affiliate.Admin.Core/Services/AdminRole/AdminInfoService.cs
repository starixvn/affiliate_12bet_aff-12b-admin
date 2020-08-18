using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Common.Extensions;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminInfoService : BaseRepository, IAdminInfoService
    {
        private const int aaLenght = 2;

        public ResultInfo CreateUser(AdminInfoParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(param, objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public AdminInfo GetUserInfo(int userId)
        {
            const string SpKey = "Aff_Admin_AdminUser_Detail_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.Int32, userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AdminInfo
                        {
                            UserId = reader.ToInt("CustId"),
                            UserName = reader.ToString("UserName").Substring(aaLenght),
                            FirstName = reader.ToString("FirstName"),
                            LastName = reader.ToString("LastName"),
                            RoleId = reader.ToInt("RoleId"),
                            IsActive = !reader.ToBool("Closed")
                        };
                    }
                }
            }

            return new AdminInfo();
        }

        public ResultInfo UpdateUser(AdminInfoParam param)
        {
            const string SpKey = "Aff_Admin_AdminUser_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.Int32, param.UserId);
                AssignParam(param, objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<RoleInfo> GetRoleList()
        {
            const string SpKey = "Aff_Admin_AdminUser_Role_Sel";
            var result = new List<RoleInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        result.Add(new RoleInfo
                        {
                            RoleId = reader.ToInt("RoleId"),
                            RoleName = reader.ToString("RoleName")
                        });
                    }
                }
            }

            return result;
        }

        private static void AssignParam(AdminInfoParam param, IObjectDb objectDb)
        {
            objectDb.AddInParameter("UserName", DbType.String, "AA" + param.UserName);
            objectDb.AddInParameter("UserPwd", DbType.String, param.Password);
            objectDb.AddInParameter("RoleId", DbType.Int32, param.Role);
            objectDb.AddInParameter("Closed", DbType.Boolean, !param.IsActive);
            objectDb.AddInParameter(Field.UserId, DbType.Int32, param.ByUserId);
            //objectDb.AddInParameter("Ip", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}

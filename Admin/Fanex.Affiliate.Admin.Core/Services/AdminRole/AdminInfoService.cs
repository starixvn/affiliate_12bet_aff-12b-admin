using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Data;
using System;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminInfoService : BaseRepository, IAdminInfoService
    {
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

        public AdminInfo GetUserInfo(string userName)
        {
            const string SpKey = "Aff_Admin_AdminUser_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("userName", DbType.String, userName);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AdminInfo
                        {
                            UserId = Convert.ToInt32(reader["CustId"]),
                            UserName = Convert.ToString(reader["UserName"]),
                            Password = Convert.ToString(reader["Password"]),
                            RoleId = Convert.ToString(reader["RoleId"]),
                            IsActive = !Convert.ToBoolean(reader["Closed"])
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
                objectDb.AddInParameter(Field.UserId, DbType.Int32, param.UserId);
                AssignParam(param, objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(AdminInfoParam param, IObjectDb objectDb)
        {
            objectDb.AddInParameter("UserName", DbType.String, param.UserName);
            objectDb.AddInParameter("Password", DbType.String, param.Password);
            objectDb.AddInParameter("Role", DbType.Int32, param.Role);
            objectDb.AddInParameter("MarketIds", DbType.String, param.MarketIds);
            objectDb.AddInParameter("IsActive", DbType.Boolean, param.IsActive);
            objectDb.AddInParameter(Field.UserId, DbType.Int32, param.ByUserId);
            objectDb.AddInParameter("Ip", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}

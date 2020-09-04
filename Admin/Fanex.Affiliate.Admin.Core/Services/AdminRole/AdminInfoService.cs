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

        public AdminInforResultEntity GetUserInfo(int userId)
        {
            const string SpKey = "Aff_Admin_AdminUser_Detail_Sel";

            var result = new AdminInforResultEntity();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.Int32, userId);

                using (var reader = objectDb.ExecuteReader())
                {
                    result.AdminInfo = GetAdminInfo(reader);
                    result.MarketList = GetMarketList(reader);
                    result.UserPermissionList = GetUserPermissionList(reader);
                }
            }

            return result;
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

        private static void AssignParam(AdminInfoParam param, IObjectDb objectDb)
        {
            objectDb.AddInParameter("UserName", DbType.String, "AA" + param.UserName);
            objectDb.AddInParameter("UserPwd", DbType.String, param.Password);
            objectDb.AddInParameter("RoleId", DbType.Int32, param.Role);
            objectDb.AddInParameter("Closed", DbType.Boolean, !param.IsActive);
            objectDb.AddInParameter(Field.UserId, DbType.Int32, param.ByUserId);
            objectDb.AddInParameter("Ip", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }


        private static AdminInfo GetAdminInfo(IDataReader reader)
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

            return new AdminInfo();
        }

        private static IEnumerable<MarketInfo> GetMarketList(IDataReader reader)
        {
            var marketList = new List<MarketInfo>();

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    marketList.Add(new MarketInfo
                    {
                        MarketId = reader.ToString("MarketId"),
                        MarketName = reader.ToString("DisplayName"),
                        HasPermission = reader.ToBool("Status")
                    });
                }
            }

            return marketList;
        }

        private static IEnumerable<UserPermissionInfo> GetUserPermissionList(IDataReader reader)
        {
            var userPermissionList = new List<UserPermissionInfo>();

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    userPermissionList.Add(new UserPermissionInfo
                    {
                        FuncId = reader.ToInt("FuncId"),
                        FuncName = reader.ToString("FuncName"),
                        UserPolicy = reader.ToInt("UserPolicy"),
                        RolePolicy = reader.ToInt("RolePolicy")
                    });
                }
            }

            return userPermissionList;
        }
    }
}

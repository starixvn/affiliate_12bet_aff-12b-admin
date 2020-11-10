using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Common.Extensions;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.AdminRole
{
    public class AdminInfoRepository : BaseRepository, IAdminInfoRepository
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

        public IEnumerable<UserPermissionInfo> GetPermissionListByRole(int roleId)
        {
            const string SpKey = "Aff_Admin_AdminUser_RolePermission_Sel";

            var result = new List<UserPermissionInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("RoleId", DbType.Int32, roleId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = BuildUserPermissionInfo(reader);
                        item.RolePolicy = reader.ToInt("Policy");

                        result.Add(item);
                    }
                }
            }

            return result;
        }

        private static void AssignParam(AdminInfoParam param, IObjectDb objectDb)
        {
            objectDb.AddInParameter("UserName", DbType.String, string.IsNullOrEmpty(param.UserName) ? null : "AA" + param.UserName);
            objectDb.AddInParameter("UserPwd", DbType.String, param.Password);
            objectDb.AddInParameter("FirstName", DbType.String, param.FirstName);
            objectDb.AddInParameter("LastName", DbType.String, param.LastName);
            objectDb.AddInParameter("RoleId", DbType.Int32, param.Role);
            objectDb.AddInParameter("Closed", DbType.Boolean, param.IsClosed);
            objectDb.AddInParameter("IsRoleMarketDefault", DbType.Boolean, param.IsRoleMarketDefault);
            objectDb.AddInParameter("IsRoleFunctionDefault", DbType.Boolean, param.IsRoleFunctionDefault);
            objectDb.AddInParameter(Field.UserId, DbType.Int32, param.ByUserId);
            objectDb.AddInParameter("Ip", DbType.String, param.Ip);
            objectDb.AddInParameter("Function", DbType.Xml, param.PermissionInfoXml);
            objectDb.AddInParameter("Market", DbType.Xml, param.MarketListXml);

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
                    IsActive = !Convert.ToBoolean(reader["Closed"]),
                    IsRoleMarketDefault = reader.ToBool("IsRoleMarketDefault"),
                    IsRoleFunctionDefault = reader.ToBool("IsRoleFunctionDefault")
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
                        MarketName = reader.ToString("MarketName"),
                        HasPermission = Convert.ToBoolean(reader["Status"])
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
                    var item = BuildUserPermissionInfo(reader);
                    item.Policy = reader.ToInt("UserPolicy");
                    item.RolePolicy = reader.ToInt("RolePolicy");

                    userPermissionList.Add(item);
                }
            }

            return userPermissionList;
        }

        private static UserPermissionInfo BuildUserPermissionInfo(IDataReader reader)
        {
            return new UserPermissionInfo
            {
                ParentId = reader.ToInt("ParentId"),
                AllParentIds = reader.ToString("AllParentIds"),
                FuncId = reader.ToInt("FuncId"),
                FuncName = reader.ToString("FuncName")
            };
        }
    }
}

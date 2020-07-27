using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminUsersService : BaseRepository, IAdminUsersService
    {
        public ResultInfo DeleteUsers(DeleteAdminUsersParam param)
        {
            const string SpKey = "";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserIds", DbType.String, param.UserIds);
                objectDb.AddInParameter("UserId", DbType.Int32, param.ByUserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public EntityResult<AdminUserInfo> GetUserList(AdminUsersParam param)
        {
            const string SpKey = "";
            var result = new EntityResult<AdminUserInfo>();
            var dataList = new List<AdminUserInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("Nrows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildItem(reader));
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private AdminUserInfo BuildItem(IDataReader reader)
        {
            return new AdminUserInfo
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                UserName = Convert.ToString(reader["UserName"]),
                LoggedIn = reader["LoggedIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LoggedIn"]),
                LastRequest = reader["LastRequest"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastRequest"]),
                Ip = Convert.ToString(reader["Ip"]),
                Role = Convert.ToInt32(reader["Role"])
            };
        }
    }
}

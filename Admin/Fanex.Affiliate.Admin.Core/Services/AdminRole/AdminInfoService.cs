using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Common.Extensions;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminInfoService: IAdminInfoService
    {
        private const int aaLenght = 2;

        private readonly IAdminInfoRepository _adminInfoRepository;
        public AdminInfoService(
            IAdminInfoRepository adminInfoRepository)
        {
            _adminInfoRepository = adminInfoRepository;
        }

        public ResultInfo CreateUser(AdminInfoParam param)
        {
            param.PermissionInfoXml = BuildPermissionInfoXml(param.PermissionInfo);

            return _adminInfoRepository.CreateUser(param);
        }

        public IEnumerable<UserPermissionInfo> GetPermissionListByRole(int roleId)
        {
            return _adminInfoRepository.GetPermissionListByRole(roleId);
        }

        public AdminInforResultEntity GetUserInfo(int userId)
        {
            return _adminInfoRepository.GetUserInfo(userId);
        }

        public ResultInfo UpdateUser(AdminInfoParam param)
        {
            throw new NotImplementedException();
        }

        private static string BuildPermissionInfoXml(IEnumerable<RolePermissionParam> permissionInfo)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in permissionInfo)
            {
                var row = $"<r FuncId=\"{item.FuncId}\" Policy=\"{item.Policy}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}

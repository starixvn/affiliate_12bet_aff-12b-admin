using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using System.Collections.Generic;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public RolePermissionService(
            IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public IEnumerable<RolePermissionInfo> GetPermissionInfoList()
        {
            return _rolePermissionRepository.GetPermissionInfoList();
        }

        public ResultInfo UpdatePermissionInfo(UpdateRolePermissionParam param)
        {
            param.PermissionInfoXml = BuildPermissionInfoXml(param.PermissionInfo, param.RoleId);

            return _rolePermissionRepository.UpdatePermissionInfo(param);
        }

        public ResultInfo UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            param.MarketInfoXml = BuildMarketInfoXml(param.MarketInfoList);

            return _rolePermissionRepository.UpdateMarketInfoList(param);
        }

        private static string BuildPermissionInfoXml(IEnumerable<RolePermissionParam> permissionInfo, int roleId)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in permissionInfo)
            {
                var row = $"<r RoleId=\"{roleId}\" FuncId=\"{item.FuncId}\" Policy=\"{item.Policy}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildMarketInfoXml(IEnumerable<MarketInfoParam> marketInfoList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in marketInfoList)
            {
                var row = $"<r RoleId=\"{item.RoleId}\" MarketId=\"{item.MarketId}\" Status=\"{item.HasPermission}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}

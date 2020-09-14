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
        private readonly IRolePermissionRepository _generalPermissionRoleRepository;
        public RolePermissionService(
            IRolePermissionRepository generalPermissionRoleRepository)
        {
            _generalPermissionRoleRepository = generalPermissionRoleRepository;
        }
        public IEnumerable<MarketInfo> GetMarketByRoleList()
        {
            return _generalPermissionRoleRepository.GetMarketByRoleList();
        }

        public IEnumerable<RolePermissionInfo> GetPermissionInfoList()
        {
            return _generalPermissionRoleRepository.GetPermissionInfoList();
        }

        public ResultInfo UpdatePermissionInfo(UpdatePermissionInfoParam param)
        {
            param.PermissionInfoXml = BuildPermissionInfoXml(param.PermissionInfo);

            return _generalPermissionRoleRepository.UpdatePermissionInfo(param);
        }

        public ResultInfo UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            param.MarketInfoXml = BuildMarketInfoXml(param.MarketInfoList);

            return _generalPermissionRoleRepository.UpdateMarketInfoList(param);
        }

        private static string BuildPermissionInfoXml(IEnumerable<PermissionInfoParam> permissionInfo)
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

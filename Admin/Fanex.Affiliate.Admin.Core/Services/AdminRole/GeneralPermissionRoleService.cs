using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class GeneralPermissionRoleService : IGeneralPermissionRoleService
    {
        private readonly IGeneralPermissionRoleRepository _generalPermissionRoleRepository;
        public GeneralPermissionRoleService(
            IGeneralPermissionRoleRepository generalPermissionRoleRepository)
        {
            _generalPermissionRoleRepository = generalPermissionRoleRepository;
        }
        public IEnumerable<MarketInfo> GetMarketByRoleList()
        {
            return _generalPermissionRoleRepository.GetMarketByRoleList();
        }

        public IEnumerable<PermissionInfo> GetPermissionInfoList()
        {
            return _generalPermissionRoleRepository.GetPermissionInfoList();
        }

        public ResultInfo UpdatePermissiontInfoList(UpdateMarketInfoParam param)
        {
            param.MarketInfoXml = BuildMarketInfoXml(param.MarketInfoList);

            return _generalPermissionRoleRepository.UpdatePermissiontInfoList(param);
        }

        private static string BuildMarketInfoXml(IEnumerable<MarketInfo> marketInfoList)
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

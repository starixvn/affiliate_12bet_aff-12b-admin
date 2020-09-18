using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using System.Collections.Generic;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public class AdminInfoService: IAdminInfoService
    {

        private readonly IAdminInfoRepository _adminInfoRepository;
        public AdminInfoService(
            IAdminInfoRepository adminInfoRepository)
        {
            _adminInfoRepository = adminInfoRepository;
        }

        public ResultInfo CreateUser(AdminInfoParam param)
        {
            param.PermissionInfoXml = BuildPermissionInfoXml(param.PermissionInfo);
            param.MarketListXml = BuildMarketXml(param.MarketList);

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
            param.PermissionInfoXml = BuildPermissionInfoXml(param.PermissionInfo);
            param.MarketListXml = BuildMarketXml(param.MarketList);

            return _adminInfoRepository.UpdateUser(param);
        }

        public ResultInfo ChangePassword(AdminInfoParam param)
        {
            return _adminInfoRepository.UpdateUser(param);
        }

        public ResultInfo UpdateStatus(AdminInfoParam param)
        {
            return _adminInfoRepository.UpdateUser(param);
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

        private static string BuildMarketXml(IEnumerable<UserMarketParam> marketList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in marketList)
            {
                var row = $"<r MarketId=\"{item.MarketId}\" Status=\"{item.HasPermission}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}

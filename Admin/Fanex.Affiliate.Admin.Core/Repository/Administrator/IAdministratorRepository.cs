
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Administrator
{
    public interface IAdministratorRepository
    {
        IEnumerable<PolicyInfo> GetPolicyList(int groupId);

        IEnumerable<AdministratorInfo> GetAdminList(int groupId, string userName, string firstLetter);
    }
}

using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.Repository.Administrator;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        public AdministratorService(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public IEnumerable<AdministratorInfo> GetAdminList(int groupId, string userName, string firstLetter)
        {
            if (firstLetter == "0-9")
            {
                return _administratorRepository.GetAdminList(groupId, userName, "[" + firstLetter + "]");
            }

            return _administratorRepository.GetAdminList(groupId, userName, firstLetter);
        }

        public IEnumerable<PolicyInfo> GetPolicyList(int groupId)
        {
            return _administratorRepository.GetPolicyList(groupId);
        }
    }
}

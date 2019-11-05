using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
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

        public IEnumerable<AdministratorInfo> GetUserList(int groupId, string userName, string firstLetter)
        {
            if (firstLetter == "0-9")
            {
                return _administratorRepository.GetUserList(groupId, userName, "[" + firstLetter + "]");
            }

            return _administratorRepository.GetUserList(groupId, userName, firstLetter);
        }

        public AdministratorInfo GetUserDetail(int userId)
        {
            return _administratorRepository.GetUserDetail(userId);
        }

        public IEnumerable<PolicyInfo> GetAccountPolicyList(int groupId)
        {
            return _administratorRepository.GetAccountPolicyList(groupId);
        }

        public IEnumerable<PolicyInfo> GetPolicyListByGroupIds(string groupIds)
        {
            return _administratorRepository.GetPolicyListByGroupIds(groupIds);
        }

        public ResultInfo InsertUser(InsertUserParam param)
        {
            return _administratorRepository.InsertUser(param);
        }

        public ResultInfo InsertExistedUser(InsertExistedUserParam param)
        {
            return _administratorRepository.InsertExistedUser(param);
        }

        public ResultInfo UpdateUser(UpdateUserParam param)
        {
            return _administratorRepository.UpdateUser(param);
        }

        public ResultInfo ChangeStatus(ChangeStatusParam param)
        {
            return _administratorRepository.ChangeStatus(param);
        }

        public ResultInfo UpdateAccountPolicy(UpdatePolicyParam param)
        {
            return _administratorRepository.UpdateAccountPolicy(param);
        }

        public ResultInfo ChangePassword(UpdatePasswordParam param)
        {
            return _administratorRepository.ChangePassword(param);
        }
    }
}

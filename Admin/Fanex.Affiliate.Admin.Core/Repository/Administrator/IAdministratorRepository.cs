
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Administrator
{
    public interface IAdministratorRepository
    {
        IEnumerable<PolicyInfo> GetPolicyList(int groupId);

        IEnumerable<AdministratorInfo> GetUserList(int groupId, string userName, string firstLetter);

        AdministratorInfo GetUserDetail(int userId);

        ResultInfo InsertUser(InsertUserParam param);

        ResultInfo InsertUserExist(InsertUserExistParam param);

        ResultInfo UpdateUser(UpdateUserParam param);

        ResultInfo ChangeStatus(ChangeStatusParam param);
    }
}

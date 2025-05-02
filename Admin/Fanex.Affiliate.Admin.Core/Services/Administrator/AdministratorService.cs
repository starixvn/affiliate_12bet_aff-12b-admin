using Fanex.Affiliate.Admin.Core.Commands.Administrator;
using Fanex.Affiliate.Admin.Core.Criterias.Administrator;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class AdministratorService : BaseDynamicService, IAdministratorService
    {
        public AdministratorService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<AdministratorInfo> GetUserList(int groupId, string userName, string firstLetter)
        {
            var criteria = new AdministratorCriteria
            {
                GroupId = groupId != 0 ? groupId : default(int?),
                UserName = $"%{userName}%",
                FirstChar = firstLetter == "0-9" ? $"[{firstLetter}]%" : $"{firstLetter}%",
            };

            return FetchTData<AdministratorInfo>(criteria);
        }

        public AdministratorInfo GetUserDetail(int userId)
        {
            var criteria = new AdministratorDetailCriteria
            {
                UserId = userId,
            };

            return GetTData<AdministratorInfo>(criteria);
        }

        public IEnumerable<PolicyInfo> GetAccountPolicyList(int accountId)
        {
            var criteria = new AccountPolicyCriteria
            {
                CustId = accountId,
            };

            return FetchTData<PolicyInfo>(criteria).Where(p => !p.FuncDesc.Contains("Transfer Balance For Player")); //Hide Transfer Player
        }

        public IEnumerable<PolicyInfo> GetPolicyListByAccountIds(string accountIds)
        {
            var criteria = new PoliciesByGroupIdsCriteria
            {
                CustIds = accountIds,
            };

            return FetchTData<PolicyInfo>(criteria).Where(p => !p.FuncDesc.Contains("Transfer Balance For Player")); //Hide Transfer Player
        }

        public ResultInfo InsertUser(InsertUserParam param)
        {
            var command = new InsertAccountCommand
            {
                UserName = $"AA{param.UserName}",
                UserPwd = param.Password,
                FirstName = param.FirstName,
                LastName = param.LastName,
                GroupId = param.GroupId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo InsertExistedUser(InsertExistedUserParam param)
        {
            var command = new InsertExistedAccountCommand
            {
                UserName = param.UserName,
                GroupId = param.GroupId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateUser(UpdateUserParam param)
        {
            var command = new UpdateAccountCommand
            {
                CustId = param.AccountId,
                FirstName = param.FirstName,
                GroupId = param.GroupId,
                LastName = param.LastName,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo ChangeStatus(ChangeStatusParam param)
        {
            var command = new ChangeAccountStatusCommand
            {
                CustId = param.SelectedUserId,
                Closed = !param.IsOpen,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateAccountPolicy(UpdatePolicyParam param)
        {
            var command = new UpdateAccountPolicyCommand
            {
                MemPolicys = param.GroupPolicies,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo ChangePassword(UpdatePasswordParam param)
        {
            var command = new ChangeAccountPasswordCommand
            {
                CustId = param.AccountId,
                NewPwd = param.Password,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }
    }
}

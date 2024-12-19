using Fanex.Affiliate.Admin.Core.Commands.Administrator;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.Administrator;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Administrator
{
    public class DepartmentService : BaseDynamicService, IDepartmentService
    {
        public DepartmentService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<DepartmentInfo> GetDepartmentList()
        {
            const string SpKey = "Aff_Con_GroupInfoSel";

            return FetchTData<DepartmentInfo>(new BaseCriteria(SpKey));
        }

        public DepartmentInfo GetDepartmentDetail(int selectedDepartmentId)
        {
            var criteria = new DepartmentDetailCriteria { GroupId = selectedDepartmentId };

            return GetTData<DepartmentInfo>(criteria);
        }

        public ResultInfo InsertDepartment(InsertUpdateDepartmentParam param)
        {
            const string SpKey = "Aff_Con_GroupInfoIns";

            var command = GetDepartmentCommand(SpKey, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateDepartment(InsertUpdateDepartmentParam param)
        {
            const string SpKey = "Aff_Con_GroupInfoUpd";

            var command = GetDepartmentCommand(SpKey, param);
            command.GroupId = param.DepartmentId;

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteDepartment(DeleteDepartmentParam param)
        {
            var command = new DeleteDepartmentCommand
            {
                GroupId = param.DepartmentId,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public IEnumerable<PolicyInfo> GetRolePolicy(int departmentId)
        {
            var criteria = new RolePolicyCriteria
            {
                GroupId = departmentId,
            };

            return FetchTData<PolicyInfo>(criteria).Where(p => !p.FuncDesc.Contains("Transfer Balance For Player")); //Hide Transfer Player
        }

        public ResultInfo UpdateRolePolicy(UpdatePolicyParam param)
        {
            var command = new UpdateRolePolicyCommand
            {
                GroupPolicys = param.GroupPolicies,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        private static DepartmentCommand GetDepartmentCommand(string spKey, InsertUpdateDepartmentParam param)
        {
            return new DepartmentCommand(spKey)
            {
                GroupName = param.DepartmentName,
                GroupDesc = param.Description,
                UserId = param.UserId,
                Ip = param.Ip,
            };
        }
    }
}

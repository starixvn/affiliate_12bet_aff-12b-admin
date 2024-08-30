using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public interface ICommissionGroupRepository
    {
        CommissionGroupResultEntity GetCommissionGroupList(GetCommissionGroupParam param);

        ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param);

        ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param);

        ResultInfo DeleteCommissionGroup(DeleteCommissionGroupParam param);

        IEnumerable<Deduction> GetDeductionList();

        IEnumerable<SelectItem> GetNormalCommGroupList(GetGroupListParam param);

        ResultInfo AssignGroup(AssignGroupParam param);
    }
}

using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public interface ICommGroupAssignmentService
    {
        IEnumerable<SelectItem> GetNormalCommGroupList(GetGroupListParam param);

        ResultInfo AssignGroup(AssignGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam);

        ResultInfo AssignPrivateGroup(CreateUpdateCommissionGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam);
    }
}
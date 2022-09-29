using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public interface ICommGroupAssignmentService
    {
        IEnumerable<SelectItem> GetNormalCommGroupList(GetGroupListParam param);

        ResultInfo AssignGroup(AssignGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam);

        ResultInfo AssignPrivateGroup(CreateUpdateCommissionGroupParam param, AffiliateBasicInfo affInfo, AffInformationEmail emailParam);
    }
}
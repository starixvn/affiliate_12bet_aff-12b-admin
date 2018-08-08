using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface ICommissionGroupService
    {
        IEnumerable<CommissionGroupSetting> GetCommissionSettingList(CommissionGroupParam param);

        ResultInfo InsertCommissionGroup(InsertCommissionGroupParam param);
    }
}

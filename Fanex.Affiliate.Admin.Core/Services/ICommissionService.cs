using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface ICommissionService
    {
        IEnumerable<CommissionGroupSetting> GetCommissionSettingList(CommissionGroupParam param);
    }
}

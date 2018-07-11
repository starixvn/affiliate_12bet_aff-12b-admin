using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Commission;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface ICommissionService
    {
        IEnumerable<CommissionSetting> GetCommissionSettingList(CommissionSettingParam param);
    }
}

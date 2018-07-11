using Fanex.Affiliate.Admin.Core.DTO.Commission;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface ICommissionRepository
    {
        CommissionResultEntity GetCommissionSettingList(CommissionSettingParam param);
    }
}

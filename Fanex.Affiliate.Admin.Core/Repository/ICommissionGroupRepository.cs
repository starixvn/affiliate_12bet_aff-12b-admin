using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface ICommissionGroupRepository
    {
        CommissionGroupResultEntity GetCommissionGroupList(CommissionGroupParam param);
    }
}

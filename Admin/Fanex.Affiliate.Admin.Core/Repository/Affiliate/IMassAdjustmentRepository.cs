using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public interface IMassAdjustmentRepository
    {
        AdjustmentResultInfo UpdateMassAdjusment(MassAdjustmentParam param);
    }
}

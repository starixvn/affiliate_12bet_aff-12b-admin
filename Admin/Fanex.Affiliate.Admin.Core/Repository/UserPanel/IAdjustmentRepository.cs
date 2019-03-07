using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IAdjustmentRepository
    {
        decimal GetBalanceBroughtForward(int affId);

        ResultInfo InsertAdjustment(AdjustmentParam param);
    }
}

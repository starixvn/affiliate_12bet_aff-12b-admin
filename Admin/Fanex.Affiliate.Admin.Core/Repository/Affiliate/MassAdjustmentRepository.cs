using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public class MassAdjustmentRepository : IMassAdjustmentRepository
    {
        public AdjustmentResultInfo InsertMassAdjusment()
        {
            return new AdjustmentResultInfo
            {
                Result = new ResultInfo()
            };
        }
    }
}

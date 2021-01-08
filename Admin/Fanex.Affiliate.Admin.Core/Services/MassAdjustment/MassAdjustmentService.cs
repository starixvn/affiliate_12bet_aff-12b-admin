using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Services.MassAdjustment
{
    public class MassAdjustmentService : IMassAdjustmentService
    {
        private readonly IMassAdjustmentRepository _massAdjustmentRepository;

        public MassAdjustmentService(IMassAdjustmentRepository massAdjustmentRepository)
        {
            _massAdjustmentRepository = massAdjustmentRepository;
        }

        public ResultInfo InsertMassAdjusment()
        {
            return _massAdjustmentRepository.InsertMassAdjusment();
        }
    }
}

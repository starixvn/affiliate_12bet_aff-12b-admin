using Fanex.Affiliate.Admin.Core.Commands.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class AdjustmentService : IAdjustmentService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public AdjustmentService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public decimal GetBalanceBroughtForward(int affId)
        {
            var command = new BalanceBroughtForwardCommand
            {
                AffId = affId
            };

            _dynamicRepository.Execute(command);

            return command.BalBroughtForward;
        }

        public ResultInfo InsertAdjustment(AdjustmentParam param)
        {
            var command = new InsertAdjustmentCommand
            {
                AffId = param.AffId,
                Amount = param.Amount,
                Description = param.Description,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            _dynamicRepository.Execute(command);

            return new ResultInfo(command.ErrCode, command.ErrMsg);
        }
    }
}

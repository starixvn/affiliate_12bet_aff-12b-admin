using Fanex.Affiliate.Admin.Core.Commands.UserPanel.Adjustment;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class AdjustmentService : BaseDynamicService, IAdjustmentService
    {
        public AdjustmentService(IDynamicRepository dynamicRepository)
            : base(dynamicRepository)
        {
        }

        public decimal GetBalanceBroughtForward(int affId)
        {
            var command = new BalanceBroughtForwardCommand
            {
                AffId = affId
            };

            DynamicRepository.Execute(command);

            return command.BalBroughtForward;
        }

        public ResultInfo InsertAdjustment(AdjustmentParam param)
        {
            var command = new InsertAdjustmentCommand
            {
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                Amount = param.Amount,
                Description = param.Description,
            };

            return ExecuteReturnResultInfo(command);
        }
    }
}

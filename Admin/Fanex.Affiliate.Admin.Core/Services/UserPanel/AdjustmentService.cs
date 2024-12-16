using Fanex.Affiliate.Admin.Core.Commands.UserPanel;
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
                Amount = param.Amount,
                Description = param.Description,
            };

            return ExecuteReturnResultInfo(command, param);
        }
    }
}

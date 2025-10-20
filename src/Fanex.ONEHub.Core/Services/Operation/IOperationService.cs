namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;

    public interface IOperationService
    {
        ResultInfo AddDeal(DealSettingParam param);

        ResultInfo DeleteDeal(DeleteDealParam param);

        ResultInfo UpdateDeal(DealSettingParam param);
    }
}

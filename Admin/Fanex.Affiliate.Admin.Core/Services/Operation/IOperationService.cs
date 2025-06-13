namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;

    public interface IOperationService
    {
        IEnumerable<DealType> GetDealTypes();

        EntityResult<DealInfo> GetDeals(DealParam param);

        ResultInfo AddCpaDeal(DealSettingParam param);
    }
}

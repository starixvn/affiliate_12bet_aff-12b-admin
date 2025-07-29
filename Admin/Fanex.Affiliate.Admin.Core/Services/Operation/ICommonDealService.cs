namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;

    public interface ICommonDealService
    {
        IEnumerable<DealType> GetDealTypes();

        IEnumerable<PaymentFrequency> GetPaymentFrequency();

        EntityResult<DealInfo> GetDeals(DealParam param);

        DealSettingDetail GetDealSettingDetail(int groupId);

        IEnumerable<DeductionItem> GetDeductionList();

        IEnumerable<NormalDeal> GetNormalDealList(GetNormalDealParam param);
    }
}

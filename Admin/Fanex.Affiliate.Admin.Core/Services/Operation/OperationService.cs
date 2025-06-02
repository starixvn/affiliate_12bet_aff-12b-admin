namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using Fanex.Affiliate.Admin.Core.Criterias.Operation;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Data.Repository;

    public class OperationService : BaseDynamicService, IOperationService
    {
        public OperationService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<DealType> GetDealTypes()
        {
            return FetchTData<DealType>("Aff_Admin_CommGroup_GroupTypeSel");
        }

        public EntityResult<DealInfo> GetDeals(DealParam param)
        {
            var criteria = new DealCriteria
            {
                GroupId = param.GroupId,
                GroupName = param.GroupName,
                GroupTypeId = param.GroupTypeId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize
            };

            return FetchEntityResult<DealInfo>(criteria);
        }
    }
}

using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class PageManagementService : BaseDynamicService, IPageManagementService
    {
        public PageManagementService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public EntityResult<PageManagement> GetPageManagementList(PageManagementParam param)
        {
            var criteria = param.MapTo<PageManagementParam, GetPageManagementCriteria>();
            return FetchEntityResult<PageManagement>(criteria);
        }
    }
}

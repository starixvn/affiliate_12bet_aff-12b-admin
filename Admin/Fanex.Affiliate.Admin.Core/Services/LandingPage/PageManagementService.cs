using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
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

        public IEnumerable<PageManagement> GetPageManagementList(PageManagementParam param)
        {
            return new List<PageManagement>
            {
                new PageManagement
                {
                    PageId = 1,
                    PageName = "MYR Join Landing Page",
                    PageCode = "myr-12join",
                    TargetCountry = "Malaysia",
                    Language = "English",
                    Deal = "NoCommission,NoCommission_MKT",
                    Status = "Published",
                },
                new PageManagement
                {
                    PageId = 2,
                    PageName = "MYR Join Landing Page 2",
                    PageCode = "myr-12join2",
                    TargetCountry = "Malaysia",
                    Language = "English",
                    Deal = "NoCommission,NoCommission_MKT",
                    Status = "Archived",
                }
            };

            //var criteria = param.MapTo<PageManagementParam, GetPageManagementCriteria>();
            //return FetchTData<PageManagement>(criteria);
        }
    }
}

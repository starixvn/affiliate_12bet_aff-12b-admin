using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    public class PageBuilderViewModel : PageManagementViewModel
    {
        public IEnumerable<ModuleInfo> ModuleList { get; set; }

        public PageBuilderDetail PageBuilderDetail { get; set; }

        public string BannerRootUrl { get; set; }
    }
}

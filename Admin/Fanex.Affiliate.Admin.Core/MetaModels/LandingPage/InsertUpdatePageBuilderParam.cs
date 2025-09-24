using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class InsertUpdatePageBuilderParam : UserInfoParam
    {
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public int CampaignId { get; set; }
        public IList<ModuleComponentItem> ModuleComponentsSetting { get; set; } = new List<ModuleComponentItem>();
        public string TargetCountryCode { get; set; }
        public string LanguageCode { get; set; }
        public string DealIds { get; set; }
        public int StatusId { get; set; }
        public bool IsPreview { get; set; }
        public int PageId { get; set; }
    }
}

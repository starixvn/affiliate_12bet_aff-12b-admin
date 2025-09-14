using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class InsertUpdatePageBuilderParam : UserInfoParam
    {
        [Required]
        public string PageName { get; set; }
        [Required]
        public string PageCode { get; set; }
        [Required]
        public int CampaignId { get; set; }

        public IList<ModuleComponentItem> ModuleComponentsSetting { get; set; }

        [Required]
        public string TargetCountryCode { get; set; }
        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string DealIds { get; set; }
        [Required]
        public int StatusId { get; set; }
        public bool IsPreview { get; set; }
        public int PageId { get; set; }
    }
}

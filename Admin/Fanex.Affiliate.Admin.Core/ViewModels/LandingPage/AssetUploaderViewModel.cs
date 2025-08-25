using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    public class AssetUploaderViewModel
    {
        public IEnumerable<LandingPageModule> ModuleList { get; set; }
        public IEnumerable<Language> LanguageList { get; set; }
        public IEnumerable<TargetCountry> TargetCountryList { get; set; }
        public int MaximumFiles { get; set; }
        public long MaxFileSize { get; set; }
        public string[] AllowedExtensions { get; set; }
        public string BannerRootUrl { get; set; }
    }
}

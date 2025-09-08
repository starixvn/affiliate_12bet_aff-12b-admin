namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class AssetListParam : BaseLandingPageParam
    {
        public string ModuleIds { get; set; }
        public string TargetCountrycodes { get; set; } = "all";
        public string LanguageCodes { get; set; } = "all";
        public int Status { get; set; }
        public int MediaId { get; set; }
    }
}

namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetLandingPageListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_LandingPage_Sel";

        public GetLandingPageListCriteria(bool isInternal, string targetCountryCode, bool isMobile, int affId)
        {
            IsInternal = isInternal;
            TargetCountryCode = targetCountryCode;
            IsMobile = isMobile;
            AffId = affId;
        }

        public bool IsMobile { get; set; }

        public int AffId { get; set; }

        public string TargetCountryCode { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

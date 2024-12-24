namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetLandingPageListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_Admin_LandingPage_Sel";

        public GetLandingPageListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

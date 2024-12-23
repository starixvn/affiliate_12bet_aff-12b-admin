namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;
    using Utilities;

    public class GetLandingPageListCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_Admin_LandingPage_Sel";

        public GetLandingPageListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        [SpParam(Ignored = true)]
        private bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetAffDomainListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_Domain_Sel";

        public GetAffDomainListCriteria(int affId, string targetCountryCode, bool isInternal)
        {
            AffId = affId;
            TargetCountryCode = targetCountryCode;
            IsInternal = isInternal;
        }

        public int AffId { get; set; }

        public string TargetCountryCode { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}

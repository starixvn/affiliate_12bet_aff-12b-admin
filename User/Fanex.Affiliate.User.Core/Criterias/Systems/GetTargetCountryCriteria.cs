namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetTargetCountryCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_TargetCountry_Sel";

        public GetTargetCountryCriteria(bool isInternal, string targetCountryCode = "", bool isDefault = true)
        {
            IsInternal = isInternal;
            TargetCountryCode = targetCountryCode;
            IsDefault = isDefault;
        }

        public string TargetCountryCode { get; set; }

        public bool IsDefault { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetTargetLanguageByCountryCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_Language_Sel";

        public GetTargetLanguageByCountryCriteria(bool isInternal, string targetCountryCode = null)
        {
            IsInternal = isInternal;
            TargetCountryCode = targetCountryCode;
        }

        public string TargetCountryCode { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
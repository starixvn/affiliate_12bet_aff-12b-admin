namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetTargetCountryCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_TargetCountry_Sel";

        public GetTargetCountryCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
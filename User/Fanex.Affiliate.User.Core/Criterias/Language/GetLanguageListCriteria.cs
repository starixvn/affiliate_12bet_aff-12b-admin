namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetLanguageListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_Market_Sel";

        public GetLanguageListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

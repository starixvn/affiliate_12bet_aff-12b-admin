namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetCurrencyListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_Admin_MediaCurrency_Sel";

        public GetCurrencyListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Fanex.Data.Repository;
    using Utilities;

    public class GetCurrencyListCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_Admin_MediaCurrency_Sel";

        public GetCurrencyListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        [SpParam(Ignored = true)]
        private bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}

namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetBaseCurrencyListCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Aff_BaseCurrency_Sel";

        public override bool IsValid() => true;
    }
}

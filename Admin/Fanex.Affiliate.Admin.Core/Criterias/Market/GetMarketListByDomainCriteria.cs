namespace Fanex.Affiliate.Admin.Core.Criterias.Market
{
    using Data.Repository;
    using Fanex.Data;

    public class GetMarketListByDomainCriteria : CriteriaBase
    {
        public int AffDomainId { get; set; }

        [SpParam(SqlParamName = "LicenseeDomainId")]
        public int RedirectDomainId { get; set; }

        public override string GetSettingKey() => "Aff_SupportBlockMarket_Sel";

        public override bool IsValid() => true;
    }
}

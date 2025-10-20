namespace Fanex.Affiliate.Admin.Core.Criterias.Domain
{
    using Data.Repository;
    using Fanex.Data;

    public class GetTargetCountryListByDomainCriteria : CriteriaBase
    {
        public int AffDomainId { get; set; }

        [SpParam(SqlParamName = "LicenseeDomainId")]
        public int RedirectDomainId { get; set; }

        public override string GetSettingKey() => "Aff_Domain_BlockTargetCountry_Sel";

        public override bool IsValid() => true;
    }
}

namespace Fanex.Affiliate.Admin.Core.Criterias.Domain
{
    public class SpecificDomainControlCriteria : PagingCriteria
    {
        const string SP_NAME = "Aff_Admin_SpecificDomainControl_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffUsername { get; set; }

        public string AffDomain { get; set; }

        public string RedirectDomain { get; set; }
    }
}

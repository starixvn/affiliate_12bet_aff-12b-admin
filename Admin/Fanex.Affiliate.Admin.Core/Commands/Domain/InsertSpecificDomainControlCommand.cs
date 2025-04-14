namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class InsertSpecificDomainControlCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_SpecificDomainControl_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffUsername { get; set; }

        public string AffDomain { get; set; }

        public bool AffDomainIsSSL { get; set; }

        public string RedirectDomain { get; set; }

        public bool RedirectDomainIsSSL { get; set; }

        public bool Status { get; set; }
    }
}

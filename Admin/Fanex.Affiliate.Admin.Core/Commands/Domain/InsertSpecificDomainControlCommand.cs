namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class InsertSpecificDomainControlCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_SpecificDomainControl_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffName { get; set; }

        public string AffDomain { get; set; }

        public bool AffDomainSSL { get; set; }

        public string RedirectDomain { get; set; }

        public bool RedirectDomainSSL { get; set; }

        public bool IsUsed { get; set; }
    }
}

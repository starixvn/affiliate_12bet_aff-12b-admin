namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public abstract class BaseDomainControlCommand : BaseNonQueryCommand
    {
        public int AffDomainID { get; set; }

        public int LicenseeDomainId { get; set; }

        public string TargetCountryCode { get; set; }

        public bool Status { get; set; }
    }
}

namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class UpdateDomainControlCommand : BaseDomainControlCommand
    {
        private const string SP_NAME = "Aff_Admin_DomainControl_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int DomainControlId { get; set; }
    }
}

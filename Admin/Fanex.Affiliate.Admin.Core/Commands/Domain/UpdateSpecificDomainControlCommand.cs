namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class UpdateSpecificDomainControlCommand : InsertSpecificDomainControlCommand
    {
        private const string SP_NAME = "Aff_Admin_SpecificDomainControl_Upd";

        public override string GetSettingKey() => SP_NAME;

        public int SpecificDomainControlId { get; set; }
    }
}

namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class UpdateSpecificDomainControlCommand : InsertSpecificDomainControlCommand
    {
        private const string SP_NAME = "Aff_Update_SpecificDomainControl_Ins";

        public override string GetSettingKey() => SP_NAME;

        public int DomainControlId { get; set; }
    }
}

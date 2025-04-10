namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class InsertDomainControlCommand : BaseDomainControlCommand
    {
        private const string SP_NAME = "Aff_Admin_DomainControl_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;
    }
}

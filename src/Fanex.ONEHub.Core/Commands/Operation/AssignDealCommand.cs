namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    public class AssignDealCommand : BaseAffNonQueryCommand
    {
        public int GroupId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Assign_Deal";

        public override bool IsValid() => GroupId > 0 && AffId > 0;
    }
}

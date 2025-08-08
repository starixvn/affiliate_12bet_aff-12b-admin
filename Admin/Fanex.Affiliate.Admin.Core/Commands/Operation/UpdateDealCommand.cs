namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    public class UpdateDealCommand : BaseDealCommand
    {
        public override string GetSettingKey() => "Aff_Admin_Deal_Upd";

        public override bool IsValid() => GroupId > 0;
    }
}

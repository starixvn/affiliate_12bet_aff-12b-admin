namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    public class InsertDealCommand : BaseDealCommand
    {
        public override string GetSettingKey() => "Aff_Admin_Deal_Ins";

        public override bool IsValid() => true;
    }
}

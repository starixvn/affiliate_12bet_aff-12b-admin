namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    public class InsertDealCommand : BaseDealCommand
    {
        public int GroupTypeId { get; set; }

        public bool IsInstant { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Deal_Ins";

        public override bool IsValid() => true;
    }
}

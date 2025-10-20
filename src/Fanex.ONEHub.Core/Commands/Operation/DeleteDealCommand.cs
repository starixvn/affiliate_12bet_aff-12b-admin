namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    public class DeleteDealCommand : BaseNonQueryCommand
    {
        public int GroupId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Deal_Del";

        public override bool IsValid() => GroupId > 0;
    }
}

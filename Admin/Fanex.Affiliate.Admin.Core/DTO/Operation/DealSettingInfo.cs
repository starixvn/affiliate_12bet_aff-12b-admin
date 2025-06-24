namespace Fanex.Affiliate.Admin.Core.DTO.Operation
{
    public class DealSettingInfo
    {
        public int GroupId { get; set; }

        public string DealName { get; set; }

        public int DealType { get; set; }

        public string Description { get; set; }

        public decimal SubCommRate { get; set; }

        public decimal QmTurnover { get; set; }

        public string CountryCodes { get; set; }

        public short ActiveDays { get; set; }

        public short HoldingDays { get; set; }

        public short HoldingTickets { get; set; }

        public short Quota { get; set; }

        public bool IsInstant { get; set; }

        public bool IsActivePlayerSum { get; set; }
    }
}

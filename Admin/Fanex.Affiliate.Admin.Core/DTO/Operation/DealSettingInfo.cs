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

        public int ActiveDays { get; set; }

        public int HoldingDays { get; set; }

        public int HoldingTickets { get; set; }

        public int Quota { get; set; }

        public bool IsInstant { get; set; }
    }
}

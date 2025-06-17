namespace Fanex.Affiliate.Admin.Core.Commands.Operation
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Affiliate.Admin.Core.Constants;

    public class InsertCpaDealCommand : BaseNonQueryCommand
    {
        public int GroupTypeId { get; set; }

        public int GroupId { get; set; }

        [Required(ErrorMessage = "Deal Name is required!")]
        public string GroupName { get; set; }

        [Range(Validation.Zero, Validation.MaxPercentage, ErrorMessage = "Sub-Affiliate Rate is incorrect!")]
        public decimal SubCommRate { get; set; }

        [MaxLength(Validation.MaxLengthCommGroupDescription, ErrorMessage = "Description should shorter than 100 characters!")]
        public string Description { get; set; }

        public string CountryCodes { get; set; }

        public short ActiveDays { get; set; }

        public short HoldingDays { get; set; }

        public short HoldingTickets { get; set; }

        public short Quota { get; set; }

        public bool IsInstant { get; set; }

        public string CpaTierList { get; set; }

        public string CpaProductList { get; set; }

        public decimal QMTurnover { get; set; }

        public bool IsActivePlayerSum { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Deal_Ins";

        public override bool IsValid() => true;
    }
}

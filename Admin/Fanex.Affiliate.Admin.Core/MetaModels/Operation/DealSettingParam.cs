namespace Fanex.Affiliate.Admin.Core.MetaModels.Operation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;

    public class DealSettingParam : UserInfoParam
    {
        public int DealType { get; set; }

        public int DealId { get; set; }

        [Required(ErrorMessage = "Group Name is required!")]
        public string DealName { get; set; }

        [Range(Validation.Zero, Validation.MaxPercentage, ErrorMessage = "Sub-Affiliate Rate is incorrect!")]
        public decimal SubCommRate { get; set; }

        [MaxLength(Validation.MaxLengthCommGroupDescription, ErrorMessage = "Description should shorter than 100 characters!")]
        public string Description { get; set; }

        public string CountryCodes { get; set; }

        public int ActiveDays { get; set; }

        public int HoldingDays { get; set; }

        public int HoldingTickets { get; set; }

        public int Quota { get; set; }

        public bool CheckedMinFdm { get; set; }

        public bool CheckedMinTotalDeposit { get; set; }

        public bool CheckedMinTurnover { get; set; }

        public bool IsInstant { get; set; }

        public IEnumerable<CpaTierSetting> CPATierSetting { get; set; }

        public IEnumerable<CpaGroupProduct> ProductGroup { get; set; }
    }
}

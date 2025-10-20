namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    using Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MarketingStatisticsParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int AffId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LanguageCodes { get; set; }

        public bool IncludedInActive { get; set; }
    }
}

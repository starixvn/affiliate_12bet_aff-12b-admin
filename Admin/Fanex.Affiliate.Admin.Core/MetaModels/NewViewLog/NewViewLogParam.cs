using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.NewViewLog
{
    public class NewViewLogParam
    {
        public int AffId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AffName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LogActionIds { get; set; }

        public int UserId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Range(Validation.MinPageIndex, Validation.MaxPageIndex)]
        public int PageIndex { get; set; }

        [Range(Validation.MinPageSize, Validation.MaxPageSize)]
        public int PageSize { get; set; }
    }
}

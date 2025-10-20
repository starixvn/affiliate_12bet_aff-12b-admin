using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class SubAffiliateParam : AffiliatePagingParam
    {
        [Required]
        public DateTime MonthDate { get; set; }

        public int CurrencyId { get; set; }
    }
}
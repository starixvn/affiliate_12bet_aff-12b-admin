using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class PlayerDetailParam : AffiliatePagingParam
    {
        public byte ProductId { get; set; }

        [Required]
        public DateTime? MonthDate { get; set; }

        public bool IsExportExcel { get; set; }
    }
}

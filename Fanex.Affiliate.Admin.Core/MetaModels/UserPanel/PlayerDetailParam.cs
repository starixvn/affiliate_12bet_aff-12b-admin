using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class PlayerDetailParam
    {
        public int AffId { get; set; }

        public string AffName { get; set;  }

        public byte ProductId { get; set; }

        public DateTime? MonthDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsExportExcel { get; set; }
    }
}

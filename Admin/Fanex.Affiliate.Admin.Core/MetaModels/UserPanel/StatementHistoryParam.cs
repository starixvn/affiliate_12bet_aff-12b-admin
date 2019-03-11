using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class StatementHistoryParam
    {
        public int AffId { get; set; }

        public int TransType { get; set; } = -1;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsGetAll { get; set; }
        
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersParam
    {
        public int AffId { get; set; }

        public int DeviceType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SiteURL { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

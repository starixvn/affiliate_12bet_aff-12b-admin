using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.MetaModels.GPI
{
    public class GpiParam
    {
        public string VendorName { get; set; }

        public GameType Game { get; set; }

        public DateTime SDate { get; set; }

        public DateTime EDate { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public string Password { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string MemberId { get; set; }
    }
}

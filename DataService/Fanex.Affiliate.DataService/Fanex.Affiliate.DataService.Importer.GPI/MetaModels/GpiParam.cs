using Fanex.Affiliate.DataService.Importer.Core.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Enum;
using System;

namespace Fanex.Affiliate.DataService.Importer.Gpi.MetaModels
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

using Fanex.Affiliate.DataService.Importer.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.LBKeno.MetaModels
{
    public class LBKenoParam
    {
        public string SecretKey { get; set; }

        public string OperatorId { get; set; }

        public string SiteCode { get; set; }

        public string ProductCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TransactionStatus Status { get; set; }
    }
}

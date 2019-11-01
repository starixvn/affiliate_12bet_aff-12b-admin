using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.MetaModels.FlowGaming
{
    public class FlowGamingParam
    {
        public string BrandId { get; set; }

        public string BrandPassword { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string UtcTimeZoneOffSet { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.ThirdPartyProduct.MetaModels
{
    public class ImporterBaseParam : RequestBaseParam
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}

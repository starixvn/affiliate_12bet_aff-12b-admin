using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.IMOne
{
    public static class Constants
    {
        public static string IMOneDateTimeFormat => "yyyy-MM-dd HH.00.00";
    }
    
    public static class ResultCode
    {
        public const int INVALID_CURRENCY = 507;
        public const int INVALID_PRODUCT_WALLET = 508;
        public const int INVALID_TIMERANGE = 525;
        public const int INVALID_STARTTIME = 526;
        public const int INVALID_TIMEFORMAT = 528;
        public const int SUCCESS = 0;
    }
}

using Fanex.Affiliate.DataService.Importer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class GPIHelper
    {

        public static string GetTransactionStatusName(TransactionStatus status)
        {
            string result = status.ToString();
            return result;
        }
    }
}

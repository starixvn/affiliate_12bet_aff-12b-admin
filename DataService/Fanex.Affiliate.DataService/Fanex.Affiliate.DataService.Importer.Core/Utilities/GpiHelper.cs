using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class GpiHelper
    {

        public static string GetTransactionStatusName(TransactionStatus status)
        {
            string result = status.ToString();
            return result;
        }

        public static string GetGameTypeName(GameType gameType)
        {
            return gameType.ToString().ToLower();
        }
    }
}

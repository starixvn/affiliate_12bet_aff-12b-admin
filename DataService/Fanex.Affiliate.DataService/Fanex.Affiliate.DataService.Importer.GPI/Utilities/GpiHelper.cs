using Fanex.Affiliate.DataService.Importer.Core.Enum;
using Fanex.Affiliate.DataService.Importer.Gpi.Enum;
using System.Globalization;
using System.Text;

namespace Fanex.Affiliate.DataService.Importer.Gpi.Utilities
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
            return gameType.ToString().ToLower(CultureInfo.InvariantCulture);
        }

        public static string BuildJsonString(string data)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append("transactions")
                .Append(":")
                .Append(data)
                .Append("}");
            return builder.ToString();
        }
    }
}

using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class AppContact
    {
        public const string Telegram = "Telegram";
        public const string WhatsApp = "WhatsApp";
        public const string Line = "Line";
        private const string DefaultRegexValue = "^.*$";

        private static readonly IReadOnlyDictionary<string, string> RegexValidations = new Dictionary<string, string>
        {
            [Telegram] = "^@.+$",
            ["Line"] = "^@.+$",
            [WhatsApp] = "^\\+(880|91|62)\\d{6,}$",
        };

        private static readonly IReadOnlyDictionary<string, string> PlaceHolders = new Dictionary<string, string>
        {
            [Telegram] = "@XXXXXXXXXX",
            ["Line"] = "@XXXXXXXXXX",
            [WhatsApp] = "+XXXXXXXXXX",
        };

        public static string GetRegexByApp(string contactAppName)
        {
            if (string.IsNullOrEmpty(contactAppName))
            {
                return DefaultRegexValue;
            }
            return GetValueDicByAppName(RegexValidations, contactAppName);
        }

        public static string GetPlaceHolderByApp(string contactAppName)
        {
            if (string.IsNullOrEmpty(contactAppName))
            {
                return string.Empty;
            }
            return GetValueDicByAppName(PlaceHolders, contactAppName);
        }

        private static string GetValueDicByAppName(
            IReadOnlyDictionary<string, string> dictionary,
            string contactAppName,
            string defaultValue = "")
        {
            if (dictionary.TryGetValue(contactAppName, out var value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}

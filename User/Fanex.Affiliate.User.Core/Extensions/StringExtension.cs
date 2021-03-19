namespace Fanex.Affiliate.User.Core.Extensions
{
    public static class StringExtension
    {
        private const char Asterisks = '*';
        private const int ShortValueLength = 1;
        private const int CharLengthToShowOfMediumValue = 1;
        private const int CharLengthToShowOfLongValue = 2;
        private const int MinLongValueLength = 3;

        public static bool MatchWith(this string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, System.StringComparison.OrdinalIgnoreCase);
        }

        public static string HideStringPart(this string value, bool hasPermission)
        {
            if (hasPermission)
            {
                return value;
            }

            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Length == ShortValueLength)
            {
                return Asterisks.ToString();
            }

            if (value.Length > MinLongValueLength)
            {
                return ReplaceValuePart(value, CharLengthToShowOfLongValue);
            }

            return ReplaceValuePart(value, CharLengthToShowOfMediumValue);
        }

        private static string ReplaceValuePart(string value, int charLengthToShow)
        {
            var length = value.Length;
            var hiddenCharCount = length - charLengthToShow;
            var stringReplace = new string(Asterisks, hiddenCharCount);
            var convertedValue = value.Remove(charLengthToShow).Insert(charLengthToShow, stringReplace);

            return convertedValue;
        }
    }
}

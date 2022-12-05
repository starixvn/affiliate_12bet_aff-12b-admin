namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class StringExtension
    {
        private const char Asterisks = '*';
        public static string ReplaceAHalfOfString(this string value, char character = Asterisks)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var length = value.Length;
            var halfLength = length / 2;
            var hiddenCharCount = length - halfLength;
            var stringReplace = new string(character, hiddenCharCount);
            var convertedValue = value.Remove(halfLength).Insert(halfLength, stringReplace);

            return convertedValue;
        }

        public static bool MatchWith(this string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}

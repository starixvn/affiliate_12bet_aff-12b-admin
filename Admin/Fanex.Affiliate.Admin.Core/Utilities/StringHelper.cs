namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class StringHelper
    {
        private const char Asterisks = '*';
        private const int ShortValueLength = 1;
        private const int CharLengthToShowOfMediumValue = 1;
        private const int CharLengthToShowOfLongValue = 2;
        private const int MinLongValueLength = 3;
        public static string HideStringPart(string value)
        {
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
            var convertedValue = value.Remove(charLengthToShow)
                                                .Insert(charLengthToShow, stringReplace);

            return convertedValue;
        }
    }
}

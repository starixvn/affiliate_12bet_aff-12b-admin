using System;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class StringExtension
    {
        private const char Asterisks = '*';
        private const int MandatoryLengthForSplittingEmail = 2;

        public static bool MatchWith(this string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }

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

        public static string HideAnEmailPart(this string email, int emailLengthToCheck = 8)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            var emailParts = email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            if (emailParts.Length != MandatoryLengthForSplittingEmail)
            {
                return string.Empty;
            }

            var emailName = emailParts[0];
            var emailSuffix = emailParts[1];

            var newEmailName = emailName.Length > emailLengthToCheck ?
                ReplaceEmailName(emailName, 4, 1) :
                ReplaceEmailName(emailName, 1, 0);

            return newEmailName + "@" + emailSuffix;
        }

        private static string ReplaceEmailName(string emailName, int charLengthToShow, int startIndex)
        {
            var hiddenCharCount = emailName.Length - charLengthToShow;
            var stringReplace = new string(Asterisks, hiddenCharCount);
            var convertedEmailName = emailName
                .Remove(startIndex, hiddenCharCount)
                .Insert(startIndex, stringReplace);

            return convertedEmailName;
        }
    }
}

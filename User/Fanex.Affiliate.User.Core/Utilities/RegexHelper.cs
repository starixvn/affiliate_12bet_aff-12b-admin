using System;
using System.Text.RegularExpressions;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class RegexHelper
    {
        private const int TimeOutSecond = 2;

        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.None, TimeSpan.FromSeconds(TimeOutSecond));
        }

        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions.None, TimeSpan.FromSeconds(TimeOutSecond));
        }

        public static Match Match(string input, string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            return Regex.Match(input, pattern, regexOptions, TimeSpan.FromSeconds(TimeOutSecond));
        }

        public static bool IsMatch(string input, string pattern, RegexOptions regexOptions)
        {
            return Regex.IsMatch(input, pattern, regexOptions, TimeSpan.FromSeconds(TimeOutSecond));
        }
    }
}

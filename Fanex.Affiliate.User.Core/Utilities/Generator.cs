using System;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class Generator
    {
        private static readonly Random _random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetRandomString(int length)
        {
            var stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = Chars[_random.Next(Chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
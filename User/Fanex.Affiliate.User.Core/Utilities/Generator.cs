using System;
using System.Security.Cryptography;
using System.Text;

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

        public static string GenerateSHA512String(string inputString)
        {
            var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha512.ComputeHash(bytes);

            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
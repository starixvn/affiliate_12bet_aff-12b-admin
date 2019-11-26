using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affiliate.ThirdPartyProduct.Helper
{
    internal class UserHelper
    {
        private static Dictionary<string, string> _userManager => new Dictionary<string, string>()
        {
            { "god", "god@nexcel" },
            { "spatar", "team@123456" }
        };
        public static bool IsValidateUser(string userId, string password)
        {
            return _userManager.Any(item => item.Key == userId && item.Value == password);
        }
    }
}
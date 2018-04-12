using System;
using System.Collections.Generic;
using System.Configuration;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class AppSetting : IAppSetting
    {
        public string Environment => GetValue("Environment");

        public bool IsLocal => Environment.Equals("Local", StringComparison.OrdinalIgnoreCase);

        public bool IsProduction => Environment.Equals("Production", StringComparison.OrdinalIgnoreCase);

        public int ServerId => Convert.ToInt32(GetValue("ServerId"));

        public IEnumerable<string> AllowedAffiliateTypes => GetValue("AllowedAffType").Split(',');

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
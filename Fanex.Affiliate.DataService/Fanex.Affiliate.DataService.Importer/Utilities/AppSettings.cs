namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class AppSettings : IAppSettings
    {
        public bool IsLocalTest => GetValue("IsLocalTest") == "1";

        public IList<string> SupportedProductList => GetValue("SupportedProductList").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}

using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    public static class AppSettingExtention
    {
        public static IEnumerable<string> GetSupportedKYGamingCurrency(this IAppSettings appSetting)
        {
            return appSetting.GetValue("KYGamingCurrencies").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.IMOne.Extension
{
    public static class AppSettingExtension
    {
        public static IEnumerable<string> GetSupportedVrLotteryCurrency(this IAppSettings appSetting)
        {
            return appSetting.GetValue("VrLotteryCurrencies").Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

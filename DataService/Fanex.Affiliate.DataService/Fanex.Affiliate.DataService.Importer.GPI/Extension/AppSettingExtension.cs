using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;


namespace Fanex.Affiliate.DataService.Importer.Gpi.Extension
{
    public static class AppSettingExtension
    {
        public static IEnumerable<string> GetGpiLotteryGame(this IAppSettings setting)
        {
            return setting.GetValue("GpiLotteryGameType").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

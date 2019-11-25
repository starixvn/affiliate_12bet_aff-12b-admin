using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Gpi.Extension
{
    public static class AppSettingExtension
    {
        public static IEnumerable<string> GetGpiLotteryGame(this IAppSettings setting)
        {
            return setting.GetValue("GpiLotteryGameType").Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

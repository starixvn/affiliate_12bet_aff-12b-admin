using Fanex.Affiliate.DataService.Importer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.IMOne.Extension
{
    public static class AppSettingExtension
    {
        public static IEnumerable<string> GetSupportedVrLotteryCurrency(this IAppSettings appSetting)
        {
            return appSetting.GetValue("VrLotteryCurrencies").Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

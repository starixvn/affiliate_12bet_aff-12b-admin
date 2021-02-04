using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class Menu
    {
        private static readonly IDictionary<string, string> MainMenuList = new Dictionary<string, string>
        {
            ["Home"] = Function.HomePage,
            ["Customer"] = Function.Customer,
            ["Affiliate"] = Function.Affiliate,
            ["Member"] = Function.AffMember,
            ["Report"] = Function.AffReport,
            ["MediaGallery"] = Function.Media,
            ["Message"] = Function.HotNews,
            ["ExchangeRate"] = Function.AffExRate,
            ["AdminRole"] = Function.UserControl,
            ["Domain"] = Function.Domain,
            ["AccessDenied"] = string.Empty
        };

        public static IDictionary<string, string> MainMenus => MainMenuList;
    }
}

using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class Menu
    {
        private static readonly IDictionary<string, string> MainMenuList = new Dictionary<string, string>
        {
            ["Home"] = Function.HomeMenu,
            ["Customer"] = Function.CustomerMenu,
            ["Affiliate"] = Function.AffiliateMenu,
            ["Member"] = Function.MemberMenu,
            ["Report"] = Function.ReportMenu,
            ["MediaGallery"] = Function.MediaGalleryMenu,
            ["Message"] = Function.MessageMenu,
            ["ExchangeRate"] = Function.ExchangeRateMenu,
            ["AdminRole"] = Function.AdminRoleMenu,
            ["Domain"] = Function.DomainMenu,
            ["AccessDenied"] = Function.AccessDenied
        };

        public static IDictionary<string, string> MainMenus => MainMenuList;
    }
}

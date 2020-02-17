using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public interface IAppSettings
    {
        bool IsLocalTest { get; }

        IList<string> SupportedProductList { get; }

        IList<string> ProductListHaveDailyJob { get; }

        string GetValue(string key);
    }
}
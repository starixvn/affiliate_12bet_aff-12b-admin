using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IAppSettings
    {
        bool IsLocalTest { get; }

        IList<string> SupportedProductList { get; }

        string GetValue(string key);
    }
}
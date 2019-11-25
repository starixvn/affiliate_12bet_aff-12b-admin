using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;

namespace Fanex.Affiliate.DataService.Importer.Ebet.Utilities
{
    public interface IEbetConfigHelper
    {
        IList<EbetApiConfig> GetConfigList();
    }
}

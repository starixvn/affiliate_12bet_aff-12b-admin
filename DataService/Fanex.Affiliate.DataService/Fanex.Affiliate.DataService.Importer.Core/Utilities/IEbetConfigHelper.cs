using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IEbetConfigHelper
    {
        IList<EbetApiConfig> GetConfigList();
    }
}

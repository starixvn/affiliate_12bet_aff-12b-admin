using System;
using System.Collections.Generic;
using System.IO;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;
using Newtonsoft.Json;

namespace Fanex.Affiliate.DataService.Importer.Ebet.Utilities
{
    public class EbetConfigHelper : IEbetConfigHelper
    {
        public IList<EbetApiConfig> GetConfigList()
        {
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\EBetConfig.json");
            var jsonData = string.Join(string.Empty, File.ReadAllLines(filePath));

            return JsonConvert.DeserializeObject<IList<EbetApiConfig>>(jsonData);
        }
    }
}

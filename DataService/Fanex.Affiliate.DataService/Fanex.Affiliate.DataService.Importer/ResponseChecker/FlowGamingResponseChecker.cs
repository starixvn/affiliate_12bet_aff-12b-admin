using Fanex.Affiliate.DataService.Importer.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.ResponseChecker
{
    public class FlowGamingResponseChecker : IThirdPartyResponseChecker
    {
        public void CheckData(string data)
        {
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var status = dic["status"];
            if (string.IsNullOrEmpty(status))
                throw new ThirdPartyProductException($"FlowGaming - Cant find status field \n Wrong response data format {data}");
            if (status.Contains("FAIL"))
            {
                var message = dic["message"];
                var error = dic["errors"];
                throw new ThirdPartyProductException($"FlowGaming - Fail request {message} - {error}");
            }
        }
    }
}

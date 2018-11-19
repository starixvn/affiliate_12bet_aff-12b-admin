using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public class QuartzService : IQuartzService
    {
        private readonly IAppSettings _appSetting;

        public QuartzService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public async Task<IEnumerable<string>> GetRawDataList(QuartzParam param)
        {
            var rawData = await GetRawDataAsync(param);
            throw new NotImplementedException();
        }

        public async Task<string> GetRawDataAsync(QuartzParam param)
        {
            var url = _appSetting.GetValue("QuartzApiUrl");
            var jsonBody = BuildBodyParam(param);

            using (var client = new WebClient())
            {
                return await client.UploadStringTaskAsync(url, jsonBody);
            }
        }

        private string BuildBodyParam(QuartzParam param)
        {
            var jAction = new JObject(
                new JProperty("command", "wagersFeed"),
                new JProperty("parameters", new JObject(
                        new JProperty("datetime", param.RequestTime.ToString(_appSetting.GetValue("QuartzFormatDateTime"))),
                        new JProperty("page", param.PageNumber)
                    ))
                );

            return new JObject(
                    new JProperty("operator", "12Bet"),
                    new JProperty("action", jAction)
                ).ToString();
        }
    }
}
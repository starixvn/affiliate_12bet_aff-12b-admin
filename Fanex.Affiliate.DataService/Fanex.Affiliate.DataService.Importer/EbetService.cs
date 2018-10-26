using System;
using System.Net;
using System.Text;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class EbetService : IEbetService
    {
        public EbetService()
        {
        }

        public string GetJsonData(EbetApiConfig apiConfig, EbetParam param)
        {
            param.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            param.Signature = RsaHelper.SignAndHash($"{param.UserName}{param.TimeStamp}", apiConfig.PrivateKey);
            var url = $"{Uri.UriSchemeHttp}{Uri.SchemeDelimiter}{apiConfig.ApiServerHost}/api/userbethistory";

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webClient.QueryString.Add("startTimeStr", param.StartTime);
                webClient.QueryString.Add("endTimeStr", param.EndTime);
                webClient.QueryString.Add("channelId", apiConfig.ChannelId);
                webClient.QueryString.Add("subChannelId", param.SubChanelId.ToString());
                webClient.QueryString.Add("pageNum", param.PageNumber.ToString());
                webClient.QueryString.Add("pageSize", param.PageSize.ToString());
                webClient.QueryString.Add("timestamp", param.TimeStamp.ToString());
                webClient.QueryString.Add("signature", param.Signature);
                webClient.QueryString.Add("username", param.UserName);

                var data = webClient.UploadValues(url, "POST", webClient.QueryString);

                return Encoding.UTF8.GetString(data);

                //return webClient.UploadString(url, queryString);
            }

            //return url + "?" + queryString;
        }
    }
}

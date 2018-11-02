using System;
using System.Net;
using System.Text;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer
{
    public class EbetService : IEbetService
    {
        public readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;

        public EbetService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public string GetJsonData(EbetParam param)
        {
            param.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            param.Signature = RsaHelper.SignAndHash($"{param.UserName}{param.TimeStamp}", param.PrivateKey);
            var url = $"{Uri.UriSchemeHttp}{Uri.SchemeDelimiter}{param.ApiUrl}/api/userbethistory";

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webClient.QueryString.Add("startTimeStr", param.StartTime.ToString(_appSetting.EbetFormatDateTime));
                webClient.QueryString.Add("endTimeStr", param.EndTime.ToString(_appSetting.EbetFormatDateTime));
                webClient.QueryString.Add("channelId", param.ChannelId);
                webClient.QueryString.Add("subChannelId", param.SubChanelId.ToString());
                webClient.QueryString.Add("pageNum", param.PageNumber.ToString());
                webClient.QueryString.Add("pageSize", param.PageSize.ToString());
                webClient.QueryString.Add("timestamp", param.TimeStamp.ToString());
                webClient.QueryString.Add("signature", param.Signature);
                webClient.QueryString.Add("username", param.UserName);

                var data = webClient.UploadValues(url, "POST", webClient.QueryString);

                return Encoding.UTF8.GetString(data);
            }
        }

        public EbetSummary ConvertDataFromJson(string jsonData)
        {
            var result = EbetParser.FromJson(jsonData);

            return result;
        }

        public string ConvertToXml(EbetSummary summaryData, EbetParam param)
        {
            var builder = new StringBuilder();

            return builder.ToString();
        }

        public void Import(string xmlData)
        {
            _thirdPartyProductRepository.ImportData(Products.EbetProductId, xmlData);
        }
    }
}

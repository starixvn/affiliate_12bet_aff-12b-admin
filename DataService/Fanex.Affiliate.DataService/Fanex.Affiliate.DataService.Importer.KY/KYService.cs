using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.KYGaming.MetaModels;
using Fanex.Affiliate.DataService.Importer.KYGaming.Utilities;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    public class KYService : IKYService
    {
        private const int DelayTimeMillisecond = 30000;

        private readonly IAppSettings _appSetting;

        private readonly IThirdPartyProductRepository _thirdPartyProductRepository;


        public KYService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        public IEnumerable<string> GetRawDataList(KYApiParam param)
        {
            var result = new List<string>();

            throw new NotImplementedException();
        }

        private KYApiParam BuildParam(KYApiParam param)
        {
            param.Agent = KYHelper.GetAgent(param.Currency);
            param.Deskey = KYHelper.GetDESKey(param.Currency);
            param.Md5Key = KYHelper.GetMd5Key(param.Currency);

            //param.EncryptedParam = $"s={_appSetting.GetValue("KYOperateSubType")}&startTime={param.StartTimeString}&endTime={param.EndTimeString}";

            return param;
        }

        private string GetRawData(KYApiParam param)
        {
            var apiUrl = BuildApiUrl(param);
            Thread.Sleep(DelayTimeMillisecond);

            using (var client = new WebClient())
            {
                return client.DownloadString(apiUrl);
            }
        }

        private string BuildApiUrl(KYApiParam param)
        {
            return _appSetting.GetValue("KYGamingApiUrl") + $"?agent={param.Agent}&timestamp={param.TimeStampString}&param={param.EncryptedParam}&key={param.Key}";
        }

        public void Import(string xmlData)
        {
            throw new NotImplementedException();
        }

        public void Import(KYApiParam param)
        {
            throw new NotImplementedException();
        }
    }
}

using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.BigGaming
{
    public class BgFishService : XmlImporterlServiceBase
    {
        protected override int ProductId => Products.BgFish;
        protected readonly IApiClientFactory _apiClientFactory;

        public BgFishService(IApiClientFactory apiClientFactory = null, IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null)
            : base(appSetting, thirdPartyProductRepository)
        {
            _apiClientFactory = apiClientFactory ?? new ApiClientFactory();
        }

        public override Task<XDocument> ConvertRawDataToXmlAsync(string raw)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            throw new NotImplementedException();
        }
    }
}

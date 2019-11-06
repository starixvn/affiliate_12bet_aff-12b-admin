using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Importer.Exceptions;
using Fanex.Affiliate.DataService.Importer.Constants;

namespace Fanex.Affiliate.DataService.Importer
{
    public class ImOneVrLotteryService : XmlImpoterlServiceBase
    {
        private readonly TimeSpan MAX_REQUEST_TIMESPAN = new TimeSpan(31, 0, 0, 0);

        public ImOneVrLotteryService(IAppSettings appSetting = null, IThirdPartyProductRepository thirdPartyProductRepository = null) : base(appSetting, thirdPartyProductRepository)
        {
        }

        public override IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime)
        {
            DateTime startTimeByHour = GetRoundByHour(startTime);
            DateTime endTimeByHour = GetRoundByHour(endTime);
            EnsureEndTimeLaterThanStartTime(startTimeByHour, endTimeByHour);
            throw new NotImplementedException();
        }

        private DateTime GetRoundByHour(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, 0);
        }

        public override void Import(string data)
        {
            _thirdPartyProductRepository.ImportData(Products.VRLotteryId, data, 0);
        }

        protected override void CheckXmlResponseData(XDocument xml)
        {
            throw new NotImplementedException();
        }

        protected override XDocument ConvertToXml(string data)
        {
            throw new NotImplementedException();
        }
    }
}

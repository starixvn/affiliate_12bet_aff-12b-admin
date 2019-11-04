using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer
{
    public abstract class ImporterXmlServiceBase : IImporterService
    {
        protected IAppSettings _appSetting;
        protected IThirdPartyProductRepository _thirdPartyProductRepository;

        protected ImporterXmlServiceBase(IAppSettings appSetting, IThirdPartyProductRepository thirdPartyProductRepository)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }
        public abstract IEnumerable<string> GetRawDataList(DateTime startTime, DateTime endTime);

        public IEnumerable<string> GetXmlDataList(DateTime startTime, DateTime endTime)
        {
            var rawDatas = GetRawDataList(startTime, endTime);
            foreach (var data in rawDatas)
            {
                var xml = ConvertToXml(data);
                CheckXmlResponseData(xml);
                yield return xml.ToString();
            }
        }

        protected abstract void CheckXmlResponseData(XDocument xml);

        protected abstract XDocument ConvertToXml(string data);

        public abstract void Import(string data);

        public void Import(DateTime startTime, DateTime endTime)
        {
            var xmls = GetXmlDataList(startTime, endTime);
            foreach (var xml in xmls)
            {
                Import(xml);
            }
        }
        
        protected void EnsureEndTimeLaterThanStartTime(DateTime startTime, DateTime endTime)
        {
            if (DateTime.Compare(startTime, endTime) >= 0)
                throw new ArgumentException($"Start date {startTime} is later or equal than end date: {endTime}");
        }
    }
}

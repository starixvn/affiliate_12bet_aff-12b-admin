using Fanex.Affiliate.DataService.Importer.Core.Repository;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public abstract class XmlImpoterlServiceBase : IImporterService
    {
        protected IAppSettings _appSetting;
        protected IThirdPartyProductRepository _thirdPartyProductRepository;

        protected abstract int ProductId { get; }

        public byte ImportType { get; set; } = 0;

        public virtual bool IsNeedConvertTime  => false;
        public virtual int HoursAjust => 0;


        protected XmlImpoterlServiceBase(IAppSettings appSetting, IThirdPartyProductRepository thirdPartyProductRepository)
        {
            _appSetting = appSetting ?? new AppSettings();
            _thirdPartyProductRepository = thirdPartyProductRepository ?? new ThirdPartyProductRepository();
        }

        protected abstract void CheckXmlResponseData(XDocument xml);

        protected virtual bool CheckIsSuitableToImport(XDocument xml)
        {
            return true;
        }

        protected virtual void EnsureEndTimeLaterThanStartTime(DateTime startTime, DateTime endTime)
        {
            if (DateTime.Compare(startTime, endTime) >= 0)
                throw new ArgumentException($"Start date {startTime} is later or equal than end date: {endTime}");
        }

        public async Task<IEnumerable<XDocument>> GetXmlDataListAsync(DateTime startTime, DateTime endTime)
        {
            EnsureEndTimeLaterThanStartTime(startTime,endTime);
            var rawDatas = await GetRawDataListAsync(startTime, endTime).ConfigureAwait(false);
            var xmlDatas = new List<XDocument>(rawDatas.Count());
            foreach (var data in rawDatas)
            {
                var xml = await ConvertRawDataToXmlAsync(data);
                CheckXmlResponseData(xml);
                xmlDatas.Add(xml);
            }
            return xmlDatas;
        }

        public abstract Task<IEnumerable<string>> GetRawDataListAsync(DateTime startTime, DateTime endTime);

        public abstract Task<XDocument> ConvertRawDataToXmlAsync(string raw);

        public async Task ImportAsync(DateTime startTime, DateTime endTime)
        {
            var xmls = await GetXmlDataListAsync(startTime, endTime).ConfigureAwait(false);
            xmls = xmls.Where(xml => CheckIsSuitableToImport(xml));

            if (IsNeedConvertTime)
                xmls = xmls.Select(xml => ConvertTimeZone(xml));

            await ImportSuitableDataAsync(xmls.Select(xml => xml.ToString()));
        }

        protected virtual XDocument ConvertTimeZone(XDocument xml)
        {
            return xml;
        }

        public virtual async Task ImportAsync(string data)
        {
            await _thirdPartyProductRepository.ImportDataAsync(ProductId, data, ImportType);
        }

        public virtual async Task ImportAsync(IEnumerable<string> datas)
        {
            var xmls = datas.Select(data => XmlHelper.ParseToXDocument(data))
                .Where(xml => CheckIsSuitableToImport(xml));

            if (IsNeedConvertTime)
                xmls = xmls.Select(xml => ConvertTimeZone(xml));

            await ImportSuitableDataAsync(xmls.Select(xml => xml.ToString()));

        }

        private async Task ImportSuitableDataAsync(IEnumerable<string> datas)
        {
            await _thirdPartyProductRepository.ImportDataAsync(ProductId, datas, ImportType);
        }


    }
}

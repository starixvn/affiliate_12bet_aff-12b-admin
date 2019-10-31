using Fanex.Affiliate.DataService.Importer.Repository;
using Fanex.Affiliate.DataService.Importer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract IEnumerable<string> GetRawDataList(DateTime startDate, DateTime endDate);

        public IEnumerable<string> GetXmlDataList(DateTime startDate, DateTime endDate)
        {
            var xmls = new List<string>();
            var rawDatas = GetRawDataList(startDate, endDate);
            foreach (var data in rawDatas)
            {
                var xml = ConvertToXmlString(data);
                xmls.Add(xml);
            }
            return xmls;
        }

        protected abstract string ConvertToXmlString(string data);

        public virtual void Import(string data)
        {
            throw new NotImplementedException();
        }

        public void Import(DateTime startDate, DateTime endDate)
        {
            var xmls = GetXmlDataList(startDate, endDate);
            foreach (var xml in xmls)
            {
                Import(xml);
            }
        }
    }
}

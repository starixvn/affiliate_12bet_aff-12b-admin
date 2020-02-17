using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.WinFormApp.Converter
{
    public class LBKenoFileImporter :IFileImporter
    {
        private IImporterService Service;
        private IDataSpliter Spliter;
        public LBKenoFileImporter(IImporterService service = null, IDataSpliter spliter = null)
        {
            Service = service ?? new LBKenoService();
            Spliter = spliter ?? new WebApiSpliter();
        }

        public Task ImportAsync(IEnumerable<string> xml)
        {
            return Service.ImportAsync(xml);
        }

        public Task<IEnumerable<XDocument>> ConvertToXml(string data)
        {
            return ConvertToXml(data, Spliter);
        }

        public async Task<IEnumerable<XDocument>> ConvertToXml(string data, IDataSpliter spliter)
        {
            var rawDatas = spliter.Split(data);
            var xmls = new List<XDocument>();
            foreach (var raw in rawDatas)
            {
                if (!String.IsNullOrWhiteSpace(raw))
                {
                    var xml = await Service.ConvertRawDataToXmlAsync(raw);
                    xmls.Add(xml);
                }
            }
            return xmls;
        }
    }
}

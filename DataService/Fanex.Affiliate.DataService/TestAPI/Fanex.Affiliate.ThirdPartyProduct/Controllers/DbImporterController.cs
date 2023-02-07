using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class DbImporterController : Controller
    {
        private IImporterService _service;

        protected static TimeSpan PullRange => new TimeSpan(0, 1, 0, 0);

        public DbImporterController()
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/DbImporter/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(TimeTrackerParam param)
        {
            var result = new List<TimeTracker>();
            var timeTracker = new TimeTracker { Currency = 111, CallTime = DateTime.Now };

            try
            {
                _service = GetImporterService(param.ProductId);
                LogHelper.Info("Start import data from UAT web " + _service.GetType().Name);
                var timeRanges = DateTimeHelper.GetProportionTimeRange(param.FromDate, param.ToDate, PullRange);
                foreach (var range in timeRanges)
                {
                    LogHelper.Info($"Import from {range.Item1} to {range.Item2}");
                    var rawDatas = await _service.GetRawDataListAsync(range.Item1, range.Item2);
                    var xmlDatas = await ConvertToXmlAsync(rawDatas);
                    await _service.ImportAsync(xmlDatas.Select(xml => xml.ToString()));
                }
                LogHelper.Info("Finished import data from UAT web");
            }
            catch (Exception ex)
            {
                timeTracker.Currency = -999;
                LogHelper.Error(ex);
            }

            result.Add(timeTracker);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        private IImporterService GetImporterService(int productId)
        {
            return SeviceSystem.ServiceList[productId].Value;
        }

        private async Task<IEnumerable<XDocument>> ConvertToXmlAsync(IEnumerable<string> rawDatas)
        {
            var xmlDatas = new List<XDocument>();
            foreach (var data in rawDatas)
            {
                xmlDatas.Add(await _service.ConvertRawDataToXmlAsync(data));
            }
            return xmlDatas;
        }
    }
}
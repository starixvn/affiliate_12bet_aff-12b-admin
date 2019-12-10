using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class BetsoftController : Controller
    {
        private readonly IBetsoftService _betsoftService;

        public BetsoftController()
        {
            _betsoftService = new BetsoftService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Betsoft/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(BetsoftRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();
            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

                if (param.IsGetByApiTimeZone)
                {
                    startTime = DateTimeConverter.ToGMT(startTime, TimeZoneInfo.Local.Id);
                    endTime = DateTimeConverter.ToGMT(endTime, TimeZoneInfo.Local.Id);
                }

                var betSoftParam = new BetsoftParam
                {
                    StartDate = startTime,
                    EndDate = endTime
                };

                var rawDataList = await _betsoftService.GetMultipleRawDataBatchAsync(betSoftParam).ConfigureAwait(false);
                if (rawDataList.Any())
                {
                    param.UserName = param.UserName?.Trim();
                    if (!string.IsNullOrEmpty(param.UserName))
                    {
                        rawDataList = GetDataByUserName(rawDataList, param.UserName);
                    }

                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
            }

            return Content(JsonConvert.SerializeObject(new ThirdPartyProductViewModel
            {
                LogMessage = logBuilder.ToString(),
                RawData = rawDataBuilder.ToString()
            }), "application/json");
        }

        private static IList<string> GetDataByUserName(IList<string> rawDataList, string userName)
        {
            var dataList = new List<string>();
            foreach (var rawData in rawDataList)
            {
                var xdoc = XDocument.Parse(rawData);
                var response = xdoc.Root.Element("RESPONSE");
                var accountList = response.Elements("ACCOUNT");

                accountList.Where(x => !string.Equals(x.Attribute("id").Value, userName, StringComparison.InvariantCultureIgnoreCase))
                    .Remove();

                if (accountList.Any())
                {
                    dataList.Add(xdoc.ToString());
                }
            }

            return dataList;
        }
    }
}

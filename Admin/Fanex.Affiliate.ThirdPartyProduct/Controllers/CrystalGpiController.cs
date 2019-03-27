using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Constants;
using Fanex.Affiliate.DataService.Importer.MetaModels.CrystalGpi;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class CrystalGpiController : Controller
    {
        private readonly ICrystalGpiService _crystalGpiService;

        public CrystalGpiController()
        {
            _crystalGpiService = new CrystalGpiService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/CrystalGpi/Index.cshtml");
        }

        [HttpPost]
        [Compress]
        public ActionResult Index(CrystalGpiRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

                if (param.IsGetByApiTimeZone)
                {
                    startTime = DateTimeConverter.ConvertTime(startTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                    endTime = DateTimeConverter.ConvertTime(endTime, TimeZoneId.GmtPlus8, TimeZoneInfo.Local.Id);
                }

                var crystalGpiParam = new CrystalGpiParam
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize
                };

                var rawDataList = _crystalGpiService.GetRawDataList(crystalGpiParam);

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
                        rawDataBuilder.AppendLine("-----------------------------------------");
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

        private static IEnumerable<string> GetDataByUserName(IEnumerable<string> rawDataList, string userName)
        {
            var dataList = new List<string>();

            foreach (var rawData in rawDataList)
            {
                var xdoc = XDocument.Parse(rawData);
                var betHistoryList = xdoc.Root.Elements("history");

                betHistoryList.Where(x => !string.Equals(x.Attribute("userId").Value, userName, StringComparison.InvariantCultureIgnoreCase))
                    .Remove();

                if (betHistoryList.Any())
                {
                    dataList.Add(xdoc.ToString());
                }
            }

            return dataList;
        }
    }
}
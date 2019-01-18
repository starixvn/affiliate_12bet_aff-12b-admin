using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.CrystalGpi;
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
            var convertedDataBuilder = new StringBuilder();
            var convertedDataList = new List<string>();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
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
                    foreach (var item in rawDataList)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine("-----------------------------------------");
                        rawDataBuilder.AppendLine();
                    }

                    convertedDataList.AddRange(_crystalGpiService.ConvertDataTimeZone(rawDataList));
                    foreach (var item in convertedDataList)
                    {
                        convertedDataBuilder.AppendLine(item);
                        convertedDataBuilder.AppendLine("-----------------------------------------");
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
                RawData = rawDataBuilder.ToString(),
                ConvertedData = convertedDataBuilder.ToString()
            }), "application/json");
        }
    }
}

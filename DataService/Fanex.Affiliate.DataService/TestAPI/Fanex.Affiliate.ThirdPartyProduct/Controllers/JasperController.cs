using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Jasper;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class JasperController : Controller
    {
        private readonly IImporterService _service;
        protected static TimeSpan PullRange => new TimeSpan(1, 0, 0, 0);

        public JasperController()
        {
            _service = new JasperService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Jasper/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(ImporterBaseParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, PullRange);

                foreach (var range in timeRanges)
                {
                    var rawDataList = await _service.GetRawDataListAsync(range.Item1, range.Item2);

                    if (rawDataList.Any())
                    {
                        foreach (var item in rawDataList)
                        {
                            rawDataBuilder.AppendLine(item);
                            rawDataBuilder.AppendLine("-----------------------------------------");
                            rawDataBuilder.AppendLine();
                        }
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
    }
}

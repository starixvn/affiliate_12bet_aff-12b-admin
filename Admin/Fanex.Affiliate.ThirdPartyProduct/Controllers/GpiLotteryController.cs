using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
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
    public class GpiLotteryController : Controller
    {
        private readonly IImporterService GpiLotteryService;

        public GpiLotteryController()
        {
            GpiLotteryService = new GpiLotteryService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/GpiLottery/Index.cshtml");
        }

        [HttpPost]
        public async  Task<ActionResult> Index(GplLotteryRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);

                var rawDataList = await GpiLotteryService.GetRawDataListAsync(startTime, endTime).ConfigureAwait(false);

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

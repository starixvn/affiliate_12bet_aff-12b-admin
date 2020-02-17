using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Opus.MetaModels;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class OpusController : Controller
    {
        private readonly IOpusService _opusService;

        public OpusController()
        {
            _opusService = new OpusService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Opus/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Index(OpusRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var time = Helpers.GetTime(param);
                var opusParam = new OpusParam
                {
                    StartTime = time.Item1,
                    EndTime = time.Item2,
                };
                var rawDataList = _opusService.GetRawDataList(opusParam);

                param.UserName = param.UserName?.Trim();
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    rawDataList = Helpers.GetDataByUserName(rawDataList, param.UserName, true);
                }

                foreach (var item in rawDataList)
                {
                    rawDataBuilder.AppendLine(item);
                    rawDataBuilder.AppendLine("-----------------------------------------");
                }
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
            }

            return Content(Helpers.Convert(logBuilder, rawDataBuilder), "application/json");
        }
    }
}

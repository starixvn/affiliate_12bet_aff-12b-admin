using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.Utilities;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Constants;

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
        public ActionResult Index(CrystalGpiRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();

            try
            {
                var time = Helpers.GetTime(param);
                var crystalGpiParam = new CrystalGpiParam
                {
                    StartTime = time.Item1,
                    EndTime = time.Item2,
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize
                };
                var rawDataList = _crystalGpiService.GetRawDataList(crystalGpiParam);

                if (rawDataList.Any())
                {
                    param.UserName = param.UserName?.Trim();
                    if (!string.IsNullOrEmpty(param.UserName))
                    {
                        rawDataList = Helpers.GetDataByUserName(rawDataList, param.UserName, false);
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

            return Content(Helpers.Convert(logBuilder, rawDataBuilder), "application/json");
        }
    }
}
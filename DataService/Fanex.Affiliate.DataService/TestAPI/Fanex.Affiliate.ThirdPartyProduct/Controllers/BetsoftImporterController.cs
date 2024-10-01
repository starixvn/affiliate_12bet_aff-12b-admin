using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;
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
    public class BetsoftImporterController : Controller
    {
        private readonly IBetsoftService _service;

        protected static TimeSpan PullRange => new TimeSpan(0, 1, 0, 0);

        public BetsoftImporterController()
        {
            _service = new BetsoftService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/BetsoftImporter/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Index(TimeTrackerParam param)
        {
            var result = new List<TimeTracker>();
            var timeTracker = new TimeTracker { Currency = 111, CallTime = DateTime.Now };

            try
            {
                LogHelper.Info("Start import data from UAT web " + _service.GetType().Name);
                
                _service.Import(new BetsoftParam { StartDate = param.FromDate, EndDate = param.ToDate });

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
    }
}
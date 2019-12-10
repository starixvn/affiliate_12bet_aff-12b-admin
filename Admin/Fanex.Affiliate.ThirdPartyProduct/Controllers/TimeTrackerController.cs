using System.Web.Mvc;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.Repository;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class TimeTrackerController : Controller
    {
        private readonly ITimeTrackerRepository _repository;

        public TimeTrackerController()
        {
            _repository = new TimeTrackerRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/TimeTracker/Index.cshtml");
        }

        [HttpPost]
        public ActionResult GetDataList(TimeTrackerParam param)
        {
            return Content(JsonConvert.SerializeObject(_repository.GetDataList(param)), "application/json");
        }

    }
}

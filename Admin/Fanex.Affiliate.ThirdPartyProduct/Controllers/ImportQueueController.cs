using System.Web.Mvc;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.Repository;
using Newtonsoft.Json;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class ImportQueueController : Controller
    {
        private readonly IImportQueueRepository _repository;

        public ImportQueueController()
        {
            _repository = new ImportQueueRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/ImportQueue/Index.cshtml");
        }

        [HttpPost]
        public ActionResult GetDataList(ImportQueueParam param)
        {
            return Content(JsonConvert.SerializeObject(_repository.GetDataList(param)), "application/json");
        }

        [HttpGet]
        public ActionResult XmlData(int queueId)
        {
            var xmlData = _repository.GetXmlData(queueId);

            return View("~/Areas/Portal/Views/ThirdPartyProduct/ImportQueue/XmlData.cshtml", model: xmlData);
        }
    }
}

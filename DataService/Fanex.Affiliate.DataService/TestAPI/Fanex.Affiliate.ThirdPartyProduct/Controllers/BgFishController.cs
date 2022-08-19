using Fanex.Affiliate.DataService.Importer.BigGaming;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class BgFishController : BaseController
    {
        public BgFishController() : base(new BgFishService())
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/BgFish/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(ImporterBaseParam param)
        {
            return await HandleImport(param);
        }
    }
}

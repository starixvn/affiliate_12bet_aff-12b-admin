using Fanex.Affiliate.DataService.Importer.Jasper;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class JasperController : BaseController
    {
        public JasperController(): base(new JasperService())
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Jasper/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(ImporterBaseParam param)
        {
            return await HandleImport(param);
        }
    }
}

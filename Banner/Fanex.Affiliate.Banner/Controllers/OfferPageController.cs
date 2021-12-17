using System.Web.Mvc;

namespace Fanex.Affiliate.Banner.Controllers
{
    public class OfferPageController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Content("OfferPage");
        }

        [HttpGet]
        public ActionResult GetContent(string fileName)
        {
            var filePath = Server.MapPath($"~/CategoryMedia/OfferPage/{fileName}");
            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);
                return Content(content);
            }
            return Content(filePath);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Banner.Controllers
{
    public class NewsLetterController : Controller
    {
        // GET: NewsLetterController
        public ActionResult Index()
        {
            return Content("NewsLetter");
        }

        [HttpGet]
        public ActionResult GetContent(string fileName)
        {
            var filePath = Server.MapPath($"~/CategoryMedia/NewsLetter/{fileName}");
            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);
                return Content(content);
            }
            return Content(filePath);
        }
    }
}
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Fanex.Affiliate.DataService.LocalAPI.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "HelloWord";
        }

        [HttpGet]
        public HttpResponseMessage Download()
        {
            var path = HttpContext.Current.Server.MapPath($"~/App_Data/SampleData/CasinoData.csv");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "FileName.csv"
            };

            return result;
        }
    }
}
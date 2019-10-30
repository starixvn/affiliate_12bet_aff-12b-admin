using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Fanex.Affiliate.DataService.LocalAPI.Controllers
{
    //api/home/
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

        [HttpPost]
        public IHttpActionResult GetGpiLotteryJson(object request)
        {
            object[] array = new object[5];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new
                {
                    OS = "1",
                    actionTime = "2013-05-28T11:20:42.58",
                    areaName = "Beijing",
                    betAmount = " 10.00",
                    betContent = "",
                    betId = "1069",
                    betNo = "130528105853510705",
                    betType = "Small",
                    currency = "IDR",
                    drawId = "10556",
                    drawNo = "1152",
                    isWin = "true",
                    memberId = "FalconCoder",
                    memberType = "CASH",
                    odds = "1.95",
                    status = "settled",
                    timeBet = "2013-05-28T10:58:53.51",
                    winAmount = "19.50",
                    isRNG = "true"
                };
            }

            return Json(array);
        }      
    }
}
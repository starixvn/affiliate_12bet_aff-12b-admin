using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetPageListService
    {
        public void GetPageList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var deviceType = Common.GetDeviceType(context.Request.Params["deviceType"]);

            context.Response.Write(Common.GeneratePagesDropdown("sportCode", deviceType));
        }
    }
}
using System.Data;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class GetDomainListService
    {
        public void GetDomainList(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            var deviceType = Common.GetDeviceType(context.Request.Params["deviceType"]);

            DataTable dtDomain = DataHandler.MediaGallery.SelAffDomains(deviceType);
            string domain = null;
            var result = Common.GenDropDownFromTable(dtDomain, "DomainName", "DomainName", "slDomain", ref domain, false);

            context.Response.Write(result);
        }
    }
}
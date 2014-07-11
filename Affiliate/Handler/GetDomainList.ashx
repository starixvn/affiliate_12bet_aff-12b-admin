<%@ WebHandler Language="C#" Class="GetDomainList" %>

using System.Data;
using System.Web;
using System.Web.SessionState;

public class GetDomainList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.UrlReferrer != null)
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

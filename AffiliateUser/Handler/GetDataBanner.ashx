<%@ WebHandler Language="C#" Class="GetDataBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetDataBanner : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetDataBannerService().GetDataBanner(context);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}

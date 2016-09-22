<%@ WebHandler Language="C#" Class="AddNewBannerV2" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AddNewBannerV2 : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new AddBannerService().AddBanner(context);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}

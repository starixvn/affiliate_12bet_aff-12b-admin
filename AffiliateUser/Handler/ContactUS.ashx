<%@ WebHandler Language="C#" Class="ContactUS" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class ContactUS : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new ContactUsService().ContactUs(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
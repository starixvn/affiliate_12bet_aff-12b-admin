<%@ WebHandler Language="C#" Class="PaymentUpdate" %>

using System.Web.SessionState;
using System.Web;
using AffiliateAdmin.Business.Services;

public class PaymentUpdate : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new PaymentUpdateService().PaymentUpdate(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
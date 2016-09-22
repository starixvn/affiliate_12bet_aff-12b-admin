<%@ WebHandler Language="C#" Class="AddNewText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AddNewText : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new AddNewTextService().AddNewText(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
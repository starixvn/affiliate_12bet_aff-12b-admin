<%@ WebHandler Language="C#" Class="AddUser" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AddUser : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new AddUserService().AddUser(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}